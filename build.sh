#!/bin/bash
# =============================================================
# 亚龙智能车 - 一键编译脚本
# 用法: ./build.sh
# 用法: ./build.sh clean     (清空后重新编译)
# =============================================================

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$SCRIPT_DIR"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

log_info()    { echo -e "${BLUE}[信息]${NC} $1"; }
log_success() { echo -e "${GREEN}[成功]${NC} $1"; }
log_warn()    { echo -e "${YELLOW}[警告]${NC} $1"; }
log_error()   { echo -e "${RED}[错误]${NC} $1"; }

echo "=========================================="
log_info "开始编译工作空间"
log_info "目录: $SCRIPT_DIR"
echo "=========================================="
echo ""

if [ ! -d "src" ]; then
    log_error "未找到 src 目录，当前目录不是 Catkin 工作空间根目录"
    exit 1
fi

if ! command -v catkin_make &> /dev/null; then
    log_error "找不到 catkin_make 命令，请先配置 ROS 环境"
    echo "  例如: source /opt/ros/melodic/setup.bash"
    echo "  或:  source /opt/ros/noetic/setup.bash"
    exit 1
fi

# 清理模式
if [ "$1" = "clean" ] || [ "$1" = "--clean" ]; then
    log_warn "清理旧的编译产物 (build/, devel/)..."
    rm -rf build devel
    log_success "清理完成。"
    echo ""
fi

log_info "执行 catkin_make ..."
echo ""

catkin_make
RESULT=$?

echo ""
if [ $RESULT -eq 0 ]; then
    # 自动给可执行文件加权限
    START_PY="src/bringup/start_game/scripts/start.py"
    if [ -f "$START_PY" ] && [ ! -x "$START_PY" ]; then
        log_info "正在给 start.py 添加可执行权限..."
        chmod +x "$START_PY"
    fi

    ROBOTIC_NODE="devel/lib/robot_package/robotic_node"
    if [ -f "$ROBOTIC_NODE" ] && [ ! -x "$ROBOTIC_NODE" ]; then
        log_info "正在给 robotic_node 添加可执行权限..."
        chmod +x "$ROBOTIC_NODE"
    fi

    echo "=========================================="
    log_success "编译完成！"
    echo ""
    log_info "下一步:"
    echo "  ./start.sh "
    echo "=========================================="
else
    log_error "编译失败，请查看上方错误信息"
    exit $RESULT
fi
