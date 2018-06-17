unit Setdlg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, StdCtrls;

type
  TfSetdlg = class(TForm)
    Label11: TLabel;
    Button2: TButton;
    Button3: TButton;
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label12: TLabel;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Edit4: TEdit;
    Edit5: TEdit;
    ComboBox1: TComboBox;
    ComboBox2: TComboBox;
    Edit6: TEdit;
    Edit7: TEdit;
    Edit8: TEdit;
    Button1: TButton;
    Edit9: TEdit;
    TabSheet2: TTabSheet;
    GroupBox2: TGroupBox;
    Button4: TButton;
    Label13: TLabel;
    ComboBox3: TComboBox;
    Label14: TLabel;
    ComboBox4: TComboBox;
    Label15: TLabel;
    ComboBox5: TComboBox;
    Label16: TLabel;
    ComboBox6: TComboBox;
    Label17: TLabel;
    ComboBox7: TComboBox;
    procedure ComboBox2Change(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  fSetdlg: TfSetdlg;

implementation
uses  NhSetDlg,PhSetDlg,fUHFReader18demomain;
{$R *.dfm}

procedure TfSetdlg.ComboBox2Change(Sender: TObject);
begin
  if(ComboBox2.ItemIndex=0)then
  begin
    Edit6.Enabled:=False;
    Edit7.Enabled:=False;
    Edit8.Enabled:=False;
    Edit9.Enabled:=False;
    Edit6.ReadOnly:=True;
    Edit7.ReadOnly:=True;
    Edit8.ReadOnly:=True;
    Edit9.ReadOnly:=True;
    Button1.Enabled:=False;
  end
  else
  begin
    Edit6.Enabled:=True;
    Edit7.Enabled:=True;
    Edit8.Enabled:=True;
    Edit9.Enabled:=True;
    Edit6.ReadOnly:=False;
    Edit7.ReadOnly:=False;
    Edit8.ReadOnly:=False;
    Edit9.ReadOnly:=False;
    Button1.Enabled:=True;
  end;
end;

procedure TfSetdlg.Button1Click(Sender: TObject);
begin
  fSetdlg.Enabled:=False;
  fNhSetDlg.Show;
end;

procedure TfSetdlg.Button4Click(Sender: TObject);
begin
   fSetdlg.Enabled:=False;
   fPhSetDlg.Show;
end;

procedure TfSetdlg.Button3Click(Sender: TObject);
begin
fSetdlg.Close;
//frmUHFReader18demomain.Enabled:=True;
end;

procedure TfSetdlg.FormClose(Sender: TObject; var Action: TCloseAction);
begin
frmUHFReader18demomain.Enabled:=True;
end;

procedure TfSetdlg.Button2Click(Sender: TObject);
var
  select_index,bOpt:Integer;
  IPstr:string;
  macstr:string;
  comd:string;
begin
    frmUHFReader18demomain.usename:=Edit1.Text;
    frmUHFReader18demomain.dsname:=Edit2.Text;
    frmUHFReader18demomain.mac:=Edit3.Text;
    frmUHFReader18demomain.IP:=Edit4.Text;
    frmUHFReader18demomain.portnum:= Edit5.Text;
    frmUHFReader18demomain.tup:=IntToStr(ComboBox1.ItemIndex);
    frmUHFReader18demomain.rm:= IntToStr(ComboBox2.ItemIndex);
    frmUHFReader18demomain.di:=Edit6.Text;
    frmUHFReader18demomain.dp:=Edit7.Text;
    frmUHFReader18demomain.gi:=Edit8.Text;
    frmUHFReader18demomain.nm:=Edit9.Text;
    frmUHFReader18demomain.br:= IntToStr(ComboBox3.ItemIndex);
    frmUHFReader18demomain.pr:= IntToStr(ComboBox4.ItemIndex);
    frmUHFReader18demomain.bb:= IntToStr(ComboBox5.ItemIndex);
    frmUHFReader18demomain.dt:=IntToStr(ComboBox6.ItemIndex);
    frmUHFReader18demomain.fc:=IntToStr(ComboBox7.ItemIndex);
    frmUHFReader18demomain.cm:=IntToStr(fNhSetDlg.ComboBox1.ItemIndex);
    frmUHFReader18demomain.ct:=IntToStr(fNhSetDlg.ComboBox2.ItemIndex);
    frmUHFReader18demomain.ml:=fPhSetDlg.Edit1.Text;
    frmUHFReader18demomain.md:=fPhSetDlg.Edit2.Text;
    frmUHFReader18demomain.rc:=IntToStr(fPhSetDlg.ComboBox1.ItemIndex);
    with frmUHFReader18demomain do
    begin
      if ListView1.Selected = nil then
      begin
        MessageDlg('No select device£¡', mtWarning, [mbOK], 0);
        exit;
      end;
      if ListView1.Selected.SubItems[0]<>''then
      begin
        IPstr:=ListView1.Selected.SubItems[1];
        macstr:= ListView1.Selected.SubItems[0];
        select_index:= ListView1.Selected.Index;
      end
      else
      begin
        Exit;
      end;
     // IdUDPServer1.Bindings[0].IP:='255.255.255.255';
      IdUDPServer1.Bindings[0].IP:=IPstr;
      IdUDPServer1.Bindings[0].Port:=65535;
      comd:='L';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(50);

      comd:='SON';
      comd:=comd+usename;
      comd:=comd+'|18';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDN';
      comd:=comd+dsname;
      comd:=comd+'|19';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='STP';
      comd:=comd+tup;
      comd:=comd+'|20';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SPN';
      comd:=comd+portnum;
      comd:=comd+'|21';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SRM';
      comd:=comd+rm;
      comd:=comd+'|22';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SFC';
      comd:=comd+fc;
      comd:=comd+'|23';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDT';
      comd:=comd+dt;
      comd:=comd+'|24';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SBR';
      comd:=comd+br;
      comd:=comd+'|25';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SPR';
      comd:=comd+pr;
      comd:=comd+'|26';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SBB';
      comd:=comd+bb;
      comd:=comd+'|27';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SRC';
      comd:=comd+rc;
      comd:=comd+'|28';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SCM';
      comd:=comd+cm;
      comd:=comd+'|29';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SCT';
      comd:=comd+ct;
      comd:=comd+'|30';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SML';
      comd:=comd+ml;
      comd:=comd+'|31';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SMD';
      comd:=comd+md;
      comd:=comd+'|32';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDI';
      comd:=comd+di;
      comd:=comd+'|33';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDP';
      comd:=comd+dp;
      comd:=comd+'|34';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SGI';
      comd:=comd+gi;
      comd:=comd+'|35';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SNM';
      comd:=comd+nm;
      comd:=comd+'|36';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SIP';
      comd:=comd+IP;
      comd:=comd+'|37';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='E';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(500);

      IdUDPServer1.Bindings[0].IP:='255.255.255.255';
      bOpt:=1;
      IdUDPServer1.Bindings[0].SetSockOpt(65535,$20,@bOpt, SizeOf(bOpt));

      comd:='W';
      comd:=comd+mac;
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(100);

      comd:='L';
      comd:=comd+mac;
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(50);

      comd:='SON';
      comd:=comd+usename;
      comd:=comd+'|18';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDN';
      comd:=comd+dsname;
      comd:=comd+'|19';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='STP';
      comd:=comd+tup;
      comd:=comd+'|20';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SPN';
      comd:=comd+portnum;
      comd:=comd+'|21';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SRM';
      comd:=comd+rm;
      comd:=comd+'|22';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SFC';
      comd:=comd+fc;
      comd:=comd+'|23';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDT';
      comd:=comd+dt;
      comd:=comd+'|24';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SBR';
      comd:=comd+br;
      comd:=comd+'|25';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SPR';
      comd:=comd+pr;
      comd:=comd+'|26';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SBB';
      comd:=comd+bb;
      comd:=comd+'|27';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SRC';
      comd:=comd+rc;
      comd:=comd+'|28';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SCM';
      comd:=comd+cm;
      comd:=comd+'|29';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SCT';
      comd:=comd+ct;
      comd:=comd+'|30';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SML';
      comd:=comd+ml;
      comd:=comd+'|31';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SMD';
      comd:=comd+md;
      comd:=comd+'|32';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDI';
      comd:=comd+di;
      comd:=comd+'|33';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SDP';
      comd:=comd+dp;
      comd:=comd+'|34';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SGI';
      comd:=comd+gi;
      comd:=comd+'|35';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SNM';
      comd:=comd+nm;
      comd:=comd+'|36';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='SIP';
      comd:=comd+IP;
      comd:=comd+'|33';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);

      comd:='E';
      IdUDPServer1.Bindings[0].SendTo( IdUDPServer1.Bindings[0].IP,IdUDPServer1.Bindings[0].Port,comd[1],Length(comd));
      Sleep(10);
    end;
    frmUHFReader18demomain.ListView1.Items.Clear;
   
    Close;
end;

end.
