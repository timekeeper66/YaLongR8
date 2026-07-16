#include "robot_package/robotic_man.h" 
#include "RobSoft/CRobotics.hpp"
#include "robot_package/chess_board_detector.hpp"

robotic_node::robotic_node(int argc, char **argv,const char* name)
{
    // 初始化 ROS 节点
    ros::init(argc, argv, name);
    m_nh = new ros::NodeHandle("~");
    this->m_nh->param("robot_arm_ip",robot_arm_ip,std::string("192.168.17.1"));
    this->m_nh->param("robot_arm_port",robot_arm_port,8001);
    std::string strPath;
    this->m_nh->param("robot_client_lib_path",strPath,std::string("libRobotConSys_Client.so"));
    
    this->m_nh->param("robot_type",robot_type,1);

    // 获取环境变量
    const char *env_value = std::getenv("LD_LIBRARY_PATH");
    if (env_value) {
        ROS_INFO("Environment variable 'LD_LIBRARY_PATH' is set to: %s", env_value);
    } else {
        ROS_WARN("Environment variable 'LD_LIBRARY_PATH' is not set.");
    }

    // m_serverMoveABSJ = new Server(*m_nh, "/robot_move_absj", boost::bind(&robotic_node::executeMoveABSJ, this, _1, m_serverMoveABSJ), false);
    // m_serverMoveABSJ->start();

    // 创建服务
    m_serviceMoveABSJ = m_nh->advertiseService("MoveABSJ", &robotic_node::MoveABSJCallback, this);
    m_serviceMoveJ = m_nh->advertiseService("MoveJ", &robotic_node::MoveJCallback, this);
    m_serviceMoveL = m_nh->advertiseService("MoveL", &robotic_node::MoveLCallback, this);
    m_serviceServo = m_nh->advertiseService("Servo", &robotic_node::ServoCallback, this);
    m_serviceClaw = m_nh->advertiseService("Claw", &robotic_node::ClawCallback, this);
    m_serviceVehicleCommModel = m_nh->advertiseService("VehicleCommModel", &robotic_node::VehicleCommModelCallback, this);
    m_serviceVehicleServo = m_nh->advertiseService("VehicleServo", &robotic_node::VehicleServoCallback, this);
    m_serviceVehicleControl = m_nh->advertiseService("VehicleControl", &robotic_node::VehicleControlCallback, this);
    m_serviceImageToFrame = m_nh->advertiseService("ImageToFrame", &robotic_node::ImageToFrameCallback, this);

    m_subscribeVel = m_nh->subscribe("/cmd_vel", 10, &robotic_node::VehicleVelCallback, this);
    // for robotic
    //export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:/opt/RobotConSys_SDK/RobotConSys_SDK/lib/linux_x64/
    //string strPath("/mnt/Ros_WS/RobotConSys_SDK/RobotConSys_SDK/lib/linux_x64/libRobotConSys_Client.so");
    initRoboticLabrary(strPath);
    //创建机器人客户端
    m_ptrRobot = createRobotClient(robot_arm_ip , robot_arm_port);
}

robotic_node::~robotic_node()
{
    if(m_nh) delete m_nh;
    m_nh=nullptr;
    if(m_ptrRobot){
        m_ptrRobot->close();
        freeRobotConSysClient(m_ptrRobot);
        m_ptrRobot = nullptr;
    }
}


int robotic_node::initRoboticLabrary(string strPath){
    m_ptrLibLoadTool = new CLoadLibrary();

    if(m_ptrLibLoadTool->OpenLibrary(strPath.c_str())){
		//获取客户端实例创建接口
		this->createRobotConSysClient = m_ptrLibLoadTool->GetFunction<CreateRobotConSys>("createRobotConSys_Client");
		//获取客户端实例释放接口
		this->freeRobotConSysClient = m_ptrLibLoadTool->GetFunction<FreeRobotConSys>("freeRobotConSys_Client");
	    
		if(this->createRobotConSysClient != nullptr && this->freeRobotConSysClient!=nullptr){
            std::cout << "robot init success" << std::endl;
        }
        
    }else{
        std::cout << "\033[31mrobot init fail\033[0m" << std::endl;
        return 1;
    }
    
    return 0;
}

