# RobotConSys_SDK使用说明(C++)
## SDK简介

RobotConSys_SDK是面向图睿智能机器人二次开发的接口工具包。

支持跨平台的二次开发，包含：

* x64架构的Linux操作系统
* aarch64架构的Linux操作系统
* x64架构的Windows操作系统

提供丰富的二次开发接口：

* 移动机器人的运动控制及状态读取
* 机器人手臂运动控制及状态读取
* 机器人传感器数据读取，包含相机、IMU、超声波等

## SDK目录说明

接口工具包包含三个目录，分别为：

* include：包含SDK所需的所有头文件
* lib：包含SDK所需的Release版库文件
* example：包含SDK的入门使用demo

## SDK demo编译说明

SDK中包含3个demo，分别为：

* CamDevDemo.cpp：相机数据获取接口样例
* RobotConSysDemo.cpp：机器人手臂和底盘接口样例

在运行demo之前，请确保虚拟机器人或真实机器人已开启处于运行状态，同时确认目标设备的ip和端口号，并在样例代码中进行修改，修改位置如下：

* CamDevDemo.cpp

```c++
cam.init("192.168.139.128", 8070)
```

* RobotConSysDemo.cpp

```c++
robot->init("192.168.139.128", 8080);
```

### x64架构linux编译

1. 安装编译工具

```
$ sudo apt-get install gcc g++ make cmake
```

2. 安装opencv

```
$ sudo apt-get install libopencv-dev
$ sudo cp /usr/include/opencv4/opencv2/ /usr/include/ -r
```

3. 解压SDK并进入example目录

```
$ cd RobotConSys_SDK_path
$ tar -xzvf RobotConSys_SDK.tar.gz
$ cd ./RobotConSys_SDK/example/
```

4. 编译

```
$ mkdir build
$ cd build
$ cmake -DTARGET_PLATFORM=linux_x64 ..
$ make
```

5. 复制依赖库到当前目录下

```
$ cp ../../lib/linux_x64/* .
```

6. 测试运行

```
$ sudo ./CamDevDemo
$ sudo ./VehicleMotionDevDemo
$ sudo ./RobotConSysDemo
```

### aarch64架构linux编译

1. 安装编译工具

```
$ sudo apt-get install gcc g++ make cmake
```

2. 安装opencv

```
$ sudo apt-get install libopencv-dev
$ sudo cp /usr/include/opencv4/opencv2/ /usr/include/ -r
```

3. 解压SDK并进入example目录

```
$ cd RobotConSys_SDK_path
$ tar -xzvf RobotConSys_SDK.tar.gz
$ cd ./RobotConSys_SDK/example/
```

4. 编译

```
$ mkdir build
$ cd build
$ cmake -DTARGET_PLATFORM=linux_aarch64 ..
$ make
```

5. 复制依赖库到当前目录下

```
$ cp ../../lib/linux_aarch64/* .
```

6. 测试运行

```
$ sudo ./CamDevDemo
$ sudo ./RobotConSysDemo
```

### x64架构windows编译

1. 下载并安装Visual Studio，cmake
2. 下载opencv，解压并将库路径添加到环境变量，以解压到C盘为例

```
C:\opencv\build\x64\vc16\bin
C:\opencv\build\x64\vc16\lib
```

3. 解压SDK并进入example目录
4. 在example目录下新建build文件夹，进入build文件夹，利用终端运行以下命令

```
$ cmake -DTARGET_PLATFORM=win_x64 ..
```

5. 用Visutal Studio打开生成的`TEST RobotConSys.sln`文件，修改CamDevDemo项目属性，链接器——系统，堆栈保留大小和堆栈提交大小都改为10000000，即e7，选择Release模式编译
6. 编译完成后，将`RobotConSys_SDK/lib/win_x64/`文件夹下的所有`.dll`库文件复制到`RobotConSys_SDK/example/build`文件夹下
7. 将两个demo依次右击设为启动项目，分别测试运行