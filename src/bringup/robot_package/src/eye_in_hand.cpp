#include <opencv2/opencv.hpp>
#include <opencv2/calib3d.hpp>
#include <vector>
#include <iostream>

// 棋盘格尺寸
const cv::Size boardSize(7, 7); // 7行7列角点
const float squareSize = 12.5f; // 每个小格子的实际边长（单位：mm）


#include <iostream>
#include <fstream>
#include <sstream>
#include <vector>
#include <string>

std::vector<std::vector<double>> terminalData;

int readTerminalData() {
    std::string filename = "/mnt/ros_ws/eye_in_hand/eye_in_hand.txt";  // 替换为你的文件名
    std::ifstream file(filename);

    if (!file.is_open()) {
        std::cerr << "无法打开文件: " << filename << std::endl;
        return 1;
    }

    std::string line;

    while (std::getline(file, line)) {
        std::vector<double> row;
        std::stringstream ss(line);
        std::string token;
        int count = 0;

        while (std::getline(ss, token, ',')) {
            try {
                double value = std::stod(token);
                row.push_back(value);
                ++count;
            } catch (const std::invalid_argument& e) {
                std::cerr << "无法解析的数字: " << token << std::endl;
            } catch (const std::out_of_range& e) {
                std::cerr << "数字超出范围: " << token << std::endl;
            }
        }

        if (count != 16) {
            std::cerr << "警告：该行不包含16个数字：" << line << std::endl;
        }

        terminalData.push_back(row);
    }

    file.close();

    //可选：输出读取的数据
    for (const auto& row : terminalData) {
        for (double val : row) {
            std::cout << val << " ";
        }
        std::cout << std::endl;
    }

    return 0;
}

// 构建棋盘格世界坐标点
void createChessboard3DPoints(std::vector<cv::Point3f>& objectPoints) {
    for (int i = 0; i < boardSize.height; ++i) {
        for (int j = 0; j < boardSize.width; ++j) {
            objectPoints.emplace_back(j * squareSize, i * squareSize, 0.0f);
        }
    }
}

