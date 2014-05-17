using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.IO.Compression;

namespace Common
{
    public class NetworkHelper
    {
        public static NetworkHelper GetInstance()
        {
            if (st_Instance == null)
            {
                st_Instance = new NetworkHelper();
            }

            return st_Instance;
        }

        public void Download(string url, string localPath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFile(Parameters.HOME_PAGE + url, localPath);
        }

        


        private NetworkHelper()
        {

        }

        
        public string HttpRequestRawWithCookie(string url, string method, byte[] data, Encoding encoding, 
            string strCookie, int timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set the Method property of the request to POST.
            request.Method = method;
            WebHeaderCollection headers = request.Headers;
            if (timeout > 0) {
                request.Timeout = timeout;                    
            }
            
            request.KeepAlive = true;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/34.0.1847.131 Safari/537.36";
            headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            headers.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6,zh-TW;q=0.4");
            headers.Add("Cookie", strCookie);

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

            // Get the response.
            HttpWebResponse response = null;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (System.Net.WebException ex)
            {
                Trace.Write(ex.ToString());
                throw;
            }
            
            // Display the status.
            //Console.WriteLine("1--" + ((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            string responseFromServer = null;
            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                if (response.ContentEncoding.Contains("gzip")) {
                    using (GZipStream stream = new GZipStream(dataStream, CompressionMode.Decompress)) {
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
                            responseFromServer = System.Text.Encoding.GetEncoding("GBK").GetString(memory.ToArray());
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
                        responseFromServer = System.Text.Encoding.GetEncoding("GBK").GetString(memory.ToArray());
                        responseFromServer = Utils.GBKToUtf8(responseFromServer);
                    }
                }
            }
            
            
            return responseFromServer;
        }

        public string HttpRequestRaw(string url, string method, byte[] data, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            // Set the Method property of the request to POST.
            request.Method = method;

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            
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

            // Get the response.
            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (System.Net.WebException ex)
            {
                Trace.Write(ex.ToString());
                throw;
            }

            // Display the status.
            //Console.WriteLine("1--" + ((HttpWebResponse)response).StatusDescription);

            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream, encoding);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            // conver to utf8
            if (!encoding.Equals(Encoding.UTF8)) {
                return Utils.GBKToUtf8(responseFromServer);
            }

            return responseFromServer;
        }

        public string HttpRequest(string url, string method, byte[] data, Encoding encoding)
        {
            return HttpRequestRaw(Parameters.HOME_PAGE + url, method, data, encoding);
        }

        private static NetworkHelper st_Instance = null;

 
    }
}
