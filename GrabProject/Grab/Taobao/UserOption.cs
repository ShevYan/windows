using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Grab.Taobao
{
    public class UserOption
    {
        public string username { set; get; }
        public string passwd { set; get; }

        public UserOption(string u, string p)
        {
            username = u;
            passwd = p;
        }

        public static UserOption GetOption()
        {
            // username:*** password:***
            string uName = null;
            string uPasswd = null;

            try
            {
                byte[] bytes = System.IO.File.ReadAllBytes("./user_info.txt");
	            string info = Encoding.GetEncoding("GBK").GetString(bytes);
                uName = Utils.GetMiddleString(info, "username:", " password:");
                uPasswd = info.Substring(info.IndexOf("password:") + "password:".Length);
            }
            catch (System.Exception ex)
            {
	            return null;
            }

            UserOption opt = new UserOption(uName, uPasswd);

            return opt;
        }

        public void save() {
            string text = "username:" + username + " password:" + passwd;

            System.IO.File.WriteAllBytes("./user_info.txt", Encoding.GetEncoding("GBK").GetBytes(text));
        }
    }
}
