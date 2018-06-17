unit ChangeDlg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, IdBaseComponent, IdComponent, IdUDPBase, IdUDPServer, StdCtrls,
  Mask,ComCtrls,DateUtils,IdSocketHandle;

type
  TChangeIPdlg = class(TForm)
    GroupBox1: TGroupBox;
    Label1: TLabel;
    MaskEdit1: TMaskEdit;
    Button1: TButton;
    Button2: TButton;
    IdUDPServer1: TIdUDPServer;
    procedure MaskEdit1Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure IdUDPServer1UDPRead(Sender: TObject; AData: TStream;
      ABinding: TIdSocketHandle);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    m_ipctrl:string;
    fRecvUDPstring,RemostIP,sendcmd:string;
    select_index:LongInt;
    { Public declarations }
  end;

var
  ChangeIPdlg: TChangeIPdlg;

implementation
uses fUHFReader18demomain,UHFReader18_Head;
{$R *.dfm}

procedure TChangeIPdlg.MaskEdit1Change(Sender: TObject);
var
t: array[0..3] of string;
i, j, len: integer;
begin
len := StrLen(PChar(MaskEdit1.text));
//取字符串长度
  for i := 0 to 3 do
      //分四段读取
  begin
    t[i] := '';
    if len < i * 3 + 1 then
      Break;
    for j := i * 3 + 1 to i * 3 + 3 do
    begin
      //读三个字符
      if j <= len then
        t[i] := t[i] + MaskEdit1.Text[j]
      else
        Break;
    end;
    if t[i] = '' then
    begin
      ShowMessage('Error: ' + t[i] + 'Error IP address');
      Break;
    end;
    if StrToIntDef(t[i], 0) > 255 then
    begin
      ShowMessage('Error: ' + t[i] + 'Error IP address');
      Break;
    end;
  end;
end;

procedure TChangeIPdlg.Button1Click(Sender: TObject);
var
  str,comd,cmpip,cmdmac:string;
  i,bOpt,t,dwNo:LongInt;
  aDateTime: TDateTime;
  hexstr,str1:string;
  ecode,use,dsname:string;
 // aListItem:TListItem;
