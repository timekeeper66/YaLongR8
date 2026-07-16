#!/bin/bash
# =============================================================
# 亚龙智能车 - 一键启动脚本
# 用法: ./start.sh
#
# 完整流程:
#   1. 检查编译环境 + 权限
#   2. 启动驱动节点 (roslaunch robot_package robot_start.launch)
#   3. 运行 start.py 控制小车运动
#   4. Ctrl+C 停止并清理
# =============================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

# ============ 颜色输出 ============
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

log_info()    { echo -e "${BLUE}[信息]${NC} $1"; }
log_success() { echo -e "${GREEN}[成功]${NC} $1"; }
log_warn()    { echo -e "${YELLOW}[警告]${NC} $1"; }
log_error()   { echo -e "${RED}[错误]${NC} $1"; }

# ============ 环境检查 ============
if [ ! -f "devel/setup.bash" ]; then
    log_error "未检测到 devel/setup.bash"
    log_info "请先执行: ./build.sh 完成编译"
    exit 1
fi

source devel/setup.bash

# 确保 start.py 有可执行权限（rosrun 需要）
START_PY="src/bringup/start_game/scripts/start.py"
if [ -f "$START_PY" ] && [ ! -x "$START_PY" ]; then
    log_warn "start.py 缺少可执行权限，正在自动添加..."
    chmod +x "$START_PY"
fi

if ! command -v roslaunch &> /dev/null; then
    log_error "找不到 roslaunch 命令，请确认ROS环境已配置"
    exit 1
fi

# ============ 默认路线 ============
MODE="${1:-31}"

# ============ 清理函数 ============
cleanup() {
    echo ""
    log_warn "收到停止信号，正在清理..."

    # 停止 start.py
    pkill -f "start_game start.py" 2>/dev/null

    # 停止 roslaunch（即驱动节点）
    if [ -n "$LAUNCH_PID" ] && kill -0 "$LAUNCH_PID" 2>/dev/null; then
        kill "$LAUNCH_PID" 2>/dev/null
        wait "$LAUNCH_PID" 2>/dev/null
    fi

    pkill -f "roslaunch robot_package" 2>/dev/null
    pkill -f "robotic_node" 2>/dev/null

    # 如果是脚本自己启动的 roscore，也一起停止
    if [ -n "$ROSCORE_PID" ] && kill -0 "$ROSCORE_PID" 2>/dev/null; then
        log_info "停止 ROS 主节点..."
        kill "$ROSCORE_PID" 2>/dev/null
        wait "$ROSCORE_PID" 2>/dev/null
    fi

    log_info "所有进程已停止。"
    exit 0
}
trap cleanup SIGINT SIGTERM

# ============ 主流程 ============
echo ""
echo "=========================================="
log_info "亚龙智能车一键启动"
log_info "工作目录: $SCRIPT_DIR"
echo "=========================================="
echo ""

# --- 步骤 0: 启动 roscore (ROS 主节点) ---
log_info "[步骤 0/2] 检查 ROS 主节点..."
if rostopic list &> /dev/null; then
    log_info "ROS 主节点已在运行"
else
    log_warn "ROS 主节点未运行，正在启动..."
    roscore &
    ROSCORE_PID=$!

    # 等待 roscore 就绪（最多等 10 秒）
    log_info "等待 ROS 主节点就绪..."
    for i in $(seq 1 10); do
        if rostopic list &> /dev/null; then
            break
        fi
        sleep 1
    done

    if ! rostopic list &> /dev/null; then
        log_error "ROS 主节点启动失败"
        kill "$ROSCORE_PID" 2>/dev/null
        exit 1
    fi
    log_success "ROS 主节点已启动 (PID: $ROSCORE_PID)"
fi

# --- 步骤 1: 启动驱动节点 ---
echo ""
log_info "[步骤 1/2] 启动驱动节点..."
roslaunch robot_package robot_start.launch &
LAUNCH_PID=$!

# 等待驱动节点启动
log_info "等待驱动节点就绪..."
sleep 5

# 检查 roslaunch 是否还在运行
if ! kill -0 "$LAUNCH_PID" 2>/dev/null; then
    log_error "驱动节点启动失败，请检查:"
    echo "  - 小车是否已上电并连接"
    echo "  - 小车IP地址是否正确 (robot_start.launch 中配置)"
    echo "  - 主机是否在 192.168.10.x 网段"
    exit 1
fi

log_success "驱动节点已启动 (PID: $LAUNCH_PID)"

# --- 步骤 2: 启动小车运动 ---
echo ""
log_info "[步骤 2/2] 启动小车运动..."
echo ""
echo "=========================================="
log_info "小车开始运动！按 Ctrl+\ 可随时停止"
echo "=========================================="
echo ""

rosrun start_game start.py _mode:=$MODE
RUN_EXIT=$?

echo ""
echo "------------------------------------------"
if [ $RUN_EXIT -eq 0 ]; then
    log_success "运行完成！"
else
    log_warn "运行结束（退出码: $RUN_EXIT）"
fi

# 等待一小会儿，让驱动节点处理完
sleep 2

# 停止驱动节点
if kill -0 "$LAUNCH_PID" 2>/dev/null; then
    kill "$LAUNCH_PID" 2>/dev/null
    wait "$LAUNCH_PID" 2>/dev/null
fi

log_success "全部结束。"
