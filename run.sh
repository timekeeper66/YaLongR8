#!/bin/bash
# 亚龙智能车 - 简洁版启动脚本
# 用法: ./run.sh

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

if [ ! -f "devel/setup.bash" ]; then
    echo "未检测到 devel/setup.bash，请先执行: ./build.sh"
    exit 1
fi

source devel/setup.bash

# 确保 start.py 有可执行权限
START_PY="src/bringup/start_game/scripts/start.py"
if [ -f "$START_PY" ] && [ ! -x "$START_PY" ]; then
    chmod +x "$START_PY"
fi

# 默认路线
MODE="${1:-11}"

# 启动 roscore
if ! rostopic list &> /dev/null; then
    echo "[信息] 启动 ROS 主节点..."
    roscore &
    ROSCORE_PID=$!
    for i in $(seq 1 10); do
        rostopic list &> /dev/null && break
        sleep 1
    done
fi

# 启动驱动节点
roslaunch robot_package robot_start.launch &
LAUNCH_PID=$!
sleep 5

# 启动小车
rosrun start_game start.py _mode:=$MODE

# 停止驱动节点
sleep 2
kill "$LAUNCH_PID" 2>/dev/null
wait "$LAUNCH_PID" 2>/dev/null

# 停止 roscore（如果是脚本启动的）
if [ -n "$ROSCORE_PID" ]; then
    kill "$ROSCORE_PID" 2>/dev/null
    wait "$ROSCORE_PID" 2>/dev/null
fi
