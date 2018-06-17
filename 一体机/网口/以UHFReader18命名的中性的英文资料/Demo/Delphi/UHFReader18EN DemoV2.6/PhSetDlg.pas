unit PhSetDlg;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TfPhSetDlg = class(TForm)
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Edit2: TEdit;
    Label3: TLabel;
    ComboBox1: TComboBox;
    Button1: TButton;
    Button2: TButton;
    procedure Button2Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  fPhSetDlg: TfPhSetDlg;

implementation
uses Setdlg,fUHFReader18demomain;
{$R *.dfm}

procedure TfPhSetDlg.Button2Click(Sender: TObject);
begin
  fSetdlg.Enabled:=True;
  fPhSetDlg.Close ;
end;

procedure TfPhSetDlg.Button1Click(Sender: TObject);
begin
  frmUHFReader18demomain.ml:=Edit1.Text;
  frmUHFReader18demomain.md:=Edit2.Text;
  frmUHFReader18demomain.rc:=IntToStr(ComboBox1.ItemIndex);
  fSetdlg.Enabled:=True;
  fPhSetDlg.Close ;
end;

end.