begin
  if frmUHFReader18demomain.ListView1.Selected = nil then
  begin
    MessageDlg('No select device！', mtWarning, [mbOK], 0);
    exit;
  end;
  if frmUHFReader18demomain.ListView1.Selected.SubItems[0]<>''then
  begin
    cmpip:=frmUHFReader18demomain.ListView1.Selected.SubItems[1];
    cmdmac:= frmUHFReader18demomain.ListView1.Selected.SubItems[0];
    select_index:= frmUHFReader18demomain.ListView1.Selected.Index;
  end
  else
  begin
    Exit;
  end;
  str:= MaskEdit1.Text;
  str:=Trim(str);
  m_ipctrl:='';
  for i:=1 to (Length(str))do
  begin
    if(str[i]<>' ')then
    m_ipctrl:=m_ipctrl+str[i];
  end;
  if( Pos('..',m_ipctrl)>0)then
  begin
    MessageDlg('Input IP wrong!', mtWarning, [mbOK], 0);
    Exit;
  end;
  try
    IdUDPServer1.DefaultPort:=0;
    IdUDPServer1.Active:=True;
  except
    MessageDlg('Service port conflict!', mtInformation, [mbOK], 0);
  end;
  comd:= 'X';
  IdUDPServer1.Bindings[0].IP:=cmpip;
  IdUDPServer1.Bindings[0].Port:=65535;
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(100);
  comd:= 'L';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(50);
  comd := 'SIP';
  comd := comd + m_ipctrl;
  comd := comd +'|34';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(10);
  comd := 'E|35';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(200);
  comd := 'W';
  comd := comd+cmdmac;
  IdUDPServer1.Bindings[0].IP:='255.255.255.255';
  bOpt:=1;
  IdUDPServer1.Bindings[0].SetSockOpt(65535,$20,@bOpt, SizeOf(bOpt));
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(100);
  comd :='L';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(50);
  comd := 'SIP';
  comd := comd+m_ipctrl;
  comd := comd+'|34';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Sleep(100);
  comd := 'E|35';
  IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  frmUHFReader18demomain.ListView1.Items.Clear;
 { Application.ProcessMessages;
  Sleep(200);
  comd := 'X';
  try
  frmUHFReader18demomain.IdUDPServer1.DefaultPort:=0;
  frmUHFReader18demomain.IdUDPServer1.Active:=True;
 except
  MessageDlg('服务端口冲突！', mtInformation, [mbOK], 0);
 end;
 comd:= 'X';
 frmUHFReader18demomain.fRecvUDPstring:='';
 frmUHFReader18demomain.ListView1.Items.Clear;
  frmUHFReader18demomain.IdUDPServer1.Bindings[0].IP:='255.255.255.255';
  frmUHFReader18demomain.IdUDPServer1.Bindings[0].Port:=65535;
  bOpt:=1;
  frmUHFReader18demomain.IdUDPServer1.Bindings[0].SetSockOpt(65535,$20,@bOpt, SizeOf(bOpt));
  frmUHFReader18demomain.IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
  Application.ProcessMessages;
  aDateTime := Now;
  while (Now < IncMilliSecond(aDateTime, 5000 )) do //询查时间没有溢出
  begin
      Application.ProcessMessages;
      if (frmUHFReader18demomain.fRecvUDPstring <> '')and (Length(frmUHFReader18demomain.fRecvUDPstring)>20) then
      begin
           ecode:=Copy(frmUHFReader18demomain.fRecvUDPstring,1,1);
           if(ecode<>'A')then
           begin
             GeteCodeDesc(ecode);
             Exit;
           end;
           dwNo:=frmUHFReader18demomain.ListView1.Items.Count;
           aListItem := frmUHFReader18demomain.ListView1.Items.Add;

           aListItem.SubItems.Add('');
           aListItem.SubItems.Add('');
           aListItem.SubItems.Add('');//
           aListItem.SubItems.Add('');//
           aListItem.Caption:=IntToStr(frmUHFReader18demomain.ListView1.Items.Count);
           str:=Copy(frmUHFReader18demomain.fRecvUDPstring,2,Pos('/',frmUHFReader18demomain.fRecvUDPstring)-2);
           frmUHFReader18demomain.ListView1.Items[dwNo].SubItems[0]:=str;
           frmUHFReader18demomain.ListView1.Items[dwNo].SubItems[1]:=frmUHFReader18demomain.RemostIP;
           t :=Pos('*',frmUHFReader18demomain.fRecvUDPstring)+8;
           str1:=Copy(frmUHFReader18demomain.fRecvUDPstring,t,Length(frmUHFReader18demomain.fRecvUDPstring)-t+1)   ;
           t:=Pos('/',str1);
           use:=Copy(str1,1,t-1);
           dsname:=Copy(str1,t+1,Length(str1)-t+1);
           str:='';
           if((use = '') and (dsname = '' ))or (dsname = '/')then
            str:=''
           else
           begin
            str:= use+'/'+dsname;
           end;
           frmUHFReader18demomain.ListView1.Items[dwNo].SubItems[2]:=str;
           Break;
      end;
  end;   }
   Close;
end;

procedure TChangeIPdlg.IdUDPServer1UDPRead(Sender: TObject; AData: TStream;
  ABinding: TIdSocketHandle);
var DataStringStream:tstringstream;
begin
  //tstringstream这个类是字流类，主要用于在socket中传递大量的字符
   fRecvUDPstring:='';
   DataStringStream:=tstringstream.Create('');
   TRY
     DataStringStream.CopyFrom(adata,adata.Size);
     fRecvUDPstring:=DataStringStream.DataString;
     RemostIP:= ABinding.PeerIP;
   finally
     DataStringStream.Free;
   end;
end;

procedure TChangeIPdlg.Button2Click(Sender: TObject);
begin
Close;
end;

end.
