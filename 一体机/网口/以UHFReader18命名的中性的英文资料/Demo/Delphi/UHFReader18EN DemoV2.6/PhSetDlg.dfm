object fPhSetDlg: TfPhSetDlg
  Left = 402
  Top = 251
  Width = 273
  Height = 172
  BorderIcons = []
  Caption = 'Serial Advanced Setting'
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
      Width = 100
      Height = 13
      Caption = 'Max packet length'#65306
    end
    object Label2: TLabel
      Left = 17
      Top = 52
      Width = 128
      Height = 13
      Caption = 'Max intercharacter delay'#65306
    end
    object Label3: TLabel
      Left = 17
      Top = 79
      Width = 110
      Height = 13
      Caption = 'On-the-Fly Command'#65306
    end
    object Edit1: TEdit
      Left = 128
      Top = 16
      Width = 107
      Height = 21
      TabOrder = 0
    end
    object Edit2: TEdit
      Left = 145
      Top = 46
      Width = 89
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
        'Disabled'
        'Enabled')
    end
  end
  object Button1: TButton
    Left = 101
    Top = 115
    Width = 68
    Height = 23
    Caption = 'OK'
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 189
    Top = 115
    Width = 68
    Height = 23
    Caption = 'Cancle'
    TabOrder = 2
    OnClick = Button2Click
  end
end
