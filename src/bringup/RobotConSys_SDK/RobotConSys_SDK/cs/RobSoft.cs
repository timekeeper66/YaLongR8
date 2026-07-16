using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RobConSysCS
{
    public enum JOINTINDEX { JOINT_NONE = -2, JOINT_WHOLE = -1, JOINT_1, JOINT_2, JOINT_3, JOINT_4, JOINT_5, JOINT_6, JOINT_7, JOINT_8, JOINT_9, JOINT_10, JOINT_11, JOINT_12, JOINT_13, JOINT_14, JOINT_15 };   // 关节序号
    public enum TERMINALINDEX { TERMINAL_X, TERMINAL_Y, TERMINAL_Z, TERMINAL_A, TERMINAL_B, TERMINAL_C }; // 末端位姿序号
    public enum POINTINDEX { POINT_X, POINT_Y, POINT_Z }; // 点的序号
    public enum ATTITUDEINDEX { ATTITUDE_A, ATTITUDE_B, ATTITUDE_C }; // 姿态角序号
    public enum QUATERNIONINDEX { QUATERNION_W, QUATERNION_X, QUATERNION_Y, QUATERNION_Z };   // 四元素序号
    public enum COORDINATESYSTEM { COORDINATE_BASE, COORDINATE_WORK, COORDINATE_TOOL, COORDINATE_JOINT };    // 坐标系
    public enum CIRCLETYPE { PART_CIRCLE, WHOLE_CIRCLE };    // 部分圆，整圆
    public enum CURVETYPE { OPEN_CURVE, CLOSE_CURVE };    // 部分圆，整圆

    public class Joints
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_jointsPtr;

        [DllImport(dllName, EntryPoint = "createJoints")]
        public static extern IntPtr createJoints();
        public Joints()
        {
            m_jointsPtr = createJoints();
        }
        public Joints(int dof)
        {
            m_jointsPtr = createJoints();
            SetDof(dof);
        }
        [DllImport(dllName, EntryPoint = "freeJoints")]
        public static extern void freeJoints(IntPtr jointsPtr);
        ~Joints()
        {
            freeJoints(m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_Joints")]
        public static extern void copy_Joints(IntPtr jointsPtr, IntPtr srcJointsPtr);
        public void Copy(Joints joints)
        {
            copy_Joints(m_jointsPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setDof_Joints")]
        public static extern void setDof_Joints(IntPtr jointsPtr, int dof);
        public void SetDof(int dof)
        {
            setDof_Joints(m_jointsPtr, dof);
        }

        [DllImport(dllName, EntryPoint = "getDof_Joints")]
        public static extern int getDof_Joints(IntPtr jointsPtr);
        public int GetDof()
        {
            return getDof_Joints(m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "set_Joints")]
        public static extern void set_Joints(IntPtr jointsPtr, JOINTINDEX index, double value);
        public void Set(JOINTINDEX index, double value)
        {
            set_Joints(m_jointsPtr, index, value);
        }

        [DllImport(dllName, EntryPoint = "get_Joints")]
        public static extern double get_Joints(IntPtr jointsPtr, JOINTINDEX index);
        public double Get(JOINTINDEX index)
        {
            return get_Joints(m_jointsPtr, index);
        }

        [DllImport(dllName, EntryPoint = "print_Joints")]
        public static extern void print_Joints(IntPtr jointsPtr, string str);
        public void Print(string str)
        {
            print_Joints(m_jointsPtr, str);
        }

        //public static Joints operator =(Joints dstJoints, Joints srcJoints)
        //{
        //    Joints newJoints = new Joints();
        //    return newJoints;
        //}
        public double[] ToArray() {
            int dof = GetDof();
            double[] rt = new double[dof];
            for (int i=0;i<dof;i++) {
                rt[i] = Get((JOINTINDEX)i);
            }
            return rt;
        }
    }

    public class JointsList
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_jointsListPtr;

        [DllImport(dllName, EntryPoint = "creatJointsList")]
        public static extern IntPtr creatJointsList();
        public JointsList()
        {
            m_jointsListPtr = creatJointsList();
        }

        [DllImport(dllName, EntryPoint = "freeJointsList")]
        public static extern void freeJointsList(IntPtr jointsListPtr);
        ~JointsList()
        {
            freeJointsList(m_jointsListPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_JointsList")]
        public static extern void copy_JointsList(IntPtr jointsListPtr, IntPtr srcJointsListPtr);
        public void Copy(JointsList jointsList)
        {
            copy_JointsList(m_jointsListPtr, jointsList.m_jointsListPtr);
        }

        [DllImport(dllName, EntryPoint = "clear_JointsList")]
        public static extern void clear_JointsList(IntPtr jointsListPtr);
        public void Clear()
        {
            clear_JointsList(m_jointsListPtr);
        }

        [DllImport(dllName, EntryPoint = "push_back_JointsList")]
        public static extern void push_back_JointsList(IntPtr jointsListPtr, IntPtr jointsPtr);
        public void Push_back(Joints joints)
        {
            push_back_JointsList(m_jointsListPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "size_JointsList")]
        public static extern int size_JointsList(IntPtr jointsListPtr);
        public int Size()
        {
            return size_JointsList(m_jointsListPtr);
        }
    }

    public class Point
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_pointPtr;

        [DllImport(dllName, EntryPoint = "creatPoint")]
        public static extern IntPtr creatPoint();
        public Point()
        {
            m_pointPtr = creatPoint();
        }

        [DllImport(dllName, EntryPoint = "freePoint")]
        public static extern void freePoint(IntPtr pointPtr);
        ~Point()
        {
            freePoint(m_pointPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_Point")]
        public static extern void copy_Point(IntPtr pointPtr, IntPtr srcPointPtr);
        public void Copy(Point point)
        {
            copy_Point(m_pointPtr, point.m_pointPtr);
        }

        [DllImport(dllName, EntryPoint = "set_Point")]
        public static extern void set_Point(IntPtr pointPtr, POINTINDEX index, double value);
        public void Set(POINTINDEX index, double value)
        {
            set_Point(m_pointPtr, index, value);
        }

        [DllImport(dllName, EntryPoint = "get_Point")]
        public static extern double get_Point(IntPtr pointPtr, POINTINDEX index);
        public double Get(POINTINDEX index)
        {
            return get_Point(m_pointPtr, index);
        }

        [DllImport(dllName, EntryPoint = "print_Point")]
        public static extern void print_Point(IntPtr pointPtr, string str);
        public void Print(string str)
        {
            print_Point(m_pointPtr, str);
        }
    }

    public class Terminal
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_terminalPtr;

        [DllImport(dllName, EntryPoint = "createTerminal")]
        public static extern IntPtr createTerminal();
        public Terminal()
        {
            m_terminalPtr = createTerminal();
        }
        public Terminal(double[] value)
        {
            m_terminalPtr = createTerminal();
            if (value.Length == 6) {
                for(int i=0;i<6;i++)    
                    Set((TERMINALINDEX)i, value[i]);
            }
        }

        [DllImport(dllName, EntryPoint = "freeTerminal")]
        public static extern void freeTerminal(IntPtr terminalPtr);
        ~Terminal()
        {
            freeTerminal(m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_Terminal")]
        public static extern void copy_Terminal(IntPtr terminalPtr, IntPtr srcTerminalPtr);
        public void Copy(Terminal terminal)
        {
            copy_Terminal(m_terminalPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "set_Terminal")]
        public static extern void set_Terminal(IntPtr terminalPtr, TERMINALINDEX index, double value);
        public void Set(TERMINALINDEX index, double value)
        {
            set_Terminal(m_terminalPtr, index, value);
        }

        [DllImport(dllName, EntryPoint = "get_Terminal")]
        public static extern double get_Terminal(IntPtr terminalPtr, TERMINALINDEX index);
        public double Get(TERMINALINDEX index)
        {
            return get_Terminal(m_terminalPtr, index);
        }

        [DllImport(dllName, EntryPoint = "print_Terminal")]
        public static extern void print_Terminal(IntPtr terminalPtr, string str);
        public void Print(string str)
        {
            print_Terminal(m_terminalPtr, str);
        }
        public double[] ToArray() {
            double[] rt = new double[6];
            for (int i=0;i<6;i++) {
                rt[i] = Get((TERMINALINDEX)i);
            }
            return rt;
        }
    }

    public class TerminalList
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_terminalListPtr;

        [DllImport(dllName, EntryPoint = "creatTerminalList")]
        public static extern IntPtr creatTerminalList();
        public TerminalList()
        {
            m_terminalListPtr = creatTerminalList();
        }

        [DllImport(dllName, EntryPoint = "freeTerminalList")]
        public static extern void freeTerminalList(IntPtr terminalListPtr);
        ~TerminalList()
        {
            freeTerminalList(m_terminalListPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_TerminalList")]
        public static extern void copy_TerminalList(IntPtr terminalListPtr, IntPtr srcTerminalListPtr);
        public void Copy(TerminalList terminalList)
        {
            copy_TerminalList(m_terminalListPtr, terminalList.m_terminalListPtr);
        }

        [DllImport(dllName, EntryPoint = "clear_TerminalList")]
        public static extern void clear_TerminalList(IntPtr terminalListPtr);
        public void Clear()
        {
            clear_TerminalList(m_terminalListPtr);
        }

        [DllImport(dllName, EntryPoint = "push_back_TerminalList")]
        public static extern void push_back_TerminalList(IntPtr terminalListPtr, IntPtr terminalPtr);
        public void Push_back(Terminal terminal)
        {
            push_back_TerminalList(m_terminalListPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "size_TerminalList")]
        public static extern int size_TerminalList(IntPtr terminalListPtr);
        public int Size()
        {
            return size_TerminalList(m_terminalListPtr);
        }
    }

    public class RobotMotion
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_robotMotionPtr;

        [DllImport(dllName, EntryPoint = "createRobotMotion")]
        public static extern IntPtr createRobotMotion();
        public RobotMotion()
        {
            m_robotMotionPtr = createRobotMotion();
        }

        [DllImport(dllName, EntryPoint = "freeRobotMotion")]
        public static extern void freeRobotMotion(IntPtr robotMotionPtr);
        ~RobotMotion()
        {
            freeRobotMotion(m_robotMotionPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotMotion")]
        public static extern void copy_RobotMotion(IntPtr robotMotionPtr, IntPtr srcRobotMotionPtr);
        public void Copy(RobotMotion robotMotion)
        {
            copy_RobotMotion(m_robotMotionPtr, robotMotion.m_robotMotionPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentJointPosition_RobotMotion")]
        public static extern void setCurrentJointPosition_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentJointPosition(Joints joints)
        {
            setCurrentJointPosition_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentJointPosition_RobotMotion")]
        public static extern void getCurrentJointPosition_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentJointPosition(ref Joints joints)
        {
            getCurrentJointPosition_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentJointVelocity_RobotMotion")]
        public static extern void setCurrentJointVelocity_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentJointVelocity(Joints joints)
        {
            setCurrentJointVelocity_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentJointVelocity_RobotMotion")]
        public static extern void getCurrentJointVelocity_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentJointVelocity(ref Joints joints)
        {
            getCurrentJointVelocity_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentJointAcceleration_RobotMotion")]
        public static extern void setCurrentJointAcceleration_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentJointAcceleration(Joints joints)
        {
            setCurrentJointAcceleration_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentJointAcceleration_RobotMotion")]
        public static extern void getCurrentJointAcceleration_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentJointAcceleration(ref Joints joints)
        {
            getCurrentJointAcceleration_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentJointTorque_RobotMotion")]
        public static extern void setCurrentJointTorque_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentJointTorque(Joints joints)
        {
            setCurrentJointTorque_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentJointTorque_RobotMotion")]
        public static extern void getCurrentJointTorque_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentJointTorque(ref Joints joints)
        {
            getCurrentJointTorque_RobotMotion(m_robotMotionPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentTerminal_RobotMotion")]
        public static extern void setCurrentTerminal_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentTerminal(Terminal terminal)
        {
            setCurrentTerminal_RobotMotion(m_robotMotionPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentTerminal_RobotMotion")]
        public static extern void getCurrentTerminal_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentTerminal(ref Terminal terminal)
        {
            getCurrentTerminal_RobotMotion(m_robotMotionPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentWorkTerminal_RobotMotion")]
        public static extern void setCurrentWorkTerminal_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void SetCurrentWorkTerminal(Terminal terminal)
        {
            setCurrentWorkTerminal_RobotMotion(m_robotMotionPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getCurrentWorkTerminal_RobotMotion")]
        public static extern void getCurrentWorkTerminal_RobotMotion(IntPtr robotMotionPtr, IntPtr jointsPtr);
        public void GetCurrentWorkTerminal(ref Terminal terminal)
        {
            getCurrentWorkTerminal_RobotMotion(m_robotMotionPtr, terminal.m_terminalPtr);
        }
    }

    public class Mat3x3
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_matPtr;

        [DllImport(dllName, EntryPoint = "createMat3x3")]
        public static extern IntPtr createMat3x3();
        public Mat3x3()
        {
            m_matPtr = createMat3x3();
        }

        [DllImport(dllName, EntryPoint = "freeMax3x3")]
        public static extern void freeMax3x3(IntPtr matPtr);
        ~Mat3x3()
        {
            freeMax3x3(m_matPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_Max3x3")]
        public static extern void copy_Max3x3(IntPtr matPtr, IntPtr srcMatPtr);
        public void Copy(Mat3x3 mat)
        {
            copy_Max3x3(m_matPtr, mat.m_matPtr);
        }

        [DllImport(dllName, EntryPoint = "setValue_Mat3x3")]
        public static extern void setValue_Mat3x3(IntPtr matPtr, int i, int j, double value);
        public void SetValue(int i, int j, double value)
        {
            setValue_Mat3x3(m_matPtr,i, j, value);
        }

        [DllImport(dllName, EntryPoint = "getValue_Mat3x3")]
        public static extern double getValue_Mat3x3(IntPtr matPtr, int i, int j);
        public double GetValue(int i, int j)
        {
            return getValue_Mat3x3(m_matPtr, i, j);
        }

        [DllImport(dllName, EntryPoint = "print_Max3x3")]
        public static extern void print_Max3x3(IntPtr matPtr);
        public void Print()
        {
            print_Max3x3(m_matPtr);
        }
    }

    public class Tool
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_toolPtr;

        [DllImport(dllName, EntryPoint = "createTool")]
        public static extern IntPtr createTool();
        public Tool()
        {
            m_toolPtr = createTool();
        }

        [DllImport(dllName, EntryPoint = "freeTool")]
        public static extern void freeTool(IntPtr toolPtr);
        ~Tool()
        {
            freeTool(m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_Tool")]
        public static extern void copy_Tool(IntPtr toolPtr, IntPtr srcToolPtr);
        public void Copy(Tool tool)
        {
            copy_Tool(m_toolPtr, tool.m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "setFrame_Tool")]
        public static extern void setFrame_Tool(IntPtr toolPtr, IntPtr terminalPtr);
        public void SetFrame(Terminal terminal)
        {
            setFrame_Tool(m_toolPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getFrame_Tool")]
        public static extern void getFrame_Tool(IntPtr toolPtr, IntPtr terminalPtr);
        public void GetFrame(ref Terminal terminal)
        {
            getFrame_Tool(m_toolPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setQuality_Tool")]
        public static extern void setQuality_Tool(IntPtr toolPtr, double quality);
        public void SetQuality(double quality)
        {
            setQuality_Tool(m_toolPtr, quality);
        }

        [DllImport(dllName, EntryPoint = "getQuality_Tool")]
        public static extern double getQuality_Tool(IntPtr toolPtr);
        public double GetQuality()
        {
            return getQuality_Tool(m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "setCentroid_Tool")]
        public static extern void setCentroid_Tool(IntPtr toolPtr, IntPtr pointPtr);
        public void SetCentroid(Point point)
        {
            setCentroid_Tool(m_toolPtr, point.m_pointPtr);
        }

        [DllImport(dllName, EntryPoint = "getCentroid_Tool")]
        public static extern void getCentroid_Tool(IntPtr toolPtr, IntPtr pointPtr);
        public void GetCentroid(ref Point point)
        {
            getCentroid_Tool(m_toolPtr, point.m_pointPtr);
        }

        [DllImport(dllName, EntryPoint = "setInertialTensor_Tool")]
        public static extern void setInertialTensor_Tool(IntPtr toolPtr, IntPtr matPtr);
        public void SetInertialTensor(Mat3x3 mat)
        {
            setInertialTensor_Tool(m_toolPtr, mat.m_matPtr);
        }

        [DllImport(dllName, EntryPoint = "getInertialTensor_Tool")]
        public static extern void getInertialTensor_Tool(IntPtr toolPtr, IntPtr matPtr);
        public void GetInertialTensor(ref Mat3x3 mat)
        {
            getInertialTensor_Tool(m_toolPtr, mat.m_matPtr);
        }

        [DllImport(dllName, EntryPoint = "print_Tool")]
        public static extern void print_Tool(IntPtr toolPtr);
        public void Print()
        {
            print_Tool(m_toolPtr);
        }
    }

    public enum ROBOTTYPE
    {                         // 机器人类型
        ROBSOFT_SERIAL_SIX_CONVENTION,      // 六轴常规串联机型
        ROBSOFT_SERIAL_SIX_COOPERATION,     // 六轴协作串联机型
        ROBSOFT_SCARA_THREE_ONERF,          // SCARA机器人，一轴可升降，无四轴
        ROBSOFT_SCARA_FOUR_ONERF,           // SCARA机器人，一轴可升降
        ROBSOFT_SCARA_FOUR_THREERF,         // SCARA机器人，三轴可升降
        ROBSOFT_SCARA_FOUR_FOURRF,          // SCARA机器人，四轴可升降
        ROBSOFT_DELTA_FOUR,                 // DELTA机器人，单姿态
        ROBSOFT_DELTA_SIX,                  // DELTA机器人，三姿态
        ROBSOFT_SERIAL_SEVEN_SRS,           // 七轴SRS串联机型
        ROBSOFT_CARTESIAN,                  // 直角坐标机器人
        ROBSOFT_SERIAL_FOUR_CONVENTION,     // 四轴常规串联机型，码垛
        ROBSOFT_SERIAL_GENERAL,             // 通用串联机型
    };

    public enum EXTERNAL_AXIS_TYPE
    {                // 外部轴类型
        ROBSOFT_EXTERNAL_AXIS_NORMAL,       // 通用外部轴
        ROBSOFT_EXTERNAL_AXIS_TRANSLATION,  // 行走轴
        ROBSOFT_EXTERNAL_AXIS_POSITIONER,   // 变位机
        ROBSOFT_EXTERNAL_AXIS_ARM_DUAL_3,   // 双臂三轴
        ROBSOFT_EXTERNAL_AXIS_Embodied_SINGLE_3,  // 单臂复合三轴
        ROBSOFT_EXTERNAL_AXIS_Embodied_DUAL_4,  // 双臂复合四轴
    };

    public enum ROBSOFT_JOINT_TYPE
    {
        ROBSOFT_JOINT_TYPE_ROTATE,          // 旋转机构
        ROBSOFT_JOINT_TYPE_MOVE,            // 平移机构
    };

    public class RobotParameter
    {
        const string dllName = "RobSoft_wrapper";
        public readonly IntPtr m_robotParameterPtr;

        [DllImport(dllName, EntryPoint = "createRobotParameter")]
        public static extern IntPtr createRobotParameter();
        public RobotParameter()
        {
            m_robotParameterPtr = createRobotParameter();
        }

        [DllImport(dllName, EntryPoint = "freeRobotParameter")]
        public static extern void freeRobotParameter(IntPtr robotParameterPtr);
        ~RobotParameter()
        {
            freeRobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotParameter")]
        public static extern void copy_RobotParameter(IntPtr robotParameterPtr, IntPtr srcRobotParameterPtr);
        public void Copy(RobotParameter robotParameter)
        {
            copy_RobotParameter(m_robotParameterPtr, robotParameter.m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setSamplePeriod_RobotParameter")]
        public static extern void setSamplePeriod_RobotParameter(IntPtr robotParameterPtr, double samplePeriod);
        public void SetSamplePeriod(double samplePeriod)
        {
            setSamplePeriod_RobotParameter(m_robotParameterPtr, samplePeriod);
        }

        [DllImport(dllName, EntryPoint = "getSamplePeriod_RobotParameter")]
        public static extern double getSamplePeriod_RobotParameter(IntPtr robotParameterPtr);
        public double GetSamplePeriod()
        {
            return getSamplePeriod_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotDOF_RobotParameter")]
        public static extern void setRobotDOF_RobotParameter(IntPtr robotParameterPtr, int robotDof);
        public void SetRobotDOF(int robotDof)
        {
            setRobotDOF_RobotParameter(m_robotParameterPtr, robotDof);
        }

        [DllImport(dllName, EntryPoint = "getRobotDOF_RobotParameter")]
        public static extern int getRobotDOF_RobotParameter(IntPtr robotParameterPtr);
        public int GetRobotDOF()
        {
            return getRobotDOF_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setExternDOF_RobotParameter")]
        public static extern void setExternDOF_RobotParameter(IntPtr robotParameterPtr, int externDof);
        public void SetExternDOF(int externDof)
        {
            setExternDOF_RobotParameter(m_robotParameterPtr, externDof);
        }

        [DllImport(dllName, EntryPoint = "getExternDOF_RobotParameter")]
        public static extern int getExternDOF_RobotParameter(IntPtr robotParameterPtr);
        public int GetExternDOF()
        {
            return getExternDOF_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "getWholeDOF_RobotParameter")]
        public static extern int getWholeDOF_RobotParameter(IntPtr robotParameterPtr);
        public int GetWholeDOF()
        {
            return getWholeDOF_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotType_RobotParameter")]
        public static extern void setRobotType_RobotParameter(IntPtr robotParameterPtr, ROBOTTYPE robotType);
        public void SetRobotType(ROBOTTYPE robotType)
        {
            setRobotType_RobotParameter(m_robotParameterPtr, robotType);
        }

        [DllImport(dllName, EntryPoint = "getRobotType_RobotParameter")]
        public static extern ROBOTTYPE getRobotType_RobotParameter(IntPtr robotParameterPtr);
        public ROBOTTYPE GetRobotType()
        {
            return getRobotType_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setExternalAxisType_RobotParameter")]
        public static extern void setExternalAxisType_RobotParameter(IntPtr robotParameterPtr, EXTERNAL_AXIS_TYPE robotType);
        public void SetExternalAxisType(EXTERNAL_AXIS_TYPE robotType)
        {
            setExternalAxisType_RobotParameter(m_robotParameterPtr, robotType);
        }

        [DllImport(dllName, EntryPoint = "getExternalAxisType_RobotParameter")]
        public static extern EXTERNAL_AXIS_TYPE getExternalAxisType_RobotParameter(IntPtr robotParameterPtr);
        public EXTERNAL_AXIS_TYPE GetExternalAxisType()
        {
            return getExternalAxisType_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setRateTorque_RobotParameter")]
        public static extern void setRateTorque_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetRateTorque(Joints joints)
        {
            setRateTorque_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getRateTorque_RobotParameter")]
        public static extern void getRateTorque_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetRateTorque(ref Joints joints)
        {
            getRateTorque_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setPosResolution_RobotParameter")]
        public static extern void setPosResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetPosResolution(Joints joints)
        {
            setPosResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getPosResolution_RobotParameter")]
        public static extern void getPosResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetPosResolution(ref Joints joints)
        {
            getPosResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setVelResolution_RobotParameter")]
        public static extern void setVelResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetVelResolution(Joints joints)
        {
            setVelResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getVelResolution_RobotParameter")]
        public static extern void getVelResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetVelResolution(ref Joints joints)
        {
            getVelResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setTorResolution_RobotParameter")]
        public static extern void setTorResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetTorResolution(Joints joints)
        {
            setTorResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getTorResolution_RobotParameter")]
        public static extern void getTorResolution_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetTorResolution(ref Joints joints)
        {
            getTorResolution_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setReduceRatio_RobotParameter")]
        public static extern void setReduceRatio_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetReduceRatio(Joints joints)
        {
            setReduceRatio_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getReduceRatio_RobotParameter")]
        public static extern void getReduceRatio_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetReduceRatio(ref Joints joints)
        {
            getReduceRatio_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setPitch_RobotParameter")]
        public static extern void setPitch_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetPitch(Joints joints)
        {
            setPitch_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getPitch_RobotParameter")]
        public static extern void getPitch_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetPitch(ref Joints joints)
        {
            getPitch_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setCouplingRatio_RobotParameter")]
        public static extern void setCouplingRatio_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetCouplingRatio(Joints joints)
        {
            setCouplingRatio_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getCouplingRatio_RobotParameter")]
        public static extern void getCouplingRatio_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetCouplingRatio(ref Joints joints)
        {
            getCouplingRatio_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointRangeMinus_RobotParameter")]
        public static extern void setJointRangeMinus_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetJointRangeMinus(Joints joints)
        {
            setJointRangeMinus_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointRangeMinus_RobotParameter")]
        public static extern void getJointRangeMinus_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointRangeMinus(ref Joints joints)
        {
            getJointRangeMinus_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointRangePlus_RobotParameter")]
        public static extern void setJointRangePlus_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetJointRangePlus(Joints joints)
        {
            setJointRangePlus_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointRangePlus_RobotParameter")]
        public static extern void getJointRangePlus_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointRangePlus(ref Joints joints)
        {
            getJointRangePlus_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxVelRange_RobotParameter")]
        public static extern void setJointMaxVelRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetJointMaxVelRange(Joints joints)
        {
            setJointMaxVelRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxVelRange_RobotParameter")]
        public static extern void getJointMaxVelRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxVelRange(ref Joints joints)
        {
            getJointMaxVelRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxAccRange_RobotParameter")]
        public static extern void setJointMaxAccRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetJointMaxAccRange(Joints joints)
        {
            setJointMaxAccRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxAccRange_RobotParameter")]
        public static extern void getJointMaxAccRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxAccRange(ref Joints joints)
        {
            getJointMaxAccRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxJerkRange_RobotParameter")]
        public static extern void setJointMaxJerkRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetJointMaxJerkRange(Joints joints)
        {
            setJointMaxJerkRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxJerkRange_RobotParameter")]
        public static extern void getJointMaxJerkRange_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxJerkRange(ref Joints joints)
        {
            getJointMaxJerkRange_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxVelRatio_RobotParameter")]
        public static extern void setJointMaxVelRatio_RobotParameter(IntPtr robotParameterPtr, double jointMaxVelRatio);
        public void SetJointMaxVelRatio(double jointMaxVelRatio)
        {
            setJointMaxVelRatio_RobotParameter(m_robotParameterPtr, jointMaxVelRatio);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxVelRatio_RobotParameter")]
        public static extern double getJointMaxVelRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetJointMaxVelRatio()
        {
            return getJointMaxVelRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxAccRatio_RobotParameter")]
        public static extern void setJointMaxAccRatio_RobotParameter(IntPtr robotParameterPtr, double jointMaxAccRatio);
        public void SetJointMaxAccRatio(double jointMaxAccRatio)
        {
            setJointMaxAccRatio_RobotParameter(m_robotParameterPtr, jointMaxAccRatio);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxAccRatio_RobotParameter")]
        public static extern double getJointMaxAccRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetJointMaxAccRatio()
        {
            return getJointMaxAccRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointMaxJerkRatio_RobotParameter")]
        public static extern void setJointMaxJerkRatio_RobotParameter(IntPtr robotParameterPtr, double jointMaxJerkRatio);
        public void SetJointMaxJerkRatio(double jointMaxJerkRatio)
        {
            setJointMaxJerkRatio_RobotParameter(m_robotParameterPtr, jointMaxJerkRatio);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxJerkRatio_RobotParameter")]
        public static extern double getJointMaxJerkRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetJointMaxJerkRatio()
        {
            return getJointMaxJerkRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxVel_RobotParameter")]
        public static extern void getJointMaxVel_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxVel(ref Joints joints)
        {
            getJointMaxVel_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxAcc_RobotParameter")]
        public static extern void getJointMaxAcc_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxAcc(ref Joints joints)
        {
            getJointMaxAcc_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointMaxJerk_RobotParameter")]
        public static extern void getJointMaxJerk_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetJointMaxJerk(ref Joints joints)
        {
            getJointMaxJerk_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalRangeMinus_RobotParameter")]
        public static extern void setTerminalRangeMinus_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetTerminalRangeMinus(Terminal terminal)
        {
            setTerminalRangeMinus_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalRangeMinus_RobotParameter")]
        public static extern void getTerminalRangeMinus_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalRangeMinus(ref Terminal terminal)
        {
            getTerminalRangeMinus_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalRangePlus_RobotParameter")]
        public static extern void setTerminalRangePlus_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetTerminalRangePlus(Terminal terminal)
        {
            setTerminalRangePlus_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalRangePlus_RobotParameter")]
        public static extern void getTerminalRangePlus_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalRangePlus(ref Terminal terminal)
        {
            getTerminalRangePlus_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxVelRange_RobotParameter")]
        public static extern void setTerminalMaxVelRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetTerminalMaxVelRange(Terminal terminal)
        {
            setTerminalMaxVelRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxVelRange_RobotParameter")]
        public static extern void getTerminalMaxVelRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxVelRange(ref Terminal terminal)
        {
            getTerminalMaxVelRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxAccRange_RobotParameter")]
        public static extern void setTerminalMaxAccRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetTerminalMaxAccRange(Terminal terminal)
        {
            setTerminalMaxAccRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxAccRange_RobotParameter")]
        public static extern void getTerminalMaxAccRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxAccRange(ref Terminal terminal)
        {
            getTerminalMaxAccRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxJerkRange_RobotParameter")]
        public static extern void setTerminalMaxJerkRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetTerminalMaxJerkRange(Terminal terminal)
        {
            setTerminalMaxJerkRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxJerkRange_RobotParameter")]
        public static extern void getTerminalMaxJerkRange_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxJerkRange(ref Terminal terminal)
        {
            getTerminalMaxJerkRange_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxVelRatio_RobotParameter")]
        public static extern void setTerminalMaxVelRatio_RobotParameter(IntPtr robotParameterPtr, double terminalMaxVelRatio);
        public void SetTerminalMaxVelRatio(double terminalMaxVelRatio)
        {
            setTerminalMaxVelRatio_RobotParameter(m_robotParameterPtr, terminalMaxVelRatio);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxVelRatio_RobotParameter")]
        public static extern double getTerminalMaxVelRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetTerminalMaxVelRatio()
        {
            return getTerminalMaxVelRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxAccRatio_RobotParameter")]
        public static extern void setTerminalMaxAccRatio_RobotParameter(IntPtr robotParameterPtr, double terminalMaxAccRatio);
        public void SetTerminalMaxAccRatio(double terminalMaxAccRatio)
        {
            setTerminalMaxAccRatio_RobotParameter(m_robotParameterPtr, terminalMaxAccRatio);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxAccRatio_RobotParameter")]
        public static extern double getTerminalMaxAccRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetTerminalMaxAccRatio()
        {
            return getTerminalMaxAccRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setTerminalMaxJerkRatio_RobotParameter")]
        public static extern void setTerminalMaxJerkRatio_RobotParameter(IntPtr robotParameterPtr, double terminalMaxJerkRatio);
        public void SetTerminalMaxJerkRatio(double terminalMaxJerkRatio)
        {
            setTerminalMaxJerkRatio_RobotParameter(m_robotParameterPtr, terminalMaxJerkRatio);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxJerkRatio_RobotParameter")]
        public static extern double getTerminalMaxJerkRatio_RobotParameter(IntPtr robotParameterPtr);
        public double GetTerminalMaxJerkRatio()
        {
            return getTerminalMaxJerkRatio_RobotParameter(m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxVel_RobotParameter")]
        public static extern void getTerminalMaxVel_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxVel(ref Terminal terminal)
        {
            getTerminalMaxVel_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxAcc_RobotParameter")]
        public static extern void getTerminalMaxAcc_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxAcc(ref Terminal terminal)
        {
            getTerminalMaxAcc_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getTerminalMaxJerk_RobotParameter")]
        public static extern void getTerminalMaxJerk_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetTerminalMaxJerk(ref Terminal terminal)
        {
            getTerminalMaxJerk_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setModelZeroOffset_RobotParameter")]
        public static extern void setModelZeroOffset_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetModelZeroOffset(Joints joints)
        {
            setModelZeroOffset_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getModelZeroOffset_RobotParameter")]
        public static extern void getModelZeroOffset_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetModelZeroOffset(ref Joints joints)
        {
            getModelZeroOffset_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setDHParameterAlpha_RobotParameter")]
        public static extern void setDHParameterAlpha_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetDHParameterAlpha(Joints joints)
        {
            setDHParameterAlpha_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getDHParameterAlpha_RobotParameter")]
        public static extern void getDHParameterAlpha_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetDHParameterAlpha(ref Joints joints)
        {
            getDHParameterAlpha_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setDHParameterA_RobotParameter")]
        public static extern void setDHParameterA_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetDHParameterA(Joints joints)
        {
            setDHParameterA_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getDHParameterA_RobotParameter")]
        public static extern void getDHParameterA_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetDHParameterA(ref Joints joints)
        {
            getDHParameterA_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setDHParameterD_RobotParameter")]
        public static extern void setDHParameterD_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetDHParameterD(Joints joints)
        {
            setDHParameterD_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getDHParameterD_RobotParameter")]
        public static extern void getDHParameterD_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetDHParameterD(ref Joints joints)
        {
            getDHParameterD_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setDHParameterTheta_RobotParameter")]
        public static extern void setDHParameterTheta_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void SetDHParameterTheta(Joints joints)
        {
            setDHParameterTheta_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getDHParameterTheta_RobotParameter")]
        public static extern void getDHParameterTheta_RobotParameter(IntPtr robotParameterPtr, IntPtr jointPtr);
        public void GetDHParameterTheta(ref Joints joints)
        {
            getDHParameterTheta_RobotParameter(m_robotParameterPtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setBaseFrame_RobotParameter")]
        public static extern void setBaseFrame_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetBaseFrame(Terminal terminal)
        {
            setBaseFrame_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getBaseFrame_RobotParameter")]
        public static extern void getBaseFrame_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetBaseFrame(ref Terminal terminal)
        {
            getBaseFrame_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setEndFrame_RobotParameter")]
        public static extern void setEndFrame_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void SetEndFrame(Terminal terminal)
        {
            setEndFrame_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "getEndFrame_RobotParameter")]
        public static extern void getEndFrame_RobotParameter(IntPtr robotParameterPtr, IntPtr terminalPtr);
        public void GetEndFrame(ref Terminal terminal)
        {
            getEndFrame_RobotParameter(m_robotParameterPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "print_RobotParameter")]
        public static extern void print_RobotParameter(IntPtr robotParameterPtr);
        public void Print()
        {
            print_RobotParameter(m_robotParameterPtr);
        }

        Joints[] NewJointsArray(int count) {
            Joints[] joints = new Joints[count];
            for (int i = 0; i < count; i++) {
                joints[i] = new Joints();
            }
            return joints;
        } 

        Terminal[] NewTerminalArray(int count) {
            Terminal[] terminals = new Terminal[count];
            for (int i = 0; i < count; i++) {
                terminals[i] = new Terminal();
            }
            return terminals;
        }

        public List<double[]> GetJointData() {
            List<double[]> lstRt = new List<double[]>();
            int i = 0;
            Joints[] joints = NewJointsArray(6);
            GetPosResolution(ref joints[i++]);
            GetVelResolution(ref joints[i++]);
            GetTorResolution(ref joints[i++]);
            GetReduceRatio(ref joints[i++]);
            GetPitch(ref joints[i++]);
            GetCouplingRatio(ref joints[i++]);
            for (int j=0;j<joints.Length;j++) {
                lstRt.Add(joints[j].ToArray());
            }
            return lstRt;
        }

        public List<double[]> GetJointMoveData()
        {
            List<double[]> lstRt = new List<double[]>();
            int i = 0;
            Joints[] joints = NewJointsArray(6);
            GetRateTorque(ref joints[i++]);
            GetJointRangeMinus(ref joints[i++]);
            GetJointRangePlus(ref joints[i++]);
            GetJointMaxVelRange(ref joints[i++]);
            GetJointMaxAccRange(ref joints[i++]);
            GetJointMaxJerkRange(ref joints[i++]);

            //GetJointMaxVelRange(ref joints[i++]);
            //GetJointMaxAccRange(ref joints[i++]);
            //GetJointMaxJerkRange(ref joints[i++]);
            for (int j = 0; j < joints.Length; j++)
            {
                lstRt.Add(joints[j].ToArray());
            }
            return lstRt;
        }

        public List<double[]> GetTerminalMoveData()
        {
            List<double[]> lstRt = new List<double[]>();
            int i = 0;
            Terminal[] joints = NewTerminalArray(5) ;
            GetTerminalRangeMinus(ref joints[i++]);
            GetTerminalRangePlus(ref joints[i++]);
            GetTerminalMaxVelRange(ref joints[i++]);
            GetTerminalMaxAccRange(ref joints[i++]);
            GetTerminalMaxJerkRange(ref joints[i++]);

            for (int j = 0; j < joints.Length; j++)
            {
                lstRt.Add(joints[j].ToArray());
            }
            return lstRt;
        }
        public List<double[]> GetDHData()
        {
            List<double[]> lstRt = new List<double[]>();
            int i = 0;
            Joints[] joints = NewJointsArray(4);
            GetDHParameterAlpha(ref joints[i++]);
            GetDHParameterA(ref joints[i++]);
            GetDHParameterD(ref joints[i++]);
            GetDHParameterTheta(ref joints[i++]);
            for (int j = 0; j < joints.Length; j++)
            {
                lstRt.Add(joints[j].ToArray());
            }
            return lstRt;
        }
    }
}
