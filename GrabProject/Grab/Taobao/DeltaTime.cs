using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Net;
using System.Threading;

namespace Grab.Taobao
{
    public class DeltaTime
    {
        public long value { set; get; }
        public long lag { set; get; }
        string uri;
        string cookie;

        public DeltaTime(long v, long l)
        {
            this.value = v;
            this.lag = l;
        }

        public DeltaTime(string uri, string cookie)
        {
            this.value = long.MaxValue;
            this.uri = uri;
            this.cookie = cookie;
        }

        public DateTime GetCurrentTaobaoTime()
        {
            return new DateTime(DateTime.Now.Ticks - (value == long.MaxValue ? value : 0) - lag);
        }

        public DateTime GetNextKillTime(DateTime now)
        {
            DateTime nextSeckTime;
            if (now.Hour >= 22) {
                nextSeckTime = new DateTime(now.Year, now.Month, now.Day + 1,
                    10, 0, 0);
            } else if (now.Hour < 10) {
                nextSeckTime = new DateTime(now.Year, now.Month, now.Day,
                    10, 0, 0);
            }
            else
            {
                if (now.Minute < 30)
                {
                    nextSeckTime = new DateTime(now.Year, now.Month, now.Day,
                        now.Hour, 30, 0);
                }
                else
                {
                    nextSeckTime = new DateTime(now.Year, now.Month, now.Day,
                        now.Hour + 1, 0, 0);
                }
            }

            return nextSeckTime;
        }

        public void getDeltaTime()
        {
            HttpWebRequest webReq = AsyncHttpWebRequest.ConstructHttpRequest(uri,
                "GET", cookie, null, 5000);
            AsyncHttpWebRequest asyncReq = new AsyncHttpWebRequest(webReq, new object[] {this, DateTime.UtcNow.Ticks},
                new AsyncHttpWebRequest.CallBack(DeltaTimeCallBack));
            asyncReq.QueueRequest();
        }

        int DeltaTimeCallBack(AsyncHttpWebRequest asyncReq, HttpWebResponse resp)
        {
            DeltaTime deltaTime = asyncReq.userObj[0] as DeltaTime;
            long startTime = (long)asyncReq.userObj[1];
            long timeUsed = DateTime.UtcNow.Ticks - startTime;

            string strResp = AsyncHttpWebRequest.ConvertResponseToString(resp, Encoding.GetEncoding("GBK"));
            if (strResp == null || !strResp.Contains("now")) {
                return -1;
            }

            long tServer = long.Parse(Utils.GetMiddleString(strResp, "\"now\":", "}")) * 10000 + Utils.UTCZero.Ticks;
            value = startTime + timeUsed / 2 - tServer;

            lag = timeUsed / 2;
            return 0;
        }
    }
}
