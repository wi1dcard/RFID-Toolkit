unit DevControl;

interface
uses Windows;
const
    DLL_NAME = 'dmdll.dll';

type
TagErrorCode=(
              DM_ERR_OK ,
              DM_ERR_PARA,
              DM_ERR_NOAUTH,
              DM_ERR_AUTHFAIL ,
              DM_ERR_SOCKET,
              DM_ERR_MEM ,
              DM_ERR_TIMEOUT ,
              DM_ERR_ARG,
              DM_ERR_MATCH,
              DM_ERR_MAX);

type
ParaTypes=(
    BAUDRATE  ,                    // '波特率
    DATABITS ,                     // '串口传输的数据位长度
    STOPBITS  ,                    // '数据停止位
    PARITY   ,                     // '数据校验位奇偶校验
    FLOWCONTROL  ,                 // '设置串口流控
    FIFO      ,                    // '串口接收发送缓冲的字节数
    ENABLEPACKING  ,               // '设置是否分包
    IDLEGAPTIME  ,                 // '选择串口上最大的非活动时间
    MATCH2BYTESEQUENCE ,           // '使能在数据包匹配首尾两个字节
    SENDFRAMEONLY ,                // '当发送数据包时是否只把数据发出去还是包含分包字节
    SENDTRAILINGBYTES,            // '选择发送None/One/Two个分包匹配字节
    INPUTWITHACTIVECONNECT ,       // '选择是当模块作为客户端且连接建立时将会清空模块到网络的连接所使用的输入缓冲区
    OUTPUTWITHACTIVECONNECT ,      // '选择是当模块作为客户端且连接建立时将会清空模块到网络的连接所使用的输出缓冲区
    INPUTWITHPASSIVECONNECT ,      // '选择是当模块作为服务端且连接建立时将会清空网络到模块的连接所使用的输入缓冲区
    OUTPUTWITHPASSIVECONNECT,      // '选择是当模块作为服务端且连接建立时将会清空网络到模块的连接所使用的输出缓冲区
    INPUTATTIMEOFDISCONNECT,       // '选择是当设备与网络之间的连接断开时将会清空输入缓冲区
    OUTPUTATTIMEOFDISCONNECT ,     // '选择是当设备与网络之间的连接断开时将会清空输出缓冲区
    IPCONFIGURATION ,             //  '设置手动/自动获取IP
    AUTONEGOTIATE ,                // '选择True模块将与网络自动协商网速及全双工or半双工的工作方式
    MACADDRESS ,                   // '给模块指定一个MAC地址同一网段内的不同模块要有不同的MAC地址
    SPEED ,                        // '选择网速10M or 100M
    Duplex ,                       // '选择网卡的工作模式全双工 or 半双工
    NETPROTOCOL ,                  // '选择模块与网络通讯所使用的网络协议
    ACCEPTINCOMING  ,              // '设置是否接收UDP包
    ARPCACHETIMEOUT,               // '设置ARP缓冲超时时间
    TCPKEEPACTIVE ,                // '
    CPUPERFORMANCEMODE,            // 'CPU实行模式提供两种模式供用户选择：High or Regular
    HTTPSERVERPORT ,               // '设定访问模块HTTP WEB服务器端口
    MTUSIZE    ,                   // '传输包MTU最大值
    RETRYCOUNTER ,                 // '当模块作为客户端依次尝试连接Hostlist中主机的次数
    IPADDRESS  ,                   // 'IP地址
    FIRMWARE  ,                    // '
    UPTIME  ,                      // '
    SERIALNO  ,                    // '
    RETRYTIMEOUT  ,                // '当模块作为客户端依次尝试连接Hostlist中主机的时间时间单位为秒
    HOSTLIST1_IP ,
    HOSTLIST1_PORT   ,
    HOSTLIST2_IP,
    HOSTLIST2_PORT ,
    HOSTLIST3_IP ,
    HOSTLIST3_PORT ,
    HOSTLIST4_IP ,
    HOSTLIST4_PORT ,
    HOSTLIST5_IP,
    HOSTLIST5_PORT ,
    HOSTLIST6_IP ,
    HOSTLIST6_PORT,
    HOSTLIST7_IP ,
    HOSTLIST7_PORT ,
    HOSTLIST8_IP ,
    HOSTLIST8_PORT ,
    HOSTLIST9_IP ,
    HOSTLIST9_PORT ,
    HOSTLIST10_IP ,
    HOSTLIST10_PORT  ,
    HOSTLIST11_IP ,
    HOSTLIST11_PORT ,
    HOSTLIST12_IP  ,
    HOSTLIST12_PORT,
    FIRSTMATCHBYTE ,              // '设定匹配数据包的首字节格式为16进制
    LASTMATCHBYTE ,               // '设定匹配数据包的尾字节格式为16进制
    DATAGRAMTYPE  ,                // '选择数据包发送方式
    DEVICEADDRESSTABLE1_BEGINIP ,   //'
    UDPLOCALPORT  ,                // 'UDP组播本地端口号
    UDPREMOTEPORT  ,               // 'UDP远程主机端口号
    UDPNETSEGMENT  ,               // 'UDP组播段地址
    DEVICEADDRESSTABLE2_BEGINIP ,
    DEVICEADDRESSTABLE2_PORT  ,
    DEVICEADDRESSTABLE2_ENDIP  ,
    DEVICEADDRESSTABLE3_BEGINIP ,
    DEVICEADDRESSTABLE3_PORT  ,
    DEVICEADDRESSTABLE3_ENDIP  ,
    DEVICEADDRESSTABLE4_BEGINIP  ,
    DEVICEADDRESSTABLE4_PORT  ,
    DEVICEADDRESSTABLE4_ENDIP ,
    UDPUNICASTLOCALPORT ,          // '单播本地端口
    ACCEPTIONINCOMING ,            // '选择Yes模块作为服务器接受新的连接请求选择No模块作为客户端主动发起连接请求
    ACTIVECONNECT ,                // '设置激活连接类型
    STARTCHARACTER  ,             //  '设定开始字符格式为16进制
    ONDSRDROP ,                   //  '选择Yes当模块检查到串口输入针脚DSR由高电平变为低电平时将模块的所有TCP连接全部断开
    HARDDISCONNECT ,             //   '模块断开连接时不经过协商（ fin-ack 式的 ）而是直接断开
    CHECKEOT ,                    //  '选择Yes当模块检查到串口输入数据中存在EOT(Ctr-D)字符时断开所有TCP连接
    INACTIVITYTIMEOUT_M ,
    INACTIVITYTIMEOUT_S ,
    LOCALPORT  ,                  //  '设定模块用于通讯的TCP本地端口
    REMOTEHOST ,                  //  '设定TCP用于与模块通讯的远程主机的IP地址
    REMOTEPORT  ,                //  '设定TCP用于与模块通讯的远程主机端口
    DNSQUERYPERIOD  ,             //  'DNS有效时间
    DEVICEADDRESSTABLE1_ENDIP,
    DEVICEADDRESSTABLE1_PORT ,
    CONNECTRESPONSE,              //  '设置连接响应模式
    TERMINALNAME ,                //  '终端类型
    USEHOSTLIST ,                 //  '设置是否使用主机列表
    EMAILADDRESS ,                //  'EMAIL发送地址
    EMAILUSERNAME ,               //  'EMAIL用户名
    EMAILPASSWORD ,               //  'EMAIL密码
    EMAILINPUTTRIGGERMESSAGE ,    //  'EMAIL标题
    EMAILADDRESS1,               //   '接收邮件的地址1
    EMAILADDRESS2 ,              //   '接收邮件的地址2
    EMAILADDRESS3 ,               //  '接收邮件的地址3
    POP3DOMAINNAME ,
    SMTPDOMAINNAME ,               // 'SMTP服务器域名
    POP3PORT ,
    SMTPPORT  ,                   //  'SMTP服务器端口号
    COLDSTART ,                   //  '冷启动触发
    DCDCHANGED ,                  //  '当modem口DCD状态发生变化时触发邮件
    DSRCHANGED ,                  //  '
    WARMSTART ,                   //  '热启动触发邮件
    AUTHENTICATIONFAILURE ,       //  '输入错误的用户名或者密码造成登录失败触发邮件
    IPADDRESSCHANGED ,             // '更改模块当前的IP地址触发邮件
    ENABLESERIALTRIGGERINPUT,      // '选择True当模块串口接收到从网络发过来的特定字符时将触发邮件
    SERIALCHANNEL  ,               // '选择触发邮件的串口号
    SERIALDATASIZE ,               // '选择串口触发邮件的字符长度
    SERIALMATCHDATA1 ,             // '触发字符1设定触发邮件的特定字符格式为16进制
    SERIALMATCHDATA2  ,            // '触发字符2设定触发邮件的特定字符格式为16进制
    EMAILTRIGGERSUBJECT ,          // '邮件标题
    PRIORITY  ,                    // '邮件优先级
    INPUTPRIORITY,
    INPUTMINNOTIFICATIONINTERVAL ,
    MINNOTIFICATIONINTERVAL  ,
    RENOTIFICATIONINTERVAL  ,
    NEWUSERPSW  ,
    BOOTP   ,                     //  '是否允许自引导协议分配IP地址
    DHCP   ,                      //  '是否允许DHCP协议分配IP地址
    AUTOIP  ,                    //   '是否允许AUTO IP协议分配IP地址
    DHCPHOSTNAME  ,              //   'DHCP主机名
    subnet  ,                    //   '子网掩码
    DEFAULTGATEWAY ,            //    '网关
    DeviceName  ,               //    '设备名
    TIMEZONE ,                  //    '时区
    LOCALTIME_YEAR  ,
    LOCALTIME_MONTH   ,
    LOCALTIME_DAY ,
    LOCALTIME_HOUR  ,
    LOCALTIME_MINUTE  ,
    LOCALTIME_SECOND ,
    TIMESERVER ,                  //  '时间服务器
    WEBCONSOLE ,                 //   'Web使能
    TELNETCONSOLE  ,             //   'Telnet使能
    PASSWORDCHANGED ,            //   '更改密码触发邮件
    SERIALPORTOPTIONS ,          //   '串口使能
    PREFERREDDNSSERVER ,         //   '首选DNS服务器
    ALTERNATEDNSSERVER  ,        //   '备用DNS服务器
    SERIALMATCHDATA3  ,          //   '触发字符3设定触发邮件的特定字符格式为16进制
    INPUT1  ,                   //   'I/O 输入1 触发邮件的I/O 输入1电平
    INPUT2 ,                    //    'I/O 输入2 触发邮件的I/O 输入1电平
    IO1  ,                      //    'Input/Output 1
    IO2  ,                     //     'Input/Output 2
    IO1TYPE  ,
    IO2TYPE ,
    IO1STATE  ,
    IO2STATE  ,
    SERIALPORTPROTOCOL ,
    FIRMWAREID ,
    PPPOEUSERNAME  ,             //   'PPPoE账号
    PPPOEPASSWORD  ,             //   'PPPoE密码
    PPPOEWORKMODE   ,            //   'PPPoE工作模式
    PPPOEMAXREDIALTIMES ,         //  '最大重拨次数
    PPPOEREDIALINTERVAL ,         //  'PPPoE重拨间隔
    PPPOEIDLETIME  ,             //   'PPPoE挂断前空闲时间
    PPPOESTATUS   ,               //  'PPPoE拨号状态
    PPPOEIP   ,                   //  'PPPoE当前获取的IP地址
    PPPOEGATEWAY  ,                // 'PPPoE当前获取的网关
    PPPOEDNS1 ,                   //  'PPPoE当前获取的DNS1
    PPPOEDNS2 ,                    // '显示PPPoE当前获取的DNS2
    ENABLEBACKUPLINK );
    Function DM_Init(searchCB:PLongInt;
                     data:LongInt ): LongInt;
                       stdcall;external DLL_NAME ;

    Function DM_SearchDevice(devIP:DWORD;
                     timeout:LongInt ): LongInt;
                       stdcall;external DLL_NAME ;

    Function DM_DeInit(): LongInt;
                       stdcall;external DLL_NAME ;

    Function DM_GetDeviceInfo(devhandle:LongInt;
                              var devIP:DWORD;
                              mac:PChar;
                              devName:PChar): LongInt;
                       stdcall;external DLL_NAME ;

     Function DM_AuthLogin(devhandle:LongInt;
                              name:PChar;
                              pwd:PChar;
                              timeout:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function DM_FreeDevice(devhandle:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function paralistCreate(devhandle:LongInt): LongInt;
                       stdcall;external DLL_NAME ;

     Function paralistDestroy(list:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function paralist_addnode(list:LongInt;
                              chanNo:LongInt;
                              paraType:ParaTypes;
                              valueLen:LongInt;
                              var value:Byte): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function paralist_getnode(list:LongInt;
                              chanNo:LongInt;
                              paraType:ParaTypes;
                              var valueLen:LongInt;
                              var value:Byte): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function DM_GetPara(devhandle:LongInt;
                              list:LongInt;
                              timeout:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;
     Function DM_SetPara(devhandle:LongInt;
                              list:LongInt;
                              timeout:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function DM_ResetDevice(devhandle:LongInt;
                              timeout:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;

     Function DM_LogOutDevice(devhandle:LongInt;
                              timeout:LongInt): TagErrorCode;
                       stdcall;external DLL_NAME ;


   type
     TSearchCallback=procedure( dev:LongInt;
                               data:LongInt); stdcall;
     procedure SearchCallback ( dev:LongInt;
                               data:LongInt); stdcall;
  //

implementation
uses  frmUHFReader28demomain;
    procedure SearchCallback ( dev:LongInt;
                               data:LongInt); stdcall;
    var   //
      devIP:DWORD;
      mac:array[0..30]of Char;
      devName:array[0..30]of Char;
    begin
      devIP:=0;
      mac:='';
      DM_GetDeviceInfo (dev, devIP, mac, devName);
      frmUHFReader28main.SearchCallback1(devIP, mac, devName, dev ) ;
    end;

end.
