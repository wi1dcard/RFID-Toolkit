object frmUHFReader18demomain: TfrmUHFReader18demomain
  Left = 124
  Top = 34
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'UHFReader18 Demo Software v2.6'
  ClientHeight = 670
  ClientWidth = 801
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  Position = poScreenCenter
  OnCloseQuery = FormCloseQuery
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 801
    Height = 651
    ActivePage = TabSheet_CMD
    Align = alClient
    TabOrder = 0
    OnChange = PageControl1Change
    object TabSheet_CMD: TTabSheet
      Caption = 'Reader Parameter'
      object SpeedButton2: TSpeedButton
        Left = 665
        Top = 543
        Width = 97
        Height = 25
        AllowAllUp = True
        GroupIndex = 5
        Caption = 'Start'
        OnClick = SpeedButton2Click
      end
      object GroupBox_ReaderInfo: TGroupBox
        Left = 136
        Top = 10
        Width = 649
        Height = 105
        Caption = 'Reader Information'
        TabOrder = 0
        object Label2: TLabel
          Left = 162
          Top = 24
          Width = 38
          Height = 13
          Caption = 'Version:'
        end
        object Label3: TLabel
          Left = 10
          Top = 52
          Width = 41
          Height = 13
          Caption = 'Address:'
        end
        object Label4: TLabel
          Left = 328
          Top = 52
          Width = 118
          Height = 13
          Caption = 'Max InventoryScanTime:'
        end
        object Label10: TLabel
          Left = 10
          Top = 24
          Width = 27
          Height = 13
          Caption = 'Type:'
        end
        object Label11: TLabel
          Left = 328
          Top = 24
          Width = 36
          Height = 13
          Caption = 'Protocl:'
        end
        object Label8: TLabel
          Left = 160
          Top = 52
          Width = 33
          Height = 13
          Caption = 'Power:'
        end
        object Label13: TLabel
          Left = 160
          Top = 81
          Width = 51
          Height = 13
          Caption = 'Dmaxfre'#65306
        end
        object Label14: TLabel
          Left = 10
          Top = 81
          Width = 53
          Height = 13
          Caption = 'Dminxfre'#65306
        end
        object Edit_Version: TEdit
          Left = 225
          Top = 20
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 2
        end
        object Edit_ComAdr: TEdit
          Left = 72
          Top = 48
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 4
        end
        object Edit_scantime: TEdit
          Left = 502
          Top = 48
          Width = 129
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 6
        end
        object Edit_Type: TEdit
          Left = 72
          Top = 20
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 1
        end
        object Button3: TButton
          Left = 504
          Top = 74
          Width = 129
          Height = 25
          Action = Action_GetReaderInformation
          Caption = 'Get Reader Info'
          TabOrder = 7
        end
        object Edit_dmaxfre: TEdit
          Left = 225
          Top = 77
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 9
        end
        object Edit_dminfre: TEdit
          Left = 72
          Top = 77
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 8
        end
        object Edit_powerdBm: TEdit
          Left = 225
          Top = 48
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 5
        end
        object EPCC1G2: TCheckBox
          Left = 496
          Top = 30
          Width = 73
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'EPCC1-G2'
          ParentBiDiMode = False
          TabOrder = 3
        end
        object ISO180006B: TCheckBox
          Left = 496
          Top = 14
          Width = 89
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'ISO18000-6B'
          ParentBiDiMode = False
          TabOrder = 0
        end
      end
      object GroupBox2: TGroupBox
        Left = 136
        Top = 115
        Width = 649
        Height = 126
        Caption = 'Set Reader Parameter'
        TabOrder = 1
        object Label15: TLabel
          Left = 8
          Top = 74
          Width = 53
          Height = 13
          Caption = 'Dminxfre'#65306
        end
        object Label16: TLabel
          Left = 8
          Top = 100
          Width = 51
          Height = 13
          Caption = 'Dmaxfre'#65306
        end
        object Label17: TLabel
          Left = 202
          Top = 19
          Width = 37
          Height = 13
          Caption = 'Baud'#65306
        end
        object Label1: TLabel
          Left = 8
          Top = 21
          Width = 69
          Height = 13
          Caption = 'Address(HEX):'
        end
        object Label7: TLabel
          Left = 8
          Top = 47
          Width = 33
          Height = 13
          Caption = 'Power:'
        end
        object Label5: TLabel
          Left = 202
          Top = 47
          Width = 121
          Height = 13
          Caption = 'Max InventoryScanTime::'
        end
        object Button5: TButton
          Left = 204
          Top = 94
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation
          Caption = 'Set Parameter'
          TabOrder = 6
        end
        object ComboBox_baud: TComboBox
          Left = 344
          Top = 17
          Width = 129
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
          Items.Strings = (
            '9600bps'
            '19200bps'
            '38400bps'
            '57600bps'
            '115200bps')
        end
        object Edit_NewComAdr: TEdit
          Left = 80
          Top = 17
          Width = 113
          Height = 21
          MaxLength = 2
          TabOrder = 0
          Text = '00'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object ComboBox_scantime: TComboBox
          Left = 344
          Top = 43
          Width = 129
          Height = 21
          Style = csDropDownList
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ItemHeight = 13
          TabOrder = 3
        end
        object Button1: TButton
          Left = 343
          Top = 94
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation_0
          Caption = 'Default Parameter'
          TabOrder = 7
        end
        object ComboBox_dminfre: TComboBox
          Left = 80
          Top = 70
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 4
          OnSelect = ComboBox_dfreSelect
        end
        object ComboBox_dmaxfre: TComboBox
          Tag = 1
          Left = 80
          Top = 96
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 8
          OnSelect = ComboBox_dfreSelect
        end
        object ComboBox_PowerDbm: TComboBox
          Left = 80
          Top = 43
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8'
            '9'
            '10'
            '11'
            '12'
            '13'
            '14'
            '15'
            '16'
            '17'
            '18'
            '19'
            '20'
            '21'
            '22'
            '23'
            '24'
            '25'
            '26'
            '27'
            '28'
            '29'
            '30')
        end
        object CheckBox_SameFre: TCheckBox
          Left = 202
          Top = 74
          Width = 81
          Height = 17
          Caption = 'Single Freq'
          TabOrder = 5
          OnClick = CheckBox_SameFreClick
        end
        object GroupBox28: TGroupBox
          Left = 485
          Top = 7
          Width = 153
          Height = 114
          Caption = 'FreqBaud'
          TabOrder = 9
          object RadioButton_band1: TRadioButton
            Left = 8
            Top = 16
            Width = 113
            Height = 17
            Caption = 'User band'
            TabOrder = 0
            OnClick = RadioButton_band1Click
          end
          object RadioButton_band2: TRadioButton
            Left = 8
            Top = 32
            Width = 113
            Height = 17
            Caption = 'Chinese band2'
            TabOrder = 1
            OnClick = RadioButton_band2Click
          end
          object RadioButton_band3: TRadioButton
            Left = 8
            Top = 48
            Width = 113
            Height = 17
            Caption = 'US band'
            TabOrder = 2
            OnClick = RadioButton_band3Click
          end
          object RadioButton_band4: TRadioButton
            Left = 8
            Top = 64
            Width = 113
            Height = 17
            Caption = 'Korean band'
            TabOrder = 3
            OnClick = RadioButton_band4Click
          end
          object RadioButton_band5: TRadioButton
            Left = 8
            Top = 84
            Width = 113
            Height = 17
            Caption = 'EU band'
            TabOrder = 4
            OnClick = RadioButton_band5Click
          end
        end
      end
      object GroupBox8: TGroupBox
        Left = 135
        Top = 243
        Width = 651
        Height = 289
        Caption = 'Set Work Mode Parameter'
        TabOrder = 2
        object Label45: TLabel
          Left = 7
          Top = 235
          Width = 80
          Height = 13
          AutoSize = False
          Caption = 'EAS Accuracy:'
        end
        object Label47: TLabel
          Left = 334
          Top = 233
          Width = 54
          Height = 13
          Caption = 'OffsetTime:'
        end
        object Label51: TLabel
          Left = 24
          Top = 265
          Width = 62
          Height = 13
          AutoSize = False
          Caption = 'Tigger time:'
        end
        object GroupBox3: TGroupBox
          Left = 8
          Top = 14
          Width = 635
          Height = 86
          Caption = 'Wiegand Parameter'
          TabOrder = 0
          object Label21: TLabel
            Left = 195
            Top = 22
            Width = 96
            Height = 13
            Caption = 'Data output interval:'
          end
          object Label22: TLabel
            Left = 444
            Top = 16
            Width = 57
            Height = 13
            Caption = 'Pulse width:'
          end
          object Label23: TLabel
            Left = 224
            Top = 56
            Width = 66
            Height = 13
            Caption = 'Pulse interval:'
          end
          object RadioButton1: TRadioButton
            Left = 8
            Top = 20
            Width = 113
            Height = 17
            Caption = 'Wiegand26'
            TabOrder = 0
          end
          object RadioButton2: TRadioButton
            Left = 96
            Top = 20
            Width = 81
            Height = 17
            Caption = 'Wiegand34'
            TabOrder = 1
          end
          object GroupBox7: TGroupBox
            Left = 7
            Top = 36
            Width = 165
            Height = 45
            TabOrder = 2
            object RadioButton3: TRadioButton
              Left = 5
              Top = 10
              Width = 142
              Height = 17
              Caption = 'Wiegand output LSB first'
              TabOrder = 0
            end
            object RadioButton4: TRadioButton
              Left = 5
              Top = 26
              Width = 157
              Height = 17
              Caption = 'Wiegand output MSB first'
              TabOrder = 1
            end
          end
          object ComboBox1: TComboBox
            Left = 298
            Top = 17
            Width = 73
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 3
          end
          object ComboBox2: TComboBox
            Left = 523
            Top = 16
            Width = 102
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 4
          end
          object ComboBox3: TComboBox
            Left = 298
            Top = 52
            Width = 74
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 5
          end
          object Button6: TButton
            Left = 491
            Top = 49
            Width = 132
            Height = 25
            Caption = 'SetWGParameter'
            TabOrder = 6
            OnClick = Button6Click
          end
        end
        object GroupBox15: TGroupBox
          Left = 8
          Top = 101
          Width = 635
          Height = 119
          Caption = 'Set Work Mode'
          TabOrder = 1
          object Label26: TLabel
            Left = 416
            Top = 18
            Width = 59
            Height = 13
            Caption = 'Work Mode:'
          end
          object Label40: TLabel
            Left = 416
            Top = 70
            Width = 101
            Height = 13
            Caption = 'First Word Addr(Hex):'
          end
          object Label41: TLabel
            Left = 417
            Top = 95
            Width = 98
            Height = 13
            Caption = 'Read Word Number:'
          end
          object Label44: TLabel
            Left = 417
            Top = 43
            Width = 123
            Height = 13
            AutoSize = False
            Caption = 'Single Tag Filtering Time:'
          end
          object ComboBox4: TComboBox
            Left = 480
            Top = 13
            Width = 145
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 0
            OnChange = ComboBox4Change
            Items.Strings = (
              'Answer Mode'
              'Active mode'
              'Trigger mode(Low)'
              'Trigger mode(High)')
          end
          object RadioButton5: TRadioButton
            Left = 8
            Top = 22
            Width = 73
            Height = 17
            Caption = 'EPCC1-G2'
            TabOrder = 1
            OnClick = RadioButton5Click
          end
          object RadioButton6: TRadioButton
            Left = 88
            Top = 22
            Width = 89
            Height = 17
            Caption = 'ISO18000-6B'
            TabOrder = 2
            OnClick = RadioButton5Click
          end
          object GroupBox25: TGroupBox
            Left = 8
            Top = 49
            Width = 161
            Height = 65
            TabOrder = 3
            object RadioButton7: TRadioButton
              Left = 8
              Top = 9
              Width = 113
              Height = 17
              Caption = 'Wiegand Output'
              TabOrder = 0
              OnClick = RadioButton7Click
            end
            object RadioButton8: TRadioButton
              Left = 8
              Top = 25
              Width = 145
              Height = 17
              Caption = 'RS232/RS485 Output'
              TabOrder = 1
              OnClick = RadioButton8Click
            end
            object RadioButton20: TRadioButton
              Left = 8
              Top = 42
              Width = 113
              Height = 17
              Caption = 'SYRIS485'
              Enabled = False
              TabOrder = 2
              OnClick = RadioButton8Click
            end
          end
          object GroupBox26: TGroupBox
            Left = 175
            Top = 9
            Width = 234
            Height = 53
            Caption = 'Storage area or inquiry conducted Tags'
            TabOrder = 4
            object RadioButton9: TRadioButton
              Left = 4
              Top = 14
              Width = 73
              Height = 17
              Caption = 'Password'
              TabOrder = 0
              OnClick = RadioButton9Click
            end
            object RadioButton10: TRadioButton
              Left = 80
              Top = 14
              Width = 46
              Height = 17
              Caption = 'EPC'
              TabOrder = 1
              OnClick = RadioButton10Click
            end
            object RadioButton11: TRadioButton
              Left = 133
              Top = 14
              Width = 39
              Height = 17
              Caption = 'TID'
              TabOrder = 2
              OnClick = RadioButton11Click
            end
            object RadioButton12: TRadioButton
              Left = 177
              Top = 13
              Width = 42
              Height = 17
              Caption = 'User'
              TabOrder = 3
              OnClick = RadioButton12Click
            end
            object RadioButton13: TRadioButton
              Left = 4
              Top = 32
              Width = 76
              Height = 17
              Caption = 'Multi-Query'
              TabOrder = 4
              OnClick = RadioButton13Click
            end
            object RadioButton18: TRadioButton
              Left = 95
              Top = 32
              Width = 73
              Height = 17
              Caption = 'One-Query'
              TabOrder = 5
              OnClick = RadioButton18Click
            end
            object RadioButton19: TRadioButton
              Left = 177
              Top = 32
              Width = 43
              Height = 17
              Caption = 'EAS'
              TabOrder = 6
              OnClick = RadioButton19Click
            end
          end
          object Edit1: TEdit
            Left = 528
            Top = 65
            Width = 36
            Height = 21
            MaxLength = 2
            TabOrder = 5
            Text = '02'
            OnKeyPress = Edit_NewComAdrKeyPress
          end
          object ComboBox5: TComboBox
            Left = 521
            Top = 90
            Width = 44
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 6
          end
          object Button7: TButton
            Left = 569
            Top = 64
            Width = 54
            Height = 46
            Caption = 'Set'
            TabOrder = 7
            OnClick = Button7Click
          end
          object GroupBox27: TGroupBox
            Left = 285
            Top = 63
            Width = 124
            Height = 51
            TabOrder = 8
            object RadioButton15: TRadioButton
              Left = 4
              Top = 30
              Width = 103
              Height = 17
              Caption = 'DisEnable buzzer'
              TabOrder = 0
            end
            object RadioButton14: TRadioButton
              Left = 4
              Top = 9
              Width = 98
              Height = 17
              Caption = 'Activate buzzer'
              TabOrder = 1
            end
          end
          object GroupBox30: TGroupBox
            Left = 175
            Top = 63
            Width = 107
            Height = 51
            Caption = 'First Addr Select'
            TabOrder = 9
            object RadioButton16: TRadioButton
              Left = 3
              Top = 14
              Width = 81
              Height = 17
              Caption = 'Word Addr'
              TabOrder = 0
              OnClick = RadioButton16Click
            end
            object RadioButton17: TRadioButton
              Left = 3
              Top = 32
              Width = 82
              Height = 17
              Caption = 'Byte Addr'
              TabOrder = 1
              OnClick = RadioButton17Click
            end
          end
          object ComboBox6: TComboBox
            Left = 553
            Top = 38
            Width = 71
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 10
            OnChange = ComboBox4Change
          end
        end
        object Button8: TButton
          Left = 464
          Top = 258
          Width = 164
          Height = 25
          Caption = 'Get Work Mode parameter'
          TabOrder = 2
          OnClick = Button8Click
        end
        object ComboBox7: TComboBox
          Left = 87
          Top = 230
          Width = 106
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 3
          OnChange = ComboBox4Change
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8')
        end
        object Button10: TButton
          Left = 197
          Top = 226
          Width = 112
          Height = 25
          Caption = 'Set Accuracy'
          Enabled = False
          TabOrder = 4
          OnClick = Button10Click
        end
        object ComboBox_OffsetTime: TComboBox
          Left = 390
          Top = 229
          Width = 106
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 5
        end
        object Button_OffsetTime: TButton
          Left = 503
          Top = 226
          Width = 124
          Height = 25
          Caption = 'Set OffsetTime'
          TabOrder = 6
          OnClick = Button_OffsetTimeClick
        end
        object ComboBox_tigtime: TComboBox
          Left = 87
          Top = 260
          Width = 106
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 7
          OnChange = ComboBox4Change
          Items.Strings = (
            '0'
            '1'
            '2'
            '3'
            '4'
            '5'
            '6'
            '7'
            '8')
        end
        object Button_Tiggertime: TButton
          Left = 197
          Top = 256
          Width = 112
          Height = 25
          Caption = 'Set Tiggertime'
          Enabled = False
          TabOrder = 8
          OnClick = Button_TiggertimeClick
        end
        object Button_GetTiggertime: TButton
          Left = 317
          Top = 256
          Width = 112
          Height = 25
          Caption = 'Get Tiggertime'
          Enabled = False
          TabOrder = 9
          OnClick = Button_GetTiggertimeClick
        end
      end
      object Memo1: TMemo
        Left = 136
        Top = 537
        Width = 497
        Height = 85
        ReadOnly = True
        ScrollBars = ssVertical
        TabOrder = 3
      end
      object Button9: TButton
        Left = 666
        Top = 584
        Width = 96
        Height = 25
        Caption = 'Clear'
        TabOrder = 4
        OnClick = Button9Click
      end
      object GroupBox32: TGroupBox
        Left = 1
        Top = 429
        Width = 131
        Height = 89
        Caption = 'Relay'
        TabOrder = 5
        object Label55: TLabel
          Left = 27
          Top = 16
          Width = 6
          Height = 13
          Caption = '1'
        end
        object Label56: TLabel
          Left = 84
          Top = 16
          Width = 6
          Height = 13
          Caption = '2'
        end
        object ComboBox9: TComboBox
          Left = 8
          Top = 31
          Width = 57
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          Items.Strings = (
            'Release'
            'Active')
        end
        object Button_relay: TButton
          Left = 7
          Top = 58
          Width = 116
          Height = 25
          Caption = 'Set'
          TabOrder = 1
          OnClick = Button_relayClick
        end
        object ComboBox10: TComboBox
          Left = 68
          Top = 31
          Width = 56
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
          Items.Strings = (
            'Release'
            'Active')
        end
      end
      object GroupBox35: TGroupBox
        Left = 0
        Top = 11
        Width = 135
        Height = 415
        Caption = 'Communication'
        TabOrder = 6
        object GroupBox_COM: TGroupBox
          Left = 9
          Top = 34
          Width = 117
          Height = 213
          Caption = 'Com'
          TabOrder = 0
          object Label6: TLabel
            Left = 5
            Top = 19
            Width = 58
            Height = 13
            Caption = 'COM Port'#65306
          end
          object Label12: TLabel
            Left = 6
            Top = 135
            Width = 87
            Height = 13
            Caption = 'Opened COM Port'
          end
          object Label46: TLabel
            Left = 9
            Top = 96
            Width = 37
            Height = 13
            Caption = 'Baud'#65306
          end
          object ComboBox_COM: TComboBox
            Left = 57
            Top = 16
            Width = 54
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 0
            OnChange = ComboBox_COMChange
          end
          object Button2: TButton
            Left = 5
            Top = 66
            Width = 105
            Height = 25
            Action = Action_OpenCOM
            TabOrder = 3
          end
          object Button4: TButton
            Left = 5
            Top = 180
            Width = 105
            Height = 25
            Action = Action_CloseCOM
            TabOrder = 5
          end
          object StaticText1: TStaticText
            Left = 6
            Top = 44
            Width = 83
            Height = 17
            Caption = 'Reader Address:'
            TabOrder = 2
          end
          object Edit_CmdComAddr: TEdit
            Left = 93
            Top = 41
            Width = 17
            Height = 21
            CharCase = ecUpperCase
            MaxLength = 2
            TabOrder = 1
            Text = 'FF'
            OnKeyPress = Edit_NewComAdrKeyPress
          end
          object ComboBox_AlreadyOpenCOM: TComboBox
            Left = 6
            Top = 153
            Width = 105
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 4
            OnCloseUp = ComboBox_AlreadyOpenCOMCloseUp
          end
          object ComboBox_baud2: TComboBox
            Left = 6
            Top = 110
            Width = 106
            Height = 21
            Style = csDropDownList
            ItemHeight = 13
            TabOrder = 6
            Items.Strings = (
              '9600bps'
              '19200bps'
              '38400bps'
              '57600bps'
              '115200bps')
          end
        end
        object gp_net: TGroupBox
          Left = 8
          Top = 250
          Width = 118
          Height = 158
          Caption = 'TCPIP'
          TabOrder = 1
          object Label62: TLabel
            Left = 12
            Top = 19
            Width = 31
            Height = 13
            Caption = 'Port'#65306
          end
          object Label63: TLabel
            Left = 12
            Top = 48
            Width = 13
            Height = 13
            Caption = 'IP:'
          end
          object Label64: TLabel
            Left = 12
            Top = 74
            Width = 71
            Height = 13
            Caption = 'Reader addr'#65306
          end
          object Edit6: TEdit
            Left = 48
            Top = 14
            Width = 61
            Height = 21
            TabOrder = 0
            Text = '6000'
          end
          object Edit7: TEdit
            Left = 32
            Top = 40
            Width = 78
            Height = 21
            TabOrder = 1
            Text = '192.168.1.192'
          end
          object Edit8: TEdit
            Left = 80
            Top = 66
            Width = 29
            Height = 21
            TabOrder = 2
            Text = 'FF'
          end
          object opnet: TButton
            Left = 11
            Top = 94
            Width = 98
            Height = 25
            Caption = 'Open Net'
            Enabled = False
            TabOrder = 3
            OnClick = opnetClick
          end
          object closenet: TButton
            Left = 11
            Top = 126
            Width = 98
            Height = 25
            Caption = 'Close Net'
            Enabled = False
            TabOrder = 4
            OnClick = closenetClick
          end
        end
        object RD_Port: TRadioButton
          Left = 10
          Top = 16
          Width = 49
          Height = 17
          Caption = 'Com'
          TabOrder = 2
          OnClick = RD_PortClick
        end
        object RD_Net: TRadioButton
          Left = 64
          Top = 16
          Width = 62
          Height = 17
          Caption = 'TCPIP'
          TabOrder = 3
          OnClick = RD_NetClick
        end
      end
    end
    object TabSheet_EPCC1G2: TTabSheet
      Caption = 'EPCC1-G2 Test'
      ImageIndex = 1
      object GroupBox5: TGroupBox
        Left = 8
        Top = 440
        Width = 481
        Height = 180
        Caption = 'Set Protect For Reading Or Writing'
        TabOrder = 7
        object Label24: TLabel
          Left = 278
          Top = 137
          Width = 124
          Height = 13
          Caption = 'Access Password (8 Hex):'
        end
        object ComboBox_EPC1: TComboBox
          Tag = 1
          Left = 8
          Top = 18
          Width = 249
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Button_SetProtectState: TButton
          Left = 387
          Top = 149
          Width = 87
          Height = 25
          Action = Action_SetProtectState
          Caption = 'Set Protect'
          TabOrder = 4
        end
        object Edit_AccessCode1: TEdit
          Left = 279
          Top = 153
          Width = 91
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object GroupBox1: TGroupBox
          Left = 277
          Top = 10
          Width = 198
          Height = 31
          TabOrder = 0
          object P_Reserve: TRadioButton
            Left = 5
            Top = 8
            Width = 68
            Height = 17
            Caption = 'Password'
            TabOrder = 0
          end
          object P_EPC: TRadioButton
            Left = 72
            Top = 8
            Width = 42
            Height = 17
            Caption = 'EPC'
            TabOrder = 1
          end
          object P_TID: TRadioButton
            Left = 113
            Top = 8
            Width = 38
            Height = 17
            Caption = 'TID'
            TabOrder = 2
          end
          object P_User: TRadioButton
            Left = 152
            Top = 8
            Width = 44
            Height = 17
            Caption = 'User'
            TabOrder = 3
          end
        end
        object GroupBox16: TGroupBox
          Left = 8
          Top = 38
          Width = 265
          Height = 136
          Caption = 'Lock of Password'
          TabOrder = 2
          object GroupBox4: TGroupBox
            Left = 8
            Top = 11
            Width = 233
            Height = 41
            TabOrder = 0
            object DestroyCode: TRadioButton
              Left = 8
              Top = 14
              Width = 84
              Height = 17
              Caption = 'Kill Password'
              TabOrder = 0
            end
            object AccessCode: TRadioButton
              Left = 112
              Top = 14
              Width = 113
              Height = 17
              Caption = 'Access Password'
              TabOrder = 1
            end
          end
          object NoProect: TRadioButton
            Left = 8
            Top = 54
            Width = 217
            Height = 17
            Caption = 'Readable and  writeable from any state'
            TabOrder = 1
          end
          object Always: TRadioButton
            Left = 8
            Top = 92
            Width = 250
            Height = 17
            Caption = 'Permanently readable and writeable'
            TabOrder = 3
          end
          object Proect: TRadioButton
            Left = 8
            Top = 74
            Width = 243
            Height = 17
            Caption = 'Readable and writeable from the secured state'
            TabOrder = 2
          end
          object AlwaysNot: TRadioButton
            Left = 9
            Top = 111
            Width = 169
            Height = 17
            Caption = 'Never readable and writeable'
            TabOrder = 4
          end
        end
        object GroupBox18: TGroupBox
          Left = 277
          Top = 43
          Width = 197
          Height = 93
          Caption = 'Lock of EPC TID and User Bank'
          TabOrder = 3
          object NoProect2: TRadioButton
            Left = 8
            Top = 17
            Width = 129
            Height = 17
            Caption = 'Writeable from any state'
            TabOrder = 0
          end
          object AlwaysNot2: TRadioButton
            Left = 8
            Top = 74
            Width = 113
            Height = 17
            Caption = 'Never writeable'
            TabOrder = 3
          end
          object Proect2: TRadioButton
            Left = 8
            Top = 36
            Width = 137
            Height = 17
            Caption = 'Writeable from the secured state'
            TabOrder = 1
          end
          object Always2: TRadioButton
            Left = 8
            Top = 55
            Width = 113
            Height = 17
            Caption = 'Permanently writeable'
            TabOrder = 2
          end
        end
      end
      object GroupBox9: TGroupBox
        Left = 498
        Top = 78
        Width = 292
        Height = 74
        Caption = 'Kill Tag'
        TabOrder = 2
        object Label33: TLabel
          Left = 9
          Top = 41
          Width = 62
          Height = 26
          Caption = 'Kill Password'#13#10'(8 Hex):'
        end
        object Button_DestroyCard: TButton
          Left = 192
          Top = 45
          Width = 94
          Height = 23
          Action = Action_DestroyCard
          Caption = 'Kill Tag'
          TabOrder = 2
        end
        object Edit_DestroyCode: TEdit
          Left = 78
          Top = 45
          Width = 107
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object ComboBox_EPC3: TComboBox
          Tag = 3
          Left = 10
          Top = 16
          Width = 276
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
      end
      object GroupBox10: TGroupBox
        Left = 8
        Top = 237
        Width = 481
        Height = 200
        Caption = 'Read Data / Write Data / Block Erase'
        TabOrder = 4
        object Label9: TLabel
          Left = 7
          Top = 117
          Width = 140
          Height = 26
          Caption = 'Password(Read/Block Erase)'#13#10'(0-120/Word/D):'
        end
        object Label18: TLabel
          Left = 8
          Top = 150
          Width = 82
          Height = 13
          Caption = 'Write Data (Hex):'
        end
        object Label19: TLabel
          Left = 8
          Top = 75
          Width = 157
          Height = 13
          Caption = 'Address of Tag Data(Word/Hex):'
        end
        object Label20: TLabel
          Left = 7
          Top = 99
          Width = 165
          Height = 13
          Caption = 'Length of Data(Read/Block Erase:'
        end
        object SpeedButton_Read_G2: TSpeedButton
          Left = 7
          Top = 170
          Width = 37
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Read'
          OnClick = SpeedButton_Read_G2Click
        end
        object ComboBox_EPC2: TComboBox
          Tag = 2
          Left = 8
          Top = 16
          Width = 265
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Edit_AccessCode2: TEdit
          Left = 184
          Top = 121
          Width = 89
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_WriteData: TEdit
          Left = 112
          Top = 146
          Width = 161
          Height = 21
          TabOrder = 6
          Text = '0000'
          OnChange = Edit_WriteDataChange
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_WordPtr: TEdit
          Left = 170
          Top = 72
          Width = 103
          Height = 21
          MaxLength = 2
          TabOrder = 3
          Text = '00'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_Len: TEdit
          Left = 184
          Top = 96
          Width = 89
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '4'
          OnKeyPress = Edit_LenKeyPress
        end
        object Memo_DataShow: TMemo
          Left = 279
          Top = 42
          Width = 194
          Height = 152
          ScrollBars = ssVertical
          TabOrder = 0
        end
        object GroupBox6: TGroupBox
          Left = 8
          Top = 37
          Width = 265
          Height = 33
          TabOrder = 2
          object C_Reserve: TRadioButton
            Left = 2
            Top = 10
            Width = 65
            Height = 17
            Caption = 'Password'
            TabOrder = 0
            OnClick = C_ReserveClick
          end
          object C_EPC: TRadioButton
            Left = 67
            Top = 10
            Width = 57
            Height = 17
            Caption = 'EPC'
            TabOrder = 1
            OnClick = C_EPCClick
          end
          object C_TID: TRadioButton
            Left = 131
            Top = 10
            Width = 56
            Height = 17
            Caption = 'TID'
            TabOrder = 2
            OnClick = C_TIDClick
          end
          object C_User: TRadioButton
            Left = 192
            Top = 10
            Width = 65
            Height = 17
            Caption = 'User'
            TabOrder = 3
            OnClick = C_UserClick
          end
        end
        object Button16: TButton
          Left = 229
          Top = 170
          Width = 42
          Height = 25
          Caption = 'Clear'
          TabOrder = 9
          OnClick = Button16Click
        end
        object Button_DataWrite: TButton
          Left = 49
          Top = 170
          Width = 39
          Height = 25
          Action = Action_ShowOrChangeData_write
          Caption = 'Write'
          TabOrder = 7
        end
        object Button_BlockErase: TButton
          Left = 159
          Top = 170
          Width = 66
          Height = 25
          Action = Action_ShowOrChangeData_BlockErase
          Caption = 'Block Erase'
          TabOrder = 8
        end
        object Button_writeblock: TButton
          Left = 93
          Top = 170
          Width = 62
          Height = 25
          Caption = 'Block Write'
          TabOrder = 10
          OnClick = Button_writeblockClick
        end
        object CheckBox2: TCheckBox
          Left = 278
          Top = 16
          Width = 131
          Height = 17
          Caption = 'Compute and add PC: '
          TabOrder = 11
          OnClick = CheckBox2Click
        end
        object Edit_PC: TEdit
          Left = 404
          Top = 12
          Width = 69
          Height = 21
          Color = clScrollBar
          ReadOnly = True
          TabOrder = 12
          Text = '0800'
        end
      end
      object GroupBox11: TGroupBox
        Left = 8
        Top = 0
        Width = 480
        Height = 184
        Caption = 'List EPC of Tags'
        TabOrder = 0
        object ListView_EPC: TListView
          Left = 8
          Top = 16
          Width = 465
          Height = 161
          Columns = <
            item
              Caption = 'No.'
            end
            item
              Caption = 'ID'
              Width = 280
            end
            item
              Caption = 'EPC Length'
              Width = 70
            end
            item
              Caption = 'Times'
            end>
          GridLines = True
          TabOrder = 0
          ViewStyle = vsReport
        end
      end
      object GroupBox17: TGroupBox
        Left = 498
        Top = 0
        Width = 291
        Height = 78
        Caption = 'Query Tag'
        TabOrder = 1
        object Label25: TLabel
          Left = 10
          Top = 17
          Width = 67
          Height = 13
          Caption = 'Read Interval:'
        end
        object SpeedButton_Query: TSpeedButton
          Left = 187
          Top = 9
          Width = 98
          Height = 25
          AllowAllUp = True
          GroupIndex = 1
          Caption = 'Query Tag'
          OnClick = Action_OpenTestModeExecute
        end
        object ComboBox_IntervalTime: TComboBox
          Left = 83
          Top = 13
          Width = 94
          Height = 21
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTimeChange
        end
        object GroupBox31: TGroupBox
          Left = 4
          Top = 34
          Width = 205
          Height = 41
          Caption = 'Query TID Parameter'
          TabOrder = 1
          object Label52: TLabel
            Left = 9
            Top = 21
            Width = 47
            Height = 13
            Caption = 'StartAddr:'
          end
          object Label53: TLabel
            Left = 120
            Top = 21
            Width = 21
            Height = 13
            Caption = 'Len:'
          end
          object Edit4: TEdit
            Left = 58
            Top = 16
            Width = 53
            Height = 21
            TabOrder = 0
            Text = '00'
          end
          object Edit5: TEdit
            Left = 145
            Top = 15
            Width = 51
            Height = 21
            TabOrder = 1
            Text = '00'
          end
        end
        object CheckBox_TID: TCheckBox
          Left = 214
          Top = 49
          Width = 55
          Height = 17
          Caption = 'TID'
          TabOrder = 2
          OnClick = CheckBox_TIDClick
        end
      end
      object GroupBox20: TGroupBox
        Left = 497
        Top = 231
        Width = 293
        Height = 181
        Caption = 'Read Protection'
        TabOrder = 5
        object Label32: TLabel
          Left = 9
          Top = 40
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object ComboBox_EPC4: TComboBox
          Tag = 3
          Left = 10
          Top = 14
          Width = 276
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode4: TEdit
          Left = 102
          Top = 40
          Width = 185
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Button_SetReadProtect_G2: TButton
          Left = 9
          Top = 67
          Width = 280
          Height = 25
          Action = Action_SetReadProtect_G2
          Caption = 'Set Single Tag Read Protection'
          TabOrder = 1
        end
        object Button_SetMultiReadProtect_G2: TButton
          Left = 8
          Top = 95
          Width = 281
          Height = 25
          Action = Action_SetMultiReadProtect_G2
          Caption = 'Set Single Tag Read Protection without EPC'
          TabOrder = 3
        end
        object Button_RemoveReadProtect_G2: TButton
          Left = 5
          Top = 123
          Width = 285
          Height = 25
          Action = Action_RemoveReadProtect_G2
          Caption = 'Reset Single Tag Read Protection without EPC'
          TabOrder = 4
        end
        object Button_CheckReadProtected_G2: TButton
          Left = 4
          Top = 150
          Width = 286
          Height = 25
          Action = Action_CheckReadProtected_G2
          Caption = 'Detect Single Tag Read Protection without EPC Password'
          TabOrder = 5
        end
      end
      object GroupBox21: TGroupBox
        Left = 498
        Top = 414
        Width = 292
        Height = 107
        Caption = 'EAS Alarm'
        TabOrder = 6
        object Label_Alarm: TLabel
          Left = 216
          Top = 39
          Width = 30
          Height = 30
          Caption = #9679
          Color = clBtnFace
          Font.Charset = GB2312_CHARSET
          Font.Color = clRed
          Font.Height = -30
          Font.Name = 'MS Sans Serif'
          Font.Style = []
          ParentColor = False
          ParentFont = False
          Visible = False
        end
        object Label35: TLabel
          Left = 9
          Top = 35
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object SpeedButton_CheckAlarm_G2: TSpeedButton
          Left = 202
          Top = 75
          Width = 81
          Height = 25
          AllowAllUp = True
          GroupIndex = 3
          Caption = 'Check Alarm'
          OnClick = Action_CheckEASAlarm_G2Execute
        end
        object Button_SetEASAlarm_G2: TButton
          Left = 96
          Top = 75
          Width = 81
          Height = 25
          Action = Action_SetEASAlarm_G2
          Caption = 'Alarm Setting'
          TabOrder = 3
        end
        object ComboBox_EPC5: TComboBox
          Tag = 3
          Left = 10
          Top = 15
          Width = 264
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode5: TEdit
          Left = 110
          Top = 42
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object GroupBox24: TGroupBox
          Left = 10
          Top = 61
          Width = 75
          Height = 43
          TabOrder = 2
          object Alarm_G2: TRadioButton
            Left = 5
            Top = 8
            Width = 57
            Height = 17
            Caption = 'Alarm'
            TabOrder = 0
          end
          object NoAlarm_G2: TRadioButton
            Left = 5
            Top = 24
            Width = 65
            Height = 17
            Caption = 'No Alarm'
            TabOrder = 1
          end
        end
      end
      object GroupBox22: TGroupBox
        Left = 498
        Top = 522
        Width = 291
        Height = 99
        Caption = 'Lock Block for User (Permanently Lock)'
        TabOrder = 8
        object Label36: TLabel
          Left = 10
          Top = 40
          Width = 98
          Height = 26
          Caption = 'Address of Tag Data'#13#10'(Word):'
        end
        object Label37: TLabel
          Left = 10
          Top = 67
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object Button_LockUserBlock_G2: TButton
          Left = 200
          Top = 66
          Width = 81
          Height = 25
          Action = Action_LockUserBlock_G2
          Caption = 'Lock'
          TabOrder = 2
        end
        object ComboBox_BlockNum: TComboBox
          Left = 110
          Top = 44
          Width = 81
          Height = 21
          ItemHeight = 13
          TabOrder = 1
        end
        object ComboBox_EPC6: TComboBox
          Tag = 3
          Left = 10
          Top = 18
          Width = 261
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode6: TEdit
          Left = 109
          Top = 70
          Width = 81
          Height = 21
          MaxLength = 8
          TabOrder = 3
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
      end
      object GroupBox23: TGroupBox
        Left = 498
        Top = 154
        Width = 292
        Height = 76
        Caption = 'Write EPC(Random write one tag in the antenna)'
        TabOrder = 3
        object Label38: TLabel
          Left = 9
          Top = 43
          Width = 84
          Height = 26
          Caption = 'Access Password'#13#10'(8 Hex):'
        end
        object Label39: TLabel
          Left = 8
          Top = 16
          Width = 53
          Height = 26
          Caption = 'Write EPC:'#13#10'(1-15Word)'
        end
        object Edit_AccessCode3: TEdit
          Left = 102
          Top = 47
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Button_WriteEPC_G2: TButton
          Left = 192
          Top = 43
          Width = 95
          Height = 25
          Action = Action_WriteEPC_G2
          Caption = 'Write EPC'
          TabOrder = 1
        end
        object Edit_WriteEPC: TEdit
          Left = 69
          Top = 16
          Width = 217
          Height = 21
          MaxLength = 60
          TabOrder = 0
          Text = '0000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
      end
      object GroupBox29: TGroupBox
        Left = 8
        Top = 185
        Width = 481
        Height = 50
        Caption = 'EPC Mask Enabled'
        TabOrder = 9
        object Label42: TLabel
          Left = 120
          Top = 25
          Width = 44
          Height = 13
          Caption = 'Maskadr:'
        end
        object Label43: TLabel
          Left = 320
          Top = 25
          Width = 47
          Height = 13
          Caption = 'MaskLen:'
        end
        object CheckBox1: TCheckBox
          Left = 8
          Top = 24
          Width = 65
          Height = 17
          Caption = 'Enabled'
          TabOrder = 0
        end
        object Edit2: TEdit
          Left = 168
          Top = 18
          Width = 105
          Height = 21
          TabOrder = 1
          Text = '00'
        end
        object Edit3: TEdit
          Left = 368
          Top = 18
          Width = 105
          Height = 21
          TabOrder = 2
          Text = '00'
        end
      end
    end
    object TabSheet_6B: TTabSheet
      Caption = '18000-6B Test'
      ImageIndex = 2
      object GroupBox12: TGroupBox
        Left = 8
        Top = 4
        Width = 769
        Height = 309
        Caption = 'List ID of Tags'
        TabOrder = 0
        object ListView_ID_6B: TListView
          Left = 8
          Top = 16
          Width = 750
          Height = 282
          Columns = <
            item
              Caption = 'No.'
            end
            item
              Caption = 'ID'
              Width = 500
            end
            item
              Caption = 'Times'
            end>
          GridLines = True
          TabOrder = 0
          ViewStyle = vsReport
        end
      end
      object GroupBox13: TGroupBox
        Left = 336
        Top = 316
        Width = 441
        Height = 304
        Caption = 
          'Read and Write Data Block / Permanently Write  Protect Block of ' +
          ' byte'
        TabOrder = 2
        object Label29: TLabel
          Left = 9
          Top = 90
          Width = 165
          Height = 13
          Caption = 'Write Data (1-32 Byte/Hex):           '
        end
        object Label30: TLabel
          Left = 9
          Top = 49
          Width = 102
          Height = 26
          Caption = 'Start/Protect Address'#13#10'(00-E9)(Hex):   '
        end
        object Label31: TLabel
          Left = 245
          Top = 49
          Width = 75
          Height = 26
          Caption = 'Length of Data:'#13#10'(1-32/Byte/D)   '
        end
        object SpeedButton_Read_6B: TSpeedButton
          Left = 8
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Read'
          OnClick = SpeedButton_ReadWrite_6BClick
        end
        object SpeedButton_Write_6B: TSpeedButton
          Left = 65
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = 'Write'
          OnClick = SpeedButton_ReadWrite_6BClick
        end
        object ComboBox_ID1_6B: TComboBox
          Tag = 2
          Left = 9
          Top = 20
          Width = 422
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_WriteData_6B: TEdit
          Left = 160
          Top = 85
          Width = 269
          Height = 21
          TabOrder = 3
          Text = '0000'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_StartAddress_6B: TEdit
          Left = 116
          Top = 54
          Width = 88
          Height = 21
          MaxLength = 2
          TabOrder = 1
          Text = '00'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_Len_6B: TEdit
          Left = 320
          Top = 54
          Width = 109
          Height = 21
          MaxLength = 2
          TabOrder = 2
          Text = '12'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Memo_DataShow_6B: TMemo
          Left = 8
          Top = 152
          Width = 420
          Height = 143
          ScrollBars = ssVertical
          TabOrder = 7
        end
        object Button14: TButton
          Left = 121
          Top = 117
          Width = 149
          Height = 25
          Action = Action_LockByte_6B
          Caption = 'Permanently Write  Protect'
          TabOrder = 4
        end
        object Button15: TButton
          Left = 275
          Top = 117
          Width = 89
          Height = 25
          Action = Action_CheckLock_6B
          Caption = 'Check Protect'
          TabOrder = 5
        end
        object Button22: TButton
          Left = 368
          Top = 117
          Width = 60
          Height = 25
          Caption = 'Clear'
          TabOrder = 6
          OnClick = Button22Click
        end
      end
      object GroupBox14: TGroupBox
        Left = 8
        Top = 452
        Width = 321
        Height = 168
        Caption = 'Query Tags by Condition'
        TabOrder = 3
        object Label34: TLabel
          Left = 8
          Top = 132
          Width = 133
          Height = 13
          Caption = 'Condition(<=8 Hex Number):'
        end
        object Label28: TLabel
          Left = 8
          Top = 92
          Width = 134
          Height = 13
          Caption = 'Address of Tag Data(0-223):'
        end
        object Edit_Query_StartAddress_6B: TEdit
          Left = 160
          Top = 87
          Width = 97
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '0'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Edit_ConditionContent_6B: TEdit
          Left = 160
          Top = 124
          Width = 97
          Height = 21
          MaxLength = 16
          TabOrder = 5
          Text = '00'
          OnKeyPress = Edit_NewComAdrKeyPress
        end
        object Less_6B: TRadioButton
          Left = 8
          Top = 56
          Width = 129
          Height = 17
          Caption = 'Less than Condition'
          TabOrder = 2
        end
        object Different_6B: TRadioButton
          Left = 160
          Top = 24
          Width = 113
          Height = 17
          Caption = 'Unequal Condition'
          TabOrder = 1
        end
        object Same_6B: TRadioButton
          Left = 8
          Top = 24
          Width = 113
          Height = 17
          Caption = 'Equal Condition'
          TabOrder = 0
        end
        object Greater_6B: TRadioButton
          Left = 160
          Top = 56
          Width = 137
          Height = 17
          Caption = 'Greater than Condition'
          TabOrder = 3
        end
      end
      object GroupBox19: TGroupBox
        Left = 8
        Top = 316
        Width = 321
        Height = 132
        Caption = 'Query Tag'
        TabOrder = 1
        object SpeedButton_Query_6B: TSpeedButton
          Left = 221
          Top = 79
          Width = 89
          Height = 26
          AllowAllUp = True
          GroupIndex = 1
          Caption = 'Query by one'
          OnClick = Action_Query_6BExecute
        end
        object Label27: TLabel
          Left = 8
          Top = 30
          Width = 67
          Height = 13
          Caption = 'Read Interval:'
        end
        object ComboBox_IntervalTime_6B: TComboBox
          Left = 113
          Top = 25
          Width = 199
          Height = 21
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTime_6BChange
        end
        object Byone_6B: TRadioButton
          Left = 8
          Top = 70
          Width = 153
          Height = 17
          Caption = 'Query by one'
          TabOrder = 1
        end
        object Bycondition_6B: TRadioButton
          Left = 8
          Top = 98
          Width = 161
          Height = 17
          Caption = 'Query by Condition'
          TabOrder = 2
        end
      end
    end
    object TabSheet1: TTabSheet
      Caption = 'Frequency Analysis'
      ImageIndex = 3
      object Label48: TLabel
        Left = 23
        Top = 11
        Width = 63
        Height = 13
        AutoSize = False
        Caption = 'Frequency'
      end
      object Label49: TLabel
        Left = 215
        Top = 11
        Width = 121
        Height = 13
        AutoSize = False
        Caption = 'Times'
      end
      object Label50: TLabel
        Left = 397
        Top = 11
        Width = 55
        Height = 13
        Caption = 'Percentage'
      end
      object Label54: TLabel
        Left = 13
        Top = 604
        Width = 132
        Height = 13
        Caption = 'Frequency hopping mode'#65306
      end
      object ListBox1: TListBox
        Left = 12
        Top = 27
        Width = 761
        Height = 561
        BiDiMode = bdLeftToRight
        Color = clBtnHighlight
        Ctl3D = False
        ExtendedSelect = False
        Font.Charset = ANSI_CHARSET
        Font.Color = clWindowText
        Font.Height = -11
        Font.Name = 'Courier'
        Font.Style = []
        ItemHeight = 13
        ParentBiDiMode = False
        ParentCtl3D = False
        ParentFont = False
        ParentShowHint = False
        ShowHint = False
        TabOrder = 0
      end
      object Button19: TButton
        Left = 481
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Start'
        TabOrder = 1
        OnClick = Button19Click
      end
      object Button20: TButton
        Left = 576
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Stop'
        Enabled = False
        TabOrder = 2
        OnClick = Button20Click
      end
      object Button18: TButton
        Left = 669
        Top = 594
        Width = 75
        Height = 25
        Caption = 'Clear'
        TabOrder = 3
        OnClick = Button18Click
      end
      object ComboBox8: TComboBox
        Left = 144
        Top = 598
        Width = 105
        Height = 21
        Style = csDropDownList
        ItemHeight = 13
        TabOrder = 4
        Items.Strings = (
          'Random'
          'Adaptive')
      end
      object Button21: TButton
        Left = 265
        Top = 595
        Width = 48
        Height = 25
        Caption = 'Set'
        Enabled = False
        TabOrder = 5
        OnClick = Button21Click
      end
      object Button23: TButton
        Left = 321
        Top = 595
        Width = 48
        Height = 25
        Caption = 'Get'
        Enabled = False
        TabOrder = 6
        OnClick = Button23Click
      end
    end
    object TabSheet2: TTabSheet
      Caption = 'TCPIP Config'
      ImageIndex = 4
      object GroupBox33: TGroupBox
        Left = 6
        Top = 5
        Width = 773
        Height = 427
        TabOrder = 0
        object ListView1: TListView
          Left = 8
          Top = 15
          Width = 604
          Height = 402
          Columns = <
            item
              Caption = 'NO.'
              Width = 60
            end
            item
              Caption = 'MAC'
              Width = 180
            end
            item
              Caption = 'IP'
              Width = 180
            end
            item
              Caption = 'User/Device '
              Width = 180
            end>
          GridLines = True
          Items.Data = {1D000000010000000000000000000000FFFFFFFF000000000000000000}
          RowSelect = True
          TabOrder = 0
          ViewStyle = vsReport
        end
        object Button11: TButton
          Left = 633
          Top = 29
          Width = 131
          Height = 25
          Caption = 'Search'
          TabOrder = 1
          OnClick = Button11Click
        end
        object Button12: TButton
          Left = 633
          Top = 71
          Width = 131
          Height = 25
          Caption = 'Specific  Search'
          TabOrder = 2
          OnClick = Button12Click
        end
        object Button13: TButton
          Left = 633
          Top = 112
          Width = 131
          Height = 25
          Caption = 'Configuration'
          TabOrder = 3
          OnClick = Button13Click
        end
        object Button17: TButton
          Left = 633
          Top = 155
          Width = 131
          Height = 25
          Caption = 'Change  IP'
          TabOrder = 4
          OnClick = Button17Click
        end
        object Button24: TButton
          Left = 633
          Top = 231
          Width = 131
          Height = 25
          Caption = 'LED Flashing'
          TabOrder = 5
          OnClick = Button24Click
        end
      end
      object GroupBox34: TGroupBox
        Left = 5
        Top = 439
        Width = 774
        Height = 173
        Caption = 'Note'
        TabOrder = 1
        object Label57: TLabel
          Left = 24
          Top = 26
          Width = 500
          Height = 13
          Caption = 
            'Search: Search local LAN devices by broadcasting. This operation' +
            ' can not find devices in different subnet'
        end
        object Label58: TLabel
          Left = 24
          Top = 54
          Width = 584
          Height = 13
          Caption = 
            'Specific Search: Search for devices by designated IP address. It' +
            ' is commonly used for finding the devices in different subnet'
        end
        object Label59: TLabel
          Left = 24
          Top = 85
          Width = 268
          Height = 13
          Caption = 'Configuration: Configure designated device'#39's parameters.'
        end
        object Label60: TLabel
          Left = 24
          Top = 112
          Width = 243
          Height = 13
          Caption = 'Change IP: Change designated device'#39's IP address'
        end
        object Label61: TLabel
          Left = 24
          Top = 143
          Width = 460
          Height = 13
          Caption = 
            'LED Flashing: Let the device'#39's LED flash quickly. It is used to ' +
            'fast distinguish a device on the field'
        end
      end
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 651
    Width = 801
    Height = 19
    AutoHint = True
    Panels = <
      item
        Width = 600
      end
      item
        Text = 'Port:'
        Width = 56
      end
      item
        Text = 'statusManufacturer nameBarPanel1'
        Width = 200
      end>
  end
  object ActionList1: TActionList
    Left = 371
    Top = 2
    object Action_GetReaderInformation: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #33719#24471#35835#20889#22120#20449#24687
      OnExecute = Action_GetReaderInformationExecute
      OnUpdate = Action_GetReaderInformationUpdate
    end
    object Action_OpenCOM: TAction
      Category = #36890#35759
      Caption = 'Open Com Port'
      OnExecute = Action_OpenCOMExecute
    end
    object Action_OpenRf: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #25171#24320#23556#39057
    end
    object Action_CloseCOM: TAction
      Category = #36890#35759
      Caption = 'Close Com Port'
      OnExecute = Action_CloseCOMExecute
    end
    object Action_CloseRf: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20851#38381#23556#39057
    end
    object Action_WriteComAdr: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20889#20837#35835#20889#22120#22320#22336
    end
    object Action_WriteInventoryScanTime: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #20889#20837
      Hint = #20889#20837#35810#26597#21629#20196#26368#22823#21709#24212#26102#38388
    end
    object Action_OpenTestMode: TAction
      Category = #27979#35797#27169#24335
      Caption = #26597#35810#26631#31614
      OnExecute = Action_OpenTestModeExecute
    end
    object Action_CloseTestMode: TAction
      Category = #27979#35797#27169#24335
      Caption = #20851#38381#27979#35797#27169#24335
      OnExecute = Action_OpenTestModeExecute
    end
    object Action_GetSystemInformation: TAction
      Tag = 2
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #33719#21462#30005#23376#26631#31614#35814#32454#20449#24687
    end
    object Action_SetReaderInformation: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #35774#32622#21442#25968
      OnExecute = Action_SetReaderInformationExecute
    end
    object Action_SetReaderInformation_0: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #24674#22797#20986#21378#21442#25968
      OnExecute = Action_SetReaderInformationExecute
    end
    object Action_Inventory: TAction
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = 'Action_Inventory'
      OnExecute = Action_InventoryExecute
    end
    object Action_ShowOrChangeData: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35835
      OnExecute = Action_ShowOrChangeDataExecute
    end
    object Action_SetProtectState: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#20445#25252
      OnExecute = Action_SetProtectStateExecute
      OnUpdate = Action_SetProtectStateUpdate
    end
    object Action_DestroyCard: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #38144#27585
      OnExecute = Action_DestroyCardExecute
    end
    object Action_Inventroy_6B: TAction
      Category = '18000-6B'
      Caption = #26597#35810#26631#31614
    end
    object Action_Query_6B: TAction
      Category = '18000-6B'
      Caption = #26597#35810#26631#31614
      OnExecute = Action_Query_6BExecute
    end
    object Action_WriteData_6B: TAction
      Category = '18000-6B'
      Caption = #20889#25968#25454
    end
    object Action_ReadData_6B: TAction
      Category = '18000-6B'
      Caption = #35835#25968#25454
    end
    object Action_LockByte_6B: TAction
      Category = '18000-6B'
      Caption = #38145#23450
      OnExecute = Action_LockByte_6BExecute
    end
    object Action_CheckLock_6B: TAction
      Category = '18000-6B'
      Caption = #26816#27979#38145#23450
      OnExecute = Action_CheckLock_6BExecute
      OnUpdate = Action_CheckLock_6BUpdate
    end
    object Action_Query2_6B: TAction
      Category = '18000-6B'
      Caption = 'Action_Query2_6B'
    end
    object Action_ShowOrChangeData_write: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #20889
      OnExecute = Action_ShowOrChangeDataExecute
    end
    object Action_ShowOrChangeData_BlockErase: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #22359#25830#38500
      OnExecute = Action_ShowOrChangeDataExecute
    end
    object Action_SetReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#21333#24352#35835#20445#25252
      OnExecute = Action_SetReadProtect_G2Execute
    end
    object Action_RemoveReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35299#38500#21333#24352#35835#20445#25252
      OnExecute = Action_RemoveReadProtect_G2Execute
    end
    object Action_SetMultiReadProtect_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622#22810#24352#35835#20445#25252
      OnExecute = Action_SetMultiReadProtect_G2Execute
    end
    object Action_CheckReadProtected_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #26816#27979#21333#24352#34987#35835#20445#25252#65288#19981#38656#35201#21345#21495#23494#30721#65289'       '
      OnExecute = Action_CheckReadProtected_G2Execute
    end
    object Action_SetEASAlarm_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #35774#32622
      OnExecute = Action_SetEASAlarm_G2Execute
    end
    object Action_CheckEASAlarm_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #26816#27979
      OnExecute = Action_CheckEASAlarm_G2Execute
    end
    object Action_LockUserBlock_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #38145#23450
      OnExecute = Action_LockUserBlock_G2Execute
    end
    object Action_WriteEPC_G2: TAction
      Category = #21345#29255#25805#20316'(TAG=2)'
      Caption = #20889'EPC'
      OnExecute = Action_WriteEPC_G2Execute
    end
  end
  object Timer_Test_: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer_Test_Timer
    Left = 320
    Top = 2
  end
  object Timer_Test_6B: TTimer
    Enabled = False
    Interval = 50
    OnTimer = Timer_G2_Timer
    Left = 443
    Top = 2
  end
  object Timer_G2_Alarm: TTimer
    Interval = 200
    OnTimer = Timer_G2_AlarmTimer
    Left = 720
    Top = 2
  end
  object Timer_G2_Read: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer_G2_ReadTimer
    Left = 267
    Top = 322
  end
  object Timer_6B_ReadWrite: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer_6B_ReadWriteTimer
    Left = 766
    Top = 162
  end
  object Timer1: TTimer
    Enabled = False
    Interval = 20
    OnTimer = Timer1Timer
    Left = 36
    Top = 472
  end
  object IdUDPServer1: TIdUDPServer
    BroadcastEnabled = True
    Bindings = <>
    DefaultPort = 11
    OnUDPRead = IdUDPServer1UDPRead
    Left = 760
    Top = 328
  end
  object IdUDPServer2: TIdUDPServer
    BroadcastEnabled = True
    Bindings = <>
    DefaultPort = 11
    OnUDPRead = IdUDPServer2UDPRead
    Left = 760
    Top = 360
  end
end
