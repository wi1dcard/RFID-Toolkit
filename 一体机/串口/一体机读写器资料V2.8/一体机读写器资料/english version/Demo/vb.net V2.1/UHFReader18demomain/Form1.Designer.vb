<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.TStatusPanel = New System.Windows.Forms.StatusBarPanel
        Me.Port = New System.Windows.Forms.StatusBarPanel
        Me.Manufacturername = New System.Windows.Forms.StatusBarPanel
        Me.Timer_Test_ = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_G2_Read = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_G2_Alarm = New System.Windows.Forms.Timer(Me.components)
        Me.timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_Test_6B = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_6B_Read = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_6B_Write = New System.Windows.Forms.Timer(Me.components)
        Me.tabControl1 = New System.Windows.Forms.TabControl
        Me.TabSheet_CMD = New System.Windows.Forms.TabPage
        Me.button11 = New System.Windows.Forms.Button
        Me.button10 = New System.Windows.Forms.Button
        Me.listBox3 = New System.Windows.Forms.ListBox
        Me.groupBox23 = New System.Windows.Forms.GroupBox
        Me.groupBox26 = New System.Windows.Forms.GroupBox
        Me.button_gettigtime = New System.Windows.Forms.Button
        Me.button_settigtime = New System.Windows.Forms.Button
        Me.comboBox_tigtime = New System.Windows.Forms.ComboBox
        Me.label53 = New System.Windows.Forms.Label
        Me.button_OffsetTime = New System.Windows.Forms.Button
        Me.comboBox_OffsetTime = New System.Windows.Forms.ComboBox
        Me.label48 = New System.Windows.Forms.Label
        Me.Button12 = New System.Windows.Forms.Button
        Me.ComboBox7 = New System.Windows.Forms.ComboBox
        Me.Label46 = New System.Windows.Forms.Label
        Me.ComboBox6 = New System.Windows.Forms.ComboBox
        Me.Label45 = New System.Windows.Forms.Label
        Me.GroupBox32 = New System.Windows.Forms.GroupBox
        Me.RadioButton16 = New System.Windows.Forms.RadioButton
        Me.RadioButton17 = New System.Windows.Forms.RadioButton
        Me.button9 = New System.Windows.Forms.Button
        Me.textBox3 = New System.Windows.Forms.TextBox
        Me.button8 = New System.Windows.Forms.Button
        Me.comboBox5 = New System.Windows.Forms.ComboBox
        Me.label42 = New System.Windows.Forms.Label
        Me.label41 = New System.Windows.Forms.Label
        Me.comboBox4 = New System.Windows.Forms.ComboBox
        Me.label40 = New System.Windows.Forms.Label
        Me.groupBox29 = New System.Windows.Forms.GroupBox
        Me.radioButton15 = New System.Windows.Forms.RadioButton
        Me.radioButton14 = New System.Windows.Forms.RadioButton
        Me.groupBox28 = New System.Windows.Forms.GroupBox
        Me.RadioButton19 = New System.Windows.Forms.RadioButton
        Me.RadioButton18 = New System.Windows.Forms.RadioButton
        Me.radioButton13 = New System.Windows.Forms.RadioButton
        Me.radioButton12 = New System.Windows.Forms.RadioButton
        Me.radioButton11 = New System.Windows.Forms.RadioButton
        Me.radioButton10 = New System.Windows.Forms.RadioButton
        Me.radioButton9 = New System.Windows.Forms.RadioButton
        Me.groupBox27 = New System.Windows.Forms.GroupBox
        Me.radioButton20 = New System.Windows.Forms.RadioButton
        Me.radioButton8 = New System.Windows.Forms.RadioButton
        Me.radioButton7 = New System.Windows.Forms.RadioButton
        Me.radioButton6 = New System.Windows.Forms.RadioButton
        Me.radioButton5 = New System.Windows.Forms.RadioButton
        Me.groupBox24 = New System.Windows.Forms.GroupBox
        Me.button6 = New System.Windows.Forms.Button
        Me.comboBox3 = New System.Windows.Forms.ComboBox
        Me.label39 = New System.Windows.Forms.Label
        Me.comboBox2 = New System.Windows.Forms.ComboBox
        Me.comboBox1 = New System.Windows.Forms.ComboBox
        Me.label38 = New System.Windows.Forms.Label
        Me.label37 = New System.Windows.Forms.Label
        Me.groupBox25 = New System.Windows.Forms.GroupBox
        Me.radioButton4 = New System.Windows.Forms.RadioButton
        Me.radioButton3 = New System.Windows.Forms.RadioButton
        Me.radioButton2 = New System.Windows.Forms.RadioButton
        Me.radioButton1 = New System.Windows.Forms.RadioButton
        Me.groupBox3 = New System.Windows.Forms.GroupBox
        Me.groupBox30 = New System.Windows.Forms.GroupBox
        Me.radioButton_band5 = New System.Windows.Forms.RadioButton
        Me.radioButton_band4 = New System.Windows.Forms.RadioButton
        Me.radioButton_band3 = New System.Windows.Forms.RadioButton
        Me.radioButton_band2 = New System.Windows.Forms.RadioButton
        Me.radioButton_band1 = New System.Windows.Forms.RadioButton
        Me.progressBar1 = New System.Windows.Forms.ProgressBar
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.ComboBox_scantime = New System.Windows.Forms.ComboBox
        Me.ComboBox_baud = New System.Windows.Forms.ComboBox
        Me.CheckBox_SameFre = New System.Windows.Forms.CheckBox
        Me.label17 = New System.Windows.Forms.Label
        Me.label16 = New System.Windows.Forms.Label
        Me.ComboBox_dmaxfre = New System.Windows.Forms.ComboBox
        Me.ComboBox_dminfre = New System.Windows.Forms.ComboBox
        Me.ComboBox_PowerDbm = New System.Windows.Forms.ComboBox
        Me.Edit_NewComAdr = New System.Windows.Forms.TextBox
        Me.label15 = New System.Windows.Forms.Label
        Me.label14 = New System.Windows.Forms.Label
        Me.label13 = New System.Windows.Forms.Label
        Me.label12 = New System.Windows.Forms.Label
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.Edit_scantime = New System.Windows.Forms.TextBox
        Me.EPCC1G2 = New System.Windows.Forms.CheckBox
        Me.ISO180006B = New System.Windows.Forms.CheckBox
        Me.label11 = New System.Windows.Forms.Label
        Me.label10 = New System.Windows.Forms.Label
        Me.Edit_dmaxfre = New System.Windows.Forms.TextBox
        Me.Edit_powerdBm = New System.Windows.Forms.TextBox
        Me.Edit_Version = New System.Windows.Forms.TextBox
        Me.label9 = New System.Windows.Forms.Label
        Me.label8 = New System.Windows.Forms.Label
        Me.label7 = New System.Windows.Forms.Label
        Me.Edit_dminfre = New System.Windows.Forms.TextBox
        Me.Edit_ComAdr = New System.Windows.Forms.TextBox
        Me.Edit_Type = New System.Windows.Forms.TextBox
        Me.label6 = New System.Windows.Forms.Label
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ComboBox_baud2 = New System.Windows.Forms.ComboBox
        Me.label47 = New System.Windows.Forms.Label
        Me.ComboBox_AlreadyOpenCOM = New System.Windows.Forms.ComboBox
        Me.label3 = New System.Windows.Forms.Label
        Me.ClosePort = New System.Windows.Forms.Button
        Me.OpenPort = New System.Windows.Forms.Button
        Me.Edit_CmdComAddr = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBox_COM = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabSheet_EPCC1G2 = New System.Windows.Forms.TabPage
        Me.groupBox31 = New System.Windows.Forms.GroupBox
        Me.maskLen_textBox = New System.Windows.Forms.TextBox
        Me.label44 = New System.Windows.Forms.Label
        Me.maskadr_textbox = New System.Windows.Forms.TextBox
        Me.label43 = New System.Windows.Forms.Label
        Me.checkBox1 = New System.Windows.Forms.CheckBox
        Me.groupBox18 = New System.Windows.Forms.GroupBox
        Me.Button_LockUserBlock_G2 = New System.Windows.Forms.Button
        Me.Edit_AccessCode6 = New System.Windows.Forms.TextBox
        Me.ComboBox_BlockNum = New System.Windows.Forms.ComboBox
        Me.label30 = New System.Windows.Forms.Label
        Me.label29 = New System.Windows.Forms.Label
        Me.ComboBox_EPC6 = New System.Windows.Forms.ComboBox
        Me.groupBox16 = New System.Windows.Forms.GroupBox
        Me.Label_Alarm = New System.Windows.Forms.Label
        Me.button4 = New System.Windows.Forms.Button
        Me.Button_SetEASAlarm_G2 = New System.Windows.Forms.Button
        Me.groupBox17 = New System.Windows.Forms.GroupBox
        Me.NoAlarm_G2 = New System.Windows.Forms.RadioButton
        Me.Alarm_G2 = New System.Windows.Forms.RadioButton
        Me.Edit_AccessCode5 = New System.Windows.Forms.TextBox
        Me.label28 = New System.Windows.Forms.Label
        Me.ComboBox_EPC5 = New System.Windows.Forms.ComboBox
        Me.groupBox15 = New System.Windows.Forms.GroupBox
        Me.Button_CheckReadProtected_G2 = New System.Windows.Forms.Button
        Me.Button_RemoveReadProtect_G2 = New System.Windows.Forms.Button
        Me.Button_SetMultiReadProtect_G2 = New System.Windows.Forms.Button
        Me.Button_SetReadProtect_G2 = New System.Windows.Forms.Button
        Me.Edit_AccessCode4 = New System.Windows.Forms.TextBox
        Me.label27 = New System.Windows.Forms.Label
        Me.ComboBox_EPC4 = New System.Windows.Forms.ComboBox
        Me.groupBox14 = New System.Windows.Forms.GroupBox
        Me.Button_WriteEPC_G2 = New System.Windows.Forms.Button
        Me.Edit_AccessCode3 = New System.Windows.Forms.TextBox
        Me.label26 = New System.Windows.Forms.Label
        Me.Edit_WriteEPC = New System.Windows.Forms.TextBox
        Me.label25 = New System.Windows.Forms.Label
        Me.groupBox13 = New System.Windows.Forms.GroupBox
        Me.Button_DestroyCard = New System.Windows.Forms.Button
        Me.Edit_DestroyCode = New System.Windows.Forms.TextBox
        Me.label24 = New System.Windows.Forms.Label
        Me.ComboBox_EPC3 = New System.Windows.Forms.ComboBox
        Me.groupBox12 = New System.Windows.Forms.GroupBox
        Me.CheckBox_TID = New System.Windows.Forms.CheckBox
        Me.groupBox33 = New System.Windows.Forms.GroupBox
        Me.textBox5 = New System.Windows.Forms.TextBox
        Me.label55 = New System.Windows.Forms.Label
        Me.textBox4 = New System.Windows.Forms.TextBox
        Me.label54 = New System.Windows.Forms.Label
        Me.button2 = New System.Windows.Forms.Button
        Me.ComboBox_IntervalTime = New System.Windows.Forms.ComboBox
        Me.label23 = New System.Windows.Forms.Label
        Me.groupBox7 = New System.Windows.Forms.GroupBox
        Me.Button_SetProtectState = New System.Windows.Forms.Button
        Me.textBox2 = New System.Windows.Forms.TextBox
        Me.label22 = New System.Windows.Forms.Label
        Me.groupBox11 = New System.Windows.Forms.GroupBox
        Me.AlwaysNot2 = New System.Windows.Forms.RadioButton
        Me.Always2 = New System.Windows.Forms.RadioButton
        Me.Proect2 = New System.Windows.Forms.RadioButton
        Me.NoProect2 = New System.Windows.Forms.RadioButton
        Me.groupBox10 = New System.Windows.Forms.GroupBox
        Me.P_User = New System.Windows.Forms.RadioButton
        Me.P_TID = New System.Windows.Forms.RadioButton
        Me.P_EPC = New System.Windows.Forms.RadioButton
        Me.P_Reserve = New System.Windows.Forms.RadioButton
        Me.groupBox8 = New System.Windows.Forms.GroupBox
        Me.AlwaysNot = New System.Windows.Forms.RadioButton
        Me.Always = New System.Windows.Forms.RadioButton
        Me.Proect = New System.Windows.Forms.RadioButton
        Me.NoProect = New System.Windows.Forms.RadioButton
        Me.groupBox9 = New System.Windows.Forms.GroupBox
        Me.AccessCode = New System.Windows.Forms.RadioButton
        Me.DestroyCode = New System.Windows.Forms.RadioButton
        Me.ComboBox_EPC1 = New System.Windows.Forms.ComboBox
        Me.groupBox5 = New System.Windows.Forms.GroupBox
        Me.textBox_pc = New System.Windows.Forms.TextBox
        Me.checkBox_pc = New System.Windows.Forms.CheckBox
        Me.Button_BlockWrite = New System.Windows.Forms.Button
        Me.ComboBox_EPC2 = New System.Windows.Forms.ComboBox
        Me.button7 = New System.Windows.Forms.Button
        Me.Button_BlockErase = New System.Windows.Forms.Button
        Me.Button_DataWrite = New System.Windows.Forms.Button
        Me.SpeedButton_Read_G2 = New System.Windows.Forms.Button
        Me.Edit_WriteData = New System.Windows.Forms.TextBox
        Me.Edit_AccessCode2 = New System.Windows.Forms.TextBox
        Me.textBox1 = New System.Windows.Forms.TextBox
        Me.Edit_WordPtr = New System.Windows.Forms.TextBox
        Me.listBox1 = New System.Windows.Forms.ListBox
        Me.label21 = New System.Windows.Forms.Label
        Me.label20 = New System.Windows.Forms.Label
        Me.label19 = New System.Windows.Forms.Label
        Me.label18 = New System.Windows.Forms.Label
        Me.groupBox6 = New System.Windows.Forms.GroupBox
        Me.C_User = New System.Windows.Forms.RadioButton
        Me.C_TID = New System.Windows.Forms.RadioButton
        Me.C_EPC = New System.Windows.Forms.RadioButton
        Me.C_Reserve = New System.Windows.Forms.RadioButton
        Me.groupBox4 = New System.Windows.Forms.GroupBox
        Me.ListView1_EPC = New System.Windows.Forms.ListView
        Me.listViewCol_Number = New System.Windows.Forms.ColumnHeader
        Me.listViewCol_ID = New System.Windows.Forms.ColumnHeader
        Me.listViewCol_Length = New System.Windows.Forms.ColumnHeader
        Me.listViewCol_Times = New System.Windows.Forms.ColumnHeader
        Me.TabSheet_6B = New System.Windows.Forms.TabPage
        Me.groupBox22 = New System.Windows.Forms.GroupBox
        Me.Edit_WriteData_6B = New System.Windows.Forms.TextBox
        Me.label36 = New System.Windows.Forms.Label
        Me.listBox2 = New System.Windows.Forms.ListBox
        Me.Button22 = New System.Windows.Forms.Button
        Me.Button15 = New System.Windows.Forms.Button
        Me.Button14 = New System.Windows.Forms.Button
        Me.SpeedButton_Write_6B = New System.Windows.Forms.Button
        Me.SpeedButton_Read_6B = New System.Windows.Forms.Button
        Me.Edit_Len_6B = New System.Windows.Forms.TextBox
        Me.label35 = New System.Windows.Forms.Label
        Me.Edit_StartAddress_6B = New System.Windows.Forms.TextBox
        Me.label34 = New System.Windows.Forms.Label
        Me.ComboBox_ID1_6B = New System.Windows.Forms.ComboBox
        Me.groupBox21 = New System.Windows.Forms.GroupBox
        Me.Edit_ConditionContent_6B = New System.Windows.Forms.TextBox
        Me.Edit_Query_StartAddress_6B = New System.Windows.Forms.TextBox
        Me.label33 = New System.Windows.Forms.Label
        Me.label32 = New System.Windows.Forms.Label
        Me.Greater_6B = New System.Windows.Forms.RadioButton
        Me.Less_6B = New System.Windows.Forms.RadioButton
        Me.Different_6B = New System.Windows.Forms.RadioButton
        Me.Same_6B = New System.Windows.Forms.RadioButton
        Me.groupBox20 = New System.Windows.Forms.GroupBox
        Me.SpeedButton_Query_6B = New System.Windows.Forms.Button
        Me.Bycondition_6B = New System.Windows.Forms.RadioButton
        Me.Byone_6B = New System.Windows.Forms.RadioButton
        Me.ComboBox_IntervalTime_6B = New System.Windows.Forms.ComboBox
        Me.label31 = New System.Windows.Forms.Label
        Me.groupBox19 = New System.Windows.Forms.GroupBox
        Me.ListView_ID_6B = New System.Windows.Forms.ListView
        Me.columnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.columnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.columnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.button19 = New System.Windows.Forms.Button
        Me.button18 = New System.Windows.Forms.Button
        Me.comboBox8 = New System.Windows.Forms.ComboBox
        Me.label49 = New System.Windows.Forms.Label
        Me.button17 = New System.Windows.Forms.Button
        Me.button16 = New System.Windows.Forms.Button
        Me.button13 = New System.Windows.Forms.Button
        Me.listBox4 = New System.Windows.Forms.ListBox
        Me.label52 = New System.Windows.Forms.Label
        Me.label51 = New System.Windows.Forms.Label
        Me.label50 = New System.Windows.Forms.Label
        CType(Me.TStatusPanel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Port, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Manufacturername, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl1.SuspendLayout()
        Me.TabSheet_CMD.SuspendLayout()
        Me.groupBox23.SuspendLayout()
        Me.groupBox26.SuspendLayout()
        Me.GroupBox32.SuspendLayout()
        Me.groupBox29.SuspendLayout()
        Me.groupBox28.SuspendLayout()
        Me.groupBox27.SuspendLayout()
        Me.groupBox24.SuspendLayout()
        Me.groupBox25.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.groupBox30.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabSheet_EPCC1G2.SuspendLayout()
        Me.groupBox31.SuspendLayout()
        Me.groupBox18.SuspendLayout()
        Me.groupBox16.SuspendLayout()
        Me.groupBox17.SuspendLayout()
        Me.groupBox15.SuspendLayout()
        Me.groupBox14.SuspendLayout()
        Me.groupBox13.SuspendLayout()
        Me.groupBox12.SuspendLayout()
        Me.groupBox33.SuspendLayout()
        Me.groupBox7.SuspendLayout()
        Me.groupBox11.SuspendLayout()
        Me.groupBox10.SuspendLayout()
        Me.groupBox8.SuspendLayout()
        Me.groupBox9.SuspendLayout()
        Me.groupBox5.SuspendLayout()
        Me.groupBox6.SuspendLayout()
        Me.groupBox4.SuspendLayout()
        Me.TabSheet_6B.SuspendLayout()
        Me.groupBox22.SuspendLayout()
        Me.groupBox21.SuspendLayout()
        Me.groupBox20.SuspendLayout()
        Me.groupBox19.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusBar1
        '
        Me.StatusBar1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.StatusBar1.Location = New System.Drawing.Point(0, 653)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.TStatusPanel, Me.Port, Me.Manufacturername})
        Me.StatusBar1.ShowPanels = True
        Me.StatusBar1.Size = New System.Drawing.Size(829, 20)
        Me.StatusBar1.SizingGrip = False
        Me.StatusBar1.TabIndex = 27
        Me.StatusBar1.Text = "StatusBar1"
        '
        'TStatusPanel
        '
        Me.TStatusPanel.Name = "TStatusPanel"
        Me.TStatusPanel.Width = 740
        '
        'Port
        '
        Me.Port.MinWidth = 66
        Me.Port.Name = "Port"
        Me.Port.Text = "Port:"
        '
        'Manufacturername
        '
        Me.Manufacturername.Name = "Manufacturername"
        Me.Manufacturername.Text = "statusManufacturer nameBarPanel1"
        '
        'Timer_Test_
        '
        '
        'Timer_G2_Read
        '
        Me.Timer_G2_Read.Interval = 200
        '
        'Timer_G2_Alarm
        '
        '
        'timer1
        '
        '
        'Timer_Test_6B
        '
        '
        'Timer_6B_Read
        '
        '
        'Timer_6B_Write
        '
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.TabSheet_CMD)
        Me.tabControl1.Controls.Add(Me.TabSheet_EPCC1G2)
        Me.tabControl1.Controls.Add(Me.TabSheet_6B)
        Me.tabControl1.Controls.Add(Me.TabPage1)
        Me.tabControl1.Location = New System.Drawing.Point(2, 3)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(825, 650)
        Me.tabControl1.TabIndex = 28
        '
        'TabSheet_CMD
        '
        Me.TabSheet_CMD.BackColor = System.Drawing.SystemColors.Control
        Me.TabSheet_CMD.Controls.Add(Me.button11)
        Me.TabSheet_CMD.Controls.Add(Me.button10)
        Me.TabSheet_CMD.Controls.Add(Me.listBox3)
        Me.TabSheet_CMD.Controls.Add(Me.groupBox23)
        Me.TabSheet_CMD.Controls.Add(Me.groupBox3)
        Me.TabSheet_CMD.Controls.Add(Me.groupBox2)
        Me.TabSheet_CMD.Controls.Add(Me.GroupBox1)
        Me.TabSheet_CMD.Location = New System.Drawing.Point(4, 21)
        Me.TabSheet_CMD.Name = "TabSheet_CMD"
        Me.TabSheet_CMD.Padding = New System.Windows.Forms.Padding(3)
        Me.TabSheet_CMD.Size = New System.Drawing.Size(817, 625)
        Me.TabSheet_CMD.TabIndex = 1
        Me.TabSheet_CMD.Text = "Reader Parameter"
        Me.TabSheet_CMD.UseVisualStyleBackColor = True
        '
        'button11
        '
        Me.button11.Location = New System.Drawing.Point(685, 575)
        Me.button11.Name = "button11"
        Me.button11.Size = New System.Drawing.Size(112, 23)
        Me.button11.TabIndex = 46
        Me.button11.Text = "Clear"
        Me.button11.UseVisualStyleBackColor = True
        '
        'button10
        '
        Me.button10.Location = New System.Drawing.Point(685, 534)
        Me.button10.Name = "button10"
        Me.button10.Size = New System.Drawing.Size(112, 23)
        Me.button10.TabIndex = 45
        Me.button10.Text = "Get "
        Me.button10.UseVisualStyleBackColor = True
        '
        'listBox3
        '
        Me.listBox3.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.listBox3.FormattingEnabled = True
        Me.listBox3.ItemHeight = 12
        Me.listBox3.Location = New System.Drawing.Point(147, 517)
        Me.listBox3.Name = "listBox3"
        Me.listBox3.ScrollAlwaysVisible = True
        Me.listBox3.Size = New System.Drawing.Size(512, 100)
        Me.listBox3.TabIndex = 44
        '
        'groupBox23
        '
        Me.groupBox23.Controls.Add(Me.groupBox26)
        Me.groupBox23.Controls.Add(Me.groupBox24)
        Me.groupBox23.Location = New System.Drawing.Point(147, 245)
        Me.groupBox23.Name = "groupBox23"
        Me.groupBox23.Size = New System.Drawing.Size(663, 268)
        Me.groupBox23.TabIndex = 43
        Me.groupBox23.TabStop = False
        Me.groupBox23.Text = "Work Mode Parameter"
        '
        'groupBox26
        '
        Me.groupBox26.Controls.Add(Me.button_gettigtime)
        Me.groupBox26.Controls.Add(Me.button_settigtime)
        Me.groupBox26.Controls.Add(Me.comboBox_tigtime)
        Me.groupBox26.Controls.Add(Me.label53)
        Me.groupBox26.Controls.Add(Me.button_OffsetTime)
        Me.groupBox26.Controls.Add(Me.comboBox_OffsetTime)
        Me.groupBox26.Controls.Add(Me.label48)
        Me.groupBox26.Controls.Add(Me.Button12)
        Me.groupBox26.Controls.Add(Me.ComboBox7)
        Me.groupBox26.Controls.Add(Me.Label46)
        Me.groupBox26.Controls.Add(Me.ComboBox6)
        Me.groupBox26.Controls.Add(Me.Label45)
        Me.groupBox26.Controls.Add(Me.GroupBox32)
        Me.groupBox26.Controls.Add(Me.button9)
        Me.groupBox26.Controls.Add(Me.textBox3)
        Me.groupBox26.Controls.Add(Me.button8)
        Me.groupBox26.Controls.Add(Me.comboBox5)
        Me.groupBox26.Controls.Add(Me.label42)
        Me.groupBox26.Controls.Add(Me.label41)
        Me.groupBox26.Controls.Add(Me.comboBox4)
        Me.groupBox26.Controls.Add(Me.label40)
        Me.groupBox26.Controls.Add(Me.groupBox29)
        Me.groupBox26.Controls.Add(Me.groupBox28)
        Me.groupBox26.Controls.Add(Me.groupBox27)
        Me.groupBox26.Controls.Add(Me.radioButton6)
        Me.groupBox26.Controls.Add(Me.radioButton5)
        Me.groupBox26.Location = New System.Drawing.Point(8, 91)
        Me.groupBox26.Name = "groupBox26"
        Me.groupBox26.Size = New System.Drawing.Size(649, 171)
        Me.groupBox26.TabIndex = 1
        Me.groupBox26.TabStop = False
        Me.groupBox26.Text = "Set Work Mode"
        '
        'button_gettigtime
        '
        Me.button_gettigtime.Enabled = False
        Me.button_gettigtime.Location = New System.Drawing.Point(260, 139)
        Me.button_gettigtime.Name = "button_gettigtime"
        Me.button_gettigtime.Size = New System.Drawing.Size(106, 23)
        Me.button_gettigtime.TabIndex = 73
        Me.button_gettigtime.Text = "Get TiggerTime"
        Me.button_gettigtime.UseVisualStyleBackColor = True
        '
        'button_settigtime
        '
        Me.button_settigtime.Enabled = False
        Me.button_settigtime.Location = New System.Drawing.Point(151, 139)
        Me.button_settigtime.Name = "button_settigtime"
        Me.button_settigtime.Size = New System.Drawing.Size(106, 23)
        Me.button_settigtime.TabIndex = 72
        Me.button_settigtime.Text = "Set TiggerTime"
        Me.button_settigtime.UseVisualStyleBackColor = True
        '
        'comboBox_tigtime
        '
        Me.comboBox_tigtime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox_tigtime.FormattingEnabled = True
        Me.comboBox_tigtime.Location = New System.Drawing.Point(95, 141)
        Me.comboBox_tigtime.Name = "comboBox_tigtime"
        Me.comboBox_tigtime.Size = New System.Drawing.Size(52, 20)
        Me.comboBox_tigtime.TabIndex = 71
        '
        'label53
        '
        Me.label53.AutoSize = True
        Me.label53.Location = New System.Drawing.Point(10, 147)
        Me.label53.Name = "label53"
        Me.label53.Size = New System.Drawing.Size(77, 12)
        Me.label53.TabIndex = 70
        Me.label53.Text = "Tigger Time:"
        '
        'button_OffsetTime
        '
        Me.button_OffsetTime.Enabled = False
        Me.button_OffsetTime.Location = New System.Drawing.Point(398, 111)
        Me.button_OffsetTime.Name = "button_OffsetTime"
        Me.button_OffsetTime.Size = New System.Drawing.Size(91, 23)
        Me.button_OffsetTime.TabIndex = 61
        Me.button_OffsetTime.Text = "SetOffsetTime"
        Me.button_OffsetTime.UseVisualStyleBackColor = True
        '
        'comboBox_OffsetTime
        '
        Me.comboBox_OffsetTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox_OffsetTime.FormattingEnabled = True
        Me.comboBox_OffsetTime.Location = New System.Drawing.Point(327, 113)
        Me.comboBox_OffsetTime.Name = "comboBox_OffsetTime"
        Me.comboBox_OffsetTime.Size = New System.Drawing.Size(68, 20)
        Me.comboBox_OffsetTime.TabIndex = 60
        '
        'label48
        '
        Me.label48.AutoSize = True
        Me.label48.Location = New System.Drawing.Point(254, 118)
        Me.label48.Name = "label48"
        Me.label48.Size = New System.Drawing.Size(71, 12)
        Me.label48.TabIndex = 59
        Me.label48.Text = "OffsetTime:"
        '
        'Button12
        '
        Me.Button12.Enabled = False
        Me.Button12.Location = New System.Drawing.Point(151, 111)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(103, 23)
        Me.Button12.TabIndex = 54
        Me.Button12.Text = "Get SetAccuracy"
        Me.Button12.UseVisualStyleBackColor = True
        '
        'ComboBox7
        '
        Me.ComboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox7.FormattingEnabled = True
        Me.ComboBox7.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8"})
        Me.ComboBox7.Location = New System.Drawing.Point(94, 113)
        Me.ComboBox7.Name = "ComboBox7"
        Me.ComboBox7.Size = New System.Drawing.Size(54, 20)
        Me.ComboBox7.TabIndex = 53
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(11, 119)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(83, 12)
        Me.Label46.TabIndex = 52
        Me.Label46.Text = "EAS Accuracy:"
        '
        'ComboBox6
        '
        Me.ComboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox6.FormattingEnabled = True
        Me.ComboBox6.Location = New System.Drawing.Point(583, 41)
        Me.ComboBox6.Name = "ComboBox6"
        Me.ComboBox6.Size = New System.Drawing.Size(59, 20)
        Me.ComboBox6.TabIndex = 51
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(427, 45)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(161, 12)
        Me.Label45.TabIndex = 50
        Me.Label45.Text = "Single Tag Filtering Time:"
        '
        'GroupBox32
        '
        Me.GroupBox32.Controls.Add(Me.RadioButton16)
        Me.GroupBox32.Controls.Add(Me.RadioButton17)
        Me.GroupBox32.Location = New System.Drawing.Point(176, 64)
        Me.GroupBox32.Name = "GroupBox32"
        Me.GroupBox32.Size = New System.Drawing.Size(119, 41)
        Me.GroupBox32.TabIndex = 49
        Me.GroupBox32.TabStop = False
        '
        'RadioButton16
        '
        Me.RadioButton16.AutoSize = True
        Me.RadioButton16.Location = New System.Drawing.Point(3, 10)
        Me.RadioButton16.Name = "RadioButton16"
        Me.RadioButton16.Size = New System.Drawing.Size(113, 16)
        Me.RadioButton16.TabIndex = 1
        Me.RadioButton16.TabStop = True
        Me.RadioButton16.Text = "First Word Addr"
        Me.RadioButton16.UseVisualStyleBackColor = True
        '
        'RadioButton17
        '
        Me.RadioButton17.AutoSize = True
        Me.RadioButton17.Location = New System.Drawing.Point(3, 23)
        Me.RadioButton17.Name = "RadioButton17"
        Me.RadioButton17.Size = New System.Drawing.Size(113, 16)
        Me.RadioButton17.TabIndex = 0
        Me.RadioButton17.TabStop = True
        Me.RadioButton17.Text = "First Byte Addr"
        Me.RadioButton17.UseVisualStyleBackColor = True
        '
        'button9
        '
        Me.button9.Location = New System.Drawing.Point(493, 110)
        Me.button9.Name = "button9"
        Me.button9.Size = New System.Drawing.Size(151, 23)
        Me.button9.TabIndex = 48
        Me.button9.Text = "Get Work Mode parameter"
        Me.button9.UseVisualStyleBackColor = True
        '
        'textBox3
        '
        Me.textBox3.Location = New System.Drawing.Point(558, 61)
        Me.textBox3.MaxLength = 2
        Me.textBox3.Name = "textBox3"
        Me.textBox3.Size = New System.Drawing.Size(33, 21)
        Me.textBox3.TabIndex = 47
        Me.textBox3.Text = "02"
        '
        'button8
        '
        Me.button8.Location = New System.Drawing.Point(595, 62)
        Me.button8.Name = "button8"
        Me.button8.Size = New System.Drawing.Size(46, 39)
        Me.button8.TabIndex = 11
        Me.button8.Text = "Set"
        Me.button8.UseVisualStyleBackColor = True
        '
        'comboBox5
        '
        Me.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox5.FormattingEnabled = True
        Me.comboBox5.Location = New System.Drawing.Point(546, 84)
        Me.comboBox5.Name = "comboBox5"
        Me.comboBox5.Size = New System.Drawing.Size(46, 20)
        Me.comboBox5.TabIndex = 9
        '
        'label42
        '
        Me.label42.AutoSize = True
        Me.label42.Location = New System.Drawing.Point(431, 92)
        Me.label42.Name = "label42"
        Me.label42.Size = New System.Drawing.Size(107, 12)
        Me.label42.TabIndex = 8
        Me.label42.Text = "Read Word Number:"
        '
        'label41
        '
        Me.label41.AutoSize = True
        Me.label41.Location = New System.Drawing.Point(430, 70)
        Me.label41.Name = "label41"
        Me.label41.Size = New System.Drawing.Size(131, 12)
        Me.label41.TabIndex = 7
        Me.label41.Text = "First Word Addr(Hex):"
        '
        'comboBox4
        '
        Me.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox4.FormattingEnabled = True
        Me.comboBox4.Items.AddRange(New Object() {"Answer Mode", "Active mode", "Trigger mode(Low)", "Trigger mode(High)"})
        Me.comboBox4.Location = New System.Drawing.Point(504, 15)
        Me.comboBox4.Name = "comboBox4"
        Me.comboBox4.Size = New System.Drawing.Size(138, 20)
        Me.comboBox4.TabIndex = 6
        '
        'label40
        '
        Me.label40.AutoSize = True
        Me.label40.Location = New System.Drawing.Point(433, 21)
        Me.label40.Name = "label40"
        Me.label40.Size = New System.Drawing.Size(65, 12)
        Me.label40.TabIndex = 5
        Me.label40.Text = "Work Mode:"
        '
        'groupBox29
        '
        Me.groupBox29.Controls.Add(Me.radioButton15)
        Me.groupBox29.Controls.Add(Me.radioButton14)
        Me.groupBox29.Location = New System.Drawing.Point(298, 64)
        Me.groupBox29.Name = "groupBox29"
        Me.groupBox29.Size = New System.Drawing.Size(127, 41)
        Me.groupBox29.TabIndex = 4
        Me.groupBox29.TabStop = False
        '
        'radioButton15
        '
        Me.radioButton15.AutoSize = True
        Me.radioButton15.Enabled = False
        Me.radioButton15.Location = New System.Drawing.Point(4, 23)
        Me.radioButton15.Name = "radioButton15"
        Me.radioButton15.Size = New System.Drawing.Size(119, 16)
        Me.radioButton15.TabIndex = 1
        Me.radioButton15.TabStop = True
        Me.radioButton15.Text = "DisEnable buzzer"
        Me.radioButton15.UseVisualStyleBackColor = True
        '
        'radioButton14
        '
        Me.radioButton14.AutoSize = True
        Me.radioButton14.Enabled = False
        Me.radioButton14.Location = New System.Drawing.Point(4, 8)
        Me.radioButton14.Name = "radioButton14"
        Me.radioButton14.Size = New System.Drawing.Size(113, 16)
        Me.radioButton14.TabIndex = 0
        Me.radioButton14.TabStop = True
        Me.radioButton14.Text = "Activate buzzer"
        Me.radioButton14.UseVisualStyleBackColor = True
        '
        'groupBox28
        '
        Me.groupBox28.Controls.Add(Me.RadioButton19)
        Me.groupBox28.Controls.Add(Me.RadioButton18)
        Me.groupBox28.Controls.Add(Me.radioButton13)
        Me.groupBox28.Controls.Add(Me.radioButton12)
        Me.groupBox28.Controls.Add(Me.radioButton11)
        Me.groupBox28.Controls.Add(Me.radioButton10)
        Me.groupBox28.Controls.Add(Me.radioButton9)
        Me.groupBox28.Location = New System.Drawing.Point(176, 8)
        Me.groupBox28.Name = "groupBox28"
        Me.groupBox28.Size = New System.Drawing.Size(249, 56)
        Me.groupBox28.TabIndex = 3
        Me.groupBox28.TabStop = False
        Me.groupBox28.Text = "Storage area or inquiry conducted Tags"
        '
        'RadioButton19
        '
        Me.RadioButton19.AutoSize = True
        Me.RadioButton19.Enabled = False
        Me.RadioButton19.Location = New System.Drawing.Point(173, 37)
        Me.RadioButton19.Name = "RadioButton19"
        Me.RadioButton19.Size = New System.Drawing.Size(41, 16)
        Me.RadioButton19.TabIndex = 6
        Me.RadioButton19.TabStop = True
        Me.RadioButton19.Text = "EAS"
        Me.RadioButton19.UseVisualStyleBackColor = True
        '
        'RadioButton18
        '
        Me.RadioButton18.AutoSize = True
        Me.RadioButton18.Location = New System.Drawing.Point(93, 37)
        Me.RadioButton18.Name = "RadioButton18"
        Me.RadioButton18.Size = New System.Drawing.Size(77, 16)
        Me.RadioButton18.TabIndex = 5
        Me.RadioButton18.TabStop = True
        Me.RadioButton18.Text = "One-Query"
        Me.RadioButton18.UseVisualStyleBackColor = True
        '
        'radioButton13
        '
        Me.radioButton13.AutoSize = True
        Me.radioButton13.Location = New System.Drawing.Point(7, 37)
        Me.radioButton13.Name = "radioButton13"
        Me.radioButton13.Size = New System.Drawing.Size(89, 16)
        Me.radioButton13.TabIndex = 4
        Me.radioButton13.TabStop = True
        Me.radioButton13.Text = "Multi-Query"
        Me.radioButton13.UseVisualStyleBackColor = True
        '
        'radioButton12
        '
        Me.radioButton12.AutoSize = True
        Me.radioButton12.Location = New System.Drawing.Point(172, 14)
        Me.radioButton12.Name = "radioButton12"
        Me.radioButton12.Size = New System.Drawing.Size(47, 16)
        Me.radioButton12.TabIndex = 3
        Me.radioButton12.TabStop = True
        Me.radioButton12.Text = "User"
        Me.radioButton12.UseVisualStyleBackColor = True
        '
        'radioButton11
        '
        Me.radioButton11.AutoSize = True
        Me.radioButton11.Location = New System.Drawing.Point(128, 15)
        Me.radioButton11.Name = "radioButton11"
        Me.radioButton11.Size = New System.Drawing.Size(41, 16)
        Me.radioButton11.TabIndex = 2
        Me.radioButton11.TabStop = True
        Me.radioButton11.Text = "TID"
        Me.radioButton11.UseVisualStyleBackColor = True
        '
        'radioButton10
        '
        Me.radioButton10.AutoSize = True
        Me.radioButton10.Location = New System.Drawing.Point(78, 15)
        Me.radioButton10.Name = "radioButton10"
        Me.radioButton10.Size = New System.Drawing.Size(41, 16)
        Me.radioButton10.TabIndex = 1
        Me.radioButton10.TabStop = True
        Me.radioButton10.Text = "EPC"
        Me.radioButton10.UseVisualStyleBackColor = True
        '
        'radioButton9
        '
        Me.radioButton9.AutoSize = True
        Me.radioButton9.Location = New System.Drawing.Point(7, 15)
        Me.radioButton9.Name = "radioButton9"
        Me.radioButton9.Size = New System.Drawing.Size(71, 16)
        Me.radioButton9.TabIndex = 0
        Me.radioButton9.TabStop = True
        Me.radioButton9.Text = "Password"
        Me.radioButton9.UseVisualStyleBackColor = True
        '
        'groupBox27
        '
        Me.groupBox27.Controls.Add(Me.radioButton20)
        Me.groupBox27.Controls.Add(Me.radioButton8)
        Me.groupBox27.Controls.Add(Me.radioButton7)
        Me.groupBox27.Location = New System.Drawing.Point(11, 35)
        Me.groupBox27.Name = "groupBox27"
        Me.groupBox27.Size = New System.Drawing.Size(159, 70)
        Me.groupBox27.TabIndex = 2
        Me.groupBox27.TabStop = False
        '
        'radioButton20
        '
        Me.radioButton20.AutoSize = True
        Me.radioButton20.Enabled = False
        Me.radioButton20.Location = New System.Drawing.Point(6, 48)
        Me.radioButton20.Name = "radioButton20"
        Me.radioButton20.Size = New System.Drawing.Size(113, 16)
        Me.radioButton20.TabIndex = 4
        Me.radioButton20.TabStop = True
        Me.radioButton20.Text = "SYRIS485 Output"
        Me.radioButton20.UseVisualStyleBackColor = True
        '
        'radioButton8
        '
        Me.radioButton8.AutoSize = True
        Me.radioButton8.Location = New System.Drawing.Point(6, 29)
        Me.radioButton8.Name = "radioButton8"
        Me.radioButton8.Size = New System.Drawing.Size(131, 16)
        Me.radioButton8.TabIndex = 1
        Me.radioButton8.TabStop = True
        Me.radioButton8.Text = "RS232/RS485 Output"
        Me.radioButton8.UseVisualStyleBackColor = True
        '
        'radioButton7
        '
        Me.radioButton7.AutoSize = True
        Me.radioButton7.Location = New System.Drawing.Point(6, 10)
        Me.radioButton7.Name = "radioButton7"
        Me.radioButton7.Size = New System.Drawing.Size(107, 16)
        Me.radioButton7.TabIndex = 0
        Me.radioButton7.TabStop = True
        Me.radioButton7.Text = "Wiegand Output"
        Me.radioButton7.UseVisualStyleBackColor = True
        '
        'radioButton6
        '
        Me.radioButton6.AutoSize = True
        Me.radioButton6.Location = New System.Drawing.Point(88, 19)
        Me.radioButton6.Name = "radioButton6"
        Me.radioButton6.Size = New System.Drawing.Size(89, 16)
        Me.radioButton6.TabIndex = 1
        Me.radioButton6.TabStop = True
        Me.radioButton6.Text = "ISO18000-6B"
        Me.radioButton6.UseVisualStyleBackColor = True
        '
        'radioButton5
        '
        Me.radioButton5.AutoSize = True
        Me.radioButton5.Location = New System.Drawing.Point(11, 19)
        Me.radioButton5.Name = "radioButton5"
        Me.radioButton5.Size = New System.Drawing.Size(71, 16)
        Me.radioButton5.TabIndex = 0
        Me.radioButton5.TabStop = True
        Me.radioButton5.Text = "EPCC1-G2"
        Me.radioButton5.UseVisualStyleBackColor = True
        '
        'groupBox24
        '
        Me.groupBox24.Controls.Add(Me.button6)
        Me.groupBox24.Controls.Add(Me.comboBox3)
        Me.groupBox24.Controls.Add(Me.label39)
        Me.groupBox24.Controls.Add(Me.comboBox2)
        Me.groupBox24.Controls.Add(Me.comboBox1)
        Me.groupBox24.Controls.Add(Me.label38)
        Me.groupBox24.Controls.Add(Me.label37)
        Me.groupBox24.Controls.Add(Me.groupBox25)
        Me.groupBox24.Controls.Add(Me.radioButton2)
        Me.groupBox24.Controls.Add(Me.radioButton1)
        Me.groupBox24.Location = New System.Drawing.Point(8, 12)
        Me.groupBox24.Name = "groupBox24"
        Me.groupBox24.Size = New System.Drawing.Size(649, 77)
        Me.groupBox24.TabIndex = 0
        Me.groupBox24.TabStop = False
        Me.groupBox24.Text = "Wiegand Parameter"
        '
        'button6
        '
        Me.button6.Location = New System.Drawing.Point(487, 48)
        Me.button6.Name = "button6"
        Me.button6.Size = New System.Drawing.Size(156, 23)
        Me.button6.TabIndex = 9
        Me.button6.Text = "SetWGParameter"
        Me.button6.UseVisualStyleBackColor = True
        '
        'comboBox3
        '
        Me.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox3.FormattingEnabled = True
        Me.comboBox3.Location = New System.Drawing.Point(567, 13)
        Me.comboBox3.Name = "comboBox3"
        Me.comboBox3.Size = New System.Drawing.Size(75, 20)
        Me.comboBox3.TabIndex = 8
        '
        'label39
        '
        Me.label39.AutoSize = True
        Me.label39.Location = New System.Drawing.Point(484, 16)
        Me.label39.Name = "label39"
        Me.label39.Size = New System.Drawing.Size(77, 12)
        Me.label39.TabIndex = 7
        Me.label39.Text = "Pulse width:"
        '
        'comboBox2
        '
        Me.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox2.FormattingEnabled = True
        Me.comboBox2.Location = New System.Drawing.Point(348, 52)
        Me.comboBox2.Name = "comboBox2"
        Me.comboBox2.Size = New System.Drawing.Size(79, 20)
        Me.comboBox2.TabIndex = 6
        '
        'comboBox1
        '
        Me.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox1.FormattingEnabled = True
        Me.comboBox1.Location = New System.Drawing.Point(348, 13)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(79, 20)
        Me.comboBox1.TabIndex = 5
        '
        'label38
        '
        Me.label38.AutoSize = True
        Me.label38.Location = New System.Drawing.Point(247, 55)
        Me.label38.Name = "label38"
        Me.label38.Size = New System.Drawing.Size(95, 12)
        Me.label38.TabIndex = 4
        Me.label38.Text = "Pulse interval:"
        '
        'label37
        '
        Me.label37.AutoSize = True
        Me.label37.Location = New System.Drawing.Point(211, 16)
        Me.label37.Name = "label37"
        Me.label37.Size = New System.Drawing.Size(131, 12)
        Me.label37.TabIndex = 3
        Me.label37.Text = "Data output interval:"
        '
        'groupBox25
        '
        Me.groupBox25.Controls.Add(Me.radioButton4)
        Me.groupBox25.Controls.Add(Me.radioButton3)
        Me.groupBox25.Location = New System.Drawing.Point(11, 27)
        Me.groupBox25.Name = "groupBox25"
        Me.groupBox25.Size = New System.Drawing.Size(176, 46)
        Me.groupBox25.TabIndex = 2
        Me.groupBox25.TabStop = False
        '
        'radioButton4
        '
        Me.radioButton4.AutoSize = True
        Me.radioButton4.Location = New System.Drawing.Point(6, 28)
        Me.radioButton4.Name = "radioButton4"
        Me.radioButton4.Size = New System.Drawing.Size(167, 16)
        Me.radioButton4.TabIndex = 1
        Me.radioButton4.TabStop = True
        Me.radioButton4.Text = "Wiegand output MSB first"
        Me.radioButton4.UseVisualStyleBackColor = True
        '
        'radioButton3
        '
        Me.radioButton3.AutoSize = True
        Me.radioButton3.Location = New System.Drawing.Point(6, 10)
        Me.radioButton3.Name = "radioButton3"
        Me.radioButton3.Size = New System.Drawing.Size(167, 16)
        Me.radioButton3.TabIndex = 0
        Me.radioButton3.TabStop = True
        Me.radioButton3.Text = "Wiegand output LSB first"
        Me.radioButton3.UseVisualStyleBackColor = True
        '
        'radioButton2
        '
        Me.radioButton2.AutoSize = True
        Me.radioButton2.Location = New System.Drawing.Point(105, 14)
        Me.radioButton2.Name = "radioButton2"
        Me.radioButton2.Size = New System.Drawing.Size(77, 16)
        Me.radioButton2.TabIndex = 1
        Me.radioButton2.TabStop = True
        Me.radioButton2.Text = "Wiegand34"
        Me.radioButton2.UseVisualStyleBackColor = True
        '
        'radioButton1
        '
        Me.radioButton1.AutoSize = True
        Me.radioButton1.Location = New System.Drawing.Point(11, 14)
        Me.radioButton1.Name = "radioButton1"
        Me.radioButton1.Size = New System.Drawing.Size(77, 16)
        Me.radioButton1.TabIndex = 0
        Me.radioButton1.TabStop = True
        Me.radioButton1.Text = "Wiegand26"
        Me.radioButton1.UseVisualStyleBackColor = True
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.groupBox30)
        Me.groupBox3.Controls.Add(Me.progressBar1)
        Me.groupBox3.Controls.Add(Me.Button1)
        Me.groupBox3.Controls.Add(Me.Button5)
        Me.groupBox3.Controls.Add(Me.ComboBox_scantime)
        Me.groupBox3.Controls.Add(Me.ComboBox_baud)
        Me.groupBox3.Controls.Add(Me.CheckBox_SameFre)
        Me.groupBox3.Controls.Add(Me.label17)
        Me.groupBox3.Controls.Add(Me.label16)
        Me.groupBox3.Controls.Add(Me.ComboBox_dmaxfre)
        Me.groupBox3.Controls.Add(Me.ComboBox_dminfre)
        Me.groupBox3.Controls.Add(Me.ComboBox_PowerDbm)
        Me.groupBox3.Controls.Add(Me.Edit_NewComAdr)
        Me.groupBox3.Controls.Add(Me.label15)
        Me.groupBox3.Controls.Add(Me.label14)
        Me.groupBox3.Controls.Add(Me.label13)
        Me.groupBox3.Controls.Add(Me.label12)
        Me.groupBox3.Location = New System.Drawing.Point(147, 110)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(663, 133)
        Me.groupBox3.TabIndex = 42
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Set Reader Parameter"
        '
        'groupBox30
        '
        Me.groupBox30.Controls.Add(Me.radioButton_band5)
        Me.groupBox30.Controls.Add(Me.radioButton_band4)
        Me.groupBox30.Controls.Add(Me.radioButton_band3)
        Me.groupBox30.Controls.Add(Me.radioButton_band2)
        Me.groupBox30.Controls.Add(Me.radioButton_band1)
        Me.groupBox30.Location = New System.Drawing.Point(475, 9)
        Me.groupBox30.Name = "groupBox30"
        Me.groupBox30.Size = New System.Drawing.Size(180, 93)
        Me.groupBox30.TabIndex = 44
        Me.groupBox30.TabStop = False
        Me.groupBox30.Text = "FreqBand Setting"
        '
        'radioButton_band5
        '
        Me.radioButton_band5.AutoSize = True
        Me.radioButton_band5.Location = New System.Drawing.Point(6, 72)
        Me.radioButton_band5.Name = "radioButton_band5"
        Me.radioButton_band5.Size = New System.Drawing.Size(65, 16)
        Me.radioButton_band5.TabIndex = 6
        Me.radioButton_band5.TabStop = True
        Me.radioButton_band5.Text = "EU band"
        Me.radioButton_band5.UseVisualStyleBackColor = True
        '
        'radioButton_band4
        '
        Me.radioButton_band4.AutoSize = True
        Me.radioButton_band4.Location = New System.Drawing.Point(6, 57)
        Me.radioButton_band4.Name = "radioButton_band4"
        Me.radioButton_band4.Size = New System.Drawing.Size(89, 16)
        Me.radioButton_band4.TabIndex = 3
        Me.radioButton_band4.TabStop = True
        Me.radioButton_band4.Text = "Korean band"
        Me.radioButton_band4.UseVisualStyleBackColor = True
        '
        'radioButton_band3
        '
        Me.radioButton_band3.AutoSize = True
        Me.radioButton_band3.Location = New System.Drawing.Point(6, 43)
        Me.radioButton_band3.Name = "radioButton_band3"
        Me.radioButton_band3.Size = New System.Drawing.Size(65, 16)
        Me.radioButton_band3.TabIndex = 2
        Me.radioButton_band3.TabStop = True
        Me.radioButton_band3.Text = "US band"
        Me.radioButton_band3.UseVisualStyleBackColor = True
        '
        'radioButton_band2
        '
        Me.radioButton_band2.AutoSize = True
        Me.radioButton_band2.Location = New System.Drawing.Point(6, 28)
        Me.radioButton_band2.Name = "radioButton_band2"
        Me.radioButton_band2.Size = New System.Drawing.Size(101, 16)
        Me.radioButton_band2.TabIndex = 1
        Me.radioButton_band2.TabStop = True
        Me.radioButton_band2.Text = "Chinese band2"
        Me.radioButton_band2.UseVisualStyleBackColor = True
        '
        'radioButton_band1
        '
        Me.radioButton_band1.AutoSize = True
        Me.radioButton_band1.Location = New System.Drawing.Point(6, 13)
        Me.radioButton_band1.Name = "radioButton_band1"
        Me.radioButton_band1.Size = New System.Drawing.Size(77, 16)
        Me.radioButton_band1.TabIndex = 0
        Me.radioButton_band1.TabStop = True
        Me.radioButton_band1.Text = "User band"
        Me.radioButton_band1.UseVisualStyleBackColor = True
        '
        'progressBar1
        '
        Me.progressBar1.Location = New System.Drawing.Point(106, 93)
        Me.progressBar1.Name = "progressBar1"
        Me.progressBar1.Size = New System.Drawing.Size(399, 23)
        Me.progressBar1.TabIndex = 43
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(538, 104)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(118, 23)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Default Parameter"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(404, 104)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(121, 23)
        Me.Button5.TabIndex = 13
        Me.Button5.Text = "Set Parameter"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ComboBox_scantime
        '
        Me.ComboBox_scantime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_scantime.FormattingEnabled = True
        Me.ComboBox_scantime.Location = New System.Drawing.Point(348, 43)
        Me.ComboBox_scantime.Name = "ComboBox_scantime"
        Me.ComboBox_scantime.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox_scantime.TabIndex = 12
        '
        'ComboBox_baud
        '
        Me.ComboBox_baud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_baud.FormattingEnabled = True
        Me.ComboBox_baud.Items.AddRange(New Object() {"9600bps", "19200bps", "38400bps", "57600bps", "115200bps"})
        Me.ComboBox_baud.Location = New System.Drawing.Point(348, 15)
        Me.ComboBox_baud.Name = "ComboBox_baud"
        Me.ComboBox_baud.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox_baud.TabIndex = 11
        '
        'CheckBox_SameFre
        '
        Me.CheckBox_SameFre.AutoSize = True
        Me.CheckBox_SameFre.Location = New System.Drawing.Point(215, 78)
        Me.CheckBox_SameFre.Name = "CheckBox_SameFre"
        Me.CheckBox_SameFre.Size = New System.Drawing.Size(162, 16)
        Me.CheckBox_SameFre.TabIndex = 10
        Me.CheckBox_SameFre.Text = "Single Frequency Point "
        Me.CheckBox_SameFre.UseVisualStyleBackColor = True
        '
        'label17
        '
        Me.label17.AutoSize = True
        Me.label17.Location = New System.Drawing.Point(213, 46)
        Me.label17.Name = "label17"
        Me.label17.Size = New System.Drawing.Size(137, 12)
        Me.label17.TabIndex = 9
        Me.label17.Text = "Max InventoryScanTime:"
        '
        'label16
        '
        Me.label16.AutoSize = True
        Me.label16.Location = New System.Drawing.Point(213, 22)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(65, 12)
        Me.label16.TabIndex = 8
        Me.label16.Text = "Baud Rate:"
        '
        'ComboBox_dmaxfre
        '
        Me.ComboBox_dmaxfre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_dmaxfre.FormattingEnabled = True
        Me.ComboBox_dmaxfre.Location = New System.Drawing.Point(95, 106)
        Me.ComboBox_dmaxfre.Name = "ComboBox_dmaxfre"
        Me.ComboBox_dmaxfre.Size = New System.Drawing.Size(100, 20)
        Me.ComboBox_dmaxfre.TabIndex = 7
        '
        'ComboBox_dminfre
        '
        Me.ComboBox_dminfre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_dminfre.FormattingEnabled = True
        Me.ComboBox_dminfre.Location = New System.Drawing.Point(95, 76)
        Me.ComboBox_dminfre.Name = "ComboBox_dminfre"
        Me.ComboBox_dminfre.Size = New System.Drawing.Size(100, 20)
        Me.ComboBox_dminfre.TabIndex = 6
        '
        'ComboBox_PowerDbm
        '
        Me.ComboBox_PowerDbm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_PowerDbm.FormattingEnabled = True
        Me.ComboBox_PowerDbm.Items.AddRange(New Object() {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30"})
        Me.ComboBox_PowerDbm.Location = New System.Drawing.Point(95, 43)
        Me.ComboBox_PowerDbm.Name = "ComboBox_PowerDbm"
        Me.ComboBox_PowerDbm.Size = New System.Drawing.Size(100, 20)
        Me.ComboBox_PowerDbm.TabIndex = 5
        '
        'Edit_NewComAdr
        '
        Me.Edit_NewComAdr.Location = New System.Drawing.Point(95, 15)
        Me.Edit_NewComAdr.MaxLength = 2
        Me.Edit_NewComAdr.Name = "Edit_NewComAdr"
        Me.Edit_NewComAdr.Size = New System.Drawing.Size(100, 21)
        Me.Edit_NewComAdr.TabIndex = 4
        Me.Edit_NewComAdr.Text = "00"
        '
        'label15
        '
        Me.label15.AutoSize = True
        Me.label15.Location = New System.Drawing.Point(6, 109)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(89, 12)
        Me.label15.TabIndex = 3
        Me.label15.Text = "Max.Frequency:"
        '
        'label14
        '
        Me.label14.AutoSize = True
        Me.label14.Location = New System.Drawing.Point(6, 79)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(89, 12)
        Me.label14.TabIndex = 2
        Me.label14.Text = "Min.Frequency:"
        '
        'label13
        '
        Me.label13.AutoSize = True
        Me.label13.Location = New System.Drawing.Point(6, 46)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(41, 12)
        Me.label13.TabIndex = 1
        Me.label13.Text = "Power:"
        '
        'label12
        '
        Me.label12.AutoSize = True
        Me.label12.Location = New System.Drawing.Point(6, 22)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(83, 12)
        Me.label12.TabIndex = 0
        Me.label12.Text = "Address(HEX):"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.Button3)
        Me.groupBox2.Controls.Add(Me.Edit_scantime)
        Me.groupBox2.Controls.Add(Me.EPCC1G2)
        Me.groupBox2.Controls.Add(Me.ISO180006B)
        Me.groupBox2.Controls.Add(Me.label11)
        Me.groupBox2.Controls.Add(Me.label10)
        Me.groupBox2.Controls.Add(Me.Edit_dmaxfre)
        Me.groupBox2.Controls.Add(Me.Edit_powerdBm)
        Me.groupBox2.Controls.Add(Me.Edit_Version)
        Me.groupBox2.Controls.Add(Me.label9)
        Me.groupBox2.Controls.Add(Me.label8)
        Me.groupBox2.Controls.Add(Me.label7)
        Me.groupBox2.Controls.Add(Me.Edit_dminfre)
        Me.groupBox2.Controls.Add(Me.Edit_ComAdr)
        Me.groupBox2.Controls.Add(Me.Edit_Type)
        Me.groupBox2.Controls.Add(Me.label6)
        Me.groupBox2.Controls.Add(Me.label5)
        Me.groupBox2.Controls.Add(Me.label4)
        Me.groupBox2.Location = New System.Drawing.Point(147, 6)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(663, 103)
        Me.groupBox2.TabIndex = 41
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Reader Information"
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(538, 74)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(118, 23)
        Me.Button3.TabIndex = 17
        Me.Button3.Text = "Get Reader Info"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Edit_scantime
        '
        Me.Edit_scantime.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_scantime.Location = New System.Drawing.Point(538, 45)
        Me.Edit_scantime.Name = "Edit_scantime"
        Me.Edit_scantime.Size = New System.Drawing.Size(118, 21)
        Me.Edit_scantime.TabIndex = 16
        '
        'EPCC1G2
        '
        Me.EPCC1G2.AutoSize = True
        Me.EPCC1G2.Location = New System.Drawing.Point(538, 27)
        Me.EPCC1G2.Name = "EPCC1G2"
        Me.EPCC1G2.Size = New System.Drawing.Size(72, 16)
        Me.EPCC1G2.TabIndex = 15
        Me.EPCC1G2.Text = "EPCC1-G2"
        Me.EPCC1G2.UseVisualStyleBackColor = True
        '
        'ISO180006B
        '
        Me.ISO180006B.AutoSize = True
        Me.ISO180006B.Location = New System.Drawing.Point(538, 10)
        Me.ISO180006B.Name = "ISO180006B"
        Me.ISO180006B.Size = New System.Drawing.Size(90, 16)
        Me.ISO180006B.TabIndex = 14
        Me.ISO180006B.Text = "ISO18000-6B"
        Me.ISO180006B.UseVisualStyleBackColor = True
        '
        'label11
        '
        Me.label11.AutoSize = True
        Me.label11.Location = New System.Drawing.Point(402, 49)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(137, 12)
        Me.label11.TabIndex = 13
        Me.label11.Text = "Max InventoryScanTime:"
        '
        'label10
        '
        Me.label10.AutoSize = True
        Me.label10.Location = New System.Drawing.Point(402, 18)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(53, 12)
        Me.label10.TabIndex = 12
        Me.label10.Text = "Protocl:"
        '
        'Edit_dmaxfre
        '
        Me.Edit_dmaxfre.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_dmaxfre.Location = New System.Drawing.Point(286, 76)
        Me.Edit_dmaxfre.Name = "Edit_dmaxfre"
        Me.Edit_dmaxfre.Size = New System.Drawing.Size(100, 21)
        Me.Edit_dmaxfre.TabIndex = 11
        '
        'Edit_powerdBm
        '
        Me.Edit_powerdBm.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_powerdBm.Location = New System.Drawing.Point(286, 46)
        Me.Edit_powerdBm.Name = "Edit_powerdBm"
        Me.Edit_powerdBm.Size = New System.Drawing.Size(100, 21)
        Me.Edit_powerdBm.TabIndex = 10
        '
        'Edit_Version
        '
        Me.Edit_Version.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_Version.Location = New System.Drawing.Point(286, 15)
        Me.Edit_Version.Name = "Edit_Version"
        Me.Edit_Version.Size = New System.Drawing.Size(100, 21)
        Me.Edit_Version.TabIndex = 9
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(197, 79)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(89, 12)
        Me.label9.TabIndex = 8
        Me.label9.Text = "Max.Frequency:"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(197, 49)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(41, 12)
        Me.label8.TabIndex = 7
        Me.label8.Text = "Power:"
        '
        'label7
        '
        Me.label7.AutoSize = True
        Me.label7.Location = New System.Drawing.Point(197, 18)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(53, 12)
        Me.label7.TabIndex = 6
        Me.label7.Text = "Version:"
        '
        'Edit_dminfre
        '
        Me.Edit_dminfre.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_dminfre.Location = New System.Drawing.Point(95, 76)
        Me.Edit_dminfre.Name = "Edit_dminfre"
        Me.Edit_dminfre.Size = New System.Drawing.Size(85, 21)
        Me.Edit_dminfre.TabIndex = 5
        '
        'Edit_ComAdr
        '
        Me.Edit_ComAdr.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_ComAdr.Location = New System.Drawing.Point(95, 46)
        Me.Edit_ComAdr.Name = "Edit_ComAdr"
        Me.Edit_ComAdr.Size = New System.Drawing.Size(85, 21)
        Me.Edit_ComAdr.TabIndex = 4
        '
        'Edit_Type
        '
        Me.Edit_Type.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.Edit_Type.Location = New System.Drawing.Point(95, 15)
        Me.Edit_Type.Name = "Edit_Type"
        Me.Edit_Type.Size = New System.Drawing.Size(85, 21)
        Me.Edit_Type.TabIndex = 3
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(6, 79)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(89, 12)
        Me.label6.TabIndex = 2
        Me.label6.Text = "Min.Frequency:"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(6, 49)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(53, 12)
        Me.label5.TabIndex = 1
        Me.label5.Text = "Address:"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(6, 18)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(35, 12)
        Me.label4.TabIndex = 0
        Me.label4.Text = "Type:"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ComboBox_baud2)
        Me.GroupBox1.Controls.Add(Me.label47)
        Me.GroupBox1.Controls.Add(Me.ComboBox_AlreadyOpenCOM)
        Me.GroupBox1.Controls.Add(Me.label3)
        Me.GroupBox1.Controls.Add(Me.ClosePort)
        Me.GroupBox1.Controls.Add(Me.OpenPort)
        Me.GroupBox1.Controls.Add(Me.Edit_CmdComAddr)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ComboBox_COM)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(5, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(136, 206)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Communication"
        '
        'ComboBox_baud2
        '
        Me.ComboBox_baud2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_baud2.FormattingEnabled = True
        Me.ComboBox_baud2.Items.AddRange(New Object() {"9600bps", "19200bps", "38400bps", "57600bps", "115200bps"})
        Me.ComboBox_baud2.Location = New System.Drawing.Point(8, 110)
        Me.ComboBox_baud2.Name = "ComboBox_baud2"
        Me.ComboBox_baud2.Size = New System.Drawing.Size(121, 20)
        Me.ComboBox_baud2.TabIndex = 14
        '
        'label47
        '
        Me.label47.AutoSize = True
        Me.label47.Location = New System.Drawing.Point(8, 95)
        Me.label47.Name = "label47"
        Me.label47.Size = New System.Drawing.Size(65, 12)
        Me.label47.TabIndex = 13
        Me.label47.Text = "Baud Rate:"
        '
        'ComboBox_AlreadyOpenCOM
        '
        Me.ComboBox_AlreadyOpenCOM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_AlreadyOpenCOM.FormattingEnabled = True
        Me.ComboBox_AlreadyOpenCOM.Location = New System.Drawing.Point(5, 153)
        Me.ComboBox_AlreadyOpenCOM.Name = "ComboBox_AlreadyOpenCOM"
        Me.ComboBox_AlreadyOpenCOM.Size = New System.Drawing.Size(125, 20)
        Me.ComboBox_AlreadyOpenCOM.TabIndex = 7
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(6, 136)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(101, 12)
        Me.label3.TabIndex = 6
        Me.label3.Text = "Opened COM Port:"
        '
        'ClosePort
        '
        Me.ClosePort.Location = New System.Drawing.Point(5, 177)
        Me.ClosePort.Name = "ClosePort"
        Me.ClosePort.Size = New System.Drawing.Size(125, 23)
        Me.ClosePort.TabIndex = 5
        Me.ClosePort.Text = "ClosePort"
        Me.ClosePort.UseVisualStyleBackColor = True
        '
        'OpenPort
        '
        Me.OpenPort.Location = New System.Drawing.Point(5, 66)
        Me.OpenPort.Name = "OpenPort"
        Me.OpenPort.Size = New System.Drawing.Size(125, 23)
        Me.OpenPort.TabIndex = 4
        Me.OpenPort.Text = "Open COM Port"
        Me.OpenPort.UseVisualStyleBackColor = True
        '
        'Edit_CmdComAddr
        '
        Me.Edit_CmdComAddr.BackColor = System.Drawing.SystemColors.Window
        Me.Edit_CmdComAddr.Location = New System.Drawing.Point(98, 39)
        Me.Edit_CmdComAddr.MaxLength = 2
        Me.Edit_CmdComAddr.Name = "Edit_CmdComAddr"
        Me.Edit_CmdComAddr.Size = New System.Drawing.Size(32, 21)
        Me.Edit_CmdComAddr.TabIndex = 3
        Me.Edit_CmdComAddr.Text = "FF"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 42)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(95, 12)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Reader Address:"
        '
        'ComboBox_COM
        '
        Me.ComboBox_COM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_COM.FormattingEnabled = True
        Me.ComboBox_COM.Location = New System.Drawing.Point(65, 13)
        Me.ComboBox_COM.Name = "ComboBox_COM"
        Me.ComboBox_COM.Size = New System.Drawing.Size(65, 20)
        Me.ComboBox_COM.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 12)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "COM Port："
        '
        'TabSheet_EPCC1G2
        '
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox31)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox18)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox16)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox15)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox14)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox13)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox12)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox7)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox5)
        Me.TabSheet_EPCC1G2.Controls.Add(Me.groupBox4)
        Me.TabSheet_EPCC1G2.Location = New System.Drawing.Point(4, 21)
        Me.TabSheet_EPCC1G2.Name = "TabSheet_EPCC1G2"
        Me.TabSheet_EPCC1G2.Size = New System.Drawing.Size(817, 625)
        Me.TabSheet_EPCC1G2.TabIndex = 2
        Me.TabSheet_EPCC1G2.Text = "EPCC1-G2 Test"
        Me.TabSheet_EPCC1G2.UseVisualStyleBackColor = True
        '
        'groupBox31
        '
        Me.groupBox31.Controls.Add(Me.maskLen_textBox)
        Me.groupBox31.Controls.Add(Me.label44)
        Me.groupBox31.Controls.Add(Me.maskadr_textbox)
        Me.groupBox31.Controls.Add(Me.label43)
        Me.groupBox31.Controls.Add(Me.checkBox1)
        Me.groupBox31.Location = New System.Drawing.Point(2, 162)
        Me.groupBox31.Name = "groupBox31"
        Me.groupBox31.Size = New System.Drawing.Size(479, 48)
        Me.groupBox31.TabIndex = 9
        Me.groupBox31.TabStop = False
        Me.groupBox31.Text = "EPC Mask Enabled"
        '
        'maskLen_textBox
        '
        Me.maskLen_textBox.Enabled = False
        Me.maskLen_textBox.Location = New System.Drawing.Point(345, 18)
        Me.maskLen_textBox.MaxLength = 2
        Me.maskLen_textBox.Name = "maskLen_textBox"
        Me.maskLen_textBox.Size = New System.Drawing.Size(122, 21)
        Me.maskLen_textBox.TabIndex = 4
        Me.maskLen_textBox.Text = "00"
        '
        'label44
        '
        Me.label44.AutoSize = True
        Me.label44.Location = New System.Drawing.Point(286, 23)
        Me.label44.Name = "label44"
        Me.label44.Size = New System.Drawing.Size(53, 12)
        Me.label44.TabIndex = 3
        Me.label44.Text = "MaskLen:"
        '
        'maskadr_textbox
        '
        Me.maskadr_textbox.Enabled = False
        Me.maskadr_textbox.Location = New System.Drawing.Point(168, 18)
        Me.maskadr_textbox.MaxLength = 2
        Me.maskadr_textbox.Name = "maskadr_textbox"
        Me.maskadr_textbox.Size = New System.Drawing.Size(100, 21)
        Me.maskadr_textbox.TabIndex = 2
        Me.maskadr_textbox.Text = "00"
        '
        'label43
        '
        Me.label43.AutoSize = True
        Me.label43.Location = New System.Drawing.Point(109, 23)
        Me.label43.Name = "label43"
        Me.label43.Size = New System.Drawing.Size(53, 12)
        Me.label43.TabIndex = 1
        Me.label43.Text = "MaskAdr:"
        '
        'checkBox1
        '
        Me.checkBox1.AutoSize = True
        Me.checkBox1.Enabled = False
        Me.checkBox1.Location = New System.Drawing.Point(4, 22)
        Me.checkBox1.Name = "checkBox1"
        Me.checkBox1.Size = New System.Drawing.Size(66, 16)
        Me.checkBox1.TabIndex = 0
        Me.checkBox1.Text = "Enabled"
        Me.checkBox1.UseVisualStyleBackColor = True
        '
        'groupBox18
        '
        Me.groupBox18.Controls.Add(Me.Button_LockUserBlock_G2)
        Me.groupBox18.Controls.Add(Me.Edit_AccessCode6)
        Me.groupBox18.Controls.Add(Me.ComboBox_BlockNum)
        Me.groupBox18.Controls.Add(Me.label30)
        Me.groupBox18.Controls.Add(Me.label29)
        Me.groupBox18.Controls.Add(Me.ComboBox_EPC6)
        Me.groupBox18.Location = New System.Drawing.Point(485, 529)
        Me.groupBox18.Name = "groupBox18"
        Me.groupBox18.Size = New System.Drawing.Size(325, 94)
        Me.groupBox18.TabIndex = 8
        Me.groupBox18.TabStop = False
        Me.groupBox18.Text = "Lock Block for User (Permanently Lock)"
        '
        'Button_LockUserBlock_G2
        '
        Me.Button_LockUserBlock_G2.Location = New System.Drawing.Point(226, 66)
        Me.Button_LockUserBlock_G2.Name = "Button_LockUserBlock_G2"
        Me.Button_LockUserBlock_G2.Size = New System.Drawing.Size(89, 23)
        Me.Button_LockUserBlock_G2.TabIndex = 5
        Me.Button_LockUserBlock_G2.Text = "Lock"
        Me.Button_LockUserBlock_G2.UseVisualStyleBackColor = True
        '
        'Edit_AccessCode6
        '
        Me.Edit_AccessCode6.Location = New System.Drawing.Point(134, 69)
        Me.Edit_AccessCode6.MaxLength = 8
        Me.Edit_AccessCode6.Name = "Edit_AccessCode6"
        Me.Edit_AccessCode6.Size = New System.Drawing.Size(85, 21)
        Me.Edit_AccessCode6.TabIndex = 4
        Me.Edit_AccessCode6.Text = "00000000"
        '
        'ComboBox_BlockNum
        '
        Me.ComboBox_BlockNum.FormattingEnabled = True
        Me.ComboBox_BlockNum.Location = New System.Drawing.Point(134, 45)
        Me.ComboBox_BlockNum.Name = "ComboBox_BlockNum"
        Me.ComboBox_BlockNum.Size = New System.Drawing.Size(87, 20)
        Me.ComboBox_BlockNum.TabIndex = 3
        '
        'label30
        '
        Me.label30.AutoSize = True
        Me.label30.Location = New System.Drawing.Point(9, 65)
        Me.label30.Name = "label30"
        Me.label30.Size = New System.Drawing.Size(95, 24)
        Me.label30.TabIndex = 2
        Me.label30.Text = "Access Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(8 Hex):"
        '
        'label29
        '
        Me.label29.AutoSize = True
        Me.label29.Location = New System.Drawing.Point(9, 42)
        Me.label29.Name = "label29"
        Me.label29.Size = New System.Drawing.Size(119, 24)
        Me.label29.TabIndex = 1
        Me.label29.Text = "Address of Tag Data" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Word):"
        '
        'ComboBox_EPC6
        '
        Me.ComboBox_EPC6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC6.FormattingEnabled = True
        Me.ComboBox_EPC6.Location = New System.Drawing.Point(6, 20)
        Me.ComboBox_EPC6.Name = "ComboBox_EPC6"
        Me.ComboBox_EPC6.Size = New System.Drawing.Size(313, 20)
        Me.ComboBox_EPC6.TabIndex = 0
        '
        'groupBox16
        '
        Me.groupBox16.Controls.Add(Me.Label_Alarm)
        Me.groupBox16.Controls.Add(Me.button4)
        Me.groupBox16.Controls.Add(Me.Button_SetEASAlarm_G2)
        Me.groupBox16.Controls.Add(Me.groupBox17)
        Me.groupBox16.Controls.Add(Me.Edit_AccessCode5)
        Me.groupBox16.Controls.Add(Me.label28)
        Me.groupBox16.Controls.Add(Me.ComboBox_EPC5)
        Me.groupBox16.Location = New System.Drawing.Point(485, 423)
        Me.groupBox16.Name = "groupBox16"
        Me.groupBox16.Size = New System.Drawing.Size(325, 106)
        Me.groupBox16.TabIndex = 7
        Me.groupBox16.TabStop = False
        Me.groupBox16.Text = "EAS Alarm"
        '
        'Label_Alarm
        '
        Me.Label_Alarm.AutoSize = True
        Me.Label_Alarm.Font = New System.Drawing.Font("MS Reference Sans Serif", 30.3!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte), True)
        Me.Label_Alarm.ForeColor = System.Drawing.Color.Red
        Me.Label_Alarm.Location = New System.Drawing.Point(243, 37)
        Me.Label_Alarm.Name = "Label_Alarm"
        Me.Label_Alarm.Size = New System.Drawing.Size(51, 46)
        Me.Label_Alarm.TabIndex = 14
        Me.Label_Alarm.Text = "l"
        '
        'button4
        '
        Me.button4.Enabled = False
        Me.button4.Location = New System.Drawing.Point(226, 78)
        Me.button4.Name = "button4"
        Me.button4.Size = New System.Drawing.Size(89, 23)
        Me.button4.TabIndex = 13
        Me.button4.Text = "Check Alarm"
        Me.button4.UseVisualStyleBackColor = True
        '
        'Button_SetEASAlarm_G2
        '
        Me.Button_SetEASAlarm_G2.Location = New System.Drawing.Point(110, 78)
        Me.Button_SetEASAlarm_G2.Name = "Button_SetEASAlarm_G2"
        Me.Button_SetEASAlarm_G2.Size = New System.Drawing.Size(97, 23)
        Me.Button_SetEASAlarm_G2.TabIndex = 12
        Me.Button_SetEASAlarm_G2.Text = "Alarm Setting"
        Me.Button_SetEASAlarm_G2.UseVisualStyleBackColor = True
        '
        'groupBox17
        '
        Me.groupBox17.Controls.Add(Me.NoAlarm_G2)
        Me.groupBox17.Controls.Add(Me.Alarm_G2)
        Me.groupBox17.Location = New System.Drawing.Point(6, 61)
        Me.groupBox17.Name = "groupBox17"
        Me.groupBox17.Size = New System.Drawing.Size(100, 41)
        Me.groupBox17.TabIndex = 11
        Me.groupBox17.TabStop = False
        '
        'NoAlarm_G2
        '
        Me.NoAlarm_G2.AutoSize = True
        Me.NoAlarm_G2.Location = New System.Drawing.Point(5, 24)
        Me.NoAlarm_G2.Name = "NoAlarm_G2"
        Me.NoAlarm_G2.Size = New System.Drawing.Size(71, 16)
        Me.NoAlarm_G2.TabIndex = 1
        Me.NoAlarm_G2.TabStop = True
        Me.NoAlarm_G2.Text = "No Alarm"
        Me.NoAlarm_G2.UseVisualStyleBackColor = True
        '
        'Alarm_G2
        '
        Me.Alarm_G2.AutoSize = True
        Me.Alarm_G2.Location = New System.Drawing.Point(5, 9)
        Me.Alarm_G2.Name = "Alarm_G2"
        Me.Alarm_G2.Size = New System.Drawing.Size(53, 16)
        Me.Alarm_G2.TabIndex = 0
        Me.Alarm_G2.TabStop = True
        Me.Alarm_G2.Text = "Alarm"
        Me.Alarm_G2.UseVisualStyleBackColor = True
        '
        'Edit_AccessCode5
        '
        Me.Edit_AccessCode5.Location = New System.Drawing.Point(107, 39)
        Me.Edit_AccessCode5.MaxLength = 8
        Me.Edit_AccessCode5.Name = "Edit_AccessCode5"
        Me.Edit_AccessCode5.Size = New System.Drawing.Size(100, 21)
        Me.Edit_AccessCode5.TabIndex = 10
        Me.Edit_AccessCode5.Text = "00000000"
        '
        'label28
        '
        Me.label28.AutoSize = True
        Me.label28.Location = New System.Drawing.Point(6, 36)
        Me.label28.Name = "label28"
        Me.label28.Size = New System.Drawing.Size(95, 24)
        Me.label28.TabIndex = 9
        Me.label28.Text = "Access Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(8 Hex):"
        '
        'ComboBox_EPC5
        '
        Me.ComboBox_EPC5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC5.FormattingEnabled = True
        Me.ComboBox_EPC5.Location = New System.Drawing.Point(6, 15)
        Me.ComboBox_EPC5.Name = "ComboBox_EPC5"
        Me.ComboBox_EPC5.Size = New System.Drawing.Size(313, 20)
        Me.ComboBox_EPC5.TabIndex = 8
        '
        'groupBox15
        '
        Me.groupBox15.Controls.Add(Me.Button_CheckReadProtected_G2)
        Me.groupBox15.Controls.Add(Me.Button_RemoveReadProtect_G2)
        Me.groupBox15.Controls.Add(Me.Button_SetMultiReadProtect_G2)
        Me.groupBox15.Controls.Add(Me.Button_SetReadProtect_G2)
        Me.groupBox15.Controls.Add(Me.Edit_AccessCode4)
        Me.groupBox15.Controls.Add(Me.label27)
        Me.groupBox15.Controls.Add(Me.ComboBox_EPC4)
        Me.groupBox15.Location = New System.Drawing.Point(485, 246)
        Me.groupBox15.Name = "groupBox15"
        Me.groupBox15.Size = New System.Drawing.Size(325, 175)
        Me.groupBox15.TabIndex = 6
        Me.groupBox15.TabStop = False
        Me.groupBox15.Text = "Read Protection"
        '
        'Button_CheckReadProtected_G2
        '
        Me.Button_CheckReadProtected_G2.Enabled = False
        Me.Button_CheckReadProtected_G2.Location = New System.Drawing.Point(6, 147)
        Me.Button_CheckReadProtected_G2.Name = "Button_CheckReadProtected_G2"
        Me.Button_CheckReadProtected_G2.Size = New System.Drawing.Size(313, 23)
        Me.Button_CheckReadProtected_G2.TabIndex = 6
        Me.Button_CheckReadProtected_G2.Text = "Detect Single Tag Read Protection without EPC Password"
        Me.Button_CheckReadProtected_G2.UseVisualStyleBackColor = True
        '
        'Button_RemoveReadProtect_G2
        '
        Me.Button_RemoveReadProtect_G2.Enabled = False
        Me.Button_RemoveReadProtect_G2.Location = New System.Drawing.Point(6, 121)
        Me.Button_RemoveReadProtect_G2.Name = "Button_RemoveReadProtect_G2"
        Me.Button_RemoveReadProtect_G2.Size = New System.Drawing.Size(313, 23)
        Me.Button_RemoveReadProtect_G2.TabIndex = 5
        Me.Button_RemoveReadProtect_G2.Text = "Reset Single Tag Read Protection without EPC"
        Me.Button_RemoveReadProtect_G2.UseVisualStyleBackColor = True
        '
        'Button_SetMultiReadProtect_G2
        '
        Me.Button_SetMultiReadProtect_G2.Enabled = False
        Me.Button_SetMultiReadProtect_G2.Location = New System.Drawing.Point(6, 94)
        Me.Button_SetMultiReadProtect_G2.Name = "Button_SetMultiReadProtect_G2"
        Me.Button_SetMultiReadProtect_G2.Size = New System.Drawing.Size(313, 23)
        Me.Button_SetMultiReadProtect_G2.TabIndex = 4
        Me.Button_SetMultiReadProtect_G2.Text = "Set Single Tag Read Protection without EPC"
        Me.Button_SetMultiReadProtect_G2.UseVisualStyleBackColor = True
        '
        'Button_SetReadProtect_G2
        '
        Me.Button_SetReadProtect_G2.Enabled = False
        Me.Button_SetReadProtect_G2.Location = New System.Drawing.Point(6, 67)
        Me.Button_SetReadProtect_G2.Name = "Button_SetReadProtect_G2"
        Me.Button_SetReadProtect_G2.Size = New System.Drawing.Size(313, 23)
        Me.Button_SetReadProtect_G2.TabIndex = 3
        Me.Button_SetReadProtect_G2.Text = "Set Single Tag Read Protection"
        Me.Button_SetReadProtect_G2.UseVisualStyleBackColor = True
        '
        'Edit_AccessCode4
        '
        Me.Edit_AccessCode4.Location = New System.Drawing.Point(107, 42)
        Me.Edit_AccessCode4.MaxLength = 8
        Me.Edit_AccessCode4.Name = "Edit_AccessCode4"
        Me.Edit_AccessCode4.Size = New System.Drawing.Size(100, 21)
        Me.Edit_AccessCode4.TabIndex = 2
        Me.Edit_AccessCode4.Text = "00000000"
        '
        'label27
        '
        Me.label27.AutoSize = True
        Me.label27.Location = New System.Drawing.Point(6, 39)
        Me.label27.Name = "label27"
        Me.label27.Size = New System.Drawing.Size(95, 24)
        Me.label27.TabIndex = 1
        Me.label27.Text = "Access Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(8 Hex):"
        '
        'ComboBox_EPC4
        '
        Me.ComboBox_EPC4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC4.FormattingEnabled = True
        Me.ComboBox_EPC4.Location = New System.Drawing.Point(8, 15)
        Me.ComboBox_EPC4.Name = "ComboBox_EPC4"
        Me.ComboBox_EPC4.Size = New System.Drawing.Size(311, 20)
        Me.ComboBox_EPC4.TabIndex = 0
        '
        'groupBox14
        '
        Me.groupBox14.Controls.Add(Me.Button_WriteEPC_G2)
        Me.groupBox14.Controls.Add(Me.Edit_AccessCode3)
        Me.groupBox14.Controls.Add(Me.label26)
        Me.groupBox14.Controls.Add(Me.Edit_WriteEPC)
        Me.groupBox14.Controls.Add(Me.label25)
        Me.groupBox14.Location = New System.Drawing.Point(485, 172)
        Me.groupBox14.Name = "groupBox14"
        Me.groupBox14.Size = New System.Drawing.Size(325, 72)
        Me.groupBox14.TabIndex = 5
        Me.groupBox14.TabStop = False
        Me.groupBox14.Text = "Write EPC(Random write one tag in the antenna)"
        '
        'Button_WriteEPC_G2
        '
        Me.Button_WriteEPC_G2.Enabled = False
        Me.Button_WriteEPC_G2.Location = New System.Drawing.Point(244, 44)
        Me.Button_WriteEPC_G2.Name = "Button_WriteEPC_G2"
        Me.Button_WriteEPC_G2.Size = New System.Drawing.Size(75, 23)
        Me.Button_WriteEPC_G2.TabIndex = 4
        Me.Button_WriteEPC_G2.Text = "Write EPC"
        Me.Button_WriteEPC_G2.UseVisualStyleBackColor = True
        '
        'Edit_AccessCode3
        '
        Me.Edit_AccessCode3.Location = New System.Drawing.Point(107, 46)
        Me.Edit_AccessCode3.MaxLength = 8
        Me.Edit_AccessCode3.Name = "Edit_AccessCode3"
        Me.Edit_AccessCode3.Size = New System.Drawing.Size(100, 21)
        Me.Edit_AccessCode3.TabIndex = 3
        Me.Edit_AccessCode3.Text = "00000000"
        '
        'label26
        '
        Me.label26.AutoSize = True
        Me.label26.Location = New System.Drawing.Point(6, 41)
        Me.label26.Name = "label26"
        Me.label26.Size = New System.Drawing.Size(95, 24)
        Me.label26.TabIndex = 2
        Me.label26.Text = "Access Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(8 Hex):"
        '
        'Edit_WriteEPC
        '
        Me.Edit_WriteEPC.Location = New System.Drawing.Point(77, 17)
        Me.Edit_WriteEPC.MaxLength = 60
        Me.Edit_WriteEPC.Name = "Edit_WriteEPC"
        Me.Edit_WriteEPC.Size = New System.Drawing.Size(242, 21)
        Me.Edit_WriteEPC.TabIndex = 1
        Me.Edit_WriteEPC.Text = "0000"
        '
        'label25
        '
        Me.label25.AutoSize = True
        Me.label25.Location = New System.Drawing.Point(6, 14)
        Me.label25.Name = "label25"
        Me.label25.Size = New System.Drawing.Size(65, 24)
        Me.label25.TabIndex = 0
        Me.label25.Text = "Write EPC:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(1-15Word)"
        '
        'groupBox13
        '
        Me.groupBox13.Controls.Add(Me.Button_DestroyCard)
        Me.groupBox13.Controls.Add(Me.Edit_DestroyCode)
        Me.groupBox13.Controls.Add(Me.label24)
        Me.groupBox13.Controls.Add(Me.ComboBox_EPC3)
        Me.groupBox13.Location = New System.Drawing.Point(485, 99)
        Me.groupBox13.Name = "groupBox13"
        Me.groupBox13.Size = New System.Drawing.Size(325, 67)
        Me.groupBox13.TabIndex = 4
        Me.groupBox13.TabStop = False
        Me.groupBox13.Text = "Kill Tag"
        '
        'Button_DestroyCard
        '
        Me.Button_DestroyCard.Location = New System.Drawing.Point(244, 39)
        Me.Button_DestroyCard.Name = "Button_DestroyCard"
        Me.Button_DestroyCard.Size = New System.Drawing.Size(75, 23)
        Me.Button_DestroyCard.TabIndex = 3
        Me.Button_DestroyCard.Text = "Kill Tag"
        Me.Button_DestroyCard.UseVisualStyleBackColor = True
        '
        'Edit_DestroyCode
        '
        Me.Edit_DestroyCode.Location = New System.Drawing.Point(115, 41)
        Me.Edit_DestroyCode.MaxLength = 8
        Me.Edit_DestroyCode.Name = "Edit_DestroyCode"
        Me.Edit_DestroyCode.Size = New System.Drawing.Size(92, 21)
        Me.Edit_DestroyCode.TabIndex = 2
        Me.Edit_DestroyCode.Text = "00000000"
        '
        'label24
        '
        Me.label24.AutoSize = True
        Me.label24.Location = New System.Drawing.Point(6, 39)
        Me.label24.Name = "label24"
        Me.label24.Size = New System.Drawing.Size(83, 24)
        Me.label24.TabIndex = 1
        Me.label24.Text = "Kill Password" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(8 Hex):"
        '
        'ComboBox_EPC3
        '
        Me.ComboBox_EPC3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC3.FormattingEnabled = True
        Me.ComboBox_EPC3.Location = New System.Drawing.Point(8, 15)
        Me.ComboBox_EPC3.Name = "ComboBox_EPC3"
        Me.ComboBox_EPC3.Size = New System.Drawing.Size(311, 20)
        Me.ComboBox_EPC3.TabIndex = 0
        '
        'groupBox12
        '
        Me.groupBox12.Controls.Add(Me.CheckBox_TID)
        Me.groupBox12.Controls.Add(Me.groupBox33)
        Me.groupBox12.Controls.Add(Me.button2)
        Me.groupBox12.Controls.Add(Me.ComboBox_IntervalTime)
        Me.groupBox12.Controls.Add(Me.label23)
        Me.groupBox12.Location = New System.Drawing.Point(485, 0)
        Me.groupBox12.Name = "groupBox12"
        Me.groupBox12.Size = New System.Drawing.Size(325, 96)
        Me.groupBox12.TabIndex = 3
        Me.groupBox12.TabStop = False
        Me.groupBox12.Text = "Query Tag"
        '
        'CheckBox_TID
        '
        Me.CheckBox_TID.AutoSize = True
        Me.CheckBox_TID.Location = New System.Drawing.Point(228, 62)
        Me.CheckBox_TID.Name = "CheckBox_TID"
        Me.CheckBox_TID.Size = New System.Drawing.Size(42, 16)
        Me.CheckBox_TID.TabIndex = 8
        Me.CheckBox_TID.Text = "TID"
        Me.CheckBox_TID.UseVisualStyleBackColor = True
        '
        'groupBox33
        '
        Me.groupBox33.Controls.Add(Me.textBox5)
        Me.groupBox33.Controls.Add(Me.label55)
        Me.groupBox33.Controls.Add(Me.textBox4)
        Me.groupBox33.Controls.Add(Me.label54)
        Me.groupBox33.Location = New System.Drawing.Point(8, 42)
        Me.groupBox33.Name = "groupBox33"
        Me.groupBox33.Size = New System.Drawing.Size(209, 48)
        Me.groupBox33.TabIndex = 7
        Me.groupBox33.TabStop = False
        Me.groupBox33.Text = "Query TID Parameter"
        '
        'textBox5
        '
        Me.textBox5.Location = New System.Drawing.Point(167, 14)
        Me.textBox5.MaxLength = 2
        Me.textBox5.Name = "textBox5"
        Me.textBox5.Size = New System.Drawing.Size(37, 21)
        Me.textBox5.TabIndex = 3
        Me.textBox5.Text = "04"
        '
        'label55
        '
        Me.label55.AutoSize = True
        Me.label55.Location = New System.Drawing.Point(126, 23)
        Me.label55.Name = "label55"
        Me.label55.Size = New System.Drawing.Size(35, 12)
        Me.label55.TabIndex = 2
        Me.label55.Text = "Len："
        '
        'textBox4
        '
        Me.textBox4.Location = New System.Drawing.Point(71, 15)
        Me.textBox4.MaxLength = 2
        Me.textBox4.Name = "textBox4"
        Me.textBox4.Size = New System.Drawing.Size(37, 21)
        Me.textBox4.TabIndex = 1
        Me.textBox4.Text = "02"
        '
        'label54
        '
        Me.label54.AutoSize = True
        Me.label54.Location = New System.Drawing.Point(4, 24)
        Me.label54.Name = "label54"
        Me.label54.Size = New System.Drawing.Size(71, 12)
        Me.label54.TabIndex = 0
        Me.label54.Text = "StartAddr："
        '
        'button2
        '
        Me.button2.Enabled = False
        Me.button2.Location = New System.Drawing.Point(228, 17)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(87, 23)
        Me.button2.TabIndex = 2
        Me.button2.Text = "Query Tag"
        Me.button2.UseVisualStyleBackColor = True
        '
        'ComboBox_IntervalTime
        '
        Me.ComboBox_IntervalTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IntervalTime.FormattingEnabled = True
        Me.ComboBox_IntervalTime.Location = New System.Drawing.Point(101, 20)
        Me.ComboBox_IntervalTime.Name = "ComboBox_IntervalTime"
        Me.ComboBox_IntervalTime.Size = New System.Drawing.Size(120, 20)
        Me.ComboBox_IntervalTime.TabIndex = 1
        '
        'label23
        '
        Me.label23.AutoSize = True
        Me.label23.Location = New System.Drawing.Point(6, 24)
        Me.label23.Name = "label23"
        Me.label23.Size = New System.Drawing.Size(89, 12)
        Me.label23.TabIndex = 0
        Me.label23.Text = "Read Interval:"
        '
        'groupBox7
        '
        Me.groupBox7.Controls.Add(Me.Button_SetProtectState)
        Me.groupBox7.Controls.Add(Me.textBox2)
        Me.groupBox7.Controls.Add(Me.label22)
        Me.groupBox7.Controls.Add(Me.groupBox11)
        Me.groupBox7.Controls.Add(Me.groupBox10)
        Me.groupBox7.Controls.Add(Me.groupBox8)
        Me.groupBox7.Controls.Add(Me.ComboBox_EPC1)
        Me.groupBox7.Location = New System.Drawing.Point(1, 414)
        Me.groupBox7.Name = "groupBox7"
        Me.groupBox7.Size = New System.Drawing.Size(481, 209)
        Me.groupBox7.TabIndex = 2
        Me.groupBox7.TabStop = False
        Me.groupBox7.Text = "Set Protect For Reading Or Writing"
        '
        'Button_SetProtectState
        '
        Me.Button_SetProtectState.Location = New System.Drawing.Point(375, 181)
        Me.Button_SetProtectState.Name = "Button_SetProtectState"
        Me.Button_SetProtectState.Size = New System.Drawing.Size(99, 23)
        Me.Button_SetProtectState.TabIndex = 6
        Me.Button_SetProtectState.Text = "Set Protect"
        Me.Button_SetProtectState.UseVisualStyleBackColor = True
        '
        'textBox2
        '
        Me.textBox2.Location = New System.Drawing.Point(263, 183)
        Me.textBox2.MaxLength = 8
        Me.textBox2.Name = "textBox2"
        Me.textBox2.Size = New System.Drawing.Size(100, 21)
        Me.textBox2.TabIndex = 5
        Me.textBox2.Text = "00000000"
        '
        'label22
        '
        Me.label22.AutoSize = True
        Me.label22.Location = New System.Drawing.Point(261, 165)
        Me.label22.Name = "label22"
        Me.label22.Size = New System.Drawing.Size(149, 12)
        Me.label22.TabIndex = 4
        Me.label22.Text = "Access Password (8 Hex):"
        '
        'groupBox11
        '
        Me.groupBox11.Controls.Add(Me.AlwaysNot2)
        Me.groupBox11.Controls.Add(Me.Always2)
        Me.groupBox11.Controls.Add(Me.Proect2)
        Me.groupBox11.Controls.Add(Me.NoProect2)
        Me.groupBox11.Location = New System.Drawing.Point(258, 43)
        Me.groupBox11.Name = "groupBox11"
        Me.groupBox11.Size = New System.Drawing.Size(217, 120)
        Me.groupBox11.TabIndex = 3
        Me.groupBox11.TabStop = False
        Me.groupBox11.Text = "Lock of EPC TID and User Bank"
        '
        'AlwaysNot2
        '
        Me.AlwaysNot2.AutoSize = True
        Me.AlwaysNot2.Location = New System.Drawing.Point(5, 97)
        Me.AlwaysNot2.Name = "AlwaysNot2"
        Me.AlwaysNot2.Size = New System.Drawing.Size(113, 16)
        Me.AlwaysNot2.TabIndex = 3
        Me.AlwaysNot2.TabStop = True
        Me.AlwaysNot2.Text = "Never writeable"
        Me.AlwaysNot2.UseVisualStyleBackColor = True
        '
        'Always2
        '
        Me.Always2.AutoSize = True
        Me.Always2.Location = New System.Drawing.Point(5, 71)
        Me.Always2.Name = "Always2"
        Me.Always2.Size = New System.Drawing.Size(149, 16)
        Me.Always2.TabIndex = 2
        Me.Always2.TabStop = True
        Me.Always2.Text = "Permanently writeable"
        Me.Always2.UseVisualStyleBackColor = True
        '
        'Proect2
        '
        Me.Proect2.AutoSize = True
        Me.Proect2.Location = New System.Drawing.Point(5, 45)
        Me.Proect2.Name = "Proect2"
        Me.Proect2.Size = New System.Drawing.Size(215, 16)
        Me.Proect2.TabIndex = 1
        Me.Proect2.TabStop = True
        Me.Proect2.Text = "Writeable from the secured state"
        Me.Proect2.UseVisualStyleBackColor = True
        '
        'NoProect2
        '
        Me.NoProect2.AutoSize = True
        Me.NoProect2.Location = New System.Drawing.Point(5, 19)
        Me.NoProect2.Name = "NoProect2"
        Me.NoProect2.Size = New System.Drawing.Size(167, 16)
        Me.NoProect2.TabIndex = 0
        Me.NoProect2.TabStop = True
        Me.NoProect2.Text = "Writeable from any state"
        Me.NoProect2.UseVisualStyleBackColor = True
        '
        'groupBox10
        '
        Me.groupBox10.Controls.Add(Me.P_User)
        Me.groupBox10.Controls.Add(Me.P_TID)
        Me.groupBox10.Controls.Add(Me.P_EPC)
        Me.groupBox10.Controls.Add(Me.P_Reserve)
        Me.groupBox10.Location = New System.Drawing.Point(259, 12)
        Me.groupBox10.Name = "groupBox10"
        Me.groupBox10.Size = New System.Drawing.Size(215, 31)
        Me.groupBox10.TabIndex = 2
        Me.groupBox10.TabStop = False
        '
        'P_User
        '
        Me.P_User.AutoSize = True
        Me.P_User.Location = New System.Drawing.Point(162, 11)
        Me.P_User.Name = "P_User"
        Me.P_User.Size = New System.Drawing.Size(47, 16)
        Me.P_User.TabIndex = 3
        Me.P_User.TabStop = True
        Me.P_User.Text = "User"
        Me.P_User.UseVisualStyleBackColor = True
        '
        'P_TID
        '
        Me.P_TID.AutoSize = True
        Me.P_TID.Location = New System.Drawing.Point(119, 11)
        Me.P_TID.Name = "P_TID"
        Me.P_TID.Size = New System.Drawing.Size(41, 16)
        Me.P_TID.TabIndex = 2
        Me.P_TID.TabStop = True
        Me.P_TID.Text = "TID"
        Me.P_TID.UseVisualStyleBackColor = True
        '
        'P_EPC
        '
        Me.P_EPC.AutoSize = True
        Me.P_EPC.Location = New System.Drawing.Point(77, 11)
        Me.P_EPC.Name = "P_EPC"
        Me.P_EPC.Size = New System.Drawing.Size(41, 16)
        Me.P_EPC.TabIndex = 1
        Me.P_EPC.TabStop = True
        Me.P_EPC.Text = "EPC"
        Me.P_EPC.UseVisualStyleBackColor = True
        '
        'P_Reserve
        '
        Me.P_Reserve.AutoSize = True
        Me.P_Reserve.Location = New System.Drawing.Point(4, 11)
        Me.P_Reserve.Name = "P_Reserve"
        Me.P_Reserve.Size = New System.Drawing.Size(71, 16)
        Me.P_Reserve.TabIndex = 0
        Me.P_Reserve.TabStop = True
        Me.P_Reserve.Text = "Password"
        Me.P_Reserve.UseVisualStyleBackColor = True
        '
        'groupBox8
        '
        Me.groupBox8.Controls.Add(Me.AlwaysNot)
        Me.groupBox8.Controls.Add(Me.Always)
        Me.groupBox8.Controls.Add(Me.Proect)
        Me.groupBox8.Controls.Add(Me.NoProect)
        Me.groupBox8.Controls.Add(Me.groupBox9)
        Me.groupBox8.Location = New System.Drawing.Point(4, 35)
        Me.groupBox8.Name = "groupBox8"
        Me.groupBox8.Size = New System.Drawing.Size(248, 171)
        Me.groupBox8.TabIndex = 1
        Me.groupBox8.TabStop = False
        Me.groupBox8.Text = "Lock of Password"
        '
        'AlwaysNot
        '
        Me.AlwaysNot.AutoSize = True
        Me.AlwaysNot.Location = New System.Drawing.Point(6, 149)
        Me.AlwaysNot.Name = "AlwaysNot"
        Me.AlwaysNot.Size = New System.Drawing.Size(191, 16)
        Me.AlwaysNot.TabIndex = 4
        Me.AlwaysNot.TabStop = True
        Me.AlwaysNot.Text = "Never readable and writeable"
        Me.AlwaysNot.UseVisualStyleBackColor = True
        '
        'Always
        '
        Me.Always.AutoSize = True
        Me.Always.Location = New System.Drawing.Point(6, 125)
        Me.Always.Name = "Always"
        Me.Always.Size = New System.Drawing.Size(227, 16)
        Me.Always.TabIndex = 3
        Me.Always.TabStop = True
        Me.Always.Text = "Permanently readable and writeable"
        Me.Always.UseVisualStyleBackColor = True
        '
        'Proect
        '
        Me.Proect.AutoSize = True
        Me.Proect.Location = New System.Drawing.Point(6, 92)
        Me.Proect.Name = "Proect"
        Me.Proect.Size = New System.Drawing.Size(215, 28)
        Me.Proect.TabIndex = 2
        Me.Proect.TabStop = True
        Me.Proect.Text = "Readable and writeable from the " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "secured state"
        Me.Proect.UseVisualStyleBackColor = True
        '
        'NoProect
        '
        Me.NoProect.AutoSize = True
        Me.NoProect.Location = New System.Drawing.Point(6, 67)
        Me.NoProect.Name = "NoProect"
        Me.NoProect.Size = New System.Drawing.Size(197, 28)
        Me.NoProect.TabIndex = 1
        Me.NoProect.TabStop = True
        Me.NoProect.Text = "Readable and  writeable from " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "any state"
        Me.NoProect.UseVisualStyleBackColor = True
        '
        'groupBox9
        '
        Me.groupBox9.Controls.Add(Me.AccessCode)
        Me.groupBox9.Controls.Add(Me.DestroyCode)
        Me.groupBox9.Location = New System.Drawing.Point(5, 18)
        Me.groupBox9.Name = "groupBox9"
        Me.groupBox9.Size = New System.Drawing.Size(237, 48)
        Me.groupBox9.TabIndex = 0
        Me.groupBox9.TabStop = False
        '
        'AccessCode
        '
        Me.AccessCode.AutoSize = True
        Me.AccessCode.Location = New System.Drawing.Point(108, 20)
        Me.AccessCode.Name = "AccessCode"
        Me.AccessCode.Size = New System.Drawing.Size(113, 16)
        Me.AccessCode.TabIndex = 1
        Me.AccessCode.TabStop = True
        Me.AccessCode.Text = "Access Password"
        Me.AccessCode.UseVisualStyleBackColor = True
        '
        'DestroyCode
        '
        Me.DestroyCode.AutoSize = True
        Me.DestroyCode.Location = New System.Drawing.Point(6, 20)
        Me.DestroyCode.Name = "DestroyCode"
        Me.DestroyCode.Size = New System.Drawing.Size(101, 16)
        Me.DestroyCode.TabIndex = 0
        Me.DestroyCode.TabStop = True
        Me.DestroyCode.Text = "Kill Password"
        Me.DestroyCode.UseVisualStyleBackColor = True
        '
        'ComboBox_EPC1
        '
        Me.ComboBox_EPC1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC1.FormattingEnabled = True
        Me.ComboBox_EPC1.Location = New System.Drawing.Point(4, 14)
        Me.ComboBox_EPC1.Name = "ComboBox_EPC1"
        Me.ComboBox_EPC1.Size = New System.Drawing.Size(249, 20)
        Me.ComboBox_EPC1.TabIndex = 0
        '
        'groupBox5
        '
        Me.groupBox5.Controls.Add(Me.textBox_pc)
        Me.groupBox5.Controls.Add(Me.checkBox_pc)
        Me.groupBox5.Controls.Add(Me.Button_BlockWrite)
        Me.groupBox5.Controls.Add(Me.ComboBox_EPC2)
        Me.groupBox5.Controls.Add(Me.button7)
        Me.groupBox5.Controls.Add(Me.Button_BlockErase)
        Me.groupBox5.Controls.Add(Me.Button_DataWrite)
        Me.groupBox5.Controls.Add(Me.SpeedButton_Read_G2)
        Me.groupBox5.Controls.Add(Me.Edit_WriteData)
        Me.groupBox5.Controls.Add(Me.Edit_AccessCode2)
        Me.groupBox5.Controls.Add(Me.textBox1)
        Me.groupBox5.Controls.Add(Me.Edit_WordPtr)
        Me.groupBox5.Controls.Add(Me.listBox1)
        Me.groupBox5.Controls.Add(Me.label21)
        Me.groupBox5.Controls.Add(Me.label20)
        Me.groupBox5.Controls.Add(Me.label19)
        Me.groupBox5.Controls.Add(Me.label18)
        Me.groupBox5.Controls.Add(Me.groupBox6)
        Me.groupBox5.Location = New System.Drawing.Point(1, 211)
        Me.groupBox5.Name = "groupBox5"
        Me.groupBox5.Size = New System.Drawing.Size(481, 203)
        Me.groupBox5.TabIndex = 1
        Me.groupBox5.TabStop = False
        Me.groupBox5.Text = "Read Data / Write Data / Block Erase"
        '
        'textBox_pc
        '
        Me.textBox_pc.BackColor = System.Drawing.SystemColors.ScrollBar
        Me.textBox_pc.Location = New System.Drawing.Point(438, 13)
        Me.textBox_pc.Name = "textBox_pc"
        Me.textBox_pc.ReadOnly = True
        Me.textBox_pc.Size = New System.Drawing.Size(39, 21)
        Me.textBox_pc.TabIndex = 23
        Me.textBox_pc.Text = "0800"
        '
        'checkBox_pc
        '
        Me.checkBox_pc.AutoSize = True
        Me.checkBox_pc.Location = New System.Drawing.Point(308, 15)
        Me.checkBox_pc.Name = "checkBox_pc"
        Me.checkBox_pc.Size = New System.Drawing.Size(132, 16)
        Me.checkBox_pc.TabIndex = 22
        Me.checkBox_pc.Text = "Compute and add PC"
        Me.checkBox_pc.UseVisualStyleBackColor = True
        '
        'Button_BlockWrite
        '
        Me.Button_BlockWrite.Enabled = False
        Me.Button_BlockWrite.Location = New System.Drawing.Point(91, 175)
        Me.Button_BlockWrite.Name = "Button_BlockWrite"
        Me.Button_BlockWrite.Size = New System.Drawing.Size(79, 23)
        Me.Button_BlockWrite.TabIndex = 16
        Me.Button_BlockWrite.Text = "Block Write"
        Me.Button_BlockWrite.UseVisualStyleBackColor = True
        '
        'ComboBox_EPC2
        '
        Me.ComboBox_EPC2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_EPC2.FormattingEnabled = True
        Me.ComboBox_EPC2.Location = New System.Drawing.Point(4, 16)
        Me.ComboBox_EPC2.Name = "ComboBox_EPC2"
        Me.ComboBox_EPC2.Size = New System.Drawing.Size(296, 20)
        Me.ComboBox_EPC2.TabIndex = 15
        '
        'button7
        '
        Me.button7.Location = New System.Drawing.Point(257, 175)
        Me.button7.Name = "button7"
        Me.button7.Size = New System.Drawing.Size(43, 23)
        Me.button7.TabIndex = 14
        Me.button7.Text = "Clear"
        Me.button7.UseVisualStyleBackColor = True
        '
        'Button_BlockErase
        '
        Me.Button_BlockErase.Location = New System.Drawing.Point(173, 175)
        Me.Button_BlockErase.Name = "Button_BlockErase"
        Me.Button_BlockErase.Size = New System.Drawing.Size(79, 23)
        Me.Button_BlockErase.TabIndex = 13
        Me.Button_BlockErase.Text = "Block Erase"
        Me.Button_BlockErase.UseVisualStyleBackColor = True
        '
        'Button_DataWrite
        '
        Me.Button_DataWrite.Location = New System.Drawing.Point(45, 175)
        Me.Button_DataWrite.Name = "Button_DataWrite"
        Me.Button_DataWrite.Size = New System.Drawing.Size(43, 23)
        Me.Button_DataWrite.TabIndex = 12
        Me.Button_DataWrite.Text = "Write"
        Me.Button_DataWrite.UseVisualStyleBackColor = True
        '
        'SpeedButton_Read_G2
        '
        Me.SpeedButton_Read_G2.Location = New System.Drawing.Point(4, 175)
        Me.SpeedButton_Read_G2.Name = "SpeedButton_Read_G2"
        Me.SpeedButton_Read_G2.Size = New System.Drawing.Size(37, 23)
        Me.SpeedButton_Read_G2.TabIndex = 11
        Me.SpeedButton_Read_G2.Text = "Read"
        Me.SpeedButton_Read_G2.UseVisualStyleBackColor = True
        '
        'Edit_WriteData
        '
        Me.Edit_WriteData.Location = New System.Drawing.Point(116, 152)
        Me.Edit_WriteData.Name = "Edit_WriteData"
        Me.Edit_WriteData.Size = New System.Drawing.Size(184, 21)
        Me.Edit_WriteData.TabIndex = 10
        Me.Edit_WriteData.Text = "0000"
        '
        'Edit_AccessCode2
        '
        Me.Edit_AccessCode2.Location = New System.Drawing.Point(155, 126)
        Me.Edit_AccessCode2.MaxLength = 8
        Me.Edit_AccessCode2.Name = "Edit_AccessCode2"
        Me.Edit_AccessCode2.Size = New System.Drawing.Size(145, 21)
        Me.Edit_AccessCode2.TabIndex = 9
        Me.Edit_AccessCode2.Text = "00000000"
        '
        'textBox1
        '
        Me.textBox1.Location = New System.Drawing.Point(205, 99)
        Me.textBox1.MaxLength = 3
        Me.textBox1.Name = "textBox1"
        Me.textBox1.Size = New System.Drawing.Size(95, 21)
        Me.textBox1.TabIndex = 8
        Me.textBox1.Text = "4"
        '
        'Edit_WordPtr
        '
        Me.Edit_WordPtr.Location = New System.Drawing.Point(205, 68)
        Me.Edit_WordPtr.MaxLength = 2
        Me.Edit_WordPtr.Name = "Edit_WordPtr"
        Me.Edit_WordPtr.Size = New System.Drawing.Size(95, 21)
        Me.Edit_WordPtr.TabIndex = 7
        Me.Edit_WordPtr.Text = "00"
        '
        'listBox1
        '
        Me.listBox1.FormattingEnabled = True
        Me.listBox1.ItemHeight = 12
        Me.listBox1.Location = New System.Drawing.Point(307, 38)
        Me.listBox1.Name = "listBox1"
        Me.listBox1.Size = New System.Drawing.Size(164, 160)
        Me.listBox1.TabIndex = 6
        '
        'label21
        '
        Me.label21.AutoSize = True
        Me.label21.Location = New System.Drawing.Point(8, 157)
        Me.label21.Name = "label21"
        Me.label21.Size = New System.Drawing.Size(107, 12)
        Me.label21.TabIndex = 5
        Me.label21.Text = "Write Data (Hex):"
        '
        'label20
        '
        Me.label20.AutoSize = True
        Me.label20.Location = New System.Drawing.Point(8, 129)
        Me.label20.Name = "label20"
        Me.label20.Size = New System.Drawing.Size(149, 12)
        Me.label20.TabIndex = 4
        Me.label20.Text = "Access Password (8 Hex):"
        '
        'label19
        '
        Me.label19.AutoSize = True
        Me.label19.Location = New System.Drawing.Point(8, 98)
        Me.label19.Name = "label19"
        Me.label19.Size = New System.Drawing.Size(197, 24)
        Me.label19.TabIndex = 3
        Me.label19.Text = "Length of Data(Read/Block Erase)" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(0-120/Word/D):"
        '
        'label18
        '
        Me.label18.AutoSize = True
        Me.label18.Location = New System.Drawing.Point(8, 77)
        Me.label18.Name = "label18"
        Me.label18.Size = New System.Drawing.Size(185, 12)
        Me.label18.TabIndex = 2
        Me.label18.Text = "Address of Tag Data(Word/Hex):"
        '
        'groupBox6
        '
        Me.groupBox6.Controls.Add(Me.C_User)
        Me.groupBox6.Controls.Add(Me.C_TID)
        Me.groupBox6.Controls.Add(Me.C_EPC)
        Me.groupBox6.Controls.Add(Me.C_Reserve)
        Me.groupBox6.Location = New System.Drawing.Point(6, 31)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(294, 31)
        Me.groupBox6.TabIndex = 1
        Me.groupBox6.TabStop = False
        '
        'C_User
        '
        Me.C_User.AutoSize = True
        Me.C_User.Location = New System.Drawing.Point(207, 11)
        Me.C_User.Name = "C_User"
        Me.C_User.Size = New System.Drawing.Size(47, 16)
        Me.C_User.TabIndex = 3
        Me.C_User.TabStop = True
        Me.C_User.Text = "User"
        Me.C_User.UseVisualStyleBackColor = True
        '
        'C_TID
        '
        Me.C_TID.AutoSize = True
        Me.C_TID.Location = New System.Drawing.Point(149, 11)
        Me.C_TID.Name = "C_TID"
        Me.C_TID.Size = New System.Drawing.Size(41, 16)
        Me.C_TID.TabIndex = 2
        Me.C_TID.TabStop = True
        Me.C_TID.Text = "TID"
        Me.C_TID.UseVisualStyleBackColor = True
        '
        'C_EPC
        '
        Me.C_EPC.AutoSize = True
        Me.C_EPC.Location = New System.Drawing.Point(90, 11)
        Me.C_EPC.Name = "C_EPC"
        Me.C_EPC.Size = New System.Drawing.Size(41, 16)
        Me.C_EPC.TabIndex = 1
        Me.C_EPC.TabStop = True
        Me.C_EPC.Text = "EPC"
        Me.C_EPC.UseVisualStyleBackColor = True
        '
        'C_Reserve
        '
        Me.C_Reserve.AutoSize = True
        Me.C_Reserve.Location = New System.Drawing.Point(4, 11)
        Me.C_Reserve.Name = "C_Reserve"
        Me.C_Reserve.Size = New System.Drawing.Size(71, 16)
        Me.C_Reserve.TabIndex = 0
        Me.C_Reserve.TabStop = True
        Me.C_Reserve.Text = "Password"
        Me.C_Reserve.UseVisualStyleBackColor = True
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.ListView1_EPC)
        Me.groupBox4.Location = New System.Drawing.Point(1, 0)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(480, 162)
        Me.groupBox4.TabIndex = 0
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "List EPC of Tags"
        '
        'ListView1_EPC
        '
        Me.ListView1_EPC.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress
        Me.ListView1_EPC.AutoArrange = False
        Me.ListView1_EPC.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ListView1_EPC.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.listViewCol_Number, Me.listViewCol_ID, Me.listViewCol_Length, Me.listViewCol_Times})
        Me.ListView1_EPC.Dock = System.Windows.Forms.DockStyle.Top
        Me.ListView1_EPC.FullRowSelect = True
        Me.ListView1_EPC.GridLines = True
        Me.ListView1_EPC.Location = New System.Drawing.Point(3, 17)
        Me.ListView1_EPC.Name = "ListView1_EPC"
        Me.ListView1_EPC.Size = New System.Drawing.Size(474, 138)
        Me.ListView1_EPC.TabIndex = 1
        Me.ListView1_EPC.UseCompatibleStateImageBehavior = False
        Me.ListView1_EPC.View = System.Windows.Forms.View.Details
        '
        'listViewCol_Number
        '
        Me.listViewCol_Number.Text = "No."
        Me.listViewCol_Number.Width = 40
        '
        'listViewCol_ID
        '
        Me.listViewCol_ID.Text = "ID"
        Me.listViewCol_ID.Width = 150
        '
        'listViewCol_Length
        '
        Me.listViewCol_Length.Text = "EPCLength"
        Me.listViewCol_Length.Width = 150
        '
        'listViewCol_Times
        '
        Me.listViewCol_Times.Text = "Times"
        '
        'TabSheet_6B
        '
        Me.TabSheet_6B.Controls.Add(Me.groupBox22)
        Me.TabSheet_6B.Controls.Add(Me.groupBox21)
        Me.TabSheet_6B.Controls.Add(Me.groupBox20)
        Me.TabSheet_6B.Controls.Add(Me.groupBox19)
        Me.TabSheet_6B.Location = New System.Drawing.Point(4, 21)
        Me.TabSheet_6B.Name = "TabSheet_6B"
        Me.TabSheet_6B.Size = New System.Drawing.Size(817, 625)
        Me.TabSheet_6B.TabIndex = 3
        Me.TabSheet_6B.Text = "18000-6B Test"
        Me.TabSheet_6B.UseVisualStyleBackColor = True
        '
        'groupBox22
        '
        Me.groupBox22.Controls.Add(Me.Edit_WriteData_6B)
        Me.groupBox22.Controls.Add(Me.label36)
        Me.groupBox22.Controls.Add(Me.listBox2)
        Me.groupBox22.Controls.Add(Me.Button22)
        Me.groupBox22.Controls.Add(Me.Button15)
        Me.groupBox22.Controls.Add(Me.Button14)
        Me.groupBox22.Controls.Add(Me.SpeedButton_Write_6B)
        Me.groupBox22.Controls.Add(Me.SpeedButton_Read_6B)
        Me.groupBox22.Controls.Add(Me.Edit_Len_6B)
        Me.groupBox22.Controls.Add(Me.label35)
        Me.groupBox22.Controls.Add(Me.Edit_StartAddress_6B)
        Me.groupBox22.Controls.Add(Me.label34)
        Me.groupBox22.Controls.Add(Me.ComboBox_ID1_6B)
        Me.groupBox22.Location = New System.Drawing.Point(328, 314)
        Me.groupBox22.Name = "groupBox22"
        Me.groupBox22.Size = New System.Drawing.Size(486, 308)
        Me.groupBox22.TabIndex = 3
        Me.groupBox22.TabStop = False
        Me.groupBox22.Text = "Read and Write Data Block / Permanently Write  Protect Block of  byte"
        '
        'Edit_WriteData_6B
        '
        Me.Edit_WriteData_6B.Location = New System.Drawing.Point(179, 85)
        Me.Edit_WriteData_6B.Name = "Edit_WriteData_6B"
        Me.Edit_WriteData_6B.Size = New System.Drawing.Size(301, 21)
        Me.Edit_WriteData_6B.TabIndex = 12
        Me.Edit_WriteData_6B.Text = "0000"
        '
        'label36
        '
        Me.label36.AutoSize = True
        Me.label36.Location = New System.Drawing.Point(6, 88)
        Me.label36.Name = "label36"
        Me.label36.Size = New System.Drawing.Size(167, 12)
        Me.label36.TabIndex = 11
        Me.label36.Text = "Write Data (1-32 Byte/Hex):"
        '
        'listBox2
        '
        Me.listBox2.FormattingEnabled = True
        Me.listBox2.ItemHeight = 12
        Me.listBox2.Location = New System.Drawing.Point(6, 143)
        Me.listBox2.Name = "listBox2"
        Me.listBox2.Size = New System.Drawing.Size(474, 160)
        Me.listBox2.TabIndex = 10
        '
        'Button22
        '
        Me.Button22.Location = New System.Drawing.Point(429, 114)
        Me.Button22.Name = "Button22"
        Me.Button22.Size = New System.Drawing.Size(51, 23)
        Me.Button22.TabIndex = 9
        Me.Button22.Text = "Clear"
        Me.Button22.UseVisualStyleBackColor = True
        '
        'Button15
        '
        Me.Button15.Location = New System.Drawing.Point(348, 114)
        Me.Button15.Name = "Button15"
        Me.Button15.Size = New System.Drawing.Size(75, 23)
        Me.Button15.TabIndex = 8
        Me.Button15.Text = "Check Protect"
        Me.Button15.UseVisualStyleBackColor = True
        '
        'Button14
        '
        Me.Button14.Location = New System.Drawing.Point(170, 114)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(172, 23)
        Me.Button14.TabIndex = 7
        Me.Button14.Text = "Permanently Write  Protect"
        Me.Button14.UseVisualStyleBackColor = True
        '
        'SpeedButton_Write_6B
        '
        Me.SpeedButton_Write_6B.Location = New System.Drawing.Point(89, 114)
        Me.SpeedButton_Write_6B.Name = "SpeedButton_Write_6B"
        Me.SpeedButton_Write_6B.Size = New System.Drawing.Size(75, 23)
        Me.SpeedButton_Write_6B.TabIndex = 6
        Me.SpeedButton_Write_6B.Text = "Write"
        Me.SpeedButton_Write_6B.UseVisualStyleBackColor = True
        '
        'SpeedButton_Read_6B
        '
        Me.SpeedButton_Read_6B.Location = New System.Drawing.Point(8, 114)
        Me.SpeedButton_Read_6B.Name = "SpeedButton_Read_6B"
        Me.SpeedButton_Read_6B.Size = New System.Drawing.Size(75, 23)
        Me.SpeedButton_Read_6B.TabIndex = 5
        Me.SpeedButton_Read_6B.Text = "Read"
        Me.SpeedButton_Read_6B.UseVisualStyleBackColor = True
        '
        'Edit_Len_6B
        '
        Me.Edit_Len_6B.Location = New System.Drawing.Point(382, 53)
        Me.Edit_Len_6B.MaxLength = 2
        Me.Edit_Len_6B.Name = "Edit_Len_6B"
        Me.Edit_Len_6B.Size = New System.Drawing.Size(100, 21)
        Me.Edit_Len_6B.TabIndex = 4
        Me.Edit_Len_6B.Text = "12"
        '
        'label35
        '
        Me.label35.AutoSize = True
        Me.label35.Location = New System.Drawing.Point(283, 53)
        Me.label35.Name = "label35"
        Me.label35.Size = New System.Drawing.Size(95, 24)
        Me.label35.TabIndex = 3
        Me.label35.Text = "Length of Data:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(1-32/Byte/D)"
        '
        'Edit_StartAddress_6B
        '
        Me.Edit_StartAddress_6B.Location = New System.Drawing.Point(143, 53)
        Me.Edit_StartAddress_6B.MaxLength = 2
        Me.Edit_StartAddress_6B.Name = "Edit_StartAddress_6B"
        Me.Edit_StartAddress_6B.Size = New System.Drawing.Size(100, 21)
        Me.Edit_StartAddress_6B.TabIndex = 2
        Me.Edit_StartAddress_6B.Text = "00"
        '
        'label34
        '
        Me.label34.AutoSize = True
        Me.label34.Location = New System.Drawing.Point(6, 50)
        Me.label34.Name = "label34"
        Me.label34.Size = New System.Drawing.Size(131, 24)
        Me.label34.TabIndex = 1
        Me.label34.Text = "Start/Protect Address" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(00-E9)(Hex):   "
        '
        'ComboBox_ID1_6B
        '
        Me.ComboBox_ID1_6B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_ID1_6B.FormattingEnabled = True
        Me.ComboBox_ID1_6B.Location = New System.Drawing.Point(6, 17)
        Me.ComboBox_ID1_6B.Name = "ComboBox_ID1_6B"
        Me.ComboBox_ID1_6B.Size = New System.Drawing.Size(474, 20)
        Me.ComboBox_ID1_6B.TabIndex = 0
        '
        'groupBox21
        '
        Me.groupBox21.Controls.Add(Me.Edit_ConditionContent_6B)
        Me.groupBox21.Controls.Add(Me.Edit_Query_StartAddress_6B)
        Me.groupBox21.Controls.Add(Me.label33)
        Me.groupBox21.Controls.Add(Me.label32)
        Me.groupBox21.Controls.Add(Me.Greater_6B)
        Me.groupBox21.Controls.Add(Me.Less_6B)
        Me.groupBox21.Controls.Add(Me.Different_6B)
        Me.groupBox21.Controls.Add(Me.Same_6B)
        Me.groupBox21.Location = New System.Drawing.Point(1, 447)
        Me.groupBox21.Name = "groupBox21"
        Me.groupBox21.Size = New System.Drawing.Size(321, 175)
        Me.groupBox21.TabIndex = 2
        Me.groupBox21.TabStop = False
        Me.groupBox21.Text = "Query Tags by Condition"
        '
        'Edit_ConditionContent_6B
        '
        Me.Edit_ConditionContent_6B.Location = New System.Drawing.Point(181, 144)
        Me.Edit_ConditionContent_6B.MaxLength = 8
        Me.Edit_ConditionContent_6B.Name = "Edit_ConditionContent_6B"
        Me.Edit_ConditionContent_6B.Size = New System.Drawing.Size(98, 21)
        Me.Edit_ConditionContent_6B.TabIndex = 7
        Me.Edit_ConditionContent_6B.Text = "00"
        '
        'Edit_Query_StartAddress_6B
        '
        Me.Edit_Query_StartAddress_6B.Location = New System.Drawing.Point(181, 105)
        Me.Edit_Query_StartAddress_6B.MaxLength = 2
        Me.Edit_Query_StartAddress_6B.Name = "Edit_Query_StartAddress_6B"
        Me.Edit_Query_StartAddress_6B.Size = New System.Drawing.Size(98, 21)
        Me.Edit_Query_StartAddress_6B.TabIndex = 6
        Me.Edit_Query_StartAddress_6B.Text = "0"
        '
        'label33
        '
        Me.label33.AutoSize = True
        Me.label33.Location = New System.Drawing.Point(9, 147)
        Me.label33.Name = "label33"
        Me.label33.Size = New System.Drawing.Size(161, 12)
        Me.label33.TabIndex = 5
        Me.label33.Text = "Condition(<=8 Hex Number):"
        '
        'label32
        '
        Me.label32.AutoSize = True
        Me.label32.Location = New System.Drawing.Point(8, 108)
        Me.label32.Name = "label32"
        Me.label32.Size = New System.Drawing.Size(167, 12)
        Me.label32.TabIndex = 4
        Me.label32.Text = "Address of Tag Data(0-223):"
        '
        'Greater_6B
        '
        Me.Greater_6B.AutoSize = True
        Me.Greater_6B.Location = New System.Drawing.Point(163, 68)
        Me.Greater_6B.Name = "Greater_6B"
        Me.Greater_6B.Size = New System.Drawing.Size(155, 16)
        Me.Greater_6B.TabIndex = 3
        Me.Greater_6B.TabStop = True
        Me.Greater_6B.Text = "Greater than Condition"
        Me.Greater_6B.UseVisualStyleBackColor = True
        '
        'Less_6B
        '
        Me.Less_6B.AutoSize = True
        Me.Less_6B.Location = New System.Drawing.Point(8, 68)
        Me.Less_6B.Name = "Less_6B"
        Me.Less_6B.Size = New System.Drawing.Size(137, 16)
        Me.Less_6B.TabIndex = 2
        Me.Less_6B.TabStop = True
        Me.Less_6B.Text = "Less than Condition"
        Me.Less_6B.UseVisualStyleBackColor = True
        '
        'Different_6B
        '
        Me.Different_6B.AutoSize = True
        Me.Different_6B.Location = New System.Drawing.Point(163, 30)
        Me.Different_6B.Name = "Different_6B"
        Me.Different_6B.Size = New System.Drawing.Size(125, 16)
        Me.Different_6B.TabIndex = 1
        Me.Different_6B.TabStop = True
        Me.Different_6B.Text = "Unequal Condition"
        Me.Different_6B.UseVisualStyleBackColor = True
        '
        'Same_6B
        '
        Me.Same_6B.AutoSize = True
        Me.Same_6B.Location = New System.Drawing.Point(8, 30)
        Me.Same_6B.Name = "Same_6B"
        Me.Same_6B.Size = New System.Drawing.Size(113, 16)
        Me.Same_6B.TabIndex = 0
        Me.Same_6B.TabStop = True
        Me.Same_6B.Text = "Equal Condition"
        Me.Same_6B.UseVisualStyleBackColor = True
        '
        'groupBox20
        '
        Me.groupBox20.Controls.Add(Me.SpeedButton_Query_6B)
        Me.groupBox20.Controls.Add(Me.Bycondition_6B)
        Me.groupBox20.Controls.Add(Me.Byone_6B)
        Me.groupBox20.Controls.Add(Me.ComboBox_IntervalTime_6B)
        Me.groupBox20.Controls.Add(Me.label31)
        Me.groupBox20.Location = New System.Drawing.Point(1, 314)
        Me.groupBox20.Name = "groupBox20"
        Me.groupBox20.Size = New System.Drawing.Size(321, 132)
        Me.groupBox20.TabIndex = 1
        Me.groupBox20.TabStop = False
        Me.groupBox20.Text = "Query Tag"
        '
        'SpeedButton_Query_6B
        '
        Me.SpeedButton_Query_6B.Enabled = False
        Me.SpeedButton_Query_6B.Location = New System.Drawing.Point(213, 64)
        Me.SpeedButton_Query_6B.Name = "SpeedButton_Query_6B"
        Me.SpeedButton_Query_6B.Size = New System.Drawing.Size(102, 49)
        Me.SpeedButton_Query_6B.TabIndex = 4
        Me.SpeedButton_Query_6B.Text = "Query"
        Me.SpeedButton_Query_6B.UseVisualStyleBackColor = True
        '
        'Bycondition_6B
        '
        Me.Bycondition_6B.AutoSize = True
        Me.Bycondition_6B.Location = New System.Drawing.Point(8, 97)
        Me.Bycondition_6B.Name = "Bycondition_6B"
        Me.Bycondition_6B.Size = New System.Drawing.Size(131, 16)
        Me.Bycondition_6B.TabIndex = 3
        Me.Bycondition_6B.TabStop = True
        Me.Bycondition_6B.Text = "Query by Condition"
        Me.Bycondition_6B.UseVisualStyleBackColor = True
        '
        'Byone_6B
        '
        Me.Byone_6B.AutoSize = True
        Me.Byone_6B.Location = New System.Drawing.Point(8, 64)
        Me.Byone_6B.Name = "Byone_6B"
        Me.Byone_6B.Size = New System.Drawing.Size(95, 16)
        Me.Byone_6B.TabIndex = 2
        Me.Byone_6B.TabStop = True
        Me.Byone_6B.Text = "Query by one"
        Me.Byone_6B.UseVisualStyleBackColor = True
        '
        'ComboBox_IntervalTime_6B
        '
        Me.ComboBox_IntervalTime_6B.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox_IntervalTime_6B.FormattingEnabled = True
        Me.ComboBox_IntervalTime_6B.Location = New System.Drawing.Point(101, 14)
        Me.ComboBox_IntervalTime_6B.Name = "ComboBox_IntervalTime_6B"
        Me.ComboBox_IntervalTime_6B.Size = New System.Drawing.Size(214, 20)
        Me.ComboBox_IntervalTime_6B.TabIndex = 1
        '
        'label31
        '
        Me.label31.AutoSize = True
        Me.label31.Location = New System.Drawing.Point(6, 17)
        Me.label31.Name = "label31"
        Me.label31.Size = New System.Drawing.Size(89, 12)
        Me.label31.TabIndex = 0
        Me.label31.Text = "Read Interval:"
        '
        'groupBox19
        '
        Me.groupBox19.Controls.Add(Me.ListView_ID_6B)
        Me.groupBox19.Location = New System.Drawing.Point(1, 3)
        Me.groupBox19.Name = "groupBox19"
        Me.groupBox19.Size = New System.Drawing.Size(813, 309)
        Me.groupBox19.TabIndex = 0
        Me.groupBox19.TabStop = False
        Me.groupBox19.Text = "List ID of Tags"
        '
        'ListView_ID_6B
        '
        Me.ListView_ID_6B.Activation = System.Windows.Forms.ItemActivation.OneClick
        Me.ListView_ID_6B.AllowDrop = True
        Me.ListView_ID_6B.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.columnHeader5, Me.columnHeader6, Me.columnHeader7})
        Me.ListView_ID_6B.FullRowSelect = True
        Me.ListView_ID_6B.GridLines = True
        Me.ListView_ID_6B.HotTracking = True
        Me.ListView_ID_6B.HoverSelection = True
        Me.ListView_ID_6B.Location = New System.Drawing.Point(6, 20)
        Me.ListView_ID_6B.Name = "ListView_ID_6B"
        Me.ListView_ID_6B.Size = New System.Drawing.Size(801, 283)
        Me.ListView_ID_6B.TabIndex = 0
        Me.ListView_ID_6B.UseCompatibleStateImageBehavior = False
        Me.ListView_ID_6B.View = System.Windows.Forms.View.Details
        '
        'columnHeader5
        '
        Me.columnHeader5.Text = "No."
        Me.columnHeader5.Width = 50
        '
        'columnHeader6
        '
        Me.columnHeader6.Text = "ID"
        Me.columnHeader6.Width = 600
        '
        'columnHeader7
        '
        Me.columnHeader7.Text = "Times"
        Me.columnHeader7.Width = 90
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.button19)
        Me.TabPage1.Controls.Add(Me.button18)
        Me.TabPage1.Controls.Add(Me.comboBox8)
        Me.TabPage1.Controls.Add(Me.label49)
        Me.TabPage1.Controls.Add(Me.button17)
        Me.TabPage1.Controls.Add(Me.button16)
        Me.TabPage1.Controls.Add(Me.button13)
        Me.TabPage1.Controls.Add(Me.listBox4)
        Me.TabPage1.Controls.Add(Me.label52)
        Me.TabPage1.Controls.Add(Me.label51)
        Me.TabPage1.Controls.Add(Me.label50)
        Me.TabPage1.Location = New System.Drawing.Point(4, 21)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(817, 625)
        Me.TabPage1.TabIndex = 4
        Me.TabPage1.Text = "Frequency Analysis"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'button19
        '
        Me.button19.Location = New System.Drawing.Point(343, 590)
        Me.button19.Name = "button19"
        Me.button19.Size = New System.Drawing.Size(49, 23)
        Me.button19.TabIndex = 25
        Me.button19.Text = "Get"
        Me.button19.UseVisualStyleBackColor = True
        '
        'button18
        '
        Me.button18.Location = New System.Drawing.Point(286, 590)
        Me.button18.Name = "button18"
        Me.button18.Size = New System.Drawing.Size(51, 23)
        Me.button18.TabIndex = 24
        Me.button18.Text = "Set"
        Me.button18.UseVisualStyleBackColor = True
        '
        'comboBox8
        '
        Me.comboBox8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.comboBox8.FormattingEnabled = True
        Me.comboBox8.Items.AddRange(New Object() {"Random", "Adaptive"})
        Me.comboBox8.Location = New System.Drawing.Point(157, 593)
        Me.comboBox8.Name = "comboBox8"
        Me.comboBox8.Size = New System.Drawing.Size(123, 20)
        Me.comboBox8.TabIndex = 23
        '
        'label49
        '
        Me.label49.AutoSize = True
        Me.label49.Location = New System.Drawing.Point(13, 595)
        Me.label49.Name = "label49"
        Me.label49.Size = New System.Drawing.Size(149, 12)
        Me.label49.TabIndex = 22
        Me.label49.Text = "Frequency hopping mode："
        '
        'button17
        '
        Me.button17.Location = New System.Drawing.Point(727, 590)
        Me.button17.Name = "button17"
        Me.button17.Size = New System.Drawing.Size(75, 23)
        Me.button17.TabIndex = 14
        Me.button17.Text = "Clear"
        Me.button17.UseVisualStyleBackColor = True
        '
        'button16
        '
        Me.button16.Location = New System.Drawing.Point(632, 590)
        Me.button16.Name = "button16"
        Me.button16.Size = New System.Drawing.Size(75, 23)
        Me.button16.TabIndex = 13
        Me.button16.Text = "Stop"
        Me.button16.UseVisualStyleBackColor = True
        '
        'button13
        '
        Me.button13.Location = New System.Drawing.Point(537, 590)
        Me.button13.Name = "button13"
        Me.button13.Size = New System.Drawing.Size(75, 23)
        Me.button13.TabIndex = 12
        Me.button13.Text = "Start"
        Me.button13.UseVisualStyleBackColor = True
        '
        'listBox4
        '
        Me.listBox4.FormattingEnabled = True
        Me.listBox4.ItemHeight = 12
        Me.listBox4.Location = New System.Drawing.Point(15, 27)
        Me.listBox4.Name = "listBox4"
        Me.listBox4.Size = New System.Drawing.Size(787, 556)
        Me.listBox4.TabIndex = 11
        '
        'label52
        '
        Me.label52.AutoSize = True
        Me.label52.Location = New System.Drawing.Point(447, 12)
        Me.label52.Name = "label52"
        Me.label52.Size = New System.Drawing.Size(65, 12)
        Me.label52.TabIndex = 10
        Me.label52.Text = "Percentage"
        '
        'label51
        '
        Me.label51.AutoSize = True
        Me.label51.Location = New System.Drawing.Point(236, 12)
        Me.label51.Name = "label51"
        Me.label51.Size = New System.Drawing.Size(35, 12)
        Me.label51.TabIndex = 9
        Me.label51.Text = "Times"
        '
        'label50
        '
        Me.label50.AutoSize = True
        Me.label50.Location = New System.Drawing.Point(20, 12)
        Me.label50.Name = "label50"
        Me.label50.Size = New System.Drawing.Size(59, 12)
        Me.label50.TabIndex = 8
        Me.label50.Text = "Frequency"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(829, 673)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.StatusBar1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "UHFReader18 VB.net V2.1"
        CType(Me.TStatusPanel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Port, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Manufacturername, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl1.ResumeLayout(False)
        Me.TabSheet_CMD.ResumeLayout(False)
        Me.groupBox23.ResumeLayout(False)
        Me.groupBox26.ResumeLayout(False)
        Me.groupBox26.PerformLayout()
        Me.GroupBox32.ResumeLayout(False)
        Me.GroupBox32.PerformLayout()
        Me.groupBox29.ResumeLayout(False)
        Me.groupBox29.PerformLayout()
        Me.groupBox28.ResumeLayout(False)
        Me.groupBox28.PerformLayout()
        Me.groupBox27.ResumeLayout(False)
        Me.groupBox27.PerformLayout()
        Me.groupBox24.ResumeLayout(False)
        Me.groupBox24.PerformLayout()
        Me.groupBox25.ResumeLayout(False)
        Me.groupBox25.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.groupBox30.ResumeLayout(False)
        Me.groupBox30.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabSheet_EPCC1G2.ResumeLayout(False)
        Me.groupBox31.ResumeLayout(False)
        Me.groupBox31.PerformLayout()
        Me.groupBox18.ResumeLayout(False)
        Me.groupBox18.PerformLayout()
        Me.groupBox16.ResumeLayout(False)
        Me.groupBox16.PerformLayout()
        Me.groupBox17.ResumeLayout(False)
        Me.groupBox17.PerformLayout()
        Me.groupBox15.ResumeLayout(False)
        Me.groupBox15.PerformLayout()
        Me.groupBox14.ResumeLayout(False)
        Me.groupBox14.PerformLayout()
        Me.groupBox13.ResumeLayout(False)
        Me.groupBox13.PerformLayout()
        Me.groupBox12.ResumeLayout(False)
        Me.groupBox12.PerformLayout()
        Me.groupBox33.ResumeLayout(False)
        Me.groupBox33.PerformLayout()
        Me.groupBox7.ResumeLayout(False)
        Me.groupBox7.PerformLayout()
        Me.groupBox11.ResumeLayout(False)
        Me.groupBox11.PerformLayout()
        Me.groupBox10.ResumeLayout(False)
        Me.groupBox10.PerformLayout()
        Me.groupBox8.ResumeLayout(False)
        Me.groupBox8.PerformLayout()
        Me.groupBox9.ResumeLayout(False)
        Me.groupBox9.PerformLayout()
        Me.groupBox5.ResumeLayout(False)
        Me.groupBox5.PerformLayout()
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox6.PerformLayout()
        Me.groupBox4.ResumeLayout(False)
        Me.TabSheet_6B.ResumeLayout(False)
        Me.groupBox22.ResumeLayout(False)
        Me.groupBox22.PerformLayout()
        Me.groupBox21.ResumeLayout(False)
        Me.groupBox21.PerformLayout()
        Me.groupBox20.ResumeLayout(False)
        Me.groupBox20.PerformLayout()
        Me.groupBox19.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    Private WithEvents TStatusPanel As System.Windows.Forms.StatusBarPanel
    Private WithEvents Port As System.Windows.Forms.StatusBarPanel
    Private WithEvents Manufacturername As System.Windows.Forms.StatusBarPanel
    Private WithEvents Timer_Test_ As System.Windows.Forms.Timer
    Private WithEvents Timer_G2_Read As System.Windows.Forms.Timer
    Private WithEvents Timer_G2_Alarm As System.Windows.Forms.Timer
    Private WithEvents timer1 As System.Windows.Forms.Timer
    Private WithEvents Timer_Test_6B As System.Windows.Forms.Timer
    Private WithEvents Timer_6B_Read As System.Windows.Forms.Timer
    Private WithEvents Timer_6B_Write As System.Windows.Forms.Timer
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl
    Private WithEvents TabSheet_CMD As System.Windows.Forms.TabPage
    Private WithEvents button11 As System.Windows.Forms.Button
    Private WithEvents button10 As System.Windows.Forms.Button
    Private WithEvents listBox3 As System.Windows.Forms.ListBox
    Private WithEvents groupBox23 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox26 As System.Windows.Forms.GroupBox
    Private WithEvents textBox3 As System.Windows.Forms.TextBox
    Private WithEvents button8 As System.Windows.Forms.Button
    Private WithEvents comboBox5 As System.Windows.Forms.ComboBox
    Private WithEvents label42 As System.Windows.Forms.Label
    Private WithEvents label41 As System.Windows.Forms.Label
    Private WithEvents comboBox4 As System.Windows.Forms.ComboBox
    Private WithEvents label40 As System.Windows.Forms.Label
    Private WithEvents groupBox29 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton15 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton14 As System.Windows.Forms.RadioButton
    Private WithEvents groupBox28 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton13 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton12 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton11 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton10 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton9 As System.Windows.Forms.RadioButton
    Private WithEvents groupBox27 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton8 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton7 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton6 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton5 As System.Windows.Forms.RadioButton
    Private WithEvents groupBox24 As System.Windows.Forms.GroupBox
    Private WithEvents button6 As System.Windows.Forms.Button
    Private WithEvents comboBox3 As System.Windows.Forms.ComboBox
    Private WithEvents label39 As System.Windows.Forms.Label
    Private WithEvents comboBox2 As System.Windows.Forms.ComboBox
    Private WithEvents comboBox1 As System.Windows.Forms.ComboBox
    Private WithEvents label38 As System.Windows.Forms.Label
    Private WithEvents label37 As System.Windows.Forms.Label
    Private WithEvents groupBox25 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton4 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton3 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton2 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton1 As System.Windows.Forms.RadioButton
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents groupBox30 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton_band4 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton_band3 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton_band2 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton_band1 As System.Windows.Forms.RadioButton
    Private WithEvents progressBar1 As System.Windows.Forms.ProgressBar
    Private WithEvents Button1 As System.Windows.Forms.Button
    Private WithEvents Button5 As System.Windows.Forms.Button
    Private WithEvents ComboBox_scantime As System.Windows.Forms.ComboBox
    Private WithEvents ComboBox_baud As System.Windows.Forms.ComboBox
    Private WithEvents CheckBox_SameFre As System.Windows.Forms.CheckBox
    Private WithEvents label17 As System.Windows.Forms.Label
    Private WithEvents label16 As System.Windows.Forms.Label
    Private WithEvents ComboBox_dmaxfre As System.Windows.Forms.ComboBox
    Private WithEvents ComboBox_dminfre As System.Windows.Forms.ComboBox
    Private WithEvents ComboBox_PowerDbm As System.Windows.Forms.ComboBox
    Private WithEvents Edit_NewComAdr As System.Windows.Forms.TextBox
    Private WithEvents label15 As System.Windows.Forms.Label
    Private WithEvents label14 As System.Windows.Forms.Label
    Private WithEvents label13 As System.Windows.Forms.Label
    Private WithEvents label12 As System.Windows.Forms.Label
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents Button3 As System.Windows.Forms.Button
    Private WithEvents Edit_scantime As System.Windows.Forms.TextBox
    Private WithEvents EPCC1G2 As System.Windows.Forms.CheckBox
    Private WithEvents ISO180006B As System.Windows.Forms.CheckBox
    Private WithEvents label11 As System.Windows.Forms.Label
    Private WithEvents label10 As System.Windows.Forms.Label
    Private WithEvents Edit_dmaxfre As System.Windows.Forms.TextBox
    Private WithEvents Edit_powerdBm As System.Windows.Forms.TextBox
    Private WithEvents Edit_Version As System.Windows.Forms.TextBox
    Private WithEvents label9 As System.Windows.Forms.Label
    Private WithEvents label8 As System.Windows.Forms.Label
    Private WithEvents label7 As System.Windows.Forms.Label
    Private WithEvents Edit_dminfre As System.Windows.Forms.TextBox
    Private WithEvents Edit_ComAdr As System.Windows.Forms.TextBox
    Private WithEvents Edit_Type As System.Windows.Forms.TextBox
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents ComboBox_AlreadyOpenCOM As System.Windows.Forms.ComboBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents ClosePort As System.Windows.Forms.Button
    Friend WithEvents OpenPort As System.Windows.Forms.Button
    Friend WithEvents Edit_CmdComAddr As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox_COM As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents TabSheet_EPCC1G2 As System.Windows.Forms.TabPage
    Private WithEvents groupBox31 As System.Windows.Forms.GroupBox
    Private WithEvents maskLen_textBox As System.Windows.Forms.TextBox
    Private WithEvents label44 As System.Windows.Forms.Label
    Private WithEvents maskadr_textbox As System.Windows.Forms.TextBox
    Private WithEvents label43 As System.Windows.Forms.Label
    Private WithEvents checkBox1 As System.Windows.Forms.CheckBox
    Private WithEvents groupBox18 As System.Windows.Forms.GroupBox
    Private WithEvents Button_LockUserBlock_G2 As System.Windows.Forms.Button
    Private WithEvents Edit_AccessCode6 As System.Windows.Forms.TextBox
    Private WithEvents ComboBox_BlockNum As System.Windows.Forms.ComboBox
    Private WithEvents label30 As System.Windows.Forms.Label
    Private WithEvents label29 As System.Windows.Forms.Label
    Private WithEvents ComboBox_EPC6 As System.Windows.Forms.ComboBox
    Private WithEvents groupBox16 As System.Windows.Forms.GroupBox
    Private WithEvents Label_Alarm As System.Windows.Forms.Label
    Private WithEvents button4 As System.Windows.Forms.Button
    Private WithEvents Button_SetEASAlarm_G2 As System.Windows.Forms.Button
    Private WithEvents groupBox17 As System.Windows.Forms.GroupBox
    Private WithEvents NoAlarm_G2 As System.Windows.Forms.RadioButton
    Private WithEvents Alarm_G2 As System.Windows.Forms.RadioButton
    Private WithEvents Edit_AccessCode5 As System.Windows.Forms.TextBox
    Private WithEvents label28 As System.Windows.Forms.Label
    Private WithEvents ComboBox_EPC5 As System.Windows.Forms.ComboBox
    Private WithEvents groupBox15 As System.Windows.Forms.GroupBox
    Private WithEvents Button_CheckReadProtected_G2 As System.Windows.Forms.Button
    Private WithEvents Button_RemoveReadProtect_G2 As System.Windows.Forms.Button
    Private WithEvents Button_SetMultiReadProtect_G2 As System.Windows.Forms.Button
    Private WithEvents Button_SetReadProtect_G2 As System.Windows.Forms.Button
    Private WithEvents Edit_AccessCode4 As System.Windows.Forms.TextBox
    Private WithEvents label27 As System.Windows.Forms.Label
    Private WithEvents ComboBox_EPC4 As System.Windows.Forms.ComboBox
    Private WithEvents groupBox14 As System.Windows.Forms.GroupBox
    Private WithEvents Button_WriteEPC_G2 As System.Windows.Forms.Button
    Private WithEvents Edit_AccessCode3 As System.Windows.Forms.TextBox
    Private WithEvents label26 As System.Windows.Forms.Label
    Private WithEvents Edit_WriteEPC As System.Windows.Forms.TextBox
    Private WithEvents label25 As System.Windows.Forms.Label
    Private WithEvents groupBox13 As System.Windows.Forms.GroupBox
    Private WithEvents Button_DestroyCard As System.Windows.Forms.Button
    Private WithEvents Edit_DestroyCode As System.Windows.Forms.TextBox
    Private WithEvents label24 As System.Windows.Forms.Label
    Private WithEvents ComboBox_EPC3 As System.Windows.Forms.ComboBox
    Private WithEvents groupBox12 As System.Windows.Forms.GroupBox
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents ComboBox_IntervalTime As System.Windows.Forms.ComboBox
    Private WithEvents label23 As System.Windows.Forms.Label
    Private WithEvents groupBox7 As System.Windows.Forms.GroupBox
    Private WithEvents Button_SetProtectState As System.Windows.Forms.Button
    Private WithEvents textBox2 As System.Windows.Forms.TextBox
    Private WithEvents label22 As System.Windows.Forms.Label
    Private WithEvents groupBox11 As System.Windows.Forms.GroupBox
    Private WithEvents AlwaysNot2 As System.Windows.Forms.RadioButton
    Private WithEvents Always2 As System.Windows.Forms.RadioButton
    Private WithEvents Proect2 As System.Windows.Forms.RadioButton
    Private WithEvents NoProect2 As System.Windows.Forms.RadioButton
    Private WithEvents groupBox10 As System.Windows.Forms.GroupBox
    Private WithEvents P_User As System.Windows.Forms.RadioButton
    Private WithEvents P_TID As System.Windows.Forms.RadioButton
    Private WithEvents P_EPC As System.Windows.Forms.RadioButton
    Private WithEvents P_Reserve As System.Windows.Forms.RadioButton
    Private WithEvents groupBox8 As System.Windows.Forms.GroupBox
    Private WithEvents AlwaysNot As System.Windows.Forms.RadioButton
    Private WithEvents Always As System.Windows.Forms.RadioButton
    Private WithEvents Proect As System.Windows.Forms.RadioButton
    Private WithEvents NoProect As System.Windows.Forms.RadioButton
    Private WithEvents groupBox9 As System.Windows.Forms.GroupBox
    Private WithEvents AccessCode As System.Windows.Forms.RadioButton
    Private WithEvents DestroyCode As System.Windows.Forms.RadioButton
    Private WithEvents ComboBox_EPC1 As System.Windows.Forms.ComboBox
    Private WithEvents groupBox5 As System.Windows.Forms.GroupBox
    Private WithEvents ComboBox_EPC2 As System.Windows.Forms.ComboBox
    Private WithEvents button7 As System.Windows.Forms.Button
    Private WithEvents Button_BlockErase As System.Windows.Forms.Button
    Private WithEvents Button_DataWrite As System.Windows.Forms.Button
    Private WithEvents SpeedButton_Read_G2 As System.Windows.Forms.Button
    Private WithEvents Edit_WriteData As System.Windows.Forms.TextBox
    Private WithEvents Edit_AccessCode2 As System.Windows.Forms.TextBox
    Private WithEvents textBox1 As System.Windows.Forms.TextBox
    Private WithEvents Edit_WordPtr As System.Windows.Forms.TextBox
    Private WithEvents listBox1 As System.Windows.Forms.ListBox
    Private WithEvents label21 As System.Windows.Forms.Label
    Private WithEvents label20 As System.Windows.Forms.Label
    Private WithEvents label19 As System.Windows.Forms.Label
    Private WithEvents label18 As System.Windows.Forms.Label
    Private WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Private WithEvents C_User As System.Windows.Forms.RadioButton
    Private WithEvents C_TID As System.Windows.Forms.RadioButton
    Private WithEvents C_EPC As System.Windows.Forms.RadioButton
    Private WithEvents C_Reserve As System.Windows.Forms.RadioButton
    Private WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Private WithEvents ListView1_EPC As System.Windows.Forms.ListView
    Private WithEvents listViewCol_Number As System.Windows.Forms.ColumnHeader
    Private WithEvents listViewCol_ID As System.Windows.Forms.ColumnHeader
    Private WithEvents listViewCol_Length As System.Windows.Forms.ColumnHeader
    Private WithEvents listViewCol_Times As System.Windows.Forms.ColumnHeader
    Private WithEvents TabSheet_6B As System.Windows.Forms.TabPage
    Private WithEvents groupBox22 As System.Windows.Forms.GroupBox
    Private WithEvents Edit_WriteData_6B As System.Windows.Forms.TextBox
    Private WithEvents label36 As System.Windows.Forms.Label
    Private WithEvents listBox2 As System.Windows.Forms.ListBox
    Private WithEvents Button22 As System.Windows.Forms.Button
    Private WithEvents Button15 As System.Windows.Forms.Button
    Private WithEvents Button14 As System.Windows.Forms.Button
    Private WithEvents SpeedButton_Write_6B As System.Windows.Forms.Button
    Private WithEvents SpeedButton_Read_6B As System.Windows.Forms.Button
    Private WithEvents Edit_Len_6B As System.Windows.Forms.TextBox
    Private WithEvents label35 As System.Windows.Forms.Label
    Private WithEvents Edit_StartAddress_6B As System.Windows.Forms.TextBox
    Private WithEvents label34 As System.Windows.Forms.Label
    Private WithEvents ComboBox_ID1_6B As System.Windows.Forms.ComboBox
    Private WithEvents groupBox21 As System.Windows.Forms.GroupBox
    Private WithEvents Edit_ConditionContent_6B As System.Windows.Forms.TextBox
    Private WithEvents Edit_Query_StartAddress_6B As System.Windows.Forms.TextBox
    Private WithEvents label33 As System.Windows.Forms.Label
    Private WithEvents label32 As System.Windows.Forms.Label
    Private WithEvents Greater_6B As System.Windows.Forms.RadioButton
    Private WithEvents Less_6B As System.Windows.Forms.RadioButton
    Private WithEvents Different_6B As System.Windows.Forms.RadioButton
    Private WithEvents Same_6B As System.Windows.Forms.RadioButton
    Private WithEvents groupBox20 As System.Windows.Forms.GroupBox
    Private WithEvents SpeedButton_Query_6B As System.Windows.Forms.Button
    Private WithEvents Bycondition_6B As System.Windows.Forms.RadioButton
    Private WithEvents Byone_6B As System.Windows.Forms.RadioButton
    Private WithEvents ComboBox_IntervalTime_6B As System.Windows.Forms.ComboBox
    Private WithEvents label31 As System.Windows.Forms.Label
    Private WithEvents groupBox19 As System.Windows.Forms.GroupBox
    Private WithEvents ListView_ID_6B As System.Windows.Forms.ListView
    Private WithEvents columnHeader5 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader6 As System.Windows.Forms.ColumnHeader
    Private WithEvents columnHeader7 As System.Windows.Forms.ColumnHeader
    Private WithEvents GroupBox32 As System.Windows.Forms.GroupBox
    Private WithEvents RadioButton16 As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton17 As System.Windows.Forms.RadioButton
    Private WithEvents button9 As System.Windows.Forms.Button
    Private WithEvents RadioButton19 As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton18 As System.Windows.Forms.RadioButton
    Private WithEvents Button12 As System.Windows.Forms.Button
    Private WithEvents ComboBox7 As System.Windows.Forms.ComboBox
    Private WithEvents Label46 As System.Windows.Forms.Label
    Private WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Private WithEvents Label45 As System.Windows.Forms.Label
    Private WithEvents ComboBox_baud2 As System.Windows.Forms.ComboBox
    Private WithEvents label47 As System.Windows.Forms.Label
    Private WithEvents radioButton20 As System.Windows.Forms.RadioButton
    Private WithEvents button_OffsetTime As System.Windows.Forms.Button
    Private WithEvents comboBox_OffsetTime As System.Windows.Forms.ComboBox
    Private WithEvents label48 As System.Windows.Forms.Label
    Private WithEvents Button_BlockWrite As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Private WithEvents button17 As System.Windows.Forms.Button
    Private WithEvents button16 As System.Windows.Forms.Button
    Private WithEvents button13 As System.Windows.Forms.Button
    Private WithEvents listBox4 As System.Windows.Forms.ListBox
    Private WithEvents label52 As System.Windows.Forms.Label
    Private WithEvents label51 As System.Windows.Forms.Label
    Private WithEvents label50 As System.Windows.Forms.Label
    Private WithEvents button19 As System.Windows.Forms.Button
    Private WithEvents button18 As System.Windows.Forms.Button
    Private WithEvents comboBox8 As System.Windows.Forms.ComboBox
    Private WithEvents label49 As System.Windows.Forms.Label
    Private WithEvents radioButton_band5 As System.Windows.Forms.RadioButton
    Private WithEvents button_gettigtime As System.Windows.Forms.Button
    Private WithEvents button_settigtime As System.Windows.Forms.Button
    Private WithEvents comboBox_tigtime As System.Windows.Forms.ComboBox
    Private WithEvents label53 As System.Windows.Forms.Label
    Private WithEvents CheckBox_TID As System.Windows.Forms.CheckBox
    Private WithEvents groupBox33 As System.Windows.Forms.GroupBox
    Private WithEvents textBox5 As System.Windows.Forms.TextBox
    Private WithEvents label55 As System.Windows.Forms.Label
    Private WithEvents textBox4 As System.Windows.Forms.TextBox
    Private WithEvents label54 As System.Windows.Forms.Label
    Private WithEvents textBox_pc As System.Windows.Forms.TextBox
    Private WithEvents checkBox_pc As System.Windows.Forms.CheckBox

End Class
