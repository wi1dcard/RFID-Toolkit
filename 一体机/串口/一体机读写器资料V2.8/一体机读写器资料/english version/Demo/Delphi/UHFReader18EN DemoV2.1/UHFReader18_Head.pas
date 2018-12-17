unit UHFReader18_Head;

interface

Const
    COM1 = 1;
    COM2 = 2;
    COM3 = 3;
    COM4 = 4;
    COM5 = 5;
    COM6 = 6;
    COM7 = 7;
    COM8 = 8;
    COM9 = 9;
    COM10 = 10;


    OK			    	= $00;

    NoElectronicTag     = $0e;
    OperationError      = $0f;



    OtherError             = $00;//其它错误
    MemoryOutPcNotSupport  = $03;//存储器超限或不被支持的PC值
    MemoryLocked           = $04;//存储器锁定
    NoPower                = $0b;//电源不足
    NotSpecialError        = $0f;//非特定错误


    CmdNotIdentify                = $02;
    OperationNotSupport_          = $03;
    UnknownError                  = $0f;

  	AbnormalCommunication  = $02;

    CommunicationErr = $30;
    RetCRCErr        = $31;
    RetDataErr       = $32;    //数据长度有错误
    CommunicationBusy= $33;
    ExecuteCmdBusy   = $34;
    ComPortOpened    = $35;
    ComPortClose     = $36;
    InvalidHandle    = $37;
    InvalidPort      = $38;
    RecmdErr         = $EE;    //返回指令错误
    InventoryReturnEarly_G2   = $01;//询查时间结束前返回
  	InventoryTimeOut_G2       = $02;// 指定的询查时间溢出
  	InventoryMoreData_G2      = $03; //本条消息之后，还有消息
    ReadermoduleMCUFull_G2    = $04;// 读写模块存储空间已满
  	AccessPasswordError          = $05;//访问密码错误
    DestroyPasswordError=$09; // 销毁密码错误
    DestroyPasswordCannotZero=$0a; //销毁密码不能为全0
    TagNotSupportCMD=$0b;// 电子标签不支持该命令
    AccessPasswordCannotZero=$0c;// 对该命令，访问密码不能为全0
    TagProtectedCannotSetAgain=$0d;//电子标签已经被设置了读保护，不能再次设置
    TagNoProtectedDonotNeedUnlock=$0e;//  电子标签没有被设置读保护，不需要解锁
    ByteLockedWriteFail=$10;//  有字节空间被锁定，写入失败
    CannotLock=$11;// 不能锁定
    LockedCannotLockAgain=$12;// 已经锁定，不能再次锁定
    ParameterSaveFailCanUseBeforeNoPower=$13;// 参数保存失败,但设置的值在读写模块断电前有效
    CannotAdjust=$14;//无法调整
    InventoryReturnEarly_6B=$15;// 询查时间结束前返回
    InventoryTimeOut_6B=$16;//指定的询查时间溢出
    InventoryMoreData_6B=$17;// 本条消息之后，还有消息
    ReadermoduleMCUFull_6B=$18;// 读写模块存储空间已满
    NotSupportCMDOrAccessPasswordCannotZero=$19;  //电子不支持该命令或者访问密码不能为0
    CMDExecuteErr=$F9;// 命令执行出错
    GetTagPoorCommunicationCannotOperation=$FA; //有电子标签，但通信不畅，无法操作
    NoTagOperation=$FB; //无电子标签可操作
    TagReturnErrorCode=$FC;// 电子标签返回错误代码
    CMDLengthWrong=$FD;// 命令长度错误
    IllegalCMD=$FE;//不合法的命令
    ParameterError=$FF;// 参数错误

    Function UHFReader18_GetErrorCodeDesc(errorCode : Byte) : String;
    Function UHFReader18_GetReturnCodeDesc(retCode : Byte) : String;

implementation


Function UHFReader18_GetErrorCodeDesc(errorCode : Byte) : String;
begin
   result := '';
    case errorCode of
        OtherError            : result := 'Other error';
        MemoryOutPcNotSupport : result := 'Memory out or pc not support';
        MemoryLocked          : result := 'Memory Locked and unwritable';
        NoPower               : result := 'No Power,memory write operation cannot be executed';
        NotSpecialError       : result := 'Not Special Error,tag not support special errorcode';

    end;
end;

Function UHFReader18_GetReturnCodeDesc(retCode : Byte) : String;
begin
    result := '';
    case retCode of
        InventoryReturnEarly_G2               : result := 'Return before Inventory finished';
        InventoryTimeOut_G2                   : result := 'the Inventory-scan-time overflow';
        InventoryMoreData_G2                  : result := 'More Data';
        ReadermoduleMCUFull_G2                : result := 'Reader module MCU is Full';
        AccessPasswordError                   : result := 'Access Password Error';
        DestroyPasswordError                  : result := 'Destroy Password Error';
        DestroyPasswordCannotZero             : result := 'Destroy Password Error Cannot be Zero';
        TagNotSupportCMD                      : result := 'Tag Not Support the command';
        AccessPasswordCannotZero              : result := 'Use the commmand,Access Password Cannot be Zero';
        TagProtectedCannotSetAgain            : result := 'Tag is protected,cannot set it again';
        TagNoProtectedDonotNeedUnlock         : result := 'Tag is unprotected,no need to reset it';
        ByteLockedWriteFail                   : result := 'There is some locked bytes,write fail';
        CannotLock                            : result := 'can not lock it';
        LockedCannotLockAgain                 : result := 'is locked,cannot lock it again';
        ParameterSaveFailCanUseBeforeNoPower  : result := 'Parameter Save Fail,Can Use Before Power';
        CannotAdjust                          : result := 'Cannot adjust';
        InventoryReturnEarly_6B               : result := 'Return before Inventory finished';
        InventoryTimeOut_6B                   : result := 'Inventory-Scan-Time overflow';
        InventoryMoreData_6B                  : result := 'More Data';
        ReadermoduleMCUFull_6B                : result := 'Reader module MCU is full';
        NotSupportCMDOrAccessPasswordCannotZero : result := 'Not Support Command Or AccessPassword Cannot be Zero';
        GetTagPoorCommunicationCannotOperation: result := 'Get Tag,Poor Communication,Inoperable';
        NoTagOperation                        : result := 'No Tag Operable';
        TagReturnErrorCode                    : result := 'Tag Return ErrorCode';
        CMDLengthWrong                        : result := 'Command length wrong';
        IllegalCMD                            : result := 'Illegal command';
        ParameterError                        : result := 'Parameter Error';


        RecmdErr            : result := 'Return command error';
        CommunicationErr    : result := 'Communication error';
        RetCRCErr           : result := 'CRC checksummat error';
        RetDataErr          : result := 'Return data length error';
        CommunicationBusy   : result := 'Communication busy';
        ExecuteCmdBusy      : result := 'Busy,command is being executed';
        ComPortOpened       : result := 'ComPort Opened';
        ComPortClose        : result := 'ComPort Closed';
        InvalidHandle       : result := 'Invalid Handle';
        InvalidPort         : result := 'Invalid Port';
    end;
end;

end.
