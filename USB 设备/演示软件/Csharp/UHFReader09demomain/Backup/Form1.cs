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

namespace UHFReader09demomain
{
    public partial class Form1 : Form
    {
        private bool fAppClosed; //在测试模式下响应关闭应用程序
        private byte fComAdr=0xff; //当前操作的ComAdr
        private int ferrorcode;
        private byte fBaud;
        private double fdminfre;
        private double fdmaxfre;
        private byte Maskadr;
        private byte MaskLen;
        private byte MaskFlag;
        private int fCmdRet=30; //所有执行指令的返回值
        private int fOpenComIndex; //打开的串口索引号
        private bool fIsInventoryScan;
        private bool fisinventoryscan_6B;
        private byte[] fOperEPC=new byte[36];
        private byte[] fPassWord=new byte[4];
        private byte[] fOperID_6B=new byte[8];
        private int CardNum1 = 0;
        ArrayList list = new ArrayList();
        private bool fTimer_6B_ReadWrite;
        private string fInventory_EPC_List; //存贮询查列表（如果读取的数据没有变化，则不进行刷新）
        private int frmcomportindex;
        private bool ComOpen=false;
        public Form1()
        {
            InitializeComponent();
        }
        private void RefreshStatus()
        { 
              if(!(ComboBox_AlreadyOpenCOM.Items.Count != 0)) 
                StatusBar1.Panels[1].Text = "通讯关闭";
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
           // ComboBox_PowerDbm.SelectedIndex = 0;
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
              i=40;
              while (i<=300)
              {
                  ComboBox_IntervalTime.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime.SelectedIndex = 1;
              for (i=0;i<7;i++)
                  ComboBox_BlockNum.Items.Add(Convert.ToString(i * 2) + " 和 " + Convert.ToString(i * 2 + 1));
              ComboBox_BlockNum.SelectedIndex = 0;
              i=40;
              while (i<=300 )
              {
                  ComboBox_IntervalTime_6B.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime_6B.SelectedIndex = 1;
              ComboBox_PowerDbm.SelectedIndex = 13;

              for (i = 0; i < 256; i++)
              {
                  comboBox1.Items.Add(Convert.ToString(i) + "*50ms");
                  comboBox2.Items.Add(Convert.ToString(i) + "*50ms");
                  comboBox3.Items.Add(Convert.ToString(i));
              }
              comboBox1.SelectedIndex = 1;
              comboBox2.SelectedIndex = 0;
              comboBox3.SelectedIndex = 1;
              
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
              button6.Enabled = false;
              button7.Enabled = false;
              button9.Enabled = false;
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
             ComboBox_baud2.SelectedIndex = 3;
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
                      if ((fCmdRet==0x35) |(fCmdRet==0x30))
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
                        if (fBaud == 3)
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

              if ((fOpenComIndex != -1) &(openresult != 0X35)  &(openresult != 0X30))
              {
                ComboBox_AlreadyOpenCOM.Items.Add("COM"+Convert.ToString(fOpenComIndex)) ;
                ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1;
                Button3.Enabled = true ;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Query_6B.Enabled = true ;        
                ComOpen = true;
              }
              if ((fOpenComIndex == -1) &&(openresult == 0x30))
                  MessageBox.Show("串口通讯错误", "信息提示");

            if ((ComboBox_AlreadyOpenCOM.Items.Count != 0)&(fOpenComIndex != -1) & (openresult != 0X35) & (openresult != 0X30)&(fCmdRet==0)) 
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
                  port = Convert.ToInt32(temp.Substring(3, temp.Length - 3));
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

              }
              if (ComboBox_AlreadyOpenCOM.Items.Count != 0)
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
                  button6.Enabled = false;
                  button8.Enabled = false;
                  button9.Enabled = false;
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
                  checkBox1.Enabled = false;

                  SpeedButton_Read_6B.Enabled = false;
                  SpeedButton_Write_6B.Enabled = false;
                  Button14.Enabled = false;
                  Button15.Enabled = false;
                  ListView_ID_6B.Items.Clear();
                  ComOpen = false;
                  timer1.Enabled = false;
              }
         }
        private void Button3_Click(object sender, EventArgs e)
        {
             byte[] TrType=new byte[2];
             byte[] VersionInfo=new byte[2];
             byte ReaderType=0;
             byte ScanTime=0;
             byte dmaxfre=0;
             byte dminfre = 0;
             byte powerdBm=0;
             byte FreBand = 0;
             Edit_Version.Text = "";
              Edit_ComAdr.Text = "";
              Edit_scantime.Text = "";
              Edit_Type.Text = "";
              ISO180006B.Checked=false;
              EPCC1G2.Checked=false;
              Edit_powerdBm.Text = "";
              Edit_dminfre.Text = "";
              Edit_dmaxfre.Text = "";
              fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime,frmcomportindex);
              if (fCmdRet == 0)
              {
                  Edit_Version.Text = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                    if (powerdBm > 13)
                        ComboBox_PowerDbm.SelectedIndex = 13;
                    else
                        ComboBox_PowerDbm.SelectedIndex = powerdBm;
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
                  if (ReaderType == 0x08)
                      Edit_Type.Text = "UHFReader09";
                  if ((TrType[0] & 0x02) == 0x02) //第二个字节低第四位代表支持的协议“ISO/IEC 15693”
                  {
                      ISO180006B.Checked = true;
                      EPCC1G2.Checked = true;
                  }
                  else
                  {
                      ISO180006B.Checked = false;
                      EPCC1G2.Checked = false;
                  }
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
              fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr,ref aNewComAdr,frmcomportindex);
              if (fCmdRet==0x13)
              fComAdr = aNewComAdr;
              if (fCmdRet == 0)
              {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "读写器地址成功";
              }
              else if (fCmdRet==0xEE )
                  returninfo = returninfo + setinfo + "读写器地址返回指令错误";
              else
              {
                  returninfo = returninfo + setinfo + "读写器地址失败";
                  returninfoDlg = returninfoDlg + setinfo + "读写器地址失败指令返回=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }
              progressBar1.Value =25; 
              fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr,powerDbm,frmcomportindex);
              if (fCmdRet == 0)
                  returninfo = returninfo + ",功率成功";
              else if (fCmdRet==0xEE )
                  returninfo = returninfo + ",功率返回指令错误";
              else
              {
                  returninfo = returninfo + ",功率失败";
                  returninfoDlg = returninfoDlg + " " + setinfo + "功率失败指令返回=0x"
                       +Convert.ToString(fCmdRet)+"("+GetReturnCodeDesc(fCmdRet)+")";
              }
              
