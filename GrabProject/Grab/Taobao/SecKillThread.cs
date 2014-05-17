using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Common;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Diagnostics;
using Grab;
using Grab.Taobao;

namespace Taobao
{
    public class SecKillThread
    {
        public const int SECTHREAD_IDLE = 1000;
        public const int SECTHREAD_STARTED = 1001;
        public const int SECTHREAD_PREPARING = 1002;
        public const int SECTHREAD_KILLING = 1003;
        public const int SECTHREAD_STOPPED = 1004;

        public static DateTime[] KillTimes = 
        {
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 0, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 13, 30, 0),

            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 16, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 30, 0),

            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 18, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 19, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 30, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 00, 0),
            new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 30, 0)
        };

        private static Dictionary<DateTime, GoodInfo> goodsToKill = new Dictionary<DateTime, GoodInfo>();
        private static Dictionary<DateTime, SecKillThread> killThreads = null;
        public static Dictionary<DateTime, GoodInfo> GetGoodsToKill()
        {
            return goodsToKill;
        }
        public static Dictionary<DateTime, SecKillThread> GetKillThreads()
        {
            if (null == killThreads)
            {
                killThreads = new Dictionary<DateTime, SecKillThread>();
                foreach (DateTime dt in KillTimes)
                {
                    killThreads.Add(dt, new SecKillThread(dt));
                }
                
            }
            
            return killThreads;
        }

        public DateTime startTime { set; get; }
        GrabForm form = null;
        public GoodInfo good { set; get; }
        public int Status { set; get; }
        SynchronizationContext syncCxt = null;
        Thread t = null;

        public SecKillThread(DateTime startTime)
        {
            this.startTime = startTime;
            this.Status = SECTHREAD_IDLE;
        }

        public void SetForm(GrabForm form, SynchronizationContext syncCxt)
        {
            this.form = form;
            this.syncCxt = syncCxt;
        }


        public void SetGood(GoodInfo good)
        {
            if (good.startTime.Equals(startTime))
            {
                this.good = good;
            }
            else
            {
                Debug.Assert(false);
            }
        }

        public void Start()
        {
            this.Status = SECTHREAD_STARTED;
            t = new Thread(new ThreadStart(_ThreadFun));
            t.Start();
        }

        public void Stop()
        {
            if (t != null)
            {
                t.Abort();
            }
        }

        

        public void _ThreadFun()
        {
            GrabMessage msg = new GrabMessage();
            msg.MsgType = GrabMessage.BEGIN_SECKILL;
            syncCxt.Post(form.SetTextSafePost, msg);

            Trace.WriteLine("Enter ThreadFun");
            // wait 5s for browser to navigate.
            Thread.Sleep(5000);
            Trace.WriteLine("Enter ThreadFun-2");
            try
            {
                int interval = 1000; // default interval

                while (true)
                {
                    // if current webbrowser is not in destination url, navigate to there.
//                     if (!form.GetCurrentItemIdFromCurrentWeb().Equals(good.GetGoodId()))
//                     {
//                         msg.MsgType = GrabMessage.NAVIGATE;
//                         msg.args = new object[] { good.uri };
//                         syncCxt.Post(form.SetTextSafePost, msg);
//                     }

                    DateTime tabaoNow = form.prepareThread.deltaTime.GetCurrentTaobaoTime();
                    TimeSpan ts = good.startTime.Subtract(tabaoNow);
                    if (ts.TotalMilliseconds < 2 * 1000)
                    {
                        Trace.WriteLine("Enter Kill");
                        // start real kill action
                        msg.MsgType = GrabMessage.START_JS_TIMER;
                        good.QueryReq(form.GetGlobleCookie(), 5000, form);
                        return;
                    }
                    else
                    {
                        interval = 1000;
                    }

                    Thread.Sleep(interval);
                }
            }
            finally
            {
                syncCxt.Post(form.SetTextSafePost, msg);
            }

            
        }


    }
}