rclib::RobotConSys* robotic_node::createRobotClient(string ip,int port){
    rclib::RobotConSys* robot = nullptr;
    if(createRobotConSysClient&&freeRobotConSysClient){
        robot = createRobotConSysClient();
        int nRt = robot->init(ip.c_str(),port);
        if(nRt!=0 ) {
            std::cout << "\033[31mConnecting robot arm fail.\033[0m" << std::endl;
            freeRobotConSysClient(robot);
            return nullptr;
        }
        std::cout << "Connecting robot arm success" << std::endl;
        
        //设置可控权限
        robot->setAuthority(DevLayer::AUTHORITY_TYPE::AUTHORITY_ROOT);
        
        if(robot->getAuthority()!=DevLayer::AUTHORITY_TYPE::AUTHORITY_ROOT)
        {
            std::cout << "\033[31mGetting robot arm authority fail.\033[0m" << std::endl;
            freeRobotConSysClient(robot);
            return nullptr;
        }
        std::cout << "Getting robot arm authority success" << std::endl;

        printf("set servo\r\n");
        //设置示教模式，打开伺服
        robot->setPlayState(rclib::SYSPLAYSTATE::SYSPLAY_TEACH);
        robot->setServoState(rclib::SWITCHSTATE::SWITCHON);

        //初始化底盘
        robot->setVehicleServoState(rclib::SWITCHSTATE::SWITCHON);
        robot->setVehicleControlType(DevLayer::VEHICLE_CONTROL_TYPE::VEHICLE_CONTROL_UART);
        robot->setMotionControl(DevLayer::VEHICLE_MODE::VEHICLE_MODE_NORMAL,0,0,0);
    }
    return robot;
}

Vector6d robotic_node::CalculateCoordinate(rclib::ROBOTCONSYS_ARM_INDEX arm_index, std::vector<float>& xyz, const std::vector<float>& ABC)
{
    Terminal t; //姿态变换
    if(robot_type == 0){
        // r8c
        t.setValue(0,0,0,-90,0,0);   
        // [0.05638303980332049, 0.9577175394374795, 0.2821312947483083;
        //     -0.9792492282212312, 0.1081415247514319, -0.1713953314783062;
        //     -0.1946584235286524, -0.266613062842493, 0.9439415092425403]
        // Matrix3d mat ;mat.setIdentity();
        // mat(0,0)= 0.05638303980332049; mat(0,1)= 0.9577175394374795; mat(0,2)= 0.2821312947483083;
        // mat(1,0)= -0.9792492282212312; mat(1,1)= 0.1081415247514319; mat(1,2)= -0.1713953314783062;
        // mat(2,0)= -0.1946584235286524; mat(2,1)= -0.266613062842493; mat(2,2)= 0.9439415092425403;
        // t.setValue(robsoft::Point(0,0,0),robsoft::RotateMatrix(mat));
    }else if(robot_type == 1){
        t.setValue(0,0,0,-90,0,-90);
    }
    Terminal tCam(xyz[0],xyz[1],xyz[2],0,0,0);
    auto newT = t.getHomogeneousMatrix();
    //* tCam.getHomogeneousMatrix();   
    m_ptrRobot->updateRobotMotion(arm_index);
    
    // set head frame
    auto paramter = m_ptrRobot->getRobotParameter(arm_index);
    robsoft::Robotics robotics;
    robotics.setRobotParameter(paramter);   
    robotics.setCurrentJointPosition(m_ptrRobot->getRobotMotion(arm_index).getCurrentJointPosition());
    robotics.setRobotParameter(paramter);

    if(robot_type == 0){
        ROS_INFO("robot type 0:Arm");
        newT = Terminal(-43.05,12.78,-66.45,0,0,0).getHomogeneousMatrix()* newT;  //位置变换
        auto rt = robotics.getTCPFrame()*newT;
        return rt.getTerminal().getValue();
    }else if(robot_type == 1){
        ROS_INFO("robot type 1:head");
        auto rt = robotics.getHeadFrame()*newT;
        return rt.getTerminal().getValue();
    }
}

