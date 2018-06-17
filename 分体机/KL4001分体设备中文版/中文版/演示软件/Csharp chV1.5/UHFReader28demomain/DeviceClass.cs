using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;

namespace UHFReader28demomain
{
    /// <summary>
    /// 设备对象，记录设备信息并提供设备操作接口；
    /// </summary>
    public class DeviceClass
    {
        private int communicationTimeout = 1000;

        /// <summary>
        /// 广播地址；
        /// </summary>
        public static uint Broadcast
        {
            get
            {
                return 0xffffffff;
            }
        }

        private IntPtr _devHandle;
        /// <summary>
        /// 设备句柄；
        /// </summary>
        public IntPtr DevHandle
        {
            get
            {
                return _devHandle;
            }
            set
            {
                _devHandle = value;
            }
        }

        private uint _devIP;
        /// <summary>
        /// 设备IP地址；
        /// </summary>
        public uint DeviceIP
        {
            get
            {
                return _devIP;
            }
            set
            {
                _devIP = value;
            }
        }

        private string _devMac;
        /// <summary>
        /// 设备设备Mac地址；
        /// </summary>
        public string DeviceMac
        {
            get
            {
                return _devMac;
            }
            set
            {
                _devMac = value;
            }
        }

        private string _devName;
        /// <summary>
        /// 设备名；
        /// </summary>
        public string DeviceName
        {
            get
            {
                return _devName;
            }
            set
            {
                _devName = value;
            }
        }

        private bool _isLogin = false;
        /// <summary>
        /// 设备登录状态；
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return _isLogin;
            }
        }

        /// <summary>
        /// 设备对象构造函数；
        /// </summary>
        /// <param name="devHandle">设备句柄</param>
        /// <param name="deviceIP">设备IP地址</param>
        /// <param name="deviceMac">设备Mac地址</param>
        /// <param name="deviceName">设备名</param>
        public DeviceClass(IntPtr devHandle, uint deviceIP, string deviceMac, string deviceName)
        {
            this.DevHandle = devHandle;
            this.DeviceIP = deviceIP;
            this.DeviceMac = deviceMac;
            this.DeviceName = deviceName;
        }

        /// <summary>
        /// 登录设备；
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode Login(string userName, string password)
        {
            StringBuilder nameBuf, passwordBuf;
            DevControl.tagErrorCode eCode;

            nameBuf = new StringBuilder(userName);
            passwordBuf = new StringBuilder(password);
            eCode = DevControl.DM_AuthLogin(this._devHandle, nameBuf, passwordBuf, this.communicationTimeout);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                this._isLogin = true;
            }
            else
            {
                this._isLogin = false;
            }

            return eCode;
        }

        /// <summary>
        /// 登出设备；
        /// </summary>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode Logout()
        {
            DevControl.tagErrorCode eCode = DevControl.tagErrorCode.DM_ERR_OK;
            if (this._isLogin == true)
            {
                eCode = DevControl.DM_LogOutDevice(this._devHandle, this.communicationTimeout);
                this._isLogin = false;
            }

            return eCode;
        }

        /// <summary>
        /// 修改设备密码；
        /// </summary>
        /// <param name="oldPassword">用户当前使用的密码</param>
        /// <param name="newPassword">用户新的密码</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode ModifyPassword(string oldPassword, string newPassword)
        {
            StringBuilder newPasswordBuf, passwordBuf;
            DevControl.tagErrorCode eCode;

            passwordBuf = new StringBuilder(oldPassword);
            newPasswordBuf = new StringBuilder(newPassword);
            eCode = DevControl.DM_ModifyPassword(this._devHandle, passwordBuf, newPasswordBuf, this.communicationTimeout);

            return eCode;
        }

        /// <summary>
        /// 重启设备；
        /// </summary>
        /// <param name="rebootType">设备重启方式类型，如是否保存当前参数、是否恢复默认值等</param>
        /// <returns>tagErrorCode</returns>
        public DevControl.tagErrorCode RebootManage(RebootType rebootType)
        {
            DevControl.tagErrorCode eCode;

            switch (rebootType)
            {
                case RebootType.DefaultWithoutReboot:
                    eCode = DevControl.DM_LoadDefault(this._devHandle, this.communicationTimeout);
                    break;

                case RebootType.DefaultAndReboot:
                    eCode = DevControl.DM_LoadDefault(this._devHandle, this.communicationTimeout);
                    if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                    {
                        eCode = DevControl.DM_ResetDevice(this._devHandle, this.communicationTimeout);
                    }
                    break;

                case RebootType.RebootWithoutSave:
                    eCode = DevControl.DM_ResetDeviceWithoutSave(this._devHandle, this.communicationTimeout);
                    break;
                    
                case RebootType.SaveAndReboot:
                    eCode = DevControl.DM_ResetDevice(this._devHandle, this.communicationTimeout);
                    break;

                default:
                    eCode = DevControl.tagErrorCode.DM_ERR_ARG;
                    Debug.Fail("Not Support this RebootType!");
                    break;
            };

            return eCode;
        }

        /// <summary>
        /// 获取设备所支持串口通道的数量；
        /// </summary>
        /// <returns>设备所支持串口通道的数量</returns>
        public bool IsSupportChannel(int channelNum)
        {
            return DevControl.DM_IsComEnable(this._devHandle, channelNum);
        }
    }

    /// <summary>
    /// 重启方式类型；
    /// </summary>
    public enum RebootType
    {
        /// <summary>
        /// 只恢复默认参数，不重启设备；
        /// </summary>
        DefaultWithoutReboot,
        /// <summary>
        /// 重启设备，并恢复默认参数；
        /// </summary>
        DefaultAndReboot,
        /// <summary>
        /// 重启设备，不保存未保存参数；
        /// </summary>
        RebootWithoutSave,
        /// <summary>
        /// 重启设备，并保存参数；
        /// </summary>
        SaveAndReboot
    };
}
