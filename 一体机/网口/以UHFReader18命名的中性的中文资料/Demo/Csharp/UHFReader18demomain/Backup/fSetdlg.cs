using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UHFReader18demomain
{
    public partial class fSetdlg : Form
    {
        public void _usename(string usename)
        {
            textBox1.Text = usename;
        }
        public void _dsname(string dsname)
        {
            textBox2.Text = dsname;
        }
        public void _MAC(string MAC)
        {
            textBox3.Text = MAC;
        }
        public void _IP(string IP)
        {
            textBox4.Text = IP;
        }
        public void _portnum(string portnum)
        {
            textBox5.Text = portnum;
        }
        public void _tup(string tup)
        {
            comboBox1.SelectedIndex = Convert.ToInt32(tup);
        }
        public void _rm(string rm)
        {
            comboBox2.SelectedIndex = Convert.ToInt32(rm);
        }
        public void _di(string di)
        {
            textBox6.Text = di;
        }
        public void _gi(string gi)
        {
            textBox7.Text = gi;
        }
        public void _nm(string nm)
        {
            textBox8.Text = nm;
        }
        public void _dp(string dp)
        {
            textBox9.Text = dp;
        }
        
        public void _br(string br)
        {
            comboBox3.SelectedIndex = Convert.ToInt32(br);
        }
        public void _pr(string pr)
        {
            comboBox4.SelectedIndex = Convert.ToInt32(pr);
        }
        public void _bb(string bb)
        {
            comboBox5.SelectedIndex = Convert.ToInt32(bb);
        }
        public void _dt(string dt)
        {
            comboBox6.SelectedIndex = Convert.ToInt32(dt);
        }
        public void _fc(string fc)
        {
            comboBox7.SelectedIndex = Convert.ToInt32(fc);
        }
       /* public void _cm(string cm1)
        {
            Information.cm = cm1;
        }
        public void _ct(string ct1)
        {
            Information.ct = ct1;
        }
        public void _ml(string ml1)
        {
            Information.ml = ml1;
        }
        public void _md(string md1)
        {
            Information.md = md1;
        }
        public void _rc(string rc1)
        {
            Information.rc = rc1;
        }*/
        public fSetdlg()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fNhSetDlg fnhSetDlg = new fNhSetDlg();
            fnhSetDlg._cm(Information.cm);
            fnhSetDlg._ct(Information.ct);
            DialogResult result=fnhSetDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    Information.cm=fnhSetDlg.cm;
                    Information.ct = fnhSetDlg.ct;
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                }
            }
            fnhSetDlg.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fPhSetDlg fphSetDlg = new fPhSetDlg();
            fphSetDlg._md(Information.md);
            fphSetDlg._ml(Information.ml);
            fphSetDlg._rc(Information.rc);
            DialogResult result = fphSetDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    Information.md = fphSetDlg.md;
                    Information.ml = fphSetDlg.ml;
                    Information.rc = fphSetDlg.rc;
                }
                catch (System.Exception ex)
                {
                    ex.ToString();
                }
            }
            fphSetDlg.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Information.usename = textBox1.Text.Trim();
            Information.dsname = textBox2.Text.Trim();
            Information.mac = textBox3.Text.Trim();
            Information.IP = textBox4.Text.Trim();
            Information.portnum = textBox5.Text.Trim();
            Information.tup = Convert.ToString(comboBox1.SelectedIndex);
            Information.rm = Convert.ToString(comboBox2.SelectedIndex);
            Information.di = textBox6.Text.Trim();
            Information.gi = textBox7.Text.Trim();
            Information.nm = textBox8.Text.Trim();
            Information.dp = textBox9.Text.Trim();
            Information.br = Convert.ToString(comboBox3.SelectedIndex);
            Information.pr = Convert.ToString(comboBox4.SelectedIndex);
            Information.bb = Convert.ToString(comboBox5.SelectedIndex);
            Information.dt = Convert.ToString(comboBox6.SelectedIndex);
            Information.fc = Convert.ToString(comboBox7.SelectedIndex); 
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex==0)
            {
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox8.ReadOnly = true;
                textBox9.ReadOnly = true;
            }
            else
            {
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBox8.ReadOnly = false;
                textBox9.ReadOnly = false;
            }
        }
    }
}