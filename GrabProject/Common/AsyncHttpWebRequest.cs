using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace Common
{
    public class AsyncHttpWebRequest
    {
        public delegate int CallBack(AsyncHttpWebRequest thisReq, HttpWebResponse resp);
        CallBack callback = null;
        public HttpWebRequest httpReq { set; get; }
        public object[] userObj { set; get; }

        public AsyncHttpWebRequest(HttpWebRequest httpReq, object[] userObj, CallBack cb)
        {
            this.httpReq = httpReq;
            this.userObj = userObj;
            this.callback = cb;
        }


        public void QueueRequest()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc), this);
        }

        public static HttpWebRequest ConstructHttpRequest(string url, string method, string cookie, byte[] data, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set the Method property of the request to POST.
            request.Method = method;
            WebHeaderCollection headers = request.Headers;
            if (timeout > 0)
            {
                request.Timeout = timeout;
            }

            request.KeepAlive = true;
            request.Accept = "*/*";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.131 Safari/537.36";
            headers.Add("Accept-Encoding", "gzip, deflate,sdch");
            headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4");
            headers.Add("Cookie", cookie);

            Stream dataStream = null;
            if (null != data && data.Length > 0)
            {
                // Set the ContentLength property of the WebRequest.
                request.ContentLength = data.Length;

                // Get the request stream.
                dataStream = request.GetRequestStream();
                // Write the data to the request stream.
                dataStream.Write(data, 0, data.Length);
                // Close the Stream object.
                dataStream.Close();
            }

            return request;
        }

        public static string ConvertResponseToString(HttpWebResponse response, Encoding encoding)
        {
            string strResp = null;
            try
            {
                Stream dataStream = response.GetResponseStream();
                if (response.StatusCode.Equals(HttpStatusCode.OK))
                {
                    if (response.ContentEncoding.Contains("gzip"))
                    {
                        using (GZipStream stream = new GZipStream(dataStream, CompressionMode.Decompress))
                        {
                            const int size = 4096;
                            byte[] buffer = new byte[size];
                            using (MemoryStream memory = new MemoryStream())
                            {
                                int count = 0;
                                do
                                {
                                    count = stream.Read(buffer, 0, size);
                                    if (count > 0)
                                    {
                                        memory.Write(buffer, 0, count);
                                    }
                                }
                                while (count > 0);
                                strResp = encoding.GetString(memory.ToArray());
                                //responseFromServer = Utils.GBKToUtf8(responseFromServer);
                            }
                        }
                    }
                    else
                    {
                        const int size = 4096;
                        byte[] buffer = new byte[size];
                        using (MemoryStream memory = new MemoryStream())
                        {
                            int count = 0;
                            do
                            {
                                count = dataStream.Read(buffer, 0, size);
                                if (count > 0)
                                {
                                    memory.Write(buffer, 0, count);
                                }
                            }
                            while (count > 0);
                            strResp = encoding.GetString(memory.ToArray());
                            //responseFromServer = Utils.GBKToUtf8(responseFromServer);
                        }
                    }
                }

                dataStream.Close();
                dataStream = null;
            }
            catch (System.Exception ex)
            {  

            }


            return strResp;
        }

        static void ThreadProc(object req)
        {
            AsyncHttpWebRequest asyncReq = req as AsyncHttpWebRequest;
            // do http request
            HttpWebResponse response = null;
            try
            {
                response = asyncReq.httpReq.GetResponse() as HttpWebResponse;
            }
            catch (System.Net.WebException ex)
            {
                Trace.Write(ex.ToString());
                return;
            }

            // do completion work
            asyncReq.callback(asyncReq, response);

            response.Close();
            response = null;
        }
    }
}
