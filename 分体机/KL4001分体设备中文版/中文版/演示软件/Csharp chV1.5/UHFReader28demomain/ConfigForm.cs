using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace UHFReader28demomain
{
    public partial class ConfigForm : Form
    {
        private AutoPropertyClass AutoProperty;
        private DeviceClass CurrentDevice;

        public ConfigForm(DeviceClass SelectedDevice)
        {
            InitializeComponent();

            Debug.Assert(SelectedDevice != null, "Invalid SelectedDevice!");
            this.CurrentDevice = SelectedDevice;

            //暂时不支持以下界面；  
            this.tabControl.TabPages.Remove(this.tabPage_PPP);
            this.tabControl.TabPages.Remove(this.tabPage_PPPoE);
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            this.AutoProperty = new AutoPropertyClass(CurrentDevice.DevHandle);

            //Basic Settings;
            this.AutoProperty.AddPropertyParaMap("ServerName", DevControl.PARA_TYPES.DEVICENAME);
            this.AutoProperty.AddPropertyParaMap("TimeZone", DevControl.PARA_TYPES.TIMEZONE);
            this.AutoProperty.AddPropertyParaMap("TimeYear", DevControl.PARA_TYPES.LOCALTIME_YEAR);
            this.AutoProperty.AddPropertyParaMap("TimeMon", DevControl.PARA_TYPES.LOCALTIME_MONTH);
            this.AutoProperty.AddPropertyParaMap("TimeDay", DevControl.PARA_TYPES.LOCALTIME_DAY);
            this.AutoProperty.AddPropertyParaMap("TimeHour", DevControl.PARA_TYPES.LOCALTIME_HOUR);
            this.AutoProperty.AddPropertyParaMap("TimeMin", DevControl.PARA_TYPES.LOCALTIME_MINUTE);
            this.AutoProperty.AddPropertyParaMap("TimeSec", DevControl.PARA_TYPES.LOCALTIME_SECOND);
            this.AutoProperty.AddPropertyParaMap("TimeServer", DevControl.PARA_TYPES.TIMESERVER);
            this.AutoProperty.AddPropertyParaMap("WebConsole", DevControl.PARA_TYPES.WEBCONSOLE);
            this.AutoProperty.AddPropertyParaMap("TelnetConsole", DevControl.PARA_TYPES.TELNETCONSOLE);
            this.AutoProperty.AddPropertyParaMap("TerminalName", DevControl.PARA_TYPES.TERMINALNAME);

            //Network Settings;
            this.AutoProperty.AddPropertyParaMap("IpConfig", DevControl.PARA_TYPES.IPCONFIGURATION);
            this.AutoProperty.AddPropertyParaMap("BOOTP", DevControl.PARA_TYPES.BOOTP);
            this.AutoProperty.AddPropertyParaMap("DHCP", DevControl.PARA_TYPES.DHCP);
            this.AutoProperty.AddPropertyParaMap("AutoIP", DevControl.PARA_TYPES.AUTOIP);
            this.AutoProperty.AddPropertyParaMap("DHCPName", DevControl.PARA_TYPES.DHCPHOSTNAME);
            this.AutoProperty.AddPropertyParaMap("IpAddress", DevControl.PARA_TYPES.IPADDRESS);
            this.AutoProperty.AddPropertyParaMap("Subnet", DevControl.PARA_TYPES.SUBNET);
            this.AutoProperty.AddPropertyParaMap("Gateway", DevControl.PARA_TYPES.DEFAULTGATEWAY);
            this.AutoProperty.AddPropertyParaMap("PreferredDNS", DevControl.PARA_TYPES.PREFERREDDNSSERVER);
            this.AutoProperty.AddPropertyParaMap("AlternateDNS", DevControl.PARA_TYPES.ALTERNATEDNSSERVER);
            this.AutoProperty.AddPropertyParaMap("MacAddress", DevControl.PARA_TYPES.MACADDRESS);
            this.AutoProperty.AddPropertyParaMap("AutoNegotiate", DevControl.PARA_TYPES.AUTONEGOTIATE);
            this.AutoProperty.AddPropertyParaMap("NetcardSpeed", DevControl.PARA_TYPES.SPEED);
            this.AutoProperty.AddPropertyParaMap("NetcardDuplex", DevControl.PARA_TYPES.DUPLEX);

            //Server Settings;
            this.AutoProperty.AddPropertyParaMap("ARPTimeout", DevControl.PARA_TYPES.ARPCACHETIMEOUT);
            this.AutoProperty.AddPropertyParaMap("CPUMode", DevControl.PARA_TYPES.CPUPERFORMANCEMODE);
            this.AutoProperty.AddPropertyParaMap("HttpPort", DevControl.PARA_TYPES.HTTPSERVERPORT);
            this.AutoProperty.AddPropertyParaMap("MTU", DevControl.PARA_TYPES.MTUSIZE);

            //Serial Settings;
            this.AutoProperty.AddPropertyParaMap("SerialEnable", DevControl.PARA_TYPES.SERIALPORTOPTIONS);
            this.AutoProperty.AddPropertyParaMap("SerialProtocol", DevControl.PARA_TYPES.SERIALPORTPROTOCOL);
            this.AutoProperty.AddPropertyParaMap("SerialFIFO", DevControl.PARA_TYPES.FIFO);
            this.AutoProperty.AddPropertyParaMap("DataBits", DevControl.PARA_TYPES.DATABITS);
            this.AutoProperty.AddPropertyParaMap("FlowControl", DevControl.PARA_TYPES.FLOWCONTROL);
            this.AutoProperty.AddPropertyParaMap("BaudRate", DevControl.PARA_TYPES.BAUDRATE);
            this.AutoProperty.AddPropertyParaMap("SerialParity", DevControl.PARA_TYPES.PARITY);
            this.AutoProperty.AddPropertyParaMap("StopBits", DevControl.PARA_TYPES.STOPBITS);
            this.AutoProperty.AddPropertyParaMap("SerialPacking", DevControl.PARA_TYPES.ENABLEPACKING);
            this.AutoProperty.AddPropertyParaMap("IdlePacking", DevControl.PARA_TYPES.IDLEGAPTIME);
            this.AutoProperty.AddPropertyParaMap("2BytesPacking", DevControl.PARA_TYPES.MATCH2BYTESEQUENCE);
            this.AutoProperty.AddPropertyParaMap("Byte1Packing", DevControl.PARA_TYPES.FIRSTMATCHBYTE);
            this.AutoProperty.AddPropertyParaMap("Byte2Packing", DevControl.PARA_TYPES.LASTMATCHBYTE);
            this.AutoProperty.AddPropertyParaMap("FramePacking", DevControl.PARA_TYPES.SENDFRAMEONLY);
            this.AutoProperty.AddPropertyParaMap("TrailingPacking", DevControl.PARA_TYPES.SENDTRAILINGBYTES);

            //Connection Settings;
            //TCP
            this.AutoProperty.AddPropertyParaMap("ConnProtocol", DevControl.PARA_TYPES.NETPROTOCOL);
            this.AutoProperty.AddPropertyParaMap("ConnWorkMode", DevControl.PARA_TYPES.ACCEPTIONINCOMING);
            this.AutoProperty.AddPropertyParaMap("ConnActive", DevControl.PARA_TYPES.ACTIVECONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnStartChar", DevControl.PARA_TYPES.STARTCHARACTER);
            this.AutoProperty.AddPropertyParaMap("ConnRemoteHost", DevControl.PARA_TYPES.REMOTEHOST);
            this.AutoProperty.AddPropertyParaMap("ConnRemotePort", DevControl.PARA_TYPES.REMOTEPORT);
            this.AutoProperty.AddPropertyParaMap("ConnLocalPort", DevControl.PARA_TYPES.LOCALPORT);
            this.AutoProperty.AddPropertyParaMap("ConnResponse", DevControl.PARA_TYPES.CONNECTRESPONSE);
            this.AutoProperty.AddPropertyParaMap("ConnDNS", DevControl.PARA_TYPES.DNSQUERYPERIOD);
            this.AutoProperty.AddPropertyParaMap("ConnHostList", DevControl.PARA_TYPES.USEHOSTLIST);
            this.AutoProperty.AddPropertyParaMap("ConnDSR", DevControl.PARA_TYPES.ONDSRDROP);
            this.AutoProperty.AddPropertyParaMap("ConnEOT", DevControl.PARA_TYPES.CHECKEOT);
            this.AutoProperty.AddPropertyParaMap("ConnHard", DevControl.PARA_TYPES.HARDDISCONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnTimeout_M", DevControl.PARA_TYPES.INACTIVITYTIMEOUT_M);
            this.AutoProperty.AddPropertyParaMap("ConnTimeout_S", DevControl.PARA_TYPES.INACTIVITYTIMEOUT_S);
            this.AutoProperty.AddPropertyParaMap("ConnFlushInActive", DevControl.PARA_TYPES.INPUTWITHACTIVECONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnFlushInPassive", DevControl.PARA_TYPES.INPUTWITHPASSIVECONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnFlushInDis", DevControl.PARA_TYPES.INPUTATTIMEOFDISCONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnFlushOutActive", DevControl.PARA_TYPES.OUTPUTWITHACTIVECONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnFlushOutPassive", DevControl.PARA_TYPES.OUTPUTWITHPASSIVECONNECT);
            this.AutoProperty.AddPropertyParaMap("ConnFlushOutDis", DevControl.PARA_TYPES.OUTPUTATTIMEOFDISCONNECT);
            //UDP
            this.AutoProperty.AddPropertyParaMap("UdpDataGram", DevControl.PARA_TYPES.DATAGRAMTYPE);
            this.AutoProperty.AddPropertyParaMap("UdpIncoming", DevControl.PARA_TYPES.ACCEPTINCOMING);
            this.AutoProperty.AddPropertyParaMap("UdpMulLocalPort", DevControl.PARA_TYPES.UDPLOCALPORT);
            this.AutoProperty.AddPropertyParaMap("UdpMulRemotePort", DevControl.PARA_TYPES.UDPREMOTEPORT);
            this.AutoProperty.AddPropertyParaMap("UdpMulRemoteIP", DevControl.PARA_TYPES.UDPNETSEGMENT);
            this.AutoProperty.AddPropertyParaMap("UdpUniLocalPort", DevControl.PARA_TYPES.UDPUNICASTLOCALPORT);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP1", DevControl.PARA_TYPES.DEVICEADDRESSTABLE1_BEGINIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP2", DevControl.PARA_TYPES.DEVICEADDRESSTABLE1_ENDIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemotePort1", DevControl.PARA_TYPES.DEVICEADDRESSTABLE1_PORT);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP3", DevControl.PARA_TYPES.DEVICEADDRESSTABLE2_BEGINIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP4", DevControl.PARA_TYPES.DEVICEADDRESSTABLE2_ENDIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemotePort2", DevControl.PARA_TYPES.DEVICEADDRESSTABLE2_PORT);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP5", DevControl.PARA_TYPES.DEVICEADDRESSTABLE3_BEGINIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP6", DevControl.PARA_TYPES.DEVICEADDRESSTABLE3_ENDIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemotePort3", DevControl.PARA_TYPES.DEVICEADDRESSTABLE3_PORT);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP7", DevControl.PARA_TYPES.DEVICEADDRESSTABLE4_BEGINIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemoteIP8", DevControl.PARA_TYPES.DEVICEADDRESSTABLE4_ENDIP);
            this.AutoProperty.AddPropertyParaMap("UdpRemotePort4", DevControl.PARA_TYPES.DEVICEADDRESSTABLE4_PORT);

            //Hostlist Settings;   
            this.AutoProperty.AddPropertyParaMap("RetryCounter", DevControl.PARA_TYPES.RETRYCOUNTER);
            this.AutoProperty.AddPropertyParaMap("RetryTimeout", DevControl.PARA_TYPES.RETRYTIMEOUT);
            //this.AutoProperty.AddPropertyParaMap("MaxTcp", DevControl.PARA_TYPES.ENABLEBACKUPLINK);
            this.AutoProperty.AddPropertyParaMap("HostIp1", DevControl.PARA_TYPES.HOSTLIST1_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp2", DevControl.PARA_TYPES.HOSTLIST2_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp3", DevControl.PARA_TYPES.HOSTLIST3_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp4", DevControl.PARA_TYPES.HOSTLIST4_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp5", DevControl.PARA_TYPES.HOSTLIST5_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp6", DevControl.PARA_TYPES.HOSTLIST6_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp7", DevControl.PARA_TYPES.HOSTLIST7_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp8", DevControl.PARA_TYPES.HOSTLIST8_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp9", DevControl.PARA_TYPES.HOSTLIST9_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp10", DevControl.PARA_TYPES.HOSTLIST10_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp11", DevControl.PARA_TYPES.HOSTLIST11_IP);
            this.AutoProperty.AddPropertyParaMap("HostIp12", DevControl.PARA_TYPES.HOSTLIST12_IP);
            this.AutoProperty.AddPropertyParaMap("HostPort1", DevControl.PARA_TYPES.HOSTLIST1_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort2", DevControl.PARA_TYPES.HOSTLIST2_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort3", DevControl.PARA_TYPES.HOSTLIST3_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort4", DevControl.PARA_TYPES.HOSTLIST4_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort5", DevControl.PARA_TYPES.HOSTLIST5_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort6", DevControl.PARA_TYPES.HOSTLIST6_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort7", DevControl.PARA_TYPES.HOSTLIST7_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort8", DevControl.PARA_TYPES.HOSTLIST8_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort9", DevControl.PARA_TYPES.HOSTLIST9_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort10", DevControl.PARA_TYPES.HOSTLIST10_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort11", DevControl.PARA_TYPES.HOSTLIST11_PORT);
            this.AutoProperty.AddPropertyParaMap("HostPort12", DevControl.PARA_TYPES.HOSTLIST12_PORT);

            //填充CHANNELNUM   
            for (int channelNum = 0; channelNum < 32; channelNum++)
            {
                bool isSupport;
                isSupport = this.CurrentDevice.IsSupportChannel(channelNum);
                if (isSupport == true)
                {
                    this.comboBox_SerialChannel.Items.Add(channelNum);
                    this.comboBox_ConnChannel.Items.Add(channelNum);
                    this.comboBox_HostChannel.Items.Add(channelNum);
                }
            }

            this.comboBox_SerialChannel.SelectedIndex = 0;
            this.comboBox_ConnChannel.SelectedIndex = 0;
            this.comboBox_HostChannel.SelectedIndex = 0;

            this.comboBox_SerialChannel.SelectedIndexChanged += new EventHandler(this.comboBox_Channel_SelectedIndexChanged);
            this.comboBox_ConnChannel.SelectedIndexChanged += new EventHandler(this.comboBox_Channel_SelectedIndexChanged);
            this.comboBox_HostChannel.SelectedIndexChanged += new EventHandler(this.comboBox_Channel_SelectedIndexChanged);

            this.netProtocolCB.SelectedItem = "UDP";
            this.comboBox_IpConfig.SelectedItem = "User Config";

            ReflashSelectedTabPage(this.tabControl.SelectedTab);
        }

        private void SubmitSelectedTabPage(TabPage currentTabPage)
        {
            string errMsg;
            DevControl.tagErrorCode eCode;

            if (currentTabPage.Equals(this.tabPage_Power))
            {
                RebootType rebootType;

                if (radioButton_Default.Checked)
                {
                    rebootType = RebootType.DefaultWithoutReboot;
                }
                else if (radioButton_DefaultReboot.Checked)
                {
                    rebootType = RebootType.DefaultAndReboot;
                }
                else if (radioButton_Reboot.Checked)
                {
                    rebootType = RebootType.RebootWithoutSave;
                }
                else
                {
                    rebootType = RebootType.SaveAndReboot;
                }

                eCode = this.CurrentDevice.RebootManage(rebootType);
                //返回值处理
                errMsg = ErrorHandling.HandleDeviceError(eCode, this.CurrentDevice);
                this.Lable_Message.Text = errMsg;
                System.Windows.Forms.MessageBox.Show(errMsg);
            }
            else if (currentTabPage.Equals(this.tabPage_Password))
            {
                string password, newPassword;

                password = this.oldPwdTB.Text.Trim();
                newPassword = this.newPwdTB.Text.Trim();

                if ((password == "") || (newPassword == ""))
                {
                    System.Windows.Forms.MessageBox.Show("Password required!");
                    return;
                }

                if (newPassword != this.retypePwdTB.Text.Trim())
                {
                    System.Windows.Forms.MessageBox.Show("Password are not same!");
                    return;
                }
                // 
                eCode = this.CurrentDevice.ModifyPassword(password, newPassword);
                //返回值处理  
                errMsg = ErrorHandling.HandleDeviceError(eCode, this.CurrentDevice);
                this.Lable_Message.Text = errMsg;
                System.Windows.Forms.MessageBox.Show(errMsg);
            }
            else
            {
                int channelNum = 0;

                if (currentTabPage.Equals(this.tabPage_Hostlist))
                {
                    channelNum = (int)this.comboBox_HostChannel.SelectedItem;
                }
                else if (currentTabPage.Equals(this.tabPage_Serial))
                {
                    channelNum = (int)this.comboBox_SerialChannel.SelectedItem;
                }
                else if (currentTabPage.Equals(this.tabPage_Connection))
                {
                    channelNum = (int)this.comboBox_ConnChannel.SelectedItem;
                }

                eCode = this.AutoProperty.SetParameter(currentTabPage, channelNum);
                //返回值处理   
                errMsg = ErrorHandling.HandleDeviceError(eCode, this.CurrentDevice);
                this.Lable_Message.Text = errMsg;
                System.Windows.Forms.MessageBox.Show(errMsg);
            }
        }

        private void ConfigForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //登出当前设备；
            if (CurrentDevice.IsLogin == true)
            {
                DevControl.tagErrorCode eCode = CurrentDevice.Logout();
                //返回值处理
                ErrorHandling.HandleDeviceError(eCode, this.CurrentDevice);
            }
        }

        private void ReflashSelectedTabPage(TabPage currentTabPage)
        {
            int channelNum=0;
            string errMsg;
            this.Lable_Message.Text = "";
            if (currentTabPage.Equals(this.tabPage_Power))
            {
                //do nothing;
            }
            else if (currentTabPage.Equals(this.tabPage_Password))
            {
                //do nothing;
            }
            else   
            {
                if (currentTabPage.Equals(this.tabPage_Hostlist))
                {
                    channelNum = (int)this.comboBox_HostChannel.SelectedItem;
                }
                else if(currentTabPage.Equals(this.tabPage_Serial))
                {
                    channelNum = (int)this.comboBox_SerialChannel.SelectedItem;
                }
                else if (currentTabPage.Equals(this.tabPage_Connection))
                {
                    channelNum = (int)this.comboBox_ConnChannel.SelectedItem;
                }

                DevControl.tagErrorCode eCode = this.AutoProperty.GetParameter(currentTabPage, channelNum);
                errMsg = ErrorHandling.HandleDeviceError(eCode, this.CurrentDevice);
                this.Lable_Message.Text = errMsg;
            //    System.Windows.Forms.MessageBox.Show(errMsg);
            }
        }

        private void SelectedChannel(TabPage currentTabPage)
        {
            ReflashSelectedTabPage(currentTabPage);
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            ReflashSelectedTabPage(this.tabControl.SelectedTab);
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            SubmitSelectedTabPage(this.tabControl.SelectedTab);
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            ReflashSelectedTabPage(this.tabControl.SelectedTab);
        }

        private void comboBox_Channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedChannel((TabPage)((Control)sender).Parent);
        }

        private void netProtocolCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.netProtocolCB.SelectedItem.ToString() == "TCP")
            {
                this.panel_UDP.Visible = false;
                this.panel_TCP.Visible = true;
            }
            else if (this.netProtocolCB.SelectedItem.ToString() == "UDP")
            {
                this.panel_TCP.Visible = false;
                this.panel_UDP.Visible = true;
            }
            else
            {
                Debug.Fail("Invalid!");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.comboBox_IpConfig.SelectedItem.ToString() == "User Config")
            {
                this.panel_AutoIp.Visible = false;
                this.panel_StaticIp.Visible = true;
            }
            else if (this.comboBox_IpConfig.SelectedItem.ToString() == "Obtain Automatically")
            {
                this.panel_StaticIp.Visible = false;
                this.panel_AutoIp.Visible = true;
            }
            else
            {
                Debug.Fail("Invalid!");
            }
        }
    }
}