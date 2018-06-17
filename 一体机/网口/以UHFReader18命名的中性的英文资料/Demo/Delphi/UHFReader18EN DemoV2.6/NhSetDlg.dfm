object fNhSetDlg: TfNhSetDlg
  Left = 392
  Top = 297
  Width = 273
  Height = 144
  BorderIcons = []
  Caption = 'Network Advanced Setting'
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object GroupBox1: TGroupBox
    Left = 7
    Top = 3
    Width = 250
    Height = 80
    TabOrder = 0
    object Label1: TLabel
      Left = 16
      Top = 24
      Width = 96
      Height = 13
      Caption = 'Connection Mode'#65306
    end
    object Label2: TLabel
      Left = 16
      Top = 53
      Width = 107
      Height = 13
      Caption = 'Connection Timeout'#65306
    end
    object ComboBox1: TComboBox
      Left = 107
      Top = 19
      Width = 129
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 0
      Items.Strings = (
        'Immediaately'
        'Connect-with-data')
    end
    object ComboBox2: TComboBox
      Left = 117
      Top = 48
      Width = 119
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 1
    end
  end
  object Button1: TButton
    Left = 101
    Top = 88
    Width = 68
    Height = 23
    Caption = 'OK'
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 189
    Top = 88
    Width = 68
    Height = 23
    Caption = 'Cancle'
    TabOrder = 2
    OnClick = Button2Click
  end
end
