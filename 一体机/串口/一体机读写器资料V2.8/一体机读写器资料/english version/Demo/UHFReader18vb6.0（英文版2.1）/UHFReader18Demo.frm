VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "UHFReader18 Demo V2.1"
   ClientHeight    =   7770
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   10185
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   ScaleHeight     =   7770
   ScaleWidth      =   10185
   StartUpPosition =   3  '´°¿ÚÈ±Ê¡
   Begin VB.Frame Frame6 
      Caption         =   "Result "
      Height          =   855
      Left            =   120
      TabIndex        =   52
      Top             =   6840
      Width           =   9975
      Begin VB.Label Label18 
         Height          =   375
         Left            =   240
         TabIndex        =   53
         Top             =   240
         Width           =   7215
      End
   End
   Begin VB.Frame Frame5 
      Caption         =   "Query Tag"
      Height          =   4335
      Left            =   5040
      TabIndex        =   46
      Top             =   2400
      Width           =   5055
      Begin VB.CommandButton Command6 
         Caption         =   "Clear"
         Height          =   375
         Left            =   3480
         TabIndex        =   50
         Top             =   3840
         Width           =   1455
      End
      Begin VB.CommandButton Command5 
         Caption         =   "Stop"
         Enabled         =   0   'False
         Height          =   375
         Left            =   1800
         TabIndex        =   49
         Top             =   3840
         Width           =   1575
      End
      Begin VB.CommandButton Command4 
         Caption         =   "Start"
         Enabled         =   0   'False
         Height          =   375
         Left            =   120
         TabIndex        =   48
         Top             =   3840
         Width           =   1575
      End
      Begin VB.ListBox List1 
         Height          =   3480
         Left            =   120
         TabIndex        =   47
         Top             =   240
         Width           =   4815
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "Set Reader Parameter"
      Height          =   4335
      Left            =   120
      TabIndex        =   27
      Top             =   2400
      Width           =   4815
      Begin VB.Frame Frame7 
         Caption         =   "Work Mode"
         Height          =   1935
         Left            =   2160
         TabIndex        =   55
         Top             =   1200
         Width           =   2535
         Begin VB.ComboBox Combo8 
            Height          =   300
            Left            =   120
            Style           =   2  'Dropdown List
            TabIndex        =   58
            Top             =   360
            Width           =   2295
         End
         Begin VB.CommandButton Command8 
            Caption         =   "Set Work Mode"
            Enabled         =   0   'False
            Height          =   375
            Left            =   120
            TabIndex        =   57
            Top             =   840
            Width           =   2295
         End
         Begin VB.CommandButton Command9 
            Caption         =   "Get Work Mode"
            Enabled         =   0   'False
            Height          =   375
            Left            =   120
            TabIndex        =   56
            Top             =   1440
            Width           =   2295
         End
      End
      Begin VB.CommandButton Command7 
         Caption         =   "Set Parameter"
         Enabled         =   0   'False
         Height          =   375
         Left            =   2760
         TabIndex        =   54
         Top             =   3720
         Width           =   1935
      End
      Begin VB.Timer Timer1 
         Enabled         =   0   'False
         Interval        =   50
         Left            =   2760
         Top             =   2160
      End
      Begin VB.CheckBox Check3 
         Caption         =   "Single Frequency Point "
         Height          =   375
         Left            =   120
         TabIndex        =   45
         Top             =   3720
         Width           =   4215
      End
      Begin VB.ComboBox Combo7 
         Height          =   300
         Left            =   3840
         TabIndex        =   44
         Text            =   "Combo7"
         Top             =   3240
         Width           =   855
      End
      Begin VB.ComboBox Combo6 
         Height          =   300
         Left            =   1440
         Style           =   2  'Dropdown List
         TabIndex        =   43
         Top             =   3240
         Width           =   855
      End
      Begin VB.Frame Frame4 
         Caption         =   "FreqBand Setting"
         Height          =   1935
         Left            =   120
         TabIndex        =   36
         Top             =   1200
         Width           =   1935
         Begin VB.OptionButton Option5 
            Caption         =   "EU band"
            Height          =   375
            Left            =   120
            TabIndex        =   59
            Top             =   1440
            Width           =   1335
         End
         Begin VB.OptionButton Option4 
            Caption         =   "Korean band"
            Height          =   375
            Left            =   120
            TabIndex        =   40
            Top             =   1080
            Width           =   1335
         End
         Begin VB.OptionButton Option3 
            Caption         =   "US band"
            Height          =   255
            Left            =   120
            TabIndex        =   39
            Top             =   840
            Width           =   1575
         End
         Begin VB.OptionButton Option2 
            Caption         =   "Chinese band2"
            Height          =   375
            Left            =   120
            TabIndex        =   38
            Top             =   480
            Width           =   1575
         End
         Begin VB.OptionButton Option1 
            Caption         =   "User band"
            Height          =   255
            Left            =   120
            TabIndex        =   37
            Top             =   240
            Width           =   1335
         End
      End
      Begin VB.ComboBox Combo5 
         Height          =   300
         Left            =   3840
         Style           =   2  'Dropdown List
         TabIndex        =   35
         Top             =   720
         Width           =   855
      End
      Begin VB.ComboBox Combo4 
         Height          =   300
         ItemData        =   "UHFReader18Demo.frx":0000
         Left            =   720
         List            =   "UHFReader18Demo.frx":0061
         Style           =   2  'Dropdown List
         TabIndex        =   33
         Top             =   720
         Width           =   975
      End
      Begin VB.ComboBox Combo3 
         Height          =   300
         Left            =   2880
         Style           =   2  'Dropdown List
         TabIndex        =   31
         Top             =   240
         Width           =   1815
      End
      Begin VB.TextBox Text8 
         Height          =   390
         Left            =   1320
         TabIndex        =   29
         Top             =   240
         Width           =   615
      End
      Begin VB.Label Label17 
         Caption         =   "Max.Frequency:"
         Height          =   255
         Left            =   2520
         TabIndex        =   42
         Top             =   3360
         Width           =   1335
      End
      Begin VB.Label Label16 
         Caption         =   "Min.Frequency:"
         Height          =   255
         Left            =   120
         TabIndex        =   41
         Top             =   3360
         Width           =   1455
      End
      Begin VB.Label Label15 
         Caption         =   "Max InventoryScanTime:"
         Height          =   255
         Left            =   1800
         TabIndex        =   34
         Top             =   840
         Width           =   2055
      End
      Begin VB.Label Label14 
         Caption         =   "Power:"
         Height          =   255
         Left            =   120
         TabIndex        =   32
         Top             =   840
         Width           =   615
      End
      Begin VB.Label Label13 
         Caption         =   "Baud Rate:"
         Height          =   255
         Left            =   2040
         TabIndex        =   30
         Top             =   360
         Width           =   975
      End
      Begin VB.Label Label12 
         Caption         =   "Address(HEX):"
         Height          =   255
         Left            =   120
         TabIndex        =   28
         Top             =   360
         Width           =   1215
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "Reader Information"
      Height          =   2295
      Left            =   1920
      TabIndex        =   9
      Top             =   0
      Width           =   8175
      Begin VB.TextBox Text9 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   6960
         Locked          =   -1  'True
         TabIndex        =   51
         Top             =   840
         Width           =   735
      End
      Begin VB.CommandButton Command3 
         Caption         =   "Get Reader Info"
         Enabled         =   0   'False
         Height          =   375
         Left            =   5880
         TabIndex        =   26
         Top             =   1680
         Width           =   1815
      End
      Begin VB.TextBox Text7 
         BackColor       =   &H8000000A&
         Height          =   375
         Left            =   4200
         Locked          =   -1  'True
         TabIndex        =   25
         Top             =   1680
         Width           =   1215
      End
      Begin VB.TextBox Text6 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   1440
         Locked          =   -1  'True
         TabIndex        =   23
         Top             =   1680
         Width           =   1215
      End
      Begin VB.TextBox Text5 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   3120
         Locked          =   -1  'True
         TabIndex        =   20
         Top             =   960
         Width           =   1335
      End
      Begin VB.TextBox Text4 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   600
         Locked          =   -1  'True
         TabIndex        =   18
         Top             =   960
         Width           =   1335
      End
      Begin VB.CheckBox Check2 
         Caption         =   "EPCC1-G2"
         Height          =   255
         Left            =   5880
         TabIndex        =   17
         Top             =   600
         Width           =   1455
      End
      Begin VB.CheckBox Check1 
         Caption         =   "ISO18000-6B"
         Height          =   255
         Left            =   5880
         TabIndex        =   16
         Top             =   240
         Width           =   1575
      End
      Begin VB.TextBox Text3 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   3120
         Locked          =   -1  'True
         TabIndex        =   14
         Top             =   360
         Width           =   1335
      End
      Begin VB.TextBox Text2 
         BackColor       =   &H8000000B&
         Height          =   375
         Left            =   600
         Locked          =   -1  'True
         TabIndex        =   12
         Top             =   360
         Width           =   1335
      End
      Begin VB.Label Label11 
         Caption         =   "Max.Frequency:"
         Height          =   255
         Left            =   2760
         TabIndex        =   24
         Top             =   1800
         Width           =   1215
      End
      Begin VB.Label Label10 
         Caption         =   "Min.Frequency:"
         Height          =   255
         Left            =   120
         TabIndex        =   22
         Top             =   1800
         Width           =   1335
      End
      Begin VB.Label Label9 
         Caption         =   "Max InventoryScanTime:"
         Height          =   255
         Left            =   4920
         TabIndex        =   21
         Top             =   960
         Width           =   2055
      End
      Begin VB.Label Label8 
         Caption         =   "Power:"
         Height          =   255
         Left            =   2280
         TabIndex        =   19
         Top             =   1080
         Width           =   615
      End
      Begin VB.Label Label7 
         Caption         =   "Protocl:"
         Height          =   255
         Left            =   4920
         TabIndex        =   15
         Top             =   480
         Width           =   855
      End
      Begin VB.Label Label6 
         Caption         =   "Version:"
         Height          =   255
         Left            =   2280
         TabIndex        =   13
         Top             =   480
         Width           =   855
      End
      Begin VB.Label Label5 
         Caption         =   "Addr:"
         Height          =   255
         Left            =   120
         TabIndex        =   11
         Top             =   1080
         Width           =   495
      End
      Begin VB.Label Label4 
         Caption         =   "Type:"
         Height          =   255
         Left            =   120
         TabIndex        =   10
         Top             =   480
         Width           =   855
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "Communication"
      Height          =   2295
      Left            =   120
      TabIndex        =   0
      Top             =   0
      Width           =   1695
      Begin VB.CommandButton Command2 
         Caption         =   "Close Port"
         Height          =   375
         Left            =   120
         TabIndex        =   8
         Top             =   1800
         Width           =   1455
      End
      Begin VB.ComboBox Combo2 
         Height          =   300
         Left            =   600
         Style           =   2  'Dropdown List
         TabIndex        =   6
         Top             =   960
         Width           =   975
      End
      Begin VB.CommandButton Command1 
         Caption         =   "Open Port"
         Height          =   375
         Left            =   120
         TabIndex        =   5
         Top             =   1320
         Width           =   1455
      End
      Begin VB.TextBox Text1 
         Height          =   270
         Left            =   1200
         TabIndex        =   4
         Text            =   "FF"
         Top             =   600
         Width           =   375
      End
      Begin VB.ComboBox Combo1 
         Height          =   300
         ItemData        =   "UHFReader18Demo.frx":00D7
         Left            =   600
         List            =   "UHFReader18Demo.frx":00D9
         Style           =   2  'Dropdown List
         TabIndex        =   2
         Top             =   240
         Width           =   975
      End
      Begin VB.Label Label3 
         Caption         =   "Baud:"
         Height          =   255
         Left            =   120
         TabIndex        =   7
         Top             =   1080
         Width           =   495
      End
      Begin VB.Label Label2 
         Caption         =   "Reader Addr:"
         Height          =   255
         Left            =   120
         TabIndex        =   3
         Top             =   720
         Width           =   1095
      End
      Begin VB.Label Label1 
         Caption         =   "Port:"
         Height          =   255
         Left            =   120
         TabIndex        =   1
         Top             =   360
         Width           =   495
      End
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Dim FrmHabdle As Long
Dim ComAdr As Byte
Dim fCmdRet As Long
Dim fdminfre As Single
Dim fdmaxfre As Single
Dim fIsInventoryScan As Boolean
Dim fcloseApp As Boolean

