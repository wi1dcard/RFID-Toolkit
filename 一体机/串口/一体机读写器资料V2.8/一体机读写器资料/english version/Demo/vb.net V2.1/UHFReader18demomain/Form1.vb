Imports ReaderB
Imports System.Text
Imports System.Windows.Forms.Keys
Imports System.IO.Ports
Imports System.Reflection
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Collections
Imports System.Resources
Imports System.IO

Public Class Form1
    Private fAppClosed As Boolean '在测试模式下响应关闭应用程序
    Private fComAdr As Byte = &HFF '当前操作的ComAdr
    Private ferrorcode As Integer
    Private fBaud As Byte
    Private fdminfre As Double
    Private fdmaxfre As Double
    Private Maskadr As Byte
    Private MaskLen As Byte
    Private MaskFlag As Byte
    Private fCmdRet As Integer = 30 '所有执行指令的返回值
    Private fOpenComIndex As Integer '打开的串口索引号
    Private fIsInventoryScan As Boolean
    Private fisinventoryscan_6B As Boolean
    Private fOperEPC(36) As Byte
    Private fPassWord(4) As Byte
    Private fOperID_6B(8) As Byte
    Private CardNum1 As Integer = 0
    Private list As ArrayList
    Private fTimer_6B_ReadWrite As Boolean
    Private fInventory_EPC_List As String '存贮询查列表（如果读取的数据没有变化，则不进行刷新）
    Private frmcomportindex As Integer
    Private ComOpen As Boolean = False
    Private breakflag As Boolean = False
    Private x_z As Double
    Private y_f As Double
    Private Sub RefreshStatus()
        If (ComboBox_AlreadyOpenCOM.Items.Count = 0) Then
            StatusBar1.Panels(1).Text = "COM Closed"
        Else
            StatusBar1.Panels(1).Text = " COM" + CStr(frmcomportindex)
        End If
        StatusBar1.Panels(0).Text = ""
        StatusBar1.Panels(2).Text = ""
    End Sub
    Private Function GetReturnCodeDesc(ByVal cmdRet As Integer) As String
        Select Case cmdRet
            Case &H0
                Return "Operation Successed"
            Case &H1
                Return "Return before Inventory finished"
            Case &H2
                Return "the Inventory-scan-time overflow"
            Case &H3
                Return "More Data"
            Case &H4
                Return "Reader module MCU is Full"
            Case &H5
                Return "Access Password Error"
            Case &H9
                Return "Destroy Password Error"
            Case &HA
                Return "Destroy Password Error Cannot be Zero"
            Case &HB
                Return "Tag Not Support the command"
            Case &HC
                Return "Use the commmand,Access Password Cannot be Zero"
            Case &HD
                Return "Tag is protected,cannot set it again"
            Case &HE
                Return "Tag is unprotected,no need to reset it"
            Case &H10
                Return "There is some locked bytes,write fail"
            Case &H11
                Return "can not lock it"
            Case &H12
                Return "is locked,cannot lock it again"
            Case &H13
                Return "Parameter Save Fail,Can Use Before Power"
            Case &H14
                Return "Cannot adjust"
            Case &H15
                Return "Return before Inventory finished"
            Case &H16
                Return "Inventory-Scan-Time overflow"
            Case &H17
                Return "More Data"
            Case &H18
                Return "Reader module MCU is full"
            Case &H19
                Return "Not Support Command Or AccessPassword Cannot be Zero"
            Case &HFA
                Return "Get Tag,Poor Communication,Inoperable"
            Case &HFB
                Return "No Tag Operable"
            Case &HFC
                Return "Tag Return ErrorCode"
            Case &HFD
                Return "Command length wrong"
            Case &HFE
                Return "Illegal command"
            Case &HFF
                Return "Parameter Error"
            Case &H30
                Return "Communication error"
            Case &H31
                Return "CRC checksummat error"
            Case &H32
                Return "Return data length error"
            Case &H33
                Return "Communication busy"
            Case &H34
                Return "Busy,command is being executed"
            Case &H35
                Return "ComPort Opened"
            Case &H36
                Return "ComPort Closed"
            Case &H37
                Return "Invalid Handle"
            Case &H38
                Return "Invalid Port"
            Case &HEE
                Return "Return command erro"
            Case Else
                Return ""
        End Select
    End Function
    Private Function GetErrorCodeDesc(ByVal cmdRet As Integer) As String
        Select Case cmdRet
            Case &H0
                Return "Other error"
            Case &H3
                Return "Memory out or pc not support"
            Case &H4
                Return "Memory Locked and unwritable"
            Case &HB
                Return "No Power,memory write operation cannot be executed"
            Case &HF
                Return "Not Special Error,tag not support special errorcode"
            Case Else
                Return ""
        End Select
    End Function
    Private Function HexStringToByteArray(ByVal s As String) As Byte()
        s = s.Replace(" ", "")
        Dim buffer(s.Length / 2 - 1) As Byte
        Dim i As Integer
        For i = 0 To s.Length - 2 Step 2
            buffer(i / 2) = Convert.ToByte(s.Substring(i, 2), 16)
        Next
        Return buffer
    End Function

    Private Function ByteArrayToHexString(ByVal data() As Byte) As String
        Dim sb As New StringBuilder(data.Length * 3)
        Dim b As Byte
        For Each b In data
            sb.Append(Convert.ToString(b, 16).PadLeft(2, "0").PadRight(3, " "))
        Next
        sb = sb.Replace(" ", "")
        Return sb.ToString().ToUpper()
    End Function
    Private Sub AddCmdLog(ByVal CMD As String, ByVal cmdStr As String, ByVal cmdRet As Integer)
        Try
            StatusBar1.Panels(0).Text = ""
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " " + cmdStr + ": " + GetReturnCodeDesc(cmdRet)
        Finally
        End Try
    End Sub 'AddCmdLog
    Private Sub ClearLastInfo()
        ComboBox_AlreadyOpenCOM.Refresh()
        RefreshStatus()
        Edit_Type.Text = ""
        Edit_Version.Text = ""
        ISO180006B.Checked = False
        EPCC1G2.Checked = False
        Edit_ComAdr.Text = ""
        Edit_powerdBm.Text = ""
        Edit_scantime.Text = ""
        Edit_dminfre.Text = ""
        Edit_dmaxfre.Text = ""
    End Sub
    Private Sub InitComList()
        Dim i As Integer = 0
        ComboBox_COM.Items.Clear()
        ComboBox_COM.Items.Add(" AUTO")
        For i = 1 To 12
            ComboBox_COM.Items.Add(" COM" + Convert.ToString(i))
        Next i
        ComboBox_COM.SelectedIndex = 0
        RefreshStatus()
    End Sub
    Private Sub InitReaderList()
        Dim i As Integer = 0
        ComboBox_baud.SelectedIndex = 3
        For i = 0 To 62
            ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 62
        ComboBox_dminfre.SelectedIndex = 0
        For i = &H3 To &HFF
            ComboBox_scantime.Items.Add(Convert.ToString(i) + "*100ms")
        Next i
        ComboBox_scantime.SelectedIndex = 7
        i = 40
        While (i <= 300)

            ComboBox_IntervalTime.Items.Add(Convert.ToString(i) + "ms")
            i = i + 10
        End While
        ComboBox_IntervalTime.SelectedIndex = 1
        For i = 0 To 6
            ComboBox_BlockNum.Items.Add(Convert.ToString(i * 2) + " and " + Convert.ToString(i * 2 + 1))
        Next i
        ComboBox_BlockNum.SelectedIndex = 0
        i = 40
        While (i <= 300)
            ComboBox_IntervalTime_6B.Items.Add(Convert.ToString(i) + "ms")
            i = i + 10
        End While
        ComboBox_IntervalTime_6B.SelectedIndex = 1
        For i = 0 To 255

            comboBox1.Items.Add(Convert.ToString(i) + "*10ms")
        Next i
        comboBox1.SelectedIndex = 30
        For i = 1 To 255

            comboBox3.Items.Add(Convert.ToString(i) + "*10us")
        Next i
        comboBox3.SelectedIndex = 9
        For i = 1 To 255
            comboBox2.Items.Add(Convert.ToString(i) + "*100us")
        Next i
        comboBox2.SelectedIndex = 14
        For i = 1 To 32
            comboBox5.Items.Add(Convert.ToString(i))
        Next i
        comboBox5.SelectedIndex = 0
        comboBox4.SelectedIndex = 0
        ComboBox_PowerDbm.SelectedIndex = 30
        For i = 0 To 255
            ComboBox6.Items.Add(Convert.ToString(i) + "*1s")
        Next i
        ComboBox6.SelectedIndex = 0
        ComboBox7.SelectedIndex = 8

        For i = 0 To 100
            comboBox_OffsetTime.Items.Add(Convert.ToString(i) + "*1ms")
        Next i
        comboBox_OffsetTime.SelectedIndex = 5

        comboBox8.SelectedIndex = 0

        For i = 0 To 254
            comboBox_tigtime.Items.Add(Convert.ToString(i) + "*1s")
        Next i
        comboBox_tigtime.SelectedIndex = 0
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        list = New ArrayList()
        progressBar1.Visible = False
        fOpenComIndex = -1
        fComAdr = 0
        ferrorcode = -1
        fBaud = 5
        InitComList()
        InitReaderList()
        NoAlarm_G2.Checked = True

        Byone_6B.Checked = True
        Different_6B.Checked = True

        P_EPC.Checked = True
        C_EPC.Checked = True
        DestroyCode.Checked = True
        NoProect.Checked = True
        NoProect2.Checked = True
        fAppClosed = False
        fIsInventoryScan = False
        fisinventoryscan_6B = False
        fTimer_6B_ReadWrite = False
        Label_Alarm.Visible = False
        Timer_Test_.Enabled = False
        Timer_G2_Read.Enabled = False
        Timer_G2_Alarm.Enabled = False
        timer1.Enabled = False

        Button3.Enabled = False
        Button5.Enabled = False
        Button1.Enabled = False
        button2.Enabled = False
        Button_DestroyCard.Enabled = False
        Button_WriteEPC_G2.Enabled = False
        Button_SetReadProtect_G2.Enabled = False
        Button_SetMultiReadProtect_G2.Enabled = False
        Button_RemoveReadProtect_G2.Enabled = False
        Button_CheckReadProtected_G2.Enabled = False
        Button_SetEASAlarm_G2.Enabled = False
        button4.Enabled = False
        Button_LockUserBlock_G2.Enabled = False
        SpeedButton_Read_G2.Enabled = False
        Button_DataWrite.Enabled = False
        Button_BlockErase.Enabled = False
        Button_BlockWrite.Enabled = False
        Button_SetProtectState.Enabled = False
        SpeedButton_Query_6B.Enabled = False
        SpeedButton_Read_6B.Enabled = False
        SpeedButton_Write_6B.Enabled = False
        Button14.Enabled = False
        Button15.Enabled = False

        DestroyCode.Enabled = False
        AccessCode.Enabled = False
        NoProect.Enabled = False
        Proect.Enabled = False
        Always.Enabled = False
        AlwaysNot.Enabled = False
        NoProect2.Enabled = False
        Proect2.Enabled = False
        Always2.Enabled = False
        AlwaysNot2.Enabled = False
        P_Reserve.Enabled = False
        P_EPC.Enabled = False
        P_TID.Enabled = False
        P_User.Enabled = False
        Same_6B.Enabled = False
        Different_6B.Enabled = False
        Less_6B.Enabled = False
        Greater_6B.Enabled = False

        radioButton1.Checked = True
        radioButton4.Checked = True
        radioButton5.Checked = True
        radioButton7.Checked = True
        radioButton10.Checked = True
        radioButton14.Checked = True
        button6.Enabled = False
        button8.Enabled = False
        button9.Enabled = False
        button10.Enabled = False
        button11.Enabled = False
        comboBox5.Enabled = False
        radioButton5.Enabled = False
        radioButton6.Enabled = False
        radioButton7.Enabled = False
        radioButton8.Enabled = False
        radioButton9.Enabled = False
        radioButton10.Enabled = False
        radioButton11.Enabled = False
        radioButton12.Enabled = False
        radioButton13.Enabled = False
        radioButton14.Enabled = False
        radioButton15.Enabled = False
        RadioButton16.Enabled = False
        RadioButton17.Enabled = False
        RadioButton19.Enabled = False
        textBox3.Enabled = False
        radioButton_band1.Checked = True
        RadioButton16.Checked = True
        ComboBox_baud2.SelectedIndex = 3
    End Sub

    Private Sub OpenPort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenPort.Click
        Dim port As Integer = 0
        Dim openresult, i As Integer
        Dim temp As String
        openresult = 30
        Cursor = Cursors.WaitCursor
        If (Edit_CmdComAddr.Text = "") Then
            Edit_CmdComAddr.Text = "FF"
        End If
        fComAdr = Convert.ToByte(Edit_CmdComAddr.Text, 16)
        Try
            If (ComboBox_COM.SelectedIndex = 0) Then
                fBaud = Convert.ToByte(ComboBox_baud2.SelectedIndex)
                If (fBaud > 2) Then
                    fBaud = Convert.ToByte(fBaud + 2)
                End If
                openresult = StaticClassReaderB.AutoOpenComPort(port, fComAdr, fBaud, frmcomportindex)
                fOpenComIndex = frmcomportindex
                If (openresult = 0) Then
                    ComOpen = True
                    If (fBaud > 3) Then
                        ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2)
                    Else
                        ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud)
                    End If
                    Button3_Click(sender, e)
                    If ((openresult = &H35) Or (openresult = &H30)) Then
                        MessageBox.Show("Serial Communication Error or Occupied", "Information")
                        StaticClassReaderB.CloseSpecComPort(frmcomportindex)
                        ComOpen = False
                    End If
                End If
            Else
                temp = ComboBox_COM.SelectedItem.ToString()
                temp = temp.Trim()
                port = Convert.ToInt32(temp.Substring(3, temp.Length - 3))
                For i = 6 To 1 Step -1
                    fBaud = Convert.ToByte(i)
                    If (fBaud = 3) Then
                        Continue For
                    End If
                    openresult = StaticClassReaderB.OpenComPort(port, fComAdr, fBaud, frmcomportindex)
                    fOpenComIndex = frmcomportindex
                    If (openresult = &H35) Then
                        MessageBox.Show("COM Opened", "Information")
                        Exit Sub
                    End If
                    If (openresult = 0) Then

                        ComOpen = True
                        Button3_Click(sender, e)
                        If (fBaud > 3) Then
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud - 2)
                        Else
                            ComboBox_baud.SelectedIndex = Convert.ToInt32(fBaud)
                        End If
                        If ((openresult = &H35) Or (openresult = &H30)) Then
                            ComOpen = False
                            MessageBox.Show("Serial Communication Error or Occupied", "Information")
                            StaticClassReaderB.CloseSpecComPort(frmcomportindex)
                            Exit Sub
                        End If
                        RefreshStatus()
                        Exit For
                    End If
                Next i

            End If
        Finally
            Cursor = Cursors.Default
        End Try
        If ((fOpenComIndex <> -1) And (openresult <> &H35) And (openresult <> &H30)) Then
            ComboBox_AlreadyOpenCOM.Items.Add("COM" + Convert.ToString(fOpenComIndex))
            ComboBox_AlreadyOpenCOM.SelectedIndex = ComboBox_AlreadyOpenCOM.SelectedIndex + 1
            Button3.Enabled = True
            Button5.Enabled = True
            Button1.Enabled = True
            button2.Enabled = True
            Button_WriteEPC_G2.Enabled = True
            Button_SetReadProtect_G2.Enabled = True
            Button_SetMultiReadProtect_G2.Enabled = True
            Button_RemoveReadProtect_G2.Enabled = True
            Button_CheckReadProtected_G2.Enabled = True
            button4.Enabled = True
            SpeedButton_Query_6B.Enabled = True
            button6.Enabled = True
            button8.Enabled = True
            button9.Enabled = True
            Button12.Enabled = True
            button_OffsetTime.Enabled = True
            button_settigtime.Enabled = True
            button_gettigtime.Enabled = True
            ComOpen = True
            button10.Text = "Get"
        End If
        If ((fOpenComIndex = -1) And (openresult = &H30)) Then
            MessageBox.Show("Serial Communication Error or Occupied", "Information")
        End If
        If ((ComboBox_AlreadyOpenCOM.Items.Count <> 0) And (fOpenComIndex <> -1) And (openresult <> &H35) And (openresult <> &H30) And (openresult = 0)) Then

            fComAdr = Convert.ToByte(Edit_ComAdr.Text, 16)
            temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString()
            frmcomportindex = Convert.ToInt32(temp.Substring(3, temp.Length - 3))
        End If
        RefreshStatus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim TrType(2) As Byte
        Dim VersionInfo(2) As Byte
        Dim ReaderType As Byte = 0
        Dim ScanTime As Byte = 0
        Dim dmaxfre As Byte = 0
        Dim dminfre As Byte = 0
        Dim powerdBm As Byte = 0
        Dim FreBand As Byte = 0
        Dim i As Integer = 0
        Edit_Version.Text = ""
        Edit_ComAdr.Text = ""
        Edit_scantime.Text = ""
        Edit_Type.Text = ""
        ISO180006B.Checked = False
        EPCC1G2.Checked = False
        Edit_powerdBm.Text = ""
        Edit_dminfre.Text = ""
        Edit_dmaxfre.Text = ""
        ComboBox_PowerDbm.Items.Clear()
        fCmdRet = StaticClassReaderB.GetReaderInformation(fComAdr, VersionInfo, ReaderType, TrType, dmaxfre, dminfre, powerdBm, ScanTime, frmcomportindex)
        If (fCmdRet = 0) Then

            Edit_Version.Text = Convert.ToString(VersionInfo(0), 10).PadLeft(2, "0") + "." + Convert.ToString(VersionInfo(1), 10).PadLeft(2, "0")
            If (VersionInfo(1) >= 30) Then
                For i = 0 To 30
                    ComboBox_PowerDbm.Items.Add(Convert.ToString(i))
                Next i
                If (powerdBm > 30) Then
                    ComboBox_PowerDbm.SelectedIndex = 30
                Else
                    ComboBox_PowerDbm.SelectedIndex = powerdBm
                End If
            Else
                For i = 0 To 18
                    ComboBox_PowerDbm.Items.Add(Convert.ToString(i))
                Next i
                If (powerdBm > 18) Then
                    ComboBox_PowerDbm.SelectedIndex = 18
                Else
                    ComboBox_PowerDbm.SelectedIndex = powerdBm
                End If
            End If
            Edit_ComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, "0")
            Edit_NewComAdr.Text = Convert.ToString(fComAdr, 16).PadLeft(2, "0")
            Edit_scantime.Text = Convert.ToString(ScanTime, 10).PadLeft(2, "0") + "*100ms"
            ComboBox_scantime.SelectedIndex = ScanTime - 3
            Edit_powerdBm.Text = Convert.ToString(powerdBm, 10).PadLeft(2, "0")
            FreBand = Convert.ToByte(((dmaxfre And &HC0) >> 4) Or (dminfre >> 6))
            Select Case FreBand

                Case 0
                    radioButton_band1.Checked = True
                    fdminfre = 902.6 + (dminfre And &H3F) * 0.4
                    fdmaxfre = 902.6 + (dmaxfre And &H3F) * 0.4
                Case 1
                    radioButton_band2.Checked = True
                    fdminfre = 920.125 + (dminfre And &H3F) * 0.25
                    fdmaxfre = 920.125 + (dmaxfre And &H3F) * 0.25
                Case 2
                    radioButton_band3.Checked = True
                    fdminfre = 902.75 + (dminfre And &H3F) * 0.5
                    fdmaxfre = 902.75 + (dmaxfre And &H3F) * 0.5
                Case 3
                    radioButton_band4.Checked = True
                    fdminfre = 917.1 + (dminfre And &H3F) * 0.2
                    fdmaxfre = 917.1 + (dmaxfre And &H3F) * 0.2
                Case 4
                    radioButton_band5.Checked = True
                    fdminfre = 865.1 + (dminfre And &H3F) * 0.2
                    fdmaxfre = 865.1 + (dmaxfre And &H3F) * 0.2
                Case Else

            End Select
            Edit_dminfre.Text = Convert.ToString(fdminfre) + "MHz"
            Edit_dmaxfre.Text = Convert.ToString(fdmaxfre) + "MHz"
            If (fdmaxfre <> fdminfre) Then
                CheckBox_SameFre.Checked = False
            End If
            ComboBox_dminfre.SelectedIndex = dminfre And &H3F
            ComboBox_dmaxfre.SelectedIndex = dmaxfre And &H3F
            If (ReaderType = &H3) Then
                Edit_Type.Text = ""
            End If
            If (ReaderType = &H6) Then
                Edit_Type.Text = ""
            End If
            If (ReaderType = &H9) Then
                Edit_Type.Text = "UHFReader18"
            End If
            If ((TrType(0) And &H2) = &H2) Then

                ISO180006B.Checked = True
                EPCC1G2.Checked = True
            Else

                ISO180006B.Checked = False
                EPCC1G2.Checked = False
            End If

        End If
        AddCmdLog("GetReaderInformation", "GetReaderInformation", fCmdRet)
    End Sub

    Private Sub ClosePort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClosePort.Click
        Dim port As Integer
        'string SelectCom ;
        Dim temp As String
        ClearLastInfo()
        Try
            If (ComboBox_AlreadyOpenCOM.SelectedIndex < 0) Then
                MessageBox.Show("Please Choose COM Port to close", "信息提示")
            Else
                temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString()
                port = Convert.ToInt32(temp.Substring(3, temp.Length - 3))
                fCmdRet = StaticClassReaderB.CloseSpecComPort(port)
                If (fCmdRet = 0) Then
                    ComboBox_AlreadyOpenCOM.Items.RemoveAt(0)
                    If (ComboBox_AlreadyOpenCOM.Items.Count <> 0) Then
                        temp = ComboBox_AlreadyOpenCOM.SelectedItem.ToString()
                        port = Convert.ToInt32(temp.Substring(3, temp.Length - 3))
                        StaticClassReaderB.CloseSpecComPort(port)
                        fComAdr = &HFF
                        StaticClassReaderB.OpenComPort(port, fComAdr, fBaud, frmcomportindex)
                        fOpenComIndex = frmcomportindex
                        RefreshStatus()
                        Button3_Click(sender, e) '自动执行读取写卡器信息
                    End If
                Else
                    MessageBox.Show("Serial Communication Error", "Information")
                End If
            End If
        Finally

        End Try
        If (ComboBox_AlreadyOpenCOM.Items.Count <> 0) Then
            ComboBox_AlreadyOpenCOM.SelectedIndex = 0
        Else
            fOpenComIndex = -1
            ComboBox_AlreadyOpenCOM.Items.Clear()
            ComboBox_AlreadyOpenCOM.Refresh()
            RefreshStatus()
            Button3.Enabled = False
            Button5.Enabled = False
            Button1.Enabled = False
            button2.Enabled = False
            Button_DestroyCard.Enabled = False
            Button_WriteEPC_G2.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetMultiReadProtect_G2.Enabled = False
            Button_RemoveReadProtect_G2.Enabled = False
            Button_CheckReadProtected_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            button4.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_SetProtectState.Enabled = False
            SpeedButton_Query_6B.Enabled = False
            SpeedButton_Read_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False

            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False

            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False

            Same_6B.Enabled = False
            Different_6B.Enabled = False
            Less_6B.Enabled = False
            Greater_6B.Enabled = False
            button6.Enabled = False
            button8.Enabled = False
            button9.Enabled = False

            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_WriteEPC_G2.Enabled = False
            Button_SetMultiReadProtect_G2.Enabled = False
            Button_RemoveReadProtect_G2.Enabled = False
            Button_CheckReadProtected_G2.Enabled = False
            button4.Enabled = False

            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_SetProtectState.Enabled = False
            ListView1_EPC.Items.Clear()
            ComboBox_EPC1.Items.Clear()
            ComboBox_EPC2.Items.Clear()
            ComboBox_EPC3.Items.Clear()
            ComboBox_EPC4.Items.Clear()
            ComboBox_EPC5.Items.Clear()
            ComboBox_EPC6.Items.Clear()
            checkBox1.Enabled = False
            button10.Text = "Get"
            SpeedButton_Read_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            ListView_ID_6B.Items.Clear()
            ComOpen = False
            timer1.Enabled = False
            button10.Enabled = False
            comboBox4.SelectedIndex = 0
            Button12.Enabled = False
            button_OffsetTime.Enabled = False
            button_settigtime.Enabled = False
            button_gettigtime.Enabled = False
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim aNewComAdr, powerDbm, dminfre, dmaxfre, scantime, band As Byte
        Dim returninfo As String = ""
        Dim returninfoDlg As String = ""
        Dim setinfo As String
        If (radioButton_band1.Checked) Then
            band = 0
        End If
        If (radioButton_band2.Checked) Then
            band = 1
        End If
        If (radioButton_band3.Checked) Then
            band = 2
        End If
        If (radioButton_band4.Checked) Then
            band = 3
        End If
        If (radioButton_band5.Checked) Then
            band = 4
        End If
        If (Edit_NewComAdr.Text = "") Then
            Exit Sub
        End If
        progressBar1.Visible = True
        progressBar1.Minimum = 0
        dminfre = Convert.ToByte(((band And 3) << 6) Or (ComboBox_dminfre.SelectedIndex And &H3F))
        dmaxfre = Convert.ToByte(((band And &HC) << 4) Or (ComboBox_dmaxfre.SelectedIndex And &H3F))
        aNewComAdr = Convert.ToByte(Edit_NewComAdr.Text)
        powerDbm = Convert.ToByte(ComboBox_PowerDbm.SelectedIndex)
        fBaud = Convert.ToByte(ComboBox_baud.SelectedIndex)
        If (fBaud > 2) Then
            fBaud = Convert.ToByte(fBaud + 2)
        End If
        scantime = Convert.ToByte(ComboBox_scantime.SelectedIndex + 3)
        setinfo = "Write"
        progressBar1.Value = 10
        fCmdRet = StaticClassReaderB.WriteComAdr(fComAdr, aNewComAdr, frmcomportindex)
        If (fCmdRet = &H13) Then
            fComAdr = aNewComAdr
        End If
        If (fCmdRet = 0) Then
            fComAdr = aNewComAdr
            returninfo = returninfo + setinfo + "Address Successfully"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + setinfo + "Address Response Command Error"
        Else
            returninfo = returninfo + setinfo + "Address Fail"
            returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If
        progressBar1.Value = 25
        fCmdRet = StaticClassReaderB.SetPowerDbm(fComAdr, powerDbm, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Power Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Power Response Command Error"
        Else
            returninfo = returninfo + ",Power Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 40
        fCmdRet = StaticClassReaderB.Writedfre(fComAdr, dmaxfre, dminfre, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Frequency Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Frequency Response Command Error"
        Else

            returninfo = returninfo + ",Frequency Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 55
        fCmdRet = StaticClassReaderB.Writebaud(fComAdr, fBaud, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Baud Rate Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Baud Rate Response Command Error"
        Else

            returninfo = returninfo + ",Baud Rate Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 70
        fCmdRet = StaticClassReaderB.WriteScanTime(fComAdr, scantime, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",InventoryScanTime Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",InventoryScanTime Response Command Error"
        Else
            returninfo = returninfo + ",InventoryScanTime Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 100
        Button3_Click(sender, e)
        progressBar1.Visible = False
        StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + returninfo
        If (returninfoDlg <> "") Then
            MessageBox.Show(returninfoDlg, "Information")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim aNewComAdr, powerDbm, dminfre, dmaxfre, scantime As Byte
        Dim returninfo As String = ""
        Dim returninfoDlg As String = ""
        Dim setinfo As String
        progressBar1.Visible = True
        progressBar1.Minimum = 0
        dminfre = 0
        dmaxfre = 62
        aNewComAdr = &H0
        If (Convert.ToInt32(Edit_Version.Text.Substring(3, 2))) Then
            powerDbm = 30
        Else
            powerDbm = 18
        End If

        fBaud = 5
        scantime = 10
        setinfo = " Recovery "
        ComboBox_baud.SelectedIndex = 3
        progressBar1.Value = 10
        fCmdRet = StaticClassReaderB.WriteComAdr(fComAdr, aNewComAdr, frmcomportindex)
        If (fCmdRet = &H13) Then
            fComAdr = aNewComAdr
        End If
        If (fCmdRet = 0) Then

            fComAdr = aNewComAdr
            returninfo = returninfo + setinfo + "Address Successfully"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + setinfo + "Address Response Command Error"
        Else

            returninfo = returninfo + setinfo + "Address Fail"
            returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 25
        fCmdRet = StaticClassReaderB.SetPowerDbm(fComAdr, powerDbm, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Power Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Power Response Command Error"
        Else

            returninfo = returninfo + ",Power Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"

        End If
        progressBar1.Value = 40
        fCmdRet = StaticClassReaderB.Writedfre(fComAdr, dmaxfre, dminfre, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Frequency Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Frequency Response Command Error"
        Else
            returninfo = returninfo + ",Frequency Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 55
        fCmdRet = StaticClassReaderB.Writebaud(fComAdr, fBaud, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Baud Rate Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Baud Rate Response Command Error"
        Else
            returninfo = returninfo + ",Baud Rate Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 70
        fCmdRet = StaticClassReaderB.WriteScanTime(fComAdr, scantime, frmcomportindex)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",InventoryScanTime Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",InventoryScanTime Response Command Error"
        Else
            returninfo = returninfo + ",InventoryScanTime Fail"
            returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        progressBar1.Value = 100
        Button3_Click(sender, e)
        progressBar1.Visible = False
        StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + returninfo
        If (returninfoDlg <> "") Then
            MessageBox.Show(returninfoDlg, "Information")
        End If
    End Sub

    Private Sub CheckBox_SameFre_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_SameFre.CheckedChanged
        If (CheckBox_SameFre.Checked) Then
            ComboBox_dmaxfre.SelectedIndex = ComboBox_dminfre.SelectedIndex
        End If
    End Sub

    Private Sub ComboBox_dminfre_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If (CheckBox_SameFre.Checked) Then
            ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex
        ElseIf (ComboBox_dminfre.SelectedIndex > ComboBox_dmaxfre.SelectedIndex) Then

            ComboBox_dminfre.SelectedIndex = ComboBox_dmaxfre.SelectedIndex
            MessageBox.Show("Min.Frequency is equal or lesser than Max.Frequency", "Error Information")
        End If
    End Sub
    Public Sub ChangeSubItem(ByVal ListItem As ListViewItem, ByVal subItemIndex As Integer, ByVal ItemText As String)
        If (subItemIndex = 1) Then
            If (ItemText = "") Then
                ListItem.SubItems(subItemIndex).Text = ItemText
                If (ListItem.SubItems(subItemIndex + 2).Text = "") Then
                    ListItem.SubItems(subItemIndex + 2).Text = "1"
                Else
                    ListItem.SubItems(subItemIndex + 2).Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems(subItemIndex + 2).Text) + 1)
                End If
            ElseIf (ListItem.SubItems(subItemIndex).Text <> ItemText) Then
                ListItem.SubItems(subItemIndex).Text = ItemText
                ListItem.SubItems(subItemIndex + 2).Text = "1"
            Else
                ListItem.SubItems(subItemIndex + 2).Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems(subItemIndex + 2).Text) + 1)
                If ((Convert.ToUInt32(ListItem.SubItems(subItemIndex + 2).Text) > 9999)) Then
                    ListItem.SubItems(subItemIndex + 2).Text = "1"
                End If
            End If
        ElseIf (subItemIndex = 2) Then
            If (ListItem.SubItems(subItemIndex).Text <> ItemText) Then
                ListItem.SubItems(subItemIndex).Text = ItemText
            End If
        End If

    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        If (CheckBox_TID.Checked) Then
            If ((textBox4.Text.Length) <> 2 Or ((textBox5.Text.Length) <> 2)) Then
                StatusBar1.Panels(0).Text = "Query TID Parameter Error！"
                Return
            End If
        End If
        Timer_Test_.Enabled = Not Timer_Test_.Enabled
        If (Not Timer_Test_.Enabled) Then
            textBox4.Enabled = True
            textBox5.Enabled = True
            CheckBox_TID.Enabled = True
            If (ListView1_EPC.Items.Count <> 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = True
                Proect2.Enabled = True
                Always2.Enabled = True
                AlwaysNot2.Enabled = True
                P_Reserve.Enabled = True
                P_EPC.Enabled = True
                P_TID.Enabled = True
                P_User.Enabled = True
                Button_DestroyCard.Enabled = True
                Button_SetReadProtect_G2.Enabled = True
                Button_SetEASAlarm_G2.Enabled = True
                Alarm_G2.Enabled = True
                NoAlarm_G2.Enabled = True
                Button_LockUserBlock_G2.Enabled = True
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button4.Enabled = True
                SpeedButton_Read_G2.Enabled = True
                Button_SetProtectState.Enabled = True
                Button_DataWrite.Enabled = True
                Button_BlockWrite.Enabled = True
                Button_BlockErase.Enabled = True
                Button_BlockWrite.Enabled = True
                checkBox1.Enabled = True
            End If
            If (ListView1_EPC.Items.Count = 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = False
                Proect2.Enabled = False
                Always2.Enabled = False
                AlwaysNot2.Enabled = False
                P_Reserve.Enabled = False
                P_EPC.Enabled = False
                P_TID.Enabled = False
                P_User.Enabled = False
                Button_DestroyCard.Enabled = False
                Button_SetReadProtect_G2.Enabled = False
                Button_SetEASAlarm_G2.Enabled = False
                Alarm_G2.Enabled = False
                NoAlarm_G2.Enabled = False
                Button_LockUserBlock_G2.Enabled = False
                SpeedButton_Read_G2.Enabled = False
                Button_DataWrite.Enabled = False
                Button_BlockWrite.Enabled = False
                Button_BlockErase.Enabled = False
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button4.Enabled = True
                Button_SetProtectState.Enabled = False
                checkBox1.Enabled = False

            End If
            AddCmdLog("Inventory", "Exit Query", 0)
            button2.Text = "Query Tag"
        Else
            textBox4.Enabled = False
            textBox5.Enabled = False
            CheckBox_TID.Enabled = False
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_WriteEPC_G2.Enabled = False
            Button_SetMultiReadProtect_G2.Enabled = False
            Button_RemoveReadProtect_G2.Enabled = False
            Button_CheckReadProtected_G2.Enabled = False
            button4.Enabled = False

            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_SetProtectState.Enabled = False
            ListView1_EPC.Items.Clear()
            ComboBox_EPC1.Items.Clear()
            ComboBox_EPC2.Items.Clear()
            ComboBox_EPC3.Items.Clear()
            ComboBox_EPC4.Items.Clear()
            ComboBox_EPC5.Items.Clear()
            ComboBox_EPC6.Items.Clear()
            button2.Text = "Stop"
            checkBox1.Enabled = False
        End If
    End Sub
    Private Sub Inventory()
        Dim i As Integer
        Dim CardNum As Integer = 0
        Dim Totallen As Integer = 0
        Dim EPClen, m As Integer
        Dim EPC(5000) As Byte
        Dim CardIndex As Integer
        Dim temps As String
        Dim s, sEPC As String
        Dim isonlistview As Boolean
        Dim aListItem As ListViewItem
        Dim AdrTID As Byte = 0
        Dim LenTID As Byte = 0
        Dim TIDFlag As Byte = 0
        If (CheckBox_TID.Checked) Then
            AdrTID = Convert.ToByte(textBox4.Text, 16)
            LenTID = Convert.ToByte(textBox5.Text, 16)
            TIDFlag = 1
        Else
            AdrTID = 0
            LenTID = 0
            TIDFlag = 0
        End If
        fCmdRet = StaticClassReaderB.Inventory_G2(fComAdr, AdrTID, LenTID, TIDFlag, EPC, Totallen, CardNum, frmcomportindex)

        If ((fCmdRet = 1) Or (fCmdRet = 2) Or (fCmdRet = 3) Or (fCmdRet = 4) Or (fCmdRet = &HFB)) Then
            Dim daw(Totallen) As Byte
            Array.Copy(EPC, daw, Totallen)
            temps = ByteArrayToHexString(daw)
            fInventory_EPC_List = temps
            m = 0

            If (CardNum = 0) Then
                fIsInventoryScan = False
                Exit Sub
            End If
            For CardIndex = 0 To CardNum - 1
                EPClen = daw(m)
                sEPC = temps.Substring(m * 2 + 2, EPClen * 2)
                m = m + EPClen + 1
                If (sEPC.Length <> EPClen * 2) Then
                    fIsInventoryScan = False
                    Exit Sub
                End If
                isonlistview = False
                For i = 0 To ListView1_EPC.Items.Count - 1
                    If (sEPC = ListView1_EPC.Items(i).SubItems(1).Text) Then
                        aListItem = ListView1_EPC.Items(i)
                        ChangeSubItem(aListItem, 1, sEPC)
                        isonlistview = True
                    End If
                Next i
                If (Not isonlistview) Then
                    aListItem = ListView1_EPC.Items.Add((ListView1_EPC.Items.Count + 1).ToString())
                    aListItem.SubItems.Add("")
                    aListItem.SubItems.Add("")
                    aListItem.SubItems.Add("")
                    s = sEPC
                    ChangeSubItem(aListItem, 1, s)
                    s = (sEPC.Length / 2).ToString().PadLeft(2, "0")
                    ChangeSubItem(aListItem, 2, s)
                    If (Not CheckBox_TID.Checked) Then
                        If (ComboBox_EPC1.Items.IndexOf(sEPC) = -1) Then
                            ComboBox_EPC1.Items.Add(sEPC)
                            ComboBox_EPC2.Items.Add(sEPC)
                            ComboBox_EPC3.Items.Add(sEPC)
                            ComboBox_EPC4.Items.Add(sEPC)
                            ComboBox_EPC5.Items.Add(sEPC)
                            ComboBox_EPC6.Items.Add(sEPC)
                        End If
                    End If
                End If
            Next CardIndex
        End If
        If (Not CheckBox_TID.Checked) Then
            If ((ComboBox_EPC1.Items.Count <> 0)) Then
                ComboBox_EPC1.SelectedIndex = 0
                ComboBox_EPC2.SelectedIndex = 0
                ComboBox_EPC3.SelectedIndex = 0
                ComboBox_EPC4.SelectedIndex = 0
                ComboBox_EPC5.SelectedIndex = 0
                ComboBox_EPC6.SelectedIndex = 0
            End If
        End If
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Timer_Test__Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Test_.Tick
        If (fIsInventoryScan) Then
            Exit Sub
        End If
        fIsInventoryScan = True
        Inventory()
        fIsInventoryScan = False
    End Sub

    Private Sub SpeedButton_Read_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeedButton_Read_G2.Click
        If (Edit_WordPtr.Text = "") Then
            MessageBox.Show("Address of Tag Data is NULL", "Information")
            Exit Sub
        End If
        If (textBox1.Text = "") Then

            MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information")
            Exit Sub
        End If
        If (Edit_AccessCode2.Text = "") Then
            MessageBox.Show("(PassWord) is NULL", "Information")
            Exit Sub
        End If
        If (Convert.ToInt32(Edit_WordPtr.Text, 16) + Convert.ToInt32(textBox1.Text) > 120) Then
            Exit Sub
        End If
        Timer_G2_Read.Enabled = Not Timer_G2_Read.Enabled
        If (Timer_G2_Read.Enabled) Then
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_WriteEPC_G2.Enabled = False
            Button_SetMultiReadProtect_G2.Enabled = False
            Button_RemoveReadProtect_G2.Enabled = False
            Button_CheckReadProtected_G2.Enabled = False
            button4.Enabled = False

            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            button2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_SetProtectState.Enabled = False
            SpeedButton_Read_G2.Text = "Stop"
        Else
            If (ListView1_EPC.Items.Count <> 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = True
                Proect2.Enabled = True
                Always2.Enabled = True
                AlwaysNot2.Enabled = True
                P_Reserve.Enabled = True
                P_EPC.Enabled = True
                P_TID.Enabled = True
                P_User.Enabled = True
                Button_DestroyCard.Enabled = True
                Button_SetReadProtect_G2.Enabled = True
                Button_SetEASAlarm_G2.Enabled = True
                Alarm_G2.Enabled = True
                NoAlarm_G2.Enabled = True
                Button_LockUserBlock_G2.Enabled = True
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button4.Enabled = True
                button2.Enabled = True
                Button_SetProtectState.Enabled = True

                Button_DataWrite.Enabled = True
                Button_BlockWrite.Enabled = True
                Button_BlockErase.Enabled = True
            End If
            If (ListView1_EPC.Items.Count = 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = False
                Proect2.Enabled = False
                Always2.Enabled = False
                AlwaysNot2.Enabled = False
                P_Reserve.Enabled = False
                P_EPC.Enabled = False
                P_TID.Enabled = False
                P_User.Enabled = False
                Button_DestroyCard.Enabled = False
                Button_SetReadProtect_G2.Enabled = False
                Button_SetEASAlarm_G2.Enabled = False
                Alarm_G2.Enabled = False
                NoAlarm_G2.Enabled = False
                Button_LockUserBlock_G2.Enabled = False
                Button_SetProtectState.Enabled = False
                button2.Enabled = True
                Button_DataWrite.Enabled = False
                Button_BlockWrite.Enabled = False
                Button_BlockErase.Enabled = False
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button4.Enabled = True
            End If
            SpeedButton_Read_G2.Text = "Read"
        End If

    End Sub

    Private Sub Timer_G2_Read_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_G2_Read.Tick
        If (fIsInventoryScan) Then
            Exit Sub
        End If
        fIsInventoryScan = True
        Dim WordPtr, ENum1 As Byte
        Dim Num As Byte = 0
        Dim Mem As Byte = 0
        Dim EPClength As Byte = 0
        Dim str As String
        Dim CardData(320) As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            fIsInventoryScan = False
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (textBox1.Text = "") Then
            fIsInventoryScan = False
            Exit Sub
        End If
        If (ComboBox_EPC2.Items.Count = 0) Then
            fIsInventoryScan = False
            Exit Sub
        End If
        If (ComboBox_EPC2.SelectedItem = Nothing) Then
            fIsInventoryScan = False
            Exit Sub
        End If
        str = ComboBox_EPC2.SelectedItem.ToString()
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        If (C_Reserve.Checked) Then
            Mem = 0
        End If
        If (C_EPC.Checked) Then
            Mem = 1
        End If
        If (C_TID.Checked) Then
            Mem = 2
        End If
        If (C_User.Checked) Then
            Mem = 3
        End If
        If (Edit_AccessCode2.Text = "") Then
            fIsInventoryScan = False
            Exit Sub
        End If
        If (Edit_WordPtr.Text = "") Then
            fIsInventoryScan = False
            Exit Sub
        End If
        WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16)
        Num = Convert.ToByte(textBox1.Text)
        If (Edit_AccessCode2.Text.Length <> 8) Then
            fIsInventoryScan = False
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode2.Text)
        fCmdRet = StaticClassReaderB.ReadCard_G2(fComAdr, EPC, Mem, WordPtr, Num, fPassWord, Maskadr, MaskLen, MaskFlag, CardData, EPClength, ferrorcode, frmcomportindex)
        If (fCmdRet = 0) Then
            Dim daw(Num * 2 - 1) As Byte
            Array.Copy(CardData, daw, Num * 2)
            listBox1.Items.Add(ByteArrayToHexString(daw))
            listBox1.SelectedIndex = listBox1.Items.Count - 1
            AddCmdLog("ReadData", "Read", fCmdRet)
        End If
        If (ferrorcode <> -1) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Read' Response ErrorCode=0x" + Convert.ToString(ferrorcode, 2) + "(" + GetErrorCodeDesc(ferrorcode) + ")"
            ferrorcode = -1
        End If
        fIsInventoryScan = False
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Button_DataWrite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DataWrite.Click
        Dim WordPtr, ENum1 As Byte
        Dim Num As Byte = 0
        Dim Mem As Byte = 0
        Dim WNum As Byte = 0
        Dim EPClength As Byte = 0
        Dim Writedatalen As Byte = 0
        Dim WrittenDataNum As Integer = 0
        Dim s2, str As String
        Dim CardData(320) As Byte
        Dim writedata(230) As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (ComboBox_EPC2.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC2.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC2.SelectedItem.ToString()
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(ENum1 * 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        If (C_Reserve.Checked) Then
            Mem = 0
        End If
        If (C_EPC.Checked) Then
            Mem = 1
        End If
        If (C_TID.Checked) Then
            Mem = 2
        End If
        If (C_User.Checked) Then
            Mem = 3
        End If
        If (Edit_WordPtr.Text = "") Then
            MessageBox.Show("Address of Tag Data is NULL", "Information")
            Exit Sub
        End If
        If (textBox1.Text = "") Then
            MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information")
            Exit Sub
        End If
        If (Convert.ToInt32(Edit_WordPtr.Text, 16) + Convert.ToInt32(textBox1.Text) > 120) Then
            Exit Sub
        End If
        If (Edit_AccessCode2.Text = "") Then
            Exit Sub
        End If
        WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16)
        Num = Convert.ToByte(textBox1.Text)
        If (Edit_AccessCode2.Text.Length <> 8) Then
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode2.Text)
        If (Edit_WriteData.Text = "") Then
            Exit Sub
        End If
        s2 = Edit_WriteData.Text
        If (s2.Length Mod 4 <> 0) Then
            MessageBox.Show("The Number must be 4 times.", "Wtite")
            Exit Sub
        End If
        WNum = Convert.ToByte(s2.Length / 4)
        ReDim writedata(WNum * 2)
        writedata = HexStringToByteArray(s2)
        Writedatalen = Convert.ToByte(WNum * 2)
        If ((checkBox_pc.Checked) And (C_EPC.Checked)) Then
            WordPtr = 1
            Writedatalen = Convert.ToByte(Edit_WriteData.Text.Length / 2 + 2)
            writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text)
        End If
        fCmdRet = StaticClassReaderB.WriteCard_G2(fComAdr, EPC, Mem, WordPtr, Writedatalen, writedata, fPassWord, Maskadr, MaskLen, MaskFlag, WrittenDataNum, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("Write data", "write", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + "‘'Write'Command Response=0x00'(completely write Data successfully)"
        End If
    End Sub

    Private Sub Button_BlockErase_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_BlockErase.Click
        Dim WordPtr, ENum1 As Byte
        Dim Num As Byte = 0
        Dim Mem As Byte = 0
        Dim EPClength As Byte = 0
        Dim str As String
        Dim CardData(320) As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (ComboBox_EPC2.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC2.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC2.SelectedItem.ToString()
        If (str = "") Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        If (C_Reserve.Checked) Then
            Mem = 0
        End If
        If (C_EPC.Checked) Then
            Mem = 1
        End If
        If (C_TID.Checked) Then
            Mem = 2
        End If
        If (C_User.Checked) Then
            Mem = 3
        End If
        If (Edit_WordPtr.Text = "") Then
            MessageBox.Show("Address of Tag Data is NULL", "Information")
            Exit Sub
        End If
        If (textBox1.Text = "") Then
            MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information")
            Exit Sub
        End If
        If (Convert.ToInt32(Edit_WordPtr.Text, 16) + Convert.ToInt32(textBox1.Text) > 120) Then
            Exit Sub
        End If
        If (Edit_AccessCode2.Text = "") Then
            Exit Sub
        End If
        WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16)
        If ((Mem = 1) And (WordPtr < 2)) Then
            MessageBox.Show("the length of start Address of erasing EPC area is equal or greater than 0x01!", "Information")
            Exit Sub
        End If
        Num = Convert.ToByte(textBox1.Text)
        If (Edit_AccessCode2.Text.Length <> 8) Then
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode2.Text)
        fCmdRet = StaticClassReaderB.EraseCard_G2(fComAdr, EPC, Mem, WordPtr, Num, fPassWord, Maskadr, MaskLen, MaskFlag, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("EraseCard", "Erase data", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Block Erase'Command Response=0x00" + "(Block Erase successfully)"
        End If
    End Sub

    Private Sub button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button7.Click
        listBox1.Items.Clear()
    End Sub

    Private Sub Button_SetProtectState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SetProtectState.Click
        Dim select1 As Byte = 0
        Dim setprotect As Byte = 0
        Dim EPClength As Byte
        Dim Str As String
        Dim ENum1 As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            fIsInventoryScan = False
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (ComboBox_EPC1.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC1.SelectedItem = Nothing) Then
            Exit Sub
        End If
        Str = ComboBox_EPC1.SelectedItem.ToString()
        If (Str = "") Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(Str.Length / 4)
        Dim EPC(200) As Byte
        EPC = HexStringToByteArray(Str)
        EPClength = Str.Length() / 2
        If (textBox2.Text.Length <> 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(textBox2.Text)
        If ((P_Reserve.Checked) And (DestroyCode.Checked)) Then
            select1 = &H0
        ElseIf ((P_Reserve.Checked) And (AccessCode.Checked)) Then
            select1 = &H1
        ElseIf (P_EPC.Checked) Then
            select1 = &H2
        ElseIf (P_TID.Checked) Then
            select1 = &H3
        ElseIf (P_User.Checked) Then
            select1 = &H4
        End If
        If (P_Reserve.Checked) Then
            If (NoProect.Checked) Then
                setprotect = &H0
            ElseIf (Proect.Checked) Then
                setprotect = &H2
            ElseIf (Always.Checked) Then
                setprotect = &H1
                If (MessageBox.Show(Me, "Set readable and writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
                    Exit Sub
                End If
            ElseIf (AlwaysNot.Checked) Then
                setprotect = &H3
                If (MessageBox.Show(Me, "Set never readable and writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
                    Exit Sub
                End If
            End If
        Else
            If (NoProect2.Checked) Then
                setprotect = &H0
            ElseIf (Proect2.Checked) Then
                setprotect = &H2
            ElseIf (Always2.Checked) Then
                setprotect = &H1
                If (MessageBox.Show(Me, "Set writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
                    Exit Sub
                End If

            ElseIf (AlwaysNot2.Checked) Then
                setprotect = &H3
                If (MessageBox.Show(Me, "Set never writeable Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
                    Exit Sub
                End If
            End If
        End If
        fCmdRet = StaticClassReaderB.SetCardProtect_G2(fComAdr, EPC, select1, setprotect, fPassWord, Maskadr, MaskLen, MaskFlag, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("SetCardProtect", "SetProtect", fCmdRet)
    End Sub

    Private Sub P_Reserve_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P_Reserve.Click
        If (ListView1_EPC.Items.Count <> 0) Then
            DestroyCode.Enabled = True
            AccessCode.Enabled = True
            NoProect.Enabled = True
            Proect.Enabled = True
            Always.Enabled = True
            AlwaysNot.Enabled = True
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
        End If
    End Sub

    Private Sub P_EPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P_EPC.Click
        If (ListView1_EPC.Items.Count <> 0) Then
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = True
            Proect2.Enabled = True
            Always2.Enabled = True
            AlwaysNot2.Enabled = True
        End If
    End Sub

    Private Sub P_TID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P_TID.Click
        If (ListView1_EPC.Items.Count <> 0) Then
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = True
            Proect2.Enabled = True
            Always2.Enabled = True
            AlwaysNot2.Enabled = True
        End If
    End Sub

    Private Sub P_User_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles P_User.Click
        If (ListView1_EPC.Items.Count <> 0) Then
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = True
            Proect2.Enabled = True
            Always2.Enabled = True
            AlwaysNot2.Enabled = True
        End If
    End Sub

    Private Sub Button_DestroyCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_DestroyCard.Click
        Dim EPClength As Byte
        Dim str As String
        Dim ENum1 As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + ""
        If (Edit_DestroyCode.Text.Length <> 8) Then
            MessageBox.Show("Kill Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        If (ComboBox_EPC3.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC3.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC3.SelectedItem.ToString()
        If (str = "") Then
            Exit Sub
        End If
        If (MessageBox.Show(Me, "Kill the Tag  Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        fPassWord = HexStringToByteArray(Edit_DestroyCode.Text)
        fCmdRet = StaticClassReaderB.DestroyCard_G2(fComAdr, EPC, fPassWord, Maskadr, MaskLen, MaskFlag, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("DestroyCard", "Kill Tag", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Kill Tag'Command Response=0x00" + "(Kill successfully)"
        End If
    End Sub

    Private Sub Button_WriteEPC_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_WriteEPC_G2.Click
        Dim WriteEPC(100) As Byte
        Dim WriteEPClen As Byte
        Dim ENum1 As Byte
        If (Edit_AccessCode3.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        If ((Edit_WriteEPC.Text.Length Mod 4) <> 0) Then

            MessageBox.Show("Please input Data in words in hexadecimal form!", "Information")
            Exit Sub
        End If
        WriteEPClen = Convert.ToByte(Edit_WriteEPC.Text.Length / 2)
        ENum1 = Convert.ToByte(Edit_WriteEPC.Text.Length / 4)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(Edit_WriteEPC.Text)
        fPassWord = HexStringToByteArray(Edit_AccessCode3.Text)
        fCmdRet = StaticClassReaderB.WriteEPC_G2(fComAdr, fPassWord, EPC, WriteEPClen, ferrorcode, frmcomportindex)
        AddCmdLog("WriteEPC_G2", "Write EPC", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Write EPC'Command Response=0x00" + "(Write EPC successfully)"
        End If
    End Sub

    Private Sub Button_SetReadProtect_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SetReadProtect_G2.Click
        Dim EPClength As Byte
        Dim ENum1 As Byte
        Dim str As String
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (Edit_AccessCode4.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        If (ComboBox_EPC4.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC4.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC4.SelectedItem.ToString()
        If (str = "") Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        fPassWord = HexStringToByteArray(Edit_AccessCode4.Text)
        fCmdRet = StaticClassReaderB.SetReadProtect_G2(fComAdr, EPC, fPassWord, Maskadr, MaskLen, MaskFlag, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("SetReadProtect_G2", "Set Single Tag Read Protection", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Set Single Tag Read Protection'Command Response=0x00" + "Set Single Tag Read Protection successfully"
        End If
    End Sub

    Private Sub Button_SetMultiReadProtect_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SetMultiReadProtect_G2.Click
        If (Edit_AccessCode4.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode4.Text)
        fCmdRet = StaticClassReaderB.SetMultiReadProtect_G2(fComAdr, fPassWord, ferrorcode, frmcomportindex)
        AddCmdLog("SetMultiReadProtect_G2", "Set Single Tag Read Protection without EPC", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Set Single Tag Read Protection without EPC'Command Response=0x00" + "(Set Single Tag Read Protection without EPC successfully)"
        End If
    End Sub

    Private Sub Button_RemoveReadProtect_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_RemoveReadProtect_G2.Click
        If (Edit_AccessCode4.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode4.Text)
        fCmdRet = StaticClassReaderB.RemoveReadProtect_G2(fComAdr, fPassWord, ferrorcode, frmcomportindex)
        AddCmdLog("RemoveReadProtect_G2", "Reset Single Tag Read Protection", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Reset Single Tag Read Protection'Command Response=0x00" + "(Reset Single Tag Read Protection successfully)"
        End If
    End Sub

    Private Sub Button_CheckReadProtected_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_CheckReadProtected_G2.Click
        Dim readpro As Byte = 2
        fCmdRet = StaticClassReaderB.CheckReadProtected_G2(fComAdr, readpro, ferrorcode, frmcomportindex)
        AddCmdLog("CheckReadProtected_G2", "Detect Single Tag Read Protection", fCmdRet)
        If (fCmdRet = 0) Then
            If (readpro = 0) Then
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Detect Single Tag Read Protection'Command Response=0x00" + "(Single Tag is unprotected)"
            End If
            If (readpro = 1) Then
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Detect Single Tag Read Protection'Command Response=0x00" + "(Single Tag is protected)"
            End If
        End If
    End Sub

    Private Sub Button_SetEASAlarm_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_SetEASAlarm_G2.Click
        Dim EPClength As Byte = 0
        Dim EAS As Byte = 0
        Dim ENum1 As Byte
        Dim str As String
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (Edit_AccessCode5.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        If (ComboBox_EPC5.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC5.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC5.SelectedItem.ToString()
        If (str = "") Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        fPassWord = HexStringToByteArray(Edit_AccessCode5.Text)
        If (Alarm_G2.Checked) Then
            EAS = 1
        Else
            EAS = 0
        End If
        fCmdRet = StaticClassReaderB.SetEASAlarm_G2(fComAdr, EPC, fPassWord, Maskadr, MaskLen, MaskFlag, EAS, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("SetEASAlarm_G2", "Alarm Setting", fCmdRet)
        If (fCmdRet = 0) Then
            If (Alarm_G2.Checked) Then
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Alarm Setting'Command Response=0x00" + "(Set EAS Alarm successfully)"
            Else
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Alarm Setting'Command Response=0x00" + "(Clear EAS Alarm successfully)"
            End If
        End If
    End Sub

    Private Sub button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button4.Click
        Timer_G2_Alarm.Enabled = Not Timer_G2_Alarm.Enabled
        If (Timer_G2_Alarm.Enabled) Then
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_WriteEPC_G2.Enabled = False
            Button_SetMultiReadProtect_G2.Enabled = False
            Button_RemoveReadProtect_G2.Enabled = False
            Button_CheckReadProtected_G2.Enabled = False
            button2.Enabled = False

            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_SetProtectState.Enabled = False
            button4.Text = "Stop"
        Else
            If (ListView1_EPC.Items.Count <> 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = True
                Proect2.Enabled = True
                Always2.Enabled = True
                AlwaysNot2.Enabled = True
                P_Reserve.Enabled = True
                P_EPC.Enabled = True
                P_TID.Enabled = True
                P_User.Enabled = True
                Button_DestroyCard.Enabled = True
                Button_SetReadProtect_G2.Enabled = True
                Button_SetEASAlarm_G2.Enabled = True
                Alarm_G2.Enabled = True
                NoAlarm_G2.Enabled = True
                Button_LockUserBlock_G2.Enabled = True
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button2.Enabled = True
                Button_SetProtectState.Enabled = True
                SpeedButton_Read_G2.Enabled = True
                Button_DataWrite.Enabled = True
                Button_BlockWrite.Enabled = True
                Button_BlockErase.Enabled = True
            End If
            If (ListView1_EPC.Items.Count = 0) Then
                DestroyCode.Enabled = False
                AccessCode.Enabled = False
                NoProect.Enabled = False
                Proect.Enabled = False
                Always.Enabled = False
                AlwaysNot.Enabled = False
                NoProect2.Enabled = False
                Proect2.Enabled = False
                Always2.Enabled = False
                AlwaysNot2.Enabled = False
                P_Reserve.Enabled = False
                P_EPC.Enabled = False
                P_TID.Enabled = False
                P_User.Enabled = False
                Button_DestroyCard.Enabled = False
                Button_SetReadProtect_G2.Enabled = False
                Button_SetEASAlarm_G2.Enabled = False
                Alarm_G2.Enabled = False
                NoAlarm_G2.Enabled = False
                Button_LockUserBlock_G2.Enabled = False
                SpeedButton_Read_G2.Enabled = False
                Button_DataWrite.Enabled = False
                Button_BlockWrite.Enabled = False
                Button_BlockErase.Enabled = False
                Button_SetProtectState.Enabled = False
                Button_WriteEPC_G2.Enabled = True
                Button_SetMultiReadProtect_G2.Enabled = True
                Button_RemoveReadProtect_G2.Enabled = True
                Button_CheckReadProtected_G2.Enabled = True
                button2.Enabled = True
            End If
            button4.Text = "Check Alarm"
            Label_Alarm.Visible = False
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Check EAS Alarm'over"
        End If
    End Sub

    Private Sub Timer_G2_Alarm_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_G2_Alarm.Tick
        If (fIsInventoryScan) Then
            Exit Sub
        End If
        fIsInventoryScan = True
        fCmdRet = StaticClassReaderB.CheckEASAlarm_G2(fComAdr, ferrorcode, frmcomportindex)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Check EAS Alarm'Command Response=0x00" + "(EAS alarm detected)"
            Label_Alarm.Visible = True
        Else
            Label_Alarm.Visible = False
            AddCmdLog("CheckEASAlarm_G2", "Check EAS Alarm", fCmdRet)
        End If
        fIsInventoryScan = False
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Button_LockUserBlock_G2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_LockUserBlock_G2.Click
        Dim EPClength As Byte = 0
        Dim BlockNum As Byte = 0
        Dim ENum1 As Byte
        Dim str As String
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (Edit_AccessCode6.Text.Length < 8) Then
            MessageBox.Show("Access Password Less Than 8 digit!Please input again!", "Information")
            Exit Sub
        End If
        If (ComboBox_EPC6.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC6.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC6.SelectedItem.ToString()
        If (str = "") Then
            Exit Sub
        End If
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(str.Length / 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        fPassWord = HexStringToByteArray(Edit_AccessCode6.Text)
        BlockNum = Convert.ToByte(ComboBox_BlockNum.SelectedIndex * 2)
        fCmdRet = StaticClassReaderB.LockUserBlock_G2(fComAdr, EPC, fPassWord, Maskadr, MaskLen, MaskFlag, BlockNum, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("LockUserBlock_G2", "Lock User Block", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Lock User Block'Command Response=0x00" + "(Lock successfully)"
        End If
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Timer_Test_.Enabled = False
        Timer_G2_Read.Enabled = False
        Timer_G2_Alarm.Enabled = False
        fAppClosed = True
        StaticClassReaderB.CloseComPort()
    End Sub

    Private Sub ComboBox_IntervalTime_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_IntervalTime.SelectedIndexChanged, ComboBox_IntervalTime_6B.SelectedIndexChanged
        If (ComboBox_IntervalTime.SelectedIndex < 6) Then
            Timer_Test_.Interval = 100
        Else
            Timer_Test_.Interval = (ComboBox_IntervalTime.SelectedIndex + 4) * 10
        End If
    End Sub

    Private Sub SpeedButton_Query_6B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeedButton_Query_6B.Click
        Timer_Test_6B.Enabled = Not Timer_Test_6B.Enabled
        If (Not Timer_Test_6B.Enabled) Then

            If (ListView_ID_6B.Items.Count <> 0) Then
                SpeedButton_Read_6B.Enabled = True
                SpeedButton_Write_6B.Enabled = True
                Button14.Enabled = True
                Button15.Enabled = True
                If (Bycondition_6B.Checked) Then
                    Same_6B.Enabled = True
                    Different_6B.Enabled = True
                    Less_6B.Enabled = True
                    Greater_6B.Enabled = True
                End If
            End If
            If (ListView_ID_6B.Items.Count = 0) Then
                SpeedButton_Read_6B.Enabled = False
                SpeedButton_Write_6B.Enabled = False
                Button14.Enabled = False
                Button15.Enabled = False
                If (Bycondition_6B.Checked) Then
                    Same_6B.Enabled = True
                    Different_6B.Enabled = True
                    Less_6B.Enabled = True
                    Greater_6B.Enabled = True
                End If
            End If
            AddCmdLog("Inventory", "Exit Query", 0)
            If (Byone_6B.Checked) Then
                SpeedButton_Query_6B.Text = "Query "
            Else
                SpeedButton_Query_6B.Text = "Query "
            End If
        Else
            SpeedButton_Read_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            Same_6B.Enabled = False
            Different_6B.Enabled = False
            Less_6B.Enabled = False
            Greater_6B.Enabled = False
            ListView_ID_6B.Items.Clear()
            ComboBox_ID1_6B.Items.Clear()
            CardNum1 = 0
            list.Clear()
            SpeedButton_Query_6B.Text = "Stop"
        End If
    End Sub
    Public Sub ChangeSubItem1(ByVal ListItem As ListViewItem, ByVal subItemIndex As Integer, ByVal ItemText As String)
        If (subItemIndex = 1) Then
            If (ListItem.SubItems(subItemIndex).Text <> ItemText) Then
                ListItem.SubItems(subItemIndex).Text = ItemText
                ListItem.SubItems(subItemIndex + 1).Text = "1"
            Else
                ListItem.SubItems(subItemIndex + 1).Text = Convert.ToString(Convert.ToInt32(ListItem.SubItems(subItemIndex + 1).Text) + 1)
                If ((Convert.ToUInt32(ListItem.SubItems(subItemIndex + 1).Text) > 9999)) Then
                    ListItem.SubItems(subItemIndex + 1).Text = "1"
                End If
            End If
        End If
    End Sub

    Private Sub Bycondition_6B_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Bycondition_6B.CheckedChanged
        SpeedButton_Query_6B.Text = "Query"
        If ((Not Timer_6B_Read.Enabled) And (Not Timer_6B_Write.Enabled) And (Not Timer_Test_6B.Enabled)) Then
            Same_6B.Enabled = True
            Different_6B.Enabled = True
            Less_6B.Enabled = True
            Greater_6B.Enabled = True
        End If
    End Sub

    Private Sub Byone_6B_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Byone_6B.CheckedChanged
        SpeedButton_Query_6B.Text = "Query"
        If ((Not Timer_6B_Read.Enabled) And (Not Timer_6B_Write.Enabled) And (Not Timer_Test_6B.Enabled)) Then
            Same_6B.Enabled = False
            Different_6B.Enabled = False
            Less_6B.Enabled = False
            Greater_6B.Enabled = False
        End If
    End Sub
    Private Sub Inventory_6B()
        Dim CardNum As Integer = 0
        Dim ID_6B(2000) As Byte
        Dim ID2_6B(5000) As Byte
        Dim isonlistview As Boolean
        Dim temps As String
        Dim s, ss, sID As String
        Dim aListItem As ListViewItem
        Dim i, j As Integer
        Dim Condition As Byte = 0
        Dim StartAddress As Byte
        Dim mask As Byte = 0
        Dim ConditionContent(300) As Byte
        Dim Contentlen As Byte
        If (Byone_6B.Checked) Then
            fCmdRet = StaticClassReaderB.Inventory_6B(fComAdr, ID_6B, frmcomportindex)
            If (fCmdRet = 0) Then
                Dim daw(7) As Byte
                Array.Copy(ID_6B, daw, 8)
                temps = ByteArrayToHexString(daw)
                If (list.IndexOf(temps) = -1) Then
                    CardNum1 = CardNum1 + 1
                    list.Add(temps)
                End If
                While (ListView_ID_6B.Items.Count < CardNum1)

                    aListItem = ListView_ID_6B.Items.Add((ListView_ID_6B.Items.Count + 1).ToString())
                    aListItem.SubItems.Add("")
                    aListItem.SubItems.Add("")
                    aListItem.SubItems.Add("")
                End While
                isonlistview = False
                For i = 0 To CardNum1 - 1
                    If (temps = ListView_ID_6B.Items(i).SubItems(1).Text) Then
                        aListItem = ListView_ID_6B.Items(i)
                        ChangeSubItem1(aListItem, 1, temps)
                        isonlistview = True
                    End If
                Next i
                If (Not isonlistview) Then
                    aListItem = ListView_ID_6B.Items(CardNum1 - 1)
                    s = temps
                    ChangeSubItem1(aListItem, 1, s)
                    If (ComboBox_EPC1.Items.IndexOf(s) = -1) Then
                        ComboBox_ID1_6B.Items.Add(temps)
                    End If
                End If
            End If

            If (ComboBox_ID1_6B.Items.Count <> 0) Then
                ComboBox_ID1_6B.SelectedIndex = 0
            End If
        End If
        If (Bycondition_6B.Checked) Then
            If (Same_6B.Checked) Then
                Condition = 0
            ElseIf (Different_6B.Checked) Then
                Condition = 1
            ElseIf (Greater_6B.Checked) Then
                Condition = 2
            ElseIf (Less_6B.Checked) Then
                Condition = 3
            End If
            If (Edit_ConditionContent_6B.Text = "") Then
                Exit Sub
            End If
            ss = Edit_ConditionContent_6B.Text
            Contentlen = Convert.ToByte((Edit_ConditionContent_6B.Text).Length)
            For i = 0 To 15 - Contentlen
                ss = ss + "0"
            Next i
            Dim Nlen As Integer
            Nlen = (ss.Length) / 2
            Dim daw(Nlen) As Byte
            daw = HexStringToByteArray(ss)
            Select Case Contentlen / 2
                Case 1
                    mask = &H80
                Case 2
                    mask = &HC0
                Case 3
                    mask = &HE0
                Case 4
                    mask = &HF0
                Case 5
                    mask = &HF8
                Case 6
                    mask = &HFC
                Case 7
                    mask = &HFE
                Case 8
                    mask = &HFF
            End Select
            If (Edit_Query_StartAddress_6B.Text = "") Then
                Exit Sub
            End If
            StartAddress = Convert.ToByte(Edit_Query_StartAddress_6B.Text)
            fCmdRet = StaticClassReaderB.inventory2_6B(fComAdr, Condition, StartAddress, mask, daw, ID2_6B, CardNum, frmcomportindex)
            If ((fCmdRet = &H15) Or (fCmdRet = &H16) Or (fCmdRet = &H17) Or (fCmdRet = &H18) Or (fCmdRet = &HFB)) Then
                Dim daw1(CardNum * 8) As Byte
                Array.Copy(ID2_6B, daw1, CardNum * 8)
                temps = ByteArrayToHexString(daw1)
                For i = 0 To CardNum - 1
                    sID = temps.Substring(16 * i, 16)
                    If ((sID.Length) <> 16) Then
                        Exit Sub
                    End If
                    If (CardNum = 0) Then
                        Exit Sub
                    End If
                    While (ListView_ID_6B.Items.Count < CardNum)
                        aListItem = ListView_ID_6B.Items.Add((ListView_ID_6B.Items.Count + 1).ToString())
                        aListItem.SubItems.Add("")
                        aListItem.SubItems.Add("")
                        aListItem.SubItems.Add("")
                    End While
                    isonlistview = False
                    For j = 0 To ListView_ID_6B.Items.Count - 1
                        If (sID = ListView_ID_6B.Items(j).SubItems(1).Text) Then
                            aListItem = ListView_ID_6B.Items(j)
                            ChangeSubItem1(aListItem, 1, sID)
                            isonlistview = True
                        End If
                    Next j
                    If (Not isonlistview) Then
                        aListItem = ListView_ID_6B.Items(i)
                        s = sID
                        ChangeSubItem1(aListItem, 1, s)
                        If (ComboBox_EPC1.Items.IndexOf(s) = -1) Then
                            ComboBox_ID1_6B.Items.Add(sID)
                        End If
                    End If
                Next i
                If (ComboBox_ID1_6B.Items.Count <> 0) Then
                    ComboBox_ID1_6B.SelectedIndex = 0
                End If
            End If
        End If
        If (Timer_Test_6B.Enabled) Then
            If (Bycondition_6B.Checked) Then
                If (fCmdRet <> 0) Then
                    AddCmdLog("Inventory", "Query tag", fCmdRet)
                ElseIf (fCmdRet = &HFB) Then
                    StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Query Tag'Command Response=0xFB" + "(No Tag Operable)"
                ElseIf (fCmdRet = 0) Then
                    StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Query Tag'Command Response=0x00" + "(Find a Tag)"
                Else
                    AddCmdLog("Inventory", "Query Tag", fCmdRet)
                End If
                If (fCmdRet = &HEE) Then
                    StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Query Tag'Command Response=0xee" + "(Response Command Error)"
                End If
            End If
        End If
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Timer_Test_6B_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_Test_6B.Tick
        If (fisinventoryscan_6B) Then
            Exit Sub
        End If
        fisinventoryscan_6B = True
        Inventory_6B()
        fisinventoryscan_6B = False
    End Sub

    Private Sub SpeedButton_Read_6B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeedButton_Read_6B.Click
        If ((Edit_StartAddress_6B.Text = "") Or (Edit_Len_6B.Text = "")) Then
            MessageBox.Show("Start address or length is empty!Please input!", "Information")
            Exit Sub
        End If
        Timer_6B_Read.Enabled = Not Timer_6B_Read.Enabled
        If (Not Timer_6B_Read.Enabled) Then
            AddCmdLog("Read", "Exit Read", 0)
            SpeedButton_Read_6B.Text = "Read "
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Write_6B.Enabled = True
            Button14.Enabled = True
            Button15.Enabled = True
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        Else
            SpeedButton_Query_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = False
                Different_6B.Enabled = False
                Less_6B.Enabled = False
                Greater_6B.Enabled = False
            End If
            SpeedButton_Read_6B.Text = "Stop"
        End If
    End Sub
    Private Sub Read_6B()
        Dim temp, temps As String
        Dim CardData(320) As Byte
        Dim ID_6B(7) As Byte
        Dim Num, StartAddress As Byte
        If (ComboBox_ID1_6B.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_ID1_6B.SelectedItem = Nothing) Then
            Exit Sub
        End If
        temp = ComboBox_ID1_6B.SelectedItem.ToString()
        If (temp = "") Then
            Exit Sub
        End If
        ID_6B = HexStringToByteArray(temp)
        If (Edit_StartAddress_6B.Text = "") Then
            Exit Sub
        End If
        StartAddress = Convert.ToByte(Edit_StartAddress_6B.Text, 16)
        If (Edit_Len_6B.Text = "") Then
            Exit Sub
        End If
        Num = Convert.ToByte(Edit_Len_6B.Text)
        fCmdRet = StaticClassReaderB.ReadCard_6B(fComAdr, ID_6B, StartAddress, Num, CardData, ferrorcode, frmcomportindex)
        If (fCmdRet = 0) Then
            Dim data(Num - 1) As Byte
            Array.Copy(CardData, data, Num)
            temps = ByteArrayToHexString(data)
            listBox2.Items.Add(temps)
        End If
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Timer_6B_Read_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_6B_Read.Tick
        If (fTimer_6B_ReadWrite) Then
            Exit Sub
        End If
        fTimer_6B_ReadWrite = True
        Read_6B()
        fTimer_6B_ReadWrite = False
    End Sub

    Private Sub SpeedButton_Write_6B_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SpeedButton_Write_6B.Click
        If ((Edit_WriteData_6B.Text = "") Or ((Edit_WriteData_6B.Text.Length Mod 2) <> 0)) Then
            MessageBox.Show("Please input in bytes in hexadecimal form!", "Information")
            Exit Sub
        End If
        If ((Edit_StartAddress_6B.Text = "") Or (Edit_Len_6B.Text = "")) Then
            MessageBox.Show("Start address or length is empty!Please input!", "Information")
            Exit Sub
        End If
        Timer_6B_Write.Enabled = Not Timer_6B_Write.Enabled
        If (Not Timer_6B_Write.Enabled) Then
            AddCmdLog("Wtite", "Exit Write", 0)
            SpeedButton_Write_6B.Text = "Write "
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Read_6B.Enabled = True
            Button14.Enabled = True
            Button15.Enabled = True
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        Else
            SpeedButton_Query_6B.Enabled = False
            SpeedButton_Read_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = False
                Different_6B.Enabled = False
                Less_6B.Enabled = False
                Greater_6B.Enabled = False
            End If
            SpeedButton_Write_6B.Text = "Stop"
        End If
    End Sub
    Private Sub Write_6B()
        Dim temp As String
        Dim CardData(320) As Byte
        Dim ID_6B(7) As Byte
        Dim StartAddress As Byte
        Dim Writedatalen As Byte
        Dim writtenbyte As Integer = 0
        If (ComboBox_ID1_6B.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_ID1_6B.SelectedItem = Nothing) Then
            Exit Sub
        End If
        temp = ComboBox_ID1_6B.SelectedItem.ToString()
        If (temp = "") Then
            Exit Sub
        End If
        ID_6B = HexStringToByteArray(temp)
        If (Edit_StartAddress_6B.Text = "") Then
            Exit Sub
        End If
        StartAddress = Convert.ToByte(Edit_StartAddress_6B.Text, 16)
        If ((Edit_WriteData_6B.Text = "") Or (Edit_WriteData_6B.Text.Length Mod 2) <> 0) Then
            Exit Sub
        End If
        Writedatalen = Convert.ToByte(Edit_WriteData_6B.Text.Length)
        Dim Writedata(Writedatalen) As Byte
        Writedata = HexStringToByteArray(Edit_WriteData_6B.Text)
        fCmdRet = StaticClassReaderB.WriteCard_6B(fComAdr, ID_6B, StartAddress, Writedata, Writedatalen, writtenbyte, ferrorcode, frmcomportindex)
        AddCmdLog("WriteCard", "Stop", fCmdRet)
        If (fAppClosed) Then
            Close()
        End If
    End Sub

    Private Sub Timer_6B_Write_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer_6B_Write.Tick
        If (fTimer_6B_ReadWrite) Then
            Exit Sub
        End If
        fTimer_6B_ReadWrite = True
        Write_6B()
        fTimer_6B_ReadWrite = False
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        Dim Address As Byte
        Dim temps As String
        Dim ID_6B(7) As Byte
        If (ComboBox_ID1_6B.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_ID1_6B.SelectedItem = Nothing) Then
            Exit Sub
        End If
        temps = ComboBox_ID1_6B.SelectedItem.ToString()
        If (temps = "") Then
            Exit Sub
        End If
        ID_6B = HexStringToByteArray(temps)
        If (Edit_StartAddress_6B.Text = "") Then
            Exit Sub
        End If
        Address = Convert.ToByte(Edit_StartAddress_6B.Text)
        If (MessageBox.Show(Me, "permanently Lock the address Confirmed?", "Information", MessageBoxButtons.OKCancel) = DialogResult.Cancel) Then
            Exit Sub
        End If
        fCmdRet = StaticClassReaderB.LockByte_6B(fComAdr, ID_6B, Address, ferrorcode, frmcomportindex)
        AddCmdLog("LockByte_6B", "Lock", fCmdRet)
    End Sub

    Private Sub Button15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button15.Click
        Dim Address As Byte
        Dim ReLockState As Byte = 2
        Dim temps As String
        Dim ID_6B(7) As Byte
        If (ComboBox_ID1_6B.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_ID1_6B.SelectedItem = Nothing) Then
            Exit Sub
        End If
        temps = ComboBox_ID1_6B.SelectedItem.ToString()
        If (temps = "") Then
            Exit Sub
        End If
        ID_6B = HexStringToByteArray(temps)
        If (Edit_StartAddress_6B.Text = "") Then
            Exit Sub
        End If
        Address = Convert.ToByte(Edit_StartAddress_6B.Text)
        fCmdRet = StaticClassReaderB.CheckLock_6B(fComAdr, ID_6B, Address, ReLockState, ferrorcode, frmcomportindex)
        AddCmdLog("CheckLock_6B", "Check Lock", fCmdRet)
        If (fCmdRet = 0) Then
            If (ReLockState = 0) Then
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Check Lock'Command Response=0x00" + "(The Byte is unlocked)"
            End If
            If (ReLockState = 1) Then
                StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + " 'Check Lock'Command Response=0x01" + "(The Byte is locked)"
            End If
        End If
    End Sub

    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        listBox2.Items.Clear()
    End Sub

    Private Sub C_Reserve_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_Reserve.CheckedChanged
        If ((Not Timer_Test_.Enabled) And (Not Timer_G2_Alarm.Enabled) And (Not Timer_G2_Read.Enabled)) Then
            If (ListView1_EPC.Items.Count <> 0) Then
                Button_DataWrite.Enabled = True
            End If
        End If
        Edit_WordPtr.ReadOnly = False
    End Sub

    Private Sub C_TID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_TID.CheckedChanged
        If ((Not Timer_Test_.Enabled) And (Not Timer_G2_Alarm.Enabled) And (Not Timer_G2_Read.Enabled)) Then
            If (ListView1_EPC.Items.Count <> 0) Then
                Button_DataWrite.Enabled = True
            End If
        End If
        Edit_WordPtr.ReadOnly = False
    End Sub

    Private Sub C_User_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_User.CheckedChanged
        If ((Not Timer_Test_.Enabled) And (Not Timer_G2_Alarm.Enabled) And (Not Timer_G2_Read.Enabled)) Then
            If (ListView1_EPC.Items.Count <> 0) Then
                Button_DataWrite.Enabled = True
            End If
        End If
        Edit_WordPtr.ReadOnly = False
    End Sub

    Private Sub C_EPC_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles C_EPC.CheckedChanged
        If ((Not Timer_Test_.Enabled) And (Not Timer_G2_Alarm.Enabled) And (Not Timer_G2_Read.Enabled)) Then
            If (ListView1_EPC.Items.Count <> 0) Then
                Button_DataWrite.Enabled = True
            End If
        End If
        If (checkBox_pc.Checked) Then
            Edit_WordPtr.Text = "02"
            Edit_WordPtr.ReadOnly = True
        Else
            Edit_WordPtr.ReadOnly = False
        End If
    End Sub

    Private Sub tabControl1_Selecting(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs)
        timer1.Enabled = False
        button10.Text = "Get"

        Timer_G2_Alarm.Enabled = False
        Timer_G2_Read.Enabled = False
        Timer_Test_.Enabled = False
        SpeedButton_Read_G2.Text = "Read"
        button2.Text = "Query Tag"
        button4.Text = "Check Alarm"
        If ((ListView1_EPC.Items.Count <> 0) And (ComOpen)) Then
            button2.Enabled = True
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = True
            Proect2.Enabled = True
            Always2.Enabled = True
            AlwaysNot2.Enabled = True
            P_Reserve.Enabled = True
            P_EPC.Enabled = True
            P_TID.Enabled = True
            P_User.Enabled = True
            Button_DestroyCard.Enabled = True
            Button_SetReadProtect_G2.Enabled = True
            Button_SetEASAlarm_G2.Enabled = True
            Alarm_G2.Enabled = True
            NoAlarm_G2.Enabled = True
            Button_LockUserBlock_G2.Enabled = True
            Button_WriteEPC_G2.Enabled = True
            Button_SetMultiReadProtect_G2.Enabled = True
            Button_RemoveReadProtect_G2.Enabled = True
            Button_CheckReadProtected_G2.Enabled = True
            button4.Enabled = True
            SpeedButton_Read_G2.Enabled = True
            Button_SetProtectState.Enabled = True
            Button_DataWrite.Enabled = True
            Button_BlockWrite.Enabled = True
            Button_BlockErase.Enabled = True
        End If
        If ((ListView1_EPC.Items.Count = 0) And (ComOpen)) Then
            button2.Enabled = True
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_WriteEPC_G2.Enabled = True
            Button_SetMultiReadProtect_G2.Enabled = True
            Button_RemoveReadProtect_G2.Enabled = True
            Button_CheckReadProtected_G2.Enabled = True
            button4.Enabled = True
            Button_SetProtectState.Enabled = False
        End If

        Timer_Test_6B.Enabled = False
        Timer_6B_Read.Enabled = False
        Timer_6B_Write.Enabled = False
        SpeedButton_Query_6B.Text = "Query"
        SpeedButton_Read_6B.Text = "Read"
        SpeedButton_Write_6B.Text = "Write"
        If ((ListView_ID_6B.Items.Count <> 0) And (ComOpen)) Then
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Read_6B.Enabled = True
            SpeedButton_Write_6B.Enabled = True
            Button14.Enabled = True
            Button15.Enabled = True
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        End If
        If ((ListView_ID_6B.Items.Count = 0) And (ComOpen)) Then
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Read_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        End If
    End Sub

    Private Sub Edit_CmdComAddr_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Edit_WriteData.KeyPress, Edit_WordPtr.KeyPress, Edit_AccessCode2.KeyPress, textBox2.KeyPress, Edit_AccessCode6.KeyPress, Edit_AccessCode5.KeyPress, Edit_AccessCode4.KeyPress, Edit_AccessCode3.KeyPress, Edit_WriteEPC.KeyPress, Edit_DestroyCode.KeyPress, Edit_WriteData_6B.KeyPress, Edit_StartAddress_6B.KeyPress, maskLen_textBox.KeyPress, maskadr_textbox.KeyPress, textBox5.KeyPress, textBox4.KeyPress
        e.Handled = ("0123456789ABCDEF".IndexOf(Char.ToUpper(e.KeyChar)) < 0)
    End Sub

    Private Sub Edit_Len_6B_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Edit_Len_6B.KeyPress, textBox1.KeyPress
        e.Handled = ("0123456789".IndexOf(Char.ToUpper(e.KeyChar)) < 0)
    End Sub

    Private Sub comboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles comboBox4.SelectedIndexChanged
        Dim i = 0
        If (comboBox4.SelectedIndex = 0) Then
            radioButton5.Enabled = False
            radioButton6.Enabled = False
            radioButton7.Enabled = False
            radioButton8.Enabled = False
            radioButton9.Enabled = False
            radioButton10.Enabled = False
            radioButton11.Enabled = False
            radioButton12.Enabled = False
            radioButton13.Enabled = False
            radioButton14.Enabled = False
            radioButton15.Enabled = False
            RadioButton16.Enabled = False
            RadioButton17.Enabled = False
            RadioButton18.Enabled = False
            RadioButton19.Enabled = False
            radioButton20.Enabled = False
            textBox3.Enabled = False
            comboBox5.Enabled = False
            ComboBox6.Enabled = False
        End If
        If ((comboBox4.SelectedIndex = 1) Or (comboBox4.SelectedIndex = 2) Or (comboBox4.SelectedIndex = 3)) Then
            radioButton5.Enabled = True
            radioButton6.Enabled = True
            radioButton7.Enabled = True
            radioButton8.Enabled = True
            radioButton20.Enabled = True
            comboBox5.Items.Clear()
            If (radioButton20.Checked) Then
                For i = 1 To 4
                    comboBox5.Items.Add(Convert.ToString(i))
                Next i
                comboBox5.SelectedIndex = 3
                label42.Text = "Read Byte Number:"
            Else
                For i = 1 To 32
                    comboBox5.Items.Add(Convert.ToString(i))
                Next i
                comboBox5.SelectedIndex = 0
                label42.Text = "Read Word Number:"
            End If

            If (radioButton7.Checked) Then
                RadioButton16.Enabled = True
                RadioButton17.Enabled = True
            Else
                RadioButton16.Enabled = False
                RadioButton17.Enabled = False
            End If
            If (radioButton5.Checked) Then
                radioButton9.Enabled = True
                radioButton10.Enabled = True
                radioButton11.Enabled = True
                radioButton12.Enabled = True
                RadioButton18.Enabled = True
                If (radioButton20.Checked) Then
                    radioButton13.Enabled = False
                    RadioButton19.Enabled = False
                Else
                    radioButton13.Enabled = True
                    RadioButton19.Enabled = True
                End If
                If ((radioButton13.Checked) Or (RadioButton19.Checked)) Then
                    ComboBox6.Enabled = False
                Else
                    ComboBox6.Enabled = True
                End If
            Else
                ComboBox6.Enabled = True
            End If
            radioButton14.Enabled = True
            radioButton15.Enabled = True
            textBox3.Enabled = True
            If (radioButton7.Checked) Then
                comboBox5.Enabled = False
            Else
                comboBox5.Enabled = True
            End If

        End If
    End Sub

    Private Sub radioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton6.CheckedChanged, radioButton5.CheckedChanged
        If (radioButton5.Checked) Then
            If ((comboBox4.SelectedIndex = 1) Or (comboBox4.SelectedIndex = 2) Or (comboBox4.SelectedIndex = 3)) Then
                radioButton9.Enabled = True
                radioButton10.Enabled = True
                radioButton11.Enabled = True
                radioButton12.Enabled = True

                radioButton13.Enabled = True
                RadioButton18.Enabled = True
                If (RadioButton16.Checked) Then
                    label41.Text = "First Word Addr(Hex):"
                Else
                    label41.Text = "First Byte Addr(Hex):"
                End If
                If (radioButton20.Checked) Then
                    radioButton13.Enabled = False
                    RadioButton19.Enabled = False
                    label41.Text = "First Byte Addr(Hex):"
                Else
                    radioButton13.Enabled = True
                    RadioButton19.Enabled = True
                End If
                If (radioButton7.Checked) Then
                    RadioButton16.Enabled = True
                    RadioButton17.Enabled = True
                    If ((radioButton13.Checked) Or (RadioButton19.Checked)) Then
                        ComboBox6.Enabled = False
                    Else
                        ComboBox6.Enabled = True
                    End If
                Else
                    RadioButton16.Enabled = False
                    RadioButton17.Enabled = False
                    If ((radioButton13.Checked) Or (RadioButton19.Checked)) Then
                        ComboBox6.Enabled = False
                    Else
                        ComboBox6.Enabled = True
                    End If
                    If (radioButton20.Checked) Then
                        label41.Text = "First Byte Addr(Hex):"
                    Else
                        label41.Text = "First Word Addr(Hex):"
                    End If
                End If
            End If
        Else
        radioButton9.Enabled = False
        radioButton10.Enabled = False
        radioButton11.Enabled = False
        radioButton12.Enabled = False
        radioButton13.Enabled = False
        RadioButton18.Enabled = False
        RadioButton16.Enabled = False
        RadioButton17.Enabled = False
        RadioButton19.Enabled = False
        ComboBox6.Enabled = True
        label41.Text = "First Byte Addr(Hex)"
        end if
    End Sub

    Private Sub radioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton7.CheckedChanged
        If (radioButton5.Checked) And (comboBox4.SelectedIndex > 0) Then
            RadioButton16.Enabled = True
            RadioButton17.Enabled = True
            radioButton13.Enabled = True
            RadioButton19.Enabled = True
            If (RadioButton16.Checked) Then
                label41.Text = "First Word Addr(Hex):"
            Else
                label41.Text = "First Byte Addr(Hex):"
            End If
            label42.Text = "Read Word Number:"
        End If
        comboBox5.Enabled = False
    End Sub

    Private Sub radioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton8.CheckedChanged, radioButton20.CheckedChanged
        Dim i = 0
        If ((comboBox4.SelectedIndex = 1) Or (comboBox4.SelectedIndex = 2) Or (comboBox4.SelectedIndex = 3)) Then
            If (radioButton8.Checked) Then
                comboBox5.Enabled = True
            End If
            comboBox5.Items.Clear()
            If (radioButton20.Checked) Then
                For i = 1 To 4
                    comboBox5.Items.Add(Convert.ToString(i))
                Next i
                comboBox5.SelectedIndex = 3
                label42.Text = "Read Byte Number:"
                comboBox5.Enabled = True
                label41.Text = "First Byte Addr(Hex):"
            Else
                For i = 1 To 32
                    comboBox5.Items.Add(Convert.ToString(i))
                Next i
                comboBox5.SelectedIndex = 0
                label42.Text = "Read Word Number:"
                label41.Text = "First Word Addr(Hex):"
            End If
            If (radioButton5.Checked) Then
                RadioButton16.Enabled = False
                RadioButton17.Enabled = False
                If (radioButton20.Checked) Then
                    radioButton13.Enabled = False
                    RadioButton19.Enabled = False
                Else
                    radioButton13.Enabled = True
                    RadioButton19.Enabled = True
                End If
            Else
                label41.Text = "First Byte Addr(Hex):"
                radioButton13.Enabled = False
                RadioButton19.Enabled = False
            End If
        End If
    End Sub

    Private Sub button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button6.Click
        Dim Wg_mode As Byte = 0
        Dim Wg_Data_Inteval As Byte
        Dim Wg_Pulse_Width As Byte
        Dim Wg_Pulse_Inteval As Byte
        If (radioButton1.Checked) Then
            If (radioButton3.Checked) Then
                Wg_mode = 2
            Else
                Wg_mode = 0
            End If
        End If
        If (radioButton2.Checked) Then
            If (radioButton3.Checked) Then
                Wg_mode = 3
            Else
                Wg_mode = 1
            End If
        End If
        Wg_Data_Inteval = Convert.ToByte(comboBox1.SelectedIndex)
        Wg_Pulse_Width = Convert.ToByte(comboBox3.SelectedIndex + 1)
        Wg_Pulse_Inteval = Convert.ToByte(comboBox2.SelectedIndex + 1)
        fCmdRet = StaticClassReaderB.SetWGParameter(fComAdr, Wg_mode, Wg_Data_Inteval, Wg_Pulse_Width, Wg_Pulse_Inteval, frmcomportindex)
        AddCmdLog("SetWGParameter", "SetWGParameter", fCmdRet)
    End Sub

    Private Sub button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button8.Click
        Dim Reader_bit0, Reader_bit1, Reader_bit2, Reader_bit3, Reader_bit4 As Integer
        Dim Parameter(5) As Byte
        Parameter(0) = Convert.ToByte(comboBox4.SelectedIndex)
        If (radioButton5.Checked) Then
            Reader_bit0 = 0
        Else
            Reader_bit0 = 1
        End If
        If (radioButton7.Checked) Then
            Reader_bit1 = 0
        Else
            Reader_bit1 = 1
        End If
        If (radioButton14.Checked) Then
            Reader_bit2 = 0
        Else
            Reader_bit2 = 1
        End If
        If (RadioButton16.Checked) Then
            Reader_bit3 = 0
        Else
            Reader_bit3 = 1
        End If
        If (radioButton20.Checked) Then
            Reader_bit4 = 1
        Else
            Reader_bit4 = 0
        End If
        Parameter(1) = Convert.ToByte(Reader_bit0 * 1 + Reader_bit1 * 2 + Reader_bit2 * 4 + Reader_bit3 * 8 + Reader_bit4 * 16)
        If (radioButton9.Checked) Then
            Parameter(2) = 0
        End If
        If (radioButton10.Checked) Then
            Parameter(2) = 1
        End If
        If (radioButton11.Checked) Then
            Parameter(2) = 2
        End If
        If (radioButton12.Checked) Then
            Parameter(2) = 3
        End If
        If (radioButton13.Checked) Then
            Parameter(2) = 4
        End If
        If (RadioButton18.Checked) Then
            Parameter(2) = 5
        End If
        If (RadioButton19.Checked) Then
            Parameter(2) = 6
        End If
        If (textBox3.Text = "") Then
            MessageBox.Show("Address is NULL!", "Information")
            Exit Sub
        End If
        Parameter(3) = Convert.ToByte(textBox3.Text, 16)
        Parameter(4) = Convert.ToByte(comboBox5.SelectedIndex + 1)
        Parameter(5) = Convert.ToByte(ComboBox6.SelectedIndex)
        fCmdRet = StaticClassReaderB.SetWorkMode(fComAdr, Parameter, frmcomportindex)
        If (fCmdRet = 0) Then
            If ((comboBox4.SelectedIndex = 1) Or (comboBox4.SelectedIndex = 2) Or (comboBox4.SelectedIndex = 3)) Then
                If (radioButton6.Checked) Then
                    radioButton13.Enabled = False
                    RadioButton19.Enabled = False
                Else
                    If (radioButton20.Checked) Then
                        radioButton13.Enabled = False
                        RadioButton19.Enabled = False
                    End If
                End If
                button10.Enabled = True
                button11.Enabled = True
            End If
                If (comboBox4.SelectedIndex = 0) Then
                    button10.Enabled = False
                    button11.Enabled = False
                    button10.Text = "Get"
                    timer1.Enabled = False
                End If
            End If
        AddCmdLog("SetWorkMode", "SetWorkMode", fCmdRet)
    End Sub



    Private Sub button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button10.Click
        timer1.Enabled = Not timer1.Enabled
        If (Not timer1.Enabled) Then
            button10.Text = "Get"
        Else
            button10.Text = "Stop"
        End If
    End Sub
    Private Sub GetData()
        Dim ScanModeData(40960) As Byte
        Dim ValidDatalength, i As Integer
        Dim temp, temps As String
        ValidDatalength = 0
        fCmdRet = StaticClassReaderB.ReadActiveModeData(ScanModeData, ValidDatalength, frmcomportindex)
        If (fCmdRet = 0) Then
            temp = ""
            temps = ByteArrayToHexString(ScanModeData)
            For i = 0 To ValidDatalength - 1
                temp = temp + temps.Substring(i * 2, 2) + " "
            Next i
            listBox3.Items.Add(temp)
            listBox3.SelectedIndex = listBox3.Items.Count - 1
        End If
        AddCmdLog("Get", "Get", fCmdRet)
    End Sub

    Private Sub timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer1.Tick
        If (fIsInventoryScan) Then
            fIsInventoryScan = True
        End If
        GetData()
        If (fAppClosed) Then
            Close()
        End If
        fIsInventoryScan = False
    End Sub

    Private Sub button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button11.Click
        listBox3.Items.Clear()
    End Sub

    Private Sub radioButton_band1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton_band1.CheckedChanged
        Dim i As Integer
        ComboBox_dmaxfre.Items.Clear()
        ComboBox_dminfre.Items.Clear()
        For i = 0 To 62
            ComboBox_dminfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(902.6 + i * 0.4) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 62
        ComboBox_dminfre.SelectedIndex = 0
    End Sub

    Private Sub radioButton_band2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton_band2.CheckedChanged
        Dim i As Integer
        ComboBox_dmaxfre.Items.Clear()
        ComboBox_dminfre.Items.Clear()
        For i = 0 To 19
            ComboBox_dminfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(920.125 + i * 0.25) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 19
        ComboBox_dminfre.SelectedIndex = 0
    End Sub

    Private Sub radioButton_band3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton_band3.CheckedChanged
        Dim i As Integer
        ComboBox_dmaxfre.Items.Clear()
        ComboBox_dminfre.Items.Clear()
        For i = 0 To 49
            ComboBox_dminfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(902.75 + i * 0.5) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 49
        ComboBox_dminfre.SelectedIndex = 0
    End Sub

    Private Sub radioButton_band4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton_band4.CheckedChanged
        Dim i As Integer
        ComboBox_dmaxfre.Items.Clear()
        ComboBox_dminfre.Items.Clear()
        For i = 0 To 31
            ComboBox_dminfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(917.1 + i * 0.2) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 31
        ComboBox_dminfre.SelectedIndex = 0
    End Sub

    Private Sub checkBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBox1.CheckedChanged
        If (checkBox1.Checked) Then
            maskadr_textbox.Enabled = True
            maskLen_textBox.Enabled = True
        Else
            maskadr_textbox.Enabled = False
            maskLen_textBox.Enabled = False
        End If
    End Sub

    Private Sub radioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton9.CheckedChanged, radioButton10.CheckedChanged, RadioButton18.CheckedChanged, radioButton12.CheckedChanged, radioButton11.CheckedChanged
        ComboBox6.Enabled = True
    End Sub

    Private Sub radioButton13_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton13.CheckedChanged, RadioButton19.CheckedChanged
        ComboBox6.Enabled = False
    End Sub

    Private Sub RadioButton16_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton16.CheckedChanged
        label41.Text = "First Word Addr(Hex):"
    End Sub

    Private Sub RadioButton17_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton17.CheckedChanged
        label41.Text = "First Byte Addr(Hex):"
    End Sub

   
    Private Sub button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button9.Click
        Dim Parameter(20) As Byte
        fCmdRet = StaticClassReaderB.GetWorkModeParameter(fComAdr, Parameter, frmcomportindex)
        If (fCmdRet = 0) Then
            If (Parameter(0) = 0) Then
                radioButton1.Checked = True
                radioButton4.Checked = True
            End If
            If (Parameter(0) = 1) Then
                radioButton2.Checked = True
                radioButton4.Checked = True
            End If
            If (Parameter(0) = 2) Then
                radioButton1.Checked = True
                radioButton3.Checked = True
            End If
            If (Parameter(0) = 3) Then
                radioButton2.Checked = True
                radioButton3.Checked = True
            End If
            comboBox1.SelectedIndex = Convert.ToInt32(Parameter(1))
            comboBox2.SelectedIndex = Convert.ToInt32(Parameter(3) - 1)
            comboBox3.SelectedIndex = Convert.ToInt32(Parameter(2) - 1)
            comboBox4.SelectedIndex = Convert.ToInt32(Parameter(4))
            If ((Parameter(4) = 1) Or (Parameter(4) = 2) Or (Parameter(4) = 3)) Then
                button10.Enabled = True
                button11.Enabled = True
                radioButton5.Enabled = True
                radioButton6.Enabled = True
                radioButton7.Enabled = True
                radioButton8.Enabled = True
                If (radioButton5.Checked) Then
                    If (radioButton7.Checked) Then
                        RadioButton16.Enabled = True
                        RadioButton17.Enabled = True
                    Else
                        RadioButton16.Enabled = False
                        RadioButton17.Enabled = False
                    End If
                    radioButton9.Enabled = True
                    radioButton10.Enabled = True
                    radioButton11.Enabled = True
                    radioButton12.Enabled = True
                    RadioButton18.Enabled = True
                    radioButton20.Enabled = True
                    If (Convert.ToInt32((Parameter(5) And &H10)) = &H10) Then
                        radioButton13.Enabled = False
                        RadioButton19.Enabled = False
                    Else
                        radioButton13.Enabled = True
                        RadioButton19.Enabled = True
                    End If
                    If ((radioButton13.Checked) Or (RadioButton19.Checked)) Then
                        ComboBox6.Enabled = False
                    Else
                        ComboBox6.Enabled = True
                    End If
                Else
                    ComboBox6.Enabled = True
                    radioButton14.Enabled = True
                    radioButton15.Enabled = True
                    textBox3.Enabled = True
                End If
                If ((radioButton8.Checked) Or (radioButton20.Checked)) Then
                    comboBox5.Enabled = True
                End If
                If (Parameter(4) = 0) Then
                    button10.Enabled = False
                    button11.Enabled = False
                    radioButton5.Enabled = False
                    radioButton6.Enabled = False
                    radioButton7.Enabled = False
                    radioButton8.Enabled = False
                    radioButton9.Enabled = False
                    radioButton10.Enabled = False
                    radioButton11.Enabled = False
                    radioButton12.Enabled = False
                    radioButton13.Enabled = False
                    radioButton14.Enabled = False
                    radioButton15.Enabled = False
                    RadioButton16.Enabled = False
                    RadioButton17.Enabled = False
                    RadioButton18.Enabled = False
                    RadioButton19.Enabled = False
                    radioButton20.Enabled = False
                    textBox3.Enabled = False
                    comboBox5.Enabled = False
                    ComboBox6.Enabled = False
                End If
            End If
            If (Convert.ToInt32((Parameter(5)) And &H1) = 0) Then
                radioButton5.Checked = True
            Else
                radioButton6.Checked = True
            End If
            If (Convert.ToInt32((Parameter(5)) And &H2) = 0) Then
                radioButton7.Checked = True
            Else
                If (Convert.ToInt32((Parameter(5)) And &H10) = 0) Then
                    radioButton8.Checked = True
                Else
                    radioButton20.Checked = True
                End If
            End If
            If (Convert.ToInt32((Parameter(5)) And &H4) = 0) Then
                radioButton14.Checked = True
            Else
                radioButton15.Checked = True
            End If
            If (Convert.ToInt32((Parameter(5)) And &H8) = 0) Then
                RadioButton16.Checked = True
            Else
                RadioButton17.Checked = True
            End If
                Select Case (Parameter(6))
                    Case 0
                        radioButton9.Checked = True
                    Case 1
                        radioButton10.Checked = True
                    Case 2
                        radioButton11.Checked = True
                    Case 3
                        radioButton12.Checked = True
                    Case 4
                        radioButton13.Checked = True
                    Case 5
                        RadioButton18.Checked = True
                    Case 6
                        RadioButton19.Checked = True
                End Select
                textBox3.Text = Convert.ToString(Parameter(7), 16).PadLeft(2, "0")
                comboBox5.SelectedIndex = Convert.ToInt32(Parameter(8) - 1)
                ComboBox6.SelectedIndex = Convert.ToInt32(Parameter(9))
            ComboBox7.SelectedIndex = Convert.ToInt32(Parameter(10))
            comboBox_OffsetTime.SelectedIndex = Convert.ToInt32(Parameter(11))
            End If
            AddCmdLog("SetAccuracy", "GetWorkModeparameter", fCmdRet)
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Dim Accuracy As Byte
        Accuracy = Convert.ToByte(ComboBox7.SelectedIndex)
        fCmdRet = StaticClassReaderB.SetAccuracy(fComAdr, Accuracy, frmcomportindex)
        AddCmdLog("SetAccuracy", "SetAccuracy", fCmdRet)
    End Sub

    Private Sub ComboBox_COM_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox_COM.SelectedIndexChanged
        ComboBox_baud2.Items.Clear()
        If (ComboBox_COM.SelectedIndex = 0) Then
            ComboBox_baud2.Items.Add("9600bps")
            ComboBox_baud2.Items.Add("19200bps")
            ComboBox_baud2.Items.Add("38400bps")
            ComboBox_baud2.Items.Add("57600bps")
            ComboBox_baud2.Items.Add("115200bps")
            ComboBox_baud2.SelectedIndex = 3
        Else       
            ComboBox_baud2.Items.Add("Auto")
            ComboBox_baud2.SelectedIndex = 0
        End If
    End Sub

    Private Sub radioButton20_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub button_OffsetTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_OffsetTime.Click
        Dim OffsetTime As Byte
        OffsetTime = Convert.ToByte(comboBox_OffsetTime.SelectedIndex)
        fCmdRet = StaticClassReaderB.SetOffsetTime(fComAdr, OffsetTime, frmcomportindex)
        AddCmdLog("SetOffsetTime", "SetOffsetTime", fCmdRet)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button_BlockWrite.Click
        Dim WordPtr, ENum1 As Byte
        Dim Num As Byte = 0
        Dim Mem As Byte = 0
        Dim WNum As Byte = 0
        Dim EPClength As Byte = 0
        Dim Writedatalen As Byte = 0
        Dim WrittenDataNum As Integer = 0
        Dim s2, str As String
        Dim CardData(320) As Byte
        Dim writedata(230) As Byte
        If ((maskadr_textbox.Text = "") Or (maskLen_textBox.Text = "")) Then
            Exit Sub
        End If
        If (checkBox1.Checked) Then
            MaskFlag = 1
        Else
            MaskFlag = 0
        End If
        Maskadr = Convert.ToByte(maskadr_textbox.Text, 16)
        MaskLen = Convert.ToByte(maskLen_textBox.Text, 16)
        If (ComboBox_EPC2.Items.Count = 0) Then
            Exit Sub
        End If
        If (ComboBox_EPC2.SelectedItem = Nothing) Then
            Exit Sub
        End If
        str = ComboBox_EPC2.SelectedItem.ToString()
        ENum1 = Convert.ToByte(str.Length / 4)
        EPClength = Convert.ToByte(ENum1 * 2)
        Dim EPC(ENum1) As Byte
        EPC = HexStringToByteArray(str)
        If (C_Reserve.Checked) Then
            Mem = 0
        End If
        If (C_EPC.Checked) Then
            Mem = 1
        End If
        If (C_TID.Checked) Then
            Mem = 2
        End If
        If (C_User.Checked) Then
            Mem = 3
        End If
        If (Edit_WordPtr.Text = "") Then
            MessageBox.Show("Address of Tag Data is NULL", "Information")
            Exit Sub
        End If
        If (textBox1.Text = "") Then
            MessageBox.Show("Length of Data(Read/Block Erase) is NULL", "Information")
            Exit Sub
        End If
        If (Convert.ToInt32(Edit_WordPtr.Text, 16) + Convert.ToInt32(textBox1.Text) > 120) Then
            Exit Sub
        End If
        If (Edit_AccessCode2.Text = "") Then
            Exit Sub
        End If
        WordPtr = Convert.ToByte(Edit_WordPtr.Text, 16)
        Num = Convert.ToByte(textBox1.Text)
        If (Edit_AccessCode2.Text.Length <> 8) Then
            Exit Sub
        End If
        fPassWord = HexStringToByteArray(Edit_AccessCode2.Text)
        If (Edit_WriteData.Text = "") Then
            Exit Sub
        End If
        s2 = Edit_WriteData.Text
        If (s2.Length Mod 4 <> 0) Then
            MessageBox.Show("The Number must be 4 times.", "WriteBlock")
            Exit Sub
        End If
        WNum = Convert.ToByte(s2.Length / 4)
        ReDim writedata(WNum * 2)
        writedata = HexStringToByteArray(s2)
        Writedatalen = Convert.ToByte(WNum * 2)
        If ((checkBox_pc.Checked) And (C_EPC.Checked)) Then
            WordPtr = 1
            Writedatalen = Convert.ToByte(Edit_WriteData.Text.Length / 2 + 2)
            writedata = HexStringToByteArray(textBox_pc.Text + Edit_WriteData.Text)
        End If
        fCmdRet = StaticClassReaderB.WriteBlock_G2(fComAdr, EPC, Mem, WordPtr, Writedatalen, writedata, fPassWord, Maskadr, MaskLen, MaskFlag, WrittenDataNum, EPClength, ferrorcode, frmcomportindex)
        AddCmdLog("Write data", "write", fCmdRet)
        If (fCmdRet = 0) Then
            StatusBar1.Panels(0).Text = DateTime.Now.ToLongTimeString() + "‘'WriteBlock'Command Response=0x00'(completely  Write Block successfully)"
        End If
    End Sub

    Private Sub button13_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button13.Click
        Dim dminfre, dmaxfre, Ffenpin As Byte
        Dim i, j, CardNum, Totallen, UID_index, n_index As Integer
        Dim EPC(5000) As Byte
        Dim temp1, temp2, temp3, temp4 As String
        Dim ncount As Double
        Dim AdrTID As Byte = 0
        Dim LenTID As Byte = 0
        Dim TIDFlag As Byte = 0
        button13.Enabled = False
        button16.Enabled = True
        button18.Enabled = False
        button19.Enabled = False
        listBox4.Items.Clear()
        temp1 = ""
        temp2 = ""
        temp3 = ""
        temp4 = ""
        breakflag = False
        For Ffenpin = 0 To 62
            If (breakflag = True) Then
                breakflag = False
                If (fAppClosed) Then
                    Close()
                End If
                Exit Sub
            End If
            dmaxfre = Ffenpin
            dminfre = Ffenpin
            y_f = Convert.ToDouble(902.6 + (Ffenpin And &H3F) * 0.4)
            temp4 = Convert.ToString(y_f)
            temp3 = temp4.PadRight(5, " ") + "MHz" + "(" + Convert.ToString(Ffenpin).PadLeft(2, " ") + ")"
            listBox4.Items.Add(temp3)
            For i = 0 To 3
                fCmdRet = StaticClassReaderB.Writedfre(fComAdr, dmaxfre, dminfre, frmcomportindex)
                If (fCmdRet = 0) Then
                    Exit For
                End If
            Next i
            ncount = 0
            For j = 0 To 29
                Application.DoEvents()
                If (breakflag) Then
                    breakflag = False
                    If (fAppClosed) Then
                        Close()
                    End If
                    Exit Sub
                End If
                CardNum = 0
                Totallen = 0
                fCmdRet = StaticClassReaderB.Inventory_G2(fComAdr, AdrTID, LenTID, TIDFlag, EPC, Totallen, CardNum, frmcomportindex)
                If ((fCmdRet = 1) Or (fCmdRet = 2) Or (fCmdRet = 3) Or (fCmdRet = 4)) Then
                    ncount = ncount + 1
                    If (ncount = 1) Then
                        UID_index = listBox4.Items.IndexOf(temp3)
                    Else
                        UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount - 1).PadLeft(2, " ") + "/30")
                    End If
                    If (UID_index >= 0) Then
                        listBox4.Items(UID_index) = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, " ") + "/30"
                    End If
                End If
            Next j
            If (ncount = 0) Then      
                UID_index = listBox4.Items.IndexOf(temp3)
                If (UID_index >= 0) Then
                    listBox4.Items(UID_index) = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, " ") + "/30" + "                                " + "00.00%"
                End If
            End If
            UID_index = listBox4.Items.IndexOf(temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, " ") + "/30")
            If (UID_index >= 0) Then
                x_z = ((ncount / 30) * 100)
                temp1 = Convert.ToString(x_z)
                If (ncount = 30) Then
                    temp2 = "100.00%"
                End If
                '  Else
                n_index = temp1.IndexOf(".")
                If (n_index > 0) Then
                    temp2 = temp1.Substring(0, n_index) + "." + temp1.Substring(n_index + 1, 2) + "%"
                Else
                    temp2 = temp1 + "." + "00" + "%"
                End If
                listBox4.Items(UID_index) = temp3 + "                        " + Convert.ToString(ncount).PadLeft(2, " ") + "/30" + "                                " + temp2
            End If
            listBox4.SelectedIndex = listBox4.Items.Count - 1
        Next Ffenpin
        button13.Enabled = True
        button16.Enabled = False
        button18.Enabled = True
        button19.Enabled = True
    End Sub

    Private Sub button16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button16.Click
        breakflag = True
        button13.Enabled = True
        button16.Enabled = False
        button18.Enabled = True
        button19.Enabled = True
    End Sub

    Private Sub tabControl1_Selecting_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlCancelEventArgs) Handles tabControl1.Selecting
        timer1.Enabled = False
        button10.Text = "Get"

        Timer_G2_Alarm.Enabled = False
        Timer_G2_Read.Enabled = False
        Timer_Test_.Enabled = False
        SpeedButton_Read_G2.Text = "Read"
        button2.Text = "Query Tag"
        button4.Text = "Check Alarm"
        If ((ListView1_EPC.Items.Count <> 0) And (ComOpen)) Then
            button2.Enabled = True
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = True
            Proect2.Enabled = True
            Always2.Enabled = True
            AlwaysNot2.Enabled = True
            P_Reserve.Enabled = True
            P_EPC.Enabled = True
            P_TID.Enabled = True
            P_User.Enabled = True
            Button_DestroyCard.Enabled = True
            Button_SetReadProtect_G2.Enabled = True
            Button_SetEASAlarm_G2.Enabled = True
            Alarm_G2.Enabled = True
            NoAlarm_G2.Enabled = True
            Button_LockUserBlock_G2.Enabled = True
            Button_WriteEPC_G2.Enabled = True
            Button_SetMultiReadProtect_G2.Enabled = True
            Button_RemoveReadProtect_G2.Enabled = True
            Button_CheckReadProtected_G2.Enabled = True
            button4.Enabled = True
            SpeedButton_Read_G2.Enabled = True
            Button_SetProtectState.Enabled = True
            Button_DataWrite.Enabled = True
            Button_BlockWrite.Enabled = True
            Button_BlockErase.Enabled = True
            checkBox1.Enabled = True
        End If
        If ((ListView1_EPC.Items.Count = 0) And (ComOpen)) Then
            button2.Enabled = True
            DestroyCode.Enabled = False
            AccessCode.Enabled = False
            NoProect.Enabled = False
            Proect.Enabled = False
            Always.Enabled = False
            AlwaysNot.Enabled = False
            NoProect2.Enabled = False
            Proect2.Enabled = False
            Always2.Enabled = False
            AlwaysNot2.Enabled = False
            P_Reserve.Enabled = False
            P_EPC.Enabled = False
            P_TID.Enabled = False
            P_User.Enabled = False
            Button_DestroyCard.Enabled = False
            Button_SetReadProtect_G2.Enabled = False
            Button_SetEASAlarm_G2.Enabled = False
            Alarm_G2.Enabled = False
            NoAlarm_G2.Enabled = False
            Button_LockUserBlock_G2.Enabled = False
            SpeedButton_Read_G2.Enabled = False
            Button_DataWrite.Enabled = False
            Button_BlockErase.Enabled = False
            Button_BlockWrite.Enabled = False
            Button_WriteEPC_G2.Enabled = True
            Button_SetMultiReadProtect_G2.Enabled = True
            Button_RemoveReadProtect_G2.Enabled = True
            Button_CheckReadProtected_G2.Enabled = True
            button4.Enabled = True
            Button_SetProtectState.Enabled = False
            checkBox1.Enabled = False
        End If

        Timer_Test_6B.Enabled = False
        Timer_6B_Read.Enabled = False
        Timer_6B_Write.Enabled = False
        SpeedButton_Query_6B.Text = "Query"
        SpeedButton_Read_6B.Text = "Read"
        SpeedButton_Write_6B.Text = "Write"
        If ((ListView_ID_6B.Items.Count <> 0) And (ComOpen)) Then
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Read_6B.Enabled = True
            SpeedButton_Write_6B.Enabled = True
            Button14.Enabled = True
            Button15.Enabled = True
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        End If
        If ((ListView_ID_6B.Items.Count = 0) And (ComOpen)) Then
            SpeedButton_Query_6B.Enabled = True
            SpeedButton_Read_6B.Enabled = False
            SpeedButton_Write_6B.Enabled = False
            Button14.Enabled = False
            Button15.Enabled = False
            If (Bycondition_6B.Checked) Then
                Same_6B.Enabled = True
                Different_6B.Enabled = True
                Less_6B.Enabled = True
                Greater_6B.Enabled = True
            End If
        End If
        breakflag = True
        button13.Enabled = ComOpen
        button16.Enabled = False
        button18.Enabled = ComOpen
        button19.Enabled = ComOpen

    End Sub

    Private Sub button17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button17.Click
        listBox4.Items.Clear()
    End Sub

    Private Sub button18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button18.Click
        Dim FlashMode As Byte
        FlashMode = Convert.ToByte(comboBox8.SelectedIndex)
        fCmdRet = StaticClassReaderB.SetFhssMode(fComAdr, FlashMode, frmcomportindex)
        AddCmdLog("SetFhssMode", "SetFhssMode", fCmdRet)
    End Sub

    Private Sub button19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button19.Click
        Dim FlashMode As Byte
        FlashMode = 0
        fCmdRet = StaticClassReaderB.GetFhssMode(fComAdr, FlashMode, frmcomportindex)
        If (fCmdRet = 0) Then
            comboBox8.SelectedIndex = FlashMode
        End If
        AddCmdLog("GetFhssMode", "GetFhssMode", fCmdRet)
    End Sub

    Private Sub radioButton_band5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radioButton_band5.CheckedChanged
        Dim i As Integer
        ComboBox_dminfre.Items.Clear()
        ComboBox_dmaxfre.Items.Clear()
        For i = 0 To 14
            ComboBox_dminfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz")
            ComboBox_dmaxfre.Items.Add(Convert.ToString(865.1 + i * 0.2) + " MHz")
        Next i
        ComboBox_dmaxfre.SelectedIndex = 14
        ComboBox_dminfre.SelectedIndex = 0
    End Sub

    Private Sub Edit_WriteData_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Edit_WriteData.TextChanged
        Dim n As Integer = 0
        Dim m As Integer = 0
        n = Edit_WriteData.Text.Length
        If ((checkBox_pc.Checked) And (n Mod 4 = 0) And (C_EPC.Checked)) Then
            m = n / 4
            m = (m And &H3F) << 3
            textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, "0") + "00"
        End If
    End Sub

    Private Sub checkBox_pc_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles checkBox_pc.CheckedChanged
        Dim n As Integer = 0
        Dim m As Integer = 0
        If (checkBox_pc.Checked) Then
            Edit_WordPtr.Text = "02"
            Edit_WordPtr.ReadOnly = True
            n = Edit_WriteData.Text.Length
            If ((checkBox_pc.Checked) And (n Mod 4 = 0) And (C_EPC.Checked)) Then
                m = n / 4
                m = (m And &H3F) << 3
                textBox_pc.Text = Convert.ToString(m, 16).PadLeft(2, "0") + "00"
            End If
        Else
            Edit_WordPtr.ReadOnly = False
        End If
    End Sub

    Private Sub CheckBox_TID_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox_TID.CheckedChanged
        If (CheckBox_TID.Checked) Then
            groupBox33.Enabled = True
            textBox4.Enabled = True
            textBox4.Enabled = True
        Else
            groupBox33.Enabled = False
            textBox4.Enabled = False
            textBox4.Enabled = False
        End If
    End Sub

    Private Sub button_settigtime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_settigtime.Click
        Dim TriggerTime As Byte
        TriggerTime = Convert.ToByte(comboBox_tigtime.SelectedIndex)
        fCmdRet = StaticClassReaderB.SetTriggerTime(fComAdr, TriggerTime, frmcomportindex)
        AddCmdLog("SetTriggerTime", "Set TriggerTime", fCmdRet)
    End Sub

    Private Sub button_gettigtime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button_gettigtime.Click
        Dim TriggerTime As Byte
        TriggerTime = 255
        fCmdRet = StaticClassReaderB.SetTriggerTime(fComAdr, TriggerTime, frmcomportindex)
        If (fCmdRet = 0) Then
            comboBox_tigtime.SelectedIndex = TriggerTime
        End If
        AddCmdLog("SetTriggerTime", "Get TriggerTime", fCmdRet)
    End Sub
End Class
