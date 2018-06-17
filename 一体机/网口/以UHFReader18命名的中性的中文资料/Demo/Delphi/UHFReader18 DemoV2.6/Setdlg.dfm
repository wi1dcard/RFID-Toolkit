object fSetdlg: TfSetdlg
  Left = 294
  Top = 200
  Width = 462
  Height = 337
  BorderIcons = [biSystemMenu]
  Caption = #23646#24615#35774#32622
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnClose = FormClose
  PixelsPerInch = 96
  TextHeight = 13
  object Label11: TLabel
    Left = 26
    Top = 214
    Width = 62
    Height = 13
    Caption = 'MAC '#22320#22336#65306
  end
  object Button2: TButton
    Left = 296
    Top = 280
    Width = 75
    Height = 25
    Caption = #30830' '#23450
    TabOrder = 0
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 376
    Top = 280
    Width = 75
    Height = 25
    Caption = #21462' '#28040
    TabOrder = 1
    OnClick = Button3Click
  end
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 454
    Height = 272
    ActivePage = TabSheet1
    Align = alCustom
    TabOrder = 2
    object TabSheet1: TTabSheet
      Caption = #32593#32476#23646#24615#35774#32622
      object GroupBox1: TGroupBox
        Left = 2
        Top = 1
        Width = 442
        Height = 239
        TabOrder = 0
        object Label1: TLabel
          Left = 24
          Top = 21
          Width = 60
          Height = 13
          Caption = #29992#25143#21517#31216#65306
        end
        object Label2: TLabel
          Left = 256
          Top = 21
          Width = 60
          Height = 13
          Caption = #35774#22791#21517#31216#65306
        end
        object Label3: TLabel
          Left = 26
          Top = 46
          Width = 62
          Height = 13
          Caption = 'MAC '#22320#22336#65306
        end
        object Label4: TLabel
          Left = 26
          Top = 71
          Width = 61
          Height = 13
          Caption = 'IP   '#22320'  '#22336#65306
        end
        object Label5: TLabel
          Left = 256
          Top = 69
          Width = 60
          Height = 13
          Caption = #28304#31471#21475#21495#65306
        end
        object Label6: TLabel
          Left = 26
          Top = 95
          Width = 60
          Height = 13
          Caption = #20256#36755#21327#35758#65306
        end
        object Label7: TLabel
          Left = 256
          Top = 95
          Width = 60
          Height = 13
          Caption = #24037#20316#27169#24335#65306
        end
        object Label8: TLabel
          Left = 26
          Top = 127
          Width = 61
          Height = 13
          Caption = #30446'  '#26631'   IP'#65306
        end
        object Label9: TLabel
          Left = 256
          Top = 125
          Width = 60
          Height = 13
          Caption = #30446#26631#31471#21475#65306
        end
        object Label10: TLabel
          Left = 26
          Top = 158
          Width = 58
          Height = 13
          Caption = #32593'  '#20851'  IP'#65306
        end
        object Label12: TLabel
          Left = 26
          Top = 188
          Width = 60
          Height = 13
          Caption = #32593#32476#25513#30721#65306
        end
        object Edit1: TEdit
          Left = 88
          Top = 13
          Width = 97
          Height = 21
          TabOrder = 0
        end
        object Edit2: TEdit
          Left = 320
          Top = 13
          Width = 97
          Height = 21
          TabOrder = 1
        end
        object Edit3: TEdit
          Left = 88
          Top = 38
          Width = 157
          Height = 21
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 2
        end
        object Edit4: TEdit
          Left = 88
          Top = 63
          Width = 157
          Height = 21
          TabOrder = 3
        end
        object Edit5: TEdit
          Left = 320
          Top = 61
          Width = 97
          Height = 21
          TabOrder = 4
        end
        object ComboBox1: TComboBox
          Left = 88
          Top = 88
          Width = 97
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 5
          Items.Strings = (
            'UDP'
            'TCP')
        end
        object ComboBox2: TComboBox
          Left = 321
          Top = 88
          Width = 97
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 6
          OnChange = ComboBox2Change
          Items.Strings = (
            #26381#21153#22120
            #23458#25143#31471)
        end
        object Edit6: TEdit
          Left = 88
          Top = 119
          Width = 157
          Height = 21
          ReadOnly = True
          TabOrder = 7
        end
        object Edit7: TEdit
          Left = 320
          Top = 117
          Width = 97
          Height = 21
          TabOrder = 8
        end
        object Edit8: TEdit
          Left = 88
          Top = 150
          Width = 157
          Height = 21
          ReadOnly = True
          TabOrder = 9
        end
        object Button1: TButton
          Left = 104
          Top = 208
          Width = 217
          Height = 25
          Caption = #39640#32423#36873#39033' . . .'
          TabOrder = 10
          OnClick = Button1Click
        end
        object Edit9: TEdit
          Left = 88
          Top = 182
          Width = 157
          Height = 21
          ReadOnly = True
          TabOrder = 11
        end
      end
    end
    object TabSheet2: TTabSheet
      Caption = #20018#21475#23646#24615#35774#32622
      ImageIndex = 1
      object GroupBox2: TGroupBox
        Left = 3
        Top = 1
        Width = 438
        Height = 240
        TabOrder = 0
        object Label13: TLabel
          Left = 84
          Top = 22
          Width = 54
          Height = 13
          Caption = #27874' '#29305' '#29575#65306
        end
        object Label14: TLabel
          Left = 84
          Top = 54
          Width = 54
          Height = 13
          Caption = #26657'      '#39564#65306
        end
        object Label15: TLabel
          Left = 84
          Top = 86
          Width = 54
          Height = 13
          Caption = #27604' '#29305' '#20301#65306
        end
        object Label16: TLabel
          Left = 84
          Top = 118
          Width = 62
          Height = 13
          Caption = 'DTR '#35774#32622#65306
        end
        object Label17: TLabel
          Left = 84
          Top = 150
          Width = 61
          Height = 13
          Caption = 'RTS '#35774#32622#65306
        end
        object Button4: TButton
          Left = 104
          Top = 208
          Width = 217
          Height = 25
          Caption = #39640#32423#36873#39033' . . .'
          TabOrder = 0
          OnClick = Button4Click
        end
        object ComboBox3: TComboBox
          Left = 160
          Top = 16
          Width = 185
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            '1200bps'
            '2400bps'
            '4800bps'
            '9600bps'
            '19200bps'
            '38400bps'
            '57600bps'
            '115200bps'
            '150bps'
            '300bps'
            '600bps'
            '28800bps')
        end
        object ComboBox4: TComboBox
          Left = 160
          Top = 48
          Width = 185
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            'None'
            'Even'
            'Odd'
            'Mark'
            'Space')
        end
        object ComboBox5: TComboBox
          Left = 160
          Top = 80
          Width = 185
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 3
          Items.Strings = (
            '7bits'
            '8bits')
        end
        object ComboBox6: TComboBox
          Left = 160
          Top = 112
          Width = 185
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 4
          Items.Strings = (
            #31105#29992
            #20351#29992)
        end
        object ComboBox7: TComboBox
          Left = 160
          Top = 144
          Width = 185
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 5
          Items.Strings = (
            #31105#29992
            #20351#29992)
        end
      end
    end
  end
end
