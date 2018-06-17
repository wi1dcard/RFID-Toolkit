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
using System.Net.Sockets;
using System.Threading;

namespace UHFReader18demomain
{
    public partial class Form1 : Form
    {
       // [STAThread]
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
        private bool breakflag=false;
        private double x_z;
        private double y_f;
        //以下TCPIP配置所需变量
        public string fRecvUDPstring="";
        public string RemostIP="";
        public Form1()
        {
            InitializeComponent();
        }
        private void RefreshStatus()
        { 
              if(!(ComboBox_AlreadyOpenCOM.Items.Count != 0)) 
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
                    return "Operation Successed";
                case 0x01:
                    return "Return before Inventory finished";
                case 0x02:
                    return "the Inventory-scan-time overflow";
                case 0x03:
                    return "More Data";
                case 0x04:
                    return "Reader module MCU is Full";
                case 0x05:
                    return "Access Password Error";
                case 0x09:
                    return "Destroy Password Error";
                case 0x0a:
                    return "Destroy Password Error Cannot be Zero";
                case 0x0b:
                    return "Tag Not Support the command";
                case 0x0c:
                    return "Use the commmand,Access Password Cannot be Zero";
                case 0x0d:
                    return "Tag is protected,cannot set it again";
                case 0x0e:
                    return "Tag is unprotected,no need to reset it";
                case 0x10:
                    return "There is some locked bytes,write fail";
                case 0x11:
                    return "can not lock it";
                case 0x12:
                    return "is locked,cannot lock it again";
                case 0x13:
                    return "Parameter Save Fail,Can Use Before Power";
                case 0x14:
                    return "Cannot adjust";
                case 0x15:
                    return "Return before Inventory finished";
                case 0x16:
                    return "Inventory-Scan-Time overflow";
                case 0x17:
                    return "More Data";
                case 0x18:
                    return "Reader module MCU is full";
                case 0x19:
                    return "Not Support Command Or AccessPassword Cannot be Zero";
                case 0xFA:
                    return "Get Tag,Poor Communication,Inoperable";
                case 0xFB:
                    return "No Tag Operable";
                case 0xFC:
                    return "Tag Return ErrorCode";
                case 0xFD:
                    return "Command length wrong";
                case 0xFE:
                    return "Illegal command";
                case 0xFF:
                    return "Parameter Error";
                case 0x30:
                    return "Communication error";
                case 0x31:
                    return "CRC checksummat error";
                case 0x32:
                    return "Return data length error";
                case 0x33:
                    return "Communication busy";
                case 0x34:
                    return "Busy,command is being executed";
                case 0x35:
                    return "ComPort Opened";
                case 0x36:
                    return "ComPort Closed";
                case 0x37:
                    return "Invalid Handle";
                case 0x38:
                    return "Invalid Port";
                case 0xEE:
                    return "Return command error";
                default:
                    return "";
            }
        }
        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "Other error";
                case 0x03:
                    return "Memory out or pc not support";
                case 0x04:
                    return "Memory Locked and unwritable";
                case 0x0b:
                    return "No Power,memory write operation cannot be executed";
                case 0x0f:
                    return "Not Special Error,tag not support special errorcode";
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
                  ComboBox_BlockNum.Items.Add(Convert.ToString(i * 2) + " and " + Convert.ToString(i * 2 + 1));
              ComboBox_BlockNum.SelectedIndex = 0;
              i=40;
              while (i<=300 )
              {
                  ComboBox_IntervalTime_6B.Items.Add(Convert.ToString(i) + "ms");
              i=i+10;
              }
              ComboBox_IntervalTime_6B.SelectedIndex = 1;
              for (i = 0; i < 256; i++)
              {
                  comboBox1.Items.Add(Convert.ToString(i) + "*10ms");
              }
              comboBox1.SelectedIndex = 30;
              for (i = 1; i < 256; i++)
              {
                  comboBox3.Items.Add(Convert.ToString(i) + "*10us");
              }
              comboBox3.SelectedIndex = 9;
              for (i = 1; i < 256; i++)
              {
                  comboBox2.Items.Add(Convert.ToString(i) + "*100us");
              }
              comboBox2.SelectedIndex = 14;
              for (i = 0; i < 256; i++)
              {
                  comboBox6.Items.Add(Convert.ToString(i) + "*1s");
              }
              comboBox6.SelectedIndex = 0;
              for (i = 1; i < 33; i++)
              {
                  comboBox5.Items.Add(Convert.ToString(i));
              }
              comboBox5.SelectedIndex = 0;
              comboBox4.SelectedIndex = 0;
              ComboBox_PowerDbm.SelectedIndex = 30;
              comboBox7.SelectedIndex = 8;
            for (i = 0; i < 101; i++)
              {
                  comboBox_OffsetTime.Items.Add(Convert.ToString(i)+"*1ms");
              }
              comboBox_OffsetTime.SelectedIndex = 5;
              comboBox8.SelectedIndex = 0;

              for (i = 0; i < 255; i++)
                  comboBox_tigtime.Items.Add(Convert.ToString(i) + "*1s");
              comboBox_tigtime.SelectedIndex = 0;   //
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
              button20.Enabled = false;
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