Private Sub Check3_Click()
If Check3.Value = 1 Then
    Combo7.ListIndex = Combo6.ListIndex
End If
End Sub

Private Sub Command1_Click()
Dim Port As Long
Dim Baud As Byte
Dim str As String
Dim TrType(2) As Byte
Dim VersionInfo(2) As Byte
Dim ReaderType, ScanTime, dmaxfre, dminfre, PowerDbm, FreBand, Accuracy As Byte
ComAdr = &HFF
Port = Combo1.ListIndex + 1
Baud = Combo2.ListIndex
If Baud > 2 Then
Baud = Baud + 2
End If
FrmHandle = -1
Label18.Caption = ""
Combo4.Clear
fCmdRet = OpenComPort(Port, ComAdr, Baud, FrmHabdle)
If (fCmdRet = 0) Then
fCmdRet = GetReaderInformation(ComAdr, VersionInfo(0), ReaderType, TrType(0), dmaxfre, dminfre, PowerDbm, ScanTime, FrmHabdle)
If (fCmdRet = 0) Then
Text3.Text = GetValue(VersionInfo(0)) + "." + GetValue(VersionInfo(1))
 If (VersionInfo(1) >= 30) Then
    For i = 0 To 30
    str = Val(i)
    Combo4.AddItem (str)
    Next i
    Combo4.ListIndex = 30
  Else
    For i = 0 To 18
     str = Val(i)
    Combo4.AddItem (str)
    Next i
    Combo4.ListIndex = 18
 End If
 Text4.Text = GetHexValue(ComAdr)
 Text8.Text = GetHexValue(ComAdr)
  str = GetValue(ScanTime)
