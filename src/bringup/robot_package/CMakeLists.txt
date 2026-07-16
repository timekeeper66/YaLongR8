cmake_minimum_required(VERSION 3.0.2)
project(robot_package)


#option(PLATFORM_LINUX_ARM "linux arm" ON)
#set(TARGET_PLATFORM "linux_aarch64")
set(RCS_SDK_Dir "/home/nvidia/YaLongR8/src/bringup/RobotConSys_SDK/RobotConSys_SDK")
set(RCS_SDK_Include ${RCS_SDK_Dir}/include/)
message(STATUS "RCS_SDK_Include: ${RCS_SDK_Include}")

#if(PLATFORM_LINUX_ARM)
set(RCS_SDK_Lib ${RCS_SDK_Dir}/lib/linux_aarch64)
#else()
#  set(RCS_SDK_Lib ${RCS_SDK_Dir}/lib/linux_x64)
#endif()



## Compile as C++11, supported in ROS Kinetic and newer
add_compile_options(-std=c++11)

find_package(catkin REQUIRED COMPONENTS
  OpenCV
  roscpp
  std_msgs
  actionlib
  actionlib_msgs
  message_generation
  geometry_msgs
  tf
  nav_msgs
  sensor_msgs
  cv_bridge
)

## System dependencies are found with CMake's conventions
# find_package(Boost REQUIRED COMPONENTS system)


## Uncomment this if the package has a setup.py. This macro ensures
## modules and global scripts declared therein get installed
## See http://ros.org/doc/api/catkin/html/user_guide/setup_dot_py.html
# catkin_python_setup()

################################################
## Declare ROS messages, services and actions ##
################################################

## To declare and build messages, services or actions from within this
## package, follow these steps:
## * Let MSG_DEP_SET be the set of packages whose message types you use in
##   your messages/services/actions (e.g. std_msgs, actionlib_msgs, ...).
## * In the file package.xml:
##   * add a build_depend tag for "message_generation"
##   * add a build_depend and a exec_depend tag for each package in MSG_DEP_SET
##   * If MSG_DEP_SET isn't empty the following dependency has been pulled in
##     but can be declared for certainty nonetheless:
##     * add a exec_depend tag for "message_runtime"
## * In this file (CMakeLists.txt):
##   * add "message_generation" and every package in MSG_DEP_SET to
##     find_package(catkin REQUIRED COMPONENTS ...)
##   * add "message_runtime" and every package in MSG_DEP_SET to
##     catkin_package(CATKIN_DEPENDS ...)
##   * uncomment the add_*_files sections below as needed
##     and list every .msg/.srv/.action file to be processed
##   * uncomment the generate_messages entry below
##   * add every package in MSG_DEP_SET to generate_messages(DEPENDENCIES ...)

## Generate messages in the 'msg' folder
add_message_files(
  FILES
#   Message1.msg
  TR_Arm_Msg.msg
  TR_Vehicle_Msg.msg
  TR_Vehicle_IMU_Msg.msg
)

## Generate services in the 'srv' folder
add_service_files(
  FILES
  # Service1.srv
  MoveABSJ.srv
  MoveJ.srv
  MoveL.srv
  Servo.srv
  Claw.srv
  VehicleCommModel.srv
  VehicleServo.srv
  VehicleControl.srv
  ImageToFrame.srv
)

## Generate actions in the 'action' folder
add_action_files(
  FILES
  # Action1.action
  MoveABSJ.action
)

## Generate added messages and services with any dependencies listed here
generate_messages(
  DEPENDENCIES
  actionlib_msgs
  std_msgs
  sensor_msgs
)

################################################
## Declare ROS dynamic reconfigure parameters ##
################################################

## To declare and build dynamic reconfigure parameters within this
## package, follow these steps:
## * In the file package.xml:
##   * add a build_depend and a exec_depend tag for "dynamic_reconfigure"
## * In this file (CMakeLists.txt):
##   * add "dynamic_reconfigure" to
##     find_package(catkin REQUIRED COMPONENTS ...)
##   * uncomment the "generate_dynamic_reconfigure_options" section below
##     and list every .cfg file to be processed

## Generate dynamic reconfigure parameters in the 'cfg' folder
# generate_dynamic_reconfigure_options(
#   cfg/DynReconf1.cfg
#   cfg/DynReconf2.cfg
# )

###################################
## catkin specific configuration ##
###################################
## The catkin_package macro generates cmake config files for your package
## Declare things to be passed to dependent projects
## INCLUDE_DIRS: uncomment this if your package contains header files
## LIBRARIES: libraries you create in this project that dependent projects also need
## CATKIN_DEPENDS: catkin_packages dependent projects also need
## DEPENDS: system dependencies of this project that dependent projects also need
catkin_package(
#  INCLUDE_DIRS include
#  LIBRARIES robot_package
 CATKIN_DEPENDS roscpp std_msgs message_runtime actionlib_msgs geometry_msgs tf nav_msgs sensor_msgs cv_bridge
#  DEPENDS system_lib
)

###########
## Build ##
###########

## Specify additional locations of header files
## Your package locations should be listed before other locations
include_directories(
  include
  ${catkin_INCLUDE_DIRS}
  ${RCS_SDK_Include}
  ${RCS_SDK_Include}/3rdLib
  ${RCS_SDK_Include}/RobotConSys
  ${RCS_SDK_Include}/SystemLayer
  ${RCS_SDK_Include}/DeviceLayer
  ${RCS_SDK_Include}/RobSoft
  ${RCS_SDK_Include}/RobotInterpreter
)

link_directories(
  ${RCS_SDK_Lib}
)

## Declare a C++ library
# add_library(${PROJECT_NAME}
#   src/${PROJECT_NAME}/robot_package.cpp
# )

## Add cmake target dependencies of the library
## as an example, code may need to be generated before libraries
## either from message generation or dynamic reconfigure
# add_dependencies(${PROJECT_NAME} ${${PROJECT_NAME}_EXPORTED_TARGETS} ${catkin_EXPORTED_TARGETS})

## Declare a C++ executable
## With catkin_make all packages are built within a single CMake context
## The recommended prefix ensures that target names across packages don't collide
# add_executable(${PROJECT_NAME}_node src/robot_package_node.cpp)
set (COMMON_SRC 
  "./src/loard_library.cpp"
  "./src/robotic_man.cpp"
  "./include/robot_package/chess_board_detector.hpp"
)

add_executable(robotic_node src/robotic_node.cpp ${COMMON_SRC})
add_dependencies(robotic_node ${${PROJECT_NAME}_EXPORTED_TARGETS} ${catkin_EXPORTED_TARGETS})

target_link_libraries(robotic_node 
  SystemLayer 
  TCPConDev_Client
  RobSoft
  RobotConSys_Client
  pthread
  dl
  ${OpenCV_LIBS}
)
set_target_properties(robotic_node PROPERTIES LINK_FLAGS "-Wl,-rpath,.")
target_link_libraries(robotic_node
  ${catkin_LIBRARIES}
)


add_executable(eye_hand_calibration src/eye_in_hand.cpp )
add_dependencies(eye_hand_calibration ${${PROJECT_NAME}_EXPORTED_TARGETS} ${catkin_EXPORTED_TARGETS})
target_link_libraries(eye_hand_calibration 
  ${OpenCV_LIBS}
  ${catkin_LIBRARIES}
)