void robotic_node::publishData(){
    // 创建一个发布者，发布到 /custom_topic 话题
    ros::Publisher pub = m_nh->advertise<robot_package::TR_Arm_Msg>("/TR_Arm_topic", 10);
    ros::Publisher pubVehicle = m_nh->advertise<robot_package::TR_Vehicle_Msg>("/TR_Vehicle_topic", 10);
    ros::Publisher pubOdom = m_nh->advertise<nav_msgs::Odometry>("/odom_real", 10);
    
    m_last_time = ros::Time::now();
    std::vector<double> wheel_speed(4);
    // 里程计数据发布函数
    auto publishOdometryFunc = [&](){
            ros::Time current_time = ros::Time::now();
            double dt = (current_time - m_last_time).toSec();
            m_last_time = current_time;

            // 计算线速度和角速度
            m_odom_vx = (wheel_speed[0] + wheel_speed[1] + wheel_speed[2] + wheel_speed[3]) / 4;
            m_odom_vth = (((wheel_speed[1] + wheel_speed[3]) / 2) - ((wheel_speed[0] + wheel_speed[2]) / 2)) / 0.382;
    
            // 使用线速度和角速度计算位置变化
            double delta_x = m_odom_vx * cos(m_odom_theta) * dt;
            double delta_y = m_odom_vx * sin(m_odom_theta) * dt;
            double delta_theta = m_odom_vth * dt;
    
            // 更新机器人位置
            m_odom_x += delta_x;
            m_odom_y += delta_y;
            m_odom_theta += delta_theta;
    
            // 角度归一化，确保theta始终在[0, 2*pi]之间
            while (m_odom_theta > 2 * M_PI) m_odom_theta -= 2 * M_PI;
            while (m_odom_theta < 0) m_odom_theta += 2 * M_PI;
    
            // 生成四元数表示机器人的姿态
            geometry_msgs::Quaternion odom_quat = tf::createQuaternionMsgFromYaw(m_odom_theta);
    
            // 发布里程计消息（需要填充相关的msg内容，如header等）    
            // odometry 消息
            nav_msgs::Odometry odom;
            odom.header.stamp = current_time;
            odom.header.frame_id = "odom";
            odom.child_frame_id = "base_link";
    
            odom.pose.pose.position.x = m_odom_x;
            odom.pose.pose.position.y = m_odom_y;
            odom.pose.pose.position.z = 0.0;
            odom.pose.pose.orientation = odom_quat;
    
            odom.twist.twist.linear.x = m_odom_vx;
            odom.twist.twist.linear.y = 0.0;
            odom.twist.twist.angular.z = m_odom_vth;
    
            pubOdom.publish(odom);
    }; 
    
    // 设置发布频率
    ros::Rate loop_rate(30); // 1 Hz

    while (ros::ok())
    {
        
        // 按照设定频率休眠
        ros::spinOnce();
        loop_rate.sleep();
        if(m_ptrRobot == nullptr){
            std::cout << "\033[31mrobot is null\033[0m" << std::endl;
            continue;
        }

        auto prefer = m_ptrRobot->getRobotConSysPreference();

        // 获取机器人手臂数量
        int arm_num = prefer.getRobotDevChannelNum();
        // 更新机器人手臂状态
        for(int i=0; i<arm_num; i++){
            auto arm_index = rclib::ROBOTCONSYS_ARM_INDEX(i);
            // 创建自定义消息对象
            robot_package::TR_Arm_Msg msg;
            auto rm = m_ptrRobot->getRobotMotion(arm_index);
            msg.name = "arm"+std::to_string(i);
            for(int i=0; i<rm.getCurrentJointPosition().getJointsDOF(); i++){
                msg.joints.push_back(rm.getCurrentJointPosition()[robsoft::JOINTINDEX(i)]);
            }
            
            robsoft::Terminal ter = rm.getCurrentTerminal();
            for(int i=0; i<6; i++){
                msg.terminal.push_back(ter[robsoft::TERMINALINDEX(i)]);
            }
            auto m = ter.getHomogeneousMatrix();
            for(int i=0; i<4; i++){
                for(int j=0; j<4; j++){
                    msg.homogeneousMatrix.push_back(m.getValue(i,j));
                }
            }
            

            auto state = m_ptrRobot->getRobotState(arm_index);
            msg.servoState = state.getServoState();
            
            auto clawState = m_ptrRobot->getClawState(arm_index);
            
            msg.clawState = clawState;
            pub.publish(msg);
        }

        
        // 获取机器人小车数量
        int vehicle_num = prefer.getVehicleDevChannelNum();
        // 更新机器人小车状态
        for(int i=0; i<vehicle_num; i++){
            DevLayer::Vehicle_State_Data vehicleState = m_ptrRobot->getVehicleState();
            // 创建自定义消息对象
            robot_package::TR_Vehicle_Msg msg;
            msg.vehicle_x_vel = vehicleState.vehicle_x_vel;
            msg.vehicle_y_vel = vehicleState.vehicle_y_vel;
            msg.vehicle_z_vel = vehicleState.vehicle_z_vel;
            for(int i=0;i<4;i++){
                msg.wheelVel.push_back(vehicleState.wheelVel[i]);
                wheel_speed[i] = vehicleState.wheelVel[i] * 0.078;
            }
            for(int i=0;i<4;i++){
                msg.servoPos.push_back(vehicleState.servoPos[i]);
            }
            for(int i=0;i<8;i++){
                msg.sonar.push_back(vehicleState.sonar[i]);
            }
            pubVehicle.publish(msg);


            // 发布里程计数据
            publishOdometryFunc();
        }
    }
}


