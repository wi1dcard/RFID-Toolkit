object fPhSetDlg: TfPhSetDlg
  Left = 402
  Top = 251
  Width = 273
  Height = 172
  BorderIcons = []
  Caption = #20018#21475#39640#32423#36873#39033#35774#32622
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
    Left = 8
    Top = 5
    Width = 249
    Height = 105
    TabOrder = 0
    object Label1: TLabel
      Left = 17
      Top = 22
      Width = 96
      Height = 13
      Caption = #26368#22823#25968#25454#23553#21253#25968#65306
    end
    object Label2: TLabel
      Left = 17
      Top = 52
      Width = 96
      Height = 13
      Caption = #26368#22823#23383#31526#38388#24310#26102#65306
    end
    object Label3: TLabel
      Left = 17
      Top = 79
      Width = 99
      Height = 13
      Caption = 'On - the - Fly '#21151#33021#65306
    end
    object Edit1: TEdit
      Left = 128
      Top = 16
      Width = 107
      Height = 21
      TabOrder = 0
    end
    object Edit2: TEdit
      Left = 128
      Top = 46
      Width = 107
      Height = 21
      TabOrder = 1
    end
    object ComboBox1: TComboBox
      Left = 129
      Top = 75
      Width = 108
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 2
      Items.Strings = (
        #31105#29992
        #20351#29992)
    end
  end
  object Button1: TButton
    Left = 101
    Top = 115
    Width = 68
    Height = 23
    Caption = #30830#23450
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 189
    Top = 115
    Width = 68
    Height = 23
    Caption = #21462#28040
    TabOrder = 2
    OnClick = Button2Click
  end
end
