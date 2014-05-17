using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace Grab.Taobao
{
    public class AreaInfo
    {
        Dictionary<int, DateTime> startTimes;
        public const string prefix = "http://tejia.taobao.com/one.htm?area=";
        public AreaInfo(Uri uri)
        {
            this.uri = uri;
            
        }
        public Uri uri { set;  get; }

        public int GetAreaId() {
            return int.Parse(uri.ToString().Replace(prefix, ""));
        }

        public DateTime startTime { set;  get; } // local time
        public LinkedList<GoodInfo> goodList = new LinkedList<GoodInfo>();

        private static int AreaInfoCallBack(AsyncHttpWebRequest thisReq, HttpWebResponse response)
        {
            AreaInfo[] areaArr = (thisReq.userObj[0]) as AreaInfo[];
            int id = (int)thisReq.userObj[1];

            string responseFromServer = AsyncHttpWebRequest.ConvertResponseToString(
                response, Encoding.GetEncoding("GBK"));
            
            if (null == responseFromServer || responseFromServer.Length == 0) {
                thisReq.QueueRequest();
            }

            areaArr[id] = AreaInfo.ParseFromHtml(thisReq.httpReq.RequestUri, responseFromServer);
            if (null == areaArr[id])
            {
                // re-queue the request if failed.
                thisReq.QueueRequest();
            }

            return 0;
        }

        public static void AreaQueryReq(AreaInfo[] arr, int id, string cookie)
        {
            object[] userObj = new object[2] { arr, id};
            HttpWebRequest orgReq = AsyncHttpWebRequest.ConstructHttpRequest(AreaInfo.prefix + id,
                "GET", cookie, null, 10000);
            AsyncHttpWebRequest req = new AsyncHttpWebRequest(orgReq, userObj,
                new AsyncHttpWebRequest.CallBack(AreaInfoCallBack));
            req.QueueRequest();
        }

        
        public static AreaInfo ParseFromHtml(Uri uri, string html)
        {
            AreaInfo area = new AreaInfo(uri);

            // parse start time
            int hour = 0;
            int minute = 0;
            int areaId = area.GetAreaId();
            if (areaId % 2 != 0)
            {
                hour = 10 + (areaId - 1) / 2;
                minute = 0;
            }
            else
            {
                hour = 10 + (areaId - 1) / 2;
                minute = 30;
            }
            area.startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                hour, minute, 0);
            
            // parse good list
//             <li class="par-item ">
// 			<dl class="to-item" data-id="684955">
// 				<dt>
// 								<a href="http://tejia.taobao.com/oneItem?spm=a3109.6190710.1997043513.4.OI6OIH&amp;id=38538539810" target="_blank" data-spm-anchor-id="a3109.6190710.1997043513.4"><img width="210" height="210" alt="" src="http://img01.taobaocdn.com/imgextra/i1/14290038968050023/T1piuWFMJXXXXXXXXX_!!1639544290-0-tejia.jpg_210x210.jpg"></a>
// 								</dt>
// 				<dd class="title">
//                             <a href="http://tejia.taobao.com/oneItem?spm=a3109.6190710.1997043513.5.OI6OIH&amp;id=38538539810" target="_blank" data-spm-anchor-id="a3109.6190710.1997043513.5">樱花油烟机</a>
//                         </dd>
// 				<dd><strong>1.00</strong>包邮<span class="sold-out">抢光了</span><span class="has-end">已结束</span></dd>
// 				<dd><del>596.00</del>|<span>限量<b>1</b>件</span>
// 								<a href="http://tejia.taobao.com/oneItem?spm=a3109.6190710.1997043513.6.OI6OIH&amp;id=38538539810" class="now-to-buy" target="_blank" data-spm-anchor-id="a3109.6190710.1997043513.6">立即去抢</a>
// 								</dd>
// 			</dl>
// 		</li>
            int start = 0;
            string strFlagPrefix = "<li class=\"par-item \">";
            string strFlagSufix = "</li>";
            while (true) {

                start = html.IndexOf(strFlagPrefix, start);
                if (-1 == start) {
                    break;
                }
                int end = html.IndexOf(strFlagSufix, start + strFlagPrefix.Length);

                string li = html.Substring(start, end + strFlagSufix.Length + 1 - start);
                GoodInfo goodInfo = GoodInfo.ParseFromLi(li, area.startTime, area);

                area.goodList.AddLast(goodInfo);
                start = end + strFlagSufix.Length;
            }

            return area;
        }
    }
}
