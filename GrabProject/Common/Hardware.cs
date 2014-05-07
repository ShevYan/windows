using System;
using System.Collections.Generic;
using System.Text;
using System.Management;

namespace Common
{
    public class Hardware
    {
        public static string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                mac = mac.Replace(":", "");
                return mac;
            }
            catch
            {
                return "unknow";
            }
        }
    }
}
