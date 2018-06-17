using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UHFReader18demomain
{
    public partial class fPhSetDlg : Form
    {
        public void _ml(string ml)
        {
            textBox1.Text = ml;
        }
        public void _md(string md)
        {
            textBox2.Text = md;
        }
        public void _rc(string rc)
        {
            comboBox1.SelectedIndex = Convert.ToInt32(rc);
        }

        public string ml
        {
            get
            {
                return this.textBox1.Text.Trim();
            }
        }
        public string md
        {
            get
            {
                return this.textBox2.Text.Trim();
            }
        }
        public string rc
        {
            get
            {
                return this.comboBox1.SelectedIndex.ToString();
            }
        }
        public fPhSetDlg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Information.ml = textBox1.Text;
            Information.md = textBox2.Text;
            Information.rc = comboBox1.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}