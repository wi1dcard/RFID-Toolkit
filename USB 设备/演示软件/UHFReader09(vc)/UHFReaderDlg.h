// UHFReaderDlg.h : header file
//

#if !defined(AFX_UHFReaderDLG_H__3A770EAB_F39A_4545_B43E_703F7CE3A186__INCLUDED_)
#define AFX_UHFReaderDLG_H__3A770EAB_F39A_4545_B43E_703F7CE3A186__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "Afxtempl.h "
#include "UHFReader.h"
#include "UHFReaderDlg.h"

/////////////////////////////////////////////////////////////////////////////
// CUHFReaderDlg dialog
const CString g_strInvalid = "FFFFFFFFFFFFFFFF";
const CString g_strNull    = "0000000000000000";
	const int g_nTimerUnitsPerSecond = 10000000;
class CUHFReaderDlg : public CDialog
{
// Construction
public:
	CUHFReaderDlg(CWnd* pParent = NULL);	// standard constructor
	typedef int (FAR WINAPI*POpenComPort)(int port, unsigned char *ComAdr,unsigned char baud,int *Frmhandle);
    typedef int (FAR WINAPI*PCloseComPort)();
	typedef int (FAR WINAPI*PGetReaderInformation)(unsigned char *ComAdr,
		                                           unsigned char *VersionInfo, 
												   unsigned char *ReaderType,
												   unsigned char *TrType, 
												   unsigned char *dmaxfre, 
												   unsigned char *dminfre, 
												   unsigned char *PowerDbm, 
												   unsigned char *ScanTime, 
												   int FrmHandle);
		
	typedef int (FAR WINAPI*PWriteComAdr)(unsigned char *ComAdr, 
										  unsigned char *ComAdrData, 
										  int FrmHandle);
		
	typedef int (FAR WINAPI*PSetPowerDbm)(unsigned char *ComAdr, 
		                                  unsigned char PowerDbm, 
										  int FrmHandle);
		
	typedef int (FAR WINAPI*PWritedfre)(unsigned char *ComAdr, 
		                                unsigned char *dmaxfre, 
										unsigned char *dminfre, 
										int FrmHandle);
		
	typedef int (FAR WINAPI*PWritebaud)(unsigned char *ComAdr,
		                                unsigned char *Baud,
										int FrmHandle);
		
	typedef int (FAR WINAPI*PWriteScanTime)(unsigned char *ComAdr,
		                                    unsigned char *ScanTime, 
											int FrmHandle);
		
	typedef int (FAR WINAPI*PInventory_G2)(unsigned char *ComAdr, 
		                                   unsigned char AdrTID, 
										   unsigned char LenTID, 
										   unsigned char TIDFlag,
		                                   unsigned char *EPClenandEPC, 
										   int *Totallen, 
										   int *CardNum, 
										   int FrmHandle);

		
	typedef int (FAR WINAPI*PReadCard_G2)(unsigned char *ComAdr, 
		                                  unsigned char *EPC, 
										  unsigned char Mem, 
										  unsigned char WordPtr, 
										  unsigned char Num, 
										  unsigned char *Password, 
										  unsigned char maskadr, 
										  unsigned char maskLen, 
										  unsigned char maskFlag, 
										  unsigned char *Data, 
										  unsigned char EPClength, 
										  int *errorcode, 
										  int FrmHandle);
		
	typedef int (FAR WINAPI*PWriteCard_G2)(unsigned char *ComAdr, 
										   unsigned char *EPC,
										   unsigned char Mem,
										   unsigned char WordPtr, 
										   unsigned char Writedatalen,
										   unsigned char *WrittenData,
										   unsigned char *Password,
										   unsigned char maskadr,
										   unsigned char maskLen,
										   unsigned char maskFlag,
										   int *WrittenDataNum, 
										   unsigned char EPClength, 
										   int *errorcode, 
										   int FrmHandle);


	HINSTANCE g_hRRLibrary;
	void InventoryEPC();



	POpenComPort		RR_OpenComPort;
	PCloseComPort       RR_CloseComPort;
	PGetReaderInformation   RR_GetReaderInformation;
	PWriteComAdr        RR_WriteComAdr;
	PSetPowerDbm        RR_SetPowerDbm;
	PWritedfre          RR_Writedfre;
	PWritebaud          RR_Writebaud;
	PWriteScanTime      RR_WriteScanTime;
	PInventory_G2       RR_Inventory_G2;
	PReadCard_G2        RR_ReadCard_G2;
	PWriteCard_G2       RR_WriteCard_G2;
// Dialog Data
	//{{AFX_DATA(CUHFReaderDlg)
	enum { IDD = IDD_UHFReader_DIALOG };
	CButton	m_bt11;
	CButton	m_bt10;
	CButton	m_bt8;
	CButton	m_bt7;
	CButton	m_bt6;
	CButton	m_bt3;
	CComboBox	m_epc;
	CButton	m_check3;
	CEdit	m_editscan;
	CEdit	m_fman;
	CEdit	m_fmin;
	CEdit	m_editdbm;
	CEdit	m_VersionInfo;
	CEdit	m_ReaderType;
	CEdit	m_readdata;
	CEdit	m_writedata;
	CEdit	m_password;
	CEdit	m_num;
	CEdit	m_startaddr;
	CComboBox	m_quyu;
	CButton	m_rad;
	CEdit	m_addr3;
	CEdit	m_addr2;
	CEdit	m_addr1;
	CListCtrl	m_list;
	CComboBox	m_maxfre;
	CComboBox	m_minfre;
	CComboBox	m_scantime;
	CComboBox	m_baud2;
	CComboBox	m_DBM;
	CComboBox	m_baud1;
	CComboBox	m_opencom;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CUHFReaderDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;
	int FrmHandle;
	BYTE ComAddr;
	bool TimerFlag;
	bool ClearFlag;
	bool InventoryFlag;
	CString  m_str;
	CStatusBar m_statusbar;
	DOUBLE fdminfre,fdmaxfre;
    bool fIsInventoryScan;
	CString fInventory_EPC_List;
	BOOL fcloseApp;
	CList<CString ,CString&> list;//

	// Generated message map functions
	//{{AFX_MSG(CUHFReaderDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnKeyDown(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnCancelMode();
	afx_msg void OnUpdateEdit10();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnButton3();
	afx_msg void OnButton4();
	afx_msg void OnButton5();
	afx_msg void OnButton6();
	afx_msg void OnCheck3();
	afx_msg void OnRadio1();
	afx_msg void OnRadio2();
	afx_msg void OnRadio3();
	afx_msg void OnRadio4();
	afx_msg void OnButton7();
	afx_msg void OnButton8();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnCaptureChanged(CWnd *pWnd);
	afx_msg void OnButton9();
	afx_msg void OnButton10();
	afx_msg void OnButton11();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_UHFReaderDLG_H__3A770EAB_F39A_4545_B43E_703F7CE3A186__INCLUDED_)
