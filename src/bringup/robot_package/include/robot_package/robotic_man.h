#ifndef ROBOTIC_MAN_H
#define ROBOTIC_MAN_H
#include <stdio.h>
#include <stdlib.h>
#include <thread>
#include <chrono>
#include "RobotConSys/RobotConSys/RobotConSys.h"
#include "SystemLayer/SystemOperation.h"
#include "robot_package/loard_library.h"

#include "ros/ros.h"
#include <geometry_msgs/Twist.h>
#include <tf/tf.h>
#include <nav_msgs/Odometry.h>
#include <geometry_msgs/Quaternion.h>


#include "robot_package/TR_Arm_Msg.h" // 自定义消息头文件
#include "robot_package/TR_Vehicle_Msg.h" 
#include "robot_package/TR_Vehicle_IMU_Msg.h" 

#include <actionlib/server/simple_action_server.h>
#include "robot_package/MoveABSJAction.h" // 

#include "robot_package/MoveABSJ.h" // 替换为你的包名
#include "robot_package/MoveJ.h" 
#include "robot_package/MoveL.h" 
#include "robot_package/Servo.h" 
#include "robot_package/Claw.h" 
#include "robot_package/VehicleCommModel.h"
#include "robot_package/VehicleServo.h"
#include "robot_package/VehicleControl.h"
#include "robot_package/ImageToFrame.h"

using namespace robsoft;

typedef rclib::RobotConSys* (*CreateRobotConSys)();
typedef void (*FreeRobotConSys)(rclib::RobotConSys* sys);

class robotic_node
{
private:
    /* data */
    ros::NodeHandle* m_nh=nullptr;
    std::string robot_arm_ip;
    int robot_arm_port;
    int robot_type;     // 0: arm 1: hear
    // 里程计数据
    double m_odom_x = 0.0, m_odom_y = 0.0, m_odom_theta = 0.0;
    double m_odom_vx = 0.0, m_odom_vth = 0.0;
    ros::Time m_last_time;

    // 发布器 服务
    ros::ServiceServer  m_serviceMoveABSJ;
    ros::ServiceServer  m_serviceMoveJ;
    ros::ServiceServer  m_serviceMoveL;
    ros::ServiceServer  m_serviceServo;
    ros::ServiceServer  m_serviceClaw;
    ros::ServiceServer  m_serviceVehicleCommModel;
    ros::ServiceServer  m_serviceVehicleServo;
    ros::ServiceServer  m_serviceVehicleControl;
    ros::ServiceServer  m_serviceImageToFrame;

    // 订阅器 底盘速度控制话题
    ros::Subscriber     m_subscribeVel;

    // typedef actionlib::SimpleActionServer<robot_package::MoveABSJAction> Server;
    // Server* m_serverMoveABSJ=nullptr;
    // void executeMoveABSJ(const robot_package::MoveABSJGoalConstPtr &goal, Server *server) ;

    bool MoveABSJCallback(robot_package::MoveABSJ::Request &req,
        robot_package::MoveABSJ::Response &res);
    bool MoveJCallback(robot_package::MoveJ::Request &req,
        robot_package::MoveJ::Response &res);
        bool MoveLCallback(robot_package::MoveL::Request &req,
            robot_package::MoveL::Response &res);
    bool ServoCallback(robot_package::Servo::Request &req,
        robot_package::Servo::Response &res);
    bool ClawCallback(robot_package::Claw::Request &req,    
        robot_package::Claw::Response &res);
    bool VehicleServoCallback(robot_package::VehicleServo::Request &req,
        robot_package::VehicleServo::Response &res);
    bool VehicleControlCallback(robot_package::VehicleControl::Request &req,
        robot_package::VehicleControl::Response &res);
    bool VehicleCommModelCallback(robot_package::VehicleCommModel::Request &req,
        robot_package::VehicleCommModel::Response &res);
    void VehicleVelCallback(const geometry_msgs::Twist::ConstPtr& msg);

    bool ImageToFrameCallback(robot_package::ImageToFrame::Request &req,
        robot_package::ImageToFrame::Response &res);

    ///robotic 
    rclib::RobotConSys* m_ptrRobot;
    CLoadLibrary* m_ptrLibLoadTool;
    CreateRobotConSys createRobotConSysClient=nullptr;
    FreeRobotConSys freeRobotConSysClient=nullptr;
    int initRoboticLabrary(string strPath);
    rclib::RobotConSys* createRobotClient(string ip,int port);
    // 计算坐标
    Vector6d CalculateCoordinate(rclib::ROBOTCONSYS_ARM_INDEX arm_index, std::vector<float>& xyz, const std::vector<float>& ABC);
    
public:
    robotic_node(int argc, char **argv,const char* name="robot_node");
    ~robotic_node();

    void publishData();
};


#endif