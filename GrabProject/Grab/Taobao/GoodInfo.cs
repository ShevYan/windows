using System;
using System.Collections.Generic;
using System.Text;
using Common;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

namespace Grab.Taobao
{
    public class GoodInfo
    {
//         stock: 'http://m.ajax.taobao.com/stock2.htm?id=38352044271',
//                             question: 'http://m.ajax.taobao.com/qst.htm?id=38352044271',
// 							QrcodeImgUrl:'http://gqrcode.alicdn.com/img?type=ms&scene_id=38352044271&v=
        string goodPrefix = "http://tejia.taobao.com/oneItem?id=";
        string stockUri = "http://m.ajax.taobao.com/stock2.htm?id=";

        //                    http://m.ajax.taobao.com/qst.htm?_ksTS=1400039993557_588&cb=jsonp589&id=38421434241
        string questionUri = "http://m.ajax.taobao.com/qst.htm?";
        string qrcodeImgUri = "http://gqrcode.alicdn.com/img?type=ms&scene_id=$shev&v=1";


        public Uri uri { set; get; }
        Uri smallPicture;
        public string name {set; get; }
        public string orgPrice { set; get; }
        public string seckillPrice { set; get; }
        public Uri questionImgUrl { set; get; }
        public DateTime startTime { set; get; } // local time
        public AreaInfo area { set; get; }
        int cbNum = 565;

