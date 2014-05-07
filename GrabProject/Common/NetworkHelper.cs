using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net;
using System.IO;
using System.Diagnostics;

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

        public string HttpRequest(string url, string method, byte[] data)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Parameters.HOME_PAGE + url);

            // Set the Method property of the request to POST.
            request.Method = method;
 
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            Stream dataStream = null;
            if (null != data && data.Length > 0) {
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
            StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            string responseFromServer = reader.ReadToEnd();
            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        private static NetworkHelper st_Instance = null;

        public void HttpRequest(string p)
        {
            throw new NotImplementedException();
        }
    }
}
