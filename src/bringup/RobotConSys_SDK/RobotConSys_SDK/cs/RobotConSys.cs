using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace RobConSysCS
{
    public enum AUTHORITY_TYPE { AUTHORITY_NONE, AUTHORITY_OBSERVER, AUTHORITY_OPERATOR, AUTHORITY_ROOT };
    public enum PROGRAM_STATE { PROGRAM_RUN, PROGRAM_STOP, PROGRAM_PAUSE, PROGRAM_CONTINUE, PROGRAM_STEP };

    public struct ProgramPointer
    {
        public int nFunction;      //函数序号
        public int nSentence;      //语句序号
        
    };

    public struct ProgramInfo
    {
        public int program_errno;
        public string program_str;
    };

    public class RobotConSys
    {
        const string dllName = "RobotConSys_wrapper";
        private IntPtr m_sysPtr;

        [DllImport(dllName, EntryPoint = "create_RobotConSys_Client")]
        public static extern IntPtr create_RobotConSys_Client(string ip, int port, string configDir = ".");
        public bool Create_RobotConSys_Client(string ip, int port, string configDir)
        {
            m_sysPtr = create_RobotConSys_Client(ip, port, configDir);
            if (m_sysPtr == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [DllImport(dllName, EntryPoint = "destroy_RobotConSys_Client")]
        public static extern void destroy_RobotConSys_Client(IntPtr sys);
        public void Destroy_RobotConSys_Client()
        {
            destroy_RobotConSys_Client(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "create_RobotConSys_Server")]
        public static extern IntPtr create_RobotConSys_Server(int port, string configDir, int modbusPort);
        public bool Create_RobotConSys_Server(int port, string configDir, int modbusPort)
        {
            m_sysPtr = create_RobotConSys_Server(port, configDir, modbusPort);
            if (m_sysPtr == IntPtr.Zero)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        [DllImport(dllName, EntryPoint = "destroy_RobotConSys_Server")]
        public static extern void destroy_RobotConSys_Server(IntPtr sys);
        public void Destroy_RobotConSys_Server()
        {
            destroy_RobotConSys_Server(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "isEstablished_RobotConSys")]
        public static extern bool isEstablished_RobotConSys(IntPtr sys);
        public bool IsEstablished()
        {
            return isEstablished_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getAuthority_RobotConSys")]
        public static extern AUTHORITY_TYPE getAuthority_RobotConSys(IntPtr sys);
        public AUTHORITY_TYPE GetAuthority()
        {
            return getAuthority_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "setAuthority_RobotConSys")]
        public static extern void setAuthority_RobotConSys(IntPtr sys, AUTHORITY_TYPE authority);
        public void SetAuthority(AUTHORITY_TYPE authority)
        {
            setAuthority_RobotConSys(m_sysPtr, authority);
        }

        [DllImport(dllName, EntryPoint = "online_RobotConSys")]
        public static extern bool online_RobotConSys(IntPtr sys);
        public bool Online()
        {
            return online_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "writeRobotConSysPreference_RobotConSys")]
        public static extern void writeRobotConSysPreference_RobotConSys(IntPtr sys, IntPtr robotConSysPreferencePtr);
        public void WriteRobotConSysPreference(RobotConSysPreference robotConSysPreference)
        {
            writeRobotConSysPreference_RobotConSys(m_sysPtr, robotConSysPreference.m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotConSysPreference_RobotConSys")]
        public static extern int setRobotConSysPreference_RobotConSys(IntPtr sys, IntPtr robotConSysPreferencePtr);
        public int SetRobotConSysPreference(RobotConSysPreference robotConSysPreference)
        {
            return setRobotConSysPreference_RobotConSys(m_sysPtr, robotConSysPreference.m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "readRobotConSysPreference_RobotConSys")]
        public static extern void readRobotConSysPreference_RobotConSys(IntPtr sys, IntPtr robotConSysPreference);
        public void ReadRobotConSysPreference(ref RobotConSysPreference robotConSysPreference)
        {
            readRobotConSysPreference_RobotConSys(m_sysPtr, robotConSysPreference.m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotConSysPreference_RobotConSys")]
        public static extern void getRobotConSysPreference_RobotConSys(IntPtr sys, IntPtr robotConSysPreference);
        public void GetRobotConSysPreference(ref RobotConSysPreference robotConSysPreference)
        {
            getRobotConSysPreference_RobotConSys(m_sysPtr, robotConSysPreference.m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "writeRobotParameter_RobotConSys")]
        public static extern void writeRobotParameter_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotParameterPtr);
        public void WriteRobotParameter(ROBOTCONSYS_ARM_INDEX armIndex, RobotParameter robotParameter)
        {
            writeRobotParameter_RobotConSys(m_sysPtr, armIndex, robotParameter.m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotParameter_RobotConSys")]
        public static extern int setRobotParameter_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotParameterPtr);
        public int SetRobotParameter(ROBOTCONSYS_ARM_INDEX armIndex, RobotParameter robotParameter)
        {
            return setRobotParameter_RobotConSys(m_sysPtr, armIndex, robotParameter.m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "readRobotParameter_RobotConSys")]
        public static extern void readRobotParameter_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotParameterPtr);
        public void ReadRobotParameter(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotParameter robotParameter)
        {
            readRobotParameter_RobotConSys(m_sysPtr, armIndex, robotParameter.m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotParameter_RobotConSys")]
        public static extern void getRobotParameter_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotParameterPtr);
        public void GetRobotParameter(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotParameter robotParameter)
        {
            getRobotParameter_RobotConSys(m_sysPtr, armIndex, robotParameter.m_robotParameterPtr);
        }

        [DllImport(dllName, EntryPoint = "writeRobotPreference_RobotConSys")]
        public static extern void writeRobotPreference_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotPreferencePtr);
        public void WriteRobotPreference(ROBOTCONSYS_ARM_INDEX armIndex, RobotPreference robotPreference)
        {
            writeRobotPreference_RobotConSys(m_sysPtr, armIndex, robotPreference.m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotPreference_RobotConSys")]
        public static extern int setRobotPreference_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotPreferencePtr);
        public int SetRobotPreference(ROBOTCONSYS_ARM_INDEX armIndex, RobotPreference robotPreference)
        {
            return setRobotPreference_RobotConSys(m_sysPtr, armIndex, robotPreference.m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "readRobotPreference_RobotConSys")]
        public static extern void readRobotPreference_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotPreferencePtr);
        public void ReadRobotPreference(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotPreference robotPreference)
        {
            readRobotPreference_RobotConSys(m_sysPtr, armIndex, robotPreference.m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotPreference_RobotConSys")]
        public static extern void getRobotPreference_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotPreferencePtr);
        public void GetRobotPreference(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotPreference robotPreference)
        {
            getRobotPreference_RobotConSys(m_sysPtr, armIndex, robotPreference.m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "writeRobotFrame_RobotConSys")]
        public static extern void writeRobotFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotFramePtr);
        public void WriteRobotFrame(ROBOTCONSYS_ARM_INDEX armIndex, RobotFrame robotFrame)
        {
            writeRobotFrame_RobotConSys(m_sysPtr, armIndex, robotFrame.m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotFrame_RobotConSys")]
        public static extern void setRobotFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotFramePtr);
        public void SetRobotFrame(ROBOTCONSYS_ARM_INDEX armIndex, RobotFrame robotFrame)
        {
            setRobotFrame_RobotConSys(m_sysPtr, armIndex, robotFrame.m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "readRobotFrame_RobotConSys")]
        public static extern void readRobotFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotFramePtr);
        public void ReadRobotFrame(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotFrame robotFrame)
        {
            readRobotFrame_RobotConSys(m_sysPtr, armIndex, robotFrame.m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotFrame_RobotConSys")]
        public static extern void getRobotFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotFramePtr);
        public void GetRobotFrame(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotFrame robotFrame)
        {
            getRobotFrame_RobotConSys(m_sysPtr, armIndex, robotFrame.m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "modifyTool_RobotConSys")]
        public static extern int modifyTool_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, string name);
        public int ModifyTool(ROBOTCONSYS_ARM_INDEX armIndex, string name)
        {
            return modifyTool_RobotConSys(m_sysPtr, armIndex, name);
        }

        [DllImport(dllName, EntryPoint = "modifyToolFrame_RobotConSys")]
        public static extern int modifyToolFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminalPtr);
        public int ModifyToolFrame(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal)
        {
            return modifyToolFrame_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "modifyWork_RobotConSys")]
        public static extern int modifyWork_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, string name);
        public int ModifyWork(ROBOTCONSYS_ARM_INDEX armIndex, string name)
        {
            return modifyWork_RobotConSys(m_sysPtr, armIndex, name);
        }

        [DllImport(dllName, EntryPoint = "modifyWorkFrame_RobotConSys")]
        public static extern int modifyWorkFrame_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminalPtr);
        public int ModifyWorkFrame(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal)
        {
            return modifyWorkFrame_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "calibrateTCP_RobotConSys")]
        public static extern int calibrateTCP_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr jointsListPtr, IntPtr terminalPtr, ref double pre);
        public int CalibrateTCP(ROBOTCONSYS_ARM_INDEX armIndex, JointsList jointsList, ref Terminal terminal, ref double pre)
        {
            return calibrateTCP_RobotConSys(m_sysPtr, armIndex, jointsList.m_jointsListPtr, terminal.m_terminalPtr, ref pre);
        }

        [DllImport(dllName, EntryPoint = "calibrateTCFZ_RobotConSys")]
        public static extern int calibrateTCFZ_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr jointsListPtr, IntPtr joPtr, IntPtr jzPtr, IntPtr terminalPtr, ref double pre);
        public int CalibrateTCFZ(ROBOTCONSYS_ARM_INDEX armIndex, JointsList jointsList, Joints jo, Joints jz, ref Terminal terminal, ref double pre)
        {
            return calibrateTCFZ_RobotConSys(m_sysPtr, armIndex, jointsList.m_jointsListPtr, jo.m_jointsPtr, jz.m_jointsPtr, terminal.m_terminalPtr, ref pre);
        }

        [DllImport(dllName, EntryPoint = "calibrateTCFX_RobotConSys")]
        public static extern int calibrateTCFX_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr jointsListPtr, IntPtr joPtr, IntPtr jxPtr, IntPtr jzPtr, IntPtr terminalPtr, ref double pre);
        public int CalibrateTCFX(ROBOTCONSYS_ARM_INDEX armIndex, JointsList jointsList, Joints jo, Joints jx, Joints jz, ref Terminal terminal, ref double pre)
        {
            return calibrateTCFX_RobotConSys(m_sysPtr, armIndex, jointsList.m_jointsListPtr, jo.m_jointsPtr, jx.m_jointsPtr, jz.m_jointsPtr, terminal.m_terminalPtr, ref pre);
        }

        [DllImport(dllName, EntryPoint = "calibrateUSRF_RobotConSys")]
        public static extern int calibrateUSRF_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr poPtr, IntPtr pxPtr, IntPtr pyPtr, IntPtr terminalPtr);
        public int CalibrateUSRF(ROBOTCONSYS_ARM_INDEX armIndex, Point po, Point px, Point py, ref Terminal terminal)
        {
            return calibrateUSRF_RobotConSys(m_sysPtr, armIndex, po.m_pointPtr, px.m_pointPtr, py.m_pointPtr, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "searchZero_RobotConSys")]
        public static extern int searchZero_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, double vel);
        public int SearchZero(ROBOTCONSYS_ARM_INDEX armIndex, double vel)
        {
            return searchZero_RobotConSys(m_sysPtr, armIndex, vel);
        }

        [DllImport(dllName, EntryPoint = "returnZero_RobotConSys")]
        public static extern int returnZero_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, double vel);
        public int ReturnZero(ROBOTCONSYS_ARM_INDEX armIndex, double vel)
        {
            return returnZero_RobotConSys(m_sysPtr, armIndex, vel);
        }

        [DllImport(dllName, EntryPoint = "returnHome_RobotConSys")]
        public static extern int returnHome_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, double vel);
        public int ReturnHome(ROBOTCONSYS_ARM_INDEX armIndex, double vel)
        {
            return returnHome_RobotConSys(m_sysPtr, armIndex, vel);
        }

        [DllImport(dllName, EntryPoint = "jointJOG_RobotConSys")]
        public static extern int jointJOG_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index, MOVEDIRECTION dir, double vel);
        public int JointJOG(ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index, MOVEDIRECTION dir, double vel)
        {
            return jointJOG_RobotConSys(m_sysPtr, armIndex, index, dir, vel);
        }

        [DllImport(dllName, EntryPoint = "terminalJOG_RobotConSys")]
        public static extern int terminalJOG_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, TERMINALINDEX index, MOVEDIRECTION dir, double vel, COORDINATESYSTEM frame);
        public int TerminalJOG(ROBOTCONSYS_ARM_INDEX armIndex, TERMINALINDEX index, MOVEDIRECTION dir, double vel, COORDINATESYSTEM frame)
        {
            return terminalJOG_RobotConSys(m_sysPtr, armIndex, index, dir, vel, frame);
        }

        [DllImport(dllName, EntryPoint = "armAngleJOG_RobotConSys")]
        public static extern int armAngleJOG_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, MOVEDIRECTION dir, double vel);
        public int ArmAngleJOG(ROBOTCONSYS_ARM_INDEX armIndex, MOVEDIRECTION dir, double vel)
        {
            return armAngleJOG_RobotConSys(m_sysPtr, armIndex, dir, vel);
        }

        [DllImport(dllName, EntryPoint = "stopJOG_RobotConSys")]
        public static extern int stopJOG_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int StopJOG(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return stopJOG_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "startJointsJOGVel_RobotConSys")]
        public static extern int startJointsJOGVel_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int StartJointsJOGVel(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return startJointsJOGVel_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "startTerminalJOGVel_RobotConSys")]
        public static extern int startTerminalJOGVel_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, COORDINATESYSTEM frame);
        public int StartTerminalJOGVel(ROBOTCONSYS_ARM_INDEX armIndex, COORDINATESYSTEM frame)
        {
            return startTerminalJOGVel_RobotConSys(m_sysPtr, armIndex, frame);
        }

        [DllImport(dllName, EntryPoint = "jointJOGVel_RobotConSys")]
        public static extern int jointJOGVel_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr joints);
        public int JointJOGVel(ROBOTCONSYS_ARM_INDEX armIndex, Joints joints)
        {
            return jointJOGVel_RobotConSys(m_sysPtr, armIndex, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "terminalJOGVel_RobotConSys")]
        public static extern int terminalJOGVel_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal);
        public int TerminalJOGVel(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal)
        {
            return terminalJOGVel_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "stopJOGVel_RobotConSys")]
        public static extern int stopJOGVel_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int StopJOGVel(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return stopJOGVel_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "jointStep_RobotConSys")]
        public static extern int jointStep_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index, MOVEDIRECTION dir, double step, double vel);
        public int JointStep(ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index, MOVEDIRECTION dir, double step, double vel)
        {
            return jointStep_RobotConSys(m_sysPtr, armIndex, index, dir, step, vel);
        }

        [DllImport(dllName, EntryPoint = "terminalStep_RobotConSys")]
        public static extern int terminalStep_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, TERMINALINDEX index, MOVEDIRECTION dir, double step, double vel,  COORDINATESYSTEM frame);
        public int TerminalStep(ROBOTCONSYS_ARM_INDEX armIndex, TERMINALINDEX index, MOVEDIRECTION dir, double step,double vel,  COORDINATESYSTEM frame)
        {
            return terminalStep_RobotConSys(m_sysPtr, armIndex, index, dir, step,vel,  frame);
        }

        [DllImport(dllName, EntryPoint = "armAngleStep_RobotConSys")]
        public static extern int armAngleStep_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, MOVEDIRECTION dir, double step, double vel);
        public int ArmAngleStep(ROBOTCONSYS_ARM_INDEX armIndex, MOVEDIRECTION dir, double step, double vel)
        {
            return armAngleStep_RobotConSys(m_sysPtr, armIndex, dir, step, vel);
        }

        [DllImport(dllName, EntryPoint = "moveABSJoint_RobotConSys")]
        public static extern int moveABSJoint_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr joints, double vel);
        public int MoveABSJoint(ROBOTCONSYS_ARM_INDEX armIndex, JointsList joints, double vel)
        {
            return moveABSJoint_RobotConSys(m_sysPtr, armIndex, joints.m_jointsListPtr, vel);
        }

        [DllImport(dllName, EntryPoint = "moveABSJointR_RobotConSys")]
        public static extern int moveABSJointR_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr joints, double vel);
        public int MoveABSJointR(ROBOTCONSYS_ARM_INDEX armIndex, JointsList joints, double vel)
        {
            return moveABSJointR_RobotConSys(m_sysPtr, armIndex, joints.m_jointsListPtr, vel);
        }

        [DllImport(dllName, EntryPoint = "moveJoint_RobotConSys")]
        public static extern int moveJoint_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, COORDINATESYSTEM frame);
        public int MoveJoint(ROBOTCONSYS_ARM_INDEX armIndex, TerminalList terminal, double vel, COORDINATESYSTEM frame)
        {
            return moveJoint_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalListPtr, vel, frame);
        }

        [DllImport(dllName, EntryPoint = "moveJointR_RobotConSys")]
        public static extern int moveJointR_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, COORDINATESYSTEM frame);
        public int MoveJointR(ROBOTCONSYS_ARM_INDEX armIndex, TerminalList terminal, double vel, COORDINATESYSTEM frame)
        {
            return moveJointR_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalListPtr, vel, frame);
        }

        [DllImport(dllName, EntryPoint = "moveLine_RobotConSys")]
        public static extern int moveLine_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveLine(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveLine_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveLineR_RobotConSys")]
        public static extern int moveLineR_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveLineR(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveLineR_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircle_RobotConSys")]
        public static extern int moveCircle_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, CIRCLETYPE cir,IntPtr t1, IntPtr t2, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCircle(ROBOTCONSYS_ARM_INDEX armIndex, CIRCLETYPE cir, Terminal t1, Terminal t2, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCircle_RobotConSys(m_sysPtr, armIndex, cir, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleR_RobotConSys")]
        public static extern int moveCircleR_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr t1, IntPtr t2, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCircleR(ROBOTCONSYS_ARM_INDEX armIndex, Terminal t1, Terminal t2, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCircleR_RobotConSys(m_sysPtr, armIndex, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleSeg_RobotConSys")]
        public static extern int moveCircleSeg_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr t1, IntPtr t2, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCircleSeg(ROBOTCONSYS_ARM_INDEX armIndex, Terminal t1, Terminal t2, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCircleSeg_RobotConSys(m_sysPtr, armIndex, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleRSeg_RobotConSys")]
        public static extern int moveCircleRSeg_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr t1, IntPtr t2, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCircleRSeg(ROBOTCONSYS_ARM_INDEX armIndex, Terminal t1, Terminal t2, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCircleRSeg_RobotConSys(m_sysPtr, armIndex, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleSegRun_RobotConSys")]
        public static extern int moveCircleSegRun_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int seg_index);
        public int MoveCircleSegRun(ROBOTCONSYS_ARM_INDEX armIndex, int seg_index)
        {
            return moveCircleSegRun_RobotConSys(m_sysPtr, armIndex, seg_index);
        }

        [DllImport(dllName, EntryPoint = "moveArmAngle_RobotConSys")]
        public static extern int moveArmAngle_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, double theta, double vel, COORDINATESYSTEM frame);
        public int MoveArmAngle(ROBOTCONSYS_ARM_INDEX armIndex, double theta, double vel, COORDINATESYSTEM frame)
        {
            return moveArmAngle_RobotConSys(m_sysPtr, armIndex, theta, vel, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCurve_RobotConSys")]
        public static extern int moveCurve_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, CURVETYPE curve, IntPtr terminalList, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCurve(ROBOTCONSYS_ARM_INDEX armIndex, CURVETYPE curve, TerminalList terminalList,double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCurve_RobotConSys(m_sysPtr, armIndex, curve, terminalList.m_terminalListPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCurveSeg_RobotConSys")]
        public static extern int moveCurveSeg_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, CURVETYPE curve, IntPtr terminalList, double vel, double acc, double jerk, COORDINATESYSTEM frame);
        public int MoveCurveSeg(ROBOTCONSYS_ARM_INDEX armIndex, CURVETYPE curve, TerminalList terminalList, double vel, double acc, double jerk, COORDINATESYSTEM frame)
        {
            return moveCurveSeg_RobotConSys(m_sysPtr, armIndex, curve, terminalList.m_terminalListPtr, vel, acc, jerk, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCurveSegRun_RobotConSys")]
        public static extern int moveCurveSegRun_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int seg_index);
        public int MoveCurveSegRun(ROBOTCONSYS_ARM_INDEX armIndex, int seg_index)
        {
            return moveCurveSegRun_RobotConSys(m_sysPtr, armIndex, seg_index);
        }

        [DllImport(dllName, EntryPoint = "moveJointSequence_RobotConSys")]
        public static extern int moveJointSequence_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr jointsListPtr);
        public int MoveJointSequence(ROBOTCONSYS_ARM_INDEX armIndex, JointsList jointsList)
        {
            return moveJointSequence_RobotConSys(m_sysPtr, armIndex, jointsList.m_jointsListPtr);
        }

        [DllImport(dllName, EntryPoint = "moveStartCon_RobotConSys")]
        public static extern int moveStartCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int delay);
        public int MoveStartCon(ROBOTCONSYS_ARM_INDEX armIndex, int delay)
        {
            return moveStartCon_RobotConSys(m_sysPtr, armIndex, delay);
        }

        [DllImport(dllName, EntryPoint = "moveEndCon_RobotConSys")]
        public static extern int moveEndCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int MoveEndCon(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return moveEndCon_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "moveLineCon_RobotConSys")]
        public static extern int moveLineCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame);
        public int MoveLineCon(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame)
        {
            return moveLineCon_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr, vel, acc, jerk, turn, frame);
        }

        [DllImport(dllName, EntryPoint = "moveLineRCon_RobotConSys")]
        public static extern int moveLineRCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr terminal, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame);
        public int MoveLineRCon(ROBOTCONSYS_ARM_INDEX armIndex, Terminal terminal, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame)
        {
            return moveLineRCon_RobotConSys(m_sysPtr, armIndex, terminal.m_terminalPtr, vel, acc, jerk, turn, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleCon_RobotConSys")]
        public static extern int moveCircleCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr t1, IntPtr t2, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame);
        public int MoveCircleCon(ROBOTCONSYS_ARM_INDEX armIndex, Terminal t1, Terminal t2, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame)
        {
            return moveCircleCon_RobotConSys(m_sysPtr, armIndex, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, turn, frame);
        }

        [DllImport(dllName, EntryPoint = "moveCircleRCon_RobotConSys")]
        public static extern int moveCircleRCon_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr t1, IntPtr t2, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame);
        public int MoveCircleRCon(ROBOTCONSYS_ARM_INDEX armIndex, Terminal t1, Terminal t2, double vel, double acc, double jerk, double turn, COORDINATESYSTEM frame)
        {
            return moveCircleRCon_RobotConSys(m_sysPtr, armIndex, t1.m_terminalPtr, t2.m_terminalPtr, vel, acc, jerk, turn, frame);
        }

        [DllImport(dllName, EntryPoint = "getMotionCMDState_RobotConSys")]
        public static extern int getMotionCMDState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int GetMotionCMDState(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return getMotionCMDState_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "waitMotionCMDFinish_RobotConSys")]
        public static extern int waitMotionCMDFinish_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int WaitMotionCMDFinish(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return waitMotionCMDFinish_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "setJointZero_RobotConSys")]
        public static extern int setJointZero_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index);
        public int SetJointZero(ROBOTCONSYS_ARM_INDEX armIndex, JOINTINDEX index)
        {
            return setJointZero_RobotConSys(m_sysPtr, armIndex, index);
        }

        [DllImport(dllName, EntryPoint = "setErrorClear_RobotConSys")]
        public static extern int setErrorClear_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public int SetErrorClear(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return setErrorClear_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "setErrorClearAll_RobotConSys")]
        public static extern int setErrorClearAll_RobotConSys(IntPtr sys);
        public int SetErrorClear()
        {
            return setErrorClearAll_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "updateRobotMotion_RobotConSys")]
        public static extern void updateRobotMotion_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateRobotMotion(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateRobotMotion_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getRobotMotion_RobotConSys")]
        public static extern void getRobotMotion_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotMotionPtr);
        public void GetRobotMotion(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotMotion robotMotion)
        {
            getRobotMotion_RobotConSys(m_sysPtr, armIndex, robotMotion.m_robotMotionPtr);
        }

        [DllImport(dllName, EntryPoint = "updateArmAngle_RobotConSys")]
        public static extern void updateArmAngle_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateArmAngle(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateArmAngle_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getArmAngle_RobotConSys")]
        public static extern double getArmAngle_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public double GetArmAngle(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return getArmAngle_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "updateMotorServoState_RobotConSys")]
        public static extern void updateMotorServoState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateMotorServoState(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateMotorServoState_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getMotorServoState_RobotConSys")]
        public static extern SWITCHSTATE getMotorServoState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int index);
        public SWITCHSTATE GetMotorServoState(ROBOTCONSYS_ARM_INDEX armIndex, int index)
        {
            return getMotorServoState_RobotConSys(m_sysPtr, armIndex, index);
        }

        [DllImport(dllName, EntryPoint = "updateMotorMode_RobotConSys")]
        public static extern void updateMotorMode_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateMotorMode(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateMotorMode_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getMotorControlMode_RobotConSys")]
        public static extern ROBOTDEV_MOTOR_CONTROL_LOOP getMotorControlMode_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int index);
        public ROBOTDEV_MOTOR_CONTROL_LOOP GetMotorControlMode(ROBOTCONSYS_ARM_INDEX armIndex, int index)
        {
            return getMotorControlMode_RobotConSys(m_sysPtr, armIndex, index);
        }

        [DllImport(dllName, EntryPoint = "getMotorInputMode_RobotConSys")]
        public static extern ROBOTDEV_MOTOR_INPUT_MODE getMotorInputMode_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, int index);
        public ROBOTDEV_MOTOR_INPUT_MODE GetMotorInputMode(ROBOTCONSYS_ARM_INDEX armIndex, int index)
        {
            return getMotorInputMode_RobotConSys(m_sysPtr, armIndex, index);
        }

        [DllImport(dllName, EntryPoint = "setClawState_RobotConSys")]
        public static extern void setClawState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, SWITCHSTATE state);
        public void SetClawState(ROBOTCONSYS_ARM_INDEX armIndex, SWITCHSTATE state)
        {
            setClawState_RobotConSys(m_sysPtr, armIndex, state);
        }

        [DllImport(dllName, EntryPoint = "updateClawState_RobotConSys")]
        public static extern void updateClawState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateClawState(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateClawState_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getClawState_RobotConSys")]
        public static extern SWITCHSTATE getClawState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public SWITCHSTATE GetClawState(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            return getClawState_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "setServoState_RobotConSys")]
        public static extern int setServoState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, SWITCHSTATE state);
        public int SetServoState(ROBOTCONSYS_ARM_INDEX armIndex, SWITCHSTATE state)
        {
            return setServoState_RobotConSys(m_sysPtr, armIndex, state);
        }

        [DllImport(dllName, EntryPoint = "setServoStateAll_RobotConSys")]
        public static extern int setServoStateAll_RobotConSys(IntPtr sys, SWITCHSTATE state);
        public int SetServoState(SWITCHSTATE state)
        {
            return setServoStateAll_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "updateRobotState_RobotConSys")]
        public static extern void updateRobotState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex);
        public void UpdateRobotState(ROBOTCONSYS_ARM_INDEX armIndex)
        {
            updateRobotState_RobotConSys(m_sysPtr, armIndex);
        }

        [DllImport(dllName, EntryPoint = "getRobotState_RobotConSys")]
        public static extern void getRobotState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, IntPtr robotStatePtr);
        public void GetRobotState(ROBOTCONSYS_ARM_INDEX armIndex, ref RobotState robotState)
        {
            getRobotState_RobotConSys(m_sysPtr, armIndex, robotState.m_robotStatePtr);
        }

        [DllImport(dllName, EntryPoint = "setRunState_RobotConSys")]
        public static extern int setRunState_RobotConSys(IntPtr sys, ROBOTCONSYS_ARM_INDEX armIndex, SYSRUNSTATE state);
        public int SetRunState(ROBOTCONSYS_ARM_INDEX armIndex, SYSRUNSTATE state)
        {
            return setRunState_RobotConSys(m_sysPtr, armIndex, state);
        }

        [DllImport(dllName, EntryPoint = "setRunStateAll_RobotConSys")]
        public static extern int setRunStateAll_RobotConSys(IntPtr sys, SYSRUNSTATE state);
        public int SetRunState(SYSRUNSTATE state)
        {
            return setRunStateAll_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "setProgramRunState_RobotConSys")]
        public static extern int setProgramRunState_RobotConSys(IntPtr sys, SYSRUNSTATE state);
        public int SetProgramRunState(SYSRUNSTATE state)
        {
            return setProgramRunState_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "setPlayState_RobotConSys")]
        public static extern int setPlayState_RobotConSys(IntPtr sys, SYSPLAYSTATE state);
        public int SetPlayState(SYSPLAYSTATE state)
        {
            return setPlayState_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "setEStopState_RobotConSys")]
        public static extern void setEStopState_RobotConSys(IntPtr sys, SWITCHSTATE state);
        public void SetEStopState(SWITCHSTATE state)
        {
            setEStopState_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "updateRobotConSysState_RobotConSys")]
        public static extern void updateRobotConSysState_RobotConSys(IntPtr sys);
        public void UpdateRobotConSysState()
        {
            updateRobotConSysState_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotConSysState_RobotConSys")]
        public static extern void getRobotConSysState_RobotConSys(IntPtr sys, IntPtr robotConSysStatePtr);
        public void GetRobotConSysState(ref RobotConSysState robotConSysState)
        {
            getRobotConSysState_RobotConSys(m_sysPtr, robotConSysState.m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleConfig_RobotConSys")]
        public static extern void setVehicleConfig_RobotConSys(IntPtr sys, ref Vehicle_Config_Data config);
        public void SetVehicleConfig(ref Vehicle_Config_Data config)
        {
            setVehicleConfig_RobotConSys(m_sysPtr, ref config);
        }

        [DllImport(dllName, EntryPoint = "getVehicleConfig_RobotConSys")]
        public static extern Vehicle_Config_Data getVehicleConfig_RobotConSys(IntPtr sys);
        public Vehicle_Config_Data GetVehicleConfig()
        {
            return getVehicleConfig_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "updateVehicleState_RobotConSys")]
        public static extern void updateVehicleState_RobotConSys(IntPtr sys);
        public void UpdateVehicleState()
        {
            updateVehicleState_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getVehicleState_RobotConSys")]
        public static extern Vehicle_State_Data getVehicleState_RobotConSys(IntPtr sys);
        public Vehicle_State_Data GetVehicleState()
        {
            return getVehicleState_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "updateVehicleIMUData_RobotConSys")]
        public static extern void updateVehicleIMUData_RobotConSys(IntPtr sys);
        public void UpdateVehicleIMUData()
        {
            updateVehicleIMUData_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getVehicleIMUData_RobotConSys")]
        public static extern Vehicle_IMU_Data getVehicleIMUData_RobotConSys(IntPtr sys);
        public Vehicle_IMU_Data GetVehicleIMUData()
        {
            return getVehicleIMUData_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleControlType_RobotConSys")]
        public static extern void setVehicleControlType_RobotConSys(IntPtr sys, VEHICLE_CONTROL_TYPE controlType);
        public void SetVehicleControlType(VEHICLE_CONTROL_TYPE controlType)
        {
            setVehicleControlType_RobotConSys(m_sysPtr, controlType);
        }

        [DllImport(dllName, EntryPoint = "setAutoCharge_RobotConSys")]
        public static extern void setAutoCharge_RobotConSys(IntPtr sys, SWITCHSTATE autoCharge);
        public void SetAutoCharge(SWITCHSTATE autoCharge)
        {
            setAutoCharge_RobotConSys(m_sysPtr, autoCharge);
        }

        [DllImport(dllName, EntryPoint = "setVehicleServoState_RobotConSys")]
        public static extern void setVehicleServoState_RobotConSys(IntPtr sys, SWITCHSTATE state);
        public void SetVehicleServoState(SWITCHSTATE state)
        {
            setVehicleServoState_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "setMotionControlRocker_RobotConSys")]
        public static extern void setMotionControlRocker_RobotConSys(IntPtr sys, VEHICLE_MODE mode, float x_ratio, float y_ratio, float z_ratio);
        public void SetMotionControlRocker(VEHICLE_MODE mode, float x_ratio, float y_ratio, float z_ratio)
        {
            setMotionControlRocker_RobotConSys(m_sysPtr, mode, x_ratio, y_ratio, z_ratio);
        }

        [DllImport(dllName, EntryPoint = "setMotionControl_RobotConSys")]
        public static extern void setMotionControl_RobotConSys(IntPtr sys, VEHICLE_MODE mode, float x_vel, float y_vel, float z_vel);
        public void SetMotionControl(VEHICLE_MODE mode, float x_vel, float y_vel, float z_vel)
        {
            setMotionControl_RobotConSys(m_sysPtr, mode, x_vel, y_vel, z_vel);
        }

        [DllImport(dllName, EntryPoint = "setDefaultParameter_RobotConSys")]
        public static extern void setDefaultParameter_RobotConSys(IntPtr sys);
        public void SetDefaultParameter()
        {
            setDefaultParameter_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "readFlashParameter_RobotConSys")]
        public static extern void readFlashParameter_RobotConSys(IntPtr sys);
        public void ReadFlashParameter()
        {
            readFlashParameter_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "saveParameter_RobotConSys")]
        public static extern void saveParameter_RobotConSys(IntPtr sys);
        public void SaveParameter()
        {
            saveParameter_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "saveServoMotorOffset_RobotConSys")]
        public static extern void saveServoMotorOffset_RobotConSys(IntPtr sys, VEHICLE_SERVO_MOTOR_OFFSET index);
        public void SaveServoMotorOffset(VEHICLE_SERVO_MOTOR_OFFSET index)
        {
            saveServoMotorOffset_RobotConSys(m_sysPtr, index);
        }

        [DllImport(dllName, EntryPoint = "setServoMotorOffsetAutoCalibrate_RobotConSys")]
        public static extern void setServoMotorOffsetAutoCalibrate_RobotConSys(IntPtr sys, SWITCHSTATE state);
        public void SetServoMotorOffsetAutoCalibrate(SWITCHSTATE state)
        {
            setServoMotorOffsetAutoCalibrate_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "setMotionDigitalInputSingle_RobotConSys")]
        public static extern void setMotionDigitalInputSingle_RobotConSys(IntPtr sys, PORTINDEX index, SWITCHSTATE state);
        public void SetMotionDigitalInputSingle(PORTINDEX index, SWITCHSTATE state)
        {
            setMotionDigitalInputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setMotionAnalogInputSingle_RobotConSys")]
        public static extern void setMotionAnalogInputSingle_RobotConSys(IntPtr sys, PORTINDEX index, double state);
        public void SetMotionAnalogInputSingle(PORTINDEX index, double state)
        {
            setMotionAnalogInputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setMotionDigitalOutputSingle_RobotConSys")]
        public static extern void setMotionDigitalOutputSingle_RobotConSys(IntPtr sys, PORTINDEX index, SWITCHSTATE state);
        public void SetMotionDigitalOutputSingle(PORTINDEX index, SWITCHSTATE state)
        {
            setMotionDigitalOutputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setMotionAnalogOutputSingle_RobotConSys")]
        public static extern void setMotionAnalogOutputSingle_RobotConSys(IntPtr sys, PORTINDEX index, double state);
        public void SetMotionAnalogOutputSingle(PORTINDEX index, double state)
        {
            setMotionAnalogOutputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "updateMotionIO_RobotConSys")]
        public static extern void updateMotionIO_RobotConSys(IntPtr sys);
        public void UpdateMotionIO()
        {
            updateMotionIO_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getMotionIO_RobotConSys")]
        public static extern void getMotionIO_RobotConSys(IntPtr sys, IntPtr robotIOPtr);
        public void GetMotionIO(ref RobotIO robotIO)
        {
            getMotionIO_RobotConSys(m_sysPtr, robotIO.m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "setDigitalInputSingle_RobotConSys")]
        public static extern void setDigitalInputSingle_RobotConSys(IntPtr sys, PORTINDEX index, SWITCHSTATE state);
        public void SetDigitalInputSingle(PORTINDEX index, SWITCHSTATE state)
        {
            setDigitalInputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setAnalogInputSingle_RobotConSys")]
        public static extern void setAnalogInputSingle_RobotConSys(IntPtr sys, PORTINDEX index, double state);
        public void SetAnalogInputSingle(PORTINDEX index, double state)
        {
            setAnalogInputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setDigitalOutputSingle_RobotConSys")]
        public static extern void setDigitalOutputSingle_RobotConSys(IntPtr sys, PORTINDEX index, SWITCHSTATE state);
        public void SetDigitalOutputSingle(PORTINDEX index, SWITCHSTATE state)
        {
            setDigitalOutputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "setAnalogOutputSingle_RobotConSys")]
        public static extern void setAnalogOutputSingle_RobotConSys(IntPtr sys, PORTINDEX index, double state);
        public void SetAnalogOutputSingle(PORTINDEX index, double state)
        {
            setAnalogOutputSingle_RobotConSys(m_sysPtr, index, state);
        }

        [DllImport(dllName, EntryPoint = "updateRobotIO_RobotConSys")]
        public static extern void updateRobotIO_RobotConSys(IntPtr sys);
        public void UpdateRobotIO()
        {
            updateRobotIO_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getRobotIO_RobotConSys")]
        public static extern void getRobotIO_RobotConSys(IntPtr sys, IntPtr robotIOPtr);
        public void GetRobotIO(ref RobotIO robotIO)
        {
            getRobotIO_RobotConSys(m_sysPtr, robotIO.m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "setModbusData_RobotConSys")]
        public static extern void setModbusData_RobotConSys(IntPtr sys, MODBUS_DATA_TYPE type, ushort addr, ushort value);
        public void SetModbusData(MODBUS_DATA_TYPE type, ushort addr, ushort value)
        {
            setModbusData_RobotConSys(m_sysPtr, type, addr, value);
        }

        [DllImport(dllName, EntryPoint = "updateModbusData_RobotConSys")]
        public static extern void updateModbusData_RobotConSys(IntPtr sys);
        public void UpdateModbusData()
        {
            updateModbusData_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "getModbusData_RobotConSys")]
        public static extern void getModbusData_RobotConSys(IntPtr sys, IntPtr modbusDataPtr);
        public void GetModbusData(ref ModbusData modbusData)
        {
            getModbusData_RobotConSys(m_sysPtr, modbusData.m_modbusDataPtr);
        }

        [DllImport(dllName, EntryPoint = "getModbusDataSingle_RobotConSys")]
        public static extern ushort getModbusDataSingle_RobotConSys(IntPtr sys, MODBUS_DATA_TYPE type, ushort addr);
        public ushort GetModbusDataSingle(MODBUS_DATA_TYPE type, ushort addr)
        {
            return getModbusDataSingle_RobotConSys(m_sysPtr, type, addr);
        }

        [DllImport(dllName, EntryPoint = "getPeripheralDevData_Joints_RobotConSys")]
        public static extern void getPeripheralDevData_Joints_RobotConSys(IntPtr sys, ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, IntPtr jointsPtr);
        public void GetPeripheralDevData_Joints(ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, ref Joints joints)
        {
            getPeripheralDevData_Joints_RobotConSys(m_sysPtr, index, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getPeripheralDevData_Terminal_RobotConSys")]
        public static extern void getPeripheralDevData_Terminal_RobotConSys(IntPtr sys, ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, IntPtr terminalPtr);
        public void GetPeripheralDevData_Terminal(ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, ref Terminal terminal)
        {
            getPeripheralDevData_Terminal_RobotConSys(m_sysPtr, index, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "setPeripheralDevData_Joints_RobotConSys")]
        public static extern void setPeripheralDevData_Joints_RobotConSys(IntPtr sys, ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, IntPtr jointsPtr);
        public void SetPeripheralDevData_Joints(ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, Joints joints)
        {
            setPeripheralDevData_Joints_RobotConSys(m_sysPtr, index, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setPeripheralDevData_Terminal_RobotConSys")]
        public static extern void setPeripheralDevData_Terminal_RobotConSys(IntPtr sys, ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, IntPtr terminalPtr);
        public void SetPeripheralDevData_Terminal(ROBOTCONSYS_PERIPHERAL_DATA_INDEX index, Terminal terminal)
        {
            setPeripheralDevData_Terminal_RobotConSys(m_sysPtr, index, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "programLoad_RobotConSys")]
        public static extern void programLoad_RobotConSys(IntPtr sys, string filePath);
        public void ProgramLoad(string filePath)
        {
            programLoad_RobotConSys(m_sysPtr, filePath);
        }

        [DllImport(dllName, EntryPoint = "programSave_RobotConSys")]
        public static extern void programSave_RobotConSys(IntPtr sys, string filePath);
        public void ProgramSave(string filePath)
        {
            programSave_RobotConSys(m_sysPtr, filePath);
        }

        [DllImport(dllName, EntryPoint = "programSetVel_RobotConSys")]
        public static extern void programSetVel_RobotConSys(IntPtr sys, double vel);
        public void ProgramSetVel(double vel)
        {
            programSetVel_RobotConSys(m_sysPtr, vel);
        }

        [DllImport(dllName, EntryPoint = "programSetDebugMode_RobotConSys")]
        public static extern void programSetDebugMode_RobotConSys(IntPtr sys, bool mode);
        public void ProgramSetDebugMode(bool mode)
        {
            programSetDebugMode_RobotConSys(m_sysPtr, mode);
        }

        [DllImport(dllName, EntryPoint = "programSetState_RobotConSys")]
        public static extern void programSetState_RobotConSys(IntPtr sys, PROGRAM_STATE state);
        public void ProgramSetState(PROGRAM_STATE state)
        {
            programSetState_RobotConSys(m_sysPtr, state);
        }

        [DllImport(dllName, EntryPoint = "programSetPointer_RobotConSys")]
        public static extern void programSetPointer_RobotConSys(IntPtr sys, ProgramPointer pointer);
        public void ProgramSetPointer(ProgramPointer pointer)
        {
            programSetPointer_RobotConSys(m_sysPtr, pointer);
        }

        [DllImport(dllName, EntryPoint = "programGetPointer_RobotConSysTest")]
        public static extern void programGetPointer_RobotConSysTest(IntPtr sys);
        public void ProgramGetPointerTest()
        {
            programGetPointer_RobotConSysTest(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "programGetPointer_RobotConSys")]
        public static extern ProgramPointer programGetPointer_RobotConSys(IntPtr sys);
        public ProgramPointer ProgramGetPointer()
        {
            return programGetPointer_RobotConSys(m_sysPtr);
        }

        [DllImport(dllName, EntryPoint = "programGetStopState_RobotConSys")]
        public static extern int programGetStopState_RobotConSys(IntPtr sys, ref int program_errno, IntPtr strPtr);
        public ProgramInfo ProgramGetStopState()
        {
            ProgramInfo info = new ProgramInfo();
            int err = new int();
            String_cpp str = new String_cpp();
            str.m_stringLen = programGetStopState_RobotConSys(m_sysPtr, ref err, str.m_stringPtr);
            info.program_errno = err;
            info.program_str = str.GetStringInCS();
            return info;
        }


        public delegate string StringDelegate(string message);

        [DllImport(dllName, EntryPoint = "setLogFunction", CallingConvention = CallingConvention.StdCall)]
        public static extern void setLogFunction(StringDelegate stringDelegate);

        [DllImport(dllName, EntryPoint = "setVehicleLogFunction", CallingConvention = CallingConvention.StdCall)]
        public static extern void setVehicleLogFunction(StringDelegate stringDelegate);

        [AOT.MonoPInvokeCallback(typeof(StringDelegate))]
        static string CLibLog(string cString)
        {
            Log.Info(cString);
            return cString;
        }
        [AOT.MonoPInvokeCallback(typeof(StringDelegate))]
        static string CLibVLog(string cString)
        {
            Log.Info(cString);
            return cString;
        }
        public static void SetUploadLogInvoke() {
            setLogFunction(CLibLog);
            setVehicleLogFunction(CLibVLog);
        }


        [DllImport(dllName, EntryPoint = "test")]
        public static extern string test();
        public string Test()
        {
            return test();
        }
    }
}
