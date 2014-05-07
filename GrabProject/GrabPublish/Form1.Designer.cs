namespace GrabPublish
{
    partial class Form1
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
            this.publish = new System.Windows.Forms.Button();
            this.unpublish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // publish
            // 
            this.publish.Location = new System.Drawing.Point(13, 13);
            this.publish.Name = "publish";
            this.publish.Size = new System.Drawing.Size(75, 23);
            this.publish.TabIndex = 0;
            this.publish.Text = "publish";
            this.publish.UseVisualStyleBackColor = true;
            this.publish.Click += new System.EventHandler(this.publish_Click);
            // 
            // unpublish
            // 
            this.unpublish.Location = new System.Drawing.Point(13, 54);
            this.unpublish.Name = "unpublish";
            this.unpublish.Size = new System.Drawing.Size(75, 23);
            this.unpublish.TabIndex = 1;
            this.unpublish.Text = "unpublish";
            this.unpublish.UseVisualStyleBackColor = true;
            this.unpublish.Click += new System.EventHandler(this.unpublish_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.unpublish);
            this.Controls.Add(this.publish);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button publish;
        private System.Windows.Forms.Button unpublish;
    }
}

