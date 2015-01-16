using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fdModifiers, uint vk);

        delegate void SetTextCallback(string text);

        private MsgManager Lst = null;
        private string filePath = "SET.INI";
        private string desPC = null;
        private bool enableAlreadyRead = false;
        private int count = 0;

        private bool onMsg = false;
        protected override void WndProc(ref Message m)
        {
            if (this.Handle == m.HWnd && (
                m.Msg != 0x007f             // WM_GETICON
                ))
            {
                timerHide.Stop();
                timerHide.Start();
                //lbTest.Text = count.ToString();
            }

            if (m.Msg == 0x0312 && (m.WParam.ToInt32() == 100 || m.WParam.ToInt32() == 101))
            {
                ShowWindow();
            }
            base.WndProc(ref m);
        }

        public Form1()
        {
            InitializeComponent();
		
            filePath = Application.StartupPath + "\\" + filePath;
            IniFile ini = new IniFile(filePath);
            desPC = ini.Read("DES", "HOST");
            string name = ini.Read("DES", "NAME");
            this.Text = "備忘事項:" + name;
            int Port = ini.ReadInt("INFO", "PORT");
            enableAlreadyRead = ini.ReadBool("INFO", "ALREADY_READ");

            Lst = new MsgManager();
            Lst.SetPort(Port);
            Lst.Listen(this);

            RegisterHotKey(this.Handle, 100, 1, 81); // Ctrl + Q
            RegisterHotKey(this.Handle, 101, 2, 187); // Ctrl + +
        }

        //private void OnHotKey(

        public void Append(string msg)
        {
            if (outputBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(Append);
                this.Invoke(d, new object[] { msg });
            }
            else
            {
                StateControl(true);

                outputBox.AppendText("(" + DateTime.Now.ToShortTimeString() +") " + msg + "\n");
                outputBox.ScrollToCaret();
            }
        }

        private void tBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Control && !e.Alt && !e.Shift)
            {
                string msg = inputBox.Text;
                msg = msg.Trim();

                if (msg.Length == 0)
                    return;

                inputBox.Clear();

                Lst.Send(desPC, msg);

                outputBox.AppendText("(" + DateTime.Now.ToShortTimeString() + ") 我: " + msg + "\n");
                outputBox.ScrollToCaret();

                StateControl(false);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Lst != null)
                Lst.StopListen();

            UnregisterHotKey(this.Handle, 100);
            UnregisterHotKey(this.Handle, 101);
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            StateControl(false);
            inputBox.Focus();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowWindow();
            }
        }

        private void ShowWindow()
        {

            if (!this.Visible)
            {
                this.Hide();
                this.Show();
                this.Activate();
                timerHide.Start();
            }
            else
                this.Hide();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void StateControl(bool isOn)
        {
            if (isOn == true && onMsg == false)
            {
                onMsg = true;
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                icon.Icon = ((System.Drawing.Icon)(resources.GetObject("on")));
            }
            else if(isOn == false && onMsg == true)
            {
                onMsg = false;
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
                icon.Icon = ((System.Drawing.Icon)(resources.GetObject("off")));
                //Append("(已讀)");
            }
		}

        private void timerHide_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }

            timerHide.Stop();
        }
    }

    //class ChatPanel : Panel
    //{
    //    //public TextBox InputBox;
    //    //public TextBox OutputBox;
    //    public ChatPanel()
    //    {
    //        this.Font = new System.Drawing.Font("細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    //        this.Location = new System.Drawing.Point(1, 3);
    //        //this.Multiline = true;
    //        this.Name = "outputBox";
    //        //this.ReadOnly = true;
    //        //this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
    //        this.Size = new System.Drawing.Size(290, 65);
    //    }
    //}
}
