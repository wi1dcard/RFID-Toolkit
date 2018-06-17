program UHFReader28demomain;



uses
  Forms,
  frmUHFReader28demomain in 'frmUHFReader28demomain.pas' {frmUHFReader28main},
  UHFReader28_Head in 'UHFReader28_Head.pas',
  UHFReader28_DLL_Head in 'UHFReader28_DLL_Head.pas',
  fProgressbar in 'fProgressbar.pas' {frmprogress},
  LoginForm in 'LoginForm.pas' {fLoginForm},
  DevControl in 'DevControl.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmUHFReader28main, frmUHFReader28main);
  Application.CreateForm(Tfrmprogress, frmprogress);
  Application.CreateForm(TfLoginForm, fLoginForm);
  Application.Run;
end.