// void robotic_node::executeMoveABSJ(const robot_package::MoveABSJGoalConstPtr &goal, Server *server) {

//     robot_package::MoveABSJFeedback feedback;
//     robot_package::MoveABSJResult result;

//     if(m_ptrRobot == nullptr){
//         ROS_ERROR("Robot is not initialized.");
//         result.result = 0;
//         server->setSucceeded(result);
//         return ;
//     }

//     rclib::ROBOTCONSYS_ARM_INDEX arm_index = rclib::ROBOTCONSYS_ARM_INDEX(goal->arm_index);
//     if(arm_index < 0 || arm_index > 2||goal->vel < 0||goal->vel > 1){
//         ROS_ERROR("Invalid arm index.");
//         result.result = -1;
//         return;
//     }
//     auto dof = m_ptrRobot->getRobotParameter(arm_index).getWholeDOF();
//     if(dof != goal->joints.size()){
//         ROS_ERROR("Joints size does not match.input size=%d, arm size=%d",goal->joints.size(),dof);
//         result.result = -1;
//         return;
//     }
//     robsoft::Joints j(dof);
//     for(int i=0; i<dof; i++){
//         j[robsoft::JOINTINDEX(i)] = goal->joints[i];
//     }
//     // 设置目标位置
//     int nRt = m_ptrRobot->moveABSJoint(rclib::ROBOTCONSYS_ARM_INDEX(arm_index), j, goal->vel);
//     if(nRt != 0){
//         ROS_ERROR("MoveABSJ failed.");
//         result.result = nRt;
//         server->setSucceeded(result);
//         return ;
//     }

//     ros::Rate rate(1); // 1 Hz
//     while (ros::ok() && m_ptrRobot->getMotionCMDState(rclib::ROBOTCONSYS_ARM_INDEX(arm_index)) == 1) {
//         if (server->isPreemptRequested()) {
//             ROS_INFO("Preempted!");
//             server->setPreempted();
//             return;
//         }
//         // 发送反馈
//         feedback.current_state = 1;
//         server->publishFeedback(feedback);

//         rate.sleep();
//     }

//     // 设置结果
//     m_ptrRobot->waitMotionCMDFinish(rclib::ROBOTCONSYS_ARM_INDEX(arm_index));   // 保险，等待运动命令执行完成
//     result.result = 0;
//     server->setSucceeded(result);
// }