Text9.Text = str + "*100ms"
index = ScanTime - 3
  Combo5.ListIndex = index
  str = Val(PowerDbm)
  Text5.Text = str
  Combo4.ListIndex = PowerDbm
FreBand = ((dmaxfre And &HC0) / 16) Or (dminfre / 64)
Select Case FreBand
 Case 0
         Option1.Value = True
         fdminfre = 902.6 + (dminfre And &H3F) * 0.4
         fdmaxfre = 902.6 + (dmaxfre And &H3F) * 0.4
Case 1
         Option2.Value = True
         fdminfre = 920.125 + (dminfre And &H3F) * 0.25
         fdmaxfre = 920.125 + (dmaxfre And &H3F) * 0.25
 Case 2
         Option3.Value = True
         fdminfre = 902.75 + (dminfre And &H3F) * 0.5
         fdmaxfre = 902.75 + (dmaxfre And &H3F) * 0.5
Case 3
         Option4.Value = True
         fdminfre = 917.1 + (dminfre And &H3F) * 0.2
         fdmaxfre = 917.1 + (dmaxfre And &H3F) * 0.2
Case 4
         Option5.Value = True
         fdminfre = 865.1 + (dminfre And &H3F) * 0.2
         fdmaxfre = 865.1 + (dmaxfre And &H3F) * 0.2
 End Select
  Text6.Text = CStr(fdminfre) + "MHz"
  Text7.Text = CStr(fdmaxfre) + "MHz"
  If fdmaxfre <> fdminfre Then
  Check3.Value = 0
  End If
 ' Combo6.ListIndex = dminfre And &H3F
  'Combo7.ListIndex = dmaxfre And &H3F
 Select Case ReaderType
      Case 6
           Text2.Text = ""
      Case 3
           Text2.Text = ""
      Case 9
           Text2.Text = "UHFReader18"
 End Select
    If (TrType(0) And &H2) = &H2 Then
    Check1.Value = 1
    Check2.Value = 1
    Else
    Check1.Value = 0
    Check2.Value = 0
    End If
