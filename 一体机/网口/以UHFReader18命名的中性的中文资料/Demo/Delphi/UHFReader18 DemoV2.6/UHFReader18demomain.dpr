program UHFReader18demomain;



uses
  Forms,
  fUHFReader18demomain in 'fUHFReader18demomain.pas' {frmUHFReader18demomain},
  UHFReader18_DLL_Head in 'UHFReader18_DLL_Head.pas',
  UHFReader18_Head in 'UHFReader18_Head.pas',
  fProgress in 'fProgress.pas' {frmProgress},
  locatedlg in 'locatedlg.pas' {locateForm},
  ChangeDlg in 'ChangeDlg.pas' {ChangeIPdlg},
  Setdlg in 'Setdlg.pas' {fSetdlg},
  NhSetDlg in 'NhSetDlg.pas' {fNhSetDlg},
  PhSetDlg in 'PhSetDlg.pas' {fPhSetDlg};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmUHFReader18demomain, frmUHFReader18demomain);
  Application.CreateForm(TfrmProgress, frmProgress);
  Application.CreateForm(TlocateForm, locateForm);
  Application.CreateForm(TChangeIPdlg, ChangeIPdlg);
  Application.CreateForm(TfSetdlg, fSetdlg);
  Application.CreateForm(TfNhSetDlg, fNhSetDlg);
  Application.CreateForm(TfPhSetDlg, fPhSetDlg);
  Application.Run;
end.
