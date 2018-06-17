program UHFReader18demomain;



uses
  Forms,
  fUHFReader18demomain in 'fUHFReader18demomain.pas' {frmUHFReader18demomain},
  UHFReader18_DLL_Head in 'UHFReader18_DLL_Head.pas',
  UHFReader18_Head in 'UHFReader18_Head.pas',
  fUHFProgress in 'fUHFProgress.pas' {frmUHFProgress};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TfrmUHFReader18demomain, frmUHFReader18demomain);
  Application.CreateForm(TfrmUHFProgress, frmUHFProgress);
  Application.Run;
end.
