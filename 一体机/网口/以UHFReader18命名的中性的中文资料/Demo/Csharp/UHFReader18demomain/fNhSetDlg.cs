using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UHFReader18demomain
{
    public partial class fNhSetDlg : Form
    {
        public void _cm(string cm)
        {
            comboBox1.SelectedIndex = Convert.ToInt32(cm);
        }
        public void _ct(string ct)
        {
            comboBox2.Items.Clear();
            for (int i = 0; i < 100; i++)
            {
                comboBox2.Items.Add(Convert.ToString(i));
            }
            comboBox2.SelectedIndex = Convert.ToInt32(ct);
        }
        public string cm
        {
            get
            {
                return this.comboBox1.SelectedIndex.ToString();
            }
        }
        public string ct
        {
            get
            {
                return this.comboBox2.SelectedIndex.ToString();
            }
        }
        public fNhSetDlg()
        {
            InitializeComponent();
        }

        private void fNhSetDlg_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}