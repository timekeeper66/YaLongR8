#include <map>
#include <vector>
#include <Eigen/Eigen>
#include <opencv2/opencv.hpp>
#include <cv_bridge/cv_bridge.h>
#include <iostream>
#include <fstream>


// 棋盘格检测器类，用于检测棋盘格并计算机器人/相机坐标系转换
class ChessboardDetector_cls
{
private:
    cv::Mat rvec;    // 旋转向量（世界坐标系到相机坐标系的旋转）
    cv::Mat tvec;    // 平移向量（世界坐标系原点到相机坐标系原点的平移）
    
    // 相机内参矩阵 [fx, 0, cx; 0, fy, cy; 0, 0, 1]
    cv::Mat cameraMatrix;
    // 畸变系数 [k1, k2, p1, p2, k3]
    cv::Mat distCoeffs;

    // 棋盘格方块物理尺寸（单位：米，需与实际应用场景一致）
    float squareSize;
    int boardWidth = 7;  // 棋盘格宽度（单位：内角点）
    int boardHeight = 7; // 棋盘格高度（单位：内角点）

    bool cvVerPlus45 = false; // OpenCV版本判断标志
public:
    // 构造函数，初始化相机参数和棋盘格尺寸
    ChessboardDetector_cls(float square_size = 12.5,int nDevType=1)  // 默认1cm方块,1=r1d,0=r8c
    {
        if(nDevType == 0){
            //     ✅ 相机内参矩阵 cameraMatrix:
            //     [[1.04969613e+03 0.00000000e+00 5.73570763e+02]
            //     [0.00000000e+00 1.05086275e+03 4.05877726e+02]
            //     [0.00000000e+00 0.00000000e+00 1.00000000e+00]]
            
            //    ✅ 畸变系数 distCoeffs:
            //     [[-1.85926395e-03  1.61417431e-03  1.14303737e-03  7.58650886e-05
            //      -1.17211371e-03]]
            this->cameraMatrix = (cv::Mat_<double>(3, 3)
                        << 593.52455247, 0, 359.99576897,
                        0, 591.1469869, 231.48422218,
                        0, 0, 1);
            this->distCoeffs = (cv::Mat_<double>(1, 5) 
                        << 0.03468398, 0.44016135, -0.00044522,
                            0.01792056, -1.15909218);
        }else{
            //     ✅ 相机内参矩阵 cameraMatrix:
            //     [[1.05382819e+03 0.00000000e+00 5.72604459e+02]
            //     [0.00000000e+00 1.05425060e+03 4.02170466e+02]
            //     [0.00000000e+00 0.00000000e+00 1.00000000e+00]]
            
            //    ✅ 畸变系数 distCoeffs:
            //     [[ 0.00272168 -0.04679861  0.00091088 -0.00053716  0.14500516]]
            this->cameraMatrix = (cv::Mat_<double>(3, 3)
                        << 1.05382819e+03, 0.00000000e+00, 5.72604459e+02,
                            0.00000000e+00, 1.05425060e+03, 4.02170466e+02,
                            0.00000000e+00, 0.00000000e+00, 1.00000000e+00);
            this->distCoeffs = (cv::Mat_<double>(1, 5)
                        << 0.00272168, -0.04679861, 0.00091088,
                        -0.00053716, 0.14500516);
        }
        
        this->squareSize = square_size;  // 初始化棋盘格方块尺寸

        int major_ver = 0, minor_ver = 0;
        std::sscanf(CV_VERSION, "%d.%d", &major_ver, &minor_ver);
        // 使用自适应阈值检测棋盘格角点
        // 这里的版本判断是为了兼容OpenCV 4.5+和4.4-的不同函数调用
        // 4.5+版本使用findChessboardCornersSB函数，4.4-版本使用findChessboardCorners函数
        if (major_ver > 4 || (major_ver == 4 && minor_ver >= 5)){
            // OpenCV 4.5+
            cvVerPlus45 = true;
        }
    }