int N=11; // 采集的图像数量
int main() {
    int cvVerPlus45 =false;
    int major_ver = 0, minor_ver = 0;
    std::sscanf(CV_VERSION, "%d.%d", &major_ver, &minor_ver);
    // 使用自适应阈值检测棋盘格角点
    // 这里的版本判断是为了兼容OpenCV 4.5+和4.4-的不同函数调用
    // 4.5+版本使用findChessboardCornersSB函数，4.4-版本使用findChessboardCorners函数
    if (major_ver > 4 || (major_ver == 4 && minor_ver >= 5)){
        // OpenCV 4.5+
        cvVerPlus45 = true;
    }

    readTerminalData(); // 读取机械臂末端位姿数据
    std::vector<std::vector<cv::Point3f>> objectPointsVec; // 世界坐标
    std::vector<std::vector<cv::Point2f>> imagePointsVec;  // 像素坐标

    std::vector<cv::Mat> R_gripper2base, t_gripper2base; // 机械臂末端位姿
    std::vector<cv::Mat> R_target2cam, t_target2cam;     // 相机估计出的棋盘板位姿

    // 初始化棋盘格世界点
    std::vector<cv::Point3f> objectPoints;
    createChessboard3DPoints(objectPoints);

    // 假设你循环采集了多组图像和机械臂末端姿态
    for (int i = 0; i < N; ++i) {
        // 1. 拍一张照片
        std::string imagePath = "/mnt/ros_ws/eye_in_hand/" + std::to_string(i) + ".jpg"; // 替换为你的图像路径
        cv::Mat image = cv::imread(imagePath.c_str());//... // 从相机获取
        if(image.empty()) {
            std::cerr << "Failed to load image: " << imagePath << std::endl;
            continue;
        }

        // 2. 查找角点
        // 转换为灰度图（棋盘格检测需要灰度图像）
        std::vector<cv::Point2f> imagePoints;
        cv::Mat gray_image;
        cv::cvtColor(image, gray_image, cv::COLOR_BGR2GRAY);
        
        // 使用自适应阈值检测棋盘格角点
        bool found = false;
        if (cvVerPlus45) {
            // OpenCV 4.5+
            found = cv::findChessboardCornersSB(
                gray_image,
                boardSize,
                imagePoints,
                cv::CALIB_CB_EXHAUSTIVE | cv::CALIB_CB_ACCURACY
            );
        } else {
            // OpenCV 4.4- or earlier fallback
            // found = cv::findChessboardCorners(gray_image, boardSize, imagePoints);
            found = cv::findChessboardCorners(
                gray_image,
                boardSize,
                imagePoints,
                cv::CALIB_CB_ADAPTIVE_THRESH + cv::CALIB_CB_NORMALIZE_IMAGE + cv::CALIB_CB_FAST_CHECK
            );
        }
        if (!found) {
            std::cerr << "未检测到棋盘格！" << std::endl;
            cv::imshow("检测结果", image);  // 显示原始图像辅助调试
            cv::waitKey(1000);             // 显示1秒后自动关闭
            cv::destroyWindow("检测结果");
            return false;
        }

        
        // 亚像素级角点精确化
        cv::cornerSubPix(
            gray_image, 
            imagePoints, 
            cv::Size(11, 11),  // 搜索窗口大小
            cv::Size(-1, -1),  // 死区大小（-1表示无死区）
            cv::TermCriteria(
                cv::TermCriteria::EPS + cv::TermCriteria::COUNT, 
                30,    // 最大迭代次数
                0.1    // 精度阈值
            )
        );
        // cv::cornerSubPix(image, imagePoints, cv::Size(5,5), cv::Size(-1,-1),
        //                  cv::TermCriteria(cv::TermCriteria::EPS + cv::TermCriteria::COUNT, 30, 0.01));

        imagePointsVec.push_back(imagePoints);
        objectPointsVec.push_back(objectPoints);

        // 3. 机械臂末端位姿记录
        cv::Mat T_gripper2base = cv::Mat(terminalData[i]).reshape(1, 4); //... // 从机械臂SDK读取，4x4矩阵
        std::cout << "T_gripper2base size: " << T_gripper2base.rows << " x " << T_gripper2base.cols << std::endl;

        // cv::Mat R_g, t_g;
        // R_g = T_gripper2base(cv::Rect(0,0,3,3)); // 旋转部分
        // t_g = T_gripper2base(cv::Rect(3,0,1,3)); // 平移部分
        cv::Mat R_g = T_gripper2base(cv::Range(0, 3), cv::Range(0, 3)).clone();  // 取旋转
        cv::Mat t_g = T_gripper2base(cv::Range(0, 3), cv::Range(3, 4)).clone();  // 取平移

        R_gripper2base.push_back(R_g);
        t_gripper2base.push_back(t_g);
    }

    // 4. 相机内参（需要事先标定）
    cv::Mat cameraMatrix, distCoeffs;
    // 请先通过OpenCV相机标定得到cameraMatrix和distCoeffs！！！
    cameraMatrix = (cv::Mat_<double>(3, 3)
                        << 1.04969613e+03, 0.00000000e+00, 5.73570763e+02,
                        0.00000000e+00, 1.05086275e+03, 4.05877726e+02,
                        0.00000000e+00, 0.00000000e+00, 1.00000000e+00);
    distCoeffs = (cv::Mat_<double>(1, 5) 
                << -1.85926395e-03, 1.61417431e-03, 1.14303737e-03,
                    7.58650886e-05, -1.17211371e-03);

    // 5. 针对每一张图像，解PnP求棋盘位姿
    for (size_t i = 0; i < imagePointsVec.size(); ++i) {
        cv::Mat rvec, tvec;
        cv::solvePnP(objectPointsVec[i], imagePointsVec[i], cameraMatrix, distCoeffs, rvec, tvec);

        cv::Mat R_c;
        cv::Rodrigues(rvec, R_c); // 将旋转向量转换成旋转矩阵

        R_target2cam.push_back(R_c);
        t_target2cam.push_back(tvec);
    }

    // 6. 进行手眼标定
    cv::Mat R_cam2gripper, t_cam2gripper;
    cv::calibrateHandEye(R_gripper2base, t_gripper2base,
                         R_target2cam, t_target2cam,
                         R_cam2gripper, t_cam2gripper,
                         cv::CALIB_HAND_EYE_TSAI); // 方法可以选Tsai

    // 7. 打印结果
    std::cout << "R_cam2gripper:\n" << R_cam2gripper << std::endl;
    std::cout << "t_cam2gripper:\n" << t_cam2gripper.t() << std::endl;

    return 0;
}
