using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Web;
using System.IO;

namespace GrabHelper
{
    public partial class MainForm : Form
    {
        delegate void SetTextCallback(string text);
        delegate void ExitAppCallback(object sender, EventArgs e);

        public MainForm()
        {
            InitializeComponent();
            thread = new BackgroudThread(this);
            thread.Start();
        }

        public void SetText(string text)
        {
            if (this.textBox.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox.Text = this.textBox.Text + text + "\r\n";
            }
        }

        public void ExitApp()
        {
            if (this.InvokeRequired) {
                ExitAppCallback e = new ExitAppCallback(cancel_Click);
                this.Invoke(e, new object[] {null, null});
            }
            else
            {
                cancel_Click(null, null);
            }
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            
            thread.Stop();
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private BackgroudThread thread = null;

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
