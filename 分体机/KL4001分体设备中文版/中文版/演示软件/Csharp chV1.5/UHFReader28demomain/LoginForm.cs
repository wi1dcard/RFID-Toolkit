using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UHFReader28demomain
{
    public partial class LoginForm : Form
    {
        public string UserName
        {
            get
            {
                return this.txtUsername.Text.Trim();
            }
        }

        public string Password
        {
            get
            {
                return this.txtPwd.Text.Trim();
            }
        }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}