        private static void DownloadRemoteImageFile(string uri, string fileName)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // Check that the remote file was found. The ContentType
            // check is performed since a request for a non-existent
            // image file might be redirected to a 404-page, which would
            // yield the StatusCode "OK", even though the image was not
            // found.
            if ((response.StatusCode == HttpStatusCode.OK ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {

                // if the remote file was found, download oit
                using (Stream inputStream = response.GetResponseStream())
                using (Stream outputStream = File.OpenWrite(fileName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead;
                    do
                    {
                        bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                        outputStream.Write(buffer, 0, bytesRead);
                    } while (bytesRead != 0);
                }
            }
        }


        private static int GoodInfoCallBack(AsyncHttpWebRequest thisReq, HttpWebResponse response)
        {
            GoodInfo good = (thisReq.userObj[0]) as GoodInfo;
            long timeAvailable = (long)thisReq.userObj[1];

            long start = (long)thisReq.userObj[2];
            GrabForm form = thisReq.userObj[3] as GrabForm;

            Trace.WriteLine("GoodInfoCallBack " + DateTime.Now.ToString() + " " + thisReq.httpReq.RequestUri.ToString());
            string responseFromServer = AsyncHttpWebRequest.ConvertResponseToString(
                response, Encoding.GetEncoding("GBK"));
            
//             // test
//             form.ImgUrlGot("http://img1.tbcdn.cn/tfscom/TB1L72CFFXXXXa_XpXXq6xXFXXX");
//             return 0;
            // parse
            //"qst":"http://img1.tbcdn.cn/tfscom/TB1L72CFFXXXXa_XpXXq6xXFXXX"
            string urlText = "";
            if (responseFromServer != null && responseFromServer.Contains("qst"))
            {
                urlText = Utils.GetMiddleString(responseFromServer, "\"qst\":\"", "\"");
            }

            if (urlText != null && urlText.Length > 0)
            {
                good.questionImgUrl = new Uri(urlText);
                Trace.WriteLine("get questiong url: " + urlText);
                DownloadRemoteImageFile(urlText, "./curImg.jpg");
                form.ImgUrlGot("./curImg.jpg");
//                 string fileName = "c:/img/" + urlText.Replace("http://img1.tbcdn.cn/tfscom/", "") + ".txt";
//                 if (System.IO.File.Exists(fileName)) {
//                     fileName = "c:/img/dup_" + urlText.Replace("http://img1.tbcdn.cn/tfscom/", "") + ".txt";
//                 }
//                 System.IO.File.WriteAllText(fileName, good.GetGoodId() + "-" + urlText);
            }
            else
            {
                // don't get url, and time available, re-queue the request.
                if (DateTime.Now.Ticks - start <= timeAvailable * 10000)
                {
                    //Thread.Sleep(50);

                    good.QueryReq(thisReq.httpReq.Headers.Get("Cookie"), timeAvailable - (DateTime.Now.Ticks - start) / 10000, form);
                }
            }

            return 0;
        }

        public void QueryReq( string cookie, long timeAvailable /*ms*/, GrabForm form)
        {
            long startTime = DateTime.Now.Ticks;
            object[] userObj = new object[4] { this, timeAvailable, startTime, form };
            HttpWebRequest orgReq = AsyncHttpWebRequest.ConstructHttpRequest(GetQuestionUri().ToString(),
                "GET", cookie, null, 10000);
            AsyncHttpWebRequest req = new AsyncHttpWebRequest(orgReq, userObj,
                new AsyncHttpWebRequest.CallBack(GoodInfoCallBack));
            req.QueueRequest();
        }

        public override string ToString()
        {
            return "uri:" + uri.ToString() +", smallPicture:" + smallPicture.ToString() + " ,name:" + name +
                " ,orgPrice:" + orgPrice + " ,seckillPrice:"+ seckillPrice +
                " ,questionImgUrl:" + questionImgUrl.ToString();
        }

        public GoodInfo(Uri u, Uri sm, string n, string o, string s, DateTime startTime, AreaInfo area)
        {
            this.uri = u;
            this.smallPicture = sm;
            this.name = n;
            this.orgPrice = o;
            this.seckillPrice = s;
            this.startTime = startTime;
            this.area = area;
        }

        public static GoodInfo ParseFromLi(string li, DateTime startTime, AreaInfo area)
        {
//             <li class="par-item ">
// 			<dl class="to-item has-end-buy" data-id="685732">
// 				<dt>
// 								<a href="http://tejia.taobao.com/oneItem?id=38352044271" target="_blank"><img width="210" height="210" alt="" src="http://img02.taobaocdn.com/imgextra/i2/13831039185912996/T117_aFNdXXXXXXXXX_!!1055493831-0-tejia.jpg_210x210.jpg"></a>
// 								</dt>
// 				<dd class="title">
//                             <a href="http://tejia.taobao.com/oneItem?id=38352044271" target="_blank">阿玛施女装新款条纹短袖T恤</a>
//                         </dd>
// 				<dd><strong>1.00</strong>包邮<span class="sold-out">抢光了</span><span class="has-end">已结束</span></dd>
// 				<dd><del>128.00</del>|<span>限量<b>50</b>件</span>
// 								<a href="http://tejia.taobao.com/oneItem?id=38352044271" class="now-to-buy" target="_blank">立即去抢</a>
// 								</dd>
// 			</dl>
// 		</li>

            Uri uri = new Uri(Utils.GetMiddleString(li, "<a href=\"", "\" target="));
            Uri smallPicture = null;
            {
                int end = li.IndexOf(".jpg");
                int start = li.LastIndexOf("http://", end);
                smallPicture = new Uri(li.Substring(start, end - start) + ".jpg");
            }
            
            int pos = li.IndexOf("<dd class=\"title\">");
            string tmp = li.Substring(pos);
            string name = Utils.GetMiddleString(tmp, "target=\"_blank\">", "</a>");
            string orgPrice = Utils.GetMiddleString(li, "<dd><del>", "</del>");
            string seckillPrice = Utils.GetMiddleString(li, "<dd><strong>", "</strong>");
            return new GoodInfo(uri, smallPicture, name, orgPrice, seckillPrice, startTime, area);
        }

        public string GetGoodId()
        {
            return uri.ToString().Replace(goodPrefix, "");
        }

        public Uri GetStockUri()
        {
            string id = GetGoodId();
            return new Uri(goodPrefix + id);
        }

        //http://m.ajax.taobao.com/qst.htm?_ksTS=1400039993557_588&cb=jsonp589&id=38421434241
        public Uri GetQuestionUri()
        {
            long ksTS = (DateTime.UtcNow.Ticks - Utils.UTCZero.Ticks) / 10000;
            string id = GetGoodId();
            return new Uri(questionUri + "_ksTS=" + ksTS + "_" + cbNum + "&cb=jsonp" + (++cbNum) + "&id=" + id);
        }

        public Uri GetQrcodeImgUri()
        {
            string id = GetGoodId();
            return new Uri(qrcodeImgUri.Replace("$shev", id));
        }

    }
}
