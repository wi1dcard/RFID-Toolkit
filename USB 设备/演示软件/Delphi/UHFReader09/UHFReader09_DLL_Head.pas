unit UHFReader09_DLL_Head;

interface

Const

    UHFREADER09_DLLName = 'UHFREADER09.dll'; // 

type
    RTempRecord=Record
    end;
    //==========================================================================================//
    Function OpenComPort(Port : LongInt;var ComAdr : byte;Baud:byte; var frmcomportindex: longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function CloseComPort(  ): LongInt; stdcall;external UHFREADER09_DLLName ;
    Function AutoOpenComPort(var Port : longint; var ComAdr : byte;Baud:byte; var frmComPortindex :longint ) : LongInt; stdcall; external UHFREADER09_DLLName ;
    Function CloseSpecComPort( frmComPortindex : longint ): LongInt; stdcall;external UHFREADER09_DLLName ;
    Function GetReaderInformation(var ComAdr: byte; VersionInfo: pchar;
                                  var ReaderType: byte; TrType: pchar;
                                  var dmaxfre ,dminfre,powerdBm:Byte;
                                  var ScanTime: byte;
                                  frmComPortindex : longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function WriteComAdr(var ComAdr : byte; var ComAdrData : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function SetPowerDbm(var ComAdr : byte;powerDbm : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function Writedfre(var ComAdr : byte;var dmaxfre : Byte; var dminfre : Byte;frmComPortindex : longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function Writebaud(var ComAdr : byte;var baud : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFREADER09_DLLName ;
    Function WriteScanTime(var ComAdr : byte;var ScanTime : Byte; frmComPortindex : longint): LongInt; stdcall;external UHFREADER09_DLLName ;
    Function BuzzerAndLEDControl(var ComAdr : byte;AvtiveTime,SilentTime,Times : Byte; frmComPortindex : longint): LongInt; stdcall;external UHFREADER09_DLLName ;
    Function SetBeep(var ComAdr : byte;var BeepEn : Byte; frmComPortindex : longint): LongInt; stdcall;external UHFREADER09_DLLName ;


    //EPC  G2
    Function Inventory_G2(var ComAdr : byte;
    AdrTID,LenTID,TIDFlag:Byte;EPClenandEPC : pchar; var Totallen:longint;var CardNum : longint;frmComPortindex:LongInt): LongInt; stdcall; external UHFREADER09_DLLName ;

    Function ReadCard_G2(var ComAdr;EPC:PChar;Mem,WordPtr,Num:Byte;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;Data:PChar;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function WriteCard_G2(var ComAdr;EPC:PChar;Mem,WordPtr,Writedatalen:Byte;Writedata:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;WrittenDataNum:LongInt;EPClength:byte;var errorcode:longint;
                    frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function EraseCard_G2(var ComAdr;EPC:PChar;Mem,WordPtr,Num:Byte;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function SetCardProtect_G2(var ComAdr;EPC:PChar;select,setprotect:Byte;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function DestroyCard_G2(var ComAdr;EPC:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function WriteEPC_G2(var ComAdr;
                    Password:PChar;WriteEPC:PChar;WriteEPClen:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function SetReadProtect_G2(var ComAdr;EPC:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function SetMultiReadProtect_G2(var ComAdr;
                    Password:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function RemoveReadProtect_G2(var ComAdr;
                    Password:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function CheckReadProtected_G2(var ComAdr; var readpro:byte;
                    var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function SetEASAlarm_G2(var ComAdr;EPC:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;EAS:byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function CheckEASAlarm_G2(var ComAdr;
                    var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function LockUserBlock_G2(var ComAdr;EPC:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;BlockNum:byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
    Function WriteBlock_G2(var ComAdr;EPC:PChar;Mem,WordPtr,Writedatalen:Byte;Writedata:PChar;
                    Password:PChar;maskadr,maskLen,maskFlag:Byte;WrittenDataNum:LongInt;EPClength:byte;var errorcode:longint;
                    frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;

//18000_6B

  Function Inventory_6B(var ComAdr : byte; ID_6B : pchar;frmComPortindex:LongInt): LongInt; stdcall; external UHFREADER09_DLLName ;
  Function inventory2_6B(var ComAdr : byte;Condition,StartAddress,mask:byte;ConditionContent:PChar; ID_6B : pchar;var Cardnum:longint;frmComPortindex:LongInt): LongInt; stdcall; external UHFREADER09_DLLName ;
  Function ReadCard_6B(var ComAdr;ID_6B:PChar;StartAddress,Num:Byte;
                      Data:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
  Function WriteCard_6B(var ComAdr;ID_6B:PChar;StartAddress:Byte;Writedata:PChar;Writedatalen:Byte;var writtenbyte:longint;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
  Function LockByte_6B(var ComAdr;ID_6B:PChar;Address:Byte;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;
  Function CheckLock_6B(var ComAdr;ID_6B:PChar;Address:Byte;var ReLockState:Byte;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFREADER09_DLLName ;


implementation


end.












 