              progressBar1.Value =40; 
              fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr,ref dmaxfre,ref dminfre,frmcomportindex);
              if (fCmdRet == 0 )
                  returninfo = returninfo + ",频率成功";
              else if (fCmdRet==0xEE)
                  returninfo = returninfo + ",频率返回指令错误";
              else
              {
                  returninfo = returninfo + ",频率失败";
                  returninfoDlg = returninfoDlg + " " + setinfo + "频率失败指令返回=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }
              progressBar1.Value =55; 
              fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr,ref fBaud,frmcomportindex);
              if (fCmdRet == 0)
                  returninfo = returninfo + ",波特率成功";
              else if (fCmdRet==0xEE)
                  returninfo = returninfo + ",波特率返回指令错误";
              else
              {
                  returninfo = returninfo + ",波特率失败";
                  returninfoDlg = returninfoDlg + " " + setinfo + "波特率失败指令返回=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

             progressBar1.Value =70; 
              fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr,ref scantime,frmcomportindex);
              if (fCmdRet == 0 )
                  returninfo = returninfo + ",询查时间成功";
             else if (fCmdRet==0xEE)
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
            powerDbm=13;
            fBaud=5;
            scantime=10;
            setinfo=" 恢复 ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr, ref aNewComAdr, frmcomportindex);
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
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr, powerDbm, frmcomportindex);
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
            fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr, ref dmaxfre, ref dminfre, frmcomportindex);
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
            fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr, ref fBaud, frmcomportindex);
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
            fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr, ref scantime, frmcomportindex);
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
                    if (ListItem.SubItems[subItemIndex + 2].Text == "")
                    {
                        ListItem.SubItems[subItemIndex + 2].Text = "1";
                    }
                    else
                    {
                        ListItem.SubItems[subItemIndex + 2].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 2].Text) + 1);
                    }
                }
                else 
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                    ListItem.SubItems[subItemIndex+2].Text = "1";
                }
                else
                {
                    ListItem.SubItems[subItemIndex + 2].Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems[subItemIndex + 2].Text) + 1);
                    if( (Convert.ToUInt32(ListItem.SubItems[subItemIndex + 2].Text)>9999))
                        ListItem.SubItems[subItemIndex + 2].Text="1";
                }

            }
            if (subItemIndex == 2)
            {
                if (ListItem.SubItems[subItemIndex].Text != ItemText)
                {
                    ListItem.SubItems[subItemIndex].Text = ItemText;
                }
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CheckBox_TID.Checked)
            {
                if ((textBox4.Text.Length) != 2 || ((textBox5.Text.Length) != 2))
                {
                    StatusBar1.Panels[0].Text = "TID询查参数错误！";
                    return;
                }
            }
            Timer_Test_.Enabled = !Timer_Test_.Enabled;
            if (!Timer_Test_.Enabled)
            {
                textBox4.Enabled = true;
                textBox5.Enabled = true;
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
                    checkBox1.Enabled=true;
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
                    checkBox1.Enabled=false;

                }
                AddCmdLog("Inventory", "退出询查", 0);
                button2.Text = "查询标签";
            }
            else
            {
                textBox4.Enabled = false;
                textBox5.Enabled = false;
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
                checkBox1.Enabled = false;
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
              fIsInventoryScan = true;
              ListViewItem aListItem = new ListViewItem();
              byte AdrTID = 0;
              byte LenTID = 0;
              byte TIDFlag = 0;
              if (CheckBox_TID.Checked)
              {
                  AdrTID = Convert.ToByte(textBox4.Text, 16);
                  LenTID = Convert.ToByte(textBox5.Text, 16);
                  TIDFlag = 1;
              }
              else
              {
                  AdrTID = 0;
                  LenTID = 0;
                  TIDFlag = 0;
              }
             fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, frmcomportindex);                
             if ( (fCmdRet == 1)| (fCmdRet == 2)| (fCmdRet == 3)| (fCmdRet == 4)|(fCmdRet == 0xFB) )//代表已查找结束，
             {
               byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);               
                temps = ByteArrayToHexString(daw);
                fInventory_EPC_List = temps;            //存贮记录
                 m=0;
                
               /*   while (ListView1_EPC.Items.Count < CardNum)
                 {
                     aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                     aListItem.SubItems.Add("");
                     aListItem.SubItems.Add("");
                     aListItem.SubItems.Add("");
                * 
                 }*/
                 if (CardNum==0)
                 {
                     fIsInventoryScan = false;
                     return;
                 }
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
                         isonlistview=true;
                        }
                      }
                      if (!isonlistview)
                      {
                          aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString());
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          aListItem.SubItems.Add("");
                          s = sEPC;
                          ChangeSubItem(aListItem, 1, s);
                          s = (sEPC.Length / 2).ToString().PadLeft(2, '0');
                          ChangeSubItem(aListItem, 2, s);
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
                byte EPClength=0;
                string str;
                byte[] CardData=new  byte[320];
                if ((maskadr_textbox.Text=="")||(maskLen_textBox.Text=="") )            
              {
                  fIsInventoryScan = false;
                  return;
              }
              if (checkBox1.Checked)
              MaskFlag=1;
              else
              MaskFlag = 0;
              Maskadr = Convert.ToByte(maskadr_textbox.Text,16);
              MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
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
                ENum = Convert.ToByte(str.Length / 4);
                EPClength = Convert.ToByte(str.Length / 2);
                byte[] EPC = new byte[ENum];
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
                fCmdRet = StaticClassReaderB.ReadCard_G2(ref fComAdr, EPC, Mem, WordPtr, Num, fPassWord,Maskadr,MaskLen,MaskFlag, CardData, EPClength, ref ferrorcode, frmcomportindex);
                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[Num*2];
                    Array.Copy(CardData, daw, Num * 2);
                    listBox1.Items.Add(ByteArrayToHexString(daw));
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    AddCmdLog("ReadData", "读", fCmdRet);
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
            byte Num = 0;
            byte Mem = 0;
            byte WNum = 0;
            byte EPClength = 0;
            byte Writedatalen = 0;
            int  WrittenDataNum = 0;
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(ENum * 2);
            byte[] EPC = new byte[ENum];
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
            if (Convert.ToInt32(Edit_WordPtr.Text) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            Num = Convert.ToByte(textBox1.Text);
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
            byte[] Writedata = new byte[WNum * 2];
            Writedata = HexStringToByteArray(s2);
            Writedatalen = Convert.ToByte(WNum * 2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                Writedatalen = Convert.ToByte(Edit_WriteData.Text.Length / 2 + 2);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.WriteCard_G2(ref fComAdr, EPC, Mem, WordPtr, Writedatalen, Writedata, fPassWord,Maskadr,MaskLen,MaskFlag, WrittenDataNum, EPClength, ref ferrorcode, frmcomportindex);
            AddCmdLog("Write data", "写", fCmdRet);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "‘写EPC”指令返回=0x00" +
                  "(写EPC成功)";
            }    
        }

        private void Button_BlockErase_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            byte EPClength = 0;
            string str;
            byte[] CardData = new byte[320];
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (str == "")
                return;
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(str.Length / 2);
            byte[] EPC = new byte[ENum];
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
                MessageBox.Show("起始地址为空","信息提示");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("读/块擦除长度", "信息提示");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text) + Convert.ToInt32(textBox1.Text) > 120)
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
            fCmdRet = StaticClassReaderB.EraseCard_G2(ref fComAdr, EPC, Mem, WordPtr, Num, fPassWord,Maskadr,MaskLen,MaskFlag,EPClength, ref ferrorcode, frmcomportindex);
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
              byte EPClength;
              string str;
              byte ENum;
              if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
              {
                  fIsInventoryScan = false;
                  return;
              }
              if (checkBox1.Checked)
                  MaskFlag = 1;
              else
                  MaskFlag = 0;
              Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
              MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
              if (ComboBox_EPC1.Items.Count == 0)
                  return;
              if (ComboBox_EPC1.SelectedItem == null)
                  return;
              str = ComboBox_EPC1.SelectedItem.ToString();
              if (str == "")
                  return;
              ENum = Convert.ToByte(str.Length / 4);             
              EPClength = Convert.ToByte(str.Length / 2);
              byte[] EPC = new byte[ENum];
              EPC = HexStringToByteArray(str);
              if (textBox2.Text.Length != 8)
              {
                  MessageBox.Show("访问密码小于8，重新输入！","信息提示");
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
                  else if (AlwaysNot.Checked )
                  {
                   setprotect=0x03;
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
                  else if (AlwaysNot2.Checked )
                  {
                   setprotect=0x03;
                   if (MessageBox.Show(this, "确定要设置为永远不可写吗？", "信息提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
              }

              fCmdRet = StaticClassReaderB.SetCardProtect_G2(ref fComAdr, EPC, select, setprotect, fPassWord,Maskadr,MaskLen,MaskFlag, EPClength, ref ferrorcode, frmcomportindex); ;
              AddCmdLog("SetCardProtect", "设置保护", fCmdRet);
        }

        private void Button_DestroyCard_Click(object sender, EventArgs e)
        {
            byte EPClength;
            string str;
            byte ENum;
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
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
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(str.Length / 2);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_DestroyCode.Text);
            fCmdRet = StaticClassReaderB.DestroyCard_G2(ref fComAdr, EPC, fPassWord,Maskadr,MaskLen,MaskFlag, EPClength, ref ferrorcode, frmcomportindex);
            AddCmdLog("DestroyCard", "销毁标签", fCmdRet);
            if (fCmdRet == 0)
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " “销毁标签”指令返回=0x00" +
                          "(销毁成功)";
        }

        private void Button_WriteEPC_G2_Click(object sender, EventArgs e)
        {
              byte[] WriteEPC =new byte[100];
              byte WriteEPClen;
              byte ENum;
              if (Edit_AccessCode3.Text.Length < 8)
              {
                  MessageBox.Show("访问密码小于8位！请重新输入！!", "信息提示");
                  return;
              }
             if ((Edit_WriteEPC.Text.Length%4) !=0) 
            {
                MessageBox.Show("请输入以字为单位的16进制数！'+#13+#10+'例如：1234、12345678!", "信息提示");
                    return;
            }
            WriteEPClen=Convert.ToByte(Edit_WriteEPC.Text.Length/ 2) ;
            ENum = Convert.ToByte(Edit_WriteEPC.Text.Length / 4);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(Edit_WriteEPC.Text);
            fPassWord = HexStringToByteArray(Edit_AccessCode3.Text);
            fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, WriteEPClen, ref ferrorcode, frmcomportindex);
            AddCmdLog("WriteEPC_G2", "写EPC", fCmdRet);
              if (fCmdRet == 0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "“写EPC”指令返回=0x00" +
                            "(写EPC成功)";
        }

        private void Button_SetReadProtect_G2_Click(object sender, EventArgs e)
        {
            byte EPClength;
            byte ENum;
            string str;
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
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
              ENum = Convert.ToByte(str.Length / 4);
              EPClength = Convert.ToByte(str.Length / 2);
              byte[] EPC = new byte[ENum];
              EPC = HexStringToByteArray(str);
              fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
              fCmdRet = StaticClassReaderB.SetReadProtect_G2(ref fComAdr, EPC, fPassWord,Maskadr,MaskLen,MaskFlag, EPClength, ref ferrorcode, frmcomportindex);
              AddCmdLog("SetReadProtect_G2", "设置单张读保护", fCmdRet);
              if (fCmdRet==0)
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
             fCmdRet=StaticClassReaderB.SetMultiReadProtect_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("SetMultiReadProtect_G2", "设置单张读保护（不需EPC号）", fCmdRet);
              if (fCmdRet==0)
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
             fCmdRet=StaticClassReaderB.RemoveReadProtect_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
             AddCmdLog("RemoveReadProtect_G2", "解除单张读保护（不需EPC号）", fCmdRet);
              if (fCmdRet==0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '解除单张读保护（不需EPC号）'指令返回=0x00" +
                        "(解除单张读保护（不需EPC号）成功)";
        }

        private void Button_CheckReadProtected_G2_Click(object sender, EventArgs e)
        {
            byte readpro=2;
              fCmdRet=StaticClassReaderB.CheckReadProtected_G2(ref fComAdr,ref readpro,ref ferrorcode,frmcomportindex);
              AddCmdLog("CheckReadProtected_G2", "检测单张被读保护（不需要访问密码）", fCmdRet);
              if (fCmdRet==0)
              {
               if (readpro==0)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '检测单张被读保护（不需要访问密码）'指令返回=0x00" +
                        "(电子标签没有被设置为读保护";
               if (readpro==1)
                   StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " '检测单张被读保护（不需要访问密码）'指令返回=0x01" +
                        "(电子标签被设置读保护锁定)";
              }
        }

        private void Button_SetEASAlarm_G2_Click(object sender, EventArgs e)
        {
            byte EPClength=0;
            byte  EAS=0;
            byte ENum;
            string str;
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
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
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(str.Length / 2);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(str);
            fPassWord = HexStringToByteArray(Edit_AccessCode5.Text);
             if (Alarm_G2.Checked) 
             EAS= 1;
             else 
             EAS=0;
         fCmdRet = StaticClassReaderB.SetEASAlarm_G2(ref fComAdr, EPC, fPassWord,Maskadr,MaskLen,MaskFlag, EAS, EPClength, ref ferrorcode, frmcomportindex);
              AddCmdLog("SetEASAlarm_G2", "报警设置", fCmdRet);     //v2.1 change
              if (fCmdRet==0)
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
             fCmdRet=StaticClassReaderB.CheckEASAlarm_G2(ref fComAdr,ref ferrorcode,frmcomportindex);
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
             byte EPClength = 0;
             byte BlockNum = 0;
             byte ENum;
             string str;
             if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
             {
                 fIsInventoryScan = false;
                 return;
             }
             if (checkBox1.Checked)
                 MaskFlag = 1;
             else
                 MaskFlag = 0;
             Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
             MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
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
             ENum = Convert.ToByte(str.Length / 4);
             EPClength = Convert.ToByte(str.Length / 2);
             byte[] EPC = new byte[ENum];
             EPC = HexStringToByteArray(str);
             fPassWord = HexStringToByteArray(Edit_AccessCode6.Text);
             BlockNum=Convert.ToByte(ComboBox_BlockNum.SelectedIndex*2) ;
             fCmdRet=StaticClassReaderB.LockUserBlock_G2(ref fComAdr,EPC,fPassWord,Maskadr,MaskLen,MaskFlag,BlockNum,EPClength,ref ferrorcode,frmcomportindex);
             AddCmdLog("LockUserBlock_G2", "用户区数据块锁定", fCmdRet);
              if (fCmdRet==0)
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
        }

        private void ComboBox_IntervalTime_SelectedIndexChanged(object sender, EventArgs e)
        {
              if   (ComboBox_IntervalTime.SelectedIndex <6)
              Timer_Test_.Interval =100;
              else
              Timer_Test_.Interval =(ComboBox_IntervalTime.SelectedIndex+4)*10;
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
                        Same_6B.Enabled = true ;
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
                fCmdRet = StaticClassReaderB.Inventory_6B(ref fComAdr, ID_6B, frmcomportindex);
                if (fCmdRet == 0)
                {
                    byte[] daw = new byte[8];
                    Array.Copy(ID_6B, daw, 8);
                    temps = ByteArrayToHexString(daw);                    
                    if (!list.Contains(temps))
                    {
                        CardNum1 = CardNum1 + 1;
                        list.Add(temps);
                    }
                    while (ListView_ID_6B.Items.Count < CardNum1)
                    {
                        aListItem = ListView_ID_6B.Items.Add((ListView_ID_6B.Items.Count + 1).ToString());
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                    }
                     isonlistview = false;
                     for (i = 0; i < CardNum1; i++)     //判断是否在Listview列表内
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
                        mask = 0XF0;
                        break;
                    case 5:
                        mask = 0XF8;
                        break;
                    case 6:
                        mask = 0XFC;
                        break;
                    case 7:
                        mask = 0XFE;
                        break;
                    case 8:
                        mask = 0XFF;
                        break;
                }
                if (Edit_Query_StartAddress_6B.Text == "")
                    return;
                StartAddress = Convert.ToByte(Edit_Query_StartAddress_6B.Text);
                fCmdRet = StaticClassReaderB.inventory2_6B(ref fComAdr, Condition, StartAddress, mask, daw, ID2_6B, ref CardNum, frmcomportindex);
                if ((fCmdRet == 0x15) | (fCmdRet == 0x16) | (fCmdRet == 0x17) | (fCmdRet == 0x18) | (fCmdRet == 0xFB))
                {
                    byte[] daw1 = new byte[CardNum * 8];
                    Array.Copy(ID2_6B, daw1, CardNum * 8);
                    temps = ByteArrayToHexString(daw1);
                    for (i = 0; i < CardNum; i++)
                    {
                        sID = temps.Substring(16*i,16);
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
                        for (j = 0; j < ListView_ID_6B.Items.Count; j++)     //判断是否在Listview列表内
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
                    if  (fCmdRet!=0 )
                    AddCmdLog("Inventory", "查询标签", fCmdRet);
                  }
                  else if (fCmdRet == 0XFB) //说明还未将所有卡读取完
                  {

                      StatusBar1.Panels[0].Text =  DateTime.Now.ToLongTimeString() + " “查询标签”指令返回=0xFB" +
                           "(无电子标签可操作)";
                  }
                  else if (fCmdRet == 0)
                      StatusBar1.Panels[0].Text =  DateTime.Now.ToLongTimeString() +  " “查询标签”指令返回=0x00" +
                           "(找到一张电子标签)";
                  else
                     AddCmdLog("Inventory", "查询标签", fCmdRet);
                  if (fCmdRet==0xEE)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  "查询标签”指令返回=0xee" +
                                "(返回指令错误)" ;
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
                MessageBox.Show("起始地址为空!", "信息提示");
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
            fCmdRet = StaticClassReaderB.ReadCard_6B(ref fComAdr, ID_6B, StartAddress, Num, CardData, ref ferrorcode, frmcomportindex);
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
                AddCmdLog("Wtite", "退出", 0);
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
            StartAddress = Convert.ToByte(Edit_StartAddress_6B.Text);
            if ((Edit_WriteData_6B.Text == "") | (Edit_WriteData_6B.Text.Length%2)!=0)
                return;
            Writedatalen =Convert.ToByte(Edit_WriteData_6B.Text.Length / 2);
            byte[] Writedata = new byte[Writedatalen];
            Writedata = HexStringToByteArray(Edit_WriteData_6B.Text);
            fCmdRet=StaticClassReaderB.WriteCard_6B(ref fComAdr,ID_6B,StartAddress,Writedata,Writedatalen,ref writtenbyte,ref ferrorcode,frmcomportindex);
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
                fCmdRet=StaticClassReaderB.LockByte_6B(ref fComAdr,ID_6B,Address,ref ferrorcode,frmcomportindex);
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
           if (fCmdRet==0)
           {
               if  (ReLockState==0)
               StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " “检测锁定”指令返回=0x00" +
                         "(该字节未被锁定)" ;
               if  (ReLockState==1)
               StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  "  “检测锁定”指令返回=0x01" +
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
            if ((!Timer_Test_.Enabled) & (!Timer_G2_Alarm.Enabled) &(!Timer_G2_Read.Enabled))
            {
            //    Button_DataWrite.Enabled = false;
            }
            if (checkBox_pc.Checked)
            {
                Edit_WordPtr.Text = "02";
                Edit_WordPtr.ReadOnly = true;
            }
            else
            {
                Edit_WordPtr.ReadOnly = false;
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
                  //  if (C_EPC.Checked)
                  //      Button_DataWrite.Enabled = false;
                  //  else
                        Button_DataWrite.Enabled = true;
                        BlockWrite.Enabled = true;
                    Button_BlockErase.Enabled = true;
                    checkBox1.Enabled = true;
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
                    checkBox1.Enabled = false;
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
            
        }

        private void Edit_CmdComAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void Edit_Len_6B_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
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

        private void maskLen_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                maskadr_textbox.Enabled = true;
                maskLen_textBox.Enabled = true;
            }
            else
            {
                maskadr_textbox.Enabled = false;
                maskLen_textBox.Enabled = false;
            }
        }

        private void groupBox30_Enter(object sender, EventArgs e)
        {

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

       

        private void BlockWrite_Click(object sender, EventArgs e)
        {
            byte WordPtr, ENum;
            byte Num = 0;
            byte Mem = 0;
            byte WNum = 0;
            byte EPClength = 0;
            byte Writedatalen = 0;
            int WrittenDataNum = 0;
            string s2, str;
            byte[] CardData = new byte[320];
            byte[] writedata = new byte[230];
            if ((maskadr_textbox.Text == "") || (maskLen_textBox.Text == ""))
            {
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text, 16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text, 16);
            if (ComboBox_EPC2.Items.Count == 0)
                return;
            if (ComboBox_EPC2.SelectedItem == null)
                return;
            str = ComboBox_EPC2.SelectedItem.ToString();
            if (str == "")
                return;
            ENum = Convert.ToByte(str.Length / 4);
            EPClength = Convert.ToByte(ENum * 2);
            byte[] EPC = new byte[ENum];
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
            if (Convert.ToInt32(Edit_WordPtr.Text) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
            {
                return;
            }
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            Num = Convert.ToByte(textBox1.Text);
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
                MessageBox.Show("以字为单位输入.", "块写");
                return;
            }
            WNum = Convert.ToByte(s2.Length / 4);
            byte[] Writedata = new byte[WNum * 2];
            Writedata = HexStringToByteArray(s2);
            Writedatalen = Convert.ToByte(WNum * 2);
            if ((checkBox_pc.Checked) && (C_EPC.Checked))
            {
                WordPtr = 1;
                Writedatalen = Convert.ToByte(Edit_WriteData.Text.Length / 2 + 2);
                Writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text);
            }
            fCmdRet = StaticClassReaderB.WriteBlock_G2(ref fComAdr, EPC, Mem, WordPtr, Writedatalen, Writedata, fPassWord, Maskadr, MaskLen, MaskFlag, WrittenDataNum, EPClength, ref ferrorcode, frmcomportindex);
            AddCmdLog("Write Block", "块写", fCmdRet, ferrorcode);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'块写'命令 返回=0x00" +
                     "(块写成功)";
            }    
        }

        private void radioButton_band5_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dmaxfre.Items.Clear();
            ComboBox_dminfre.Items.Clear();
            for (i = 0; i < 15; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 14;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void CheckBox_TID_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_TID.Checked)
            {
                groupBox33.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            else
            {
                groupBox33.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
            }
        }

        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                if (C_EPC.Checked)
                {
                    Edit_WordPtr.Text = "02";
                    Edit_WordPtr.ReadOnly = true;
                }
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

        private void button8_Click(object sender, EventArgs e)
        {
            byte BeepEn = 0;
            if (radioButton1.Checked)
            {
                BeepEn = 1;
            }
            BeepEn = Convert.ToByte(BeepEn | 0x80);
            fCmdRet = StaticClassReaderB.SetBeep(ref fComAdr, ref BeepEn, frmcomportindex);
            AddCmdLog("Set", "设置", fCmdRet);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte BeepEn = 0;
            fCmdRet = StaticClassReaderB.SetBeep(ref fComAdr, ref BeepEn, frmcomportindex);
            if (fCmdRet == 0)
            {
                if ((BeepEn & 1) == 1)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
            }
            AddCmdLog("Get", "读取", fCmdRet);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte AvtiveTime=Convert.ToByte(comboBox1.SelectedIndex);
            byte SilentTime = Convert.ToByte(comboBox2.SelectedIndex);
            byte Times = Convert.ToByte(comboBox3.SelectedIndex);
            fCmdRet = StaticClassReaderB.BuzzerAndLEDControl(ref fComAdr, AvtiveTime,SilentTime,Times, frmcomportindex);
            AddCmdLog("Get", "设置", fCmdRet);
        }

       
    }
}