bool robotic_node::MoveABSJCallback(robot_package::MoveABSJ::Request &req,
    robot_package::MoveABSJ::Response &res){
        
    if(m_ptrRobot == nullptr){
        ROS_ERROR("Robot is not initialized.");
        res.result = -1;
        res.strRt = "Robot is not initialized.";
        return true;
    }
    
    if(req.vel < 0.01|| req.vel > 1){
        ROS_ERROR("Invalid velocity");
        res.result =  -1;
        res.strRt = "Invalid velocity";
        return true;
    }

    // 处理请求数据
    int dataIndex =0;
    std::vector<rclib::ROBOTCONSYS_ARM_INDEX> arm_indexsVector;
    for(int i=0;i<3;i++){
        //获取手臂引脚
        if(req.arm_index & (1<<i)){
            auto arm_index = rclib::ROBOTCONSYS_ARM_INDEX(i);
            int dof = m_ptrRobot->getRobotParameter(arm_index).getWholeDOF();
            if(req.joints.size()-dataIndex < dof){
                ROS_ERROR("Joints size does not match arm[%d]dof:%d.dataIndex=%d",arm_index,dof,dataIndex);
                res.result = -1;
                res.strRt = "Joints size does not match.";
                return true;
            }
            
            robsoft::Joints js(dof);
            for(int j=0; j<dof; j++){
                js[robsoft::JOINTINDEX(j)] = req.joints[dataIndex+j];
            }
            // 设置目标位置
            int nRt = 0;
            if(req.relative){
                nRt = m_ptrRobot->moveABSJointR(rclib::ROBOTCONSYS_ARM_INDEX(arm_index), js, req.vel);
            }else{
                nRt = m_ptrRobot->moveABSJoint(rclib::ROBOTCONSYS_ARM_INDEX(arm_index), js, req.vel);
            }
            
            if(nRt != 0){
                ROS_ERROR("MoveABSJ failed.arm=%d,nRt=%d",arm_index,nRt);
                res.result = nRt;
                return true;
            }
            arm_indexsVector.push_back(arm_index);
            dataIndex += dof;
        }
    }

    for(auto arm_index: arm_indexsVector){
        m_ptrRobot->waitMotionCMDFinish(arm_index);   // 保险，等待运动命令执行完成
    }
    // 设置结果
    res.result = 0;
    res.strRt = "MoveABSJ success.";
    return true;
}

bool robotic_node::MoveJCallback(robot_package::MoveJ::Request &req,
    robot_package::MoveJ::Response &res)
{
    if(req.vel < 0.01 || req.vel >1){
        res.result = -1;
        res.strRt = "Invalid velocity.";
        return true;
    }
    // 处理请求数据
    int dataIndex =0;
    std::vector<rclib::ROBOTCONSYS_ARM_INDEX> arm_indexsVector;
    for(int i=0;i<3;i++){
        //获取手臂引脚
        if(req.arm_index & (1<<i)){
            auto arm_index = rclib::ROBOTCONSYS_ARM_INDEX(i);
            
            if(req.terminal.size()<dataIndex+6)
            {
                res.result = -1;
                return true;
            }
            robsoft::Terminal ter;
            for(int j=0; j<6; j++){
                ter[robsoft::TERMINALINDEX(j)] = req.terminal[dataIndex+j];
            }
            // 设置目标位置
            // int nRt = m_ptrRobot->moveJoint(arm_index, ter, req.vel);               
            int nRt = 0;
            if(req.relative){
                nRt = m_ptrRobot->moveJointR(arm_index, ter, req.vel);
            }else{  
                nRt = m_ptrRobot->moveJoint(arm_index, ter, req.vel);
            }
            if(nRt != 0){
                ROS_ERROR("MoveJ failed.arm=%d,rt=%d",arm_index,nRt);
                res.result = nRt;
                return true;
            }
            arm_indexsVector.push_back(arm_index);
            dataIndex += 6;
        }
    }
    
    for(auto arm_index: arm_indexsVector){
        m_ptrRobot->waitMotionCMDFinish(arm_index);   // 保险，等待运动命令执行完成
    }
    // 设置结果
    res.result = 0;
    res.strRt = "MoveJ success.";
    return true;
}


