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
    public class PrepareThread
    {
        public DeltaTime deltaTime { set; get; }
        public AreaInfo[] areaArray { set; get; }
        public const long TIMEOUT = 15 * 1000; //ms
        GrabForm form = null;
        SynchronizationContext syncCxt = null;
        Thread t = null;

        public PrepareThread(GrabForm form, SynchronizationContext syncCxt)
        {
            this.form = form;
            this.syncCxt = syncCxt;
            deltaTime = new DeltaTime(0, 0);
            areaArray = new AreaInfo[25];
        }

        public void Start()
        {
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

        public void ClearAllAreaInfo()
        {
            for (int i = 0; i <= 24; i++)
            {
                if (null != areaArray[i])
                {
                    while (areaArray[i].goodList.Count > 0)
                    {
                        areaArray[i].goodList.RemoveFirst();
                    }
                    areaArray[i] = null;
                }
            }
        }

        public bool GetAllAreaInfo(long timeout /*ms*/)
        {
            long before = DateTime.Now.Ticks / 10000;
            for (int i = 0; i <= 24; i++)
            {
                if (null == areaArray[i])
                {
                    AreaInfo.AreaQueryReq(areaArray, i, form.GetGlobleCookie());
                }
            }

            for (int i = 0; i <= 24; )
            {
                if (null == areaArray[i])
                {
                    if (DateTime.Now.Ticks / 10000 - before > timeout)
                    {
                        return false;
                    }
                    Thread.Sleep(200);
                }
                else
                {
                    i++;
                }
            }

            return true;
        }

        public GoodInfo GetGoodById(string id)
        {
            for (int i=1; i<areaArray.Length; i++)
            {
                foreach (GoodInfo good in areaArray[i].goodList)
                {
                    if (good.GetGoodId().Equals(id)) {
                        return good;
                    }
                }
            }

            return null;
        }

        public DeltaTime GetDeltaTimeInNS(long timeout /*ms*/)
        {
            int retry = 5;
            long sum = 0;
            long sum2 = 0;
            long before = DateTime.Now.Ticks / 10000;

            while (areaArray[0] == null || areaArray[0].goodList == null || areaArray[0].goodList.Count == 0)
            {
                if (DateTime.Now.Ticks / 10000 - before > timeout)
                {
                    return null;
                }
                Thread.Sleep(200);
            }

            for (int i = 0; i < retry; i++)
            {
                DeltaTime dt = new DeltaTime(
                    areaArray[0].goodList.First.Value.GetQuestionUri().ToString(),
                    form.GetGlobleCookie());
                dt.getDeltaTime();
                while (dt.value == long.MaxValue)
                {
                    if (DateTime.Now.Ticks / 10000 - before > timeout)
                    {
                        return null;
                    }
                    Thread.Sleep(200);
                }
                sum += dt.value;
                sum2 += dt.lag;
            }

            return new DeltaTime(sum / retry, sum2 / retry);
        }


        public void _ThreadFun()
        {
            GrabMessage msg = new GrabMessage();
            msg.MsgType = GrabMessage.BEGIN_PREPARE;
            syncCxt.Post(form.SetTextSafePost, msg);

            form.isPrepared = false;

            try
            {
                // get area list and good list
                ClearAllAreaInfo();
                if (!GetAllAreaInfo(TIMEOUT))
                {
                    msg.MsgType = GrabMessage.PREPARED_FAILED;
                    return;
                }

                DeltaTime tmpDelta = null;
                if ((tmpDelta = GetDeltaTimeInNS(TIMEOUT)) == null)
                {
                    msg.MsgType = GrabMessage.PREPARED_FAILED;
                    return;
                }
                deltaTime = tmpDelta;
                form.isPrepared = true;

                msg.MsgType = GrabMessage.PREPARED_OK;
            }
            catch (System.Exception ex)
            {
                Debug.Assert(false);
            }
            finally
            {
                syncCxt.Post(form.SetTextSafePost, msg);
            }

            
        }


    }
}