# YaLongR8

ROS小车控制项目。适用于YaLongR8平台，实现自动循迹功能。

## 快速开始

```bash
# 克隆项目
git clone https://github.com/yourusername/YaLongR8.git
cd YaLongR8

# 加权限并编译
chmod +x *.sh
./build.sh

# 启动小车
./start.sh
```

## 使用流程

```
(1) 拖入压缩包 YaLongR8.zip 到 /home/nvidia 目录
(2) 解压：unzip YaLongR8.zip
(3) 进入目录：cd YaLongR8
(4) 加权限：chmod +x *.sh src/bringup/start_game/scripts/start.py
(5) 编译：./build.sh
(6) 确认小车上电、网线连接好
(7) 启动：./start.sh
(8) 停止：按 Ctrl+C 或执行 ./stop.sh
```

## 脚本说明

| 脚本     | 用途             | 使用方法     |
| -------- | ---------------- | ------------ |
| build.sh | 编译工作空间     | `./build.sh` |
| start.sh | 一键启动（推荐） | `./start.sh` |
| run.sh   | 简洁版启动       | `./run.sh`   |
| stop.sh  | 停止小车         | `./stop.sh`  |

## 运行原理

整个系统分两步启动：

1. **驱动节点** - `roslaunch robot_package robot_start.launch`
   - 负责连接小车硬件，接收速度指令并驱动电机

2. **控制逻辑** - `rosrun start_game start.py`
   - 按预设路线发布 `/cmd_vel` 速度指令

脚本会自动帮你完成这两步。

## 环境要求

- Ubuntu 18.04 / 20.04
- ROS Melodic / Noetic
- Python 2.7 或 3.x

## 硬件连接

1. 小车上电
2. 网线连接主机和小车
3. 主机 IP 设置为 `192.168.10.x` 网段
4. 测试连通性：`ping 192.168.10.169`

## 常见问题

**Q1：提示 "未检测到 devel/setup.bash"**
A：先执行 `./build.sh` 完成编译。

**Q2：提示 "找不到 roslaunch / rosrun"**
A：ROS 环境没配好，执行：`source /opt/ros/melodic/setup.bash`

**Q3：提示 "Couldn't find executable named start.py"**
A：start.py 缺少可执行权限，执行：`chmod +x src/bringup/start_game/scripts/start.py`

**Q4：提示 "socket.error: [Errno 111] Connection refused"**
A：ROS Master 连不上，检查网络：
   - 小车上电了吗？
   - 网线插好了吗？
   - 主机 IP 在 192.168.10.x 网段吗？

**Q5：脚本运行了但小车不动**
A：先检查网络，再确认驱动节点是否启动成功。

**Q6：小车冲出去停不下来**
A：按 Ctrl+C；或执行 `./stop.sh`；最快方法是直接断电。

**Q7：想看速度指令**
A：新终端执行：`rostopic echo /cmd_vel`

## 项目结构

```
YaLongR8/
├── src/
│   └── bringup/
│       ├── robot_package/          # 驱动节点
│       ├── start_game/             # 控制逻辑
│       │   └── scripts/start.py    # 自动循迹主程序
│       ├── RobotConSys_SDK/        # 机器人控制 SDK
│       ├── hipnuc_imu/             # IMU 驱动
│       ├── ros_astra_camera/       # 摄像头驱动
│       └── ydlidar_ros_driver/     # 激光雷达驱动
├── build.sh
├── start.sh
├── run.sh
├── stop.sh
└── README.md
```

## 许可证

MIT License

## 贡献

欢迎提交 issue 和 pull request。
