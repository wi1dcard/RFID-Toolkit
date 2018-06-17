object frmprogress: Tfrmprogress
  Left = 367
  Top = 341
  BorderIcons = []
  BorderStyle = bsNone
  Caption = 'frmprogress'
  ClientHeight = 44
  ClientWidth = 302
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poMainFormCenter
  PixelsPerInch = 96
  TextHeight = 13
  object Panel1: TPanel
    Left = 0
    Top = 0
    Width = 297
    Height = 42
    BevelInner = bvRaised
    Caption = 'Panel1'
    TabOrder = 0
    object ProgressBar1: TProgressBar
      Left = 7
      Top = 10
      Width = 282
      Height = 22
      Max = 120
      Smooth = True
      TabOrder = 0
    end
  end
end
