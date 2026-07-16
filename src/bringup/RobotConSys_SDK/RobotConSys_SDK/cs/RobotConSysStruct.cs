using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RobConSysCS
{
    public class String_cpp
    {
        const string dllName = "RobotConSys_wrapper";
        public  IntPtr m_stringPtr;
        public int m_stringLen;

        private bool disposed = false;
        // 实现 IDisposable 接口
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // 释放托管资源
                }

                // 释放非托管资源
                if (m_stringPtr != IntPtr.Zero)
                {
                    destroy_String(m_stringPtr);
                    m_stringPtr = IntPtr.Zero;
                }
                    

                disposed = true;
            }
        }

        [DllImport(dllName, EntryPoint = "create_String")]
        public static extern void create_String( out IntPtr intPtr);
        public String_cpp()
        {
            //m_stringPtr = create_String();
            //create_String( out m_stringPtr);
            unsafe
            {
                create_String(out m_stringPtr);
            }
        }

        [DllImport(dllName, EntryPoint = "destroy_String")]
        public static extern void destroy_String(IntPtr stringPtr);
        ~String_cpp()
        {
            //destroy_String(m_stringPtr);
            Dispose();
        }

        [DllImport(dllName, EntryPoint = "test_String")]
        public static extern int test_String(IntPtr stringPtr);
        public void Test()
        {
            m_stringLen = test_String(m_stringPtr);
        }

        public string GetStringInCS()
        {
            // 解析返回的字符串内容
            IntPtr innerPtr = Marshal.ReadIntPtr(m_stringPtr); // 获取指向字符串的一级指针
            string managedString = Marshal.PtrToStringAnsi(innerPtr, m_stringLen);
            //string str = Marshal.PtrToStringAnsi(m_stringPtr, m_stringLen);
            //Dispose();
            return managedString;
        }
    }

    //public class String_cpp
    //{
    //    const string dllName = "RobotConSys_wrapper";
    //    public readonly IntPtr m_stringPtr;
    //    public int m_stringLen;
    //    private bool disposed = false;

    //    // 实现 IDisposable 接口
    //    public void Dispose()
    //    {
    //        Dispose(true);
    //        GC.SuppressFinalize(this);
    //    }

    //    protected virtual void Dispose(bool disposing)
    //    {
    //        if (!disposed)
    //        {
    //            if (disposing)
    //            {
    //                // 释放托管资源
    //            }

    //            // 释放非托管资源

    //            disposed = true;
    //        }
    //    }

    //    [DllImport(dllName, EntryPoint = "create_String")]
    //    public static extern IntPtr create_String();
    //    public String_cpp()
    //    {
    //        m_stringPtr = create_String();
    //    }

    //    [DllImport(dllName, EntryPoint = "destroy_String")]
    //    public static extern void destroy_String(IntPtr stringPtr);
    //    ~String_cpp()
    //    {
    //        destroy_String(m_stringPtr);
    //    }

    //    [DllImport(dllName, EntryPoint = "test_String")]
    //    public static extern int test_String(IntPtr stringPtr);
    //    public void Test()
    //    {
    //        m_stringLen = test_String(m_stringPtr);
    //    }

    //    public string GetStringInCS()
    //    {
    //        string str = Marshal.PtrToStringAnsi(m_stringPtr, m_stringLen);
    //        return str;
    //    }
    //}

    public enum ROBOTDEV_MOTOR_MASTER_INDEX { ROBOTDEV_MOTOR_MASTER_1, ROBOTDEV_MOTOR_MASTER_2, ROBOTDEV_MOTOR_MASTER_3, ROBOTDEV_MOTOR_MASTER_4 };
    public enum ROBOTDEV_MOTOR_CHANNEL_INDEX { ROBOTDEV_MOTOR_CHANNEL_1, ROBOTDEV_MOTOR_CHANNEL_2, ROBOTDEV_MOTOR_CHANNEL_3, ROBOTDEV_MOTOR_CHANNEL_4 };
    public enum ROBOTDEV_MOTOR_CONTROL_LOOP { ROBOTDEV_MOTOR_CONTROL_LOOP_UNKNOWN, ROBOTDEV_MOTOR_CONTROL_LOOP_POSTION, ROBOTDEV_MOTOR_CONTROL_LOOP_VELOCITY, ROBOTDEV_MOTOR_CONTROL_LOOP_TORQUE };
    public enum ROBOTDEV_MOTOR_INPUT_MODE { ROBOTDEV_MOTOR_INPUT_MODE_UNKNOWN, ROBOTDEV_MOTOR_PERIODIC_MODE, ROBOTDEV_MOTOR_CONTOUR_MODE };
    public enum ROBOTDEV_CLAW_TYPE
    {
        ROBOTDEV_CLAW_SIM_TRC12 = 0x00,
        ROBOTDEV_CLAW_SIM_RH56 = 0x01,
        ROBOTDEV_CLAW_CAN_TRC12 = 0x10,
        ROBOTDEV_CLAW_RS485_RH56 = 0x20,
    };

    public enum ROBOTCONSYS_ARM_INDEX { ROBOTCONSYS_ARM_1 = 0, ROBOTCONSYS_ARM_2, ROBOTCONSYS_ARM_3, ROBOTCONSYS_ARM_4 };
    public enum ROBOTCONSYS_VEHICLE_INDEX { ROBOTCONSYS_VEHICLE_1 = 0, ROBOTCONSYS_VEHICLE_2, ROBOTCONSYS_VEHICLE_3, ROBOTCONSYS_VEHICLE_4 };
    public enum ROBOTCONSYS_PERIPHERAL_DATA_INDEX
    {
        ROBOTCONSYS_PERIPHERAL_DATA_1,
        ROBOTCONSYS_PERIPHERAL_DATA_2,
        ROBOTCONSYS_PERIPHERAL_DATA_3,
        ROBOTCONSYS_PERIPHERAL_DATA_4,
        ROBOTCONSYS_PERIPHERAL_DATA_5,
        ROBOTCONSYS_PERIPHERAL_DATA_6,
        ROBOTCONSYS_PERIPHERAL_DATA_7,
        ROBOTCONSYS_PERIPHERAL_DATA_8,
        ROBOTCONSYS_PERIPHERAL_DATA_9,
        ROBOTCONSYS_PERIPHERAL_DATA_10
    };

    public enum SWITCHSTATE { SWITCHOFF, SWITCHON };
    public enum PORTINDEX
    {
        PORT_1, PORT_2, PORT_3, PORT_4, PORT_5, PORT_6, PORT_7, PORT_8,
        PORT_9, PORT_10, PORT_11, PORT_12, PORT_13, PORT_14, PORT_15, PORT_16
    };

    public class RobotIO
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotIOPtr;

        [DllImport(dllName, EntryPoint = "createRobotIO")]
        public static extern IntPtr createRobotIO();
        public RobotIO()
        {
            m_robotIOPtr = createRobotIO();
        }

        [DllImport(dllName, EntryPoint = "freeRobotIO")]
        public static extern void freeRobotIO(IntPtr robotIOPtr);
        ~RobotIO()
        {
            freeRobotIO(m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotIO")]
        public static extern void copy_RobotIO(IntPtr robotIOPtr, IntPtr srcRobotIOPtr);
        public void Copy(RobotIO robotIO)
        {
            copy_RobotIO(m_robotIOPtr, robotIO.m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "getIOConnectState_RobotIO")]
        public static extern SWITCHSTATE getIOConnectState_RobotIO(IntPtr robotIOPtr);
        public SWITCHSTATE GetIOConnectState()
        {
            return getIOConnectState_RobotIO(m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "getDigitalInputNum_RobotIO")]
        public static extern int getDigitalInputNum_RobotIO(IntPtr robotIOPtr);
        public int GetDigitalInputNum()
        {
            return getDigitalInputNum_RobotIO(m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "getDigitalOutputNum_RobotIO")]
        public static extern int getDigitalOutputNum_RobotIO(IntPtr robotIOPtr);
        public int GetDigitalOutputNum()
        {
            return getDigitalOutputNum_RobotIO(m_robotIOPtr);
        }


        [DllImport(dllName, EntryPoint = "getAnalogInputNum_RobotIO")]
        public static extern int getAnalogInputNum_RobotIO(IntPtr robotIOPtr);
        public int GetAnalogInputNum()
        {
            return getAnalogInputNum_RobotIO(m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "getAnalogOutputNum_RobotIO")]
        public static extern int getAnalogOutputNum_RobotIO(IntPtr robotIOPtr);
        public int GetAnalogOutputNum()
        {
            return getAnalogOutputNum_RobotIO(m_robotIOPtr);
        }

        [DllImport(dllName, EntryPoint = "getDigitalInputStateSingle_RobotIO")]
        public static extern SWITCHSTATE getDigitalInputStateSingle_RobotIO(IntPtr robotIOPtr, PORTINDEX index);
        public SWITCHSTATE GetDigitalInputStateSingle(PORTINDEX index)
        {
            return getDigitalInputStateSingle_RobotIO(m_robotIOPtr, index);
        }

        [DllImport(dllName, EntryPoint = "getDigitalOutputStateSingle_RobotIO")]
        public static extern SWITCHSTATE getDigitalOutputStateSingle_RobotIO(IntPtr robotIOPtr, PORTINDEX index);
        public SWITCHSTATE GetDigitalOutputStateSingle(PORTINDEX index)
        {
            return getDigitalOutputStateSingle_RobotIO(m_robotIOPtr, index);
        }

        [DllImport(dllName, EntryPoint = "getAnalogInputStateSingle_RobotIO")]
        public static extern double getAnalogInputStateSingle_RobotIO(IntPtr robotIOPtr, PORTINDEX index);
        public double GetAnalogInputStateSingle(PORTINDEX index)
        {
            return getAnalogInputStateSingle_RobotIO(m_robotIOPtr, index);
        }

        [DllImport(dllName, EntryPoint = "getAnalogOutputStateSingle_RobotIO")]
        public static extern double getAnalogOutputStateSingle_RobotIO(IntPtr robotIOPtr, PORTINDEX index);
        public double GetAnalogOutputStateSingle(PORTINDEX index)
        {
            return getAnalogOutputStateSingle_RobotIO(m_robotIOPtr, index);
        }

        [DllImport(dllName, EntryPoint = "setDigitalOutputSingle_RobotConSys")]
        public static extern int setDigitalOutputSingle_RobotConSys(IntPtr robotIOPtr, PORTINDEX index,SWITCHSTATE state);
        public void SetDigitalOutputStateSingle_RobotIO(PORTINDEX index, SWITCHSTATE state)
        {
            setDigitalOutputSingle_RobotConSys(m_robotIOPtr, index,state);
        }


        [DllImport(dllName, EntryPoint = "setAnalogOutputSingle_RobotConSys")]
        public static extern void setAnalogOutputSingle_RobotConSys(IntPtr robotIOPtr, PORTINDEX index, double state);
        public void SetAnalogOutputStateSingle(PORTINDEX index, double state)
        {
            setAnalogOutputSingle_RobotConSys(m_robotIOPtr, index, state);
        }

        public int[] GetDigitalInputState() {
            int len = GetDigitalInputNum();
            int[] rt = new int[len];
            for (int i = 0; i < len; i++) {
                rt[i] = GetDigitalInputStateSingle((PORTINDEX)i) == SWITCHSTATE.SWITCHON ? 1 : 0;
            }
            return rt;
        }

        public int[] GetDigitalOutputState()
        {
            int len = GetDigitalOutputNum();
            int[] rt = new int[len];
            for (int i = 0; i < len; i++)
            {
                rt[i] = GetDigitalOutputStateSingle((PORTINDEX)i) == SWITCHSTATE.SWITCHON ? 1 : 0;
            }
            return rt;
        }

        public double[] GetAnalogInputState()
        {
            int len = GetDigitalInputNum();
            double[] rt = new double[len];
            for (int i = 0; i < len; i++)
            {
                rt[i] = GetAnalogInputStateSingle((PORTINDEX)i);
            }
            return rt;
        }

        public double[] GetAnalogOutputState()
        {
            int len = GetDigitalOutputNum();
            double[] rt = new double[len];
            for (int i = 0; i < len; i++)
            {
                rt[i] = GetAnalogOutputStateSingle((PORTINDEX)i);
            }
            return rt;
        }

    }

    public enum MODBUS_DATA_TYPE
    {
        MODBUS_DATA_BIT,
        MODBUS_DATA_INPUT_BIT,
        MODBUS_DATA_REG,
        MODBUS_DATA_INPUT_REG,
    };

    public class ModbusData
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_modbusDataPtr;

        [DllImport(dllName, EntryPoint = "createModbusData")]
        public static extern IntPtr createModbusData();
        public ModbusData()
        {
            m_modbusDataPtr = createModbusData();
        }

        [DllImport(dllName, EntryPoint = "freeModbusData")]
        public static extern void freeModbusData(IntPtr modbusDataPtr);
        ~ModbusData()
        {
            freeModbusData(m_modbusDataPtr);
        }

        [DllImport(dllName, EntryPoint = "copy_ModbusData")]
        public static extern void copy_ModbusData(IntPtr modbusDataPtr, IntPtr srcModbusDataPtrPtr);
        public void Copy(ModbusData modbusData)
        {
            copy_ModbusData(m_modbusDataPtr, modbusData.m_modbusDataPtr);
        }

        [DllImport(dllName, EntryPoint = "getDataLength_ModbusData")]
        public static extern int getDataLength_ModbusData(IntPtr modbusDataPtr, MODBUS_DATA_TYPE type);
        public int GetDataLength(MODBUS_DATA_TYPE type)
        {
            return getDataLength_ModbusData(m_modbusDataPtr, type);
        }

        [DllImport(dllName, EntryPoint = "setDataValue_ModbusData")]
        public static extern void setDataValue_ModbusData(IntPtr modbusDataPtr, MODBUS_DATA_TYPE type, ushort addr, ushort value);
        public void SetDataValue(MODBUS_DATA_TYPE type, ushort addr, ushort value)
        {
            setDataValue_ModbusData(m_modbusDataPtr, type, addr, value);
        }

        [DllImport(dllName, EntryPoint = "getDataValue_ModbusData")]
        public static extern ushort getDataValue_ModbusData(IntPtr modbusDataPtr, MODBUS_DATA_TYPE type, ushort addr);
        public ushort GetDataValue(MODBUS_DATA_TYPE type, ushort addr)
        {
            return getDataValue_ModbusData(m_modbusDataPtr, type, addr);
        }

        public ushort[] GetDataValue(MODBUS_DATA_TYPE type)
        {
            int len = GetDataLength(type);
            ushort[] rt = new ushort[len];
            for (int i = 0; i < len; i++) {
                rt[i] = getDataValue_ModbusData(m_modbusDataPtr, type, (ushort)i);
            }
            return rt;
        }

    }

    public enum SYSRUNSTATE { SYSRUN_RUN, SYSRUN_PAUSE, SYSRUN_STOP };
    public enum SYSPLAYSTATE { SYSPLAY_TEACH, SYSPLAY_PLAY, SYSPLAY_REMOTE };
    public enum SYSREMOTESTATE { SYSREMOTE_NONE = -1, SYSREMOTE_RUN, SYSREMOTE_STOP, SYSREMOTE_HOME };

    public class RobotState
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotStatePtr;

        [DllImport(dllName, EntryPoint = "createRobotState")]
        public static extern IntPtr createRobotState();
        public RobotState()
        {
            m_robotStatePtr = createRobotState();
        }

        [DllImport(dllName, EntryPoint = "freeRobotState")]
        public static extern void freeRobotState(IntPtr robotStatePtr);
        ~RobotState()
        {
            freeRobotState(m_robotStatePtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotState")]
        public static extern void copy_RobotState(IntPtr robotStatePtr, IntPtr srcRobotStatePtr);
        public void Copy(RobotState robotState)
        {
            copy_RobotState(m_robotStatePtr, robotState.m_robotStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getServoState_RobotState")]
        public static extern SWITCHSTATE getServoState_RobotState(IntPtr robotStatePtr);
        public SWITCHSTATE GetServoState()
        {
            return getServoState_RobotState(m_robotStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getErrorState_RobotState")]
        public static extern SWITCHSTATE getErrorState_RobotState(IntPtr robotStatePtr);
        public SWITCHSTATE GetErrorState()
        {
            return getErrorState_RobotState(m_robotStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getErrorCode_RobotState")]
        public static extern ulong getErrorCode_RobotState(IntPtr robotStatePtr, int index);
        public ulong GetErrorCode(int index)
        {
            return getErrorCode_RobotState(m_robotStatePtr, index);
        }

        [DllImport(dllName, EntryPoint = "getMotionFinishErrorCode_RobotState")]
        public static extern int getMotionFinishErrorCode_RobotState(IntPtr robotStatePtr);
        public int GetMotionFinishErrorCode()
        {
            return getMotionFinishErrorCode_RobotState(m_robotStatePtr);
        }
    }

    public class RobotConSysState
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotConSysStatePtr;

        [DllImport(dllName, EntryPoint = "createRobotConSysState")]
        public static extern IntPtr createRobotConSysState();
        public RobotConSysState()
        {
            m_robotConSysStatePtr = createRobotConSysState();
        }

        [DllImport(dllName, EntryPoint = "freeRobotConSysState")]
        public static extern void freeRobotConSysState(IntPtr robotConSysStatePtr);
        ~RobotConSysState()
        {
            freeRobotConSysState(m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotConSysState")]
        public static extern void copy_RobotConSysState(IntPtr robotConSysStatePtr, IntPtr srcRobotConSysStatePtr);
        public void Copy(RobotConSysState robotConSysState)
        {
            copy_RobotConSysState(m_robotConSysStatePtr, robotConSysState.m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getSystemState_RobotConSysState")]
        public static extern SWITCHSTATE getSystemState_RobotConSysState(IntPtr robotConSysStatePtr);
        public SWITCHSTATE GetSystemState()
        {
            return getSystemState_RobotConSysState(m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getPlayState_RobotConSysState")]
        public static extern SYSPLAYSTATE getPlayState_RobotConSysState(IntPtr robotConSysStatePtr);
        public SYSPLAYSTATE GetPlayState()
        {
            return getPlayState_RobotConSysState(m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getEstopState_RobotConSysState")]
        public static extern SWITCHSTATE getEstopState_RobotConSysState(IntPtr robotConSysStatePtr);
        public SWITCHSTATE GetEstopState()
        {
            return getEstopState_RobotConSysState(m_robotConSysStatePtr);
        }

        [DllImport(dllName, EntryPoint = "getPorgramFinishErrorCode_RobotConSysState")]
        public static extern int getPorgramFinishErrorCode_RobotConSysState(IntPtr robotConSysStatePtr);
        public int GetPorgramFinishErrorCode()
        {
            return getPorgramFinishErrorCode_RobotConSysState(m_robotConSysStatePtr);
        }
    }

    public class RobotFrame
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotFramePtr;

        [DllImport(dllName, EntryPoint = "createRobotFrame")]
        public static extern IntPtr createRobotFrame();
        public RobotFrame()
        {
            m_robotFramePtr = createRobotFrame();
        }

        [DllImport(dllName, EntryPoint = "freeRobotFrame")]
        public static extern void freeRobotFrame(IntPtr robotFramePtr);
        ~RobotFrame()
        {
            freeRobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotFrame")]
        public static extern void copy_RobotFrame(IntPtr robotFramePtr, IntPtr srcRobotFramePtr);
        public void Copy(RobotFrame robotFrame)
        {
            copy_RobotFrame(m_robotFramePtr, robotFrame.m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "getDefaultName_RobotFrame")]
        public static extern int getDefaultName_RobotFrame(IntPtr robotFramePtr, IntPtr stringPtr);
        public string GetDefaultName()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getDefaultName_RobotFrame(m_robotFramePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "getTempName_RobotFrame")]
        public static extern int getTempName_RobotFrame(IntPtr robotFramePtr, IntPtr stringPtr);
        public string GetTempName()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getTempName_RobotFrame(m_robotFramePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "getToolListNum_RobotFrame")]
        public static extern int getToolListNum_RobotFrame(IntPtr robotFramePtr);
        public int GetToolListNum()
        {
            return getToolListNum_RobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "getToolList_RobotFrame")]
        public static extern int getToolList_RobotFrame(IntPtr robotFramePtr, int index, IntPtr name, IntPtr toolPtr);
        public void GetToolList(int index, ref string name, ref Tool tool)
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getToolList_RobotFrame(m_robotFramePtr, index, str.m_stringPtr, tool.m_toolPtr);
            name = str.GetStringInCS();
        }

        public Dictionary<string, double[]> GetToolFrameDict()
        {
            Dictionary<string, double[]> dict = new Dictionary<string, double[]>();
            int len = GetToolListNum();
            for (int i = 0; i < len; i++)
            {
                string strName = string.Empty;
                Terminal terminal = new Terminal();
                Tool tool = new Tool();
                GetToolList(i, ref strName, ref tool);
                tool.GetFrame(ref terminal);
                dict.Add(strName, terminal.ToArray());
            }
            return dict;
        }

        [DllImport(dllName, EntryPoint = "getCurrentTool_RobotFrame")]
        public static extern int getCurrentTool_RobotFrame(IntPtr robotFramePtr, IntPtr stringPtr);
        public string GetCurrentTool()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getCurrentTool_RobotFrame(m_robotFramePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "getTool_RobotFrame")]
        public static extern void getTool_RobotFrame(IntPtr robotFramePtr, string name, IntPtr toolPtr);
        public void GetTool(string name, ref Tool tool)
        {
            getTool_RobotFrame(m_robotFramePtr, name, tool.m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "addTool_RobotFrame")]
        public static extern int addTool_RobotFrame(IntPtr robotFramePtr, string name, IntPtr toolPtr);
        public int AddTool(string name, Tool tool)
        {
            return addTool_RobotFrame(m_robotFramePtr, name, tool.m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "modifyTool_RobotFrame")]
        public static extern int modifyTool_RobotFrame(IntPtr robotFramePtr, string name, IntPtr toolPtr);
        public int ModifyTool(string name, Tool tool)
        {
            return modifyTool_RobotFrame(m_robotFramePtr, name, tool.m_toolPtr);
        }

        [DllImport(dllName, EntryPoint = "deleteTool_RobotFrame")]
        public static extern int deleteTool_RobotFrame(IntPtr robotFramePtr, string name);
        public int DeleteTool(string name)
        {
            return deleteTool_RobotFrame(m_robotFramePtr, name);
        }

        [DllImport(dllName, EntryPoint = "clearTool_RobotFrame")]
        public static extern void clearTool_RobotFrame(IntPtr robotFramePtr);
        public void ClearTool()
        {
            clearTool_RobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentTool_RobotFrame")]
        public static extern int setCurrentTool_RobotFrame(IntPtr robotFramePtr, string name);
        public int SetCurrentTool(string name)
        {
            return setCurrentTool_RobotFrame(m_robotFramePtr, name);
        }

        [DllImport(dllName, EntryPoint = "setDefaultTool_RobotFrame")]
        public static extern void setDefaultTool_RobotFrame(IntPtr robotFramePtr);
        public void SetDefaultTool()
        {
            setDefaultTool_RobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "getWorkFrameListNum_RobotFrame")]
        public static extern int getWorkFrameListNum_RobotFrame(IntPtr robotFramePtr);
        public int GetWorkFrameListNum()
        {
            return getWorkFrameListNum_RobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "getWorkFrameList_RobotFrame")]
        public static extern int getWorkFrameList_RobotFrame(IntPtr robotFramePtr, int index, IntPtr strName, IntPtr terminalPtr);
        public void GetWorkFrameList(int index, ref string name, ref Terminal terminal)
        {
            String_cpp string_Cpp = new String_cpp();
            string_Cpp.m_stringLen = getWorkFrameList_RobotFrame(m_robotFramePtr, index, string_Cpp.m_stringPtr, terminal.m_terminalPtr);
            name = string_Cpp.GetStringInCS();
        }
        public Dictionary<string, double[]> GetWorkFrameDict() {
            Dictionary<string, double[]> dict = new Dictionary<string, double[]>();
            int len = GetWorkFrameListNum();
            for (int i = 0; i < len; i++)
            {
                string strName = string.Empty;
                Terminal terminal = new Terminal();
                GetWorkFrameList(i, ref strName, ref terminal);
                dict.Add(strName, terminal.ToArray());
            }
            return dict;
        }

        [DllImport(dllName, EntryPoint = "getCurrentWorkFrame_RobotFrame")]
        public static extern int getCurrentWorkFrame_RobotFrame(IntPtr robotFramePtr, IntPtr stringPtr);
        public string GetCurrentWorkFrame()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getCurrentWorkFrame_RobotFrame(m_robotFramePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "getWorkFrame_RobotFrame")]
        public static extern void getWorkFrame_RobotFrame(IntPtr robotFramePtr, string name, IntPtr terminalPtr);
        public void GetWorkFrame(string name, ref Terminal terminal)
        {
            getWorkFrame_RobotFrame(m_robotFramePtr, name, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "addWorkFrame_RobotFrame")]
        public static extern int addWorkFrame_RobotFrame(IntPtr robotFramePtr, string name, IntPtr terminalPtr);
        public int AddWorkFrame(string name, Terminal terminal)
        {
            return addWorkFrame_RobotFrame(m_robotFramePtr, name, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "modifyWorkFrame_RobotFrame")]
        public static extern int modifyWorkFrame_RobotFrame(IntPtr robotFramePtr, string name, IntPtr terminalPtr);
        public int ModifyWorkFrame(string name, Terminal terminal)
        {
            return modifyWorkFrame_RobotFrame(m_robotFramePtr, name, terminal.m_terminalPtr);
        }

        [DllImport(dllName, EntryPoint = "deleteWorkFrame_RobotFrame")]
        public static extern int deleteWorkFrame_RobotFrame(IntPtr robotFramePtr, string name);
        public int DeleteWorkFrame(string name)
        {
            return deleteWorkFrame_RobotFrame(m_robotFramePtr, name);
        }

        [DllImport(dllName, EntryPoint = "clearWorkFrame_RobotFrame")]
        public static extern void clearWorkFrame_RobotFrame(IntPtr robotFramePtr);
        public void ClearWorkFrame()
        {
            clearWorkFrame_RobotFrame(m_robotFramePtr);
        }

        [DllImport(dllName, EntryPoint = "setCurrentWorkFrame_RobotFrame")]
        public static extern int setCurrentWorkFrame_RobotFrame(IntPtr robotFramePtr, string name);
        public int SetCurrentWorkFrame(string name)
        {
            return setCurrentWorkFrame_RobotFrame(m_robotFramePtr, name);
        }

        [DllImport(dllName, EntryPoint = "setDefaultWorkFrame_RobotFrame")]
        public static extern void setDefaultWorkFrame_RobotFrame(IntPtr robotFramePtr);
        public void SetDefaultWorkFrame()
        {
            setDefaultWorkFrame_RobotFrame(m_robotFramePtr);
        }
    }

    public class RobotPreference
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotPreferencePtr;

        [DllImport(dllName, EntryPoint = "createRobotPreference")]
        public static extern IntPtr createRobotPreference();
        public RobotPreference()
        {
            m_robotPreferencePtr = createRobotPreference();
        }

        [DllImport(dllName, EntryPoint = "freeRobotPreference")]
        public static extern void freeRobotPreference(IntPtr robotPreferencePtr);
        ~RobotPreference()
        {
            freeRobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotPreference")]
        public static extern void copy_RobotPreference(IntPtr robotPreferencePtr, IntPtr srcRobotPreferencePtr);
        public void Copy(RobotPreference robotPreference)
        {
            copy_RobotPreference(m_robotPreferencePtr, robotPreference.m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "getJointInitPosition_RobotPreference")]
        public static extern void getJointInitPosition_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void GetJointInitPosition(ref Joints joints)
        {
            getJointInitPosition_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointInitPosition_RobotPreference")]
        public static extern void setJointInitPosition_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void SetJointInitPosition(Joints joints)
        {
            setJointInitPosition_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointCompensation_RobotPreference")]
        public static extern void getJointCompensation_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void GetJointCompensation(ref Joints joints)
        {
            getJointCompensation_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointCompensation_RobotPreference")]
        public static extern void setJointCompensation_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void SetJointCompensation(Joints joints)
        {
            setJointCompensation_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "getJointSearchSequence_RobotPreference")]
        public static extern void getJointSearchSequence_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void GetJointSearchSequence(ref Joints joints)
        {
            getJointSearchSequence_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setJointSearchSequence_RobotPreference")]
        public static extern void setJointSearchSequence_RobotPreference(IntPtr robotPreferencePtr, IntPtr jointPtr);
        public void SetJointSearchSequence(Joints joints)
        {
            setJointSearchSequence_RobotPreference(m_robotPreferencePtr, joints.m_jointsPtr);
        }

        [DllImport(dllName, EntryPoint = "setChannelIndex_RobotPreference")]
        public static extern void setChannelIndex_RobotPreference(IntPtr robotPreferencePtr, ROBOTDEV_MOTOR_CHANNEL_INDEX index);
        public void SetChannelIndex(ROBOTDEV_MOTOR_CHANNEL_INDEX index)
        {
            setChannelIndex_RobotPreference(m_robotPreferencePtr, index);
        }

        [DllImport(dllName, EntryPoint = "getChannelIndex_RobotPreference")]
        public static extern ROBOTDEV_MOTOR_CHANNEL_INDEX getChannelIndex_RobotPreference(IntPtr robotPreferencePtr);
        public ROBOTDEV_MOTOR_CHANNEL_INDEX GetChannelIndex()
        {
            return getChannelIndex_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMasterIndex_RobotPreference")]
        public static extern void setMasterIndex_RobotPreference(IntPtr robotPreferencePtr, ROBOTDEV_MOTOR_MASTER_INDEX index);
        public void SetMasterIndex(ROBOTDEV_MOTOR_MASTER_INDEX index)
        {
            setMasterIndex_RobotPreference(m_robotPreferencePtr, index);
        }

        [DllImport(dllName, EntryPoint = "getMasterIndex_RobotPreference")]
        public static extern ROBOTDEV_MOTOR_MASTER_INDEX getMasterIndex_RobotPreference(IntPtr robotPreferencePtr);
        public ROBOTDEV_MOTOR_MASTER_INDEX GetMasterIndex()
        {
            return getMasterIndex_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMasterIDStart_RobotPreference")]
        public static extern void setMasterIDStart_RobotPreference(IntPtr robotPreferencePtr, byte id);
        public void SetMasterIDStart(byte id)
        {
            setMasterIDStart_RobotPreference(m_robotPreferencePtr, id);
        }

        [DllImport(dllName, EntryPoint = "getMasterIDStart_RobotPreference")]
        public static extern byte getMasterIDStart_RobotPreference(IntPtr robotPreferencePtr);
        public byte GetMasterIDStart()
        {
            return getMasterIDStart_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawEnableState_RobotPreference")]
        public static extern void setClawEnableState_RobotPreference(IntPtr robotPreferencePtr, SWITCHSTATE state);
        public void SetClawEnableState(SWITCHSTATE state)
        {
            setClawEnableState_RobotPreference(m_robotPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getClawEnableState_RobotPreference")]
        public static extern byte getClawEnableState_RobotPreference(IntPtr robotPreferencePtr);
        public byte GetClawEnableState()
        {
            return getClawEnableState_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawType_RobotPreference")]
        public static extern void setClawType_RobotPreference(IntPtr robotPreferencePtr, ROBOTDEV_CLAW_TYPE type);
        public void SetClawType(ROBOTDEV_CLAW_TYPE type)
        {
            setClawType_RobotPreference(m_robotPreferencePtr, type);
        }

        [DllImport(dllName, EntryPoint = "getClawType_RobotPreference")]
        public static extern ROBOTDEV_CLAW_TYPE getClawType_RobotPreference(IntPtr robotPreferencePtr);
        public ROBOTDEV_CLAW_TYPE GetClawType()
        {
            return getClawType_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawMasterIndex_RobotPreference")]
        public static extern void setClawMasterIndex_RobotPreference(IntPtr robotPreferencePtr, ROBOTDEV_MOTOR_MASTER_INDEX index);
        public void SetClawMasterIndex(ROBOTDEV_MOTOR_MASTER_INDEX index)
        {
            setClawMasterIndex_RobotPreference(m_robotPreferencePtr, index);
        }

        [DllImport(dllName, EntryPoint = "getClawMasterIndex_RobotPreference")]
        public static extern ROBOTDEV_MOTOR_MASTER_INDEX getClawMasterIndex_RobotPreference(IntPtr robotPreferencePtr);
        public ROBOTDEV_MOTOR_MASTER_INDEX GetClawMasterIndex()
        {
            return getClawMasterIndex_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawID_RobotPreference")]
        public static extern void setClawID_RobotPreference(IntPtr robotPreferencePtr, byte id);
        public void SetClawID(byte id)
        {
            setClawID_RobotPreference(m_robotPreferencePtr, id);
        }

        [DllImport(dllName, EntryPoint = "getClawID_RobotPreference")]
        public static extern byte getClawID_RobotPreference(IntPtr robotPreferencePtr);
        public byte GetClawID()
        {
            return getClawID_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawVelOutput_RobotPreference")]
        public static extern void setClawVelOutput_RobotPreference(IntPtr robotPreferencePtr, double clawVelOutput);
        public void SetClawVelOutput(double clawVelOutput)
        {
            setClawVelOutput_RobotPreference(m_robotPreferencePtr, clawVelOutput);
        }

        [DllImport(dllName, EntryPoint = "getClawVelOutput_RobotPreference")]
        public static extern double getClawVelOutput_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawVelOutput()
        {
            return getClawVelOutput_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawVelRamp_RobotPreference")]
        public static extern void setClawVelRamp_RobotPreference(IntPtr robotPreferencePtr, double clawVelRamp);
        public void SetClawVelRamp(double clawVelRamp)
        {
            setClawVelRamp_RobotPreference(m_robotPreferencePtr, clawVelRamp);
        }

        [DllImport(dllName, EntryPoint = "getClawVelRamp_RobotPreference")]
        public static extern double getClawVelRamp_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawVelRamp()
        {
            return getClawVelRamp_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawTorLimit_RobotPreference")]
        public static extern void setClawTorLimit_RobotPreference(IntPtr robotPreferencePtr, double clawTorLimit);
        public void SetClawTorLimit(double clawTorLimit)
        {
            setClawTorLimit_RobotPreference(m_robotPreferencePtr, clawTorLimit);
        }

        [DllImport(dllName, EntryPoint = "getClawTorLimit_RobotPreference")]
        public static extern double getClawTorLimit_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawTorLimit()
        {
            return getClawTorLimit_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawPosResolution_RobotPreference")]
        public static extern void setClawPosResolution_RobotPreference(IntPtr robotPreferencePtr, double resolution);
        public void SetClawPosResolution(double resolution)
        {
            setClawPosResolution_RobotPreference(m_robotPreferencePtr, resolution);
        }

        [DllImport(dllName, EntryPoint = "getClawPosResolution_RobotPreference")]
        public static extern double getClawPosResolution_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawPosResolution()
        {
            return getClawPosResolution_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawVelResolution_RobotPreference")]
        public static extern void setClawVelResolution_RobotPreference(IntPtr robotPreferencePtr, double resolution);
        public void SetClawVelResolution(double resolution)
        {
            setClawVelResolution_RobotPreference(m_robotPreferencePtr, resolution);
        }

        [DllImport(dllName, EntryPoint = "getClawVelResolution_RobotPreference")]
        public static extern double getClawVelResolution_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawVelResolution()
        {
            return getClawVelResolution_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawTorResolution_RobotPreference")]
        public static extern void setClawTorResolution_RobotPreference(IntPtr robotPreferencePtr, double resolution);
        public void SetClawTorResolution(double resolution)
        {
            setClawTorResolution_RobotPreference(m_robotPreferencePtr, resolution);
        }

        [DllImport(dllName, EntryPoint = "getClawTorResolution_RobotPreference")]
        public static extern double getClawTorResolution_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawTorResolution()
        {
            return getClawTorResolution_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawReduceRatio_RobotPreference")]
        public static extern void setClawReduceRatio_RobotPreference(IntPtr robotPreferencePtr, double reduceRatio);
        public void SetClawReduceRatio(double reduceRatio)
        {
            setClawReduceRatio_RobotPreference(m_robotPreferencePtr, reduceRatio);
        }

        [DllImport(dllName, EntryPoint = "getClawReduceRatio_RobotPreference")]
        public static extern double getClawReduceRatio_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawReduceRatio()
        {
            return getClawReduceRatio_RobotPreference(m_robotPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setClawPitch_RobotPreference")]
        public static extern void setClawPitch_RobotPreference(IntPtr robotPreferencePtr, double pitch);
        public void SetClawPitch(double pitch)
        {
            setClawPitch_RobotPreference(m_robotPreferencePtr, pitch);
        }

        [DllImport(dllName, EntryPoint = "getClawPitch_RobotPreference")]
        public static extern double getClawPitch_RobotPreference(IntPtr robotPreferencePtr);
        public double GetClawPitch()
        {
            return getClawPitch_RobotPreference(m_robotPreferencePtr);
        }

        Joints[] NewJointsArray(int count)
        {
            Joints[] joints = new Joints[count];
            for (int i = 0; i < count; i++)
            {
                joints[i] = new Joints();
            }
            return joints;
        }

        public List<double[]> GetData() {
            List<double[]> lstRt = new List<double[]>();
            Joints[] joints = NewJointsArray(3);
            int i = 0;
            GetJointInitPosition(ref joints[i++]);
            GetJointCompensation(ref joints[i++]);
            GetJointSearchSequence(ref joints[i++]);
            for (int j=0;j<joints.Length;j++) {
                lstRt.Add(joints[j].ToArray());
            }
            return lstRt;
        }
    }

    public enum ROBOTMODEL
    {
        ROBOTMODEL_HI_ARM_3 = 0x00, ROBOTMODEL_HI_ARM_4, ROBOTMODEL_HI_ARM_6, ROBOTMODEL_HI_ARM_7,
        ROBOTMODEL_HI_ARM_DUAL_17 = 0x10,
        ROBOTMODEL_HI_VEHICLE_DIFF_TWO = 0x20, ROBOTMODEL_HI_VEHICLE_ACKERMANN, ROBOTMODEL_HI_VEHICLE_QUATTRO,
        ROBOTMODEL_HI_VEHICLE_OMNI_QUATTRO, ROBOTMODEL_HI_VEHICLE_MECANUM_QUATTRO, ROBOTMODEL_HI_VEHICLE_TANK,
        ROBOTMODEL_HI_ROBOT_SINGLE_13 = 0x30,
        ROBOTMODEL_HI_ROBOT_DUAL_26 = 0x40,
        ROBOTMODEL_ABB_IRB120 = 0x50,
        ROBOTMODEL_FANUC_LRMATE200ID4S = 0x60,
        ROBOTMODEL_KUKA_KR3R540 = 0x70, ROBOTMODEL_KUKA_KR4R600 = 0x71,
        ROBOTMODEL_YASKAWA_GP4 = 0x80,
        ROBOTMODEL_ARM_TEST = 0x100
    };
    public enum ROBOTDEV_TYPE
    {
        ROBOTDEV_SIM = 0X00,    // 仿真  
        ROBOTDEV_AXI = 0X10,    // ZYNQ控制器
        ROBOTDEV_CAN = 0X20,    // CAN通讯
        ROBOTDEV_ECAT = 0X30,   // EtherCAT通讯
    };
    public enum VEHICLEDEV_TYPE
    {
        VEHICLEDEV_SIM = 0x00,  // 仿真
        VEHICLEDEV_UART = 0x10, // UART
        VEHICLEDEV_UDP = 0x20,  // UDP
    };

    public class RobotConSysPreference
    {
        const string dllName = "RobotConSys_wrapper";
        public readonly IntPtr m_robotConSysPreferencePtr;

        [DllImport(dllName, EntryPoint = "createRobotConSysPreference")]
        public static extern IntPtr createRobotConSysPreference();
        public RobotConSysPreference()
        {
            m_robotConSysPreferencePtr = createRobotConSysPreference();
        }

        [DllImport(dllName, EntryPoint = "freeRobotConSysPreference")]
        public static extern void freeRobotConSysPreference(IntPtr robotConSysPreferencePtr);
        ~RobotConSysPreference()
        {
            freeRobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "copy_RobotConSysPreference")]
        public static extern void copy_RobotConSysPreference(IntPtr robotConSysPreferencePtr, IntPtr srcRobotConSysPreferencePtr);
        public void Copy(RobotConSysPreference robotConSysPreference)
        {
            copy_RobotConSysPreference(m_robotConSysPreferencePtr, robotConSysPreference.m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotName_RobotConSysPreference")]
        public static extern void setRobotName_RobotConSysPreference(IntPtr robotConSysPreferencePtr, string name);
        public void SetRobotName(string name)
        {
            setRobotName_RobotConSysPreference(m_robotConSysPreferencePtr, name);
        }

        [DllImport(dllName, EntryPoint = "getRobotName_RobotConSysPreference")]
        public static extern int getRobotName_RobotConSysPreference(IntPtr robotConSysPreferencePtr, IntPtr stringPtr);
        public string GetRobotName()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getRobotName_RobotConSysPreference(m_robotConSysPreferencePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "setRobotModel_RobotConSysPreference")]
        public static extern void setRobotModel_RobotConSysPreference(IntPtr robotConSysPreferencePtr, ROBOTMODEL model);
        public void SetRobotModel(ROBOTMODEL model)
        {
            setRobotModel_RobotConSysPreference(m_robotConSysPreferencePtr, model);
        }

        [DllImport(dllName, EntryPoint = "getRobotModel_RobotConSysPreference")]
        public static extern ROBOTMODEL getRobotModel_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public ROBOTMODEL GetRobotModel()
        {
            return getRobotModel_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotDevType_RobotConSysPreference")]
        public static extern void setRobotDevType_RobotConSysPreference(IntPtr robotConSysPreferencePtr, ROBOTDEV_TYPE type);
        public void SetRobotDevType(ROBOTDEV_TYPE type)
        {
            setRobotDevType_RobotConSysPreference(m_robotConSysPreferencePtr, type);
        }

        [DllImport(dllName, EntryPoint = "getRobotDevType_RobotConSysPreference")]
        public static extern ROBOTDEV_TYPE getRobotDevType_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public ROBOTDEV_TYPE GetRobotDevType()
        {
            return getRobotDevType_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotDevPeriod_RobotConSysPreference")]
        public static extern void setRobotDevPeriod_RobotConSysPreference(IntPtr robotConSysPreferencePtr, uint period_us);
        public void SetRobotDevPeriod(uint period_us)
        {
            setRobotDevPeriod_RobotConSysPreference(m_robotConSysPreferencePtr, period_us);
        }

        [DllImport(dllName, EntryPoint = "getRobotDevPeriod_RobotConSysPreference")]
        public static extern uint getRobotDevPeriod_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public uint GetRobotDevPeriod()
        {
            return getRobotDevPeriod_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setRobotDevChannelNum_RobotConSysPreference")]
        public static extern void setRobotDevChannelNum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetRobotDevChannelNum(byte num)
        {
            setRobotDevChannelNum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getRobotDevChannelNum_RobotConSysPreference")]
        public static extern byte getRobotDevChannelNum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetRobotDevChannelNum()
        {
            return getRobotDevChannelNum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setIOEnableState_RobotConSysPreference")]
        public static extern void setIOEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr, SWITCHSTATE state);
        public void SetIOEnableState(SWITCHSTATE state)
        {
            setIOEnableState_RobotConSysPreference(m_robotConSysPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getIOEnableState_RobotConSysPreference")]
        public static extern SWITCHSTATE getIOEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public SWITCHSTATE GetIOEnableState()
        {
            return getIOEnableState_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setDINum_RobotConSysPreference")]
        public static extern void setDINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetDINum(byte num)
        {
            setDINum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getDINum_RobotConSysPreference")]
        public static extern byte getDINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetDINum()
        {
            return getDINum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setDONum_RobotConSysPreference")]
        public static extern void setDONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetDONum(byte num)
        {
            setDONum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getDONum_RobotConSysPreference")]
        public static extern byte getDONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetDONum()
        {
            return getDONum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setAINum_RobotConSysPreference")]
        public static extern void setAINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetAINum(byte num)
        {
            setAINum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getAINum_RobotConSysPreference")]
        public static extern byte getAINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetAINum()
        {
            return getAINum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setAONum_RobotConSysPreference")]
        public static extern void setAONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetAONum(byte num)
        {
            setAONum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getAONum_RobotConSysPreference")]
        public static extern byte getAONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetAONum()
        {
            return getAONum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMotionIOEnableState_RobotConSysPreference")]
        public static extern void setMotionIOEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr, SWITCHSTATE state);
        public void SetMotionIOEnableState(SWITCHSTATE state)
        {
            setMotionIOEnableState_RobotConSysPreference(m_robotConSysPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getMotionIOEnableState_RobotConSysPreference")]
        public static extern SWITCHSTATE getMotionIOEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public SWITCHSTATE GetMotionIOEnableState()
        {
            return getMotionIOEnableState_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMotionDINum_RobotConSysPreference")]
        public static extern void setMotionDINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetMotionDINum(byte num)
        {
            setMotionDINum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getMotionDINum_RobotConSysPreference")]
        public static extern byte getMotionDINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetMotionDINum()
        {
            return getMotionDINum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMotionDONum_RobotConSysPreference")]
        public static extern void setMotionDONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetMotionDONum(byte num)
        {
            setMotionDONum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getMotionDONum_RobotConSysPreference")]
        public static extern byte getMotionDONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetMotionDONum()
        {
            return getMotionDONum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMotionAINum_RobotConSysPreference")]
        public static extern void setMotionAINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetMotionAINum(byte num)
        {
            setMotionAINum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getMotionAINum_RobotConSysPreference")]
        public static extern byte getMotionAINum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetMotionAINum()
        {
            return getMotionAINum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setMotionAONum_RobotConSysPreference")]
        public static extern void setMotionAONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetMotionAONum(byte num)
        {
            setMotionAONum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getMotionAONum_RobotConSysPreference")]
        public static extern byte getMotionAONum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetMotionAONum()
        {
            return getMotionAONum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setEStopEnableState_RobotConSysPreference")]
        public static extern void setEStopEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr, SWITCHSTATE state);
        public void SetEStopEnableState(SWITCHSTATE state)
        {
            setEStopEnableState_RobotConSysPreference(m_robotConSysPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getEStopEnableState_RobotConSysPreference")]
        public static extern SWITCHSTATE getEStopEnableState_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public SWITCHSTATE GetEStopEnableState()
        {
            return getEStopEnableState_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setModbusSlavePort_RobotConSysPreference")]
        public static extern void setModbusSlavePort_RobotConSysPreference(IntPtr robotConSysPreferencePtr, int port);
        public void SetModbusSlavePort(int port)
        {
            setModbusSlavePort_RobotConSysPreference(m_robotConSysPreferencePtr, port);
        }

        [DllImport(dllName, EntryPoint = "getModbusSlavePort_RobotConSysPreference")]
        public static extern int getModbusSlavePort_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public int GetModbusSlavePort()
        {
            return getModbusSlavePort_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevChannelNum_RobotConSysPreference")]
        public static extern void setVehicleDevChannelNum_RobotConSysPreference(IntPtr robotConSysPreferencePtr, byte num);
        public void SetVehicleDevChannelNum(byte num)
        {
            setVehicleDevChannelNum_RobotConSysPreference(m_robotConSysPreferencePtr, num);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevChannelNum_RobotConSysPreference")]
        public static extern byte getVehicleDevChannelNum_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public byte GetVehicleDevChannelNum()
        {
            return getVehicleDevChannelNum_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevType_RobotConSysPreference")]
        public static extern void setVehicleDevType_RobotConSysPreference(IntPtr robotConSysPreferencePtr, VEHICLEDEV_TYPE type);
        public void SetVehicleDevType(VEHICLEDEV_TYPE type)
        {
            setVehicleDevType_RobotConSysPreference(m_robotConSysPreferencePtr, type);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevType_RobotConSysPreference")]
        public static extern VEHICLEDEV_TYPE getVehicleDevType_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public VEHICLEDEV_TYPE GetVehicleDevType()
        {
            return getVehicleDevType_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevPeriod_RobotConSysPreference")]
        public static extern void setVehicleDevPeriod_RobotConSysPreference(IntPtr robotConSysPreferencePtr, uint period_us);
        public void SetVehicleDevPeriod(uint period_us)
        {
            setVehicleDevPeriod_RobotConSysPreference(m_robotConSysPreferencePtr, period_us);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevPeriod_RobotConSysPreference")]
        public static extern uint getVehicleDevPeriod_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public uint GetVehicleDevPeriod()
        {
            return getVehicleDevPeriod_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevIP_RobotConSysPreference")]
        public static extern void setVehicleDevIP_RobotConSysPreference(IntPtr robotConSysPreferencePtr, string ip);
        public void SetVehicleDevIP(string ip)
        {
            setVehicleDevIP_RobotConSysPreference(m_robotConSysPreferencePtr, ip);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevIP_RobotConSysPreference")]
        public static extern int getVehicleDevIP_RobotConSysPreference(IntPtr robotConSysPreferencePtr, IntPtr stringPtr);
        public string GetVehicleDevIP()
        {
            String_cpp str = new String_cpp();
            str.m_stringLen = getVehicleDevIP_RobotConSysPreference(m_robotConSysPreferencePtr, str.m_stringPtr);
            return str.GetStringInCS();
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevPort_RobotConSysPreference")]
        public static extern void setVehicleDevPort_RobotConSysPreference(IntPtr robotConSysPreferencePtr, int port);
        public void SetVehicleDevPort(int port)
        {
            setVehicleDevPort_RobotConSysPreference(m_robotConSysPreferencePtr, port);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevPort_RobotConSysPreference")]
        public static extern int getVehicleDevPort_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public int GetVehicleDevPort()
        {
            return getVehicleDevPort_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setVehicleDevIMUOnBoardEnable_RobotConSysPreference")]
        public static extern void setVehicleDevIMUOnBoardEnable_RobotConSysPreference(IntPtr robotConSysPreferencePtr, SWITCHSTATE state);
        public void SetVehicleDevIMUOnBoardEnable(SWITCHSTATE state)
        {
            setVehicleDevIMUOnBoardEnable_RobotConSysPreference(m_robotConSysPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getVehicleDevIMUOnBoardEnable_RobotConSysPreference")]
        public static extern SWITCHSTATE getVehicleDevIMUOnBoardEnable_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public SWITCHSTATE GetVehicleDevIMUOnBoardEnable()
        {
            return getVehicleDevIMUOnBoardEnable_RobotConSysPreference(m_robotConSysPreferencePtr);
        }

        [DllImport(dllName, EntryPoint = "setInitPlayState_RobotConSysPreference")]
        public static extern void setInitPlayState_RobotConSysPreference(IntPtr robotConSysPreferencePtr, SYSPLAYSTATE state);
        public void SetInitPlayState(SYSPLAYSTATE state)
        {
            setInitPlayState_RobotConSysPreference(m_robotConSysPreferencePtr, state);
        }

        [DllImport(dllName, EntryPoint = "getInitPlayState_RobotConSysPreference")]
        public static extern SYSPLAYSTATE getInitPlayState_RobotConSysPreference(IntPtr robotConSysPreferencePtr);
        public SYSPLAYSTATE GetInitPlayState()
        {
            return getInitPlayState_RobotConSysPreference(m_robotConSysPreferencePtr);
        }
    }

    public enum MOVEDIRECTION { MOVE_NEGATIVE, MOVE_POSITIVE };

    public enum VEHICLE_SERVO_MOTOR_OFFSET { OFFSET_1, OFFSET_2, OFFSET_3, OFFSET_4, OFFSET_ALL };
    public enum VEHICLE_CONTROL_TYPE { REMOTE, UART, BLUETOOTH, CAN, ETHERNET };
    public enum VEHICLE_TYPE { DIFF_TWO, ACKERMANN, QUATTRO, OMNI_QUATTRO, MECANUM_QUATTRO, TANK };
    public enum VEHICLE_MOTOR_INPUT_MODE { UNKNOWN, PERIODIC_MODE, CONTOUR_MODE };
    public enum VEHICLE_MODE { NORMAL, _MOVE_XY, MOVE_Y, ROTATE, PARKING };
    public enum VEHICLE_MOTOR_SERVO_STATE { OFF, ON };

    public struct Vehicle_Config_Data
    {
        public VEHICLE_CONTROL_TYPE controlType;
        public VEHICLE_TYPE vehicleType;

        public float wheelDistance;                  // mm
        public float axisDistance;                   // mm

        public float targetx_max;                    // mm/s
        public float targety_max;                    // mm/s
        public float targetz_max;                    // rad/s

        public float targetx_max_acc;                // mm/s^2
        public float targety_max_acc;                // mm/s^2
        public float targetz_max_acc;                // rad/s^2

        public VEHICLE_MOTOR_INPUT_MODE wheelMotorInputMode;
        public float wheelMotorVelMax;               // mm/s
        public float wheelMotorAccMax;               // mm/s^2
        public short wheelMotorVelResolution;
        public short wheelMotorAccResolution;
        public float wheelMotorDiameter;             // mm
        public float wheelMotorReduceRatio;
        public int wheelMotorDirection1;
        public int wheelMotorDirection2;
        public int wheelMotorDirection3;
        public int wheelMotorDirection4;

        public VEHICLE_MOTOR_INPUT_MODE servoMotorInputMode;
        public float servoMotorPosMin;               // rad
        public float servoMotorPosMax;               // rad
        public float servoMotorVelMax;               // rad/s
        public float servoMotorAccMax;               // rad/s^2
        public int servoMotorPosResolution;
        public short servoMotorVelResolution;
        public short servoMotorAccResolution;
        public float servoMotorReduceRatio;
        public int servoMotorDirection1;
        public int servoMotorDirection2;
        public int servoMotorDirection3;
        public int servoMotorDirection4;
        public int servoMotorOffset1;
        public int servoMotorOffset2;
        public int servoMotorOffset3;
        public int servoMotorOffset4;
        public float servoMotorOffsetAngle1;
        public float servoMotorOffsetAngle2;
        public float servoMotorOffsetAngle3;
        public float servoMotorOffsetAngle4;
        public short servoMotorCalibrateTorque;

        public byte canMotorEnable;

        public List<string> GetDataList() {
            List<string> lstRt = new List<string>();
            lstRt.Add(controlType.ToString());
            lstRt.Add(vehicleType.ToString());

            lstRt.Add(wheelDistance.ToString());
            lstRt.Add(axisDistance.ToString());
            lstRt.Add(targetx_max.ToString());
            lstRt.Add(targety_max.ToString());
            lstRt.Add(targetz_max.ToString());
            lstRt.Add(targetx_max_acc.ToString());
            lstRt.Add(targety_max_acc.ToString());
            lstRt.Add(targetz_max_acc.ToString());


            lstRt.Add(wheelMotorInputMode.ToString());
            lstRt.Add(wheelMotorVelMax.ToString());
            lstRt.Add(wheelMotorAccMax.ToString());
            lstRt.Add(wheelMotorVelResolution.ToString());
            lstRt.Add(wheelMotorAccResolution.ToString());
            lstRt.Add(wheelMotorDiameter.ToString());
            lstRt.Add(wheelMotorReduceRatio.ToString());
            lstRt.Add(wheelMotorDirection1.ToString());
            lstRt.Add(wheelMotorDirection2.ToString());
            lstRt.Add(wheelMotorDirection3.ToString());
            lstRt.Add(wheelMotorDirection4.ToString());

            lstRt.Add(servoMotorInputMode.ToString());
            lstRt.Add(servoMotorPosMin.ToString());
            lstRt.Add(servoMotorPosMax.ToString());
            lstRt.Add(servoMotorVelMax.ToString());
            lstRt.Add(servoMotorAccMax.ToString());

            lstRt.Add(servoMotorPosResolution.ToString());
            lstRt.Add(servoMotorVelResolution.ToString());
            lstRt.Add(servoMotorAccResolution.ToString());
            lstRt.Add(servoMotorReduceRatio.ToString());
            lstRt.Add(servoMotorDirection1.ToString());
            lstRt.Add(servoMotorDirection2.ToString());

            lstRt.Add(servoMotorDirection3.ToString());
            lstRt.Add(servoMotorDirection4.ToString());
            lstRt.Add(servoMotorOffset1.ToString());
            lstRt.Add(servoMotorOffset2.ToString());
            lstRt.Add(servoMotorOffset3.ToString());
            lstRt.Add(servoMotorOffset4.ToString());

            lstRt.Add(servoMotorOffsetAngle1.ToString());
            lstRt.Add(servoMotorOffsetAngle2.ToString());
            lstRt.Add(servoMotorOffsetAngle3.ToString());
            lstRt.Add(servoMotorOffsetAngle4.ToString());

            lstRt.Add(servoMotorCalibrateTorque.ToString());
            lstRt.Add(canMotorEnable.ToString());
            return lstRt;
        }

        public void SetData(int index, string value) {
            switch (index) {
                case 1: controlType = (VEHICLE_CONTROL_TYPE)int.Parse(value); break;
                case 2: vehicleType = (VEHICLE_TYPE)int.Parse(value); break;
                case 3: wheelDistance = float.Parse(value); break;
                case 4: axisDistance = float.Parse(value); break;
                case 5: targetx_max = float.Parse(value); break;
                case 6: targety_max = float.Parse(value); break;
                case 7: targetz_max = float.Parse(value); break;
                case 8: targetx_max_acc = float.Parse(value); break;
                case 9: targety_max_acc = float.Parse(value); break;
                case 10: targetz_max_acc = float.Parse(value); break;
                case 11: wheelMotorInputMode = (VEHICLE_MOTOR_INPUT_MODE)int.Parse(value); break;
                case 12: wheelMotorVelMax = float.Parse(value); break;
                case 13: wheelMotorAccMax = float.Parse(value); break;
                case 14: wheelMotorVelResolution = short.Parse(value); break;
                case 15: wheelMotorAccResolution = short.Parse(value); break;
                case 16: wheelMotorDiameter = float.Parse(value); break;
                case 17: wheelMotorReduceRatio = float.Parse(value); break;
                case 18: wheelMotorDirection1 = int.Parse(value); break;
                case 19: wheelMotorDirection2 = int.Parse(value); break;
                case 20: wheelMotorDirection3 = int.Parse(value); break;
                case 21: wheelMotorDirection4 = int.Parse(value); break;
                
                case 22: servoMotorInputMode = (VEHICLE_MOTOR_INPUT_MODE)int.Parse(value); break;
                case 23: servoMotorPosMin = float.Parse(value); break;
                case 24: servoMotorPosMax = float.Parse(value); break;
                case 25: servoMotorVelMax = float.Parse(value); break;
                case 26: servoMotorAccMax = float.Parse(value); break;
                case 27: servoMotorPosResolution = int.Parse(value); break;
                case 28: servoMotorVelResolution = short.Parse(value); break;
                case 29: servoMotorAccResolution = short.Parse(value); break;
                case 30: servoMotorReduceRatio = float.Parse(value); break;

                case 31: servoMotorDirection1 = int.Parse(value); break;
                case 32: servoMotorDirection2 = int.Parse(value); break;
                case 33: servoMotorDirection3 = int.Parse(value); break;
                case 34: servoMotorDirection4 = int.Parse(value); break;
                case 35: servoMotorOffset1 = int.Parse(value); break;
                case 36: servoMotorOffset2 = int.Parse(value); break;
                case 37: servoMotorOffset3 = int.Parse(value); break;
                case 38: servoMotorOffset4 = int.Parse(value); break;
                case 39: servoMotorOffsetAngle1 = float.Parse(value); break;
                case 40: servoMotorOffsetAngle2 = float.Parse(value); break;
                case 41: servoMotorOffsetAngle3 = float.Parse(value); break;
                case 42: servoMotorOffsetAngle4 = float.Parse(value); break;
                case 43: servoMotorCalibrateTorque = short.Parse(value); break;
                case 44: canMotorEnable = byte.Parse(value); break;

                default: break;
            }
        }
    }

    public struct Vehicle_State_Data
    {
        public byte batterySOC;
        public VEHICLE_MODE vehicleMode;
        public VEHICLE_MOTOR_SERVO_STATE vehicleServoState;
        public float vehicle_x_vel;  // mm/s
        public float vehicle_y_vel;  // mm/s
        public float vehicle_z_vel;  // rad/s

        public float wheelVel1;    // rad/s
        public float wheelVel2;
        public float wheelVel3;
        public float wheelVel4;
        public float servoPos1;    // rad
        public float servoPos2;
        public float servoPos3;
        public float servoPos4;

        public byte sonar1;         // cm
        public byte sonar2;
        public byte sonar3;
        public byte sonar4;
        public byte sonar5;
        public byte sonar6;
        public byte sonar7;
        public byte sonar8;
        public byte irDutyCycle1;   // %
        public byte irDutyCycle2;
    }

    public struct Vehicle_IMU_Data
    {
        public short ppsSyncStamp;
        public sbyte temprature;
        public float pressure;
        public uint timestamp;
        public float acc1;
        public float acc2;
        public float acc3;
        public float gry1;
        public float gry2;
        public float gry3;
        public float mag1;
        public float mag2;
        public float mag3;
        public float eul1;
        public float eul2;
        public float eul3;
        public float quat1;
        public float quat2;
        public float quat3;
        public float quat4;
    };
}