Label18.Caption = "GetReaderInformation success!"
End If
Command3.Enabled = True
Command4.Enabled = True
Command7.Enabled = True
Command8.Enabled = True
Command9.Enabled = True
Label18.Caption = "OpenComPort Success"
Else
Label18.Caption = "OpenComPort Failed"
End If

End Sub
Function GetHexData(ByRef uBuff() As Byte, ByVal count As Long) As String
Dim GetData, GetBuff As String
Dim i As Long
GetData = ""
For i = 0 To count - 1
If uBuff(i) < 16 Then
GetBuff = "0" & Hex(uBuff(i))
Else
GetBuff = Hex(uBuff(i))
End If
GetData = GetData + GetBuff
Next i
GetHexData = GetData
End Function
Function GetHexValue(ByVal uBuff As Byte) As String
Dim GetData As String
Dim i As Long
If uBuff < 16 Then
GetData = "0" & Hex(uBuff)
Else
GetData = Hex(uBuff)
End If
GetHexValue = GetData
End Function
Function GetValue(ByVal uBuff As Byte) As String
Dim GetData As String
Dim i As Long
If uBuff < 10 Then
GetData = "0" & Val(uBuff)
Else
GetData = Val(uBuff)
End If
GetValue = GetData
End Function

Private Sub Command2_Click()
Timer1.Enabled = False
fCmdRet = CloseComPort()


