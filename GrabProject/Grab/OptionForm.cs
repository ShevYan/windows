using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Grab.Taobao;

namespace Grab
{
    public partial class OptionForm : Form
    {
        GrabForm parent;
        public OptionForm(GrabForm parent)
        {
            this.parent = parent;
            InitializeComponent();
        }

        private void OptionForm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            new UserOption(usernameText.Text, passwdText.Text).save();
            this.Close();
            parent.isLogin = false;
            parent.isPrepared = false;
            parent.userOpt = UserOption.GetOption();
            parent.GetWebBrowser().Navigate(parent.GetDefaultUrl());
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void passwdBtn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) {
                okBtn_Click(null, null);
            }
        }
    }
}
