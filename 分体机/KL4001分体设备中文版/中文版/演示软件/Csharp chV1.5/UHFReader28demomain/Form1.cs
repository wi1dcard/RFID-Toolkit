using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Reflection;
using ReaderB;
using System.IO.Ports;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace UHFReader28demomain
{
    public partial class Form1 : Form
    {
        private bool fAppClosed; //在测试模式下响应关闭应用程序
        private byte fComAdr=0xff; //当前操作的ComAdr
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private int fCmdRet=30; //所有执行指令的返回值
        private int fOpenComIndex; //打开的串口索引号
        private bool fIsInventoryScan;
        private bool fisinventoryscan_6B;
        private byte[] fOperEPC=new byte[100];
        private byte[] fPassWord=new byte[4];
        private byte[] fOperID_6B=new byte[10];
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private bool fTimer_6B_ReadWrite;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private int frmcomportindex;
        private bool ComOpen=false;
        private bool breakflag = false;
        private bool SeriaATflag = false;
        private double x_z;
        private double y_f;
        public DeviceClass SelectedDevice;
        private static List<DeviceClass> DevList;
        private static SearchCallBack searchCallBack = new SearchCallBack(searchCB);
        
        /// <summary>
        /// Device Search的回调函数;
        /// </summary>
        private static void searchCB(IntPtr dev, IntPtr data)
        {
            uint ipAddr = 0;
            StringBuilder devname = new StringBuilder(100);
            StringBuilder macAdd = new StringBuilder(100);

            //获取搜索到的设备信息；
            DevControl.tagErrorCode eCode = DevControl.DM_GetDeviceInfo(dev, ref ipAddr, macAdd, devname);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                //将搜索到的设备加入设备列表；
                DeviceClass device = new DeviceClass(dev, ipAddr, macAdd.ToString(), devname.ToString());
                DevList.Add(device);
            }
            else
            {
                //异常处理；
                string errMsg = ErrorHandling.GetErrorMsg(eCode);
                Log.WriteError(errMsg);
            }
        }
        private static IPAddress getIPAddress(uint interIP)
        {
            return new IPAddress((uint)IPAddress.HostToNetworkOrder((int)interIP));
        }
        public Form1()
        {
            InitializeComponent();
            //初始化设备列表；
            DevList = new List<DeviceClass>();

            //初始化设备控制模块；
            DevControl.tagErrorCode eCode = DevControl.DM_Init(searchCallBack, IntPtr.Zero);
            if (eCode != DevControl.tagErrorCode.DM_ERR_OK)
            {
                //如果初始化失败则关闭程序，并进行异常处理；
                string errMsg = ErrorHandling.HandleError(eCode);
                throw new Exception(errMsg);
            }
        }
        private void RefreshStatus()
        {
            if (!(ComboBox_AlreadyOpenCOM.Items.Count != 0))
                StatusBar1.Panels[1].Text = "COM Closed";
            else
                StatusBar1.Panels[1].Text = " COM" + Convert.ToString(frmcomportindex);
              StatusBar1.Panels[0].Text ="";
              StatusBar1.Panels[2].Text ="";
        }
        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "操作成功";
                case 0x01:
                    return "询查时间结束前返回";
                case 0x02:
                    return "指定的询查时间溢出";
                case 0x03:
                    return "本条消息之后，还有消息";
                case 0x04:
                    return "读写模块存储空间已满";
                case 0x05:
                    return "访问密码错误";
                case 0x09:
                    return "销毁密码错误";
                case 0x0a:
                    return "销毁密码不能为全0";
                case 0x0b:
                    return "电子标签不支持该命令";
                case 0x0c:
                    return "对该命令，访问密码不能为全0";
                case 0x0d:
                    return "电子标签已经被设置了读保护，不能再次设置";
                case 0x0e:
                    return "电子标签没有被设置读保护，不需要解锁";
                case 0x10:
                    return "有字节空间被锁定，写入失败";
                case 0x11:
                    return "不能锁定";
                case 0x12:
                    return "已经锁定，不能再次锁定";
                case 0x13:
                    return "参数保存失败,但设置的值在读写模块断电前有效";
                case 0x14:
                    return "无法调整";
                case 0x15:
                    return "询查时间结束前返回";
                case 0x16:
                    return "指定的询查时间溢出";
                case 0x17:
                    return "本条消息之后，还有消息";
                case 0x18:
                    return "读写模块存储空间已满";
                case 0x19:
                    return "电子不支持该命令或者访问密码不能为0";
                case 0xFA:
                    return "有电子标签，但通信不畅，无法操作";
                case 0xFB:
                    return "无电子标签可操作";
                case 0xFC:
                    return "电子标签返回错误代码";
                case 0xFD:
                    return "命令长度错误";
                case 0xFE:
                    return "不合法的命令";
                case 0xFF:
                    return "参数错误";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x32:
                    return "返回数据长度有错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x34:
                    return "繁忙，指令正在执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效句柄";
                case 0x38:
                    return "无效端口";
                case 0xEE:
                    return "返回指令错误";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "其它错误";
                case 0x03:
                    return "存储器超限或不被支持的PC值";
                case 0x04:
                    return "存储器锁定";
                case 0x0b:
                    return "电源不足";
                case 0x0f:
                    return "非特定错误";
                default:
                    return "";
            }
        }
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet);
            }
            finally
            {
                ;
            }
        }
        private void AddCmdLog(string CMD, string cmdStr, int cmdRet,int errocode)
        {
            try
            {
                StatusBar1.Panels[0].Text = "";
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " " +
                                            cmdStr + ": " +
                                            GetReturnCodeDesc(cmdRet)+" "+"0x"+Convert.ToString(errocode,16).PadLeft(2,'0');
            }
            finally
            {
                ;
            }
        }
        private void ClearLastInfo()
        { 
            ComboBox_AlreadyOpenCOM.Refresh();
              RefreshStatus();
              Edit_Type.Text = "";
              Edit_Version.Text = "";
              ISO180006B.Checked=false;
              EPCC1G2.Checked=false;
              Edit_ComAdr.Text = "";
              Edit_powerdBm.Text = "";
              Edit_scantime.Text = "";
              Edit_dminfre.Text = "";
              Edit_dmaxfre.Text = "";
            //  PageControl1.TabIndex = 0;
        }
        private void InitComList()
        {
            int i = 0;
            ComboBox_COM.Items.Clear();
              ComboBox_COM.Items.Add(" AUTO");
              for (i = 1; i < 13;i++ )
                  ComboBox_COM.Items.Add(" COM" + Convert.ToString(i));
              ComboBox_COM.SelectedIndex = 0;
              RefreshStatus();
        }
        private void InitReaderList()
        {
            int i=0;
            ComboBox_PowerDbm.SelectedIndex = 30;
            ComboBox_baud.SelectedIndex =3;
             for (i=0 ;i< 63;i++)
             {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6+i*0.4)+" MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
             }
              ComboBox_dmaxfre.SelectedIndex = 62;
              ComboBox_dminfre.SelectedIndex = 0;
              for (i=0x03;i<=0xff;i++)
                  ComboBox_scantime.Items.Add(Convert.ToString(i) + "*100ms");
              ComboBox_scantime.SelectedIndex = 7;
              i=10;
              while (i<=300)
              {
                  ComboBox_IntervalTime.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime.SelectedIndex = 0;
              for (i=0;i<7;i++)
                  ComboBox_BlockNum.Items.Add(Convert.ToString(i * 2) + " and " + Convert.ToString(i * 2 + 1));
              ComboBox_BlockNum.SelectedIndex = 0;
              i=40;
              while (i<=300 )
              {
                  ComboBox_IntervalTime_6B.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime_6B.SelectedIndex = 1;

              for (i=0;i<256;i++)
              ComboBox_RelayTime.Items.Add(Convert.ToString(i)) ;
              ComboBox_RelayTime.SelectedIndex=0;
              for (i=0;i<256;i++)
                comboBox3.Items.Add(Convert.ToString(i));
              comboBox3.SelectedIndex = 0;
              comboBox2.SelectedIndex = 1;
              comboBox4.SelectedIndex=0;
              comboBox5.SelectedIndex = 0;
              ComboBox_Accuracy.SelectedIndex = 8;

              for (i = 0; i < 256; i++)
                  comboBox1.Items.Add(Convert.ToString(i));
              comboBox1.SelectedIndex = 0;
            for (i = 1; i < 256; i++)
                  comboBox6.Items.Add(Convert.ToString(i));
              comboBox6.SelectedIndex = 29;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Visible = false;
              fOpenComIndex = -1;
              fComAdr = 0;
              ferrorcode= -1;
              fBaud =5;
              InitComList();
              InitReaderList();
              NoAlarm_G2.Checked  =true;
             
              Byone_6B.Checked=true;
              Different_6B.Checked=true;

              P_EPC.Checked=true;
              C_EPC.Checked=true;
              DestroyCode.Checked=true;
              NoProect.Checked=true;
              NoProect2.Checked=true;
              fAppClosed = false;
              fIsInventoryScan = false;
              fisinventoryscan_6B = false;
              fTimer_6B_ReadWrite=false ;
              Label_Alarm.Visible=false;
              Timer_Test_.Enabled = false;
              Timer_G2_Read.Enabled = false;
              Timer_G2_Alarm.Enabled = false;
              timer1.Enabled = false;

              Button3.Enabled = false;
              Button5.Enabled = false;
              Button1.Enabled = false;
              button2.Enabled = false;
              Button_DestroyCard.Enabled = false;
              Button_WriteEPC_G2.Enabled = false;
              Button_SetReadProtect_G2.Enabled = false;
              Button_SetMultiReadProtect_G2.Enabled = false;
              Button_RemoveReadProtect_G2.Enabled = false;
              Button_CheckReadProtected_G2.Enabled = false;
              Button_SetEASAlarm_G2.Enabled = false;
              button4.Enabled = false;
              Button_LockUserBlock_G2.Enabled = false;
              SpeedButton_Read_G2.Enabled = false;
              Button_DataWrite.Enabled = false;
              BlockWrite.Enabled = false;
              Button_BlockErase.Enabled = false;
              Button_SetProtectState.Enabled = false;
              SpeedButton_Query_6B.Enabled = false;
              SpeedButton_Read_6B.Enabled = false;
              SpeedButton_Write_6B.Enabled = false;
              Button14.Enabled = false;
              Button15.Enabled = false;

              DestroyCode.Enabled = false;
              AccessCode.Enabled = false;
              NoProect.Enabled = false;
              Proect.Enabled = false;
              Always.Enabled = false;
              AlwaysNot.Enabled = false;
              NoProect2.Enabled = false;
              Proect2.Enabled = false;
              Always2.Enabled = false;
              AlwaysNot2.Enabled = false;
              P_Reserve.Enabled = false;
              P_EPC.Enabled = false;
              P_TID.Enabled = false;
              P_User.Enabled = false;
              Same_6B.Enabled = false;
              Different_6B.Enabled = false;
              Less_6B.Enabled = false;
              Greater_6B.Enabled = false;

             
              button10.Enabled = false ;
              button11.Enabled = false ;
             
               radioButton_band1.Checked = true;
       
             ComboBox_baud2.SelectedIndex = 3;
             button13.Enabled = false;

             GetClock.Checked = true;
             Radio_beepEn.Checked = true;
             radioButton3.Checked = true;
             radioButton5.Checked = true;
             radioButton8.Checked = true;
             radioButton10.Checked = true;
             R_EPC.Checked = true;
             Button_SetGPIO.Enabled = false;
             Button_GetGPIO.Enabled = false;
             Button_Ant.Enabled = false;
             Button_RelayTime.Enabled = false;
             ClockCMD.Enabled = false;
             Button_OutputRep.Enabled = false;
             Button_Beep.Enabled = false;
             Button_Accuracy.Enabled = false;
             button6.Enabled = false;
             button8.Enabled = false;
             button9.Enabled = false;
             button12.Enabled = false;
             button18.Enabled = false;
             button19.Enabled = false;
             button20.Enabled = false;
             button21.Enabled = false;
             button23.Enabled = false;
             button24.Enabled = false;
             radioButton1.Checked = true;
             button25.Enabled = false;
             button26.Enabled = false;
             button27.Enabled = false;
             button28.Enabled = false;
             button29.Enabled = false;
             button31.Enabled = false;
             button32.Enabled = false;
             button33.Enabled = false;
             button34.Enabled = false;
             button35.Enabled = false;
             button36.Enabled = false;
             button37.Enabled = false;
             button38.Enabled = false;

            /////////////////TCPIP网口配置
             protocolCB.SelectedIndex = 0;
        }

        private void OpenPort_Click(object sender, EventArgs e)
        {
            int port=0;
            int openresult,i;
            openresult = 30;
            string temp;
            Cursor = Cursors.WaitCursor;
              if  (Edit_CmdComAddr.Text=="")
              Edit_CmdComAddr.Text="FF";
              fComAdr = Convert.ToByte(Edit_CmdComAddr.Text,16); // $FF;
              try
              {
                  if (ComboBox_COM.SelectedIndex == 0)//Auto
                  {
                      fBaud=Convert.ToByte(ComboBox_baud2.SelectedIndex);
                      if (fBaud>2)
                          fBaud =Convert.ToByte(fBaud + 2);
                    openresult =StaticClassReaderB.AutoOpenComPort(ref port,ref fComAdr,fBaud,ref frmcomportindex);
                    fOpenComIndex = frmcomportindex;
                    if (openresult == 0 )
                    {
                        ComOpen = true;
                       // Button3_Click(sender, e); //自动执行读取写卡器信息
                        if (fBaud > 3)
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                        }
                        else
                        {
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                        }
                        Button3_Click(sender, e); //自动执行读取写卡器信息
                      if ((fCmdRet==0x35) ||(fCmdRet==0x30))
                        {
                            MessageBox.Show("串口通讯错误", "信息提示");
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                            ComOpen = false;
                        }
                    }          
                  }
                  else
                  {
                    temp = ComboBox_COM.SelectedItem.ToString();
                    temp = temp.Trim();
                    port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                    for (i = 6; i >= 0; i--)
                    {
                        fBaud = Convert.ToByte(i);
                        if ((fBaud == 3) || (fBaud == 4))
                            continue;
                        openresult = StaticClassReaderB.OpenComPort(port, ref fComAdr, fBaud, ref frmcomportindex);
                        fOpenComIndex = frmcomportindex;
                        if (openresult == 0x35)
                        {
                            MessageBox.Show("串口已打开", "信息提示");
                            return;
                        }
                        if (openresult == 0)
                        {
                            ComOpen = true;
                            Button3_Click(sender, e); //自动执行读取写卡器信息
                            if (fBaud > 3)
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2);
                            }
                            else
                            {
                                ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud);
                            }
                            if ((fCmdRet == 0x35) || (fCmdRet == 0x30))
                            {
                                ComOpen = false;
                                MessageBox.Show("串口通讯错误", "信息提示");
                                StaticClassReaderB.CloseSpecComPort(frmcomportindex);
                                return;
                            }
                            RefreshStatus();
                            break;
                        }
                    }
                  }
              }
              finally
              {
                  Cursor = Cursors.Default;
              }

              if ((fOpenComIndex != -1) &&(openresult != 0X35)  &&(openresult != 0X30))
              {
                ComboBox_AlreadyOpenCOM.Items.Add("COM"+Convert.ToString(fOpenComIndex)) ;
                ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
                Button3.Enabled = true ;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Query_6B.Enabled = true ;
                Button_SetGPIO.Enabled = true;
                Button_GetGPIO.Enabled = true;
                Button_Ant.Enabled = true;
                Button_RelayTime.Enabled = true;
                ClockCMD.Enabled = true;
                Button_OutputRep.Enabled = true;
                Button_Beep.Enabled = true;
                Button_Accuracy.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button12.Enabled = true;
                button18.Enabled = true;
                button19.Enabled = true;
                button20.Enabled = true;
                button21.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button23.Enabled = true;
                button24.Enabled = true;
                button25.Enabled = true;
                button26.Enabled = true;
                button27.Enabled = true;
                button28.Enabled = true;
                button29.Enabled = true;
                button31.Enabled = true;
                button32.Enabled = true;
                button33.Enabled = true;
                button34.Enabled = true;
                button35.Enabled = true;
                button36.Enabled = true;
                button37.Enabled = true;
                button38.Enabled = true;
                ComOpen = true;
              }
              if ((fOpenComIndex == -1) &&(openresult == 0x30))
                  MessageBox.Show("串口通讯错误", "信息提示");

            if ((ComboBox_AlreadyOpenCOM.Items.Count != 0)&&(fOpenComIndex != -1) && (openresult != 0x35) && (openresult != 0x30)&&(fCmdRet==0)) 
              {
                fComAdr = Convert.ToByte(Edit_ComAdr.Text,16);
                temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
              }
              RefreshStatus();
          }

        private void ClosePort_Click(object sender, EventArgs e)
        {
            int port;
            //string SelectCom ;
            string temp;
            ClearLastInfo();
              try
              {
                if (ComboBox_AlreadyOpenCOM.SelectedIndex  < 0 )
                {
                    MessageBox.Show("请选择要关闭的端口", "信息提示");
                }
                else
                {
                    temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                    port = Convert.ToInt32(temp.Substring(3,temp.Length - 3));
                    fCmdRet = StaticClassReaderB.CloseSpecComPort(port);
                    if (fCmdRet == 0)
                    {
                        ComboBox_AlreadyOpenCOM.Items.RemoveAt(0);
                        if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
                        {
                            temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString();
                            port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
                            StaticClassReaderB.CloseSpecComPort(port);
                            fComAdr = 0xFF;
                            StaticClassReaderB.OpenComPort(port,ref fComAdr, fBaud,ref frmcomportindex);
                            fOpenComIndex = frmcomportindex;
                            RefreshStatus();
                            Button3_Click(sender,e); //自动执行读取写卡器信息
                        }
                    }               
                    else
                        MessageBox.Show("串口通讯错误", "信息提示");
                  }
              }
              finally
              {
                  ;
              }
              if(ComboBox_AlreadyOpenCOM.Items.Count != 0)
                  ComboBox_AlreadyOpenCOM.SelectedIndex = 0;
              else
              {
                  fOpenComIndex = -1;
                  ComboBox_AlreadyOpenCOM.Items.Clear();
                  ComboBox_AlreadyOpenCOM.Refresh();
                  RefreshStatus();
                  Button3.Enabled = false;
                  Button5.Enabled = false;
                  Button1.Enabled = false;
                  button2.Enabled = false;
                  Button_DestroyCard.Enabled = false;
                  Button_WriteEPC_G2.Enabled = false;
                  Button_SetReadProtect_G2.Enabled = false;
                  Button_SetMultiReadProtect_G2.Enabled = false;
                  Button_RemoveReadProtect_G2.Enabled = false;
                  Button_CheckReadProtected_G2.Enabled = false;
                  Button_SetEASAlarm_G2.Enabled = false;
                  button4.Enabled = false;
                  Button_LockUserBlock_G2.Enabled = false;
                  SpeedButton_Read_G2.Enabled = false;
                  Button_DataWrite.Enabled = false;
                  BlockWrite.Enabled = false;
                  Button_BlockErase.Enabled = false;
                  Button_SetProtectState.Enabled = false;
                  SpeedButton_Query_6B.Enabled = false;
                  SpeedButton_Read_6B.Enabled = false;
                  SpeedButton_Write_6B.Enabled = false;
                  Button14.Enabled = false;
                  Button15.Enabled = false;

                  DestroyCode.Enabled = false;
                  AccessCode.Enabled = false;
                  NoProect.Enabled = false;
                  Proect.Enabled = false;
                  Always.Enabled = false;
                  AlwaysNot.Enabled = false;
                  NoProect2.Enabled = false;
                  Proect2.Enabled = false;
                  Always2.Enabled = false;
                  AlwaysNot2.Enabled = false;

                  P_Reserve.Enabled = false;
                  P_EPC.Enabled = false;
                  P_TID.Enabled = false;
                  P_User.Enabled = false;
                  Alarm_G2.Enabled = false;
                  NoAlarm_G2.Enabled = false;

                  Same_6B.Enabled = false;
                  Different_6B.Enabled = false;
                  Less_6B.Enabled = false;
                  Greater_6B.Enabled = false;
                  
                  DestroyCode.Enabled = false;
                  AccessCode.Enabled = false;
                  NoProect.Enabled = false;
                  Proect.Enabled = false;
                  Always.Enabled = false;
                  AlwaysNot.Enabled = false;
                  NoProect2.Enabled = false;
                  Proect2.Enabled = false;
                  Always2.Enabled = false;
                  AlwaysNot2.Enabled = false;
                  P_Reserve.Enabled = false;
                  P_EPC.Enabled = false;
                  P_TID.Enabled = false;
                  P_User.Enabled = false;
                  Button_WriteEPC_G2.Enabled = false;
                  Button_SetMultiReadProtect_G2.Enabled = false;
                  Button_RemoveReadProtect_G2.Enabled = false;
                  Button_CheckReadProtected_G2.Enabled = false;
                  button4.Enabled = false;

                  Button_DestroyCard.Enabled = false;
                  Button_SetReadProtect_G2.Enabled = false;
                  Button_SetEASAlarm_G2.Enabled = false;
                  Alarm_G2.Enabled = false;
                  NoAlarm_G2.Enabled = false;
                  Button_LockUserBlock_G2.Enabled = false;
                  SpeedButton_Read_G2.Enabled = false;
                  Button_DataWrite.Enabled = false;
                  BlockWrite.Enabled = false;
                  Button_BlockErase.Enabled = false;
                  Button_SetProtectState.Enabled = false;
                  ListView1_EPC.Items.Clear();
                  ComboBox_EPC1.Items.Clear();
                  ComboBox_EPC2.Items.Clear();
                  ComboBox_EPC3.Items.Clear();
                  ComboBox_EPC4.Items.Clear();
                  ComboBox_EPC5.Items.Clear();
                  ComboBox_EPC6.Items.Clear();
                  button2.Text = "查询标签";
                 

                  SpeedButton_Read_6B.Enabled = false;
                  SpeedButton_Write_6B.Enabled = false;
                  Button14.Enabled = false;
                  Button15.Enabled = false;
                
                  ListView_ID_6B.Items.Clear();
                  ComOpen = false;

                  button10.Text = "获取";
                  timer1.Enabled = false;
                  button10.Enabled = false;
                  button11.Enabled = false;
                  Button_SetGPIO.Enabled = false;
                  Button_GetGPIO.Enabled = false;
                  Button_Ant.Enabled = false;
                  Button_RelayTime.Enabled = false;
                  ClockCMD.Enabled = false;
                  Button_OutputRep.Enabled = false;
                  Button_Beep.Enabled = false;
                  Button_Accuracy.Enabled = false;
                  button6.Enabled = false;
                  button8.Enabled = false;
                  button9.Enabled = false;
                  button12.Enabled = false;
                  button18.Enabled = false;
                  button19.Enabled = false;
                  button20.Enabled = false;
                  button21.Enabled = false;
                  button23.Enabled = false;
                  button24.Enabled = false;
                  button25.Enabled = false;
                  button26.Enabled = false;
                  button27.Enabled = false;
                  button28.Enabled = false;
                  button29.Enabled = false;
                  button31.Enabled = false;
                  button32.Enabled = false;
                  button33.Enabled = false;
                  button34.Enabled = false;
                  button35.Enabled = false;
                  button36.Enabled = false;
                  button37.Enabled = false;
                  button38.Enabled = false;
              }
         }
        private void Button3_Click(object sender, EventArgs e)
        {
              byte TrType=0;
              byte[] VersionInfo=new byte[2];
              byte ReaderType=0;
              byte ScanTime=0;
              byte dmaxfre=0;
              byte dminfre = 0;
              byte powerdBm=0;
              byte FreBand = 0;
              byte Ant=0;
			  byte BeepEn=0;
              byte OutputRep = 0;
              Edit_Version.Text = "";
              Edit_ComAdr.Text = "";
              Edit_scantime.Text = "";
              Edit_Type.Text = "";
              ISO180006B.Checked=false;
              EPCC1G2.Checked=false;
              Edit_powerdBm.Text = "";
              Edit_dminfre.Text = "";
              Edit_dmaxfre.Text = "";
              
              fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, ref TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime,ref Ant,ref BeepEn,ref OutputRep, frmcomportindex);
              if (fCmdRet == 0)
              {
                  Edit_Version.Text = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                 
                  ComboBox_PowerDbm.SelectedIndex = Convert.ToInt32(powerdBm);   
                  Edit_ComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                  Edit_NewComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, '0');
                  Edit_scantime.Text = Convert.ToString(ScanTime, 10).PadLeft(2, '0') + "*100ms";
                  ComboBox_scantime.SelectedIndex = ScanTime - 3;
                  Edit_powerdBm.Text = Convert.ToString(powerdBm, 10).PadLeft(2, '0');

                  FreBand= Convert.ToByte(((dmaxfre & 0xc0)>> 4)|(dminfre >> 6)) ;
                  switch (FreBand)
                  {
                      case 0:
                          {
                              radioButton_band1.Checked = true;
                              fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
                              fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
                          }
                          break;
                      case 1: 
                          {
                              radioButton_band2.Checked = true;
                              fdminfre = 920.125 + (dminfre & 0x3F) * 0.25;
                              fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.25;
                          }
                          break;
                      case 2:
                          {
                              radioButton_band3.Checked = true;
                              fdminfre = 902.75 + (dminfre & 0x3F) * 0.5;
                              fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.5;
                          }
                          break;
                      case 3:
                          {
                              radioButton_band4.Checked = true;
                              fdminfre = 917.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 917.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                      case 4:
                          {
                              radioButton_band5.Checked = true;
                              fdminfre = 865.1 + (dminfre & 0x3F) * 0.2;
                              fdmaxfre = 865.1 + (dmaxfre & 0x3F) * 0.2;
                          }
                          break;
                  }
                  Edit_dminfre.Text = Convert.ToString(fdminfre) + "MHz";
                  Edit_dmaxfre.Text = Convert.ToString(fdmaxfre) + "MHz";
                  if (fdmaxfre != fdminfre)
                      CheckBox_SameFre.Checked = false;
                  ComboBox_dminfre.SelectedIndex = dminfre & 0x3F;
                  ComboBox_dmaxfre.SelectedIndex = dmaxfre & 0x3F;
                  if (ReaderType == 0x0A)
                      Edit_Type.Text = "UHFReader28";
                  if ((TrType & 0x02) == 0x02) //第二个字节低第四位代表支持的协议“ISO/IEC 15693”
                  {
                      ISO180006B.Checked = true;
                      EPCC1G2.Checked = true;
                  }
                  else
                  {
                      ISO180006B.Checked = false;
                      EPCC1G2.Checked = false;
                  }
                   switch (BeepEn)
                   {
                       case 1: 
                           Radio_beepEn.Checked=true;
                           break;
                       case 0: 
                           Radio_beepDis.Checked=true;
                           break;
                   }
                  if((Ant & 0x01)==1)
                   checkBox10.Checked=true;
                  else
                   checkBox10.Checked=false;

                 if((Ant & 0x02)==2)
                   checkBox11.Checked=true;
                  else
                   checkBox11.Checked=false;

                 if((Ant & 0x04)==4)
                   checkBox12.Checked=true;
                  else
                   checkBox12.Checked=false;

                 if((Ant & 0x08)==8)
                   checkBox13.Checked=true;
                  else
                   checkBox13.Checked=false;

                  if((OutputRep & 0x01)==1)
                   checkBox17.Checked=true;
                  else
                   checkBox17.Checked=false;

                  if((OutputRep & 0x02)==2)
                   checkBox16.Checked=true;
                  else
                   checkBox16.Checked=false;

               if ((OutputRep & 0x04) == 4)
                   checkBox15.Checked = true;
               else
                   checkBox15.Checked = false;

               if ((OutputRep & 0x08) == 8)
                   checkBox14.Checked = true;
               else
                   checkBox14.Checked = false;
              }
              AddCmdLog("GetReaderInformation", "获取读写器信息", fCmdRet);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
              byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime, band=0;
              string returninfo="";
              string returninfoDlg="";
              string setinfo;
              if (radioButton_band1.Checked)
                  band = 0;
              if (radioButton_band2.Checked)
                  band = 1;
              if (radioButton_band3.Checked)
                  band = 2;
              if (radioButton_band4.Checked)
                  band = 3;
              if (radioButton_band5.Checked)
                  band = 4;
              if (Edit_NewComAdr.Text == "")
                  return;
              progressBar1.Visible = true;
              progressBar1.Minimum = 0;
              dminfre = Convert.ToByte(((band & 3) << 6) | (ComboBox_dminfre.SelectedIndex & 0x3F));
              dmaxfre = Convert.ToByte(((band & 0x0c) << 4) | (ComboBox_dmaxfre.SelectedIndex & 0x3F));
                  aNewComAdr = Convert.ToByte(Edit_NewComAdr.Text);
                  powerDbm = Convert.ToByte(ComboBox_PowerDbm.SelectedIndex);
                  fBaud = Convert.ToByte(ComboBox_baud.SelectedIndex);
                  if (fBaud > 2)
                      fBaud = Convert.ToByte(fBaud + 2);
                  scantime = Convert.ToByte(ComboBox_scantime.SelectedIndex + 3);
                  setinfo = "写";
              progressBar1.Value =10;     
              fCmdRet = StaticClassReaderB.SetAddress(ref fComAdr,aNewComAdr,frmcomportindex);
              if (fCmdRet==0x13)
              fComAdr = aNewComAdr;
          if (fCmdRet == 0)
          {
              fComAdr = aNewComAdr;
              returninfo = returninfo + setinfo + "读写器地址成功";
          }
          else if (fCmdRet == 0xEE)
              returninfo = returninfo + setinfo + "读写器地址返回指令错误";
          else
          {
              returninfo = returninfo + setinfo + "读写器地址失败";
              returninfoDlg = returninfoDlg + setinfo + "读写器地址失败指令返回=0x"
               + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
          }
          progressBar1.Value = 25;
          fCmdRet = StaticClassReaderB.SetRfPower(ref fComAdr, powerDbm, frmcomportindex);
          if (fCmdRet == 0)
              returninfo = returninfo + ",功率成功";
          else if (fCmdRet == 0xEE)
              returninfo = returninfo + ",功率返回指令错误";
          else
          {
              returninfo = returninfo + ",功率失败";
              returninfoDlg = returninfoDlg + " " + setinfo + "功率失败指令返回=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
          }

          progressBar1.Value = 40;
          fCmdRet = StaticClassReaderB.SetRegion(ref fComAdr, dmaxfre, dminfre, frmcomportindex);
          if (fCmdRet == 0)
              returninfo = returninfo + ",频率成功";
          else if (fCmdRet == 0xEE)
              returninfo = returninfo + ",频率返回指令错误";
          else
          {
              returninfo = returninfo + ",频率失败";
              returninfoDlg = returninfoDlg + " " + setinfo + "频率失败指令返回=0x"
               + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
          }

          progressBar1.Value = 55;
          fCmdRet = StaticClassReaderB.SetBaudRate(ref fComAdr, fBaud, frmcomportindex);
          if (fCmdRet == 0)
              returninfo = returninfo + ",波特率成功";
          else if (fCmdRet == 0xEE)
              returninfo = returninfo + ",波特率返回指令错误";
          else
          {
              returninfo = returninfo + ",波特率失败";
              returninfoDlg = returninfoDlg + " " + setinfo + "波特率失败指令返回=0x"
               + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
          }

          progressBar1.Value = 70;
          fCmdRet = StaticClassReaderB.SetInventoryScanTime(ref fComAdr, scantime, frmcomportindex);
          if (fCmdRet == 0)
              returninfo = returninfo + ",询查时间成功";
          else if (fCmdRet == 0xEE)
              returninfo = returninfo + ",询查时间返回指令错误";
          else
          {
              returninfo = returninfo + ",询查时间失败";
              returninfoDlg = returninfoDlg + " " + setinfo + "询查时间失败指令返回=0x"
               + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
          }
              progressBar1.Value =100; 
              Button3_Click(sender,e);
              progressBar1.Visible=false;
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
              if  (returninfoDlg!="")
                  MessageBox.Show(returninfoDlg, "提示");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte aNewComAdr, powerDbm, dminfre, dmaxfre, scantime;
            string returninfo = "";
            string returninfoDlg = "";
            string setinfo;
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            dminfre = 0;
            dmaxfre = 62;
            aNewComAdr =0x00;
            powerDbm = 30;
            fBaud=5;
            scantime=10;
            setinfo = " 恢复 ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.SetAddress(ref fComAdr, aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "读写器地址成功";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "读写器地址返回指令错误";
            else
            {
                returninfo = returninfo + setinfo + "读写器地址失败";
                returninfoDlg = returninfoDlg + setinfo + "读写器地址失败指令返回=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetRfPower(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",功率成功";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",功率返回指令错误";
            else
            {
                returninfo = returninfo + ",功率失败";
                returninfoDlg = returninfoDlg + " " + setinfo + "功率失败指令返回=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.SetRegion(ref fComAdr, dmaxfre, dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",频率成功";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",频率返回指令错误";
            else
            {
                returninfo = returninfo + ",频率失败";
                returninfoDlg = returninfoDlg + " " + setinfo + "频率失败指令返回=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.SetBaudRate(ref fComAdr, fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",波特率成功";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",波特率返回指令错误";
            else
            {
                returninfo = returninfo + ",波特率失败";
                returninfoDlg = returninfoDlg + " " + setinfo + "波特率失败指令返回=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.SetInventoryScanTime(ref fComAdr, scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",询查时间成功";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",询查时间返回指令错误";
            else
            {
                returninfo = returninfo + ",询查时间失败";
                returninfoDlg = returninfoDlg + " " + setinfo + "询查时间失败指令返回=0x"
                 + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }


            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "提示");
            
        }

        private void CheckBox_SameFre_CheckedChanged(object sender, EventArgs e)
        {
             if (CheckBox_SameFre.Checked)
              ComboBox_dmaxfre.SelectedIndex = ComboBox_dminfre.SelectedIndex;
        }


        private void ComboBox_dfreSelect(object sender, EventArgs e)
        {
             if (CheckBox_SameFre.Checked )
             {
                 ComboBox_dminfre.SelectedIndex =ComboBox_dmaxfre.SelectedIndex;
             }
              else if  (ComboBox_dminfre.SelectedIndex> ComboBox_dmaxfre.SelectedIndex )
             {
                 ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex;
                 MessageBox.Show("最低频率应小于或等于最高频率", "错误提示");
             }
        }
        public void ChangeSubItem(ListViewItem ListItem, int subItemIndex, string ItemText)
        {
            if (subItemIndex == 1)
            {
                if (ItemText=="")
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    if (ListItem.SubItems[subItemIndex + 3].Text == "")
                    {
                        ListItem.SubItems[subItemIndex + 3].Text = "1";
                    }
                    else
                    {
                        ListItem.SubItems[subItemIndex + 3].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 3].Text) + 1);
                    }
                }
                else 
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[subItemIndex+3].Text = "1";
                }
                else
                {
                    ListItem.SubItems[subItemIndex + 3].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 3].Text) + 1);
                    if( (Convert.ToUInt32(ListItem.SubItems[subItemIndex + 3].Text)>9999))
                        ListItem.SubItems[subItemIndex + 3].Text="1";
                }

            }
            if (subItemIndex == 2)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                }
            }
            if (subItemIndex == 3)
            {
                if (ListItem.SubItems[subItemIndex].Text =="")
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                }
                else
                {
                    ListItem.SubItems[subItemIndex].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex].Text, 2) | Convert.ToInt32(ItemText, 2), 2);
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckBox_TID.Checked)
            {
                if ((textBox12.Text.Length) != 2 || ((textBox13.Text.Length) != 2))
                {
                    StatusBar1.Panels[0].Text = "询查TID参数错误！";
                    return;
                }
            }
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
           
            Timer_Test_.Enabled = !Timer_Test_.Enabled;
            if (!Timer_Test_.Enabled)
            {
                textBox12.Enabled = true;
                textBox13.Enabled = true;
                CheckBox_TID.Enabled = true;
                if (ListView1_EPC.Items.Count != 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = true;
                    Proect2.Enabled = true;
                    Always2.Enabled = true;
                    AlwaysNot2.Enabled = true;
                    P_Reserve.Enabled = true;
                    P_EPC.Enabled = true;
                    P_TID.Enabled = true;
                    P_User.Enabled = true;
                    Button_DestroyCard.Enabled = true;
                    Button_SetReadProtect_G2.Enabled = true;
                    Button_SetEASAlarm_G2.Enabled = true;
                    Alarm_G2.Enabled = true;
                    NoAlarm_G2.Enabled = true;
                    Button_LockUserBlock_G2.Enabled = true;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    SpeedButton_Read_G2.Enabled = true;
                    Button_SetProtectState.Enabled = true;
                    Button_DataWrite.Enabled = true;
                    BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                }
                if (ListView1_EPC.Items.Count == 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = false ;
                    Proect2.Enabled = false ;
                    Always2.Enabled = false ;
                    AlwaysNot2.Enabled = false ;
                    P_Reserve.Enabled = false;
                    P_EPC.Enabled = false;
                    P_TID.Enabled = false;
                    P_User.Enabled = false;
                    Button_DestroyCard.Enabled = false;
                    Button_SetReadProtect_G2.Enabled = false;
                    Button_SetEASAlarm_G2.Enabled = false;
                    Alarm_G2.Enabled = false;
                    NoAlarm_G2.Enabled = false;
                    Button_LockUserBlock_G2.Enabled = false;
                    SpeedButton_Read_G2.Enabled = false;
                    Button_DataWrite.Enabled = false;
                    BlockWrite.Enabled = false;
                    Button_BlockErase.Enabled = false;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true; 
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    Button_SetProtectState.Enabled = false;

                }
                AddCmdLog("Inventory", "退出询查", 0);
                button2.Text = "查询标签";
            }
            else
            {
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                CheckBox_TID.Enabled = false;
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false ;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button4.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                ListView1_EPC.Items.Clear();
                ComboBox_EPC1.Items.Clear();
                ComboBox_EPC2.Items.Clear();
                ComboBox_EPC3.Items.Clear();
                ComboBox_EPC4.Items.Clear();
                ComboBox_EPC5.Items.Clear();
                ComboBox_EPC6.Items.Clear();
                button2.Text = "停止";
                textBox18.Text = "";
                textBox17.Text = "";
            }
        }
        private void Inventory()
        {
              int i;
              int CardNum=0;
              int Totallen = 0;
              int EPClen,m;
              byte[] EPC=new byte[5000];
              int CardIndex;
              string temps;
              string s, sEPC;
              bool isonlistview;
              byte MaskMem=0;
              byte[] MaskAdr=new byte[2];
              byte MaskLen=0;
              byte[] MaskData=new byte[100];
              byte MaskFlag=0;
              byte Ant=0;
              string antstr="";
              string lastepc = "";
              byte AdrTID = 0;
              byte LenTID = 0;
              byte TIDFlag = 0;
              if (CheckBox_TID.Checked)
              {
                  AdrTID = Convert.ToByte(textBox12.Text, 16);
                  LenTID = Convert.ToByte(textBox13.Text, 16);
                  TIDFlag = 1;
              }
              else
              {
                  AdrTID = 0;
                  LenTID = 0;
                  TIDFlag = 0;
              }
              fIsInventoryScan = true;
              if(checkBox1.Checked)
                  MaskFlag=1;
              else
                  MaskFlag=0;
              if (R_EPC.Checked) MaskMem = 1;
              if (R_TID.Checked) MaskMem = 2;
              if (R_User.Checked) MaskMem = 3;
              MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
              MaskLen = Convert.ToByte(maskLen_textBox.Text);
              MaskData = HexStringToByteArray(maskData_textBox.Text);
              ListViewItem aListItem = new ListViewItem();
              fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, EPC, ref Ant, ref Totallen, ref CardNum, frmcomportindex);      
             if ( (fCmdRet == 1)| (fCmdRet == 2)| (fCmdRet == 3)| (fCmdRet == 4)|(fCmdRet == 0xFB) )//代表已查找结束，
             {
                 byte[] daw = new byte[Totallen];
                 Array.Copy(EPC, daw, Totallen);               
                 temps = ByteArrayToHexString(daw);
                 fInventory_EPC_List = temps;            //存贮记录
                 m=0;
                 if (CardNum==0)
                 {
                     fIsInventoryScan = false;
                     return;
                 }
                 antstr = Convert.ToString(Ant, 2);
                 for (CardIndex = 0;CardIndex<CardNum;CardIndex++)
                 {
                     EPClen = daw[m];
                     sEPC = temps.Substring(m * 2 + 2, EPClen * 2);
                     m = m + EPClen + 1;
                     if (sEPC.Length != EPClen*2 )
                     return;
                     isonlistview = false;
                   
                      for (i=0; i< ListView1_EPC.Items.Count;i++)     //判断是否在Listview列表内
                      {
                            if (sEPC==ListView1_EPC.Items[i].SubItems[1].Text)
                            {
                                aListItem = ListView1_EPC.Items[i];
                                ChangeSubItem(aListItem, 1, sEPC);
                                ChangeSubItem(aListItem, 3, antstr);                    
                                isonlistview=true;
                            }
                      }
                      if (!isonlistview)
                      {
                          aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          s = sEPC;
                          ChangeSubItem(aListItem, 1, s);
                          s = (sEPC.Length / 2).ToString().PadLeft(2, '0');
                          ChangeSubItem(aListItem, 2, s);
                          ChangeSubItem(aListItem, 3, antstr);
                          ListView1_EPC.EnsureVisible(ListView1_EPC.Items.Count-1);
                          if (!CheckBox_TID.Checked)
                          {
                              if (ComboBox_EPC1.Items.IndexOf(sEPC) == -1)
                              {
                                  ComboBox_EPC1.Items.Add(sEPC);
                                  ComboBox_EPC2.Items.Add(sEPC);
                                  ComboBox_EPC3.Items.Add(sEPC);
                                  ComboBox_EPC4.Items.Add(sEPC);
                                  ComboBox_EPC5.Items.Add(sEPC);
                                  ComboBox_EPC6.Items.Add(sEPC);
                              }
                          }
                         
                      }
                      if (sEPC != "") lastepc = sEPC;
                      if (ListView1_EPC.Items.Count > 0)
                      {
                          textBox18.Text = ListView1_EPC.Items.Count.ToString();
                          textBox17.Text = lastepc;
                          ListView1_EPC.TopItem = ListView1_EPC.Items[ListView1_EPC.Items.Count - 1];
                      }
                 }            
            }
            if (!CheckBox_TID.Checked)
            {
                if ((ComboBox_EPC1.Items.Count != 0))
                {
                    ComboBox_EPC1.SelectedIndex = 0;
                    ComboBox_EPC2.SelectedIndex = 0;
                    ComboBox_EPC3.SelectedIndex = 0;
                    ComboBox_EPC4.SelectedIndex = 0;
                    ComboBox_EPC5.SelectedIndex = 0;
                    ComboBox_EPC6.SelectedIndex = 0;
                }
            }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }
        private void Timer_Test__Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;           
            Inventory();
        }

        private void SpeedButton_Read_G2_Click(object sender, EventArgs e)
        {
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("起始地址为空", "信息提示");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("读/块擦除长度", "信息提示");
                return;
            }
            if (Edit_AccessCode2.Text == "")
            {
                MessageBox.Show("密码为空", "信息提示");
                return;
            }
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
  
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
               Timer_G2_Read.Enabled =!Timer_G2_Read.Enabled;
               if (Timer_G2_Read.Enabled)
               {
                   DestroyCode.Enabled = false;
                   AccessCode.Enabled = false;
                   NoProect.Enabled = false;
                   Proect.Enabled = false;
                   Always.Enabled = false;
                   AlwaysNot.Enabled = false;
                   NoProect2.Enabled = false;
                   Proect2.Enabled = false;
                   Always2.Enabled = false;
                   AlwaysNot2.Enabled = false;
                   P_Reserve.Enabled = false;
                   P_EPC.Enabled = false;
                   P_TID.Enabled = false;
                   P_User.Enabled = false;
                   Button_WriteEPC_G2.Enabled = false;
                   Button_SetMultiReadProtect_G2.Enabled = false;
                   Button_RemoveReadProtect_G2.Enabled = false;
                   Button_CheckReadProtected_G2.Enabled = false;
                   button4.Enabled = false;

                   Button_DestroyCard.Enabled = false;
                   Button_SetReadProtect_G2.Enabled = false;
                   Button_SetEASAlarm_G2.Enabled = false;
                   Alarm_G2.Enabled = false;
                   NoAlarm_G2.Enabled = false;
                   Button_LockUserBlock_G2.Enabled = false;
                   button2.Enabled = false;
                   Button_DataWrite.Enabled = false;
                   BlockWrite.Enabled = false;
                   Button_BlockErase.Enabled = false;
                   Button_SetProtectState.Enabled = false;
                   SpeedButton_Read_G2.Text = "停止";
               }
               else
               {
                   if (ListView1_EPC.Items.Count != 0)
                   {
                       DestroyCode.Enabled = false;
                       AccessCode.Enabled = false;
                       NoProect.Enabled = false;
                       Proect.Enabled = false;
                       Always.Enabled = false;
                       AlwaysNot.Enabled = false;
                       NoProect2.Enabled = true;
                       Proect2.Enabled = true;
                       Always2.Enabled = true;
                       AlwaysNot2.Enabled = true;
                       P_Reserve.Enabled = true;
                       P_EPC.Enabled = true;
                       P_TID.Enabled = true;
                       P_User.Enabled = true;
                       Button_DestroyCard.Enabled = true;
                       Button_SetReadProtect_G2.Enabled = true;
                       Button_SetEASAlarm_G2.Enabled = true;
                       Alarm_G2.Enabled = true;
                       NoAlarm_G2.Enabled = true;
                       Button_LockUserBlock_G2.Enabled = true;
                       Button_WriteEPC_G2.Enabled = true;
                       Button_SetMultiReadProtect_G2.Enabled = true;
                       Button_RemoveReadProtect_G2.Enabled = true;
                       Button_CheckReadProtected_G2.Enabled = true;
                       button4.Enabled = true;
                       button2.Enabled = true;
                       Button_SetProtectState.Enabled = true;
                       
                       Button_DataWrite.Enabled = true;
                       BlockWrite.Enabled = true;
                       Button_BlockErase.Enabled = true;
                   }
                   if (ListView1_EPC.Items.Count == 0)
                   {
                       DestroyCode.Enabled = false;
                       AccessCode.Enabled = false;
                       NoProect.Enabled = false;
                       Proect.Enabled = false;
                       Always.Enabled = false;
                       AlwaysNot.Enabled = false;
                       NoProect2.Enabled = false;
                       Proect2.Enabled = false;
                       Always2.Enabled = false;
                       AlwaysNot2.Enabled = false;
                       P_Reserve.Enabled = false;
                       P_EPC.Enabled = false;
                       P_TID.Enabled = false;
                       P_User.Enabled = false;
                       Button_DestroyCard.Enabled = false;
                       Button_SetReadProtect_G2.Enabled = false;
                       Button_SetEASAlarm_G2.Enabled = false;
                       Alarm_G2.Enabled = false;
                       NoAlarm_G2.Enabled = false;
                       Button_LockUserBlock_G2.Enabled = false;
                       Button_SetProtectState.Enabled = false;
                       button2.Enabled = true;
                       Button_DataWrite.Enabled = false;
                       BlockWrite.Enabled = false;
                       Button_BlockErase.Enabled = false;
                       Button_WriteEPC_G2.Enabled = true;
                       Button_SetMultiReadProtect_G2.Enabled = true;
                       Button_RemoveReadProtect_G2.Enabled = true;
                       Button_CheckReadProtected_G2.Enabled = true;
                       button4.Enabled = true;
                   }
                   SpeedButton_Read_G2.Text = "读";
               }
        }

        private void Timer_G2_Read_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            string str;
            byte[] CardData=new  byte[320];
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if ((maskadr_textbox.Text=="")||(maskLen_textBox.Text==""))            
              {
                  fIsInventoryScan = false;
                  return;
              }
             
              if (textBox1.Text == "")
              {
                  fIsInventoryScan = false;
                  return;
              }
                if (ComboBox_EPC2.Items.Count == 0)
                {
                    fIsInventoryScan = false;
                    return;
                }
                if (ComboBox_EPC2.SelectedItem == null)
                {
                    fIsInventoryScan = false;
                    return;
                }
                str = ComboBox_EPC2.SelectedItem.ToString();
                if (checkBox1.Checked)
                {
                    ENum = 255;
                    if (R_EPC.Checked) MaskMem = 1;
                    if (R_TID.Checked) MaskMem = 2;
                    if (R_User.Checked) MaskMem = 3;
                    MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                    MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                    MaskData = HexStringToByteArray(maskData_textBox.Text);
                }
                else
                {
                    ENum = Convert.ToByte(str.Length / 4);
                }
                byte[] EPC = new byte[ENum*2];
                EPC = HexStringToByteArray(str);
                if (C_Reserve.Checked)
                    Mem = 0;
                if (C_EPC.Checked)
                    Mem = 1;
                if (C_TID.Checked)
                    Mem = 2;
                if (C_User.Checked)
                    Mem = 3;
                if (Edit_AccessCode2.Text == "")
                {
                    fIsInventoryScan = false;
                    return;
                }
                if (Edit_WordPtr.Text == "")
                {
                    fIsInventoryScan = false;
                    return;
                }
                WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
                Num = Convert.ToByte(textBox1.Text);
                if (Edit_AccessCode2.Text.Length != 8)
                {
                    fIsInventoryScan = false;
                    return;
                }
                fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
                fCmdRet = StaticClassReaderB.ReadData_G2(ref fComAdr, EPC, ENum, Mem, WordPtr, Num, fPassWord, MaskMem,MaskAdr, MaskLen, MaskData, CardData, ref ferrorcode, frmcomportindex);
                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Num*2];
                    Array.Copy(CardData, daw, Num * 2);
                    listBox1.Items.Add(ByteArrayToHexString(daw));
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    AddCmdLog("ReadData_G2", "读", fCmdRet);
                }
                if (ferrorcode != -1)
                {
                    StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +
                     " '读' 返回错误=0x" + Convert.ToString(ferrorcode, 2) +
                     "(" + GetErrorCodeDesc(ferrorcode) + ")";
                    ferrorcode=-1;
                }
                fIsInventoryScan = false;
                  if (fAppClosed)
                    Close();
        }

        private void Button_DataWrite_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Mem = 0;
            byte WNum = 0;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("起始地址为空", "信息提示");
                return;
            }
            
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            if (Edit_WriteData.Text == "")
                return;
            s2 = Edit_WriteData.Text;
            if (s2.Length % 4 != 0)
            {
                MessageBox.Show("以字为单位输入.", "写");
                return;
            }

            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2+1];
            Writedata = HexStringToByteArray(s2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                WNum = Convert.ToByte(s2.Length / 4 + 1);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.WriteData_G2(ref fComAdr, EPC,WNum,ENum, Mem, WordPtr, Writedata, fPassWord,MaskMem,MaskAdr,MaskLen,MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("Write data", "写", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "‘写”指令返回=0x00" +
                  "(写成功)";
            }    
        }

        private void Button_BlockErase_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            string str;
            byte[] CardData = new byte[320];
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("起始地址为空", "信息提示");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("读/块擦除长度", "信息提示");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
                return;
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            if ((Mem == 1) & (WordPtr < 2))
            {
                MessageBox.Show("擦除EPC区的起始地址长度必须大于等于0x01！请重新输入！", "信息提示");
                return;
            }
            Num = Convert.ToByte(textBox1.Text);
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            fCmdRet = StaticClassReaderB.BlockErase_G2(ref fComAdr, EPC, ENum, Mem, WordPtr, Num, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("EraseCard", "块擦除", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "“擦除数据”指令返回=0x00" +
                     "(擦除数据成功)";
            }       
        }

        private void button7_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void Button_SetProtectState_Click(object sender, EventArgs e)
        {
              byte select=0;
              byte setprotect=0;
              string str;
              byte ENum;
              byte[] CardData = new byte[320];
              byte MaskMem = 0;
              byte[] MaskAdr = new byte[2];
              byte MaskLen = 0;
              byte[] MaskData = new byte[100];
              if (checkBox1.Checked)
              {
                  if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                  {
                      MessageBox.Show("掩码数据不正确！", "错误提示");
                      return;
                  }
              }
              if (ComboBox_EPC1.Items.Count == 0)
                  return;
              if (ComboBox_EPC1.SelectedItem == null)
                  return;
              str = ComboBox_EPC1.SelectedItem.ToString();
              if (str == "")
                  return;
              if (checkBox1.Checked)
              {
                  ENum = 255;
                  if (R_EPC.Checked) MaskMem = 1;
                  if (R_TID.Checked) MaskMem = 2;
                  if (R_User.Checked) MaskMem = 3;
                  MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                  MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                  MaskData = HexStringToByteArray(maskData_textBox.Text);
              }
              else
              {
                  ENum = Convert.ToByte(str.Length / 4);
              }
              byte[] EPC = new byte[ENum*2];
              EPC = HexStringToByteArray(str);
              if (textBox2.Text.Length != 8)
              {
                  MessageBox.Show("访问密码小于8，重新输入！", "信息提示");
                  return;
              }
              fPassWord = HexStringToByteArray(textBox2.Text);
              if ((P_Reserve.Checked) & (DestroyCode.Checked))
                  select = 0x00;
              else if ((P_Reserve.Checked) & (AccessCode.Checked))
                  select = 0x01;
              else if (P_EPC.Checked)
                  select = 0x02;
              else if (P_TID.Checked)
                  select = 0x03;
              else if (P_User.Checked)
                  select = 0x04;
              if (P_Reserve.Checked)
              {
                  if (NoProect.Checked )
                   setprotect=0x00;
                  else if (Proect.Checked)
                   setprotect=0x02;
                  else if (Always.Checked )
                  {
                   setprotect=0x01;
                   if (MessageBox.Show(this, "确定要设置为永远可读可写吗？", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                       return;
                   }
                   else if (AlwaysNot.Checked)
                   {
                       setprotect = 0x03;
                       if (MessageBox.Show(this, "确定要设置为永远不可读不可写吗", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                           return;
                   }
              }
              else
              {
                  if (NoProect2.Checked)
                   setprotect=0x00;
                  else if (Proect2.Checked)
                   setprotect=0x02;
                  else if (Always2.Checked)
                  {
                   setprotect=0x01;
                   if (MessageBox.Show(this, "确定要设置为永远可写吗", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                       return;
                   }
                   else if (AlwaysNot2.Checked)
                   {
                       setprotect = 0x03;
                       if (MessageBox.Show(this, "确定要设置为永远不可写吗？", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                           return;
                   }
           }
           fCmdRet = StaticClassReaderB.Lock_G2(ref fComAdr, EPC, ENum, select, setprotect, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex); ;
           AddCmdLog("SetCardProtect", "设置保护", fCmdRet);
        }

        private void Button_DestroyCard_Click(object sender, EventArgs e)
        {
            string str;
            byte ENum;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "";
            if (MessageBox.Show(this, "确定要销毁这张标签吗？", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            if (Edit_DestroyCode.Text.Length != 8)
            {
                MessageBox.Show("销毁密码小于8位！请重新输入！", "信息提示");
                return;
            }
            if (ComboBox_EPC3.Items.Count == 0)
                return;
            if (ComboBox_EPC3.SelectedItem == null)
                return;
            str = ComboBox_EPC3.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_DestroyCode.Text);
            fCmdRet = StaticClassReaderB.KillTag_G2(ref fComAdr, EPC, ENum,fPassWord,MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("DestroyCard", "销毁标签", fCmdRet);
            if (fCmdRet == 0)
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “销毁标签”指令返回=0x00" +
                          "(销毁成功)";
        }

        private void Button_WriteEPC_G2_Click(object sender, EventArgs e)
        {
            byte[] WriteEPC = new byte[100];
            byte WriteEPClen;
            byte ENum;
            if (Edit_AccessCode3.Text.Length < 8)
            {
                MessageBox.Show("访问密码小于8位！请重新输入！!", "信息提示");
                return;
            }
            if ((Edit_WriteEPC.Text.Length % 4) != 0)
            {
                MessageBox.Show("请输入以字为单位的16进制数！'+#13+#10+'例如：1234、12345678!", "信息提示");
                return;
            }
            WriteEPClen = Convert.ToByte(Edit_WriteEPC.Text.Length / 2);
            ENum = Convert.ToByte(Edit_WriteEPC.Text.Length / 4);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(Edit_WriteEPC.Text);
            fPassWord = HexStringToByteArray(Edit_AccessCode3.Text);
            fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, ENum, ref ferrorcode, frmcomportindex);
            AddCmdLog("WriteEPC_G2", "写EPC", fCmdRet);
            if (fCmdRet == 0)
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "“写EPC”指令返回=0x00" +
                          "(写EPC成功)";
        }

        private void Button_SetReadProtect_G2_Click(object sender, EventArgs e)
        {
            byte ENum;
            string str;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
             if (Edit_AccessCode4.Text.Length < 8)
              {
                  MessageBox.Show("访问密码小于8位！请重新输入！", "信息提示");
                  return;
              }
              if (ComboBox_EPC4.Items.Count == 0)
                  return;
              if (ComboBox_EPC4.SelectedItem == null)
                  return;
              str = ComboBox_EPC4.SelectedItem.ToString();
              if (str == "")
                  return;
              if (checkBox1.Checked)
              {
                  ENum = 255;
                  if (R_EPC.Checked) MaskMem = 1;
                  if (R_TID.Checked) MaskMem = 2;
                  if (R_User.Checked) MaskMem = 3;
                  MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                  MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                  MaskData = HexStringToByteArray(maskData_textBox.Text);
              }
              else
              {
                  ENum = Convert.ToByte(str.Length / 4);
              }
              byte[] EPC = new byte[ENum*2];
              EPC = HexStringToByteArray(str);
              fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
              fCmdRet = StaticClassReaderB.SetPrivacyByEPC_G2(ref fComAdr, EPC, ENum, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
              AddCmdLog("SetReadProtect_G2", "设置单张读保护", fCmdRet);
              if (fCmdRet == 0)
              {
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '设置单张读保护'指令返回=0x00" +
                        "设置单张读保护成功";
              }
        }

        private void Button_SetMultiReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("访问密码小于8位！请重新输入！", "信息提示");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.SetPrivacyWithoutEPC_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("SetMultiReadProtect_G2", "设置单张读保护（不需EPC号）", fCmdRet);
             if (fCmdRet == 0)
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '设置单张读保护（不需EPC号）'指令返回=0x00" +
                       "(设置单张读保护（不需EPC号）成功)";
        }

        private void Button_RemoveReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("访问密码小于8位！请重新输入！", "信息提示");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.ResetPrivacy_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("ResetPrivacy_G2", "解除单张读保护（不需EPC号）", fCmdRet);
             if (fCmdRet == 0)
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '解除单张读保护（不需EPC号）'指令返回=0x00" +
                       "(解除单张读保护（不需EPC号）成功)";
        }

        private void Button_CheckReadProtected_G2_Click(object sender, EventArgs e)
        {
              byte readpro=2;
              fCmdRet=StaticClassReaderB.CheckPrivacy_G2(ref fComAdr,ref readpro,ref ferrorcode,frmcomportindex);
              AddCmdLog("CheckPrivacy_G2", "检测单张被读保护（不需要访问密码）", fCmdRet);
              if (fCmdRet == 0)
              {
                  if (readpro == 0)
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '检测单张被读保护（不需要访问密码）'指令返回=0x00" +
                           "(电子标签没有被设置为读保护";
                  if (readpro == 1)
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '检测单张被读保护（不需要访问密码）'指令返回=0x01" +
                           "(电子标签被设置读保护锁定)";
              }
        }

        private void Button_SetEASAlarm_G2_Click(object sender, EventArgs e)
        {
            byte  EAS=0;
            byte ENum;
            string str;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确！", "错误提示");
                    return;
                }
            }
            if (Edit_AccessCode5.Text.Length < 8)
            {
                MessageBox.Show("访问密码小于8位！请重新输入！", "信息提示");
                return;
            }
            if (ComboBox_EPC5.Items.Count == 0)
                return;
            if (ComboBox_EPC5.SelectedItem == null)
                return;
            str = ComboBox_EPC5.SelectedItem.ToString();
            if (str == "")
                return;
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum*2];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_AccessCode5.Text);
             if (Alarm_G2.Checked) 
             EAS= 1;
             else 
             EAS=0;
            fCmdRet = StaticClassReaderB.EASConfigure_G2(ref fComAdr, EPC, ENum, fPassWord, EAS, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("EASConfigure_G2", "报警设置", fCmdRet);     //v2.1 change
            if (fCmdRet == 0)
            {
                if (Alarm_G2.Checked)                                //v2.1 add
                    StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '报警设置'指令返回=0x00" +
                              "设置EAS报警 成功)";
                else
                    StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '报警设置'指令返回=0x00" +
                              "(清除EAS报警成功)";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Timer_G2_Alarm.Enabled = !Timer_G2_Alarm.Enabled;
            if (Timer_G2_Alarm.Enabled)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button2.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                button4.Text = "停止";
            }
            else
            {
                if (ListView1_EPC.Items.Count != 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = true;
                    Proect2.Enabled = true;
                    Always2.Enabled = true;
                    AlwaysNot2.Enabled = true;
                    P_Reserve.Enabled = true;
                    P_EPC.Enabled = true;
                    P_TID.Enabled = true;
                    P_User.Enabled = true;
                    Button_DestroyCard.Enabled = true;
                    Button_SetReadProtect_G2.Enabled = true;
                    Button_SetEASAlarm_G2.Enabled = true;
                    Alarm_G2.Enabled = true;
                    NoAlarm_G2.Enabled = true;
                    Button_LockUserBlock_G2.Enabled = true;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button2.Enabled = true;
                    Button_SetProtectState.Enabled = true;
                    SpeedButton_Read_G2.Enabled = true;
                
                        Button_DataWrite.Enabled = true;
                        BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                }
                if (ListView1_EPC.Items.Count == 0)
                {
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = false;
                    Proect2.Enabled = false;
                    Always2.Enabled = false;
                    AlwaysNot2.Enabled = false;
                    P_Reserve.Enabled = false;
                    P_EPC.Enabled = false;
                    P_TID.Enabled = false;
                    P_User.Enabled = false;
                    Button_DestroyCard.Enabled = false;
                    Button_SetReadProtect_G2.Enabled = false;
                    Button_SetEASAlarm_G2.Enabled = false;
                    Alarm_G2.Enabled = false;
                    NoAlarm_G2.Enabled = false;
                    Button_LockUserBlock_G2.Enabled = false;
                    SpeedButton_Read_G2.Enabled = false;
                    Button_DataWrite.Enabled = false;
                    BlockWrite.Enabled = false;
                    Button_BlockErase.Enabled = false;
                    Button_SetProtectState.Enabled = false;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button2.Enabled = true;

                }
                button4.Text = "检测EAS报警";
                Label_Alarm.Visible = false;                       //v2.1 add
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "  “检测EAS报警”结束";
            }
        }

        private void Timer_G2_Alarm_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                return;
            fIsInventoryScan = true;
             fCmdRet=StaticClassReaderB.EASAlarm_G2(ref fComAdr,ref ferrorcode,frmcomportindex);
            if (fCmdRet==0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "“检测EAS报警”指令返回=0x00" +
                          "(检测到EAS报警)";
                 Label_Alarm.Visible=true;                       //v2.1 add
            }
            else
            {
              Label_Alarm.Visible=false;                       //v2.1 add
              AddCmdLog("CheckEASAlarm_G2", "检测EAS报警", fCmdRet);
            }
            fIsInventoryScan = false;
            if (fAppClosed)
                Close();
        }

        private void Button_LockUserBlock_G2_Click(object sender, EventArgs e)
        {
             byte BlockNum = 0;
             byte ENum;
             string str;
             byte MaskMem = 0;
             byte[] MaskAdr = new byte[2];
             byte MaskLen = 0;
             byte[] MaskData = new byte[100];
             if (checkBox1.Checked)
             {
                 if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                 {
                     MessageBox.Show("掩码数据不正确！", "错误提示");
                     return;
                 }
             }
             if (Edit_AccessCode6.Text.Length < 8)
             {
                 MessageBox.Show("访问密码小于8位！请重新输入！", "信息提示");
                 return;
             }
             if (ComboBox_EPC6.Items.Count == 0)
                 return;
             if (ComboBox_EPC6.SelectedItem == null)
                 return;
             str = ComboBox_EPC6.SelectedItem.ToString();
             if (str == "")
                 return;
             if (checkBox1.Checked)
             {
                 ENum = 255;
                 if (R_EPC.Checked) MaskMem = 1;
                 if (R_TID.Checked) MaskMem = 2;
                 if (R_User.Checked) MaskMem = 3;
                 MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                 MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                 MaskData = HexStringToByteArray(maskData_textBox.Text);
             }
             else
             {
                 ENum = Convert.ToByte(str.Length / 4);
             }
             byte[] EPC = new byte[ENum*2];
             EPC = HexStringToByteArray(str);
             fPassWord = HexStringToByteArray(Edit_AccessCode6.Text);
             BlockNum=Convert.ToByte(ComboBox_BlockNum.SelectedIndex*2);
             fCmdRet=StaticClassReaderB.BlockLock_G2(ref fComAdr,EPC,ENum,fPassWord,BlockNum,MaskMem, MaskAdr, MaskLen, MaskData,ref ferrorcode,frmcomportindex);
             AddCmdLog("LockUserBlock_G2", "用户区数据块锁定", fCmdRet);
             if (fCmdRet == 0)
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “用户区数据块锁定”指令返回=0x00" +
                       "(锁定成功)";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            fAppClosed = true;
            StaticClassReaderB.CloseComPort();
            if (frmcomportindex>1023)
            StaticClassReaderB.CloseNetPort(frmcomportindex);
        }

        private void ComboBox_IntervalTime_SelectedIndexChanged(object sender, EventArgs e)
        {
             Timer_Test_.Interval =(ComboBox_IntervalTime.SelectedIndex+1)*10;
        }

        private void SpeedButton_Query_6B_Click(object sender, EventArgs e)
        {
            Timer_Test_6B.Enabled = !Timer_Test_6B.Enabled;
            if (!Timer_Test_6B.Enabled)
            {
                if (ListView_ID_6B.Items.Count != 0)
                {
                    SpeedButton_Read_6B.Enabled = true;
                    SpeedButton_Write_6B.Enabled = true;
                    Button14.Enabled = true;
                    Button15.Enabled = true;
                    if (Bycondition_6B.Checked)
                    {
                        Same_6B.Enabled = true;
                        Different_6B.Enabled = true;
                        Less_6B.Enabled = true;
                        Greater_6B.Enabled = true;
                    }
                }
                if (ListView_ID_6B.Items.Count == 0)
                {
                    SpeedButton_Read_6B.Enabled = false;
                    SpeedButton_Write_6B.Enabled = false;
                    Button14.Enabled = false;
                    Button15.Enabled = false;
                    if (Bycondition_6B.Checked)
                    {
                        Same_6B.Enabled = true;
                        Different_6B.Enabled = true;
                        Less_6B.Enabled = true;
                        Greater_6B.Enabled = true;
                    }
                }
                AddCmdLog("Inventory", "退出询查", 0);
                SpeedButton_Query_6B.Text = "单张查询 ";
            }
            else
            {
                SpeedButton_Read_6B.Enabled = false;
                SpeedButton_Write_6B.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;
                Same_6B.Enabled = false;
                Different_6B.Enabled = false;
                Less_6B.Enabled = false;
                Greater_6B.Enabled = false;
                ListView_ID_6B.Items.Clear();
                ComboBox_ID1_6B.Items.Clear();
                CardNum1 = 0;
                list.Clear();
                SpeedButton_Query_6B.Text = "停止";
            }
        }

        public void ChangeSubItem1(ListViewItem ListItem, int subItemIndex, string ItemText)
        {
            if (subItemIndex == 1)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[subItemIndex + 1].Text = "1";
                }
                else
                {
                    ListItem.SubItems[subItemIndex + 1].Text = Convert.ToString(Convert.ToUInt32(ListItem.SubItems[subItemIndex + 1].Text) + 1);
                    if ((Convert.ToUInt32(ListItem.SubItems[subItemIndex + 1].Text) > 9999))
                        ListItem.SubItems[subItemIndex + 1].Text = "1";
                }

             }
        }
        private void Inventory_6B()
        {
             int CardNum = 0 ;
             byte[] ID_6B=new byte[2000];
             byte[] ID2_6B=new byte[5000] ;
             bool isonlistview;
             string temps;
             string s,ss, sID;
             ListViewItem aListItem = new ListViewItem();
             int i, j;
             byte Condition=0;
             byte StartAddress;
             byte mask = 0;
             byte[] ConditionContent =new byte[300];
             byte Contentlen;
             if (Byone_6B.Checked)
             {
                fCmdRet = StaticClassReaderB.InventorySingle_6B(ref fComAdr, ID_6B, frmcomportindex);
                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[9];
                    Array.Copy(ID_6B, daw, 9);
                    temps = ByteArrayToHexString(daw);
                    temps = temps.Substring(2, 16);
                    if (!list.Contains(temps))
                    {
                        CardNum1 = CardNum1 + 1;
                        list.Add(temps);
                    }
                    while(ListView_ID_6B.Items.Count < CardNum1)
                    {
                        aListItem = ListView_ID_6B.Items.Add((ListView_ID_6B.Items.Count + 1).ToString());
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                    }
                     isonlistview = false;
                     for (i = 0; i < CardNum1; i++)     //判断是否在ListView列表内
                      {        
                        if (temps==ListView_ID_6B.Items[i].SubItems[1].Text)
                        {
                         aListItem = ListView_ID_6B.Items[i];
                         ChangeSubItem1(aListItem, 1, temps);
                         isonlistview=true;
                        }
                      }
                      if (!isonlistview)
                      {
                         // CardNum1 = Convert.ToByte(ListView_ID_6B.Items.Count+1);
                          aListItem = ListView_ID_6B.Items[CardNum1-1];
                          s = temps;
                          ChangeSubItem1(aListItem, 1, s);                        
                          if (ComboBox_EPC1.Items.IndexOf(s) == -1)
                          {                   
                             ComboBox_ID1_6B.Items.Add(temps);
                          }

                      }
                }

                 if (ComboBox_ID1_6B.Items.Count != 0)
                     ComboBox_ID1_6B.SelectedIndex = 0;
            }
            if (Bycondition_6B.Checked)
            {
                if (Same_6B.Checked)
                    Condition = 0;
                else if (Different_6B.Checked)
                    Condition = 1;
                else if (Greater_6B.Checked)
                    Condition = 2;
                else if (Less_6B.Checked)
                    Condition = 3;
                if (Edit_ConditionContent_6B.Text == "")
                    return;
                ss = Edit_ConditionContent_6B.Text;
                Contentlen = Convert.ToByte((Edit_ConditionContent_6B.Text).Length);
                for (i = 0; i < 16 - Contentlen; i++)
                    ss = ss + "0";
                int Nlen = (ss.Length) / 2;
                byte[] daw = new byte[Nlen];
                daw = HexStringToByteArray(ss);
                switch (Contentlen / 2)
                {
                    case 1:                                                                                                                                                                                           
                        mask = 0x80;
                        break;
                    case 2:
                        mask = 0xC0;
                        break;
                    case 3:
                        mask = 0xE0;
                        break;
                    case 4:
                        mask = 0xF0;
                        break;
                    case 5:
                        mask = 0xF8;
                        break;
                    case 6:
                        mask = 0xFC;
                        break;
                    case 7:
                        mask = 0xFE;
                        break;
                    case 8:
                        mask = 0xFF;
                        break;
                }
                if (Edit_Query_StartAddress_6B.Text == "")
                    return;
                StartAddress = Convert.ToByte(Edit_Query_StartAddress_6B.Text);
                fCmdRet = StaticClassReaderB.InventoryMultiple_6B(ref fComAdr, Condition, StartAddress, mask, daw, ID2_6B, ref CardNum, frmcomportindex);
                if ((fCmdRet == 0x15) | (fCmdRet == 0x16) | (fCmdRet == 0x17) | (fCmdRet == 0x18) | (fCmdRet == 0xFB))
                {
                    byte[] daw1 = new byte[CardNum * 9];
                    Array.Copy(ID2_6B, daw1, CardNum * 9);
                    temps = ByteArrayToHexString(daw1);
                    for (i = 0; i < CardNum; i++)
                    {
                        sID = temps.Substring(18*i+2,16);
                        if ((sID.Length) != 16)
                            return;
                        if (CardNum == 0)
                            return;
                        while (ListView_ID_6B.Items.Count < CardNum)
                        {
                            aListItem = ListView_ID_6B.Items.Add((ListView_ID_6B.Items.Count + 1).ToString());
                            aListItem.SubItems.Add("");
                            aListItem.SubItems.Add("");
                            aListItem.SubItems.Add("");
                        }
                        isonlistview = false;
                        for (j = 0;j< ListView_ID_6B.Items.Count; j++)     //判断是否在Listview列表内
                        {
                            if (sID == ListView_ID_6B.Items[j].SubItems[1].Text)
                            {
                                aListItem = ListView_ID_6B.Items[j];
                                ChangeSubItem1(aListItem, 1, sID);
                                isonlistview = true;
                            }
                        }
                        if (!isonlistview)
                        {
                            // CardNum1 = Convert.ToByte(ListView_ID_6B.Items.Count+1);
                            aListItem = ListView_ID_6B.Items[i];
                            s = sID;
                            ChangeSubItem1(aListItem, 1, s);
                            if (ComboBox_EPC1.Items.IndexOf(s) == -1)
                            {
                                ComboBox_ID1_6B.Items.Add(sID);
                            }
                        }
                    }
                    if (ComboBox_ID1_6B.Items.Count != 0)
                        ComboBox_ID1_6B.SelectedIndex = 0;
                }
            }
             if (Timer_Test_6B.Enabled)
             {
                 if (Bycondition_6B.Checked)
                 {
                     if (fCmdRet != 0)
                         AddCmdLog("Inventory", "查询标签", fCmdRet);
                 }
                 else if (fCmdRet == 0XFB) //说明还未将所有卡读取完
                 {

                     StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “查询标签”指令返回=0xFB" +
                          "(无电子标签可操作)";
                 }
                 else if (fCmdRet == 0)
                     StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “查询标签”指令返回=0x00" +
                          "(找到一张电子标签)";
                 else
                     AddCmdLog("Inventory", "查询标签", fCmdRet);
                 if (fCmdRet == 0xEE)
                     StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "查询标签”指令返回=0xee" +
                                   "(返回指令错误)";
             }
             if (fAppClosed)
                 Close();
        }
        private void Timer_Test_6B_Tick(object sender, EventArgs e)
        {
            if (fisinventoryscan_6B)
                return;
            fisinventoryscan_6B = true;
            Inventory_6B();
            fisinventoryscan_6B = false;
        }

        private void SpeedButton_Read_6B_Click(object sender, EventArgs e)
        {
             if (( Edit_StartAddress_6B.Text=="" )|( Edit_Len_6B.Text==""))
             {
                 MessageBox.Show("起始地址或长度为空!", "信息提示");
                 return;
             }
             Timer_6B_Read.Enabled = !Timer_6B_Read.Enabled;
             if (!Timer_6B_Read.Enabled)
             {
                 AddCmdLog("Read", "退出", 0);
                 SpeedButton_Read_6B.Text = "读 ";
                 SpeedButton_Query_6B.Enabled = true;
                 SpeedButton_Write_6B.Enabled = true;
                 Button14.Enabled = true;
                 Button15.Enabled = true;
                 if (Bycondition_6B.Checked)
                 {
                     Same_6B.Enabled = true;
                     Different_6B.Enabled = true;
                     Less_6B.Enabled = true;
                     Greater_6B.Enabled = true;
                 }
             }
             else
             {
                 SpeedButton_Query_6B.Enabled = false ;
                 SpeedButton_Write_6B.Enabled = false ;
                 Button14.Enabled = false;
                 Button15.Enabled = false;
                 if (Bycondition_6B.Checked)
                 {
                     Same_6B.Enabled = false;
                     Different_6B.Enabled = false;
                     Less_6B.Enabled = false;
                     Greater_6B.Enabled = false;
                 }
                 SpeedButton_Read_6B.Text = "停止";
             }
        }
        private void Read_6B()
        {
            string temp, temps;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte  Num, StartAddress;
            if (ComboBox_ID1_6B.Items.Count == 0)
                return;
            if (ComboBox_ID1_6B.SelectedItem == null)
                return;
            temp = ComboBox_ID1_6B.SelectedItem.ToString();
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            if (Edit_StartAddress_6B.Text == "")
                return;
            StartAddress = Convert.ToByte(Edit_StartAddress_6B.Text,16);
            if (Edit_Len_6B.Text == "")
                return;
            Num = Convert.ToByte(Edit_Len_6B.Text);
            fCmdRet = StaticClassReaderB.ReadData_6B(ref fComAdr, ID_6B, StartAddress, Num, CardData, ref ferrorcode, frmcomportindex);
            if (fCmdRet == 0)
            {
                byte[] data = new byte[Num];
                Array.Copy(CardData, data, Num);
                temps = ByteArrayToHexString(data);
                listBox2.Items.Add(temps);
            }
            if(fAppClosed )
                Close();
        }

        private void Timer_6B_Read_Tick(object sender, EventArgs e)
        {
            if (fTimer_6B_ReadWrite)
                return;
            fTimer_6B_ReadWrite = true;
            Read_6B();
            fTimer_6B_ReadWrite = false;
        }

        private void SpeedButton_Write_6B_Click(object sender, EventArgs e)
        {
            if (( Edit_WriteData_6B.Text=="" )| ((Edit_WriteData_6B.Text.Length% 2)!=0))
            {
                MessageBox.Show("请输入16进制数据!", "信息提示");
                return;
            }
            if ((Edit_StartAddress_6B.Text == "") | (Edit_Len_6B.Text == ""))
            {
                MessageBox.Show("起始地址为空", "信息提示");
                return;
            }
            Timer_6B_Write.Enabled = !Timer_6B_Write.Enabled;
            if (!Timer_6B_Write.Enabled)
            {
                AddCmdLog("写", "退出", 0);
                SpeedButton_Write_6B.Text = "写 ";
            }
            else
            {
                SpeedButton_Write_6B.Text = "停止";
            }
        }
        private void Write_6B()
        {
            string temp;
            byte[] CardData = new byte[320];
            byte[] ID_6B = new byte[8];
            byte  StartAddress;       
            byte Writedatalen;
            int writtenbyte=0;
            if (ComboBox_ID1_6B.Items.Count == 0)
                return;
            if (ComboBox_ID1_6B.SelectedItem == null)
                return;
            temp = ComboBox_ID1_6B.SelectedItem.ToString();
            if (temp == "")
                return;
            ID_6B = HexStringToByteArray(temp);
            if (Edit_StartAddress_6B.Text == "")
                return;
            StartAddress = Convert.ToByte(Edit_StartAddress_6B.Text,16);
            if ((Edit_WriteData_6B.Text == "") | (Edit_WriteData_6B.Text.Length%2)!=0)
                return;
            Writedatalen =Convert.ToByte(Edit_WriteData_6B.Text.Length / 2);
            byte[] Writedata = new byte[Writedatalen];
            Writedata = HexStringToByteArray(Edit_WriteData_6B.Text);
            fCmdRet=StaticClassReaderB.WriteData_6B(ref fComAdr,ID_6B,StartAddress,Writedata,Writedatalen,ref writtenbyte,ref ferrorcode,frmcomportindex);
            AddCmdLog("WriteCard", "写", fCmdRet);
              if (fAppClosed)
                  Close();
        }

        private void Timer_6B_Write_Tick(object sender, EventArgs e)
        {
            if (fTimer_6B_ReadWrite)
                return;
            fTimer_6B_ReadWrite = true;
            Write_6B();
            fTimer_6B_ReadWrite = false;
        }

        private void Button14_Click(object sender, EventArgs e)
        {
               byte Address;
               string temps;
               byte[] ID_6B = new byte[8];
               if (ComboBox_ID1_6B.Items.Count == 0)
                   return;
               if (ComboBox_ID1_6B.SelectedItem == null)
                   return;
               temps = ComboBox_ID1_6B.SelectedItem.ToString();
               if (temps == "")
                   return;
               ID_6B = HexStringToByteArray(temps);
               if (Edit_StartAddress_6B.Text == "")
                   return;
               Address = Convert.ToByte(Edit_StartAddress_6B.Text);
               if (MessageBox.Show(this, "确定要永久锁定该地址吗?", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                   return;
               fCmdRet = StaticClassReaderB.Lock_6B(ref fComAdr, ID_6B, Address, ref ferrorcode, frmcomportindex);
               AddCmdLog("LockByte_6B", "锁定", fCmdRet);
        }

        private void Button15_Click(object sender, EventArgs e)
        {
           byte Address,ReLockState=2;
           string temps;
           byte[] ID_6B = new byte[8];
           if (ComboBox_ID1_6B.Items.Count == 0)
               return;
           if (ComboBox_ID1_6B.SelectedItem == null)
               return;
           temps = ComboBox_ID1_6B.SelectedItem.ToString();
           if (temps == "")
               return;
           ID_6B = HexStringToByteArray(temps);
           if (Edit_StartAddress_6B.Text == "")
               return;
           Address = Convert.ToByte(Edit_StartAddress_6B.Text);
           fCmdRet=StaticClassReaderB.CheckLock_6B(ref fComAdr,ID_6B,Address,ref ReLockState,ref ferrorcode,frmcomportindex);
           AddCmdLog("CheckLock_6B", "检测锁定", fCmdRet);
           if (fCmdRet == 0)
           {
               if (ReLockState == 0)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “检测锁定”指令返回=0x00" +
                             "(该字节未被锁定)";
               if (ReLockState == 1)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "  “检测锁定”指令返回=0x01" +
                           "(该字节已经被锁定)";
           }
        }

        private void Button22_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        private void P_Reserve_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = true;
                AccessCode.Enabled = true;
                NoProect.Enabled = true;
                Proect.Enabled = true;
                Always.Enabled = true;
                AlwaysNot.Enabled = true;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
            }
        }

        private void P_EPC_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }
        
        private void P_TID_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count != 0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }

        private void P_User_CheckedChanged(object sender, EventArgs e)
        {
            if (ListView1_EPC.Items.Count!=0)
            {
                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = true;
                Proect2.Enabled = true;
                Always2.Enabled = true;
                AlwaysNot2.Enabled = true;
            }
        }

        private void Byone_6B_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_6B_Read.Enabled) & (!Timer_6B_Write.Enabled) & (!Timer_Test_6B.Enabled))
            {
                Same_6B.Enabled = false;
                Different_6B.Enabled = false;
                Less_6B.Enabled = false;
                Greater_6B.Enabled = false;
            }
        }

        private void Bycondition_6B_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_6B_Read.Enabled) &(!Timer_6B_Write.Enabled)&(!Timer_Test_6B.Enabled))
            {
                Same_6B.Enabled = true;
                Different_6B.Enabled = true;
                Less_6B.Enabled = true;
                Greater_6B.Enabled = true;
            }
        }

        private void C_EPC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                Edit_WordPtr.Text = "02";
                Edit_WordPtr.ReadOnly = true;
            }
            else
            {
                Edit_WordPtr.ReadOnly = false;
            }
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
            //    Button_DataWrite.Enabled = false;
            }
        }

        private void C_TID_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void C_User_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) & (!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void C_Reserve_CheckedChanged(object sender, EventArgs e)
        {
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
                if (ListView1_EPC.Items.Count != 0)
                    Button_DataWrite.Enabled = true;
            }
            Edit_WordPtr.ReadOnly = false;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
                timer1.Enabled = false;
                button10.Text = "获取";
                Timer_G2_Alarm.Enabled = false;
                Timer_G2_Read.Enabled = false;
                Timer_Test_.Enabled = false;
                SpeedButton_Read_G2.Text = "读";
                button2.Text = "查询标签";
                button4.Text = "检测报警";
                if ((ListView1_EPC.Items.Count != 0)&&(ComOpen))
                {
                    button2.Enabled = true;
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = true;
                    Proect2.Enabled = true;
                    Always2.Enabled = true;
                    AlwaysNot2.Enabled = true;
                    P_Reserve.Enabled = true;
                    P_EPC.Enabled = true;
                    P_TID.Enabled = true;
                    P_User.Enabled = true;
                    Button_DestroyCard.Enabled = true;
                    Button_SetReadProtect_G2.Enabled = true;
                    Button_SetEASAlarm_G2.Enabled = true;
                    Alarm_G2.Enabled = true;
                    NoAlarm_G2.Enabled = true;
                    Button_LockUserBlock_G2.Enabled = true;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    SpeedButton_Read_G2.Enabled = true;
                    Button_SetProtectState.Enabled = true;
                    Button_DataWrite.Enabled = true;
                    BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                }
                if ((ListView1_EPC.Items.Count == 0)&&(ComOpen))
                {
                    button2.Enabled = true;
                    DestroyCode.Enabled = false;
                    AccessCode.Enabled = false;
                    NoProect.Enabled = false;
                    Proect.Enabled = false;
                    Always.Enabled = false;
                    AlwaysNot.Enabled = false;
                    NoProect2.Enabled = false;
                    Proect2.Enabled = false;
                    Always2.Enabled = false;
                    AlwaysNot2.Enabled = false;
                    P_Reserve.Enabled = false;
                    P_EPC.Enabled = false;
                    P_TID.Enabled = false;
                    P_User.Enabled = false;
                    Button_DestroyCard.Enabled = false;
                    Button_SetReadProtect_G2.Enabled = false;
                    Button_SetEASAlarm_G2.Enabled = false;
                    Alarm_G2.Enabled = false;
                    NoAlarm_G2.Enabled = false;
                    Button_LockUserBlock_G2.Enabled = false;
                    SpeedButton_Read_G2.Enabled = false;
                    Button_DataWrite.Enabled = false;
                    BlockWrite.Enabled = false;
                    Button_BlockErase.Enabled = false;
                    Button_WriteEPC_G2.Enabled = true;
                    Button_SetMultiReadProtect_G2.Enabled = true;
                    Button_RemoveReadProtect_G2.Enabled = true;
                    Button_CheckReadProtected_G2.Enabled = true;
                    button4.Enabled = true;
                    Button_SetProtectState.Enabled = false;
                }

                Timer_Test_6B.Enabled = false;
                Timer_6B_Read.Enabled = false;
                Timer_6B_Write.Enabled = false;
                SpeedButton_Query_6B.Text = "单张查询";
                SpeedButton_Read_6B.Text = "读";
                SpeedButton_Write_6B.Text ="写";
                if ((ListView_ID_6B.Items.Count != 0)&&(ComOpen))
                {
                    SpeedButton_Query_6B.Enabled = true;
                    SpeedButton_Read_6B.Enabled = true;
                    SpeedButton_Write_6B.Enabled = true;
                    Button14.Enabled = true;
                    Button15.Enabled = true;
                    if (Bycondition_6B.Checked)
                    {
                        Same_6B.Enabled = true;
                        Different_6B.Enabled = true;
                        Less_6B.Enabled = true;
                        Greater_6B.Enabled = true;
                    }
                }
                if ((ListView_ID_6B.Items.Count == 0)&&(ComOpen))
                {
                    SpeedButton_Query_6B.Enabled = true;
                    SpeedButton_Read_6B.Enabled = false;
                    SpeedButton_Write_6B.Enabled = false;
                    Button14.Enabled = false;
                    Button15.Enabled = false;
                    if (Bycondition_6B.Checked)
                    {
                        Same_6B.Enabled = true;
                        Different_6B.Enabled = true;
                        Less_6B.Enabled = true;
                        Greater_6B.Enabled = true;
                    }
                }

                breakflag = true;
                button13.Enabled = ComOpen;
                button16.Enabled = false;
                if ((comboBox2.SelectedIndex == 0) && (ComOpen == true))
                {
                    button20.Enabled = true;
                    button21.Enabled = true;
                }
                else
                {
                    button20.Enabled = false;
                    button21.Enabled = false;
                }
            //if((tabControl1.SelectedIndex==6)&&(ComOpen == true))
            //{
            //    button27_Click(sender,e);
            //    Thread.Sleep(200);
            //    if(SeriaATflag)
            //    {
            //        SeriaATflag = false;
            //        return;
            //    }
            //    button34_Click(sender, e);
            //    Thread.Sleep(100);
            //    if (SeriaATflag)
            //    {
            //        SeriaATflag = false;
            //        return;
            //    }
            //    button35_Click(sender, e);
            //    Thread.Sleep(100);
            //    if (SeriaATflag)
            //    {
            //        SeriaATflag = false;
            //        return;
            //    }
            //    button36_Click(sender, e);

            //}    
        }

        private void Edit_CmdComAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void Edit_Len_6B_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void GetData()
        {
              byte[] ScanModeData = new byte[40960];
              int ValidDatalength,i;
              string temp, temps;
              ValidDatalength = 0;
              fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ref ValidDatalength, frmcomportindex);
              if (fCmdRet == 0)
              { 
                temp="";
                temps=ByteArrayToHexString(ScanModeData);
                for(i=0;i<ValidDatalength;i++)
                {
                    temp = temp + temps.Substring(i * 2, 2) + " ";
                }
                listBox3.Items.Add(temp);
                listBox3.SelectedIndex = listBox3.Items.Count-1;
              }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (fIsInventoryScan)
                fIsInventoryScan = true;
            GetData();
            if (fAppClosed)
                Close();
            fIsInventoryScan = false;
        }


        private void radioButton_band1_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 63; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 62;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band2_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 20; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 19;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band3_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 50; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 49;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void radioButton_band4_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 32; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 31;
            ComboBox_dminfre.SelectedIndex = 0;
        } 

        private void ComboBox_COM_SelectedIndexChanged(object sender, EventArgs e)
        {
              ComboBox_baud2.Items.Clear();
              if(ComboBox_COM.SelectedIndex==0)
              {
                  ComboBox_baud2.Items.Add("9600bps");
                  ComboBox_baud2.Items.Add("19200bps");
                  ComboBox_baud2.Items.Add("38400bps");
                  ComboBox_baud2.Items.Add("57600bps");
                  ComboBox_baud2.Items.Add("115200bps");
                  ComboBox_baud2.SelectedIndex=3;
              }
              else
              {
                  ComboBox_baud2.Items.Add("Auto");
                  ComboBox_baud2.SelectedIndex=0;
              }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Byte dminfre, dmaxfre, Ffenpin;
            int i, j, CardNum, Totallen, UID_index, n_index;
            byte[] EPC = new byte[5000];
            string temp1, temp2, temp3, temp4;
            float ncount;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            byte MaskFlag = 0;
            byte Ant = 0;
            byte AdrTID = 0;
            byte LenTID = 0;
            byte TIDFlag = 0;
            button13.Enabled = false;
            button16.Enabled = true;
            listBox4.Items.Clear();
            breakflag = false;
            for (Ffenpin = 0; Ffenpin < 63; Ffenpin++)
            {
                if (breakflag == true)
                {
                    breakflag = false;
                    if (fAppClosed)
                        Close();
                    return;
                }
                dmaxfre = Ffenpin;
                dminfre = Ffenpin;
                y_f = Convert.ToDouble(902.6 + (Ffenpin & 0x3F) * 0.4);
                temp4 = Convert.ToString(y_f);
                temp3 = temp4.PadRight(5, ' ') + "MHz" + "(" + Convert.ToString(Ffenpin).PadLeft(2, ' ') + ")";
                // ListBox1.Items.Add(Format('%-4d',[Ffenpin]));
                listBox4.Items.Add(temp3);
                for (i = 0; i < 4; i++)
                {
                    fCmdRet = StaticClassReaderB.SetRegion(ref fComAdr, dmaxfre, dminfre, frmcomportindex);
                    if (fCmdRet == 0)
                        break;
                }
                ncount = 0;
                for (j = 0; j < 30; j++)
                {
                    Application.DoEvents();
                    if (breakflag)
                    {
                        breakflag = false;
                        if (fAppClosed)
                        {
                            Close();
                        }
                        return;
                    }
                    CardNum = 0;
                    Totallen = 0;
                    fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, EPC, ref Ant, ref Totallen, ref CardNum, frmcomportindex); 
                    if ((fCmdRet == 1) || (fCmdRet == 2) || (fCmdRet == 3) || (fCmdRet == 4))
                    {
                        ncount = ncount + 1;
                        if (ncount == 1)
                            UID_index = listBox4.Items.IndexOf(temp3);
                        else
                            UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount - 1).PadLeft(2, ' ') + "/30");
                        if (UID_index >= 0)
                        {
                            listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30";
                        }
                    }
                }
                if (ncount == 0)
                {
                    UID_index = listBox4.Items.IndexOf(temp3);
                    if (UID_index >= 0)
                        listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30" + "                                " + "00.00%";
                }
                UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30");
                if (UID_index >= 0)
                {
                    x_z = ((ncount / 30) * 100);
                    temp1 = Convert.ToString(x_z);
                    if (ncount == 30)
                        temp2 = "100.00%";
                    else
                    {
                        n_index = temp1.IndexOf('.');
                        //temp2:=Copy(temp1,1,2)+'.'+copy(temp1,3,2)+'%';
                        if (n_index > 0)
                            temp2 = temp1.Substring(0, n_index) + "." + temp1.Substring(n_index + 1, 2) + "%";
                        else
                            temp2 = temp1 + "." + "00" + "%";
                        // temp2:=Copy(temp1,1,2)+'.'+copy(temp1,3,2)+'%';
                    }
                    listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30" + "                                " + temp2;
                }
                listBox4.SelectedIndex = listBox4.Items.Count - 1;
            }
            button13.Enabled = true;
            button16.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            breakflag = true;
            button13.Enabled = true;
            button16.Enabled = false;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
        }

        private void BlockWrite_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Mem = 0;
            byte WNum = 0;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if (checkBox1.Checked)
            {
                if ((maskadr_textbox.Text.Length != 4) || (maskLen_textBox.Text.Length != 2) || (maskData_textBox.Text.Length % 2 != 0) && (maskData_textBox.Text.Length == 0))
                {
                    MessageBox.Show("掩码数据不正确", "信息");
                    return;
                }
            }
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (checkBox1.Checked)
            {
                ENum = 255;
                if (R_EPC.Checked) MaskMem = 1;
                if (R_TID.Checked) MaskMem = 2;
                if (R_User.Checked) MaskMem = 3;
                MaskAdr = HexStringToByteArray(maskadr_textbox.Text);
                MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
                MaskData = HexStringToByteArray(maskData_textBox.Text);
            }
            else
            {
                ENum = Convert.ToByte(str.Length / 4);
            }
            byte[] EPC = new byte[ENum * 2];
            EPC = HexStringToByteArray(str);
            if (C_Reserve.Checked)
                Mem = 0;
            if (C_EPC.Checked)
                Mem = 1;
            if (C_TID.Checked)
                Mem = 2;
            if (C_User.Checked)
                Mem = 3;
            if (Edit_WordPtr.Text == "")
            {
                MessageBox.Show("起始地址为空", "信息提示");
                return;
            }
            

            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            if (Edit_WriteData.Text == "")
                return;
            s2 = Edit_WriteData.Text;
            if (s2.Length % 4 != 0)
            {
                MessageBox.Show("以字为单位输入.", "写");
                return;
            }
            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2+1];
            Writedata = HexStringToByteArray(s2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                WNum = Convert.ToByte(s2.Length / 4 + 1);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.BlockWrite_G2(ref fComAdr, EPC, WNum, ENum, Mem, WordPtr, Writedata, fPassWord, MaskMem, MaskAdr, MaskLen, MaskData, ref ferrorcode, frmcomportindex);
            AddCmdLog("Block Write", "块写", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'块写'命令 返回=0x00" +
                     "(块写成功)";
            }    
        }
  
        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
              int i;
              ComboBox_dminfre.Items.Clear();
              ComboBox_dmaxfre.Items.Clear();
             for (i=0;i<15;i++)
             {
                 ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
                 ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
             }
             ComboBox_dmaxfre.SelectedIndex = 14;
             ComboBox_dminfre.SelectedIndex=0;
        }
        
        private void Button_SetGPIO_Click(object sender, EventArgs e)
        {
              byte OutputPin=0;
              if(checkBox2.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x01);
              if(checkBox3.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x02);
              if(checkBox4.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x04);
              if(checkBox5.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x08);
              if (checkBox6.Checked)
              OutputPin = Convert.ToByte(OutputPin | 0x10);
              if(checkBox7.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x20);
              if(checkBox8.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x40);
              if(checkBox9.Checked)
              OutputPin=Convert.ToByte(OutputPin | 0x80);
              fCmdRet = StaticClassReaderB.SetGPIO(ref fComAdr, OutputPin, frmcomportindex);
              AddCmdLog("SetGPIO", "设置", fCmdRet);
        }

        private void Button_GetGPIO_Click(object sender, EventArgs e)
        {
              byte OutputPin=0;
              fCmdRet=StaticClassReaderB.GetGPIOStatus(ref fComAdr,ref OutputPin,frmcomportindex);
              if(fCmdRet==0)
              {
                   if((OutputPin & 0x01)==1)
                   checkBox2.Checked=true;
                   else
                   checkBox2.Checked=false;

                   if((OutputPin & 0x02)==2)
                   checkBox3.Checked=true;
                   else
                   checkBox3.Checked=false;
                   
                   if((OutputPin & 0x04)==4)
                   checkBox4.Checked=true;
                   else
                   checkBox4.Checked=false;

                   if((OutputPin & 0x08)==8)
                   checkBox5.Checked=true;
                   else
                   checkBox5.Checked=false;
                   
                   if((OutputPin & 0x10)==0x10)
                   checkBox6.Checked=true;  
                   else
                   checkBox6.Checked=false;

                   if((OutputPin & 0x20)==0x20)
                   checkBox7.Checked=true;
                   else
                   checkBox7.Checked=false;
               
                   if ((OutputPin & 0x40) == 0x40)
                   checkBox8.Checked = true;
                   else
                   checkBox8.Checked = false;

                   if ((OutputPin & 0x80) == 0x80)
                       checkBox9.Checked = true;
                   else
                       checkBox9.Checked = false;
               }
               AddCmdLog("GetGPIOStatus", "读取", fCmdRet);
        }

        private void Button_Ant_Click(object sender, EventArgs e)
        {
            byte  ANT=0;
            if(checkBox10.Checked) ANT=Convert.ToByte(ANT | 1);
            if(checkBox11.Checked) ANT=Convert.ToByte(ANT | 2);
            if(checkBox12.Checked) ANT=Convert.ToByte(ANT | 4);
            if(checkBox13.Checked) ANT=Convert.ToByte(ANT | 8);
            fCmdRet=StaticClassReaderB.SetAntennaMultiplexing(ref fComAdr,ANT,frmcomportindex);
            AddCmdLog("SetAntenna", "天线设置", fCmdRet);
        }

        private void Button_RelayTime_Click(object sender, EventArgs e)
        {   
              byte RelayTime=0;
              RelayTime=Convert.ToByte(ComboBox_RelayTime.SelectedIndex);
              fCmdRet = StaticClassReaderB.SetRelay(ref fComAdr, RelayTime, frmcomportindex);
              AddCmdLog("SetRelay", "闭合", fCmdRet);
        }

        private void ClockCMD_Click(object sender, EventArgs e)
        {
             byte[] SetTime=new byte[6] ;
             byte[]  CurrentTime=new byte[6];
             if (SetClock.Checked)
             {
                   if((Text_year.Text == "") || (Text_month.Text == "")||(Text_day.Text == "")||(Text_hour.Text =="")||(Text_min.Text =="")||(Text_sec.Text ==""))
                   {
                       MessageBox.Show("请输入数据", "信息提示");
                        return;
                   }
                      SetTime[0] = Convert.ToByte(Text_year.Text) ;       //需要设置的时间
                      SetTime[1] = Convert.ToByte(Text_month.Text) ; 
                      SetTime[2] = Convert.ToByte(Text_day.Text) ; 
                      SetTime[3] = Convert.ToByte(Text_hour.Text) ; 
                      SetTime[4] = Convert.ToByte(Text_min.Text) ; 
                      SetTime[5] = Convert.ToByte(Text_sec.Text) ; 

                    if ((Convert.ToByte(Text_year.Text) < 0)||(Convert.ToByte(Text_year.Text) > 0x63))
                    {
                        MessageBox.Show("请输入00-99之间的任一数值", "信息提示");
                      return;
                    }
                    fCmdRet = StaticClassReaderB.SetReal_timeClock(ref fComAdr, SetTime,frmcomportindex);
                    AddCmdLog("SetClock", "设置实时时钟", fCmdRet);
              }
              else
              {
                fCmdRet = StaticClassReaderB.GetTime(ref fComAdr, CurrentTime,frmcomportindex);
                if (fCmdRet==0)
                {
                    Text_year.Text= Convert.ToString(CurrentTime[0]).PadLeft(2,'0');
                    Text_month.Text = Convert.ToString(CurrentTime[1]).PadLeft(2, '0');
                    Text_day.Text = Convert.ToString(CurrentTime[2]).PadLeft(2, '0');
                    Text_hour.Text = Convert.ToString(CurrentTime[3]).PadLeft(2, '0');
                    Text_min.Text = Convert.ToString(CurrentTime[4]).PadLeft(2, '0');
                    Text_sec.Text = Convert.ToString(CurrentTime[5]).PadLeft(2, '0');                     
                }
                AddCmdLog("GetClock", "查询实时时钟", fCmdRet);
              }
        }

        private void Button_OutputRep_Click(object sender, EventArgs e)
        {
              byte OutputRep=0;
              if(checkBox17.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x01);
              if(checkBox16.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x02);
              if(checkBox15.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x04);
              if(checkBox14.Checked)
              OutputRep=Convert.ToByte(OutputRep | 0x08);
              fCmdRet=StaticClassReaderB.SetNotificationPulseOutput(ref fComAdr,OutputRep,frmcomportindex);
              AddCmdLog("SetNotificationPulseOutput", "设置", fCmdRet);
        }

        private void Button_Beep_Click(object sender, EventArgs e)
        {
             byte BeepEn=0;
             if(Radio_beepEn.Checked)
              BeepEn=1;
             else
              BeepEn=0;
             fCmdRet=StaticClassReaderB.SetBeepNotification(ref fComAdr,BeepEn,frmcomportindex);
             AddCmdLog("SetBeepNotification", "设置", fCmdRet);
        }

        private void Button_Accuracy_Click(object sender, EventArgs e)
        {
              byte Accuracy=Convert.ToByte(ComboBox_Accuracy.SelectedIndex);
              if(radioButton3.Checked)
              Accuracy=Convert.ToByte(Accuracy | 0x80) ;
              fCmdRet=StaticClassReaderB.SetEASSensitivity(ref fComAdr,Accuracy,frmcomportindex);
              AddCmdLog("SetEASSensitivity", "设置", fCmdRet);
        }

        private void button6_Click(object sender, EventArgs e)
        {
              byte MaskMem=0;
              byte[] MaskAdr=new byte[2];
              byte MaskLen=0;
              byte[] MaskData=new byte[100];
              if ((textBox3.Text.Length != 4) || (textBox4.Text.Length != 2) || (textBox6.Text.Length % 2 != 0))
              {
                  MessageBox.Show("请输入正确的数据！", "提示");
                  return;
              }
               if(radioButton5.Checked) MaskMem=1;
               if(radioButton6.Checked) MaskMem=2;
               if(radioButton7.Checked) MaskMem=3;
               MaskAdr=HexStringToByteArray(textBox3.Text.Trim());
               MaskLen=Convert.ToByte(textBox4.Text,16);
               MaskData=HexStringToByteArray(textBox6.Text.Trim());
               fCmdRet=StaticClassReaderB.SetMask(ref fComAdr,MaskMem,MaskAdr,MaskLen,MaskData,frmcomportindex);
               AddCmdLog("SetMask", "掩码设置", fCmdRet);
        }

        private void button8_Click(object sender, EventArgs e)
        {
              byte RepCondition=0;
              byte RepPauseTime=0;
              RepCondition=Convert.ToByte(comboBox2.SelectedIndex);
              RepPauseTime = Convert.ToByte(comboBox3.SelectedIndex);
              fCmdRet = StaticClassReaderB.SetResponsePamametersofAuto_runningMode(ref fComAdr, RepCondition, RepPauseTime,frmcomportindex);
              AddCmdLog("SetResponsePamametersofAuto_runningMode", "设置", fCmdRet);
        }
        
        private void button9_Click(object sender, EventArgs e)
        {
               byte Protocol = 0;
               if(radioButton8.Checked)
               {
                 if(radioButton10.Checked) Protocol=0;
                 if(radioButton11.Checked) Protocol=1;
                 if(radioButton12.Checked) Protocol=2;
                 if (radioButton13.Checked) Protocol = 0x10;
                 if (radioButton14.Checked) Protocol = 0x11;
               }
               else
               {
                 Protocol=0x80;
               }
               fCmdRet=StaticClassReaderB.SelectTagType(ref fComAdr,Protocol,frmcomportindex);
               AddCmdLog("SelectTagType", "设置", fCmdRet);
        }

        private void button12_Click(object sender, EventArgs e)
        {
              byte ReadPauseTim=0;
              ReadPauseTim=Convert.ToByte(comboBox4.SelectedIndex);
              fCmdRet=StaticClassReaderB.SetInventoryInterval(ref fComAdr,ReadPauseTim,frmcomportindex);
              AddCmdLog("SetInventoryInterval", "设置", fCmdRet);
        }

        private void button18_Click(object sender, EventArgs e)
        {
              byte Read_mode=0;
              Read_mode=Convert.ToByte(comboBox5.SelectedIndex);
              fCmdRet=StaticClassReaderB.SetWorkMode(ref fComAdr,Read_mode,frmcomportindex);
              AddCmdLog("SetWorkMode", "设置", fCmdRet);
        }

        private void button19_Click(object sender, EventArgs e)
        {
              byte Read_mode=0;
              byte Accuracy=0;
              byte RepCondition=0;
              byte RepPauseTime=0;
              byte ReadPauseTim=0;
              byte TagProtocol = 0;
              byte MaskMem=0;
              byte[] MaskAdr=new byte[2];
              byte[] MaskData=new byte[100];
              byte MaskLen=0;
              byte TriggerTime=0;
			  byte AdrTID=0;
              byte LenTID = 0;
              int i,m_byte;
              string temp;
              temp="";
              textBox3.Text="";
              textBox4.Text="";
              textBox6.Text="";
              textBox10.Text = "";
              textBox11.Text = "";
              fCmdRet = StaticClassReaderB.GetSystemParameter(ref fComAdr, ref Read_mode, ref Accuracy, ref RepCondition, ref RepPauseTime, ref ReadPauseTim, ref TagProtocol, ref MaskMem, MaskAdr, ref MaskLen, MaskData, ref TriggerTime, ref AdrTID, ref LenTID ,frmcomportindex);
              if(fCmdRet==0)
              {
                comboBox5.SelectedIndex=Convert.ToInt32(Read_mode);
                if((Accuracy & 0x80)==0x80)
                radioButton3.Checked=true;
                else
                radioButton4.Checked=true;
                ComboBox_Accuracy.SelectedIndex=Convert.ToInt32(Accuracy & 0x3F);
                comboBox2.SelectedIndex = RepCondition;
                comboBox3.SelectedIndex = RepPauseTime;
                if(comboBox2.SelectedIndex==1)
                comboBox3.Enabled=true;
                else
                comboBox3.Enabled=false;
                comboBox4.SelectedIndex=Convert.ToInt32(ReadPauseTim);
                switch (TagProtocol)
                { 
                    case 0:
                     radioButton8.Checked=true;
                     radioButton10.Checked=true;
                     break;
                    case 1:
                     radioButton8.Checked=true;
                     radioButton11.Checked=true;
                     break;
                    case 2:
                     radioButton8.Checked=true;
                     radioButton12.Checked=true;
                     break;
                    case 0x10:
                     radioButton8.Checked = true;
                     radioButton13.Checked = true;
                     break;
                    case 0x11:
                     radioButton8.Checked = true;
                     radioButton14.Checked = true;
                     break;
                    case 0x80:
                     radioButton9.Checked=true;
                     break; 
                }
                switch (MaskMem)
                {
                   case 1:
                        radioButton5.Checked=true;
                        break;
                   case 2:
                        radioButton6.Checked=true;
                       break;
                   case 3: 
                        radioButton7.Checked=true;
                        break;
                }
                 for (i=0;i<2;i++)
                    temp = temp + Convert.ToString(MaskAdr[i],16).PadLeft(2,'0');
                 textBox3.Text=temp;
                 textBox4.Text=Convert.ToString(MaskLen,16).PadLeft(2,'0');
                 temp="";
                 if ((MaskLen % 8) == 0)
                     m_byte = MaskLen / 8;
                 else
                     m_byte = MaskLen / 8 + 1;
                 for(i=0;i<m_byte;i++)
                    temp = temp +  Convert.ToString(MaskData[i],16).PadLeft(2,'0');
                textBox6.Text=temp;
                comboBox1.SelectedIndex = TriggerTime;
                textBox10.Text = Convert.ToString(AdrTID,16).PadLeft(2,'0');
                textBox11.Text = Convert.ToString(LenTID,16).PadLeft(2, '0');
              }
              AddCmdLog("GetSystemParameter", "读取主动工作模式参数", fCmdRet);
        }
        
        private void button21_Click(object sender, EventArgs e)
        {
            fCmdRet = StaticClassReaderB.ClearTagBuffer(ref fComAdr, frmcomportindex);
            AddCmdLog("ClearTagBuffer", "清缓存", fCmdRet);
        }
        
        private void button20_Click(object sender, EventArgs e)
        {
              byte[] Data=new byte[8000];
              int dataLength=0;
              int nLen,NumLen;
              string temp="";
              string temp1="";
              string syear="";
              string smonth="";
              string sday="";
              string shour="";
              string smin="";
              string ssec="";
              string Lyear="";
              string Lmonth="";
              string Lday="";
              string Lhour="";
              string Lmin="";
              string Lsec="";
              string binarystr1="";
              string binarystr2="";
              string CountStr="";
              string AntStr="";
              string EPCStr="";
              listView1.Items.Clear();
              ListViewItem aListItem = new ListViewItem();
              fCmdRet=StaticClassReaderB.GetTagBufferInfo(ref fComAdr,Data,ref dataLength,frmcomportindex);
              if(fCmdRet==0)
              { 
                nLen= dataLength*2;
                temp=ByteArrayToHexString(Data);
                while(nLen>0)
                {
                  NumLen=24+Convert.ToInt32(temp.Substring(22,2),16)*2;          
                  temp1=temp.Substring(0,NumLen);
                  binarystr1 = Convert.ToString(Convert.ToInt32(temp1.Substring(0, 8),16),2).PadLeft(32,'0');
                  syear = Convert.ToString(Convert.ToInt32(binarystr1.Substring(0, 6), 2)).PadLeft(2, '0');
                  smonth = Convert.ToString(Convert.ToInt32(binarystr1.Substring(6, 4), 2)).PadLeft(2, '0');
                  sday = Convert.ToString(Convert.ToInt32(binarystr1.Substring(10, 5), 2)).PadLeft(2, '0');
                  shour = Convert.ToString(Convert.ToInt32(binarystr1.Substring(15, 5), 2)).PadLeft(2, '0');
                  smin = Convert.ToString(Convert.ToInt32(binarystr1.Substring(20, 6), 2)).PadLeft(2, '0');
                  ssec = Convert.ToString(Convert.ToInt32(binarystr1.Substring(26, 6), 2)).PadLeft(2, '0');
                  
                  binarystr2 = Convert.ToString(Convert.ToInt32(temp1.Substring(8, 8),16), 2).PadLeft(32,'0');
                  Lyear = Convert.ToString(Convert.ToInt32(binarystr2.Substring(0, 6),2)).PadLeft(2, '0');
                  Lmonth = Convert.ToString(Convert.ToInt32(binarystr2.Substring(6, 4),2)).PadLeft(2, '0');
                  Lday  = Convert.ToString(Convert.ToInt32(binarystr2.Substring(10, 5),2)).PadLeft(2, '0');
                  Lhour = Convert.ToString(Convert.ToInt32(binarystr2.Substring(15, 5),2)).PadLeft(2, '0');
                  Lmin = Convert.ToString(Convert.ToInt32(binarystr2.Substring(20, 6),2)).PadLeft(2, '0');
                  Lsec = Convert.ToString(Convert.ToInt32(binarystr2.Substring(26, 6),2)).PadLeft(2, '0');
                  
                  CountStr = Convert.ToString(Convert.ToInt32(temp1.Substring(16,4),16), 10);
                  
                  AntStr = Convert.ToString(Convert.ToInt32(temp1.Substring(20,2),16),2).PadLeft(4,'0');
                  EPCStr=temp1.Substring(24,temp1.Length-24);
                  aListItem = listView1.Items.Add((listView1.Items.Count + 1).ToString());
                  aListItem.SubItems.Add("");
                  aListItem.SubItems.Add("");
                  aListItem.SubItems.Add("");
                  aListItem.SubItems.Add("");
                  aListItem.SubItems.Add("");
                  aListItem.SubItems[1].Text = EPCStr;
                  aListItem.SubItems[2].Text = "20" + syear + "-" + smonth + "-" + sday + " " + shour + ":" + smin + ":" + ssec;
                  aListItem.SubItems[3].Text = "20" + Lyear + "-" + Lmonth + "-" + Lday + " " + Lhour + ":" + Lmin + ":" + Lsec;
                  aListItem.SubItems[4].Text = AntStr;
                  aListItem.SubItems[5].Text = CountStr;
                  if ((temp.Length- NumLen )> 0)
                      temp = temp.Substring(NumLen, temp.Length - NumLen);
                  nLen=nLen-NumLen;
                  listView1.EnsureVisible(listView1.Items.Count-1);
                }
              }
              AddCmdLog("Get Tag Buffer Info", "获取存储块标签信息", fCmdRet);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                button10.Text = "获取";
            }
            else
            {
                button10.Text = "停止";
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            radioButton10.Enabled = false;
            radioButton11.Enabled = false;
            radioButton12.Enabled = false;
            radioButton13.Enabled = false;
            radioButton14.Enabled = false;

        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            radioButton10.Enabled = true;
            radioButton11.Enabled = true;
            radioButton12.Enabled = true;
            radioButton13.Enabled = true;
            radioButton14.Enabled = true;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 1)
                comboBox3.Enabled = true;
            else
                comboBox3.Enabled = false;
            if ((comboBox2.SelectedIndex == 0)&&(ComOpen==true))
            {
                button20.Enabled = true;
                button21.Enabled = true;
            }
            else
            {
                button20.Enabled = false;
                button21.Enabled = false;
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskadr_textbox.Enabled = true;
                maskLen_textBox.Enabled = true;
                maskData_textBox.Enabled = true;
                R_EPC.Enabled = true;
                R_TID.Enabled = true;
                R_User.Enabled = true;
            }
            else
            {
                maskadr_textbox.Enabled = false;
                maskLen_textBox.Enabled = false;
                maskData_textBox.Enabled = false;
                R_EPC.Enabled = false;
                R_TID.Enabled = false;
                R_User.Enabled = false;
            }
        }
        /// <summary>
        /// 将Device List中所记录设备显示至DeviceListView控件;
        /// </summary>
        private void ReflashDeviceListView(List<DeviceClass> deviceList)
        {
            this.DeviceListView.Items.Clear();

            foreach (DeviceClass device in deviceList)
            {
                IPAddress ipAddr = getIPAddress(device.DeviceIP);
                ListViewItem deviceListViewItem = new ListViewItem(new string[] { device.DeviceName, ipAddr.ToString(), device.DeviceMac });
                deviceListViewItem.ImageIndex = 0;
                this.DeviceListView.Items.Add(deviceListViewItem);
            }
        }

        /// <summary>
        /// 将Device List中所记录设备显示至DeviceListView控件;
        /// </summary>
        private void ClearDeviceListView()
        {
            DevControl.tagErrorCode eCode;
            List<DeviceClass> deviceList = DevList;

            foreach (DeviceClass device in deviceList)
            {
                eCode = DevControl.DM_FreeDevice(device.DevHandle);
                Debug.Assert(eCode == DevControl.tagErrorCode.DM_ERR_OK);
            }

            //清空设备列表，并清空对应显示控件；
            DevList.Clear();
            ReflashDeviceListView(DevList);
        }

        /// <summary>
        /// 搜索设备，然后将记录搜索结果的DevList显示至DeviceListView控件;
        /// </summary>
        private bool SearchDevice(uint targetIP)
        {
            ClearDeviceListView();

            DevControl.tagErrorCode eCode = DevControl.DM_SearchDevice(targetIP, 1500);
            if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
            {
                ReflashDeviceListView(DevList);
                return true;
            }
            else
            {
                //异常处理；
                string errMsg = ErrorHandling.HandleError(eCode);
                System.Windows.Forms.MessageBox.Show(errMsg);
                return false;
            }
        }

        /// <summary>
        /// 配置选定设备，开启对应配置窗体;
        /// </summary>
        private void ConfigSelectedDevice()
        {
            if (this.DeviceListView.SelectedIndices.Count > 0
                && this.DeviceListView.SelectedIndices[0] != -1)
            {
                //通过用户在显示控件中选择的索引值，在查找其所对应的设备对象；
                DeviceClass currentDevice = DevList[DeviceListView.SelectedIndices[0]];
                
                LoginForm loginform = new LoginForm();
                DialogResult result = loginform.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DevControl.tagErrorCode eCode = currentDevice.Login(loginform.UserName, loginform.Password);
                    if (eCode == DevControl.tagErrorCode.DM_ERR_OK)
                    {
                        //记录当前选择设备对象，作为父窗体属性传递至新开启的子配置窗体；
                        this.SelectedDevice = currentDevice;
                        ConfigForm deviceConfigForm = new ConfigForm(this.SelectedDevice);
                        deviceConfigForm.ShowDialog(this);
                        deviceConfigForm.Dispose();
                    }
                    else
                    {
                        //异常处理；
                        string errMsg = ErrorHandling.HandleError(eCode);
                        System.Windows.Forms.MessageBox.Show(errMsg);
                    }
                }

                loginform.Dispose();
            }
        }
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //使用广播搜索设备；
            SearchDevice(DeviceClass.Broadcast);
        }

        private void DeviceListView_DoubleClick(object sender, EventArgs e)
        {
            ConfigSelectedDevice();
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigSelectedDevice();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDeviceListView();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //关闭主窗体并退出程序；
            this.Close();
        }

        private void iEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //开启IE访问目标设备；
            try
            {
                if (DeviceListView.SelectedIndices.Count > 0
                    && DeviceListView.SelectedIndices[0] != -1)
                {
                    DeviceClass currentDevice = DevList[DeviceListView.SelectedIndices[0]];
                    System.Diagnostics.Process.Start("iexplore.exe", "HTTP://" + getIPAddress(currentDevice.DeviceIP).ToString());
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void telnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //开启TELNET客户端访问目标设备；
            try
            {
                if (DeviceListView.SelectedIndices.Count > 0
                    && DeviceListView.SelectedIndices[0] != -1)
                {
                    DeviceClass currentDevice = DevList[DeviceListView.SelectedIndices[0]];

                    System.Diagnostics.Process.Start("telnet.exe", getIPAddress(currentDevice.DeviceIP).ToString());
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void pingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //Ping目标设备；
                if (DeviceListView.SelectedIndices.Count > 0
                    && DeviceListView.SelectedIndices[0] != -1)
                {
                    DeviceClass currentDevice = DevList[DeviceListView.SelectedIndices[0]];

                    System.Diagnostics.Process.Start("ping.exe", getIPAddress(currentDevice.DeviceIP).ToString() + " -t");
                }
            }
            catch (Exception ex)
            {
                Log.WriteException(ex);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            DevControl.tagErrorCode eCode = DevControl.DM_DeInit();
            if (eCode != DevControl.tagErrorCode.DM_ERR_OK)
            {
                //异常处理
                ErrorHandling.HandleError(eCode);
            }
        }


        private void ComboBox_COM_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ComboBox_baud2.Items.Clear();
            if (ComboBox_COM.SelectedIndex == 0)
            {
              ComboBox_baud2.Items.Add("9600bps");
              ComboBox_baud2.Items.Add("19200bps");
              ComboBox_baud2.Items.Add("38400bps");
              ComboBox_baud2.Items.Add("57600bps");
              ComboBox_baud2.Items.Add("115200bps");
              ComboBox_baud2.SelectedIndex =3;
            }
            else
            {
              ComboBox_baud2.Items.Add("Auto");
              ComboBox_baud2.SelectedIndex=0;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            OpenPort.Enabled=true;
            ClosePort.Enabled = true;
            OpenNetPort.Enabled=false;
            CloseNetPort.Enabled=false;
            CloseNetPort_Click(sender, e);
        }

        private void CloseNetPort_Click(object sender, EventArgs e)
        {
            ClearLastInfo();
            fCmdRet = StaticClassReaderB.CloseNetPort(frmcomportindex);
            if (fCmdRet == 0)
            {
                fOpenComIndex = -1;
                RefreshStatus();
                Button3.Enabled = false;
                Button5.Enabled = false;
                Button1.Enabled = false;
                button2.Enabled = false;
                Button_DestroyCard.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                button4.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                SpeedButton_Query_6B.Enabled = false;
                SpeedButton_Read_6B.Enabled = false;
                SpeedButton_Write_6B.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;

                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;

                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;

                Same_6B.Enabled = false;
                Different_6B.Enabled = false;
                Less_6B.Enabled = false;
                Greater_6B.Enabled = false;

                DestroyCode.Enabled = false;
                AccessCode.Enabled = false;
                NoProect.Enabled = false;
                Proect.Enabled = false;
                Always.Enabled = false;
                AlwaysNot.Enabled = false;
                NoProect2.Enabled = false;
                Proect2.Enabled = false;
                Always2.Enabled = false;
                AlwaysNot2.Enabled = false;
                P_Reserve.Enabled = false;
                P_EPC.Enabled = false;
                P_TID.Enabled = false;
                P_User.Enabled = false;
                Button_WriteEPC_G2.Enabled = false;
                Button_SetMultiReadProtect_G2.Enabled = false;
                Button_RemoveReadProtect_G2.Enabled = false;
                Button_CheckReadProtected_G2.Enabled = false;
                button4.Enabled = false;

                Button_DestroyCard.Enabled = false;
                Button_SetReadProtect_G2.Enabled = false;
                Button_SetEASAlarm_G2.Enabled = false;
                Alarm_G2.Enabled = false;
                NoAlarm_G2.Enabled = false;
                Button_LockUserBlock_G2.Enabled = false;
                SpeedButton_Read_G2.Enabled = false;
                Button_DataWrite.Enabled = false;
                BlockWrite.Enabled = false;
                Button_BlockErase.Enabled = false;
                Button_SetProtectState.Enabled = false;
                ListView1_EPC.Items.Clear();
                ComboBox_EPC1.Items.Clear();
                ComboBox_EPC2.Items.Clear();
                ComboBox_EPC3.Items.Clear();
                ComboBox_EPC4.Items.Clear();
                ComboBox_EPC5.Items.Clear();
                ComboBox_EPC6.Items.Clear();
                button2.Text = "Query Tag";


                SpeedButton_Read_6B.Enabled = false;
                SpeedButton_Write_6B.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;

                ListView_ID_6B.Items.Clear();
                ComOpen = false;

                button10.Text = "获取";
                timer1.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                Button_SetGPIO.Enabled = false;
                Button_GetGPIO.Enabled = false;
                Button_Ant.Enabled = false;
                Button_RelayTime.Enabled = false;
                ClockCMD.Enabled = false;
                Button_OutputRep.Enabled = false;
                Button_Beep.Enabled = false;
                Button_Accuracy.Enabled = false;
                button6.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button12.Enabled = false;
                button18.Enabled = false;
                button19.Enabled = false;
                button20.Enabled = false;
                button21.Enabled = false;
                button23.Enabled = false;
                button24.Enabled = false;
                button25.Enabled = false;
                button26.Enabled = false;
                button27.Enabled = false;
                button28.Enabled = false;
                button29.Enabled = false;
                button31.Enabled = false;
                button32.Enabled = false;
                button33.Enabled = false;
                button34.Enabled = false;
                button35.Enabled = false;
                button36.Enabled = false;
                button37.Enabled = false;
                button37.Enabled = false;
                button38.Enabled = false;
            }
        }

        private void OpenNetPort_Click(object sender, EventArgs e)
        {
            int port, openresult = 0;
            string IPAddr;
            if (textBox9.Text == "")
                Edit_CmdComAddr.Text = "FF";
            fComAdr = Convert.ToByte(textBox9.Text, 16); // $FF;
            if ((textBox7.Text == "") || (textBox8.Text == ""))
                MessageBox.Show("配置信息错误!", "信息");
            port = Convert.ToInt32(textBox7.Text);
            IPAddr = textBox8.Text;
            openresult = StaticClassReaderB.OpenNetPort(port, IPAddr, ref fComAdr, ref frmcomportindex);
            fOpenComIndex = frmcomportindex;
            if (openresult == 0)
            {
                ComOpen = true;
                Button3_Click(sender, e); //自动执行读取写卡器信息
            }
            if ((openresult == 0x35) || (openresult == 0x30))
            {
                MessageBox.Show("连接TCPIP错误", "信息");
                StaticClassReaderB.CloseNetPort(frmcomportindex);
                ComOpen = false;
                return;
            }
            if ((fOpenComIndex != -1) && (openresult != 0X35) && (openresult != 0X30))
            {
                Button3.Enabled = true;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Query_6B.Enabled = true;
                Button_SetGPIO.Enabled = true;
                Button_GetGPIO.Enabled = true;
                Button_Ant.Enabled = true;
                Button_RelayTime.Enabled = true;
                ClockCMD.Enabled = true;
                Button_OutputRep.Enabled = true;
                Button_Beep.Enabled = true;
                Button_Accuracy.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button12.Enabled = true;
                button18.Enabled = true;
                button19.Enabled = true;
                button20.Enabled = true;
                button21.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button23.Enabled = true ;
                button24.Enabled = true;
                button25.Enabled = true;
                button26.Enabled = true;
                button27.Enabled = true;
                button28.Enabled = true;
                button29.Enabled = true;
                button31.Enabled = true;
                button32.Enabled = true;
                button33.Enabled = true;
                button34.Enabled = true;
                button35.Enabled = true;
                button36.Enabled = true;
                button37.Enabled = true;
                button38.Enabled = true;
                ComOpen = true;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
                MessageBox.Show("TCPIP通讯错误", "信息");
            RefreshStatus();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(ComboBox_AlreadyOpenCOM.Items.Count>0)
            ClosePort_Click( sender,  e);
            OpenPort.Enabled=false;
            ClosePort.Enabled=false;
            OpenNetPort.Enabled=true;
            CloseNetPort.Enabled = true;
        }

        private void Edit_WriteData_TextChanged(object sender, EventArgs e)
        {
            int m, n;
            n = Edit_WriteData.Text.Length;
            if ((checkBox_pc.Checked) && (n % 4 == 0) && (C_EPC.Checked))
            {
                m = n / 4;
                m = (m & 0x3F) << 3;
                textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, '0') + "00";
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
              byte TriggerTime=0;
              TriggerTime = Convert.ToByte(comboBox1.SelectedIndex);
              fCmdRet=StaticClassReaderB.SetTriggerTime(ref fComAdr,TriggerTime,frmcomportindex);
              AddCmdLog("SetTriggerTime", "设置", fCmdRet);
        }

        private void button24_Click(object sender, EventArgs e)
        {
              byte LenTID=0;
              byte AdrTID=0;
              if((textBox10.Text.Length!=2)||(textBox11.Text.Length!=2))
              {
                  MessageBox.Show("TID参数错数", "信息");
                  return;
              }
              AdrTID=Convert.ToByte(textBox10.Text,16);
              LenTID=Convert.ToByte(textBox11.Text,16);
              fCmdRet=StaticClassReaderB.SetTIDParameter(ref fComAdr,AdrTID,LenTID,frmcomportindex);
              AddCmdLog("SetTIDParameter", "设置", fCmdRet);
        }

        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                Edit_WordPtr.Text = "02";
                Edit_WordPtr.ReadOnly = true;
                int m, n;
                n = Edit_WriteData.Text.Length;
                if ((checkBox_pc.Checked) && (n % 4 == 0) && (C_EPC.Checked))
                {
                    m = n / 4;
                    m = (m & 0x3F) << 3;
                    textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, '0') + "00";
                }
            }
            else
            {
                Edit_WordPtr.ReadOnly = false;
            }
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void NoAlarm_G2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            if(textBox14.Text.Length!=8)
            {
                MessageBox.Show("序列号长度不正确!");
                return;
            }
            byte[] SeriaNo = new byte[4];
            SeriaNo = HexStringToByteArray(textBox14.Text);
            fCmdRet = StaticClassReaderB.SetSeriaNo(ref fComAdr, SeriaNo, frmcomportindex);
            AddCmdLog("SetSeriaNo", "设置", fCmdRet);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            byte[] SeriaNo = new byte[4];
            textBox14.Text = "";
            fCmdRet = StaticClassReaderB.GetSeriaNo(ref fComAdr, SeriaNo, frmcomportindex);
            if(fCmdRet==0)
            {
                textBox14.Text = ByteArrayToHexString(SeriaNo);
            }
            AddCmdLog("GetSeriaNo", "获取", fCmdRet);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            byte ATMode = 1;
            SeriaATflag = false;
            fCmdRet = StaticClassReaderB.ChangeATMode(ref fComAdr, ATMode, frmcomportindex);
            if (fCmdRet!=0)
            {
                SeriaATflag = true;
            }
            AddCmdLog("ChangeATMode", "进入AT模式", fCmdRet);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            byte ATMode = 0;
            fCmdRet = StaticClassReaderB.ChangeATMode(ref fComAdr, ATMode, frmcomportindex);
            AddCmdLog("ChangeATMode", "退出AT模式", fCmdRet);
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox15.Text = "";
            textBox16.Text = "";
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (textBox16.Text=="")
            {
                MessageBox.Show("指令不能为空!");
                return;
            }
            byte timeout=0;
            byte cmdlen=0;
            byte[] data = new byte[100];
            byte[] cmddata=new byte[100];
            byte recvLen=0;
            byte[] recvdata = new byte[1000];
            data = Encoding.ASCII.GetBytes(textBox16.Text);
            cmdlen = Convert.ToByte(textBox16.Text.Length);
            Array.Copy(data, cmddata, cmdlen);   
            timeout = Convert.ToByte(comboBox6.SelectedIndex + 1);        
            cmddata[cmdlen] = 0x0d;
            cmddata[cmdlen+1] = 0x0a;
            cmdlen = Convert.ToByte(cmdlen + 2);
            fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata,frmcomportindex);
            if(fCmdRet==0)
            {
                textBox15.Text = Encoding.ASCII.GetString(recvdata);
            }
            
            AddCmdLog("ChangeATMode", "发送", fCmdRet);
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                SeriaATflag = false;
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!SP?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen==0))
                    {
                        MessageBox.Show("读取失败!","提示");
                        SeriaATflag = true;
                        return;
                    }
                    
                    int m = 0;
                    int n = 0;
                    int p = 0;
                    int q = 0;
                    string code = "";
                    m = recvs.IndexOf(":");
                    recvs = recvs.Substring(m + 2);
                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    
                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    baudrateCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    databitCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    stopbitCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    parityCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    flowCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf("\r\n");
                    code = recvs.Substring(0, n);
                    fifoCB.SelectedIndex = Convert.ToInt32(code);
                   // recvs = recvs.Substring();

                   
                    
                }
                AddCmdLog("TransparentCMD", "读取", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }
        //AT!SP=0,1,14,3,0,0,0,1
        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!SP=0,1,";
                cmd = cmd + baudrateCB.SelectedIndex.ToString() + "," + databitCB.SelectedIndex.ToString()
                    + "," + stopbitCB.SelectedIndex.ToString() + "," + parityCB.SelectedIndex.ToString()
                    + "," + flowCB.SelectedIndex.ToString() + "," + fifoCB.SelectedIndex.ToString();
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("设置失败!", "提示");
                        return;
                    }
                    
                }

              
                AddCmdLog("TransparentCMD", "发送", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            try
            {
                SeriaATflag = false;
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!IC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("读取失败!", "提示");
                        SeriaATflag = true;
                        return;
                    }

                    int m = 0;
                    int n = 0;
                    int p = 0;
                    int q = 0;
                    string code = "";
                    m = recvs.IndexOf("\"");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    ipTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    subnetTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    gatewayTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 3);
                    pDNSTB.Text = code;

                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    altDNSTB.Text = code;              
                   // recvs = recvs.Substring();
                    AddCmdLog("TransparentCMD", "读取", fCmdRet);
                }


                cmd = "AT!EC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("读取失败!", "提示");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    int p = 0;
                    int q = 0;
                    string code = "";
                    m = recvs.IndexOf("\"");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf("\"");
                    code = recvs.Substring(0, n);
                    macTB.Text = code;
                    
                }
                AddCmdLog("TransparentCMD", "读取", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!IC=0,\"";
                cmd = cmd + ipTB.Text + "\",\"" + subnetTB.Text
                    + "\",\"" + gatewayTB.Text + "\",\"" + pDNSTB.Text
                    + "\",\"" + altDNSTB.Text+"\"";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        cmd = "AT!IC=0,\"";
                        cmd = cmd + ipTB.Text + "\","
                            + ",\"" + gatewayTB.Text + "\",\"" + pDNSTB.Text
                            + "\",\"" + altDNSTB.Text + "\"";
                        data = Encoding.ASCII.GetBytes(cmd);
                        cmdlen = Convert.ToByte(cmd.Length);
                        Array.Copy(data, cmddata, cmdlen);
                        timeout = 60;
                        cmddata[cmdlen] = 0x0d;
                        cmddata[cmdlen + 1] = 0x0a;
                        cmdlen = Convert.ToByte(cmdlen + 2);
                        fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                        if (fCmdRet == 0)
                        {
                            recvs = Encoding.ASCII.GetString(recvdata);
                            if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                            {
                                MessageBox.Show("设置失败!", "提示");
                                return;
                            }

                        }
                    }
                    
                }
                AddCmdLog("TransparentCMD", "发送", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                SeriaATflag = false;
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!TC?";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("读取失败!", "提示");
                        SeriaATflag = true;
                        return;
                    }
                    int m = 0;
                    int n = 0;
                    string code = "";
                    m = recvs.IndexOf(",");
                    recvs = recvs.Substring(m + 1);
                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    workasCB.SelectedIndex = Convert.ToInt32(code);

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    tcpActiveCB.SelectedIndex = Convert.ToInt32(code)-1;

                    n = recvs.IndexOf(",");
                    code = recvs.Substring(0, n);
                    recvs = recvs.Substring(n + 1);
                    tcpLocalPortNUD.Text = code;

                    if (recvs.IndexOf("1800") > 0)
                    {
                        recvs = recvs.Substring(1);
                        n = recvs.IndexOf("\"");
                        code = recvs.Substring(0, n);
                        recvs = recvs.Substring(n + 2);
                        tcpRomteHostTB.Text = code;

                        n = recvs.IndexOf(",");
                        code = recvs.Substring(0, n);
                        tcpRemotePortNUD.Text = code;
                    }
                    else
                    {
                        n = recvs.IndexOf("\"");
                        code = recvs.Substring(0, n - 1);
                        recvs = recvs.Substring(n + 1);
                        tcpRemotePortNUD.Text = code;

                        n = recvs.IndexOf(",");
                        code = recvs.Substring(0, n - 1);
                        recvs = recvs.Substring(n + 1);
                        tcpRomteHostTB.Text = code;
                    }
                   
                }
                AddCmdLog("TransparentCMD", "读取", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!TC=0,";
                cmd = cmd + workasCB.SelectedIndex.ToString() + "," + Convert.ToString(tcpActiveCB.SelectedIndex + 1)
                    + "," + tcpLocalPortNUD.Text + ",\"" + tcpRomteHostTB.Text
                    + "\"," + tcpRemotePortNUD.Text + "," + ",";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        cmd = "AT!TC=0,";
                        cmd = cmd + workasCB.SelectedIndex.ToString() + "," + Convert.ToString(tcpActiveCB.SelectedIndex + 1)
                            + "," + tcpLocalPortNUD.Text + ",\"" + tcpRemotePortNUD.Text
                            + "\"," + tcpRomteHostTB.Text + "," + ",";
                        data = Encoding.ASCII.GetBytes(cmd);
                        cmdlen = Convert.ToByte(cmd.Length);
                        Array.Copy(data, cmddata, cmdlen);
                        timeout = 30;
                        cmddata[cmdlen] = 0x0d;
                        cmddata[cmdlen + 1] = 0x0a;
                        cmdlen = Convert.ToByte(cmdlen + 2);
                        fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                        if (fCmdRet == 0)
                        {
                            recvs = Encoding.ASCII.GetString(recvdata);
                            if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                            {
                                MessageBox.Show("设置失败!", "提示");
                                return;
                            }

                        }
                    }

                }
                AddCmdLog("TransparentCMD", "设置", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void groupBox49_Enter(object sender, EventArgs e)
        {

        }

        private void button37_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!LD";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    cmd = "AT!R";
                    data = Encoding.ASCII.GetBytes(cmd);
                    cmdlen = Convert.ToByte(cmd.Length);
                    Array.Copy(data, cmddata, cmdlen);
                    timeout = 30;
                    cmddata[cmdlen] = 0x0d;
                    cmddata[cmdlen + 1] = 0x0a;
                    cmdlen = Convert.ToByte(cmdlen + 2);
                    fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                }
                AddCmdLog("TransparentCMD", "设置", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            try
            {
                byte timeout = 0;
                byte cmdlen = 0;
                byte[] data = new byte[100];
                byte[] cmddata = new byte[100];
                byte recvLen = 0;
                byte[] recvdata = new byte[1000];
                string cmd = "AT!S";
                data = Encoding.ASCII.GetBytes(cmd);
                cmdlen = Convert.ToByte(cmd.Length);
                Array.Copy(data, cmddata, cmdlen);
                timeout = 30;
                cmddata[cmdlen] = 0x0d;
                cmddata[cmdlen + 1] = 0x0a;
                cmdlen = Convert.ToByte(cmdlen + 2);
                fCmdRet = StaticClassReaderB.TransparentCMD(ref fComAdr, timeout, cmdlen, cmddata, ref recvLen, recvdata, frmcomportindex);
                if (fCmdRet == 0)
                {
                    string recvs = Encoding.ASCII.GetString(recvdata);
                    if ((recvs.IndexOf("ERROR") > 0) || (recvLen == 0))
                    {
                        MessageBox.Show("设置失败!", "提示");
                        return;
                    }
                }
                AddCmdLog("TransparentCMD", "恢复出厂设置", fCmdRet);
            }
            catch (System.Exception ex)
            {
                ex.ToString();
            }
        }

       
    }
}