If fCmdRet = 0 Then
Label18.Caption = "CloseComPort success!"
Label18.Caption = ""
Text2.Text = ""
Text3.Text = ""
Text4.Text = ""
Text5.Text = ""
Text6.Text = ""
Text7.Text = ""
Text9.Text = ""
Check1.Value = 0
Check2.Value = 0
Command3.Enabled = False
Command4.Enabled = False
Command5.Enabled = False
Command7.Enabled = False
Command8.Enabled = False
Command9.Enabled = False
Else
Label18.Caption = "CloseComPort Failed!"
End If
End Sub

Private Sub Command3_Click()
Dim TrType(2) As Byte
Dim VersionInfo(2) As Byte
Dim ReaderType, ScanTime, dmaxfre, dminfre, PowerDbm, FreBand, Accuracy As Byte
Dim str As String
Dim index As Long
Label18.Caption = ""
Text2.Text = ""
Text3.Text = ""
Text4.Text = ""
Text5.Text = ""
Text6.Text = ""
Text7.Text = ""
Text9.Text = ""
Check1.Value = 0
Check2.Value = 0
Combo4.Clear
fCmdRet = GetReaderInformation(ComAdr, VersionInfo(0), ReaderType, TrType(0), dmaxfre, dminfre, PowerDbm, ScanTime, FrmHabdle)
If (fCmdRet = 0) Then
Text3.Text = GetValue(VersionInfo(0)) + "." + GetValue(VersionInfo(1))
 If (VersionInfo(1) >= 30) Then
    For i = 0 To 30
    str = Val(i)
    Combo4.AddItem (str)
    Next i
    Combo4.ListIndex = 30
  Else
    For i = 0 To 18
     str = Val(i)
    Combo4.AddItem (str)
    Next i
    Combo4.ListIndex = 18
 End If
 Text4.Text = GetHexValue(ComAdr)
 Text8.Text = GetHexValue(ComAdr)
  str = GetValue(ScanTime)
Text9.Text = str + "*100ms"
index = ScanTime - 3
  Combo5.ListIndex = index
  str = Val(PowerDbm)
  Text5.Text = str
  Combo4.ListIndex = PowerDbm
