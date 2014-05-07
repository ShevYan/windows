using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;

namespace Grab
{
    class BackgroudThread
    {
        public BackgroudThread(GrabForm form, SynchronizationContext syncCxt)
        {
            this.form = form;
            this.syncCxt = syncCxt;
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
            Glogger.log.Info("检查更新...");

            try
            {
	            GrabConfig remoteConf = Common.GrabConfig.getRemoteConfig();
	            GrabConfig localConf = Common.GrabConfig.getLocalConfig();
	            
                if (remoteConf == null) 
                {
                    Glogger.log.Error("连接服务器失败, 退出程序.");
                    ExitApp();
                    return;
                }

	            if (localConf == null || localConf.confs["GrabHelper.version"].CompareTo(remoteConf.confs["GrabHelper.version"]) < 0)
	            {
	                NetworkHelper.GetInstance().Download(remoteConf.confs["GrabHelper.path"], "GrabHelper.dat");
	                Utils.DearchiveFiles("GrabHelper.dat", "../GrabHelper/");
	            }

                Glogger.log.Info("检查错误日志...");
	            LinkedList<string> exFiles = ExceptionHanlder.getInstance().GetExceptionFiles();
	            foreach (string file in exFiles)
	            {
	                byte[] data = System.IO.File.ReadAllBytes(file);
	                NetworkHelper.GetInstance().HttpRequest("/upload_exception", "POST", data);
	                System.IO.File.Delete(file);
	            }

                Glogger.log.Info("获得注册信息...");
                string resp = NetworkHelper.GetInstance().HttpRequest("/auth/" + Hardware.GetMacAddress() + "/" + Parameters.PRODCUT_NAME, "GET", null);
                if (resp.Equals("ERR")) {
                    Glogger.log.Info("验证失败，退出程序。");
                    ExitApp();
                }
                remoteConf.writeToLocal();
                
            }
            catch (System.Exception ex)
            {
                Glogger.log.Error("启动程序失败\n" + ex.ToString());
                foreach (string s in ex.ToString().Split('\n'))
                {
                    Glogger.log.Error(s);
                }

                Trace.WriteLine(ex.ToString());
                Thread.Sleep(1000);
                ExitApp();
            }

            
        }

        private void PostMessage(object obj) {
            syncCxt.Post(form.SetTextSafePost, obj);
        }

        private void ExitApp()
        {
            GrabMessage msg = new GrabMessage();
            msg.MsgType = GrabMessage.EXIT_APP;
            PostMessage(msg);
        }

        private void SetStatus(string text)
        {

        }

        GrabForm form = null;
        SynchronizationContext syncCxt = null;
        Thread t = null;
    }
}