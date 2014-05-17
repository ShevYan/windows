using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Grab
{
    public partial class PictureForm : Form
    {
        public string myText {
            get
            {
                return richTextBox.Text;
            }
        }

        public PictureForm(string url)
        {
            InitializeComponent();
            this.pictureBox.LoadAsync(url);
            richTextBox.Focus();
        }

        private void PictureForm_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter)) {
                this.Close();
            }
        }

        private void picLoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            using (pictureBox.Image)
            {
                var bmp2 = new Bitmap(pictureBox.Width, pictureBox.Height);
                using (var g = Graphics.FromImage(bmp2))
                {
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    g.DrawImage(pictureBox.Image, new Rectangle(Point.Empty, bmp2.Size));
                    pictureBox.Image = bmp2;
                }
            }
        }
    }
}
