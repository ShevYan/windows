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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GrabForm));
            this.buttonOption = new System.Windows.Forms.Button();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.statusLabel = new System.Windows.Forms.Label();
            this.seckillBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.taobaoTimeLabel = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.answerText = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.testBtn = new System.Windows.Forms.Button();
            this.nextpageBtn = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.prepageBtn = new System.Windows.Forms.Button();
            this.pageLabel = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orgPriceHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.seckillPricHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.webUrl = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.Button();
            this.prepareBtn = new System.Windows.Forms.Button();
            this.countTimer = new System.Windows.Forms.Timer(this.components);
            this.navigateFreshTimer = new System.Windows.Forms.Timer(this.components);
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOption
            // 
            this.buttonOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonOption.Location = new System.Drawing.Point(672, 19);
            this.buttonOption.Name = "buttonOption";
            this.buttonOption.Size = new System.Drawing.Size(70, 30);
            this.buttonOption.TabIndex = 3;
            this.buttonOption.Text = "设置";
            this.buttonOption.UseVisualStyleBackColor = true;
            this.buttonOption.Click += new System.EventHandler(this.buttonOption_Click);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(6, 55);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScriptErrorsSuppressed = true;
            this.webBrowser.Size = new System.Drawing.Size(759, 713);
            this.webBrowser.TabIndex = 6;
            this.webBrowser.Url = new System.Uri("http://tejia.taobao.com/one.htm?area=0", System.UriKind.Absolute);
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.docCompleted);
            this.webBrowser.NewWindow += new System.ComponentModel.CancelEventHandler(this.webBrowser_NewWindow);
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.statusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.statusLabel.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.statusLabel.Location = new System.Drawing.Point(1, 781);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(1103, 24);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "状态：";
            // 
            // seckillBtn
            // 
            this.seckillBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.seckillBtn.Location = new System.Drawing.Point(576, 19);
            this.seckillBtn.Name = "seckillBtn";
            this.seckillBtn.Size = new System.Drawing.Size(78, 30);
            this.seckillBtn.TabIndex = 9;
            this.seckillBtn.Text = "秒杀";
            this.seckillBtn.UseVisualStyleBackColor = true;
            this.seckillBtn.Click += new System.EventHandler(this.seckillBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.taobaoTimeLabel);
            this.groupBox1.Location = new System.Drawing.Point(4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(154, 61);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "淘宝时间";
            // 
            // taobaoTimeLabel
            // 
            this.taobaoTimeLabel.BackColor = System.Drawing.Color.Teal;
            this.taobaoTimeLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.taobaoTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.taobaoTimeLabel.ForeColor = System.Drawing.Color.White;
            this.taobaoTimeLabel.Location = new System.Drawing.Point(13, 20);
            this.taobaoTimeLabel.Name = "taobaoTimeLabel";
            this.taobaoTimeLabel.Size = new System.Drawing.Size(130, 33);
            this.taobaoTimeLabel.TabIndex = 23;
            this.taobaoTimeLabel.Text = "10:00:00";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.countLabel);
            this.groupBox2.Location = new System.Drawing.Point(164, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 61);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "倒计时";
            // 
            // countLabel
            // 
            this.countLabel.BackColor = System.Drawing.Color.Teal;
            this.countLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.countLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.countLabel.ForeColor = System.Drawing.Color.White;
            this.countLabel.Location = new System.Drawing.Point(8, 19);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(151, 33);
            this.countLabel.TabIndex = 23;
            this.countLabel.Text = "00.00";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.InitialImage = null;
            this.pictureBox.Location = new System.Drawing.Point(2, 24);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(320, 70);
            this.pictureBox.TabIndex = 17;
            this.pictureBox.TabStop = false;
            this.pictureBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.pictureBox_LoadCOmpleted);
            // 
            // answerText
            // 
            this.answerText.AcceptsReturn = true;
            this.answerText.BackColor = System.Drawing.Color.Teal;
            this.answerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.answerText.ForeColor = System.Drawing.Color.Yellow;
            this.answerText.Location = new System.Drawing.Point(2, 103);
            this.answerText.Name = "answerText";
            this.answerText.Size = new System.Drawing.Size(320, 41);
            this.answerText.TabIndex = 18;
            this.answerText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.answerText_KeyDown);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.groupBox3.Controls.Add(this.pictureBox);
            this.groupBox3.Controls.Add(this.answerText);
            this.groupBox3.Location = new System.Drawing.Point(4, 70);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(325, 151);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图片问答区";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBox4.Controls.Add(this.testBtn);
            this.groupBox4.Controls.Add(this.nextpageBtn);
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.prepageBtn);
            this.groupBox4.Controls.Add(this.pageLabel);
            this.groupBox4.Controls.Add(this.listView);
            this.groupBox4.Location = new System.Drawing.Point(4, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(325, 549);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "商品列表";
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(55, 12);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(75, 23);
            this.testBtn.TabIndex = 4;
            this.testBtn.Text = "testBtn";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.testBtn_Click);
            // 
            // nextpageBtn
            // 
            this.nextpageBtn.Location = new System.Drawing.Point(267, 11);
            this.nextpageBtn.Name = "nextpageBtn";
            this.nextpageBtn.Size = new System.Drawing.Size(27, 23);
            this.nextpageBtn.TabIndex = 3;
            this.nextpageBtn.Text = ">";
            this.nextpageBtn.UseVisualStyleBackColor = true;
            this.nextpageBtn.Click += new System.EventHandler(this.nextpageBtn_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(136, 11);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "*";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // prepageBtn
            // 
            this.prepageBtn.Location = new System.Drawing.Point(180, 11);
            this.prepageBtn.Name = "prepageBtn";
            this.prepageBtn.Size = new System.Drawing.Size(27, 23);
            this.prepageBtn.TabIndex = 3;
            this.prepageBtn.Text = "<";
            this.prepageBtn.UseVisualStyleBackColor = true;
            this.prepageBtn.Click += new System.EventHandler(this.prepageBtn_Click);
            // 
            // pageLabel
            // 
            this.pageLabel.AutoSize = true;
            this.pageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageLabel.Location = new System.Drawing.Point(216, 15);
            this.pageLabel.Name = "pageLabel";
            this.pageLabel.Size = new System.Drawing.Size(44, 15);
            this.pageLabel.TabIndex = 2;
            this.pageLabel.Text = "第 1 区";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.orgPriceHeader,
            this.seckillPricHeader,
            this.startTimeHeader});
            this.listView.GridLines = true;
            this.listView.Location = new System.Drawing.Point(0, 40);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(322, 503);
            this.listView.TabIndex = 0;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listview_MouseDoubleClick);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "名称";
            this.nameHeader.Width = 120;
            // 
            // orgPriceHeader
            // 
            this.orgPriceHeader.Text = "原价";
            // 
            // seckillPricHeader
            // 
            this.seckillPricHeader.Text = "秒杀价";
            // 
            // startTimeHeader
            // 
            this.startTimeHeader.Text = "开始时间";
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.SystemColors.Menu;
            this.groupBox6.Controls.Add(this.webUrl);
            this.groupBox6.Controls.Add(this.webBrowser);
            this.groupBox6.Controls.Add(this.goBtn);
            this.groupBox6.Controls.Add(this.buttonOption);
            this.groupBox6.Controls.Add(this.prepareBtn);
            this.groupBox6.Controls.Add(this.seckillBtn);
            this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox6.Location = new System.Drawing.Point(339, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(774, 774);
            this.groupBox6.TabIndex = 22;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "网页";
            // 
            // webUrl
            // 
            this.webUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.webUrl.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.webUrl.Location = new System.Drawing.Point(6, 24);
            this.webUrl.Name = "webUrl";
            this.webUrl.Size = new System.Drawing.Size(383, 20);
            this.webUrl.TabIndex = 2;
            this.webUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.webUrl_Click);
            // 
            // goBtn
            // 
            this.goBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.goBtn.Location = new System.Drawing.Point(403, 19);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(62, 30);
            this.goBtn.TabIndex = 3;
            this.goBtn.Text = "GO";
            this.goBtn.UseVisualStyleBackColor = true;
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // prepareBtn
            // 
            this.prepareBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.prepareBtn.Location = new System.Drawing.Point(483, 19);
            this.prepareBtn.Name = "prepareBtn";
            this.prepareBtn.Size = new System.Drawing.Size(78, 30);
            this.prepareBtn.TabIndex = 9;
            this.prepareBtn.Text = "准备";
            this.prepareBtn.UseVisualStyleBackColor = true;
            this.prepareBtn.Click += new System.EventHandler(this.perpareBtn_Click);
            // 
            // countTimer
            // 
            this.countTimer.Enabled = true;
            this.countTimer.Interval = 1000;
            this.countTimer.Tick += new System.EventHandler(this.countTimer_Tick);
            // 
            // navigateFreshTimer
            // 
            this.navigateFreshTimer.Enabled = true;
            this.navigateFreshTimer.Interval = 300000;
            this.navigateFreshTimer.Tick += new System.EventHandler(this.navigateFreshTimer_Tick);
            // 
            // statusTimer
            // 
            this.statusTimer.Enabled = true;
            this.statusTimer.Interval = 1000;
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // GrabForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1105, 805);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusLabel);
            this.Name = "GrabForm";
            this.Text = "主窗口";
            this.Load += new System.EventHandler(this.GrabForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOption;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button seckillBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.TextBox answerText;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button nextpageBtn;
        private System.Windows.Forms.Button prepageBtn;
        private System.Windows.Forms.Label pageLabel;
        private System.Windows.Forms.Label taobaoTimeLabel;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.TextBox webUrl;
        private System.Windows.Forms.Button goBtn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader orgPriceHeader;
        private System.Windows.Forms.ColumnHeader seckillPricHeader;
        private System.Windows.Forms.ColumnHeader startTimeHeader;
        private System.Windows.Forms.Button prepareBtn;
        private System.Windows.Forms.Timer countTimer;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Timer navigateFreshTimer;
        private System.Windows.Forms.Timer statusTimer;
    }
}

