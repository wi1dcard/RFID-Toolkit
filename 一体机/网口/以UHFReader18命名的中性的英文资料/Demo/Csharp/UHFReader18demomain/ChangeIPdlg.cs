using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UHFReader18demomain
{
    public partial class ChangeIPdlg : Form
    {
        public string IP1
        {
            get
            {
                return this.textBox_IP1.Text.Trim();
            }
        }

        public string IP2
        {
            get
            {
                return this.textBox_IP2.Text.Trim();
            }
        }
        public string IP3
        {
            get
            {
                return this.textBox_IP3.Text.Trim();
            }
        }

        public string IP4
        {
            get
            {
                return this.textBox_IP4.Text.Trim();
            }
        }
        public ChangeIPdlg()
        {
            InitializeComponent();
        }
        private void MaskIPAddr(TextBox textBox, KeyPressEventArgs e)
        {
            //判断输入的值是否为数字或删除键
            if (char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {

                if (e.KeyChar != 8 && textBox.Text.Length == 2)
                {
                    string tempStr = textBox.Text + e.KeyChar;
                    if (textBox.Name == "textBox_IP1")
                    {
                        if (Int32.Parse(tempStr) > 223)
                        {
                            MessageBox.Show(tempStr + " 不是一个有效项目。请指定一个介于 1 和 223 之间的数值。");
                            textBox.Text = "223";
                            textBox.Focus();
                            return;
                        }
                        this.textBox_IP2.Focus();
                        this.textBox_IP2.SelectAll();
                    }
                    else if (textBox.Name == "textBox_IP2")
                    {
                        if (Int32.Parse(tempStr) > 255)
                        {
                            MessageBox.Show(tempStr + " 不是一个有效项目。请指定一个介于 1 和 255 之间的数值。");
                            textBox.Text = "255";
                            textBox.Focus();
                            return;
                        }
                        this.textBox_IP3.Focus();
                        this.textBox_IP3.SelectAll();
                    }
                    else if (textBox.Name == "textBox_IP3")
                    {
                        if (Int32.Parse(tempStr) > 255)
                        {
                            MessageBox.Show(tempStr + " 不是一个有效项目。请指定一个介于 1 和 225 之间的数值。");
                            textBox.Text = "255";
                            textBox.Focus();
                            return;
                        }
                        this.textBox_IP4.Focus();
                        this.textBox_IP4.SelectAll();
                    }
                    else if (textBox.Name == "textBox_IP4")
                    {
                        if (Int32.Parse(tempStr) > 255)
                        {
                            MessageBox.Show(tempStr + "不是一个有效项目。请指定一个介于 1 和 225 之间的数值。");
                            textBox.Text = "255";
                            textBox.Focus();
                            return;
                        }
                    }
                }
                else if (e.KeyChar == 8)
                {
                    if (textBox.Name == "textBox_IP1" && textBox.Text.Length == 0)
                    {
                        this.textBox_IP2.Focus();
                    }
                    else if (textBox.Name == "textBox_IP2" && textBox.Text.Length == 0)
                    {
                        this.textBox_IP3.Focus();
                    }
                    else if (textBox.Name == "textBox_IP3" && textBox.Text.Length == 0)
                    {
                        this.textBox_IP4.Focus();
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox_IP1_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIPAddr(this.textBox_IP1, e);
        }

        private void textBox_IP2_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIPAddr(this.textBox_IP2, e);
        }

        private void textBox_IP3_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIPAddr(this.textBox_IP3, e);
        }

        private void textBox_IP4_KeyPress(object sender, KeyPressEventArgs e)
        {
            MaskIPAddr(this.textBox_IP4, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    }
}