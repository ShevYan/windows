using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Grab
{
    public partial class GrabForm : Form
    {
        
        public GrabForm()
        {
            InitializeComponent();
            syncCxt = SynchronizationContext.Current;
            thread = new BackgroudThread(this, syncCxt);
            thread.Start();
        }

        public void SetTextSafePost(object obj)
        {
            GrabMessage msg = obj as GrabMessage;
            switch (msg.MsgType) {
                case GrabMessage.EXIT_APP:
                    thread.Stop();
                    Application.Exit();
                    break;
                default:
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private BackgroudThread thread;
        private SynchronizationContext syncCxt = null;

        private void buttonOption_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(this);
            optionForm.ShowDialog();
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(webUrl.Text);
        }

        private void formResize(object sender, EventArgs e)
        {
            Size sz = this.Size;
            sz.Height -= 100;
            sz.Width -= 100;
            tabControl1.Size = sz;
        }

        private void webUrl_Click(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) {
                buttonAction_Click(null, null);
            }
        }
    }
}