bool robotic_node::MoveLCallback(robot_package::MoveL::Request &req,
    robot_package::MoveL::Response &res){
    // 处理请求数据
    int dataIndex =0;
    std::vector<rclib::ROBOTCONSYS_ARM_INDEX> arm_indexsVector;
    for(int i=0;i<3;i++){
        //获取手臂引脚
        if(req.arm_index & (1<<i)){
            auto arm_index = rclib::ROBOTCONSYS_ARM_INDEX(i);
            
            if(req.terminal.size()<dataIndex+6)
            {
                res.result = -1;
                return true;
            }
            robsoft::Terminal ter;
            for(int j=0; j<6; j++){
                ter[robsoft::TERMINALINDEX(j)] = req.terminal[dataIndex+j];
            }
            // 设置目标位置
            // int nRt = m_ptrRobot->moveJoint(arm_index, ter, req.vel);               
            int nRt = 0;
            if(req.relative){
                nRt = m_ptrRobot->moveLineR(arm_index, ter, req.vel);
            }else{  
                nRt = m_ptrRobot->moveLine(arm_index, ter, req.vel);
            }
            if(nRt != 0){
                ROS_ERROR("MoveJ failed.arm=%d",arm_index);
                res.result = nRt;
                return true;
            }
            arm_indexsVector.push_back(arm_index);
            dataIndex += 6;
        }
    }
    
    for(auto arm_index: arm_indexsVector){
        m_ptrRobot->waitMotionCMDFinish(arm_index);   // 保险，等待运动命令执行完成
    }
    // 设置结果
    res.result = 0;
    res.strRt = "MoveJ success.";
    return true;
}

bool robotic_node::ServoCallback(robot_package::Servo::Request &req,
    robot_package::Servo::Response &res)
{
    rclib::ROBOTCONSYS_ARM_INDEX arm_index = rclib::ROBOTCONSYS_ARM_INDEX (req.arm_index);
    if(arm_index < 0 || arm_index > 2){
        res.result = -1;
        res.strRt = "Invalid arm index.";
        return true;
    }
    if(req.servoState != 0 && req.servoState != 1){
        res.result = -1;
        res.strRt = "Invalid servo state.";
        return true;
    }

    rclib::SWITCHSTATE state = rclib::SWITCHSTATE::SWITCHOFF;
    if(req.servoState){
        state = rclib::SWITCHSTATE::SWITCHON;
    }else{
        state = rclib::SWITCHSTATE::SWITCHOFF;
    }
    

    // 设置伺服状态
    int nRt = m_ptrRobot->setServoState(arm_index,state);
    if(nRt != 0){
        res.result = nRt;
        return true;
    }else{
        res.result = 0;
        return true;
    }
}
bool robotic_node::ClawCallback(robot_package::Claw::Request &req,
    robot_package::Claw::Response &res)
{
    rclib::ROBOTCONSYS_ARM_INDEX arm_index = rclib::ROBOTCONSYS_ARM_INDEX (req.arm_index);
    if(arm_index < 0 || arm_index > 2){
        res.result = -1;
        res.strRt = "Invalid arm index.";
        return true;
    }
    if(req.clawState != 0 && req.clawState != 1){
        res.result = -1;
        res.strRt = "Invalid claw state.";
        return true;
    }


    rclib::SWITCHSTATE state = rclib::SWITCHSTATE::SWITCHOFF;
    if(req.clawState){
        state = rclib::SWITCHSTATE::SWITCHON;
    }else{
        state = rclib::SWITCHSTATE::SWITCHOFF;
    }
    
    // 设置夹爪状态
    m_ptrRobot->setClawState(arm_index,state);
    res.result = 0;
    return true;
}


