using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace GrabHelper
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

//             try
//             {
//             	Application.Run(new MainForm());
//             }
//             catch (System.Exception ex)
//             {
//                 Common.ExceptionHanlder.getInstance().dump(ex);
//             }
        }
    }
}
