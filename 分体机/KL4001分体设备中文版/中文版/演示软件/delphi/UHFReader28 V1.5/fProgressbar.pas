unit fProgressbar;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, ComCtrls, ExtCtrls;

type
  Tfrmprogress = class(TForm)
    Panel1: TPanel;
    ProgressBar1: TProgressBar;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  frmprogress: Tfrmprogress;

implementation

{$R *.dfm}

end.
