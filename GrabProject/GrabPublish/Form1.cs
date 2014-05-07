using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Common;

namespace GrabPublish
{
    public partial class Form1 : Form
    {
        private static string publishHome = "../../publish";
        private static string unpublishHome = "../../unpublish";

        private static string grabHelperVersion = "1.0";
        private static string grabHelperHome = "../GrabHelper";
        private static string grabHelperArchivePath = publishHome + "/GrabHelper/" + grabHelperVersion + "/GrabHelper.dat";

        private static string grabVersion = "1.0";
        private static string grabHome = "../Grab";
        private static string grabArchivePath = publishHome + "/Grab/" + grabVersion + "/Grab.dat";

        public Form1()
        {
            InitializeComponent();
        }

        private void publish_Click(object sender, EventArgs e)
        {
            DirectoryInfo folder = new DirectoryInfo(grabHelperHome);
            LinkedList<string> files = new LinkedList<string>();
            foreach (FileInfo file in folder.GetFiles())
            {
                files.AddLast(file.FullName);
            }

            Utils.ArchiveFiles(files, new DirectoryInfo(grabHelperHome).FullName, grabHelperArchivePath);

            folder = new DirectoryInfo(grabHome);
            files = new LinkedList<string>();
            foreach (FileInfo file in folder.GetFiles())
            {
                files.AddLast(file.FullName);
            }
            Utils.ArchiveFiles(files, new DirectoryInfo(grabHome).FullName, grabArchivePath);
        }

        private void unpublish_Click(object sender, EventArgs e)
        {
            Utils.DearchiveFiles(grabHelperArchivePath, unpublishHome+"/GrabHelper");
            Utils.DearchiveFiles(grabArchivePath, unpublishHome+"/Grab");
        }
    }
}
