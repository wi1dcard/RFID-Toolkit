object fSetdlg: TfSetdlg
  Left = 294
  Top = 200
  Width = 462
  Height = 337
  BorderIcons = [biSystemMenu]
  Caption = 'Property Setting'
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
    Caption = 'OK'
    TabOrder = 0
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 376
    Top = 280
    Width = 75
    Height = 25
    Caption = 'Cancel'
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
      Caption = 'Network Setting'
      object GroupBox1: TGroupBox
        Left = 2
        Top = 1
        Width = 442
        Height = 239
        TabOrder = 0
        object Label1: TLabel
          Left = 24
          Top = 21
          Width = 65
          Height = 13
          Caption = 'User Name'#65306
        end
        object Label2: TLabel
          Left = 256
          Top = 21
          Width = 77
          Height = 13
          Caption = 'Device Name'#65306
        end
        object Label3: TLabel
          Left = 26
          Top = 46
          Width = 38
          Height = 13
          Caption = 'MAC '#65306
        end
        object Label4: TLabel
          Left = 26
          Top = 71
          Width = 25
          Height = 13
          Caption = 'IP '#65306
        end
        object Label5: TLabel
          Left = 256
          Top = 69
          Width = 31
          Height = 13
          Caption = 'Port'#65306
        end
        object Label6: TLabel
          Left = 26
          Top = 95
          Width = 51
          Height = 13
          Caption = 'Protocol'#65306
        end
        object Label7: TLabel
          Left = 256
          Top = 95
          Width = 68
          Height = 13
          Caption = 'Work Mode'#65306
        end
        object Label8: TLabel
          Left = 26
          Top = 127
          Width = 62
          Height = 13
          Caption = 'Remote IP'#65306
        end
        object Label9: TLabel
          Left = 256
          Top = 125
          Width = 71
          Height = 13
          Caption = 'Remote Port'#65306
        end
        object Label10: TLabel
          Left = 26
          Top = 158
          Width = 64
          Height = 13
          Caption = 'Getway  IP'#65306
        end
        object Label12: TLabel
          Left = 26
          Top = 188
          Width = 38
          Height = 13
          Caption = 'Mask'#65306
        end
        object Edit1: TEdit
          Left = 88
          Top = 13
          Width = 97
          Height = 21
          TabOrder = 0
        end
        object Edit2: TEdit
          Left = 328
          Top = 13
          Width = 89
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
            'Server'
            'Client')
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
          Caption = 'Advanced option...'
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
      Caption = 'Serial Setting'
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
          Width = 63
          Height = 13
          Caption = 'Baud Rate'#65306
        end
        object Label14: TLabel
          Left = 84
          Top = 54
          Width = 38
          Height = 13
          Caption = 'Parity'#65306
        end
        object Label15: TLabel
          Left = 84
          Top = 86
          Width = 55
          Height = 13
          Caption = 'Data Bits'#65306
        end
        object Label16: TLabel
          Left = 84
          Top = 118
          Width = 65
          Height = 13
          Caption = 'DTR Mode'#65306
        end
        object Label17: TLabel
          Left = 84
          Top = 150
          Width = 37
          Height = 13
          Caption = 'RTS '#65306
        end
        object Button4: TButton
          Left = 104
          Top = 208
          Width = 217
          Height = 25
          Caption = 'Advanced option...'
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
            'Disabled'
            'Enabled')
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
            'Disabled'
            'Enabled')
        end
      end
    end
  end
end
