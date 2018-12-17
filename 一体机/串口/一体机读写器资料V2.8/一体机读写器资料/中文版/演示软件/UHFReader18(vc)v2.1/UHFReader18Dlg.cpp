// UHFReader18Dlg.cpp : implementation file
//

#include "stdafx.h"
#include "UHFReader18.h"
#include "UHFReader18Dlg.h"


#include <process.h>
#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif
typedef unsigned int (__stdcall *PSTARTTHREAD)(PVOID);
/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CUHFReader18Dlg dialog

CUHFReader18Dlg::CUHFReader18Dlg(CWnd* pParent /*=NULL*/)
	: CDialog(CUHFReader18Dlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CUHFReader18Dlg)
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CUHFReader18Dlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CUHFReader18Dlg)
	DDX_Control(pDX, IDC_BUTTON11, m_bt11);
	DDX_Control(pDX, IDC_BUTTON10, m_bt10);
	DDX_Control(pDX, IDC_BUTTON8, m_bt8);
	DDX_Control(pDX, IDC_BUTTON7, m_bt7);
	DDX_Control(pDX, IDC_BUTTON6, m_bt6);
	DDX_Control(pDX, IDC_BUTTON5, m_bt5);
	DDX_Control(pDX, IDC_BUTTON4, m_bt4);
	DDX_Control(pDX, IDC_BUTTON3, m_bt3);
	DDX_Control(pDX, IDC_COMBO11, m_epc);
	DDX_Control(pDX, IDC_CHECK3, m_check3);
	DDX_Control(pDX, IDC_EDIT6, m_editscan);
	DDX_Control(pDX, IDC_EDIT8, m_fman);
	DDX_Control(pDX, IDC_EDIT7, m_fmin);
	DDX_Control(pDX, IDC_EDIT5, m_editdbm);
	DDX_Control(pDX, IDC_EDIT3, m_VersionInfo);
	DDX_Control(pDX, IDC_EDIT2, m_ReaderType);
	DDX_Control(pDX, IDC_EDIT14, m_readdata);
	DDX_Control(pDX, IDC_EDIT13, m_writedata);
	DDX_Control(pDX, IDC_EDIT12, m_password);
	DDX_Control(pDX, IDC_EDIT11, m_num);
	DDX_Control(pDX, IDC_EDIT10, m_startaddr);
	DDX_Control(pDX, IDC_COMBO12, m_quyu);
	DDX_Control(pDX, IDC_RADIO1, m_rad);
	DDX_Control(pDX, IDC_EDIT9, m_addr3);
	DDX_Control(pDX, IDC_EDIT4, m_addr2);
	DDX_Control(pDX, IDC_EDIT1, m_addr1);
	DDX_Control(pDX, IDC_LIST2, m_list);
	DDX_Control(pDX, IDC_COMBO9, m_maxfre);
	DDX_Control(pDX, IDC_COMBO8, m_minfre);
	DDX_Control(pDX, IDC_COMBO7, m_scantime);
	DDX_Control(pDX, IDC_COMBO5, m_baud2);
	DDX_Control(pDX, IDC_COMBO6, m_DBM);
	DDX_Control(pDX, IDC_COMBO4, m_workmode);
	DDX_Control(pDX, IDC_COMBO3, m_baud1);
	DDX_Control(pDX, IDC_COMBO2, m_opencom);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CUHFReader18Dlg, CDialog)
	//{{AFX_MSG_MAP(CUHFReader18Dlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_KEYDOWN()
	ON_WM_CANCELMODE()
	ON_EN_UPDATE(IDC_EDIT10, OnUpdateEdit10)
	ON_BN_CLICKED(IDC_BUTTON1, OnButton1)
	ON_BN_CLICKED(IDC_BUTTON2, OnButton2)
	ON_BN_CLICKED(IDC_BUTTON3, OnButton3)
	ON_BN_CLICKED(IDC_BUTTON4, OnButton4)
	ON_BN_CLICKED(IDC_BUTTON5, OnButton5)
	ON_BN_CLICKED(IDC_BUTTON6, OnButton6)
	ON_BN_CLICKED(IDC_CHECK3, OnCheck3)
	ON_BN_CLICKED(IDC_RADIO1, OnRadio1)
	ON_BN_CLICKED(IDC_RADIO2, OnRadio2)
	ON_BN_CLICKED(IDC_RADIO3, OnRadio3)
	ON_BN_CLICKED(IDC_RADIO4, OnRadio4)
	ON_BN_CLICKED(IDC_BUTTON7, OnButton7)
	ON_BN_CLICKED(IDC_BUTTON8, OnButton8)
	ON_WM_TIMER()
	ON_WM_CAPTURECHANGED()
	ON_BN_CLICKED(IDC_BUTTON9, OnButton9)
	ON_BN_CLICKED(IDC_BUTTON10, OnButton10)
	ON_BN_CLICKED(IDC_BUTTON11, OnButton11)
	ON_BN_CLICKED(IDC_RADIO5, OnRadio5)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CUHFReader18Dlg message handlers

