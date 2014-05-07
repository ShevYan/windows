using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Common
{
    public class GrabConfig
    {
        public static string GRAB_CONF_PATH = "grab_conf.txt";
        public static string[] OPTIONS = {
            "GrabHelper.version",
            "GrabHelper.path",
            "Grab.version",
            "Grab.path"};

        public GrabConfig()
        {
        }

        public static GrabConfig getRemoteConfig()
        {
            string resp = null;
            try
            {
            	resp = NetworkHelper.GetInstance().HttpRequest("/getcfg", "GET", null);
            }
            catch (System.Net.WebException ex)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream(Encoding.Default.GetBytes(resp));

            GrabConfig cfg = new GrabConfig();
            StreamReader reader = new StreamReader(ms);
            string line = null;
            while ((line = reader.ReadLine()) != null) {
                foreach (string opt in OPTIONS)
                {
                    if (line.Contains(opt))
                    {
                        cfg.confs.Add(opt, line.Replace(opt + "=", ""));
                    }
                }
            }
            reader.Close();
            return cfg;
        }

        public static GrabConfig getLocalConfig()
        {
            GrabConfig cfg = new GrabConfig();
            StreamReader reader = null;
            try
            {
            	reader = File.OpenText(GRAB_CONF_PATH);
            }
            catch (System.Exception)
            {
                return null;
            }

            string line = null;
            while ((line = reader.ReadLine()) != null) {
                foreach (string opt in OPTIONS)
                {
                    if (line.Contains(opt))
                    {
                        cfg.confs.Add(opt, line.Replace(opt + "=", ""));
                    }
                }
            }
            reader.Close();

            return cfg;
        }
        
        public override string ToString()
        {
            string ret = "";
            foreach (KeyValuePair<string, string> pair in confs)
            {
                ret += pair.Key + "=" + pair.Value;
                ret += "\r\n";
            }

            return ret;
        }

        public void writeToLocal()
        {
            StreamWriter sw = new StreamWriter(GRAB_CONF_PATH);
            sw.Write(this.ToString());
            sw.Close();
        }

        public Dictionary<string, string> confs = new Dictionary<string, string>();
    }
}
