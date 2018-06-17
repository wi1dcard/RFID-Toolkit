// UHFReader18.h : main header file for the UHFReader18 application
//

#if !defined(AFX_UHFReader18_H__52F06B6D_C4FC_4B29_A6C6_B7559B7E5F25__INCLUDED_)
#define AFX_UHFReader18_H__52F06B6D_C4FC_4B29_A6C6_B7559B7E5F25__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CUHFReader18App:
// See UHFReader18.cpp for the implementation of this class
//

class CUHFReader18App : public CWinApp
{
public:
	CUHFReader18App();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CUHFReader18App)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CUHFReader18App)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_UHFReader18_H__52F06B6D_C4FC_4B29_A6C6_B7559B7E5F25__INCLUDED_)
