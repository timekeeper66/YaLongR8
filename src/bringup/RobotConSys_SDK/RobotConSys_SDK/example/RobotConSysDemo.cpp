#include "RobotConSys/RobotConSys.h"
#include "SystemOperation.h"

#include <stdio.h>
#include <chrono>
#include <thread>

using namespace std;

int main(int argc, char* argv[]){
    // load lib
    SysLayer::CLoadLibrary libRobotConSys;
    int ret = libRobotConSys.open(".", "RobotConSys_Client");
    if(ret != 0){
        printf("load RobotConSys_Client failed!\n");
    }
    rclib::RobotConSys* (*createRobotConSys)();
    createRobotConSys = (rclib::RobotConSys* (*)())libRobotConSys.loadFunc("createRobotConSys_Client");
    if(createRobotConSys == NULL){
        libRobotConSys.loadErrorPrint();
        exit(-1);
    }
    void (*freeRobotConSys)(rclib::RobotConSys* sys);
    freeRobotConSys = (void (*)(rclib::RobotConSys*))libRobotConSys.loadFunc("freeRobotConSys_Client");
    if(freeRobotConSys == NULL){
        libRobotConSys.loadErrorPrint();
        exit(-1);
    }

    // create robot
    rclib::RobotConSys* robot = (*createRobotConSys)();

    // init robot
    ret = robot->init("192.168.139.128", 8080);
    if(ret != 0){
        printf("RobotConSys init failed\n");
        exit(0);
    }
    robot->setAuthority(DevLayer::AUTHORITY_OPERATOR);

    /*User code begin*/

    robsoft::RobotParameter rParam = robot->getRobotParameter(rclib::ROBOTCONSYS_ARM_1);

    robot->setServoState(rclib::ROBOTCONSYS_ARM_1, rclib::SWITCHON);
    std::this_thread::sleep_for(std::chrono::milliseconds(1000));

    robsoft::Joints joint(rParam.getWholeDOF());
    for(int i=0; i<joint.getJointsDOF(); i++){
        joint[robsoft::JOINTINDEX(i)] = 5*i;
    }
    ret = robot->moveABSJoint(rclib::ROBOTCONSYS_ARM_1, joint, 0.2);
    if(ret != 0){
        printf("error: ret = %d\n", ret);
        exit(-1);
    }
    ret = robot->waitMotionCMDFinish(rclib::ROBOTCONSYS_ARM_1);
    if(ret != 0){
        printf("error: ret = %d\n", ret);
        exit(-1);
    }
    std::this_thread::sleep_for(std::chrono::milliseconds(1000));

    ret = robot->returnZero(rclib::ROBOTCONSYS_ARM_1, 0.2);
    if(ret != 0){
        printf("error: ret = %d\n", ret);
        exit(-1);
    }
    ret = robot->waitMotionCMDFinish(rclib::ROBOTCONSYS_ARM_1);
    if(ret != 0){
        printf("error: ret = %d\n", ret);
        exit(-1);
    }
    std::this_thread::sleep_for(std::chrono::milliseconds(1000));

    robot->setServoState(rclib::ROBOTCONSYS_ARM_1, rclib::SWITCHOFF);
    std::this_thread::sleep_for(std::chrono::milliseconds(1000));

    /*User code end*/

    // close robot
    robot->close();
    printf("close\n");

    // free robot
    (*freeRobotConSys)(robot);
}