FreBand = ((dmaxfre And &HC0) / 16) Or (dminfre / 64)
Select Case FreBand
Case 0
        Option1.Value = True
        fdminfre = 902.6 + (dminfre And &H3F) * 0.4
        fdmaxfre = 902.6 + (dmaxfre And &H3F) * 0.4
Case 1
        Option2.Value = True
        fdminfre = 920.125 + (dminfre And &H3F) * 0.25
        fdmaxfre = 920.125 + (dmaxfre And &H3F) * 0.25
Case 2
        Option3.Value = True
        fdminfre = 902.75 + (dminfre And &H3F) * 0.5
        fdmaxfre = 902.75 + (dmaxfre And &H3F) * 0.5
Case 3
        Option4.Value = True
        fdminfre = 917.1 + (dminfre And &H3F) * 0.2
        fdmaxfre = 917.1 + (dmaxfre And &H3F) * 0.2
Case 4
        Option5.Value = True
        fdminfre = 865.1 + (dminfre And &H3F) * 0.2
        fdmaxfre = 865.1 + (dmaxfre And &H3F) * 0.2
End Select
  Text6.Text = CStr(fdminfre) + "MHz"
  Text7.Text = CStr(fdmaxfre) + "MHz"
  If fdmaxfre <> fdminfre Then
  Check3.Value = 0
  End If
 ' Combo6.ListIndex = CByte(dminfre And &H3F)
 ' Combo7.ListIndex = CByte(dmaxfre And &H3F)
 Select Case ReaderType
 Case 6
     Text2.Text = ""
 Case 3
     Text2.Text = ""
 Case 9
     Text2.Text = "UHFReader18"
 End Select
    If (TrType(0) And &H2) = &H2 Then
    Check1.Value = 1
    Check2.Value = 1
    Else
    Check1.Value = 0
    Check2.Value = 0
    End If
Label18.Caption = "GetReaderInformation success!"
End If
End Sub
Function Inventory() As Long
Dim CardNum As Long
        Dim Totallen As Long
        Dim AdrTID As Byte
        Dim LenTID As Byte
        Dim TIDFlag As Byte
        Dim EPClen, m As Long
        Dim EPC(5000) As Byte
        Dim CardIndex As Long
        Dim temps As String
        Dim s, sEPC As String
        Dim ScanModeData(5000) As Byte
        Dim ValidDatalength As Long
        Dim isonlistview As Boolean
        AdrTID = 0
        LenTID = 0
        TIDFlag = 0
        If (Combo8.ListIndex = 0) Then
        fCmdRet = Inventory_G2(ComAdr, AdrTID, LenTID, TIDFlag, EPC(0), Totallen, CardNum, FrmHabdle)
             If ((fCmdRet = 1) Or (fCmdRet = 2) Or (fCmdRet = 3) Or (fCmdRet = 4) Or (fCmdRet = &HFB)) Then
                temps = GetHexData(EPC, Totallen)
                fInventory_EPC_List = temps
                m = 1
    
                If (CardNum = 0) Then
                    fIsInventoryScan = False
                    Exit Function
                End If
                For CardIndex = 0 To CardNum - 1
                If (fcloseApp) Then
                Exit For
                End If
                    EPClen = EPC(m - 1)
                   sEPC = Mid(temps, m * 2 + 1, EPClen * 2)
                    m = m + EPClen + 1
                    If (Len(sEPC) <> EPClen * 2) Then
                        Exit Function
                    End If
                    List1.AddItem (sEPC)
                Next CardIndex
            End If
            If (List1.ListCount > 0) Then
            List1.ListIndex = List1.ListCount - 1
            End If
        Else
        fCmdRet = ReadActiveModeData(ScanModeData(0), ValidDatalength, FrmHabdle)
        If (ValidDatalength > 0) Then
        temps = GetHexData(ScanModeData, ValidDatalength)
        List1.AddItem (temps)
        End If
         If (List1.ListCount > 0) Then
            List1.ListIndex = List1.ListCount - 1
            End If
        End If
        fIsInventoryScan = False
        If (fcloseApp) Then
            Close
        End If