              radioButton1.Checked = true ;
              radioButton4.Checked = true ;
              radioButton5.Checked = true ;
              radioButton7.Checked = true ;
              radioButton10.Checked = true ;
              radioButton14.Checked = true ;
              button6.Enabled=false ;
              button8.Enabled = false ;
              button9.Enabled = false ;
              button10.Enabled = false ;
              button11.Enabled = false ;
              comboBox5.Enabled = false ;
              radioButton5.Enabled =false;
               radioButton6.Enabled =false;
               radioButton7.Enabled =false;
               radioButton8.Enabled =false;
               radioButton9.Enabled =false;
               radioButton10.Enabled =false;
               radioButton11.Enabled =false;
               radioButton12.Enabled =false;
               radioButton13.Enabled =false;
               radioButton14.Enabled =false;
               radioButton15.Enabled =false;
               textBox3.Enabled = false;
               radioButton_band1.Checked = true;
               radioButton16.Enabled = false;
               radioButton17.Enabled = false;
               radioButton18.Enabled = false;
               radioButton19.Enabled = false;
               radioButton16.Checked = true;
               ComboBox_baud2.SelectedIndex = 3;
               comboBox9.SelectedIndex = 0;
               comboBox10.SelectedIndex = 0;
               radioButton22.Checked = true;
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
                      fBaud = Convert.ToByte(ComboBox_baud2.SelectedIndex);
                      if (fBaud>2)
                      {
                          fBaud = Convert.ToByte(fBaud + 2);
                      }
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
                            MessageBox.Show ("Serial Communication Error or Occupied", "Information");
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
                            MessageBox.Show("COM Opened", "Information");
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
                                MessageBox.Show("Serial Communication Error or Occupied", "Information");
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
                button20.Enabled = true;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Query_6B.Enabled = true ;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button12.Enabled = true;
                button_OffsetTime.Enabled = true;
                button_settigtime.Enabled = true;
                button_gettigtime.Enabled = true;
                ComOpen = true;
              }
              if ((fOpenComIndex == -1) &&(openresult == 0x30)) 
                MessageBox.Show("Serial Communication Error", "Information");

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
                    MessageBox.Show("Please Choose COM Port to close", "Information");
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
                    MessageBox.Show("Serial Communication Error", "Information");
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
                  button20.Enabled = false;
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
                  button6.Enabled = false;
                  button8.Enabled = false;
                  button9.Enabled = false;

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
                  button2.Text = "Stop";
                  checkBox1.Enabled = false;

                  SpeedButton_Read_6B.Enabled = false;
                  SpeedButton_Write_6B.Enabled = false;
                  Button14.Enabled = false;
                  Button15.Enabled = false;
                  ListView_ID_6B.Items.Clear();
                  ComOpen = false;
                  button12.Enabled = false;
                  button10.Text = "Get"; 
                  button10.Enabled = false;
                  button11.Enabled = false;
                  timer1.Enabled = false;
                  comboBox4.SelectedIndex = 0;
                  button_OffsetTime.Enabled = false;
                  button_settigtime.Enabled = false;
                  button_gettigtime.Enabled = false;
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
              ComboBox_PowerDbm.Items.Clear();
              fCmdRet = StaticClassReaderB.GetReaderInformation(ref fComAdr, VersionInfo, ref ReaderType, TrType, ref dmaxfre, ref dminfre, ref powerdBm, ref ScanTime, frmcomportindex);
              if (fCmdRet == 0)
              {
                  Edit_Version.Text = Convert.ToString(VersionInfo[0], 10).PadLeft(2, '0') + "." + Convert.ToString(VersionInfo[1], 10).PadLeft(2, '0');
                  if (VersionInfo[1] >= 30)
                  {
                      for (int i = 0; i < 31; i++)
                          ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                      if (powerdBm > 30)
                          ComboBox_PowerDbm.SelectedIndex = 30;
                      else
                          ComboBox_PowerDbm.SelectedIndex = powerdBm;
                  }
                  else
                  {
                      for (int i = 0; i < 19; i++)
                          ComboBox_PowerDbm.Items.Add(Convert.ToString(i));
                      if (powerdBm > 18)
                          ComboBox_PowerDbm.SelectedIndex = 18;
                      else
                          ComboBox_PowerDbm.SelectedIndex = powerdBm;
                  }
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
                  if (ReaderType == 0x03)
                      Edit_Type.Text = "";
                  if (ReaderType == 0x06)
                      Edit_Type.Text = "";
                  if (ReaderType == 0x09)
                      Edit_Type.Text = "UHFReader18";
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
              AddCmdLog("GetReaderInformation","GetReaderInformation", fCmdRet);
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
                  setinfo = "Write";
              progressBar1.Value =10;     
              fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr,ref aNewComAdr,frmcomportindex);
              if (fCmdRet==0x13)
              fComAdr = aNewComAdr;
              if (fCmdRet == 0)
              {
                fComAdr = aNewComAdr;
                returninfo=returninfo+setinfo+"Address Successfully";
              }
              else if (fCmdRet==0xEE )
              returninfo=returninfo+setinfo+"Address Response Command Error";
              else
              {
              returninfo=returninfo+setinfo+"Address Fail";
              returninfoDlg=returninfoDlg+setinfo+"Address Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

              progressBar1.Value =25; 
              fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr,powerDbm,frmcomportindex);
              if (fCmdRet == 0)
               returninfo=returninfo+",Power Success";
              else if (fCmdRet==0xEE )
              returninfo=returninfo+",Power Response Command Error";
              else
              {
                  returninfo=returninfo+",Power Fail";
                  returninfoDlg=returninfoDlg+" "+setinfo+"Power Fail Command Response=0x"
                       +Convert.ToString(fCmdRet)+"("+GetReturnCodeDesc(fCmdRet)+")";
              }
              
              progressBar1.Value =40; 
              fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr,ref dmaxfre,ref dminfre,frmcomportindex);
              if (fCmdRet == 0 )
               returninfo=returninfo+",Frequency Success";
              else if (fCmdRet==0xEE)
              returninfo=returninfo+",Frequency Response Command Error";
              else
              {
              returninfo =returninfo+",Frequency Fail";
              returninfoDlg=returninfoDlg+" "+setinfo+"Frequency Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
              }

                    progressBar1.Value =55; 
                  fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr,ref fBaud,frmcomportindex);
                  if (fCmdRet == 0)
                   returninfo=returninfo+",Baud Rate Success";
                  else if (fCmdRet==0xEE)
                  returninfo=returninfo+",Baud Rate Response Command Error";
                  else
                  {
                  returninfo=returninfo+",Baud Rate Fail";
                  returninfoDlg=returninfoDlg+" "+setinfo+"Baud Rate Fail Command Response=0x"
                       + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
                  }

             progressBar1.Value =70; 
              fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr,ref scantime,frmcomportindex);
              if (fCmdRet == 0 )
               returninfo=returninfo+",InventoryScanTime Success";
             else if (fCmdRet==0xEE)
              returninfo=returninfo+",InventoryScanTime Response Command Error";
              else
              {
              returninfo=returninfo+",InventoryScanTime Fail";
              returninfoDlg=returninfoDlg+" "+setinfo+"InventoryScanTime Fail Command Response=0x"
                   + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
             }

              progressBar1.Value =100; 
              Button3_Click(sender,e);
              progressBar1.Visible=false;
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
              if  (returninfoDlg!="")
                 MessageBox.Show(returninfoDlg, "Information");
            
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
            aNewComAdr =0X00;
            if (Convert.ToInt32(Edit_Version.Text.Substring(3, 2)) >= 30)
                powerDbm = 30;
            else
                powerDbm = 18;
            fBaud=5;
            scantime=10;
            setinfo=" Recovery ";
            ComboBox_baud.SelectedIndex = 3;
            progressBar1.Value = 10;
            fCmdRet = StaticClassReaderB.WriteComAdr(ref fComAdr, ref aNewComAdr, frmcomportindex);
            if (fCmdRet == 0x13)
                fComAdr = aNewComAdr;
            if (fCmdRet == 0)
            {
                fComAdr = aNewComAdr;
                returninfo = returninfo + setinfo + "Address Successfully";
            }
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + setinfo + "Address Response Command Error";
            else
            {
                returninfo = returninfo + setinfo + "Address Fail";
                returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 25;
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref fComAdr, powerDbm, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Power Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Power Response Command Error";
            else
            {
                returninfo = returninfo + ",Power Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 40;
            fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr, ref dmaxfre, ref dminfre, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Frequency Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Frequency Response Command Error";
            else
            {
                returninfo = returninfo + ",Frequency Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 55;
            fCmdRet = StaticClassReaderB.Writebaud(ref fComAdr, ref fBaud, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",Baud Rate Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",Baud Rate Response Command Error";
            else
            {
                returninfo = returninfo + ",Baud Rate Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 70;
            fCmdRet = StaticClassReaderB.WriteScanTime(ref fComAdr, ref scantime, frmcomportindex);
            if (fCmdRet == 0)
                returninfo = returninfo + ",InventoryScanTime Success";
            else if (fCmdRet == 0xEE)
                returninfo = returninfo + ",InventoryScanTime Response Command Error";
            else
            {
                returninfo = returninfo + ",InventoryScanTime Fail";
                returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x"
                     + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")";
            }

            progressBar1.Value = 100;
            Button3_Click(sender, e);
            progressBar1.Visible = false;
            StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + returninfo;
            if (returninfoDlg != "")
                MessageBox.Show(returninfoDlg, "Information");
            
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
                MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information");
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
                    StatusBar1.Panels[0].Text = "TID Parameter Error！";
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
                AddCmdLog("Inventory", "Exit Query", 0);
                button2.Text = "Query Tag";
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
                button2.Text = "Stop";
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
              ListViewItem aListItem = new ListViewItem();
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
                MessageBox.Show("Address of Tag Data is NULL", "Information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information");
                return;
            }
            if (Edit_AccessCode2.Text == "")
            {
                MessageBox.Show("(PassWord) is NULL", "Information");
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
                   SpeedButton_Read_G2.Text = "Stop";
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
                   SpeedButton_Read_G2.Text = "Read";
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
                if (str == "")
                {
                  // fIsInventoryScan = false;
                  //  return;
                }
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
                    AddCmdLog("ReadData", "Read", fCmdRet);
                }
                if (ferrorcode != -1)
             {
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +
                   " 'Read' Response ErrorCode=0x" + Convert.ToString(ferrorcode, 2) +
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
                fIsInventoryScan = false;
                return;
            }
            if (checkBox1.Checked)
                MaskFlag = 1;
            else
                MaskFlag = 0;
            Maskadr = Convert.ToByte(maskadr_textbox.Text,16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
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
                MessageBox.Show("Address of Tag Data is NULL", "Information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
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
                MessageBox.Show("The Number must be 4 times.", "Wtite");
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
            AddCmdLog("Write data", "Write", fCmdRet, ferrorcode);
            if (fCmdRet == 0)
            {
             StatusBar1.Panels[0].Text =DateTime.Now.ToLongTimeString() +  "'Write'Command Response=0x00" +
                  "(completely write Data successfully)";
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
            Maskadr = Convert.ToByte(maskadr_textbox.Text,16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
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
                MessageBox.Show("Address of Tag Data is NULL", "Information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
                return;
            if (Edit_AccessCode2.Text == "")
                return;
            WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16);
            if ((Mem == 1) & (WordPtr < 2))
            {
                MessageBox.Show("the length of start Address of erasing EPC area is equal or greater than 0x01!", "Information");
                return;
            }
            Num = Convert.ToByte(textBox1.Text);
            if (Edit_AccessCode2.Text.Length != 8)
            {
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode2.Text);
            fCmdRet = StaticClassReaderB.EraseCard_G2(ref fComAdr, EPC, Mem, WordPtr, Num, fPassWord,Maskadr,MaskLen,MaskFlag,EPClength, ref ferrorcode, frmcomportindex);
            AddCmdLog("EraseCard", "Erase data", fCmdRet);
            if (fCmdRet == 0)
            {
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Block Erase'Command Response=0x00" +
                     "(Block Erase successfully)";
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
              Maskadr = Convert.ToByte(maskadr_textbox.Text,16);
              MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
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
                  MessageBox.Show("Access Password Less Than 8 digit!Please input again!","Information");
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
                   if(MessageBox.Show(this, "Set readable and writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
                  else if (AlwaysNot.Checked )
                  {
                   setprotect=0x03;
                   if(MessageBox.Show(this, "Set never readable and writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
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
                   if(MessageBox.Show(this, "Set writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
                  else if (AlwaysNot2.Checked )
                  {
                   setprotect=0x03;
                  if(MessageBox.Show(this, "Set never writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                         return;
                  }
              }

              fCmdRet = StaticClassReaderB.SetCardProtect_G2(ref fComAdr, EPC, select, setprotect, fPassWord,Maskadr,MaskLen,MaskFlag, EPClength, ref ferrorcode, frmcomportindex); ;
            AddCmdLog("SetCardProtect", "SetProtect", fCmdRet);
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
            if (MessageBox.Show(this, "Kill the Tag  Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                return;
            if (Edit_DestroyCode.Text.Length != 8)
            {
                MessageBox.Show("Kill Password Less Than 8 digit!Please input again!", "Information");
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
            AddCmdLog("DestroyCard", "Kill Tag", fCmdRet);
            if (fCmdRet == 0)
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Kill Tag'Command Response=0x00" +
                          "(Kill successfully)";
        }

        private void Button_WriteEPC_G2_Click(object sender, EventArgs e)
        {
              byte[] WriteEPC =new byte[100];
              byte WriteEPClen;
              byte ENum;
              if (Edit_AccessCode3.Text.Length < 8)
              {
                  MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
                  return;
              }
             if ((Edit_WriteEPC.Text.Length%4) !=0) 
            {
                    MessageBox.Show("Please input Data in words in hexadecimal form!","Information");
                    return;
            }
            WriteEPClen=Convert.ToByte(Edit_WriteEPC.Text.Length/ 2) ;
            ENum = Convert.ToByte(Edit_WriteEPC.Text.Length / 4);
            byte[] EPC = new byte[ENum];
            EPC = HexStringToByteArray(Edit_WriteEPC.Text);
            fPassWord = HexStringToByteArray(Edit_AccessCode3.Text);
            fCmdRet = StaticClassReaderB.WriteEPC_G2(ref fComAdr, fPassWord, EPC, WriteEPClen, ref ferrorcode, frmcomportindex);
              AddCmdLog("WriteEPC_G2", "Write EPC", fCmdRet);
              if (fCmdRet == 0)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Write EPC'Command Response=0x00" +
                            "(Write EPC successfully)";
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
                  MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
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
            AddCmdLog("SetReadProtect_G2", "Set Single Tag Read Protection", fCmdRet);
              if (fCmdRet==0)
              {
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Set Single Tag Read Protection'Command Response=0x00" +
                        "Set Single Tag Read Protection successfully";
              }
        }

        private void Button_SetMultiReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.SetMultiReadProtect_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
              AddCmdLog("SetMultiReadProtect_G2", "Set Single Tag Read Protection without EPC", fCmdRet);
              if (fCmdRet==0)            
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Set Single Tag Read Protection without EPC'Command Response=0x00" +
                        "(Set Single Tag Read Protection without EPC successfully)";
        }

        private void Button_RemoveReadProtect_G2_Click(object sender, EventArgs e)
        {
            if (Edit_AccessCode4.Text.Length < 8)
            {
                MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
                return;
            }
            fPassWord = HexStringToByteArray(Edit_AccessCode4.Text);
             fCmdRet=StaticClassReaderB.RemoveReadProtect_G2(ref fComAdr,fPassWord,ref ferrorcode,frmcomportindex);
              AddCmdLog("RemoveReadProtect_G2", "Reset Single Tag Read Protection", fCmdRet);
              if (fCmdRet==0)
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Reset Single Tag Read Protection'Command Response=0x00" +
                        "(Reset Single Tag Read Protection successfully)";
        }

        private void Button_CheckReadProtected_G2_Click(object sender, EventArgs e)
        {
            byte readpro=2;
              fCmdRet=StaticClassReaderB.CheckReadProtected_G2(ref fComAdr,ref readpro,ref ferrorcode,frmcomportindex);
              AddCmdLog("CheckReadProtected_G2", "Detect Single Tag Read Protection", fCmdRet);
              if (fCmdRet==0)
              {
               if (readpro==0)
              StatusBar1.Panels[0].Text =DateTime.Now.ToLongTimeString() +  " 'Detect Single Tag Read Protection'Command Response=0x00" +
                        "(Single Tag is unprotected)";
               if (readpro==1)
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Detect Single Tag Read Protection'Command Response=0x01" +
                        "(Single Tag is protected)";
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
                MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
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
              AddCmdLog("SetEASAlarm_G2", "Alarm Setting", fCmdRet);     //v2.1 change
              if (fCmdRet==0)
              {
                  if (Alarm_G2.Checked)                                //v2.1 add
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Alarm Setting'Command Response=0x00" +
                                "(Set EAS Alarm successfully)";
                  else
                      StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Alarm Setting'Command Response=0x00" +
                                "(Clear EAS Alarm successfully)";
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
                button4.Text = "Stop";
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
                button4.Text = "Check Alarm";
                Label_Alarm.Visible = false;                       //v2.1 add
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 'Check EAS Alarm'over";
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
                 StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Check EAS Alarm'Command Response=0x00" +
                          "(EAS alarm detected)";
                 Label_Alarm.Visible=true;                       //v2.1 add
            }
            else
            {
              Label_Alarm.Visible=false;                       //v2.1 add
              AddCmdLog("CheckEASAlarm_G2", "Check EAS Alarm", fCmdRet);
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
                 MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information");
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
              AddCmdLog("LockUserBlock_G2", "Lock User Block", fCmdRet);
              if (fCmdRet==0)
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Lock User Block'Command Response=0x00" +
                        "(Lock successfully)";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Timer_Test_.Enabled = false;
            Timer_G2_Read.Enabled = false;
            Timer_G2_Alarm.Enabled = false;
            breakflag = true;
            fAppClosed = true;
            if (radioButton22.Checked && frmcomportindex>0)
            {
                StaticClassReaderB.CloseComPort();
            }
            if (radioButton21.Checked && frmcomportindex > 0)
            {
                StaticClassReaderB.CloseNetPort(frmcomportindex);
            }
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
                AddCmdLog("Inventory", "Exit Query", 0);
                SpeedButton_Query_6B.Text = "Query ";
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
                SpeedButton_Query_6B.Text = "Stop";
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
                    AddCmdLog("Inventory", "Query tag", fCmdRet);
                  }
                  else if (fCmdRet == 0XFB) //说明还未将所有卡读取完
                  {

                      StatusBar1.Panels[0].Text =  DateTime.Now.ToLongTimeString() + " 'Query Tag'Command Response=0xFB" +
                           "(No Tag Operable)";
                  }
                  else if (fCmdRet == 0)
                      StatusBar1.Panels[0].Text =  DateTime.Now.ToLongTimeString() +  " 'Query Tag'Command Response=0x00" +
                           "(Find a Tag)";
                  else
                     AddCmdLog("Inventory", "Query Tag", fCmdRet);
                  if (fCmdRet==0xEE)
                  StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Query Tag'Command Response=0xee" +
                                "(Response Command Error)" ;
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
                MessageBox.Show("Start address or length is empty!Please input!", "Information");
                return;
             }
             Timer_6B_Read.Enabled = !Timer_6B_Read.Enabled;
             if (!Timer_6B_Read.Enabled)
             {
                 AddCmdLog("Read", "Exit Read", 0);
                 SpeedButton_Read_6B.Text = "Read ";
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
                 SpeedButton_Read_6B.Text = "Stop";
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
                MessageBox.Show("Please input in bytes in hexadecimal form!", "Information");
                return;
            }
            if ((Edit_StartAddress_6B.Text == "") | (Edit_Len_6B.Text == ""))
            {
                MessageBox.Show("Start address or length is empty!Please input!", "Information");
                return;
            }
            Timer_6B_Write.Enabled = !Timer_6B_Write.Enabled;
            if (!Timer_6B_Write.Enabled)
            {
                AddCmdLog("Wtite", "Exit Query", 0);
                SpeedButton_Write_6B.Text = "Write ";
            }
            else
            {
                SpeedButton_Write_6B.Text = "Stop";
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
              AddCmdLog("WriteCard", "Write", fCmdRet);
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
               if (MessageBox.Show(this, "permanently Lock the address Confirmed?", "Information", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                   return;
                fCmdRet=StaticClassReaderB.LockByte_6B(ref fComAdr,ID_6B,Address,ref ferrorcode,frmcomportindex);
                AddCmdLog("LockByte_6B", "Lock", fCmdRet);
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
           AddCmdLog("CheckLock_6B", "Check Lock", fCmdRet);
           if (fCmdRet==0)
           {
               if  (ReLockState==0)
               StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Check Lock'Command Response=0x00" +
                         "(The Byte is unlocked)" ;
               if  (ReLockState==1)
               StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() +  " 'Check Lock'Command Response=0x01" +
                       "(The Byte is locked)";

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
                button10.Text = "Get";
                Timer_G2_Alarm.Enabled = false;
                Timer_G2_Read.Enabled = false;
                Timer_Test_.Enabled = false;
                SpeedButton_Read_G2.Text = "Read";
                button2.Text = "Query Tag";
                button4.Text = "Check Alarm";
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
                SpeedButton_Query_6B.Text = "Query";
                SpeedButton_Read_6B.Text = "Read";
                SpeedButton_Write_6B.Text ="Write";
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
                    button18.Enabled = ComOpen;
                    button19.Enabled = ComOpen;
            
        }

        private void Edit_CmdComAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void Edit_Len_6B_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = ("0123456789".IndexOf(Char.ToUpper(e.KeyChar)) < 0);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex == 0)
            {   
                radioButton5.Enabled = false;
                radioButton6.Enabled = false;
                radioButton7.Enabled = false;
                radioButton8.Enabled = false;
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;
                radioButton11.Enabled = false;
                radioButton12.Enabled = false;
                radioButton13.Enabled = false;
                radioButton14.Enabled = false;
                radioButton15.Enabled = false;
                radioButton16.Enabled = false;
                radioButton17.Enabled = false;
                radioButton18.Enabled = false;
                radioButton19.Enabled = false;
                radioButton20.Enabled = false;
                textBox3.Enabled = false;
                comboBox5.Enabled = false;
                comboBox6.Enabled = false;
            }
            if ((comboBox4.SelectedIndex == 1) | (comboBox4.SelectedIndex == 2) | (comboBox4.SelectedIndex == 3))
            {
                radioButton5.Enabled = true;
                radioButton6.Enabled = true;
                radioButton7.Enabled = true;
                radioButton8.Enabled = true;
                radioButton20.Enabled = true;
                comboBox5.Items.Clear();
                if (radioButton20.Checked)
                {
                    for (int i = 1; i < 5; i++)
                        comboBox5.Items.Add(Convert.ToString(i));
                    comboBox5.SelectedIndex = 3;
                    label42.Text = "Read Byte Number:";
                }
                else
                {
                    for (int i = 1; i < 33; i++)
                        comboBox5.Items.Add(Convert.ToString(i));
                    comboBox5.SelectedIndex = 0;
                    label42.Text = "Read Word Number:";
                }

                if (radioButton7.Checked)
                {
                    radioButton16.Enabled = true;
                    radioButton17.Enabled = true;
                }
                else
                {
                    radioButton16.Enabled = false;
                    radioButton17.Enabled = false;
                }
                if (radioButton5.Checked)
                {
                    radioButton9.Enabled = true;
                    radioButton10.Enabled = true;
                    radioButton11.Enabled = true;
                    radioButton12.Enabled = true;
                    radioButton18.Enabled = true;
                    if (radioButton20.Checked)    //Syris485
                    {
                        radioButton13.Enabled = false;
                        radioButton19.Enabled = false;
                    }
                    else
                    {
                        radioButton13.Enabled = true;
                        radioButton19.Enabled = true;
                    }
                    if ((radioButton13.Checked) || (radioButton19.Checked))
                        comboBox6.Enabled = false;
                    else
                        comboBox6.Enabled = true;
                }
                else
                    comboBox6.Enabled = true;
                radioButton14.Enabled = true;
                radioButton15.Enabled = true;
                textBox3.Enabled = true;
                if (radioButton7.Checked)
                    comboBox5.Enabled = false;
                else
                    comboBox5.Enabled = true;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                if ((comboBox4.SelectedIndex == 1) | (comboBox4.SelectedIndex == 2) | (comboBox4.SelectedIndex == 3))
                {
                    radioButton9.Enabled = true;
                    radioButton10.Enabled = true;
                    radioButton11.Enabled = true;
                    radioButton12.Enabled = true;
                    radioButton13.Enabled = true;
                    radioButton18.Enabled = true;
                    if (radioButton16.Checked)
                        label41.Text = "First Word Addr(Hex):";
                    else
                        label41.Text = "First Byte Addr(Hex):";
                    if (radioButton20.Checked)
                    {
                        radioButton13.Enabled = false;
                        radioButton19.Enabled = false;
                        label41.Text = "First Byte Addr(Hex):";
                    }
                    else
                    {
                        radioButton13.Enabled = true;
                        radioButton19.Enabled = true;
                    }
                    if (radioButton7.Checked)
                    {
                        radioButton16.Enabled = true;
                        radioButton17.Enabled = true;
                        if ((radioButton13.Checked) | (radioButton19.Checked))
                        {
                            comboBox6.Enabled = false;
                        }
                        else
                        {
                            comboBox6.Enabled = true;
                        }

                    }
                    else
                    {
                        radioButton16.Enabled = false;
                        radioButton17.Enabled = false;
                        if ((radioButton13.Checked) || (radioButton19.Checked))
                            comboBox6.Enabled = false;
                        else
                            comboBox6.Enabled = true;
                        if (radioButton20.Checked)
                            label41.Text = "First Byte Addr(Hex):";
                        else
                            label41.Text = "First Word Addr(Hex):";
                    }
                }
            }
            else
            {
                radioButton9.Enabled = false;
                radioButton10.Enabled = false;
                radioButton11.Enabled = false;
                radioButton12.Enabled = false;
                radioButton13.Enabled = false;
                radioButton18.Enabled = false;
                radioButton16.Enabled = false;
                radioButton17.Enabled = false;
                radioButton19.Enabled = false;
                comboBox6.Enabled = true;
                label41.Text = "First Byte Addr(Hex)";
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if ((radioButton5.Checked)&&(comboBox4.SelectedIndex>0))
            {
                radioButton16.Enabled = true;
                radioButton17.Enabled = true;
                radioButton13.Enabled = true;
                radioButton19.Enabled = true;
                if (radioButton16.Checked)
                    label41.Text = "First Word Addr(Hex):";
                else
                    label41.Text = "First Byte Addr(Hex):";
                label42.Text = "Read Word Number:";
            }
            comboBox5.Enabled = false;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if ((comboBox4.SelectedIndex == 1) || (comboBox4.SelectedIndex == 2) || (comboBox4.SelectedIndex == 3))
            {
                if (radioButton8.Checked)
                    comboBox5.Enabled = true;
                comboBox5.Items.Clear();
                if (radioButton20.Checked)
                {
                    for (int i = 1; i < 5; i++)
                        comboBox5.Items.Add(Convert.ToString(i));
                    comboBox5.SelectedIndex = 3;
                    label42.Text = "Read Byte Number:";
                    comboBox5.Enabled = true;
                    label41.Text = "First Byte Addr(Hex):";
                }
                else
                {
                    for (int i = 1; i < 33; i++)
                        comboBox5.Items.Add(Convert.ToString(i));
                    comboBox5.SelectedIndex = 0;
                    label42.Text = "Read Word Number:";
                    label41.Text = "First Word Addr((Hex):";
                }
                if (radioButton5.Checked)
                {
                    radioButton16.Enabled = false;
                    radioButton17.Enabled = false;
                    if (radioButton20.Checked)
                    {
                        radioButton13.Enabled = false;
                        radioButton19.Enabled = false;
                    }
                    else
                    {
                        radioButton13.Enabled = true;
                        radioButton19.Enabled = true;
                    }
                }
                else
                {
                    label41.Text = "First Byte Addr((Hex):";
                    radioButton13.Enabled = false;
                    radioButton19.Enabled = false;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte Wg_mode=0;
            byte Wg_Data_Inteval;
            byte Wg_Pulse_Width;
            byte Wg_Pulse_Inteval;
            if(radioButton1.Checked)
            {
            if(radioButton3.Checked)
            Wg_mode=2;
            else
            Wg_mode= 0;
            }
            if(radioButton2.Checked)
            {
            if(radioButton3.Checked) 
            Wg_mode=3;
            else
            Wg_mode= 1;
            }
            Wg_Data_Inteval=Convert.ToByte(comboBox1.SelectedIndex);
            Wg_Pulse_Width=Convert.ToByte(comboBox3.SelectedIndex+1);
            Wg_Pulse_Inteval = Convert.ToByte(comboBox2.SelectedIndex + 1);
            fCmdRet = StaticClassReaderB.SetWGParameter(ref fComAdr, Wg_mode, Wg_Data_Inteval, Wg_Pulse_Width, Wg_Pulse_Inteval,frmcomportindex);
            AddCmdLog("SetWGParameter", "SetWGParameter", fCmdRet);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int Reader_bit0;
            int Reader_bit1;
            int Reader_bit2;
            int Reader_bit3;
            int Reader_bit4;
            byte[] Parameter = new byte[6];
            Parameter[0] = Convert.ToByte(comboBox4.SelectedIndex);
            if (radioButton5.Checked)
                Reader_bit0 = 0;
            else
                Reader_bit0 = 1;
            if (radioButton7.Checked)
                Reader_bit1 = 0;
            else
                Reader_bit1 = 1;
            if (radioButton14.Checked)
                Reader_bit2 = 0;
            else
                Reader_bit2 = 1;
            if (radioButton16.Checked)
                Reader_bit3 = 0;
            else
                Reader_bit3 = 1;
            if (radioButton20.Checked)
                Reader_bit4 = 1;
            else
                Reader_bit4 = 0;
            Parameter[1] = Convert.ToByte(Reader_bit0 * 1 + Reader_bit1 * 2 + Reader_bit2 * 4 + Reader_bit3 * 8 + Reader_bit4 * 16);
            if (radioButton9.Checked)
                Parameter[2] = 0;
            if (radioButton10.Checked)
                Parameter[2] = 1;
            if (radioButton11.Checked)
                Parameter[2] = 2;
            if (radioButton12.Checked)
                Parameter[2] = 3;
            if (radioButton13.Checked)
                Parameter[2] = 4;
            if (radioButton18.Checked)
                Parameter[2] = 5;
            if (radioButton19.Checked)
                Parameter[2] = 6;
            if (textBox3.Text == "")
            {
                MessageBox.Show("Address is NULL!", "Information");
                return;
            }
            Parameter[3] = Convert.ToByte(textBox3.Text, 16);
            Parameter[4] = Convert.ToByte(comboBox5.SelectedIndex + 1);
            Parameter[5] = Convert.ToByte(comboBox6.SelectedIndex); ;
            fCmdRet = StaticClassReaderB.SetWorkMode(ref fComAdr, Parameter, frmcomportindex);
            if (fCmdRet == 0)
            {
                if ((comboBox4.SelectedIndex == 1) | (comboBox4.SelectedIndex == 2) | (comboBox4.SelectedIndex == 3))
                {
                    if (radioButton6.Checked)
                    {
                        radioButton13.Enabled = false;
                        radioButton19.Enabled = false;
                    }
                    else
                    {
                        if (radioButton20.Checked)
                        {
                            radioButton13.Enabled = false;
                            radioButton19.Enabled = false;
                        }
                    }
                    button10.Enabled = true;
                    button11.Enabled = true;
                }
                if (comboBox4.SelectedIndex == 0)
                {
                    button10.Enabled = false;
                    button11.Enabled = false;
                    button10.Text = "Get";
                    timer1.Enabled = false;
                }
            }
            AddCmdLog("SetWorkMode", "SetWorkMode", fCmdRet);
        }


        private void button10_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            if (!timer1.Enabled)
            {
                button10.Text = "Get";
            }
            else
            {
                button10.Text = "Stop";
            }
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
              temp = "";
              temps = ByteArrayToHexString(ScanModeData);
              for (i = 0; i < ValidDatalength; i++)
              {
                  temp = temp + temps.Substring(i * 2, 2) + " ";
              }
              if (ValidDatalength>0)
              listBox3.Items.Add(temp);
              listBox3.SelectedIndex = listBox3.Items.Count - 1;
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 操作成功";
          }
          else
              StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + " 操作失败";
          
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

        private void button11_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
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

        private void radioButton16_CheckedChanged(object sender, EventArgs e)
        {
            label41.Text = "First Word Addr";
        }

        private void radioButton17_CheckedChanged(object sender, EventArgs e)
        {
            label41.Text = "First Byte Addr";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = true;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = true;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = true;
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = true;
        }

        private void radioButton18_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = true;
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            byte[] Parameter = new byte[12];

            fCmdRet = StaticClassReaderB.GetWorkModeParameter(ref fComAdr, Parameter, frmcomportindex);
            if (fCmdRet == 0)
            {
                if (Parameter[0] == 0)
                {
                    radioButton1.Checked = true;
                    radioButton4.Checked = true;
                }
                if (Parameter[0] == 1)
                {
                    radioButton2.Checked = true;
                    radioButton4.Checked = true;
                }
                if (Parameter[0] == 2)
                {
                    radioButton1.Checked = true;
                    radioButton3.Checked = true;
                }
                if (Parameter[0] == 3)
                {
                    radioButton2.Checked = true;
                    radioButton3.Checked = true;
                }
                comboBox1.SelectedIndex = Convert.ToInt32(Parameter[1]);
                comboBox2.SelectedIndex = Convert.ToInt32(Parameter[3] - 1);
                comboBox3.SelectedIndex = Convert.ToInt32(Parameter[2] - 1);
                comboBox4.SelectedIndex = Convert.ToInt32(Parameter[4]);
                if ((Parameter[4] == 1) || (Parameter[4] == 2) || (Parameter[4] == 3))
                {
                    button10.Enabled = true;
                    button11.Enabled = true;
                    radioButton5.Enabled = true;
                    radioButton6.Enabled = true;
                    radioButton7.Enabled = true;
                    radioButton8.Enabled = true;
                   
                    if (radioButton5.Checked)
                    {
                        if (radioButton7.Checked)
                        {
                            radioButton16.Enabled = true;
                            radioButton17.Enabled = true;
                        }
                        else
                        {
                            radioButton16.Enabled = false;
                            radioButton17.Enabled = false;
                        }
                        radioButton9.Enabled = true;
                        radioButton10.Enabled = true;
                        radioButton11.Enabled = true;
                        radioButton12.Enabled = true;
                        radioButton18.Enabled = true;
                        radioButton20.Enabled = true;
                        if (Convert.ToInt32((Parameter[5] & 0x10)) == 0x10)
                        {
                            radioButton13.Enabled = false;
                            radioButton19.Enabled = false;
                        }
                        else
                        {
                            radioButton13.Enabled = true;
                            radioButton19.Enabled = true;
                        }
                        if ((radioButton13.Checked) || (radioButton19.Checked))
                            comboBox6.Enabled = false;
                        else
                            comboBox6.Enabled = true;
                    }
                    else
                        comboBox6.Enabled = true;
                    radioButton14.Enabled = true;
                    radioButton15.Enabled = true;
                    textBox3.Enabled = true;
                    if ((radioButton8.Checked) || (radioButton20.Checked))
                        comboBox5.Enabled = true;
                }
                if (Parameter[4] == 0)
                {
                    button10.Enabled = false;
                    button11.Enabled = false;
                    radioButton5.Enabled = false;
                    radioButton6.Enabled = false;
                    radioButton7.Enabled = false;
                    radioButton8.Enabled = false;
                    radioButton9.Enabled = false;
                    radioButton10.Enabled = false;
                    radioButton11.Enabled = false;
                    radioButton12.Enabled = false;
                    radioButton13.Enabled = false;
                    radioButton14.Enabled = false;
                    radioButton15.Enabled = false;
                    radioButton16.Enabled = false;
                    radioButton17.Enabled = false;
                    radioButton18.Enabled = false;
                    radioButton19.Enabled = false;
                    radioButton20.Enabled = false;
                    textBox3.Enabled = false;
                    comboBox5.Enabled = false;
                    comboBox6.Enabled = false;
                }
                if (Convert.ToInt32((Parameter[5]) & 0x01) == 0)
                    radioButton5.Checked = true;
                else
                    radioButton6.Checked = true;
                if (Convert.ToInt32((Parameter[5]) & 0x02) == 0)
                    radioButton7.Checked = true;
                else
                {
                    if (Convert.ToInt32((Parameter[5] & 0x10)) == 0)
                        radioButton8.Checked = true;
                    else
                        radioButton20.Checked = true;
                }
                if (Convert.ToInt32((Parameter[5]) & 0x04) == 0)
                    radioButton14.Checked = true;
                else
                    radioButton15.Checked = true;
                if (Convert.ToInt32((Parameter[5]) & 0x08) == 0)
                    radioButton16.Checked = true;
                else
                    radioButton17.Checked = true;
                switch (Parameter[6])
                {
                    case 0:
                        radioButton9.Checked = true;
                        break;
                    case 1:
                        radioButton10.Checked = true;
                        break;
                    case 2:
                        radioButton11.Checked = true;
                        break;
                    case 3:
                        radioButton12.Checked = true;
                        break;
                    case 4:
                        radioButton13.Checked = true;
                        break;
                    case 5:
                        radioButton18.Checked = true;
                        break;
                    case 6:
                        radioButton19.Checked = true;
                        break;
                    default:
                        break;
                }
                textBox3.Text = Convert.ToString(Parameter[7], 16).PadLeft(2, '0');
                comboBox5.SelectedIndex = Convert.ToInt32(Parameter[8] - 1);
                comboBox6.SelectedIndex = Convert.ToInt32(Parameter[9]);
                comboBox7.SelectedIndex = Convert.ToInt32(Parameter[10]);
                comboBox_OffsetTime.SelectedIndex = Convert.ToInt32(Parameter[11]);
            }
            AddCmdLog("GetWorkModeParameter", "GetWorkModeParameter", fCmdRet);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            byte Accuracy;
            Accuracy=Convert.ToByte(comboBox7.SelectedIndex);
             fCmdRet=StaticClassReaderB.SetAccuracy(ref fComAdr,Accuracy,frmcomportindex);
             AddCmdLog("SetAccuracy", "SetAccuracy", fCmdRet);
        }

        private void radioButton19_CheckedChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = false;
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
              ComboBox_baud2.SelectedIndex = 3;
            }
            else
            {
              ComboBox_baud2.Items.Add("Auto");
              ComboBox_baud2.SelectedIndex = 0;
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button_OffsetTime_Click(object sender, EventArgs e)
        {
            byte OffsetTime;
            OffsetTime = Convert.ToByte(comboBox_OffsetTime.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetOffsetTime(ref fComAdr, OffsetTime, frmcomportindex);
            AddCmdLog("SetOffsetTime", "SetOffsetTime", fCmdRet);
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
            Maskadr = Convert.ToByte(maskadr_textbox.Text,16);
            MaskLen = Convert.ToByte(maskLen_textBox.Text,16);
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
                MessageBox.Show("Address of Tag Data is NULL", "Information");
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information");
                return;
            }
            if (Convert.ToInt32(Edit_WordPtr.Text,16) + Convert.ToInt32(textBox1.Text) > 120)
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
                MessageBox.Show("The Number must be 4 times.", "Wtite");
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
            AddCmdLog("Write Block", "WriteBlock", fCmdRet, ferrorcode);
            if (fCmdRet == 0)
            {
                StatusBar1.Panels[0].Text = DateTime.Now.ToLongTimeString() + "'WriteBlock'Command Response=0x00" +
                     "(completely write Data successfully)";
            }    
        }

        private void button13_Click(object sender, EventArgs e)
        {
           Byte dminfre, dmaxfre,Ffenpin;
           int i, j, CardNum, Totallen, UID_index, n_index;
          byte[] EPC=new byte[5000];
          string temp1,temp2,temp3,temp4;
          float ncount;
          byte AdrTID = 0;
          byte LenTID = 0;
          byte TIDFlag = 0;
          byte band = 0;
          int m_max = 0;
          button13.Enabled=false;
          button16.Enabled=true;
          button18.Enabled = false;
          button19.Enabled = false;
          listBox4.Items.Clear();
          breakflag = false;
          if (radioButton_band1.Checked)
          { 
               m_max=62;
               band=0;
          }
          if (radioButton_band2.Checked)
          {
              m_max = 19;
              band = 1;
          }
          if (radioButton_band3.Checked)
          {
              m_max = 49;
              band = 2;
          }
          if (radioButton_band4.Checked)
          {
              m_max = 31;
              band = 3;
          }
          if (radioButton_band5.Checked)
          {
              m_max = 14;
              band = 4;
          }
          for (Ffenpin = 0; Ffenpin < m_max; Ffenpin++)
             {
               if(breakflag==true)
               {
                   breakflag=false;
                   if (fAppClosed )
                       Close();
                   return;
               }

              dminfre =Convert.ToByte(((band & 3)<< 6)| (Ffenpin & 0x3F) );
              dmaxfre = Convert.ToByte(((band & 0x0c) << 4) | (Ffenpin & 0x3F));
               if(radioButton_band1.Checked)
                y_f=Convert.ToDouble(902.6+(Ffenpin & 0x3F)*0.4);
                if(radioButton_band2.Checked)
                y_f=Convert.ToDouble(920.125+(Ffenpin & 0x3F)*0.25);
                if(radioButton_band3.Checked)
                y_f=Convert.ToDouble(902.75+(Ffenpin & 0x3F)*0.5);
                if(radioButton_band4.Checked)
                y_f=Convert.ToDouble(917.1+(Ffenpin & 0x3F)*0.2);
                if(radioButton_band5.Checked)
                y_f=Convert.ToDouble(865.1+(Ffenpin & 0x3F)*0.2);

                temp4 = Convert.ToString(y_f);
                temp3 = temp4.PadRight(5, ' ') + "MHz" + "(" + Convert.ToString(Ffenpin).PadLeft(2, ' ') + ")";
                listBox4.Items.Add(temp3);
               for (i=0;i< 4;i++)
               {
                   fCmdRet = StaticClassReaderB.Writedfre(ref fComAdr,ref dmaxfre,ref dminfre,frmcomportindex);
                   if(fCmdRet==0)
                   break;
               }
               ncount=0;
               for (j=0;j< 30;j++)
               {
                 Application.DoEvents();
                 if(breakflag)
                 {
                     breakflag=false;
                     if (fAppClosed )
                     {
                         Close();
                     }
                     return;
                 }
                 CardNum=0;
                 Totallen = 0;
                 fCmdRet = StaticClassReaderB.Inventory_G2(ref fComAdr,AdrTID,LenTID,TIDFlag, EPC, ref Totallen, ref CardNum, frmcomportindex);
                 if((fCmdRet==1) ||(fCmdRet==2)||(fCmdRet==3)||(fCmdRet==4))
                 {
                    ncount=ncount+1;
                    if(ncount==1)
                        UID_index = listBox4.Items.IndexOf(temp3);
                    else
                        UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount - 1).PadLeft(2, ' ') + "/30");
                    if (UID_index>=0)
                    {
                        listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30";
                    }
                 }
               }
               if(ncount==0)
               {
                   UID_index = listBox4.Items.IndexOf(temp3);
                  if (UID_index>=0)
                      listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30" + "                                " + "00.00%";
               }
               UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30");
                if (UID_index>=0)
                {
                  x_z=((ncount/30)*100);
                  temp1= Convert.ToString(x_z);
                  if(ncount==30)
                  temp2="100.00%";
                  else
                  {
                      n_index = temp1.IndexOf('.');
                  //temp2:=Copy(temp1,1,2)+'.'+copy(temp1,3,2)+'%';
                   if(n_index>0)
                    temp2=temp1.Substring(0,n_index)+"."+temp1.Substring(n_index+1,2)+"%";
                   else
                    temp2= temp1+"."+"00"+"%";
                  // temp2:=Copy(temp1,1,2)+'.'+copy(temp1,3,2)+'%';
                  }
                  listBox4.Items[UID_index] = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, ' ') + "/30" + "                                " + temp2;
                 }
                 listBox4.SelectedIndex = listBox4.Items.Count-1;
             }
            button13.Enabled=true ;
            button16.Enabled=false;
            button18.Enabled = true;
            button19.Enabled = true;
        }

        private void button16_Click(object sender, EventArgs e)
        {
             breakflag=true;
             button13.Enabled=true;
             button16.Enabled=false;
             button18.Enabled = true;
             button19.Enabled = true;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button17_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            byte FlashMode;
            FlashMode = Convert.ToByte(comboBox8.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetFhssMode(ref fComAdr, FlashMode, frmcomportindex);
            AddCmdLog("SetFhssMode", "SetFhssMode", fCmdRet);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            byte FlashMode = 0;
            fCmdRet = StaticClassReaderB.GetFhssMode(ref fComAdr, ref FlashMode, frmcomportindex);
            if (fCmdRet == 0)
            {
                comboBox8.SelectedIndex = FlashMode;
            }
            AddCmdLog("GetFhssMode", "GetFhssMode", fCmdRet);
        }

        private void radioButton_band5_CheckedChanged(object sender, EventArgs e)
        {
            int i;
            ComboBox_dminfre.Items.Clear();
            ComboBox_dmaxfre.Items.Clear();
            for (i = 0; i < 15; i++)
            {
                ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
                ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz");
            }
            ComboBox_dmaxfre.SelectedIndex = 14;
            ComboBox_dminfre.SelectedIndex = 0;
        }

        private void button_settigtime_Click(object sender, EventArgs e)
        {
            byte TriggerTime;
            TriggerTime = Convert.ToByte(comboBox_tigtime.SelectedIndex);
            fCmdRet = StaticClassReaderB.SetTriggerTime(ref fComAdr, ref TriggerTime, frmcomportindex);
            AddCmdLog("SetTriggerTime", "Set TriggerTime", fCmdRet);
        }

        private void button_gettigtime_Click(object sender, EventArgs e)
        {
            byte TriggerTime;
            TriggerTime = 255;
            fCmdRet = StaticClassReaderB.SetTriggerTime(ref fComAdr, ref TriggerTime, frmcomportindex);
            if (fCmdRet == 0)
            {
                comboBox_tigtime.SelectedIndex = TriggerTime;
            }
            AddCmdLog("SetTriggerTime", "Get TriggerTime", fCmdRet);
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

        private void CheckBox_TID_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_TID.Checked)
            {
                groupBox33.Enabled = true;
                textBox4.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                groupBox33.Enabled = false;
                textBox4.Enabled = false;
                textBox4.Enabled = false;
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

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button20_Click(object sender, EventArgs e)
        {
            byte RelayStatus=0;
            if(comboBox9.SelectedIndex ==0)
            RelayStatus= Convert.ToByte(RelayStatus | 0);
            else
            RelayStatus= Convert.ToByte(RelayStatus | 1);
            if (comboBox10.SelectedIndex == 0)
            RelayStatus= Convert.ToByte(RelayStatus | 0);
            else
            RelayStatus= Convert.ToByte(RelayStatus | 2);
            fCmdRet = StaticClassReaderB.SetRelay(ref fComAdr, RelayStatus, frmcomportindex);
            AddCmdLog("SetRelay", "Set", fCmdRet);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            string str="";
            Information.IP = "";
            Information.usename = "";
            Information.dsname = "";
            Information.mac = "";
            Information.portnum = "";
            Information.tup = "";
            Information.rm = "";
            Information.cm = "";
            Information.ct = "";
            Information.fc = "";
            Information.dt = "";
            Information.br = "";
            Information.pr = "";
            Information.bb = "";
            Information.rc = "";
            Information.ml = "";
            Information.md = "";
            Information.di = "";
            Information.dp = "";
            Information.gi = "";
            Information.nm = "";
            byte[] data = new byte[1024];
            ListViewItem aListItem = new ListViewItem();
            try
           {
	           Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输
                IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 65535);//初始化一个发送广播和指定端口的网络端口实例
                EndPoint ep = (EndPoint)iep;
                sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式
                string request = "X";//初始化需要发送的数据
                byte[] buffer = Encoding.ASCII.GetBytes(request); 
                sock.SendTo(buffer, iep);
                for(int i=0;i<255;i++)
                {
                    byte[] buffer1 = new byte[1000];
                    sock.ReceiveTimeout = 10;
                    int m_count = sock.ReceiveFrom(buffer1, ref ep);
                    if (m_count > 0)
                    {
                        aListItem = listView1.Items.Add((listView1.Items.Count + 1).ToString());
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                        aListItem.SubItems.Add("");
                        byte[] buffer2 = new byte[m_count];
                        Array.Copy(buffer1, buffer2, m_count);
                        string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                        if (fRecvIDPstring.Substring(0, 1) == "A")
                        {
                            int m = fRecvIDPstring.IndexOf('/');
                            Information.mac = fRecvIDPstring.Substring(1, m - 1);
                            Information.portnum = fRecvIDPstring.Substring(m + 1, 4);
                            m = fRecvIDPstring.IndexOf('*');
                            int n = fRecvIDPstring.Length - m - 8;
                            string IDPstring = fRecvIDPstring.Substring(m + 8, n);
                            Information.usename = IDPstring.Substring(0, IDPstring.IndexOf('/'));
                            Information.dsname = IDPstring.Substring(IDPstring.IndexOf('/') + 1, IDPstring.Length - IDPstring.IndexOf('/') - 1);
                            Information.IP = ep.ToString().Substring(0, ep.ToString().IndexOf(':'));
                            if (((Information.usename == "") && (Information.dsname == "")) || (Information.dsname == "/"))
                            {
                                str = "";
                            }
                            else
                            {
                                str = Information.usename + '/' + Information.dsname;
                            }
                            aListItem.SubItems[1].Text = Information.mac;
                            aListItem.SubItems[2].Text = Information.IP;
                            aListItem.SubItems[3].Text = str;
                        }
                    }  
                }
                sock.Close();
           }
           catch (System.Exception ex)
           { 
               ex.ToString();
               return;
           }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Information.IP = "";
            Information.usename = "";
            Information.dsname = "";
            Information.mac = "";
            Information.portnum = "";
            Information.tup = "";
            Information.rm = "";
            Information.cm = "";
            Information.ct = "";
            Information.fc = "";
            Information.dt = "";
            Information.br = "";
            Information.pr = "";
            Information.bb = "";
            Information.rc = "";
            Information.ml = "";
            Information.md = "";
            Information.di = "";
            Information.dp = "";
            Information.gi = "";
            Information.nm = "";
            locateForm loginform = new locateForm();
            DialogResult result = loginform.ShowDialog();
             if (result == DialogResult.OK)
             {
                 try
                 {
	                 string IPAddr=loginform.IP1+"."+loginform.IP2+"."+loginform.IP3+"."+loginform.IP4;
	                 listView1.Items.Clear();
	                 string str = "";
	                 byte[] data = new byte[1024];
	                 ListViewItem aListItem = new ListViewItem();
	                 Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输
                     
	                 IPEndPoint iep = new IPEndPoint(IPAddress.Broadcast, 65535);//初始化一个发送广播和指定端口的网络端口实例
	                 EndPoint ep = (EndPoint)iep;
	                 sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式
	                 string request = "X";//初始化需要发送的数据
	                 byte[] buffer = Encoding.ASCII.GetBytes(request);
	                 sock.SendTo(buffer, iep);
	                 for (int i = 0; i < 3; i++)
	                 {
	                     byte[] buffer1 = new byte[1000];
	                     sock.ReceiveTimeout = 10;
                         iep = new IPEndPoint(IPAddress.Parse(IPAddr), 65535);
	                     int m_count = sock.ReceiveFrom(buffer1, ref ep);
	                     if (m_count > 0)
	                     {
	                         aListItem = listView1.Items.Add((listView1.Items.Count + 1).ToString());
	                         aListItem.SubItems.Add("");
	                         aListItem.SubItems.Add("");
	                         aListItem.SubItems.Add("");
	                         byte[] buffer2 = new byte[m_count];
	                         Array.Copy(buffer1, buffer2, m_count);
	                         string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
	                         if (fRecvIDPstring.Substring(0, 1) == "A")
	                         {
	                             int m = fRecvIDPstring.IndexOf('/');
                                 Information.mac = fRecvIDPstring.Substring(1, m - 1);
                                 Information.portnum = fRecvIDPstring.Substring(m + 1, 4);
	                             m = fRecvIDPstring.IndexOf('*');
	                             int n = fRecvIDPstring.Length - m - 8;
	                             string IDPstring = fRecvIDPstring.Substring(m + 8, n);
                                 Information.usename = IDPstring.Substring(0, IDPstring.IndexOf('/'));
                                 Information.dsname = IDPstring.Substring(IDPstring.IndexOf('/') + 1, IDPstring.Length - IDPstring.IndexOf('/') - 1);
                                 Information.IP = ep.ToString().Substring(0, ep.ToString().IndexOf(':'));
                                 if (((Information.usename == "") && (Information.dsname == "")) || (Information.dsname == "/"))
	                             {
	                                 str = "";
	                             }
	                             else
	                             {
                                     str = Information.usename + '/' + Information.dsname;
	                             }
                                 if (IPAddr == Information.IP)
                                 {
                                     aListItem.SubItems[1].Text = Information.mac;
                                     aListItem.SubItems[2].Text = Information.IP;
                                     aListItem.SubItems[3].Text = str;
                                 }      
	                         }
	                         break;
	                     }
	
	                 }
	                 sock.Close();
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                }
             }
             loginform.Dispose();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            string IPAddr="";
            ChangeIPdlg changeIPdlg = new ChangeIPdlg();
            DialogResult result = changeIPdlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                IPAddr = changeIPdlg.IP1 + "." + changeIPdlg.IP2 + "." + changeIPdlg.IP3 + "." + changeIPdlg.IP4;
            }
            else
            {
                return;
            }
            try
            {
                if (listView1.SelectedIndices.Count > 0
                    && listView1.SelectedIndices[0] != -1)
                {
                    Information.IP = listView1.SelectedItems[0].SubItems[2].Text;
                    Information.mac = listView1.SelectedItems[0].SubItems[1].Text;
                   Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输
                   IPEndPoint iep = new IPEndPoint(IPAddress.Parse(Information.IP), 65535);//初始化一个发送广播和指定端口的网络端口实例
                   EndPoint ep = (EndPoint)iep;
              //   sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式
                   string request = "X";//初始化需要发送的数据
                   byte[] buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(100);
                   
                   request = "L";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep); 
                   Thread.Sleep(50);

                   request = "SIP" + IPAddr + "|34";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(10);

                   request = "E|35";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(200);
                   
                   iep = new IPEndPoint(IPAddress.Broadcast, 65535);//初始化一个发送广播和指定端口的网络端口实例
                   sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
                   request = "W" + Information.mac;
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(100);

                   request = "L";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(50);

                   request = "SIP" + IPAddr + "|34";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   Thread.Sleep(100);

                   request = "E|35";
                   buffer = Encoding.ASCII.GetBytes(request);
                   sock.SendTo(buffer, iep);
                   sock.Close();
                   listView1.Items.Clear();
                }
                else
                {
                    MessageBox.Show("No select device!","Information");
                }
                
            }
            catch (Exception ex)
            {
                ex.ToString(); 
            }
            changeIPdlg.Dispose();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            int i=0;
            try
            {
                if (listView1.SelectedIndices.Count > 0
                    && listView1.SelectedIndices[0] != -1)
                {
                   Information.IP = listView1.SelectedItems[0].SubItems[2].Text;
                   Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输
                
                   IPEndPoint iep = new IPEndPoint(IPAddress.Parse(Information.IP), 65535);//初始化一个发送广播和指定端口的网络端口实例
                   EndPoint ep = (EndPoint)iep;
                   sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式
                   string request = "B";//初始化需要发送的数据
                   byte[] buffer = Encoding.ASCII.GetBytes(request);
                   while(i<3)
                   {
                        sock.SendTo(buffer, iep);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            i = 3;
                            sock.Close();
                            break;
                        }
                        i = i + 1;
                   } 
                }
                else
                {
                    MessageBox.Show("No select device!","Information");
                }
            }
            catch (Exception ex)
            {
                ex.ToString(); 
            }
        }
       
        private void button24_Click(object sender, EventArgs e)
        {
            int i = 0;
            string IPaddr = "";
            try
            {
                if (listView1.SelectedIndices.Count > 0
                    && listView1.SelectedIndices[0] != -1)
                {
                   IPaddr = listView1.SelectedItems[0].SubItems[2].Text;
                     Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);//初始化一个Scoket实习,采用UDP传输

                     IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPaddr), 65535);//初始化一个发送广播和指定端口的网络端口实例
                    EndPoint ep= (EndPoint)iep;
                    sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);//设置该scoket实例的发送形式
                    string request = "X";//初始化需要发送的数据
                    byte[] buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        
                        sock.SendTo(buffer, iep);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
	                         byte[] buffer2 = new byte[m_count];
	                         Array.Copy(buffer1, buffer2, m_count);
	                         string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                             if (fRecvIDPstring.Substring(0, 1) == "A")
                             {
                                 int m = fRecvIDPstring.IndexOf('/');
                                 Information.mac = fRecvIDPstring.Substring(1, m - 1);
                                 Information.portnum = fRecvIDPstring.Substring(m + 1, 4);
                                 m = fRecvIDPstring.IndexOf('*');
                                 int n = fRecvIDPstring.Length - m - 8;
                                 string IDPstring = fRecvIDPstring.Substring(m + 8, n);
                                 Information.usename = IDPstring.Substring(0, IDPstring.IndexOf('/'));
                                 Information.dsname = IDPstring.Substring(IDPstring.IndexOf('/') + 1, IDPstring.Length - IDPstring.IndexOf('/') - 1);
                                 Information.IP = ep.ToString().Substring(0, ep.ToString().IndexOf(':'));
                                 i = 3;
                                 break;
                             }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "W" + Information.mac;//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(100);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "L";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(50);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GON|1";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m+1,1)=="1")
                                {
                                    Information.usename = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GDN|2";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "2")
                                {
                                    Information.dsname = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GFE|3";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "3")
                                {
                                    Information.mac = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GIP|4";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "4")
                                {
                                    Information.IP = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GPN|5";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "5")
                                {
                                    Information.portnum = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GTP|6";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "6")
                                {
                                    Information.tup = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GRM|7";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "7")
                                {
                                    Information.rm = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GCM|8";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "8")
                                {
                                    Information.cm = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GCT|9";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 1) == "9")
                                {
                                    Information.ct = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GFC|10";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "10")
                                {
                                    Information.fc = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GDT|11";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "11")
                                {
                                    Information.dt = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GBR|12";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "12")
                                {
                                    Information.br = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GPR|13";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "13")
                                {
                                    Information.pr = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GBB|14";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "14")
                                {
                                    Information.bb = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GRC|15";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "15")
                                {
                                    Information.rc = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GML|16";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "16")
                                {
                                    Information.ml = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GMD|17";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "17")
                                {
                                    Information.md = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GDI|18";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "18")
                                {
                                    Information.di = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GDP|19";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "19")
                                {
                                    Information.dp = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GGI|20";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "20")
                                {
                                    Information.gi = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    i = 0;
                    request = "GNM|21";//初始化需要发送的数据
                    buffer = Encoding.ASCII.GetBytes(request);
                    while (i < 3)
                    {
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        byte[] buffer1 = new byte[1000];
                        sock.ReceiveTimeout = 10;
                        int m_count = sock.ReceiveFrom(buffer1, ref ep);
                        if (m_count > 0)
                        {
                            byte[] buffer2 = new byte[m_count];
                            Array.Copy(buffer1, buffer2, m_count);
                            string fRecvIDPstring = Encoding.ASCII.GetString(buffer2);
                            if (fRecvIDPstring.Substring(0, 1) == "A")
                            {
                                int m = fRecvIDPstring.IndexOf('|');
                                if (fRecvIDPstring.Substring(m + 1, 2) == "21")
                                {
                                    Information.nm = fRecvIDPstring.Substring(1, m - 1);
                                }
                                i = 3;
                                break;
                            }
                        }
                        i = i + 1;
                    }

                    if ((Information.nm == "") || (Information.mac == "") || (Information.IP == "") || (Information.portnum == "") ||
                        (Information.br == "") || (Information.bb == "") || (Information.dt == "") || (Information.rm == "") ||
                        (Information.tup == "") || (Information.pr == "") || (Information.fc == "") || (Information.di == "") || (Information.dp == "") || (Information.gi == ""))
                    {
                        MessageBox.Show("Please check device and PC connection status!", "Information");
                        return;
                    }
                    fSetdlg fsetdlg= new fSetdlg();
                    fsetdlg._IP(Information.IP);
                    fsetdlg._bb(Information.bb);
                    fsetdlg._br(Information.br);
                    fsetdlg._di(Information.di);
                    fsetdlg._dp(Information.dp);
                    fsetdlg._dsname(Information.dsname);
                    fsetdlg._dt(Information.dt);
                    fsetdlg._fc(Information.fc);
                    fsetdlg._gi(Information.gi);
                    fsetdlg._MAC(Information.mac);
                    fsetdlg._nm(Information.nm);
                    fsetdlg._portnum(Information.portnum);
                    fsetdlg._pr(Information.pr);
                    fsetdlg._rm(Information.rm);
                    fsetdlg._tup(Information.tup);
                    fsetdlg._usename(Information.usename);
                    DialogResult result= fsetdlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        request = "L";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(50);

                        request = "SON" + Information.usename + "|18";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDN" + Information.dsname + "|19";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "STP" + Information.tup + "|20";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SPN" + Information.portnum + "|21";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SRM" + Information.rm + "|22";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SFC" + Information.fc + "|23";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDT" + Information.dt + "|24";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SBR" + Information.br + "|25";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SPR" + Information.pr + "|26";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SBB" + Information.bb + "|27";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SRC" + Information.rc + "|28";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SCM" + Information.cm + "|29";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SCT" + Information.ct + "|30";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SML" + Information.ml + "|31";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SMD" + Information.md + "|32";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDI" + Information.di + "|33";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDP" + Information.dp + "|34";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SGI" + Information.gi + "|35";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SNM" + Information.nm + "|36";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SIP" + Information.IP + "|37";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "E";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(500);

                        iep = new IPEndPoint(IPAddress.Broadcast, 65535);//初始化一个发送广播和指定端口的网络端口实例
                        sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

                        request = "W" + Information.mac;//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(100);

                        request = "L";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(50);

                        request = "SON" + Information.usename + "|18";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDN" + Information.dsname + "|19";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "STP" + Information.tup + "|20";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SPN" + Information.portnum + "|21";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SRM" + Information.rm + "|22";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SFC" + Information.fc + "|23";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDT" + Information.dt + "|24";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SBR" + Information.br + "|25";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SPR" + Information.pr + "|26";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SBB" + Information.bb + "|27";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SRC" + Information.rc + "|28";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SCM" + Information.cm + "|29";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SCT" + Information.ct + "|30";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SML" + Information.ml + "|31";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SMD" + Information.md + "|32";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDI" + Information.di + "|33";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SDP" + Information.dp + "|34";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        
                        request = "SGI" + Information.gi + "|35";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SNM" + Information.nm + "|36";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);

                        request = "SIP" + Information.IP + "|33";//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        
                        request = "E" ;//初始化需要发送的数据
                        buffer = Encoding.ASCII.GetBytes(request);
                        sock.SendTo(buffer, iep);
                        Thread.Sleep(10);
                        listView1.Items.Clear();
                    }
                    fsetdlg.Dispose();
                    sock.Close();
                }
                else
                {
                    MessageBox.Show("No select device!", "Information");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
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
                MessageBox.Show("Config error!", "information");
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
                MessageBox.Show("TCPIP error", "Information");
                StaticClassReaderB.CloseNetPort(frmcomportindex);
                ComOpen = false;
                return;
            }
            if ((fOpenComIndex != -1) && (openresult != 0X35) && (openresult != 0X30))
            {
                Button3.Enabled = true;
                button20.Enabled = true;
                Button5.Enabled = true;
                Button1.Enabled = true;
                button2.Enabled = true;
                Button_WriteEPC_G2.Enabled = true;
                Button_SetMultiReadProtect_G2.Enabled = true;
                Button_RemoveReadProtect_G2.Enabled = true;
                Button_CheckReadProtected_G2.Enabled = true;
                button4.Enabled = true;
                SpeedButton_Query_6B.Enabled = true;
                button6.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button12.Enabled = true;
                button_OffsetTime.Enabled = true;
                button_settigtime.Enabled = true;
                button_gettigtime.Enabled = true;
                ComOpen = true;
            }
            if ((fOpenComIndex == -1) && (openresult == 0x30))
                MessageBox.Show("TCPIP Communication Error", "Information");
            RefreshStatus();
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
                button20.Enabled = false;
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
                button6.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;

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
                button2.Text = "Stop";
                checkBox1.Enabled = false;

                SpeedButton_Read_6B.Enabled = false;
                SpeedButton_Write_6B.Enabled = false;
                Button14.Enabled = false;
                Button15.Enabled = false;
                ListView_ID_6B.Items.Clear();
                ComOpen = false;
                button12.Enabled = false;
                button10.Text = "Get";
                button10.Enabled = false;
                button11.Enabled = false;
                timer1.Enabled = false;
                comboBox4.SelectedIndex = 0;
                button_OffsetTime.Enabled = false;
                button_settigtime.Enabled = false;
                button_gettigtime.Enabled = false;
            }
        }

        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            OpenPort.Enabled = true;
            ClosePort.Enabled = true;
            OpenNetPort.Enabled = false;
            CloseNetPort.Enabled = false;
            CloseNetPort_Click(sender, e);
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
            if (ComboBox_AlreadyOpenCOM.Items.Count > 0)
                ClosePort_Click(sender, e);
            OpenPort.Enabled = false;
            ClosePort.Enabled = false;
            OpenNetPort.Enabled = true;
            CloseNetPort.Enabled = true;
        }

    }
}