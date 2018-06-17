object fNhSetDlg: TfNhSetDlg
  Left = 392
  Top = 297
  Width = 273
  Height = 144
  BorderIcons = []
  Caption = #32593#32476#39640#32423#36873#39033#35774#32622
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
      Width = 60
      Height = 13
      Caption = #36830#25509#26041#24335#65306
    end
    object Label2: TLabel
      Left = 16
      Top = 53
      Width = 60
      Height = 13
      Caption = #36830#25509#26102#38480#65306
    end
    object ComboBox1: TComboBox
      Left = 84
      Top = 19
      Width = 153
      Height = 21
      Style = csDropDownList
      ItemHeight = 13
      TabOrder = 0
      Items.Strings = (
        #31435#21363#36830#25509
        #26377#25968#25454#25165#36830#25509)
    end
    object ComboBox2: TComboBox
      Left = 84
      Top = 48
      Width = 153
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
    Caption = #30830#23450
    TabOrder = 1
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 189
    Top = 88
    Width = 68
    Height = 23
    Caption = #21462#28040
    TabOrder = 2
    OnClick = Button2Click
  end
end
