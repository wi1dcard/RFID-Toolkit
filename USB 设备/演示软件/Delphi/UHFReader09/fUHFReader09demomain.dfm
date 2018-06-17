object frmUHFReader09demomain: TfrmUHFReader09demomain
  Left = 307
  Top = 69
  BorderIcons = [biSystemMenu, biMinimize]
  BorderStyle = bsSingle
  Caption = 'UHFReader09 Demo Software v1.6'
  ClientHeight = 671
  ClientWidth = 794
  Color = clScrollBar
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
    Width = 794
    Height = 652
    ActivePage = TabSheet_CMD
    Align = alClient
    TabOrder = 0
    OnChange = PageControl1Change
    object TabSheet_CMD: TTabSheet
      Caption = #35835#20889#22120#21442#25968#35774#32622
      object GroupBox_ReaderInfo: TGroupBox
        Left = 136
        Top = 10
        Width = 633
        Height = 119
        Caption = #35835#20889#22120#20449#24687
        TabOrder = 1
        object Label2: TLabel
          Left = 162
          Top = 24
          Width = 36
          Height = 13
          Caption = #29256#26412#65306
        end
        object Label3: TLabel
          Left = 10
          Top = 58
          Width = 36
          Height = 13
          Caption = #22320#22336#65306
        end
        object Label4: TLabel
          Left = 328
          Top = 58
          Width = 132
          Height = 13
          Caption = #35810#26597#21629#20196#26368#22823#21709#24212#26102#38388#65306
        end
        object Label10: TLabel
          Left = 10
          Top = 24
          Width = 36
          Height = 13
          Caption = #22411#21495#65306
        end
        object Label11: TLabel
          Left = 328
          Top = 24
          Width = 60
          Height = 13
          Caption = #25903#25345#21327#35758#65306
        end
        object Label8: TLabel
          Left = 160
          Top = 58
          Width = 36
          Height = 13
          Caption = #21151#29575#65306
        end
        object Label13: TLabel
          Left = 160
          Top = 92
          Width = 60
          Height = 13
          Caption = #26368#39640#39057#29575#65306
        end
        object Label14: TLabel
          Left = 10
          Top = 92
          Width = 60
          Height = 13
          Caption = #26368#20302#39057#29575#65306
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
          Top = 54
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 4
        end
        object Edit_scantime: TEdit
          Left = 488
          Top = 54
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
          Left = 488
          Top = 85
          Width = 129
          Height = 25
          Action = Action_GetReaderInformation
          TabOrder = 7
        end
        object Edit_dmaxfre: TEdit
          Left = 225
          Top = 88
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 9
        end
        object Edit_dminfre: TEdit
          Left = 72
          Top = 88
          Width = 81
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 8
        end
        object Edit_powerdBm: TEdit
          Left = 225
          Top = 54
          Width = 96
          Height = 21
          Color = clSilver
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ReadOnly = True
          TabOrder = 5
        end
        object EPCC1G2: TCheckBox
          Left = 488
          Top = 30
          Width = 73
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'EPCC1-G2'
          ParentBiDiMode = False
          TabOrder = 3
        end
        object ISO180006B: TCheckBox
          Left = 488
          Top = 14
          Width = 89
          Height = 17
          BiDiMode = bdLeftToRight
          Caption = 'ISO18000-6B'
          ParentBiDiMode = False
          TabOrder = 0
        end
      end
      object GroupBox_COM: TGroupBox
        Left = 8
        Top = 10
        Width = 117
        Height = 221
        Caption = #36890#35759
        TabOrder = 0
        object Label6: TLabel
          Left = 12
          Top = 19
          Width = 36
          Height = 13
          Caption = #31471#21475#65306
        end
        object Label12: TLabel
          Left = 13
          Top = 139
          Width = 87
          Height = 13
          Caption = #24050#25171#24320#31471#21475#65306'     '
        end
        object Label46: TLabel
          Left = 16
          Top = 96
          Width = 48
          Height = 13
          Caption = #27874#29305#29575#65306
        end
        object ComboBox_COM: TComboBox
          Left = 40
          Top = 16
          Width = 63
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_COMChange
        end
        object Button2: TButton
          Left = 12
          Top = 66
          Width = 92
          Height = 25
          Action = Action_OpenCOM
          TabOrder = 3
        end
        object Button4: TButton
          Left = 12
          Top = 184
          Width = 92
          Height = 25
          Action = Action_CloseCOM
          TabOrder = 5
        end
        object StaticText1: TStaticText
          Left = 13
          Top = 44
          Width = 64
          Height = 17
          Caption = #35835#20889#22120#22320#22336
          TabOrder = 2
        end
        object Edit_CmdComAddr: TEdit
          Left = 76
          Top = 41
          Width = 25
          Height = 21
          CharCase = ecUpperCase
          MaxLength = 2
          TabOrder = 1
          Text = 'FF'
          OnKeyPress = s
        end
        object ComboBox_AlreadyOpenCOM: TComboBox
          Left = 13
          Top = 157
          Width = 92
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 4
          OnCloseUp = ComboBox_AlreadyOpenCOMCloseUp
        end
        object ComboBox_baud2: TComboBox
          Left = 13
          Top = 112
          Width = 93
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
      object GroupBox2: TGroupBox
        Left = 136
        Top = 136
        Width = 633
        Height = 148
        Caption = #35774#32622#35835#20889#22120#21442#25968
        TabOrder = 2
        object Label15: TLabel
          Left = 8
          Top = 88
          Width = 60
          Height = 13
          Caption = #26368#20302#39057#29575#65306
        end
        object Label16: TLabel
          Left = 8
          Top = 119
          Width = 60
          Height = 13
          Caption = #26368#39640#39057#29575#65306
        end
        object Label17: TLabel
          Left = 202
          Top = 24
          Width = 48
          Height = 13
          Caption = #27874#29305#29575#65306
        end
        object Label1: TLabel
          Left = 8
          Top = 26
          Width = 64
          Height = 13
          Caption = #22320#22336'(HEX)'#65306
        end
        object Label7: TLabel
          Left = 8
          Top = 57
          Width = 36
          Height = 13
          Caption = #21151#29575#65306
        end
        object Label5: TLabel
          Left = 202
          Top = 57
          Width = 144
          Height = 13
          Caption = #35810#26597#21629#20196#26368#22823#21709#24212#26102#38388#65306'    '
        end
        object Button5: TButton
          Left = 344
          Top = 113
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation
          TabOrder = 6
        end
        object ComboBox_baud: TComboBox
          Left = 344
          Top = 22
          Width = 123
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
          Top = 22
          Width = 113
          Height = 21
          MaxLength = 2
          TabOrder = 0
          Text = '00'
          OnKeyPress = s
        end
        object ComboBox_scantime: TComboBox
          Left = 344
          Top = 53
          Width = 123
          Height = 21
          Style = csDropDownList
          ImeName = #20013#25991' ('#31616#20307') - '#24494#36719#25340#38899
          ItemHeight = 13
          TabOrder = 3
        end
        object Button1: TButton
          Left = 488
          Top = 113
          Width = 129
          Height = 25
          Action = Action_SetReaderInformation_0
          TabOrder = 7
        end
        object ComboBox_dminfre: TComboBox
          Left = 80
          Top = 84
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
          Top = 115
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 8
          OnSelect = ComboBox_dfreSelect
        end
        object ComboBox_PowerDbm: TComboBox
          Left = 80
          Top = 53
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
            '13')
        end
        object CheckBox_SameFre: TCheckBox
          Left = 202
          Top = 88
          Width = 81
          Height = 17
          Caption = #21333#39057#28857
          TabOrder = 5
          OnClick = CheckBox_SameFreClick
        end
        object GroupBox3: TGroupBox
          Left = 473
          Top = 10
          Width = 153
          Height = 99
          Caption = #39057#27573#36873#25321
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
            Top = 80
            Width = 113
            Height = 17
            Caption = 'EU band'
            TabOrder = 4
            OnClick = RadioButton_band5Click
          end
        end
      end
      object GroupBox8: TGroupBox
        Left = 136
        Top = 290
        Width = 409
        Height = 119
        Caption = #22768#20809#25511#21046
        TabOrder = 3
        object Label23: TLabel
          Left = 32
          Top = 24
          Width = 123
          Height = 13
          Caption = #28783#20142#21644#34562#40483#22120#40483#21483#26102#38388':'
        end
        object Label26: TLabel
          Left = 32
          Top = 57
          Width = 123
          Height = 13
          Caption = #28783#28781#21644#34562#40483#22120#38745#40664#26102#38388':'
        end
        object Label40: TLabel
          Left = 32
          Top = 90
          Width = 123
          Height = 13
          Caption = #28783#20142#21644#34562#40483#22120#40483#21483#27425#25968':'
        end
        object ComboBox1: TComboBox
          Left = 168
          Top = 21
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object ComboBox2: TComboBox
          Left = 168
          Top = 54
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object ComboBox3: TComboBox
          Left = 168
          Top = 87
          Width = 113
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 2
        end
        object Button6: TButton
          Left = 296
          Top = 24
          Width = 97
          Height = 25
          Caption = #35774#32622
          TabOrder = 3
          OnClick = Button6Click
        end
      end
      object GroupBox15: TGroupBox
        Left = 552
        Top = 289
        Width = 185
        Height = 120
        Caption = #34562#40483#22120#24320#20851
        TabOrder = 4
        object RadioButton1: TRadioButton
          Left = 24
          Top = 37
          Width = 57
          Height = 17
          Caption = #24320
          Checked = True
          TabOrder = 0
          TabStop = True
        end
        object RadioButton2: TRadioButton
          Left = 88
          Top = 37
          Width = 57
          Height = 17
          Caption = #20851
          TabOrder = 1
        end
        object Button7: TButton
          Left = 24
          Top = 69
          Width = 57
          Height = 25
          Caption = #35774#32622
          TabOrder = 2
          OnClick = Button7Click
        end
        object Button9: TButton
          Left = 96
          Top = 69
          Width = 57
          Height = 25
          Caption = #35835#21462
          TabOrder = 3
          OnClick = Button9Click
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
        Caption = #35774#32622#35835#20889#20445#25252#29366#24577
        TabOrder = 7
        object Label24: TLabel
          Left = 224
          Top = 135
          Width = 132
          Height = 13
          Caption = #35775#38382#23494#30721#65306'(8'#20010'16'#36827#21046#25968')'
        end
        object ComboBox_EPC1: TComboBox
          Tag = 1
          Left = 8
          Top = 18
          Width = 209
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 1
        end
        object Button_SetProtectState: TButton
          Left = 368
          Top = 146
          Width = 97
          Height = 25
          Action = Action_SetProtectState
          TabOrder = 4
        end
        object Edit_AccessCode1: TEdit
          Left = 224
          Top = 150
          Width = 91
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = s
        end
        object GroupBox1: TGroupBox
          Left = 224
          Top = 10
          Width = 250
          Height = 31
          TabOrder = 0
          object P_Reserve: TRadioButton
            Left = 8
            Top = 8
            Width = 61
            Height = 17
            Caption = #20445#30041#21306
            TabOrder = 0
          end
          object P_EPC: TRadioButton
            Left = 72
            Top = 8
            Width = 54
            Height = 17
            Caption = 'EPC'#21306
            TabOrder = 1
          end
          object P_TID: TRadioButton
            Left = 128
            Top = 8
            Width = 53
            Height = 17
            Caption = 'TID'#21306
            TabOrder = 2
          end
          object P_User: TRadioButton
            Left = 184
            Top = 8
            Width = 62
            Height = 17
            Caption = #29992#25143#21306
            TabOrder = 3
          end
        end
        object GroupBox16: TGroupBox
          Left = 8
          Top = 41
          Width = 208
          Height = 131
          Caption = #23494#30721#21306#30340#35835#20889#20445#25252#31867#22411
          TabOrder = 2
          object GroupBox4: TGroupBox
            Left = 8
            Top = 13
            Width = 169
            Height = 34
            TabOrder = 0
            object DestroyCode: TRadioButton
              Left = 8
              Top = 11
              Width = 73
              Height = 17
              Caption = #38144#27585#23494#30721
              TabOrder = 0
            end
            object AccessCode: TRadioButton
              Left = 88
              Top = 11
              Width = 73
              Height = 17
              Caption = #35775#38382#23494#30721
              TabOrder = 1
            end
          end
          object NoProect: TRadioButton
            Left = 16
            Top = 51
            Width = 177
            Height = 17
            Caption = #26080#20445#25252#19979#30340#21487#35835#21487#20889
            TabOrder = 1
          end
          object Always: TRadioButton
            Left = 16
            Top = 90
            Width = 113
            Height = 17
            Caption = #27704#36828#21487#35835#21487#20889
            TabOrder = 3
          end
          object Proect: TRadioButton
            Left = 16
            Top = 70
            Width = 169
            Height = 17
            Caption = #23494#30721#20445#25252#19979#30340#21487#35835#21487#20889
            TabOrder = 2
          end
          object AlwaysNot: TRadioButton
            Left = 16
            Top = 107
            Width = 169
            Height = 17
            Caption = #27704#36828#19981#33021#35835#19981#33021#20889
            TabOrder = 4
          end
        end
        object GroupBox18: TGroupBox
          Left = 224
          Top = 43
          Width = 249
          Height = 90
          Caption = 'EPC-TID-'#29992#25143#21306#30340#20889#20445#25252#31867#22411
          TabOrder = 3
          object NoProect2: TRadioButton
            Left = 8
            Top = 15
            Width = 129
            Height = 17
            Caption = #26080#20445#25252#19979#30340#21487#20889
            TabOrder = 0
          end
          object AlwaysNot2: TRadioButton
            Left = 8
            Top = 71
            Width = 113
            Height = 17
            Caption = #27704#36828#19981#21487#20889
            TabOrder = 3
          end
          object Proect2: TRadioButton
            Left = 8
            Top = 34
            Width = 137
            Height = 17
            Caption = #23494#30721#20445#25252#19979#30340#21487#20889
            TabOrder = 1
          end
          object Always2: TRadioButton
            Left = 8
            Top = 52
            Width = 113
            Height = 17
            Caption = #27704#36828#21487#20889
            TabOrder = 2
          end
        end
      end
      object GroupBox9: TGroupBox
        Left = 498
        Top = 106
        Width = 281
        Height = 77
        Caption = #38144#27585#26631#31614
        TabOrder = 2
        object Label33: TLabel
          Left = 9
          Top = 41
          Width = 72
          Height = 26
          Caption = #38144#27585#23494#30721#65306#13#10'(8'#20010'16'#36827#21046#25968')'
        end
        object Button_DestroyCard: TButton
          Left = 192
          Top = 45
          Width = 81
          Height = 23
          Action = Action_DestroyCard
          TabOrder = 2
        end
        object Edit_DestroyCode: TEdit
          Left = 94
          Top = 45
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = s
        end
        object ComboBox_EPC3: TComboBox
          Tag = 3
          Left = 10
          Top = 16
          Width = 261
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
      end
      object GroupBox10: TGroupBox
        Left = 8
        Top = 243
        Width = 481
        Height = 193
        Caption = #35835#25968#25454'/'#20889#25968#25454'/'#22359#25830#38500
        TabOrder = 4
        object Label9: TLabel
          Left = 8
          Top = 115
          Width = 132
          Height = 13
          Caption = #35775#38382#23494#30721#65306'(8'#20010'16'#36827#21046#25968')'
        end
        object Label18: TLabel
          Left = 8
          Top = 142
          Width = 90
          Height = 13
          Caption = #20889#25968#25454#65306'(16'#36827#21046')'
        end
        object Label19: TLabel
          Left = 8
          Top = 79
          Width = 71
          Height = 26
          Caption = #36215#22987#22320#22336':'#13#10'('#23383'/16'#36827#21046#25968')'
        end
        object Label20: TLabel
          Left = 125
          Top = 79
          Width = 103
          Height = 26
          Caption = #35835'/'#22359#25830#38500#38271#24230#65306#13#10'(0-120/'#23383'/10'#36827#21046#25968')'
        end
        object SpeedButton_Read_G2: TSpeedButton
          Left = 9
          Top = 162
          Width = 35
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = #35835
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
          Left = 152
          Top = 111
          Width = 121
          Height = 21
          MaxLength = 8
          TabOrder = 5
          Text = '00000000'
          OnKeyPress = s
        end
        object Edit_WriteData: TEdit
          Left = 112
          Top = 138
          Width = 161
          Height = 21
          TabOrder = 6
          Text = '0000'
          OnChange = Edit_WriteDataChange
          OnKeyPress = s
        end
        object Edit_WordPtr: TEdit
          Left = 82
          Top = 84
          Width = 40
          Height = 21
          MaxLength = 2
          TabOrder = 3
          Text = '00'
          OnKeyPress = s
        end
        object Edit_Len: TEdit
          Left = 229
          Top = 84
          Width = 43
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '4'
          OnKeyPress = Edit_LenKeyPress
        end
        object Memo_DataShow: TMemo
          Left = 279
          Top = 45
          Width = 194
          Height = 142
          ScrollBars = ssVertical
          TabOrder = 0
        end
        object GroupBox6: TGroupBox
          Left = 8
          Top = 41
          Width = 265
          Height = 33
          TabOrder = 2
          object C_Reserve: TRadioButton
            Left = 2
            Top = 10
            Width = 65
            Height = 17
            Caption = #20445#30041#21306
            TabOrder = 0
            OnClick = C_ReserveClick
          end
          object C_EPC: TRadioButton
            Left = 67
            Top = 10
            Width = 57
            Height = 17
            Caption = 'EPC'#21306
            TabOrder = 1
            OnClick = C_EPCClick
          end
          object C_TID: TRadioButton
            Left = 131
            Top = 10
            Width = 56
            Height = 17
            Caption = 'TID'#21306
            TabOrder = 2
            OnClick = C_TIDClick
          end
          object C_User: TRadioButton
            Left = 192
            Top = 10
            Width = 65
            Height = 17
            Caption = #29992#25143#21306
            TabOrder = 3
            OnClick = C_UserClick
          end
        end
        object Button16: TButton
          Left = 219
          Top = 162
          Width = 54
          Height = 25
          Caption = #28165#38500#26174#31034
          TabOrder = 9
          OnClick = Button16Click
        end
        object Button_DataWrite: TButton
          Left = 48
          Top = 162
          Width = 37
          Height = 25
          Action = Action_ShowOrChangeData_write
          TabOrder = 7
        end
        object Button_BlockErase: TButton
          Left = 154
          Top = 162
          Width = 60
          Height = 25
          Action = Action_ShowOrChangeData_BlockErase
          TabOrder = 8
        end
        object Button_writeblock: TButton
          Left = 90
          Top = 162
          Width = 59
          Height = 25
          Caption = #22359#20889
          TabOrder = 10
          OnClick = Button_writeblockClick
        end
        object CheckBox2: TCheckBox
          Left = 280
          Top = 21
          Width = 137
          Height = 17
          Caption = #33258#21160#35745#31639#24182#28155#21152'PC'
          TabOrder = 11
          OnClick = CheckBox2Click
        end
        object Edit_PC: TEdit
          Left = 414
          Top = 17
          Width = 58
          Height = 21
          Color = cl3DLight
          TabOrder = 12
          Text = '0800'
        end
      end
      object GroupBox11: TGroupBox
        Left = 8
        Top = 0
        Width = 480
        Height = 198
        Caption = #21345#29255#26174#31034
        TabOrder = 0
        object ListView_EPC: TListView
          Left = 8
          Top = 16
          Width = 465
          Height = 169
          Columns = <
            item
              Caption = #24207#21495
            end
            item
              Caption = #21345#21495
              Width = 280
            end
            item
              Caption = 'EPC'#38271#24230
              Width = 60
            end
            item
              Caption = #27425#25968
            end>
          GridLines = True
          TabOrder = 0
          ViewStyle = vsReport
        end
      end
      object GroupBox17: TGroupBox
        Left = 498
        Top = 0
        Width = 281
        Height = 105
        Caption = #23547#21345
        TabOrder = 1
        object Label25: TLabel
          Left = 10
          Top = 17
          Width = 84
          Height = 13
          Caption = #35835#21345#38388#38548#26102#38388#65306
        end
        object SpeedButton_Query: TSpeedButton
          Left = 191
          Top = 73
          Width = 81
          Height = 25
          AllowAllUp = True
          GroupIndex = 1
          Caption = #26597#35810#26631#31614
          OnClick = Action_OpenTestModeExecute
        end
        object ComboBox_IntervalTime: TComboBox
          Left = 115
          Top = 13
          Width = 158
          Height = 21
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTimeChange
        end
        object GroupBox7: TGroupBox
          Left = 7
          Top = 38
          Width = 171
          Height = 61
          Caption = 'TID'#23547#26597#26465#20214
          TabOrder = 1
          object Label21: TLabel
            Left = 8
            Top = 16
            Width = 60
            Height = 13
            Caption = #36215#22987#22320#22336#65306
          end
          object Label22: TLabel
            Left = 8
            Top = 39
            Width = 60
            Height = 13
            Caption = #25968#25454#23383#25968#65306
          end
          object Edit1: TEdit
            Left = 77
            Top = 13
            Width = 84
            Height = 21
            Enabled = False
            MaxLength = 2
            TabOrder = 0
            Text = '02'
            OnKeyPress = s
          end
          object Edit4: TEdit
            Left = 77
            Top = 36
            Width = 84
            Height = 21
            Enabled = False
            MaxLength = 2
            TabOrder = 1
            Text = '04'
            OnKeyPress = s
          end
        end
        object CheckBox_TID: TCheckBox
          Left = 193
          Top = 45
          Width = 74
          Height = 17
          Caption = 'TID'#35810#26597
          TabOrder = 2
          OnClick = CheckBox_TIDClick
        end
      end
      object GroupBox20: TGroupBox
        Left = 498
        Top = 263
        Width = 281
        Height = 149
        Caption = #35835#20445#25252
        TabOrder = 5
        object Label32: TLabel
          Left = 9
          Top = 38
          Width = 72
          Height = 26
          Caption = #35775#38382#23494#30721#65306#13#10'(8'#20010'16'#36827#21046#25968')'
        end
        object ComboBox_EPC4: TComboBox
          Tag = 3
          Left = 10
          Top = 14
          Width = 261
          Height = 21
          Style = csDropDownList
          ItemHeight = 13
          TabOrder = 0
        end
        object Edit_AccessCode4: TEdit
          Left = 94
          Top = 38
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = s
        end
        object Button_SetReadProtect_G2: TButton
          Left = 179
          Top = 37
          Width = 94
          Height = 25
          Action = Action_SetReadProtect_G2
          TabOrder = 1
        end
        object Button_SetMultiReadProtect_G2: TButton
          Left = 8
          Top = 65
          Width = 265
          Height = 25
          Action = Action_SetMultiReadProtect_G2
          Caption = #35774#32622#21333#24352#35835#20445#25252#65288#19981#38656#21345#21495#65289
          TabOrder = 3
        end
        object Button_RemoveReadProtect_G2: TButton
          Left = 8
          Top = 92
          Width = 265
          Height = 25
          Action = Action_RemoveReadProtect_G2
          Caption = #35299#38500#21333#24352#35835#20445#25252#65288#19981#38656#21345#21495#65289
          TabOrder = 4
        end
        object Button_CheckReadProtected_G2: TButton
          Left = 8
          Top = 119
          Width = 265
          Height = 25
          Action = Action_CheckReadProtected_G2
          Caption = #26816#27979#21333#24352#34987#35835#20445#25252#65288#19981#38656#21345#21495#23494#30721#65289'       '
          TabOrder = 5
        end
      end
      object GroupBox21: TGroupBox
        Left = 498
        Top = 414
        Width = 281
        Height = 112
        Caption = 'EAS'#25253#35686
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
          Top = 39
          Width = 72
          Height = 26
          Caption = #35775#38382#23494#30721#65306#13#10'(8'#20010'16'#36827#21046#25968')'
        end
        object SpeedButton_CheckAlarm_G2: TSpeedButton
          Left = 192
          Top = 75
          Width = 81
          Height = 25
          AllowAllUp = True
          GroupIndex = 3
          Caption = #26816#27979#25253#35686
          OnClick = Action_CheckEASAlarm_G2Execute
        end
        object Button_SetEASAlarm_G2: TButton
          Left = 96
          Top = 75
          Width = 81
          Height = 25
          Action = Action_SetEASAlarm_G2
          Caption = #25253#35686#35774#32622
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
          Left = 94
          Top = 42
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 1
          Text = '00000000'
          OnKeyPress = s
        end
        object GroupBox24: TGroupBox
          Left = 10
          Top = 65
          Width = 75
          Height = 43
          TabOrder = 2
          object Alarm_G2: TRadioButton
            Left = 5
            Top = 8
            Width = 57
            Height = 17
            Caption = #25253#35686
            TabOrder = 0
          end
          object NoAlarm_G2: TRadioButton
            Left = 5
            Top = 24
            Width = 65
            Height = 17
            Caption = #19981#25253#35686
            TabOrder = 1
          end
        end
      end
      object GroupBox22: TGroupBox
        Left = 498
        Top = 528
        Width = 281
        Height = 92
        Caption = #38145#23450#29992#25143#21306#25968#25454#22359#38145#65288#27704#20037#38145#23450#65289
        TabOrder = 8
        object Label36: TLabel
          Left = 10
          Top = 45
          Width = 84
          Height = 13
          Caption = #25968#25454#22359#23383#22320#22336#65306
        end
        object Label37: TLabel
          Left = 10
          Top = 61
          Width = 72
          Height = 26
          Caption = #35775#38382#23494#30721#65306#13#10'(8'#20010'16'#36827#21046#25968')'
        end
        object Button_LockUserBlock_G2: TButton
          Left = 192
          Top = 62
          Width = 81
          Height = 25
          Action = Action_LockUserBlock_G2
          TabOrder = 2
        end
        object ComboBox_BlockNum: TComboBox
          Left = 94
          Top = 41
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
          Left = 94
          Top = 66
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 3
          Text = '00000000'
          OnKeyPress = s
        end
      end
      object GroupBox23: TGroupBox
        Left = 498
        Top = 186
        Width = 281
        Height = 76
        Caption = #20889'EPC'#21495#65288#21482#25913#20889#22825#32447#33539#22260#20869#26576#19968#24352#26631#31614#65289
        TabOrder = 3
        object Label38: TLabel
          Left = 9
          Top = 43
          Width = 72
          Height = 26
          Caption = #35775#38382#23494#30721#65306#13#10'(8'#20010'16'#36827#21046#25968')'
        end
        object Label39: TLabel
          Left = 8
          Top = 16
          Width = 48
          Height = 26
          Caption = #20889'EPC'#21495':'#13#10'(1-15'#23383')'
        end
        object Edit_AccessCode3: TEdit
          Left = 94
          Top = 48
          Width = 78
          Height = 21
          MaxLength = 8
          TabOrder = 2
          Text = '00000000'
          OnKeyPress = s
        end
        object Button_WriteEPC_G2: TButton
          Left = 192
          Top = 43
          Width = 81
          Height = 25
          Action = Action_WriteEPC_G2
          TabOrder = 1
        end
        object Edit_WriteEPC: TEdit
          Left = 61
          Top = 16
          Width = 212
          Height = 21
          MaxLength = 60
          TabOrder = 0
          Text = '0000'
          OnKeyPress = s
        end
      end
      object GroupBox29: TGroupBox
        Left = 8
        Top = 189
        Width = 481
        Height = 48
        Caption = 'EPC'#25513#27169#20351#33021
        TabOrder = 9
        object Label42: TLabel
          Left = 89
          Top = 23
          Width = 108
          Height = 13
          Caption = #25513#27169#36215#22987#23383#33410#22320#22336#65306
        end
        object Label43: TLabel
          Left = 304
          Top = 23
          Width = 72
          Height = 13
          Caption = #25513#27169#23383#33410#25968#65306
        end
        object CheckBox1: TCheckBox
          Left = 7
          Top = 21
          Width = 49
          Height = 17
          Caption = #20351#33021
          TabOrder = 0
        end
        object Edit2: TEdit
          Left = 193
          Top = 16
          Width = 81
          Height = 21
          MaxLength = 2
          TabOrder = 1
          Text = '00'
          OnKeyPress = s
        end
        object Edit3: TEdit
          Left = 392
          Top = 15
          Width = 81
          Height = 21
          MaxLength = 2
          TabOrder = 2
          Text = '00'
          OnKeyPress = s
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
        Caption = #21345#29255#26174#31034
        TabOrder = 0
        object ListView_ID_6B: TListView
          Left = 8
          Top = 16
          Width = 750
          Height = 282
          Columns = <
            item
              Caption = #24207#21495
            end
            item
              Caption = #21345#21495
              Width = 500
            end
            item
              Caption = #27425#25968
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
        Caption = #35835#20889#25968#25454'/'#23383#33410#22359#27704#20037#20889#20445#25252
        TabOrder = 2
        object Label29: TLabel
          Left = 9
          Top = 90
          Width = 173
          Height = 13
          Caption = #20889#25968#25454#65306'(1-32'#23383#33410'/16'#36827#21046')           '
        end
        object Label30: TLabel
          Left = 9
          Top = 49
          Width = 101
          Height = 26
          Caption = #36215#22987'/'#20889#20445#25252#22320#22336#65306#13#10'(00-E9)(16'#36827#21046#25968')   '
        end
        object Label31: TLabel
          Left = 205
          Top = 49
          Width = 127
          Height = 26
          Caption = #25968#25454#38271#24230#65306#13#10'(1-32/'#23383#33410'/10'#36827#21046#25968')      '
        end
        object SpeedButton_Read_6B: TSpeedButton
          Left = 8
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = #35835#25968#25454
          OnClick = SpeedButton_ReadWrite_6BClick
        end
        object SpeedButton_Write_6B: TSpeedButton
          Left = 73
          Top = 117
          Width = 49
          Height = 25
          AllowAllUp = True
          GroupIndex = 5
          Caption = #20889#25968#25454
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
          TabOrder = 2
        end
        object Edit_WriteData_6B: TEdit
          Left = 160
          Top = 85
          Width = 269
          Height = 21
          TabOrder = 5
          Text = '0000'
          OnKeyPress = s
        end
        object Edit_StartAddress_6B: TEdit
          Left = 108
          Top = 54
          Width = 88
          Height = 21
          MaxLength = 2
          TabOrder = 3
          Text = '00'
          OnKeyPress = s
        end
        object Edit_Len_6B: TEdit
          Left = 320
          Top = 54
          Width = 109
          Height = 21
          MaxLength = 2
          TabOrder = 4
          Text = '12'
          OnKeyPress = s
        end
        object Button11: TButton
          Left = 249
          Top = 5
          Width = 56
          Height = 25
          Action = Action_ReadData_6B
          TabOrder = 0
          Visible = False
        end
        object Memo_DataShow_6B: TMemo
          Left = 8
          Top = 152
          Width = 420
          Height = 143
          ScrollBars = ssVertical
          TabOrder = 9
        end
        object Button12: TButton
          Left = 318
          Top = 5
          Width = 54
          Height = 25
          Action = Action_WriteData_6B
          TabOrder = 1
          Visible = False
        end
        object Button14: TButton
          Left = 138
          Top = 117
          Width = 74
          Height = 25
          Action = Action_LockByte_6B
          Caption = #27704#20037#20889#20445#25252
          TabOrder = 6
        end
        object Button15: TButton
          Left = 228
          Top = 117
          Width = 125
          Height = 25
          Action = Action_CheckLock_6B
          Caption = #26816#27979#23383#33410#22359#27704#20037#20889#20445#25252
          TabOrder = 7
        end
        object Button22: TButton
          Left = 368
          Top = 117
          Width = 60
          Height = 25
          Caption = #28165#38500#26174#31034
          TabOrder = 8
          OnClick = Button22Click
        end
      end
      object GroupBox14: TGroupBox
        Left = 8
        Top = 452
        Width = 321
        Height = 168
        Caption = #26597#35810#26465#20214
        TabOrder = 3
        object Label34: TLabel
          Left = 8
          Top = 132
          Width = 150
          Height = 13
          Caption = #26465#20214'(<=8'#20010'16'#36827#21046#25968')'#65306'          '
        end
        object Label28: TLabel
          Left = 8
          Top = 92
          Width = 156
          Height = 13
          Caption = #26631#31614#25968#25454#36215#22987#22320#22336'(0-233)'#65306'     '
        end
        object Edit_Query_StartAddress_6B: TEdit
          Left = 160
          Top = 87
          Width = 97
          Height = 21
          MaxLength = 3
          TabOrder = 4
          Text = '0'
          OnKeyPress = s
        end
        object Edit_ConditionContent_6B: TEdit
          Left = 160
          Top = 124
          Width = 97
          Height = 21
          MaxLength = 16
          TabOrder = 5
          Text = '00'
          OnKeyPress = s
        end
        object Less_6B: TRadioButton
          Left = 8
          Top = 56
          Width = 81
          Height = 17
          Caption = #23567#20110#26465#20214
          TabOrder = 2
        end
        object Different_6B: TRadioButton
          Left = 160
          Top = 24
          Width = 113
          Height = 17
          Caption = #19982#26465#20214#19981#21516
          TabOrder = 1
        end
        object Same_6B: TRadioButton
          Left = 8
          Top = 24
          Width = 113
          Height = 17
          Caption = #19982#26465#20214#30456#21516
          TabOrder = 0
        end
        object Greater_6B: TRadioButton
          Left = 160
          Top = 56
          Width = 81
          Height = 17
          Caption = #22823#20110#26465#20214
          TabOrder = 3
        end
      end
      object GroupBox19: TGroupBox
        Left = 8
        Top = 316
        Width = 321
        Height = 132
        Caption = #23547#21345
        TabOrder = 1
        object SpeedButton_Query_6B: TSpeedButton
          Left = 221
          Top = 79
          Width = 89
          Height = 26
          AllowAllUp = True
          GroupIndex = 1
          Caption = #21333#24352#26597#35810
          OnClick = Action_Query_6BExecute
        end
        object Label27: TLabel
          Left = 8
          Top = 30
          Width = 84
          Height = 13
          Caption = #35835#21345#38388#38548#26102#38388#65306
        end
        object ComboBox_IntervalTime_6B: TComboBox
          Left = 104
          Top = 25
          Width = 207
          Height = 21
          ItemHeight = 13
          TabOrder = 0
          OnChange = ComboBox_IntervalTime_6BChange
        end
        object Byone_6B: TRadioButton
          Left = 8
          Top = 70
          Width = 73
          Height = 17
          Caption = #21333#24352#26597#35810
          TabOrder = 1
        end
        object Bycondition_6B: TRadioButton
          Left = 8
          Top = 98
          Width = 81
          Height = 17
          Caption = #26377#26465#20214#26597#35810
          TabOrder = 2
        end
      end
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 652
    Width = 794
    Height = 19
    AutoHint = True
    Panels = <
      item
        Width = 600
      end
      item
        Text = #20018#21475
        Width = 56
      end
      item
        Text = #21378#21830#21517#31216
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
      Caption = #25171#24320#31471#21475
      OnExecute = Action_OpenCOMExecute
    end
    object Action_OpenRf: TAction
      Tag = 1
      Category = #20018#21475#25171#24320#21363#21487#25191#34892'(TAG=1)'
      Caption = #25171#24320#23556#39057
    end
    object Action_CloseCOM: TAction
      Category = #36890#35759
      Caption = #20851#38381#31471#21475
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
    Left = 35
    Top = 466
  end
  object Timer_6B_ReadWrite: TTimer
    Enabled = False
    Interval = 200
    OnTimer = Timer_6B_ReadWriteTimer
    Left = 771
    Top = 290
  end
end
