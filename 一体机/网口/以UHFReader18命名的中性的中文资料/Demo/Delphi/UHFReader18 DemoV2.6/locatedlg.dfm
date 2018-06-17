object locateForm: TlocateForm
  Left = 380
  Top = 261
  Width = 321
  Height = 140
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = #25628#32034#25351#23450#35774#22791
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label2: TLabel
    Left = 10
    Top = 96
    Width = 252
    Height = 13
    Caption = #25552#31034#65306#25351#23450#25628#32034#21151#33021#22810#25968#34987#29992#20110#26597#25214#36328#32593#27573#35774#22791
  end
  object GroupBox1: TGroupBox
    Left = 10
    Top = 8
    Width = 299
    Height = 83
    TabOrder = 0
    object Label1: TLabel
      Left = 49
      Top = 24
      Width = 82
      Height = 13
      Caption = #35831#36755#20837#35774#22791'IP'#65306
    end
    object MaskEdit1: TMaskEdit
      Left = 137
      Top = 16
      Width = 136
      Height = 21
      AutoSize = False
      EditMask = '999.999.999.999;1; '
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -11
      Font.Name = 'Courier'
      Font.Style = []
      MaxLength = 15
      ParentFont = False
      TabOrder = 0
      Text = '   .   .   .   '
      OnChange = MaskEdit1Change
    end
    object Button1: TButton
      Left = 101
      Top = 48
      Width = 75
      Height = 23
      Caption = #30830#23450
      TabOrder = 1
      OnClick = Button1Click
    end
    object Button2: TButton
      Left = 197
      Top = 48
      Width = 75
      Height = 23
      Caption = #21462#28040
      TabOrder = 2
      OnClick = Button2Click
    end
  end
  object IdUDPServer2: TIdUDPServer
    Bindings = <>
    DefaultPort = 0
    OnUDPRead = IdUDPServer2UDPRead
    Left = 26
    Top = 32
  end
end
