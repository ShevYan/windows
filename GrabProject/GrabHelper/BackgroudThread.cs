using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace GrabHelper
{
    class BackgroudThread
    {
        public BackgroudThread(MainForm f)
        {
            form = f;
        }

        public void Start()
        {
            t = new Thread(new ThreadStart(_ThreadFun));
            t.Start();
        }

        public void Stop()
        {
            if (t != null) {
                t.Abort();
            }
        }

        public void _ThreadFun()
        {
            form.SetText("检查更新...");

            try
            {
	            GrabConfig remoteConf = Common.GrabConfig.getRemoteConfig();
	            GrabConfig localConf = Common.GrabConfig.getLocalConfig();
	            
	            if (localConf == null || localConf.confs["Grab.version"].CompareTo(remoteConf.confs["Grab.version"]) < 0)
	            {
	                NetworkHelper.GetInstance().Download(remoteConf.confs["Grab.path"], "Grab.dat");
	                Utils.DearchiveFiles("Grab.dat", "../Grab/");
	            }
	
	            form.SetText("检查错误日志...");
	            LinkedList<string> exFiles = ExceptionHanlder.getInstance().GetExceptionFiles();
	            foreach (string file in exFiles)
	            {
	                byte[] data = System.IO.File.ReadAllBytes(file);
	                NetworkHelper.GetInstance().HttpRequest("/upload_exception", "POST", data);
	                System.IO.File.Delete(file);
	            }
	
	            form.SetText("启动程序...");
                Trace.WriteLine(Utils.GetWorkingDir());
                System.Diagnostics.Process.Start(Path.GetFullPath("../Grab/Grab.exe"));
                remoteConf.writeToLocal();
                
            }
            catch (System.Exception ex)
            {
                form.SetText("启动程序失败");
                foreach (string s in ex.ToString().Split('\n'))
                {
                    form.SetText(s);
                }

                Trace.WriteLine(ex.ToString());
                return;
            }

            Thread.Sleep(1000);
            form.ExitApp();
        }

        private MainForm form;
        Thread t = null;
    }
}