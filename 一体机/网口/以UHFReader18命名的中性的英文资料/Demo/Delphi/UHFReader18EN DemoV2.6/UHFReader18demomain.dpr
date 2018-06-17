program UHFReader18demomain;

{%File 'History.txt'}

uses
  Forms,
  fUHFReader18demomain in 'fUHFReader18demomain.pas' {frmUHFReader18demomain},
  UHFReader18_DLL_Head in 'UHFReader18_DLL_Head.pas',
  UHFReader18_Head in 'UHFReader18_Head.pas',
  fProgress in 'fProgress.pas' {frmProgress},
  locatedlg in 'locatedlg.pas' {locateForm},
  ChangeDlg in 'ChangeDlg.pas' {ChangeIPdlg},
  Setdlg in 'Setdlg.pas' {fSetdlg},
  PhSetDlg in 'PhSetDlg.pas' {fPhSetDlg},
  NhSetDlg in 'NhSetDlg.pas' {fNhSetDlg},
  ScktComp3 in 'ScktComp3.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmUHFReader18demomain, frmUHFReader18demomain);
  Application.CreateForm(TfrmProgress, frmProgress);
  Application.CreateForm(TlocateForm, locateForm);
  Application.CreateForm(TChangeIPdlg, ChangeIPdlg);
  Application.CreateForm(TfSetdlg, fSetdlg);
  Application.CreateForm(TfPhSetDlg, fPhSetDlg);
  Application.CreateForm(TfNhSetDlg, fNhSetDlg);
  Application.Run;
end.
