#!/bin/bash
# 亚龙智能车 - 一键停止脚本
# 停止所有相关进程

echo "正在停止智能车相关进程..."

# 停止 start.py
pkill -f "start_game start.py" 2>/dev/null

# 停止驱动节点
pkill -f "roslaunch robot_package" 2>/dev/null
pkill -f "robotic_node" 2>/dev/null

# 停止 roscore（可选，默认保留，因为可能还有其他节点在用）
# pkill -f roscore 2>/dev/null

sleep 1
echo "已完成。"