    // 棋盘格检测服务回调函数
    // 输入：原始图像，输出：位姿向量[x,y,z]（相机坐标系下的棋盘格原点坐标）
    bool ChessboardDetectionCallback(cv::Mat image, std::vector<float>& vecRt)
    {   
        if(!ChessboardDetection(image)) {  // 执行检测
            return false;  // 检测失败返回false
        }
                
        // 生成唯一文件名
        auto now = std::chrono::system_clock::now();
        auto timestamp = std::chrono::duration_cast<std::chrono::milliseconds>(
            now.time_since_epoch()).count();
        std::string filename = "/home/nvidia/YaLongR8/src/bringup/detected_" + std::to_string(timestamp) + ".jpg";

        // 保存带角点的图像
        if(!cv::imwrite(filename, image)){
            std::cerr << "Failed to save image: " << filename << std::endl;
        } else {
            std::cout << "Saved image with corners: " << filename << std::endl;
        }

        vecRt = PoseCalculation();  // 计算并返回位姿
        return true;
    }

    // 计算标定板坐标系到相机坐标系的转换矩阵
    cv::Point3f transformToCameraFrame(const cv::Point3f& obj_in_board)
    {
        cv::Mat R;
        cv::Rodrigues(rvec, R);  // 将旋转向量转为旋转矩阵

        cv::Mat pt_board = (cv::Mat_<double>(3,1) << 
                            obj_in_board.x, 
                            obj_in_board.y, 
                            obj_in_board.z);

        cv::Mat pt_camera = R * pt_board + tvec;

        return cv::Point3f(
            static_cast<float>(pt_camera.at<double>(0)),
            static_cast<float>(pt_camera.at<double>(1)),
            static_cast<float>(pt_camera.at<double>(2))
        );
    }


private:
    // 核心检测函数
    bool ChessboardDetection(cv::Mat image)
    {
        if(image.empty()) {
            std::cerr << "图像为空！" << std::endl;
            return false;
        }
        const cv::Size boardSize(boardWidth, boardHeight);  // 棋盘格内部角点数量（7x7网格）
        std::vector<cv::Point2f> corners; // 存储检测到的角点坐标
        
        // 转换为灰度图（棋盘格检测需要灰度图像）
        cv::Mat gray_image;
        cv::cvtColor(image, gray_image, cv::COLOR_BGR2GRAY);
        
        // 使用自适应阈值检测棋盘格角点
        bool found = false;
        if (cvVerPlus45) {
            ROS_INFO("OpenCV version >= 4.5 detected, using findChessboardCornersSB.");
            // OpenCV 4.5+
            found = cv::findChessboardCornersSB(
                gray_image,
                boardSize,
                corners,
                cv::CALIB_CB_EXHAUSTIVE | cv::CALIB_CB_ACCURACY
            );
        } else {
            ROS_INFO("OpenCV version < 4.5 detected, using findChessboardCorners.");
            // OpenCV 4.4- or earlier fallback
            // found = cv::findChessboardCorners(gray_image, boardSize, corners);
            found = cv::findChessboardCorners(
                gray_image,
                boardSize,
                corners,
                cv::CALIB_CB_ADAPTIVE_THRESH + cv::CALIB_CB_NORMALIZE_IMAGE + cv::CALIB_CB_FAST_CHECK
            );
        }
        if (!found) {
            // std::cerr << "未检测到棋盘格！" << std::endl;
            // cv::imshow("检测结果", image);  // 显示原始图像辅助调试
            // cv::waitKey(1000);             // 显示1秒后自动关闭
            // cv::destroyWindow("检测结果");
            ROS_ERROR("Chessboard not found!");
            return false;
        }

        // 亚像素级角点精确化
        cv::cornerSubPix(
            gray_image, 
            corners, 
            cv::Size(11, 11),  // 搜索窗口大小
            cv::Size(-1, -1),  // 死区大小（-1表示无死区）
            cv::TermCriteria(
                cv::TermCriteria::EPS + cv::TermCriteria::COUNT, 
                30,    // 最大迭代次数
                0.1    // 精度阈值
            )
        );

        // 绘制角点（绿色）
        cv::drawChessboardCorners(image, boardSize, 
            cv::Mat(corners), found);

        // 生成三维物体点（假设棋盘格在Z=0平面）
        std::vector<cv::Point3f> objectPoints;
        // 棋盘格原点在左上第一个角点处
        // for (int i = 0; i < boardSize.height; ++i) {
        //     for (int j = 0; j < boardSize.width; ++j) {
        //         objectPoints.emplace_back(
        //             j * squareSize,  // X坐标（沿宽度方向）
        //             i * squareSize,  // Y坐标（沿高度方向）
        //             0               // Z坐标固定为0
        //         );
        //     }
        // }
        // 棋盘格原点在中心位置
        for (int i = 0; i < boardSize.height; ++i) {
            for (int j = 0; j < boardSize.width; ++j) {
                objectPoints.emplace_back(
                    (j - (boardSize.width - 1) / 2.0) * squareSize,  // X坐标（沿宽度方向）
                    (i - (boardSize.height - 1) / 2.0) * squareSize, // Y坐标（沿高度方向）
                    0               // Z坐标固定为0
                );
            }
        }

        // 求解PnP问题（透视n点定位）
        // if (this->rvec.empty() || this->tvec.empty()){
        //     cv::solvePnP(
        //         objectPoints,          // 物体坐标系中的3D点
        //         corners,               // 图像坐标系中的2D点
        //         cameraMatrix,          // 相机内参矩阵
        //         distCoeffs,            // 畸变系数
        //         rvec,                  // 输出旋转向量
        //         tvec,                  // 输出平移向量
        //         false,                  // 使用初始估计（连续帧时提高稳定性）
        //         cv::SOLVEPNP_ITERATIVE // 使用迭代算法
        //     );
        // } else {
        //     cv::solvePnP(
        //         objectPoints,          // 物体坐标系中的3D点
        //         corners,               // 图像坐标系中的2D点
        //         cameraMatrix,          // 相机内参矩阵
        //         distCoeffs,            // 畸变系数
        //         rvec,                  // 输出旋转向量
        //         tvec,                  // 输出平移向量
        //         true,                  // 使用初始估计（连续帧时提高稳定性）
        //         cv::SOLVEPNP_ITERATIVE // 使用迭代算法
        //     );
        // }
        
        std::vector<int> inliers;
        cv::solvePnPRansac(
            objectPoints,
            corners,
            cameraMatrix,
            distCoeffs,
            rvec,
            tvec,
            false,          // 不使用初始估计
            100,            // 最大迭代次数
            8.0,            // 重投影误差阈值（像素）
            0.99,           // 置信度
            inliers,        // 返回的内点索引
            cv::SOLVEPNP_ITERATIVE
        );

        return true;
    }

