program UHFReader09demomain;



uses
  Forms,
  fUHFReader09demomain in 'fUHFReader09demomain.pas' {frmUHFReader09demomain},
  UHFReader09_DLL_Head in 'UHFReader09_DLL_Head.pas',
  UHFReader09_Head in 'UHFReader09_Head.pas',
  fProgress in 'fProgress.pas' {frmProgress};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmUHFReader09demomain, frmUHFReader09demomain);
  Application.CreateForm(TfrmProgress, frmProgress);
  Application.Run;
end.
