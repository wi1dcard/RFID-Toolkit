object locateForm: TlocateForm
  Left = 380
  Top = 261
  Width = 321
  Height = 140
  BorderIcons = [biSystemMenu, biMinimize]
  Caption = 'Specific  Search'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 10
    Top = 8
    Width = 299
    Height = 83
    TabOrder = 0
    object Label1: TLabel
      Left = 19
      Top = 24
      Width = 118
      Height = 13
      Caption = 'Please input device IP'#65306
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
      Caption = 'OK'
      TabOrder = 1
      OnClick = Button1Click
    end
    object Button2: TButton
      Left = 197
      Top = 48
      Width = 75
      Height = 23
      Caption = 'Cancle'
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
