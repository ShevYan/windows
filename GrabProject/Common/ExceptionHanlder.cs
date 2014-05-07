using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Common
{
    public class ExceptionHanlder
    {
        public static string LOG_DIR = "log/";

        public static ExceptionHanlder getInstance() {
            if (st_Instance == null) {
                st_Instance = new ExceptionHanlder();
            }

            return st_Instance;
        }

        public LinkedList<string> GetExceptionFiles()
        {
            // TODO:
            LinkedList<string> ret = new LinkedList<string>();
            return ret;
        }

        public void dump(Exception ex)
        {
            if (! Directory.Exists(LOG_DIR)) {
                Directory.CreateDirectory(LOG_DIR);
            }

            DateTime dt = DateTime.Now;
            string cur = (dt.ToString() + "." + dt.Millisecond).Replace('/', '-').Replace(':', '-') + ".txt";

            StreamWriter sw = new StreamWriter(LOG_DIR + cur);
            GrabConfig localCfg = GrabConfig.getLocalConfig();
            sw.WriteLine(cur);
            sw.Write(localCfg.ToString());
            sw.Write(ex.ToString());
            sw.Close();
        }

        private ExceptionHanlder()
        {

        }

        private static ExceptionHanlder st_Instance = null;
    }
}