End Function

Private Sub Command4_Click()
Timer1.Enabled = True
Command5.Enabled = True
Command4.Enabled = False
End Sub

Private Sub Command5_Click()
Timer1.Enabled = False
Command4.Enabled = True
Command5.Enabled = False
End Sub

Private Sub Command6_Click()
List1.Clear
End Sub

Private Sub Command7_Click()
  Dim aNewComAdr, PowerDbm, dminfre, dmaxfre, ScanTime, band As Byte
  Dim fBaud As Byte
        Dim returninfo As String
        Dim returninfoDlg As String
        Dim setinfo As String
        setinfo = ""
        returninfoDlg = ""
        returninfo = ""
        Label18.Caption = ""
        If (Option1.Value = True) Then
            band = 0
        End If
        If (Option2.Value = True) Then
            band = 1
        End If
        If (Option3.Value = True) Then
            band = 2
        End If
        If (Option4.Value = True) Then
            band = 3
        End If
        If (Option5.Value = True) Then
            band = 4
        End If
        If (Text8.Text = "") Then
            Exit Sub
        End If
        dminfre = CByte(((band And 3) * 64) Or (Combo6.ListIndex And &H3F))
        dmaxfre = CByte(((band And &HC) * 16) Or (Combo7.ListIndex And &H3F))
        aNewComAdr = CByte(Text8.Text)
        PowerDbm = CByte(Combo4.ListIndex)
        fBaud = CByte(Combo3.ListIndex)
        If (fBaud > 2) Then
            fBaud = CByte(fBaud + 2)
        End If
        ScanTime = CByte(Combo5.ListIndex + 3)
        setinfo = "Write"
       fCmdRet = WriteComAdr(ComAdr, aNewComAdr, FrmHabdle)
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
       '     returninfoDlg = returninfoDlg + setinfo + "Address Fail Command Response=0x" + Cstr(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If
        fCmdRet = SetPowerDbm(ComAdr, PowerDbm, FrmHabdle)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Power Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Power Response Command Error"
        Else
            returninfo = returninfo + ",Power Fail"
         '   returninfoDlg = returninfoDlg + " " + setinfo + "Power Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        fCmdRet = Writedfre(ComAdr, dmaxfre, dminfre, FrmHabdle)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Frequency Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Frequency Response Command Error"
        Else

            returninfo = returninfo + ",Frequency Fail"
         '   returninfoDlg = returninfoDlg + " " + setinfo + "Frequency Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        fCmdRet = Writebaud(ComAdr, fBaud, FrmHabdle)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",Baud Rate Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",Baud Rate Response Command Error"
        Else

            returninfo = returninfo + ",Baud Rate Fail"
           ' returninfoDlg = returninfoDlg + " " + setinfo + "Baud Rate Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If

        fCmdRet = WriteScanTime(ComAdr, ScanTime, FrmHabdle)
        If (fCmdRet = 0) Then
            returninfo = returninfo + ",InventoryScanTime Success"
        ElseIf (fCmdRet = &HEE) Then
            returninfo = returninfo + ",InventoryScanTime Response Command Error"
        Else
            returninfo = returninfo + ",InventoryScanTime Fail"
          '  returninfoDlg = returninfoDlg + " " + setinfo + "InventoryScanTime Fail Command Response=0x" + Convert.ToString(fCmdRet) + "(" + GetReturnCodeDesc(fCmdRet) + ")"
        End If
        Label18.Caption = returninfo
End Sub

Private Sub Command8_Click()
Dim Parameter(6) As Byte
Parameter(0) = CByte(Combo8.ListIndex)
Parameter(1) = 2
Parameter(2) = 1
Parameter(3) = 0
Parameter(4) = 1
Parameter(5) = 0
fCmdRet = SetWorkMode(ComAdr, Parameter(0), FrmHabdle)
If fCmdRet = 0 Then
Label18.Caption = "SetWorkMode success!"
Else
Label18.Caption = "SetWorkMode Failed!"
End If
End Sub