    // 位姿计算函数
    std::vector<float> PoseCalculation()
    {
        // 欧拉角（ZYX）
        cv::Mat R;
        cv::Rodrigues(rvec, R);
        float roll  = atan2(R.at<double>(2,1), R.at<double>(2,2));
        float pitch = atan2(-R.at<double>(2,0), sqrt(R.at<double>(2,1)*R.at<double>(2,1) + R.at<double>(2,2)*R.at<double>(2,2)));
        float yaw   = atan2(R.at<double>(1,0), R.at<double>(0,0));
        return {
            static_cast<float>(tvec.at<double>(0)), // X
            static_cast<float>(tvec.at<double>(1)), // Y
            static_cast<float>(tvec.at<double>(2)),  // Z
            // static_cast<float>(rvec.at<double>(0)), // Rx
            // static_cast<float>(rvec.at<double>(1)), // Ry
            // static_cast<float>(rvec.at<double>(2))  // Rz
            static_cast<float>(roll),  // Roll
            static_cast<float>(pitch), // Pitch
            static_cast<float>(yaw)    // Yaw
        };
        // // 将旋转向量转换为旋转矩阵
        // cv::Mat rotationMatrix;
        // cv::Rodrigues(rvec, rotationMatrix);
        
        // // 计算棋盘格原点在相机坐标系中的坐标
        // cv::Mat chessboardOrigin = -rotationMatrix.t() * tvec;
        
        // // 转换为float类型向量
        // return {
        //     static_cast<float>(chessboardOrigin.at<double>(0)), // X坐标
        //     static_cast<float>(chessboardOrigin.at<double>(1)), // Y坐标
        //     static_cast<float>(chessboardOrigin.at<double>(2))  // Z坐标
        // };
    }
};