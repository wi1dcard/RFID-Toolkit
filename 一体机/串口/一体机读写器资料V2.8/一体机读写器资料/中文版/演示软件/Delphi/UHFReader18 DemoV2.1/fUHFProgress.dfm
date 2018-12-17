object frmUHFProgress: TfrmUHFProgress
  Left = 358
  Top = 389
  BorderIcons = []
  BorderStyle = bsNone
  Caption = 'frmUHFProgress'
  ClientHeight = 43
  ClientWidth = 303
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 303
    Height = 43
    Align = alClient
    BevelInner = bvRaised
    Caption = 'Panel1'
    TabOrder = 0
    object ProgressBar1: TProgressBar
      Left = 14
      Top = 11
      Width = 275
      Height = 22
      Max = 120
      Smooth = True
      TabOrder = 0
    end
  end
end
