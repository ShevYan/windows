namespace Grab
{
    partial class GrabForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.webUrl = new System.Windows.Forms.TextBox();
            this.buttonAction = new System.Windows.Forms.Button();
            this.buttonOption = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tab = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tabControl1.SuspendLayout();
            this.tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(723, 22);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "网址：";
            // 
            // webUrl
            // 
            this.webUrl.Location = new System.Drawing.Point(63, 13);
            this.webUrl.Name = "webUrl";
            this.webUrl.Size = new System.Drawing.Size(381, 20);
            this.webUrl.TabIndex = 2;
            this.webUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.webUrl_Click);
            // 
            // buttonAction
            // 
            this.buttonAction.Location = new System.Drawing.Point(464, 10);
            this.buttonAction.Name = "buttonAction";
            this.buttonAction.Size = new System.Drawing.Size(44, 23);
            this.buttonAction.TabIndex = 3;
            this.buttonAction.Text = "开始";
            this.buttonAction.UseVisualStyleBackColor = true;
            this.buttonAction.Click += new System.EventHandler(this.buttonAction_Click);
            // 
            // buttonOption
            // 
            this.buttonOption.Location = new System.Drawing.Point(514, 10);
            this.buttonOption.Name = "buttonOption";
            this.buttonOption.Size = new System.Drawing.Size(44, 23);
            this.buttonOption.TabIndex = 3;
            this.buttonOption.Text = "设置";
            this.buttonOption.UseVisualStyleBackColor = true;
            this.buttonOption.Click += new System.EventHandler(this.buttonOption_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tab);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 44);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(545, 482);
            this.tabControl1.TabIndex = 4;
            // 
            // tab
            // 
            this.tab.AutoScroll = true;
            this.tab.Controls.Add(this.webBrowser);
            this.tab.Location = new System.Drawing.Point(4, 22);
            this.tab.Name = "tab";
            this.tab.Padding = new System.Windows.Forms.Padding(3);
            this.tab.Size = new System.Drawing.Size(537, 456);
            this.tab.TabIndex = 0;
            this.tab.Text = "Tab1";
            this.tab.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(537, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tab2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(3, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(531, 450);
            this.webBrowser.TabIndex = 0;
            // 
            // GrabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 538);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonOption);
            this.Controls.Add(this.buttonAction);
            this.Controls.Add(this.webUrl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "GrabForm";
            this.Text = "主窗口";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.formResize);
            this.tabControl1.ResumeLayout(false);
            this.tab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox webUrl;
        private System.Windows.Forms.Button buttonAction;
        private System.Windows.Forms.Button buttonOption;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tab;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.WebBrowser webBrowser;
    }
}