bool robotic_node::VehicleControlCallback(robot_package::VehicleControl::Request &req,
    robot_package::VehicleControl::Response &res)
{
    m_ptrRobot->setMotionControl(DevLayer::VEHICLE_MODE(req.mode),
        req.vehicle_x_vel, req.vehicle_y_vel, req.vehicle_z_vel);
    ROS_INFO("VehicleControl: mode=%d, x_vel=%f, y_vel=%f, z_vel=%f", req.mode, req.vehicle_x_vel, req.vehicle_y_vel, req.vehicle_z_vel);
    res.result = 0;
    return true;
}

bool robotic_node::VehicleServoCallback(robot_package::VehicleServo::Request &req,
    robot_package::VehicleServo::Response &res)
{
    if(req.servoState != 0 && req.servoState != 1){
        res.result = -1;
        res.strRt = "Invalid servo state.";
        return true;
    }

    rclib::SWITCHSTATE state = rclib::SWITCHSTATE::SWITCHOFF;
    if(req.servoState){
        state = rclib::SWITCHSTATE::SWITCHON;
    }else{
        state = rclib::SWITCHSTATE::SWITCHOFF;
    }
    
    // 设置伺服状态
    m_ptrRobot->setVehicleServoState(state);
    res.result = 0;
    return true;
}


bool robotic_node::VehicleCommModelCallback(robot_package::VehicleCommModel::Request &req,
    robot_package::VehicleCommModel::Response &res){
    int model = req.model;
    if(model <0 || model >4){
        res.result = -1;
        return true;
    }
    m_ptrRobot->setVehicleControlType(DevLayer::VEHICLE_CONTROL_TYPE(model));
    res.result = 0;
    return true;
}

void robotic_node::VehicleVelCallback(const geometry_msgs::Twist::ConstPtr& msg)
{

    double x_vel = (msg->linear.x)*1000;
    double y_vel = msg->linear.y;
    double z_vel = msg->angular.z;

    m_ptrRobot->setMotionControl(DevLayer::VEHICLE_MODE(0), x_vel, y_vel, z_vel);
}

bool robotic_node::ImageToFrameCallback(robot_package::ImageToFrame::Request &req,
    robot_package::ImageToFrame::Response &res)
{
    if(req.armIndex < 0 || req.armIndex > 3){
        res.result = -1;
        res.strRt = "Invalid armIndex index.";
        return true;
    }
    cv::Mat image;
    try {
        // 使用 toCvCopy，它有 (const sensor_msgs::Image&, const std::string&) 的重载
        cv_bridge::CvImagePtr cv_ptr = cv_bridge::toCvCopy(
            req.image, 
            sensor_msgs::image_encodings::BGR8
        );
        image = cv_ptr->image;  // 或者 .clone() 一份
    }
    catch (cv_bridge::Exception& e) {
        res.result = -1;
        res.strRt  = "cv_bridge exception: " + std::string(e.what());
        return true;
    }
    ChessboardDetector_cls cls(12.5,robot_type);
    std::vector<float> vecRt;
    bool bRt = cls.ChessboardDetectionCallback(image,vecRt);
    if(!bRt){
        res.result = -1;
        res.strRt = "ChessboardDetection failed.";
        return true;
    }
    for(int i=0;i<vecRt.size();i++){
        ROS_INFO("vecRt[%d]=%f",i,vecRt[i]);
    }

    // 计算标定板坐标系到机器人坐标系的变换
    if(req.position.size() == 3){
        //cv::Point3f obj_in_board(-155.0f, 40.0f, -220.0f);
        cv::Point3f obj_in_board(req.position[0], req.position[1], req.position[2]);
        auto pointer = cls.transformToCameraFrame(obj_in_board);
        vecRt[0] = pointer.x;
        vecRt[1] = pointer.y;
        vecRt[2] = pointer.z;
        for(int i=0;i<vecRt.size();i++){
            ROS_INFO("point vecRt[%d]=%f",i,vecRt[i]);
        }
    }

    auto vecArm = CalculateCoordinate(rclib::ROBOTCONSYS_ARM_INDEX(req.armIndex),vecRt, vector<float>{0,0,0});
    
    for(int i=0;i<vecArm.size();i++){
        res.terminal.push_back(vecArm[i]);
    }
    res.result = 0;
    return true;
}