unit UHFReader18_DLL_Head;

interface

Const

    UHFReader18_DLLName = 'UHFReader18.dll'; // 'RR9000EXT.DLL'

type
    RTempRecord=Record
    end;
    //==========================================================================================//
   
    Function OpenComPort(Port : LongInt;var ComAdr : byte;Baud:byte; var frmcomportindex: longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function CloseComPort(  ): LongInt; stdcall;external UHFReader18_DLLName ;
    Function AutoOpenComPort(var Port : longint; var ComAdr : byte;Baud:byte; var frmComPortindex :longint ) : LongInt; stdcall; external UHFReader18_DLLName ;
    Function CloseSpecComPort( frmComPortindex : longint ): LongInt; stdcall;external UHFReader18_DLLName ;
    Function GetReaderInformation(var ComAdr: byte; VersionInfo: pchar;
                                  var ReaderType: byte; TrType: pchar;
                                  var dmaxfre ,dminfre,powerdBm:Byte;
                                  var ScanTime: byte;
                                  frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
     function SetWGParameter(var ComAdr:Byte;
                        Wg_mode:Byte;
                        Wg_Data_Inteval:Byte;
                        Wg_Pulse_Width:Byte;
                        Wg_Pulse_Inteval:Byte;
                        frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function ReadActiveModeData(ScanModeData: pchar;
                          Var ValidDatalength: longint;
                          frmComPortindex: longint): LongInt; Stdcall;external UHFReader18_DLLName ;
   function SetWorkMode(var ComAdr:Byte;
                      Parameter:PChar;
                      frmComPortindex : longint): LongInt; stdcall;external UHFReader18_DLLName ;
   function GetWorkModeParameter(var ComAdr:Byte;
                      Parameter:PChar;
                      frmComPortindex : longint): LongInt; stdcall;external UHFReader18_DLLName ;
   function BuzzerAndLEDControl(var ComAdr:Byte;
                                 AvtiveTime:Byte;
                                 SilentTime:Byte;
                                 Times:Byte;
                                 frmComPortindex: LongInt):LongInt; stdcall;external UHFReader18_DLLName ;

    Function WriteComAdr(var ComAdr : byte; var ComAdrData : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function SetPowerDbm(var ComAdr : byte;powerDbm : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function Writedfre(var ComAdr : byte;var dmaxfre : Byte; var dminfre : Byte;frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function Writebaud(var ComAdr : byte;var baud : Byte; frmComPortindex : longint): LongInt; stdcall; external UHFReader18_DLLName ;
    Function WriteScanTime(var ComAdr:byte;var ScanTime : Byte; frmComPortindex : longint): LongInt; stdcall;external UHFReader18_DLLName ;
    Function SetAccuracy(var ComAdr:Byte;Accuracy:Byte;frmComPortindex:longint):LongInt; stdcall;external UHFReader18_DLLName ;
    Function SetOffsetTime(var ComAdr:Byte;OffsetTime:Byte;frmComPortindex:longint):LongInt; stdcall;external UHFReader18_DLLName ;
    Function SetFhssMode(var ComAdr:Byte;FhssMode :Byte;frmComPortindex: longint):LongInt; stdcall;external UHFReader18_DLLName ;
    Function GetFhssMode(var ComAdr:Byte;var FhssMode :Byte;frmComPortindex: longint):LongInt; stdcall;external UHFReader18_DLLName ;
    Function SetTriggerTime(var ComAdr:Byte;var TriggerTime :Byte;frmComPortindex: longint):LongInt; stdcall;external UHFReader18_DLLName ;

//EPC  G2
    Function Inventory_G2(var ComAdr : byte;
                          AdrTID,LenTID,TIDFlag:Byte;
                          EPClenandEPC : pchar;
                          var Totallen:longint;
                          var CardNum : longint;
                          frmComPortindex:LongInt): LongInt;
                          stdcall; external UHFReader18_DLLName ;

    Function ReadCard_G2(var ComAdr:Byte;EPC:PChar;Mem,WordPtr,Num:Byte;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;Data:PChar;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function WriteCard_G2(var ComAdr:Byte;EPC:PChar;Mem,WordPtr,Writedatalen:Byte;Writedata:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;WrittenDataNum:LongInt;EPClength:byte;var errorcode:longint;
                    frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function EraseCard_G2(var ComAdr:Byte;EPC:PChar;Mem,WordPtr,Num:Byte;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function SetCardProtect_G2(var ComAdr:Byte;EPC:PChar;select,setprotect:Byte;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function DestroyCard_G2(var ComAdr:Byte;EPC:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function WriteEPC_G2(var ComAdr:Byte;
                    Password:PChar;WriteEPC:PChar;WriteEPClen:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function SetReadProtect_G2(var ComAdr:Byte;EPC:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function SetMultiReadProtect_G2(var ComAdr:Byte;
                    Password:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function RemoveReadProtect_G2(var ComAdr:Byte;
                    Password:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function CheckReadProtected_G2(var ComAdr:Byte; var readpro:byte;
                    var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function SetEASAlarm_G2(var ComAdr:Byte;EPC:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;EAS:byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function CheckEASAlarm_G2(var ComAdr:Byte;
                    var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
    Function LockUserBlock_G2(var ComAdr:Byte;EPC:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;BlockNum:byte;EPClength:byte;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;

    Function WriteBlock_G2(var ComAdr:Byte;EPC:PChar;Mem,WordPtr,Writedatalen:Byte;Writedata:PChar;
                    Password:PChar;maskadr:Byte;maskLen:Byte;maskFlag:Byte;WrittenDataNum:LongInt;EPClength:byte;var errorcode:longint;
                    frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;

//18000_6B

  Function Inventory_6B(var ComAdr : byte; ID_6B : pchar;frmComPortindex:LongInt): LongInt; stdcall; external UHFReader18_DLLName ;
  Function inventory2_6B(var ComAdr : byte;Condition,StartAddress,mask:byte;ConditionContent:PChar; ID_6B : pchar;var Cardnum:longint;frmComPortindex:LongInt): LongInt; stdcall; external UHFReader18_DLLName ;
  Function ReadCard_6B(var ComAdr;ID_6B:PChar;StartAddress,Num:Byte;
                      Data:PChar;var errorcode:longint;frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
  Function WriteCard_6B(var ComAdr;ID_6B:PChar;StartAddress:Byte;Writedata:PChar;Writedatalen:Byte;var writtenbyte:longint;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
  Function LockByte_6B(var ComAdr;ID_6B:PChar;Address:Byte;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;
  Function CheckLock_6B(var ComAdr;ID_6B:PChar;Address:Byte;var ReLockState:Byte;var errorcode:longint;
                  frmComPortindex : longint ): LongInt;stdcall;external UHFReader18_DLLName ;


implementation


end.












 
