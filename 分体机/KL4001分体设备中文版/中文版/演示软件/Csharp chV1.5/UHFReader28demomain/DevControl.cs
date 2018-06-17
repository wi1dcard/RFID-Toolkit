using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace UHFReader28demomain
{
	internal delegate void SearchCallBack(IntPtr dev, IntPtr data);

    public class DevControl
    {
        private const string DLL_NAME = "dmdll.dll";

        public enum tagErrorCode
        {
            DM_ERR_OK,				/* no error */
            DM_ERR_PARA,			/* parameter error */
            DM_ERR_NOAUTH,			/* */
            DM_ERR_AUTHFAIL,		/* auth fail */
            DM_ERR_SOCKET,			/* socket error */
            DM_ERR_MEM,				/* */
            DM_ERR_TIMEOUT,
            DM_ERR_ARG,
            DM_ERR_MATCH,			/* parameters in command and reply are not match */
            DM_ERR_OPR,
            DM_ERR_MAX
        };

        internal enum DataType
        {
            PARA_TYPE_STRING,
            PARA_TYPE_UCHAR,
            PARA_TYPE_USHORT,
            PARA_TYPE_ULONG,
            PARA_TYPE_UCHAR_HEX,
            PARA_TYPE_INVALID
        };
        /// <summary>
        /// 初始化系统
        /// </summary>
        /// <param name="searchCB">搜索到设备后的回调函数</param>
        /// <returns>初始化是否成功</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_Init(SearchCallBack searchCB, IntPtr data);

        /// <summary>
        /// 回收系统
        /// </summary>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_DeInit();

        /// <summary>
        /// 搜索网络中的设备，搜索到的设备将通过搜索回调函数SearchCallBack返回
        /// </summary>
        /// <param name="deviceIP">搜索的IP，如果搜索所有，请使用255.255.255.255</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_SearchDevice(uint deviceIP, int timeout);

        /// <summary>
        /// 返回设备的基本信息，主要在搜索回调中使用
        /// </summary>
        /// <param name="devhandle">内部handle</param>
        /// <param name="ipaddr">IP地址</param>
        /// <param name="macaddr">物理地址</param>
        /// <param name="devname">设备名称</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_GetDeviceInfo(IntPtr devhandle, ref uint ipaddr, StringBuilder macaddr, StringBuilder devname);

        /// <summary>
        /// 登录设备
        /// </summary>
        /// <param name="devHandle">设备内部handle</param>
        /// <param name="name">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>登录结果</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_AuthLogin(IntPtr devHandle, StringBuilder name, StringBuilder password, int timeout);

        /// <summary>
        /// 修改设备密码
        /// </summary>
        /// <param name="devHandle">设备内部handle</param>
        /// <param name="oldPassword">用户当前使用的密码</param>
        /// <param name="newPassword">用户新的密码</param>
        /// <param name="timeout">超时时间</param>
        /// <returns>登录结果</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ModifyPassword(IntPtr devHandle, StringBuilder oldPassword, StringBuilder newPassword, int timeout);

        /// <summary>
        /// 创建一个获取、配置参数的列表
        /// </summary>
        /// <param name="devHandle">内部handle</param>
        /// <returns>如果创建失败，返回空</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr paralistCreate(IntPtr devHandle);

        /// <summary>
        /// 销毁一个参数列表
        /// </summary>
        /// <param name="list">create函数返回的指针</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralistDestroy(IntPtr list);

        /// <summary>
        /// 向参数列表中添加一个参数
        /// </summary>
        /// <param name="list">列表指针</param>
        /// <param name="chanNo">通道号</param>
        /// <param name="paraType">参数类型</param>
        /// <param name="valueLen">配置的数据长度</param>
        /// <param name="value">配置的数据</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_addnode(IntPtr list, int chanNo, PARA_TYPES paraType, int valueLen, byte[] value);

        /// <summary>
        /// 向参数列表中添加一个参数
        /// </summary>
        /// <param name="list">列表指针</param>
        /// <param name="chanNo">通道号</param>
        /// <param name="paraType">参数类型</param>
        /// <returns></returns>
        internal static tagErrorCode paralist_addnode(IntPtr list, int chanNo, PARA_TYPES paraType)
        {
            return paralist_addnode(list, chanNo, paraType, 0, null);
        }

        /// <summary>
        /// 在参数列表中销毁一个参数
        /// </summary>
        /// <param name="list">列表指针</param>
        /// <param name="chanNo">通道号</param>
        /// <param name="paraType">参数类型</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_delnode(IntPtr list, int chanNo, PARA_TYPES paraType);

        /// <summary>
        /// 向获取参数列表中查询获取结果
        /// </summary>
        /// <param name="list">列表</param>
        /// <param name="chanNo">通道号</param>
        /// <param name="paraType">参数类型</param>
        /// <param name="valueType">数据类型</param>
        /// <param name="valueLen">数据长度</param>
        /// <param name="value">数据值</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode paralist_getnode(IntPtr list, int chanNo, PARA_TYPES paraType, ref int valueLen, byte[] value);

        /// <summary>
        /// 向获取参数列表中查询获取结果，结果全部为字符串；
        /// </summary>
        /// <param name="paraType">参数类型</param>
        /// <param name="valueType">数据缓冲区</param>
        /// <param name="valueLen">数据长度</param>
        /// <param name="value">字符串数据缓冲区</param>
        /// /// <param name="valueLen">字符串数据长度</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_Value2String(PARA_TYPES eParaType, byte[] bufferValue, int nValueLen, StringBuilder bufferString, ref int nStringLen);

        /// <summary>
        /// 向获取参数列表中查询获取结果，结果全部为字符串；
        /// </summary>
        /// <param name="paraType">参数类型</param>
        /// <param name="valueType">数据缓冲区</param>
        /// <param name="valueLen">数据长度</param>
        /// <param name="value">字符串数据缓冲区</param>
        /// /// <param name="valueLen">字符串数据长度</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_String2Value(PARA_TYPES eParaType, StringBuilder bufferString, int nStringLen, byte[] bufferValue, ref int nValueLen);

        /// <summary>
        /// 验证参数是否有效
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="chanNo">通道号</param>
        /// <param name="paraType">参数类型</param>
        /// <param name="valuelen">参数长度</param>
        /// <param name="value">参数值</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_CheckPara(IntPtr devHandle, int chanNo, PARA_TYPES paraType, int valuelen, byte[] value);

        /// <summary>
        /// 向设备获取list中的参数
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="list">list</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_GetPara(IntPtr devHandle, IntPtr list, int timeout);

        /// <summary>
        /// 向设备配置list中的参数
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="list">list</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_SetPara(IntPtr devHandle, IntPtr list, int timeout);

        /// <summary>
        /// 重启设备并保存当前设置参数；
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ResetDevice(IntPtr devHandle, int timeout);

        /// <summary>
        /// 重启设备，不保存当前设置参数；
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_ResetDeviceWithoutSave(IntPtr devHandle, int timeout);

        /// <summary>
        /// 恢复设备默认参数，但不保存也不重启设备；
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_LoadDefault(IntPtr devHandle, int timeout);

        /// <summary>
        /// 登出设备
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_LogOutDevice(IntPtr devHandle, int timeout);

        /// <summary>
        /// 释放搜索到设备
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <returns></returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern tagErrorCode DM_FreeDevice(IntPtr devHandle);

        /// <summary>
        /// 检查设备是否支持指定COM；
        /// </summary>
        /// <param name="devHandle">设备handle</param>
        /// <param name="comNum">COM编号</param>
        /// <returns>BOOLEAN</returns>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool DM_IsComEnable(IntPtr devHandle, int comNum);

        internal enum PARA_TYPES
        {
            BAUDRATE,
            DATABITS,
            STOPBITS,
            PARITY,
            FLOWCONTROL,
            FIFO,
            ENABLEPACKING,
            IDLEGAPTIME,
            MATCH2BYTESEQUENCE,
            SENDFRAMEONLY,
            SENDTRAILINGBYTES,
            INPUTWITHACTIVECONNECT,
            OUTPUTWITHACTIVECONNECT,
            INPUTWITHPASSIVECONNECT,
            OUTPUTWITHPASSIVECONNECT,
            INPUTATTIMEOFDISCONNECT,
            OUTPUTATTIMEOFDISCONNECT,
            IPCONFIGURATION,				/* */
            AUTONEGOTIATE,
            MACADDRESS,						/* ethernet interface mac address */
            SPEED,
            DUPLEX,
            NETPROTOCOL,
            ACCEPTINCOMING,
            ARPCACHETIMEOUT,
            TCPKEEPACTIVE,
            CPUPERFORMANCEMODE,
            HTTPSERVERPORT,
            MTUSIZE,
            RETRYCOUNTER,
            IPADDRESS,						/* ethernet interface ip address(static) */
            FIRMWARE,
            UPTIME,
            SERIALNO,
            RETRYTIMEOUT,
            HOSTLIST1_IP,
            HOSTLIST1_PORT,
            HOSTLIST2_IP,
            HOSTLIST2_PORT,
            HOSTLIST3_IP,
            HOSTLIST3_PORT,
            HOSTLIST4_IP,
            HOSTLIST4_PORT,
            HOSTLIST5_IP,
            HOSTLIST5_PORT,
            HOSTLIST6_IP,
            HOSTLIST6_PORT,
            HOSTLIST7_IP,
            HOSTLIST7_PORT,
            HOSTLIST8_IP,
            HOSTLIST8_PORT,
            HOSTLIST9_IP,
            HOSTLIST9_PORT,
            HOSTLIST10_IP,
            HOSTLIST10_PORT,
            HOSTLIST11_IP,
            HOSTLIST11_PORT,
            HOSTLIST12_IP,
            HOSTLIST12_PORT,
            FIRSTMATCHBYTE,
            LASTMATCHBYTE,
            DATAGRAMTYPE,
            DEVICEADDRESSTABLE1_BEGINIP,
            UDPLOCALPORT,
            UDPREMOTEPORT,
            UDPNETSEGMENT,
            DEVICEADDRESSTABLE2_BEGINIP,
            DEVICEADDRESSTABLE2_PORT,
            DEVICEADDRESSTABLE2_ENDIP,
            DEVICEADDRESSTABLE3_BEGINIP,
            DEVICEADDRESSTABLE3_PORT,
            DEVICEADDRESSTABLE3_ENDIP,
            DEVICEADDRESSTABLE4_BEGINIP,
            DEVICEADDRESSTABLE4_PORT,
            DEVICEADDRESSTABLE4_ENDIP,
            UDPUNICASTLOCALPORT,
            ACCEPTIONINCOMING,
            ACTIVECONNECT,
            STARTCHARACTER,
            ONDSRDROP,
            HARDDISCONNECT,
            CHECKEOT,
            INACTIVITYTIMEOUT_M,
            INACTIVITYTIMEOUT_S,
            LOCALPORT,
            REMOTEHOST,
            REMOTEPORT,
            DNSQUERYPERIOD,
            DEVICEADDRESSTABLE1_ENDIP,
            DEVICEADDRESSTABLE1_PORT,
            CONNECTRESPONSE,
            TERMINALNAME,
            USEHOSTLIST,
            EMAILADDRESS,
            EMAILUSERNAME,
            EMAILPASSWORD,
            EMAILINPUTTRIGGERMESSAGE,
            EMAILADDRESS1,
            EMAILADDRESS2,
            EMAILADDRESS3,
            POP3DOMAINNAME,
            SMTPDOMAINNAME,
            POP3PORT,
            SMTPPORT,
            COLDSTART,
            DCDCHANGED,
            DSRCHANGED,
            WARMSTART,
            AUTHENTICATIONFAILURE,
            IPADDRESSCHANGED,
            ENABLESERIALTRIGGERINPUT,
            SERIALCHANNEL,
            SERIALDATASIZE,
            SERIALMATCHDATA1,
            SERIALMATCHDATA2,
            EMAILTRIGGERSUBJECT,
            PRIORITY,
            INPUTPRIORITY,
            INPUTMINNOTIFICATIONINTERVAL,
            MINNOTIFICATIONINTERVAL,
            RENOTIFICATIONINTERVAL,
            NEWUSERPSW,
            BOOTP,
            DHCP,
            AUTOIP,
            DHCPHOSTNAME,
            SUBNET,
            DEFAULTGATEWAY,
            DEVICENAME,				/* device name, server name */
            TIMEZONE,
            LOCALTIME_YEAR,
            LOCALTIME_MONTH,
            LOCALTIME_DAY,
            LOCALTIME_HOUR,
            LOCALTIME_MINUTE,
            LOCALTIME_SECOND,
            TIMESERVER,
            WEBCONSOLE,
            TELNETCONSOLE,
            PASSWORDCHANGED,
            SERIALPORTOPTIONS,
            PREFERREDDNSSERVER,
            ALTERNATEDNSSERVER,
            SERIALMATCHDATA3,
            INPUT1,
            INPUT2,
            IO1,
            IO2,
            IO1TYPE,
            IO2TYPE,
            IO1STATE,
            IO2STATE,
            SERIALPORTPROTOCOL,
            FIRMWAREID,
            PPPOEUSERNAME,
            PPPOEPASSWORD,
            PPPOEWORKMODE,
            PPPOEMAXREDIALTIMES,
            PPPOEREDIALINTERVAL,
            PPPOEIDLETIME,
            PPPOESTATUS,
            PPPOEIP,
            PPPOEGATEWAY,
            PPPOEDNS1,
            PPPOEDNS2,
            ENABLEBACKUPLINK,

            END_OF_PARA_TYPES
        };

        internal static tagErrorCode getParaStringValue(IntPtr paraList, int chanNo, PARA_TYPES paraType, ref int valueLen, StringBuilder value)
        {
            byte[] bufferValue = new byte[100];
            int getLen = bufferValue.Length;
            tagErrorCode errCode;
            errCode = paralist_getnode(paraList, chanNo, paraType, ref getLen, bufferValue);
            if (errCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                errCode = DM_Value2String(paraType, bufferValue, getLen, value, ref valueLen);
            }

            return errCode;
        }
    }
}