Private Sub Command9_Click()
Dim Parameter(20) As Byte
fCmdRet = GetWorkModeParameter(ComAdr, Parameter(0), FrmHabdle)
If fCmdRet = 0 Then
Combo8.ListIndex = Parameter(4)
Label18.Caption = "GetWorkModeParameter success!"
Else
Label18.Caption = "GetWorkModeParameter Failed!"
End If
End Sub

Private Sub Form_Load()
Dim i As Integer
Dim str As String
Option1.Value = True
For i = 1 To 9
str = Val(i)
Combo1.AddItem ("COM" + str)
Next i
Combo1.ListIndex = 0
Combo2.AddItem ("9600bps")
Combo2.AddItem ("19200bps")
Combo2.AddItem ("38400bps")
Combo2.AddItem ("57600bps")
Combo2.AddItem ("115200bps")
Combo2.ListIndex = 3
Combo3.AddItem ("9600bps")
Combo3.AddItem ("19200bps")
Combo3.AddItem ("38400bps")
Combo3.AddItem ("57600bps")
Combo3.AddItem ("115200bps")
Combo3.ListIndex = 3
 For i = 3 To 255
 str = Val(i)
  Combo5.AddItem (str + "*100ms")
Next i
  Combo5.ListIndex = 7
  
 For i = 0 To 62
    Combo6.AddItem (CStr(902.6 + i * 0.4) + " MHz")
    Combo7.AddItem (CStr(902.6 + i * 0.4) + "MHz")
 Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 62
  Combo4.ListIndex = 30
  fIsInventoryScan = False
  fcloseApp = False
  
  Combo8.AddItem ("Answer mode")
  Combo8.AddItem ("Active mode")
  Combo8.AddItem ("Trigger mode(Low)")
  Combo8.AddItem ("Trigger mode(High)")
  Combo8.ListIndex = 0
End Sub

Private Sub Form_Unload(Cancel As Integer)
fcloseApp = True
End Sub

Private Sub Option1_Click()
Dim i As Integer
  Combo6.Clear
  Combo7.Clear
  Check3.Value = 0
 For i = 0 To 62
    Combo6.AddItem (CStr(902.6 + i * 0.4) + " MHz")
    Combo7.AddItem (CStr(902.6 + i * 0.4) + " MHz")
 Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 62
End Sub

Private Sub Option2_Click()
  Dim i As Integer
  Combo6.Clear
  Combo7.Clear
  Check3.Value = 0
  For i = 0 To 19
    Combo6.AddItem (CStr(920.125 + i * 0.25) + " MHz")
    Combo7.AddItem (CStr(920.125 + i * 0.25) + " MHz")
  Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 19
End Sub

Private Sub Option3_Click()
  Dim i As Integer
  Combo6.Clear
  Combo7.Clear
  Check3.Value = 0
  For i = 0 To 49
    Combo6.AddItem (CStr(902.75 + i * 0.5) + " MHz")
    Combo7.AddItem (CStr(902.75 + i * 0.5) + " MHz")
  Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 49
End Sub

Private Sub Option4_Click()
 Dim i As Integer
  Combo6.Clear
  Combo7.Clear
  Check3.Value = 0
  For i = 0 To 31
    Combo6.AddItem (CStr(917.1 + i * 0.2) + " MHz")
    Combo7.AddItem (CStr(917.1 + i * 0.2) + " MHz")
  Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 31

End Sub

Private Sub Option5_Click()
Dim i As Integer
  Combo6.Clear
  Combo7.Clear
  Check3.Value = 0
  For i = 0 To 14
    Combo6.AddItem (CStr(865.1 + i * 0.2) + " MHz")
    Combo7.AddItem (CStr(865.1 + i * 0.2) + " MHz")
  Next i
  Combo6.ListIndex = 0
  Combo7.ListIndex = 14
End Sub

Private Sub Timer1_Timer()
Dim i As Long
If (fIsInventoryScan) Then
Exit Sub
End If
fIsInventoryScan = True
b_flag = Inventory()
fIsInventoryScan = False
End Sub