BOOL CUHFReader18Dlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	m_list.InsertColumn(0,"EPC号",LVCFMT_LEFT,320);
	m_list.SetExtendedStyle(LVS_EX_FULLROWSELECT|LVS_EX_GRIDLINES |LVS_EX_HEADERDRAGDROP);
    m_bt3.EnableWindow(FALSE);
	m_bt4.EnableWindow(FALSE);
	m_bt5.EnableWindow(FALSE);
	m_bt6.EnableWindow(FALSE);
	m_bt7.EnableWindow(FALSE);
	m_bt8.EnableWindow(FALSE);
	m_bt10.EnableWindow(FALSE);
	m_bt11.EnableWindow(FALSE);
	int i;
	CString temp,str;
	for(i=1;i<10;i++)
	{
		str.Format("%d",i);
		m_opencom.AddString("COM"+str);	
	}
	m_opencom.SetCurSel(0);
	m_addr1.SetWindowText("FF");
    m_addr3.SetWindowText("00");
	m_startaddr.SetWindowText("00");
	m_num.SetWindowText("4");
	m_password.SetWindowText("00000000");
	m_writedata.SetWindowText("0000");
	m_baud1.AddString("9600bps");
	m_baud1.AddString("19200bps");
	m_baud1.AddString("38400bps");
	m_baud1.AddString("57600bps");
	m_baud1.AddString("115200bps");
	m_baud1.SetCurSel(3);

	m_baud2.AddString("9600bps");
	m_baud2.AddString("19200bps");
	m_baud2.AddString("38400bps");
	m_baud2.AddString("57600bps");
	m_baud2.AddString("115200bps");
	m_baud2.SetCurSel(3);

	m_workmode.AddString("应答模式");
	m_workmode.AddString("主动模式");
	m_workmode.AddString("触发模式(低电平)");
	m_workmode.AddString("触发模式(高电平)");
	m_workmode.SetCurSel(0);

	for(i=3;i<256;i++)
	{
		str.Format("%d",i);
		m_scantime.AddString(str+"*100ms");	
	}
	m_scantime.SetCurSel(7);

	for(i=0;i<31;i++)
	{
		str.Format("%d",i);
		m_DBM.AddString(str);	
	}
	m_DBM.SetCurSel(30);

	for(i=0;i<63;i++)
	{
		str.Format("%-3.1f",902.6 + i * 0.4);
		m_minfre.AddString(str+" MHz");
		m_maxfre.AddString(str+" MHz");
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(62);

	m_quyu.AddString("保留区");
    m_quyu.AddString("EPC区");
	m_quyu.AddString("TID区");
	m_quyu.AddString("用户区");
    m_quyu.SetCurSel(1);


((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(TRUE);
	m_startaddr.SetLimitText(2);
	m_num.SetLimitText(2);
	m_password.SetLimitText(8);
	
	g_hRRLibrary = LoadLibrary("UHFReader18.dll");
	
	if (g_hRRLibrary ==NULL)
		return FALSE;
	if(!(RR_OpenComPort = (POpenComPort)GetProcAddress(g_hRRLibrary,"OpenComPort")))
		return FALSE;
	if(!(RR_CloseComPort = (PCloseComPort)GetProcAddress(g_hRRLibrary,"CloseComPort")))
		return FALSE;
    
	if(!(RR_GetReaderInformation = (PGetReaderInformation)GetProcAddress(g_hRRLibrary,"GetReaderInformation")))
		return FALSE;
	if(!(RR_WriteComAdr = (PWriteComAdr)GetProcAddress(g_hRRLibrary,"WriteComAdr")))
		return FALSE;
	if(!(RR_SetPowerDbm = (PSetPowerDbm)GetProcAddress(g_hRRLibrary,"SetPowerDbm")))
		return FALSE;
	if(!(RR_Writedfre = (PWritedfre)GetProcAddress(g_hRRLibrary,"Writedfre")))
		return FALSE;
	if(!(RR_Writebaud = (PWritebaud)GetProcAddress(g_hRRLibrary,"Writebaud")))
		return FALSE;
	if(!(RR_WriteScanTime = (PWriteScanTime)GetProcAddress(g_hRRLibrary,"WriteScanTime")))
		return FALSE;
	if(!(RR_Inventory_G2 = (PInventory_G2)GetProcAddress(g_hRRLibrary,"Inventory_G2")))
		return FALSE;
   	if(!(RR_SetWorkMode = (PSetWorkMode)GetProcAddress(g_hRRLibrary,"SetWorkMode")))
		return FALSE;
	if(!(RR_GetWorkModeParameter = (PGetWorkModeParameter)GetProcAddress(g_hRRLibrary,"GetWorkModeParameter")))
		return FALSE;
	if(!(RR_ReadCard_G2 = (PReadCard_G2)GetProcAddress(g_hRRLibrary,"ReadCard_G2")))
		return FALSE;
	if(!(RR_WriteCard_G2 = (PWriteCard_G2)GetProcAddress(g_hRRLibrary,"WriteCard_G2")))
		return FALSE;
    if(!(RR_ReadActiveModeData = (PReadActiveModeData)GetProcAddress(g_hRRLibrary,"ReadActiveModeData")))
		return FALSE;
	UINT arr[3];
	for(i=0;i<3;i++)
	{
		arr[i]=200+i;
	}
	m_statusbar.Create(this);
    m_statusbar.SetIndicators(arr,sizeof(arr)/sizeof(UINT));
	for(i=0;i<3;i++)
	{
		m_statusbar.SetPaneInfo(i,arr[i],0,200);
	}
    m_statusbar.SetPaneInfo(2,arr[2],0,300);

	InventoryFlag=FALSE;
	fcloseApp=FALSE;
	
	RepositionBars(AFX_IDW_CONTROLBAR_FIRST,AFX_IDW_CONTROLBAR_LAST,0);
	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CUHFReader18Dlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CUHFReader18Dlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CUHFReader18Dlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CUHFReader18Dlg::OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
	// TODO: Add your message handler code here and/or call default


	CDialog::OnKeyDown(nChar, nRepCnt, nFlags);
}

void CUHFReader18Dlg::OnCancelMode() 
{
	CDialog::OnCancelMode();
	
	// TODO: Add your message handler code here
	
}

void CUHFReader18Dlg::OnUpdateEdit10() 
{

	// TODO: If this is a RICHEDIT control, the control will not
	// send this notification unless you override the CDialog::OnInitDialog()
	// function to send the EM_SETEVENTMASK message to the control
	// with the ENM_UPDATE flag ORed into the lParam mask.
	
	// TODO: Add your control notification handler code here
	
}

void CUHFReader18Dlg::OnButton1() 
{
	// TODO: Add your control notification handler code here
	int port;
	BYTE baud=0; 
	CString strport,str;
	GetDlgItemText(IDC_EDIT1,str);
	ComAddr=(BYTE)strtoul(str,NULL,16);
	
	port=m_opencom.GetCurSel()+1;
//	GetDlgItemText(IDC_COMBO1,str);
//	str=str.Right(1);
//	port=strtoul(str,NULL,16);
    baud=m_baud1.GetCurSel();
	if(baud>2)
		baud=baud+2;
	int ret;
	ret = RR_OpenComPort(port, &ComAddr,baud,&FrmHandle);
	if(ret==0)
	{
		OnButton3();
		m_statusbar.SetPaneText(0,"端口打开成功!");
		str.Format("%d",port);
		m_statusbar.SetPaneText(1,"COM"+str);
		m_bt3.EnableWindow(TRUE);
		m_bt4.EnableWindow(TRUE);
		m_bt5.EnableWindow(TRUE);
		m_bt6.EnableWindow(TRUE);
		m_bt7.EnableWindow(TRUE);
		m_bt8.EnableWindow(FALSE);
		m_bt10.EnableWindow(TRUE);
		m_bt11.EnableWindow(TRUE);
	m_bt11.EnableWindow(TRUE);
	}
	else if(ret==0x35)
	{
		m_statusbar.SetPaneText(0,"端口已打开或被占用!");
	}
	else
	{
		m_statusbar.SetPaneText(0,"端口打开失败!");
		m_statusbar.SetPaneText(1,"");
	}
	
}

void CUHFReader18Dlg::OnButton2() 
{
	// TODO: Add your control notification handler code here
	int ret = RR_CloseComPort();
	if(ret==0)
	{
		KillTimer(1);
		m_VersionInfo.SetWindowText("");
		m_addr2.SetWindowText("");
		m_editdbm.SetWindowText("");
		m_editscan.SetWindowText("");
		m_fman.SetWindowText("");
		m_fmin.SetWindowText("");
        m_ReaderType.SetWindowText("");
		m_statusbar.SetPaneText(0,"端口关闭成功!");
		m_statusbar.SetPaneText(1,"");
		m_list.DeleteAllItems();
		m_epc.ResetContent();
		m_bt3.EnableWindow(FALSE);
		m_bt4.EnableWindow(FALSE);
		m_bt5.EnableWindow(FALSE);
		m_bt6.EnableWindow(FALSE);
		m_bt7.EnableWindow(FALSE);
		m_bt8.EnableWindow(FALSE);
		m_bt10.EnableWindow(FALSE);
	    m_bt11.EnableWindow(FALSE);
	}
	else
	{
		m_statusbar.SetPaneText(0,"端口关闭失败!");
	}
}

void CUHFReader18Dlg::OnButton3() 
{
	// TODO: Add your control notification handler code here
	BYTE TrType[2];
	BYTE VersionInfo[2];
	BYTE ReaderType, ScanTime, dmaxfre, dminfre, PowerDbm, FreBand;
	CString str,temp ;
	int index,i ;
	m_VersionInfo.SetWindowText("");
	m_addr2.SetWindowText("");
    m_editdbm.SetWindowText("");
	m_editscan.SetWindowText("");
	m_fman.SetWindowText("");
	m_fmin.SetWindowText("");
    m_ReaderType.SetWindowText("");
	m_DBM.ResetContent();
	int fCmdRet = RR_GetReaderInformation(&ComAddr, VersionInfo, &ReaderType,TrType, &dmaxfre, &dminfre,&PowerDbm,&ScanTime, FrmHandle);
	if (fCmdRet == 0)
	{
		temp.Format("%02d",VersionInfo[0]);
		temp=temp+".";
		str .Format("%02d",VersionInfo[1]);
		str=temp+str;
        m_VersionInfo.SetWindowText(str);
		if (VersionInfo[1] >= 30)
		{
			for (i=0;i<31;i++)
			{
				str.Format("%d",i);
				m_DBM.AddString(str);
			}
			m_DBM.SetCurSel(30);
		}
		else
		{
			for (i=0;i<19;i++)
			{
				str.Format("%d",i);
				m_DBM.AddString(str);
			}
			m_DBM.SetCurSel(18);
		}
		
		str.Format("%02X",ComAddr);
		m_addr2.SetWindowText(str);
		m_addr3.SetWindowText(str);
		str.Format("%d",ScanTime);
		m_editscan.SetWindowText(str+"*100ms");
		index = ScanTime - 3;
		m_scantime.SetCurSel(index);
		str.Format("%d",PowerDbm);
		m_editdbm.SetWindowText(str);
		m_DBM.SetCurSel(PowerDbm);
		FreBand = (((dmaxfre & 0xC0) >> 4) | (dminfre >> 6));
		switch (FreBand)
		{
		case 0:
			{
				((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(TRUE);
				((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
				m_minfre.ResetContent();
				m_maxfre.ResetContent();
				for (i = 0;i< 63;i++)
				{
					fdminfre = 902.6 + (i & 0x3F) * 0.4;
					fdmaxfre = 902.6 + (i & 0x3F) * 0.4;
					str.Format("%3.1f",fdminfre);
					m_minfre.AddString(str);
					m_maxfre.AddString(str);
				}
				fdminfre = 902.6 + (dminfre & 0x3F) * 0.4;
			    fdmaxfre = 902.6 + (dmaxfre & 0x3F) * 0.4;
				str.Format("%3.1f",fdminfre);
				m_fmin.SetWindowText(str+ "MHz");
				str.Format("%3.1f",fdmaxfre);
		        m_fman.SetWindowText(str+ "MHz");
			}
			break;
		case 1:
			{
				((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(TRUE);
				((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
				m_minfre.ResetContent();
				m_maxfre.ResetContent();
				for (i = 0;i< 20;i++)
				{
					fdminfre = 920.125 + (i & 0x3F) * 0.250;
					fdmaxfre = 920.125 + (i & 0x3F) * 0.250;
					str.Format("%3.3f",fdminfre);
					m_minfre.AddString(str+" MHz");
					m_maxfre.AddString(str+" MHz");
				}
				fdminfre = 920.125 + (dminfre & 0x3F) * 0.250;
				fdmaxfre = 920.125 + (dmaxfre & 0x3F) * 0.250;
				str.Format("%3.3f",fdminfre);
				m_fmin.SetWindowText(str+ "MHz");
				str.Format("%3.3f",fdmaxfre);
		        m_fman.SetWindowText(str+ "MHz");
			}
			break;
		case 2:
			{
				((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(TRUE);
				((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
				m_minfre.ResetContent();
				m_maxfre.ResetContent();
				for (i = 0;i< 50;i++)
				{
					fdminfre = 902.75  + (i & 0x3F) * 0.50;
					fdmaxfre = 902.75  + (i & 0x3F) * 0.50;
					str.Format("%3.2f",fdminfre);
					m_minfre.AddString(str+" MHz");
					m_maxfre.AddString(str+" MHz");
				}
				fdminfre = 902.75 + (dminfre & 0x3F) * 0.50;
			    fdmaxfre = 902.75 + (dmaxfre & 0x3F) * 0.50;
				str.Format("%3.2f",fdminfre);
				m_fmin.SetWindowText(str+ "MHz");
				str.Format("%3.2f",fdmaxfre);
		        m_fman.SetWindowText(str+ "MHz");
			}
			break;
		case 3:
			{
				((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(TRUE);
				m_minfre.ResetContent();
				m_maxfre.ResetContent();
				for (i = 0;i< 32;i++)
				{
					fdminfre = 917.1  + (i & 0x3F) * 0.20;
					fdmaxfre = 917.1  + (i & 0x3F) * 0.20;
					str.Format("%3.2f",fdminfre);
					m_minfre.AddString(str+" MHz");
					m_maxfre.AddString(str+" MHz");
				}
				fdminfre = 917.1 + ((int)dminfre & 0x3F) * 0.2;
				fdmaxfre = 917.1 + ((int)dmaxfre & 0x3F) * 0.2;
				str.Format("%3.1f",fdminfre);
				m_fmin.SetWindowText(str+ "MHz");
				str.Format("%3.1f",fdmaxfre);
		        m_fman.SetWindowText(str+ "MHz");
			}
			break;
        case 4:
			{
				((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
				((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(TRUE);
				m_minfre.ResetContent();
				m_maxfre.ResetContent();
				for (i = 0;i< 15;i++)
				{
					fdminfre = 865.1  + (i & 0x3F) * 0.20;
					fdmaxfre = 865.1  + (i & 0x3F) * 0.20;
					str.Format("%3.2f",fdminfre);
					m_minfre.AddString(str+" MHz");
					m_maxfre.AddString(str+" MHz");
				}
				fdminfre = 865.1 + ((int)dminfre & 0x3F) * 0.2;
				fdmaxfre = 865.1 + ((int)dmaxfre & 0x3F) * 0.2;
				str.Format("%3.1f",fdminfre);
				m_fmin.SetWindowText(str+ "MHz");
				str.Format("%3.1f",fdmaxfre);
				m_fman.SetWindowText(str+ "MHz");
			}
			break;
		}		
		if (fdmaxfre == fdminfre)
		m_check3.SetCheck(1);
		m_minfre.SetCurSel(dminfre & 0x3F);
		m_maxfre.SetCurSel(dmaxfre & 0x3F);

		switch (ReaderType)
		{
		case 6:
				m_ReaderType.SetWindowText("");
				break;
		case 3:
				m_ReaderType.SetWindowText("");
				break;
		case 9:
			m_ReaderType.SetWindowText("UHFReader18");
				break;
		}
		if ((TrType[0] & 0x02) == 0x02)
		{
			((CButton *)GetDlgItem(IDC_CHECK1))->SetCheck(TRUE);
			((CButton *)GetDlgItem(IDC_CHECK2))->SetCheck(TRUE);
		}
		else
		{
			((CButton *)GetDlgItem(IDC_CHECK1))->SetCheck(FALSE);
			((CButton *)GetDlgItem(IDC_CHECK2))->SetCheck(FALSE);
		}
			m_statusbar.SetPaneText(0,"获取读写器信息成功!");
	}
	else
		m_statusbar.SetPaneText(0,"获取读写器信息失败!");
	
}

void CUHFReader18Dlg::OnButton4() 
{
	// TODO: Add your control notification handler code here
	BYTE Parameter[10];
	Parameter[0] = m_workmode.GetCurSel();
	Parameter[1] = 2;
	Parameter[2] = 1;
	Parameter[3] = 0;
	Parameter[4] = 1;
	Parameter[5] = 0;
	int	fCmdRet = RR_SetWorkMode(&ComAddr, Parameter, FrmHandle);
	if (fCmdRet == 0)
		m_statusbar.SetPaneText(0,"模式设置成功!");
	else
	m_statusbar.SetPaneText(0,"模式设置失败!");

}

void CUHFReader18Dlg::OnButton5() 
{
	// TODO: Add your control notification handler code here
	BYTE Parameter[20];
	int	fCmdRet = RR_GetWorkModeParameter(&ComAddr, Parameter, FrmHandle);
	if (fCmdRet == 0) 
	{
		m_workmode.SetCurSel(Parameter[4]);
		m_statusbar.SetPaneText(0,"模式获取成功!");
	}
	else
	m_statusbar.SetPaneText(0,"模式获取失败!");
}

void CUHFReader18Dlg::OnButton6() 
{
	// TODO: Add your control notification handler code here
	BYTE aNewComAdr, PowerDbm, dminfre, dmaxfre, ScanTime, band;
	BYTE fBaud;
    CString returninfo,returninfoDlg,setinfo,str;
    setinfo = "";
    returninfoDlg = "";
    returninfo = "";
	if(((CButton *)GetDlgItem(IDC_RADIO1))->GetCheck())
		band=0;
	if(((CButton *)GetDlgItem(IDC_RADIO2))->GetCheck())
		band=1;
	if(((CButton *)GetDlgItem(IDC_RADIO3))->GetCheck())
		band=2;
	if(((CButton *)GetDlgItem(IDC_RADIO4))->GetCheck())
		band=3;
	if(((CButton *)GetDlgItem(IDC_RADIO5))->GetCheck())
		band=4;
	dminfre = (((band & 3) * 64) | (m_minfre.GetCurSel() & 0x3F));
    dmaxfre = (((band & 0x0C) * 16) | (m_maxfre.GetCurSel() & 0x3F));
	GetDlgItemText(IDC_EDIT9,str);
	aNewComAdr=(BYTE)strtoul(str,NULL,16);
    PowerDbm = m_DBM.GetCurSel();
    fBaud = m_baud2.GetCurSel();
    if (fBaud > 2)
	fBaud = fBaud + 2;
    ScanTime =m_scantime.GetCurSel()+3 ;
    setinfo = "Write";
	int fCmdRet = RR_WriteComAdr(&ComAddr, &aNewComAdr, FrmHandle);
    if (fCmdRet == 0)
	{
		ComAddr = aNewComAdr;
	   returninfo = returninfo + setinfo + "地址设置成功";
	}
	else
	returninfo = returninfo + setinfo + "地址设置失败";
    fCmdRet = RR_SetPowerDbm(&ComAddr, PowerDbm, FrmHandle);
    if (fCmdRet == 0) 
	returninfo = returninfo + ",功率设置成功";
    else
	returninfo = returninfo + ",功率设置失败";

    fCmdRet = RR_Writedfre(&ComAddr, &dmaxfre, &dminfre, FrmHandle);
    if (fCmdRet == 0) 
	returninfo = returninfo + ",频点设置成功";
    else	
	returninfo = returninfo + ",频点设置失败";
	
    fCmdRet = RR_Writebaud(&ComAddr, &fBaud, FrmHandle);
    if (fCmdRet == 0) 
	returninfo = returninfo + ",波特率设置成功";
    else
	returninfo = returninfo + ",波特率设置失败";

    fCmdRet = RR_WriteScanTime(&ComAddr, &ScanTime, FrmHandle);
    if (fCmdRet == 0) 
	returninfo = returninfo + ",询查时间设置成功";
    else
	returninfo = returninfo + ",询查时间设置失败";
    m_statusbar.SetPaneText(0,returninfo);
}

void CUHFReader18Dlg::OnCheck3() 
{
	// TODO: Add your control notification handler code here
	if(((CButton *)GetDlgItem(IDC_CHECK3))->GetCheck())
		m_maxfre.SetCurSel(m_minfre.GetCurSel());	
}

void CUHFReader18Dlg::OnRadio1() 
{
	// TODO: Add your control notification handler code here
	int i;
	CString str;
	m_minfre.ResetContent();
	m_maxfre.ResetContent();
	((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(FALSE);
	for (i = 0;i< 63;i++)
	{
		fdminfre = 902.6 + (i & 0x3F) * 0.4;
		fdmaxfre = 902.6 + (i & 0x3F) * 0.4;
		str.Format("%3.1f",fdminfre);
		m_minfre.AddString(str);
		m_maxfre.AddString(str);
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(62);
}

void CUHFReader18Dlg::OnRadio2() 
{
	// TODO: Add your control notification handler code here
	int i;
	CString str;
	m_minfre.ResetContent();
	m_maxfre.ResetContent();
	((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(FALSE);
	for (i = 0;i< 20;i++)
	{
		fdminfre = 920.125 + (i & 0x3F) * 0.250;
		fdmaxfre = 920.125 + (i & 0x3F) * 0.250;
		str.Format("%3.3f",fdminfre);
		m_minfre.AddString(str+" MHz");
		m_maxfre.AddString(str+" MHz");
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(19);
}

void CUHFReader18Dlg::OnRadio3() 
{
	// TODO: Add your control notification handler code here
	int i;
	CString str;
	m_minfre.ResetContent();
	m_maxfre.ResetContent();
	((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(FALSE);
	for (i = 0;i< 50;i++)
	{
		fdminfre = 902.75  + (i & 0x3F) * 0.50;
		fdmaxfre = 902.75  + (i & 0x3F) * 0.50;
		str.Format("%3.2f",fdminfre);
		m_minfre.AddString(str+" MHz");
		m_maxfre.AddString(str+" MHz");
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(49);
}

void CUHFReader18Dlg::OnRadio4() 
{
	// TODO: Add your control notification handler code here
	int i;
	CString str;
	m_minfre.ResetContent();
	m_maxfre.ResetContent();
	((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(TRUE);
	((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(FALSE);
	for (i = 0;i< 32;i++)
	{
		fdminfre = 917.1  + (i & 0x3F) * 0.20;
		fdmaxfre = 917.1  + (i & 0x3F) * 0.20;
		str.Format("%3.2f",fdminfre);
		m_minfre.AddString(str+" MHz");
		m_maxfre.AddString(str+" MHz");
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(31);
}

void CUHFReader18Dlg::OnButton7() 
{
	// TODO: Add your control notification handler code here
	m_bt3.EnableWindow(FALSE);
	m_bt4.EnableWindow(FALSE);
	m_bt5.EnableWindow(FALSE);
	m_bt6.EnableWindow(FALSE);
	m_bt7.EnableWindow(FALSE);
	m_bt8.EnableWindow(TRUE);
	m_bt10.EnableWindow(FALSE);
	m_bt11.EnableWindow(FALSE);
	m_list.DeleteAllItems();
	m_epc.ResetContent();
	list.RemoveAll();
	SetTimer(1,50,NULL);
}

void CUHFReader18Dlg::OnButton8() 
{
	// TODO: Add your control notification handler code here
	KillTimer(1);	
	m_bt3.EnableWindow(TRUE);
	m_bt4.EnableWindow(TRUE);
	m_bt5.EnableWindow(TRUE);
	m_bt6.EnableWindow(TRUE);
	m_bt7.EnableWindow(TRUE);
	m_bt8.EnableWindow(FALSE);
	m_bt10.EnableWindow(TRUE);
	m_bt11.EnableWindow(TRUE);

}
void CUHFReader18Dlg::InventoryEPC()
{
	int CardNum,Totallen,EPClen, m,n,CardIndex;
	BOOL EPCflag ;
    BYTE EPC[5000];
	BYTE EPC1[500];
	BYTE ScanModeData[5000];
	int Datalength=0;
	BYTE AdrTID,LenTID,TIDFlag;
    CString temps,s, sEPC,temp;
	AdrTID=0;
    LenTID=0;
	TIDFlag=0;
	if(m_workmode.GetCurSel()==0)
	{
	   EPCflag = FALSE;
	   int fCmdRet = RR_Inventory_G2(&ComAddr,AdrTID,LenTID,TIDFlag, EPC, &Totallen, &CardNum, FrmHandle);
	   if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))
	   {
		   fInventory_EPC_List = temps;
		   m = 1;
		   if (CardNum == 0)
		   {
			   fIsInventoryScan = FALSE;
			   return;
		   }
		   for (CardIndex = 0 ;CardIndex<CardNum;CardIndex++)
		   {
			   temp="";
			   temps="";
			   if (fcloseApp)
				   break;
			   EPCflag = FALSE;
			   EPClen = EPC[m - 1];
			   memcpy(EPC1,&EPC[m], EPClen);
			   m = m + EPClen + 1;
			   for(n=0;n<EPClen;n++)
			   {
				   temp.Format("%02X",EPC1[n]);
				   temps=temps+temp;
			   }
			   POSITION pos = list.GetHeadPosition();
			   list.AddHead(temps);
			   while(pos!=NULL)
			   {
				   
				   CString strText = list.GetNext(pos);
				   if(temps==strText)
				   {
					   EPCflag=TRUE;
				   }
				   //函数里
			   }
			   if(EPCflag==FALSE)
				   m_epc.AddString(temps);	
			   if(m_epc.GetCount()>0)
				   m_epc.SetCurSel(0);
			   m_list.InsertItem(0,temps);
			   m_list.EnsureVisible(m_list.GetItemCount()-1,TRUE);
		   }
		   fIsInventoryScan = FALSE;
		   //	if (fcloseApp)	   
	   }
   }
	else
	{
		int fCmdRet = RR_ReadActiveModeData(ScanModeData,&Datalength,FrmHandle);
        if(Datalength>0)
		{
			for(n=0;n<Datalength;n++)
			{
				temp.Format("%02X",ScanModeData[n]);
				temps=temps+temp;
			}
			m_list.InsertItem(0,temps);
			m_list.EnsureVisible(m_list.GetItemCount()-1,TRUE);
		}
	}
}
void CUHFReader18Dlg::OnTimer(UINT nIDEvent) 
{
	// TODO: Add your message handler code here and/or call default
	
	if(nIDEvent==1)
	{
		if(InventoryFlag)
			return;
		InventoryFlag=TRUE;
		InventoryEPC();
	    InventoryFlag=FALSE;
	}
	CDialog::OnTimer(nIDEvent);
}

void CUHFReader18Dlg::OnCaptureChanged(CWnd *pWnd) 
{
	// TODO: Add your message handler code here
	
	CDialog::OnCaptureChanged(pWnd);
}

void CUHFReader18Dlg::OnButton9() 
{
	// TODO: Add your control notification handler code here
	m_list.DeleteAllItems();
}

void CUHFReader18Dlg::OnButton10() 
{
	// TODO: Add your control notification handler code here
	BYTE Mem, Num, WordPtr,EPClength, maskFlag, maskadr, maskLen ;
    int i,Errorcode ;
	CString str,s2,temp,temps;
	BYTE CardData[320];
    BYTE fOperEPC[320];
	BYTE fPassword[4];
	SetDlgItemText(IDC_EDIT14,"");
    GetDlgItemText(IDC_EDIT10,str);
	if(str=="")
		return;
	WordPtr=(BYTE)strtoul(str,NULL,16);
    GetDlgItemText(IDC_EDIT11,str);
	if(str=="")
		return;
    Num=(BYTE)strtoul(str,NULL,10)*2;
    GetDlgItemText(IDC_EDIT12,str);
	if(str.GetLength()<8)
	{
		MessageBox("密码长度不能小于8！",NULL,MB_OK);
		return;
	}
	int n=str.GetLength()/2;
	for(i=0;i<n;i++)
	{
		fPassword[i]=(BYTE)strtoul(str.Left(2),NULL,16);
		str=str.Right(str.GetLength()-2);
	}
	maskFlag = 0;
	maskadr = 0;
	maskLen = 0;
	if(m_epc.GetCount()==0)
		return;
	int index=m_epc.GetCurSel();
	m_epc.GetLBText(index,str);
	if(str=="")
		return;
	EPClength=str.GetLength()/2;
    for(i=0;i<EPClength;i++)
	{
		fOperEPC[i]=(BYTE)strtoul(str.Left(2),NULL,16);
		str=str.Right(str.GetLength()-2);
	}
	Mem = m_quyu.GetCurSel();	 
	int fCmdRet = RR_ReadCard_G2(&ComAddr, fOperEPC, Mem, WordPtr, Num, fPassword, maskadr, maskLen, maskFlag, CardData, EPClength, &Errorcode, FrmHandle);
	if (fCmdRet == 0)
	{
		temps="";
		temp="";
		for(i=0;i<Num;i++)
		{
			temp.Format("%02X",CardData[i]);
			temps=temps+temp;
		}

		SetDlgItemText(IDC_EDIT14,temps);
		m_statusbar.SetPaneText(0,"读数据成功!");
	}
	else
	{
		m_statusbar.SetPaneText(0,"读数据失败!");
	}
}

void CUHFReader18Dlg::OnButton11() 
{
	// TODO: Add your control notification handler code here
	BYTE Mem, Writedatalen, WordPtr,EPClength, maskFlag, maskadr, maskLen ;
    int i,Errorcode ,WritedataNum;
	CString str,s2,temp,temps;
	BYTE CardData[320];
    BYTE fOperEPC[320];
	BYTE fPassword[4];
	SetDlgItemText(IDC_EDIT14,"");
    GetDlgItemText(IDC_EDIT10,str);
	if(str=="")
		return;
	WordPtr=(BYTE)strtoul(str,NULL,16);
    GetDlgItemText(IDC_EDIT12,str);
	if(str.GetLength()<8)
	{
		MessageBox("密码长度不能小于8！",NULL,MB_OK);
		return;
	}
	int n=str.GetLength()/2;
	for(i=0;i<n;i++)
	{
		fPassword[i]=(BYTE)strtoul(str.Left(2),NULL,16);
		str=str.Right(str.GetLength()-2);
	}

	GetDlgItemText(IDC_EDIT13,str);
	if((str.GetLength()%4) !=0)
	{
		MessageBox("请输入以字数单位长度的字符串！",NULL,MB_OK);
		return;
	}
	 Writedatalen=str.GetLength()/2;
	for(i=0;i<Writedatalen;i++)
	{
		CardData[i]=(BYTE)strtoul(str.Left(2),NULL,16);
		str=str.Right(str.GetLength()-2);
	}
	maskFlag = 0;
	maskadr = 0;
	maskLen = 0;
	if(m_epc.GetCount()==0)
		return;
	int index=m_epc.GetCurSel();
	m_epc.GetLBText(index,str);
	if(str=="")
		return;
	EPClength=str.GetLength()/2;
    for(i=0;i<EPClength;i++)
	{
		fOperEPC[i]=(BYTE)strtoul(str.Left(2),NULL,16);
		str=str.Right(str.GetLength()-2);
	}
	Mem = m_quyu.GetCurSel();	 
	int fCmdRet =  RR_WriteCard_G2(&ComAddr, fOperEPC, Mem, WordPtr, Writedatalen, CardData, fPassword, maskadr, maskLen, maskFlag, &WritedataNum, EPClength, &Errorcode, FrmHandle);
	if (fCmdRet == 0)
	{
		m_statusbar.SetPaneText(0,"写数据成功!");
	}
	else
	{
		m_statusbar.SetPaneText(0,"写数据失败!");
	}
}

void CUHFReader18Dlg::OnRadio5() 
{
	// TODO: Add your control notification handler code here
	int i;
	CString str;
	m_minfre.ResetContent();
	m_maxfre.ResetContent();
	((CButton *)GetDlgItem(IDC_RADIO1))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO2))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO3))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO4))->SetCheck(FALSE);
	((CButton *)GetDlgItem(IDC_RADIO5))->SetCheck(TRUE);
	for (i = 0;i< 15;i++)
	{
		fdminfre = 865.1 + (i & 0x3F) * 0.2;
		fdmaxfre = 865.1 + (i & 0x3F) * 0.2;
		str.Format("%3.1f",fdminfre);
		m_minfre.AddString(str);
		m_maxfre.AddString(str);
	}
	m_minfre.SetCurSel(0);
	m_maxfre.SetCurSel(14);
}
