using System;
using System.Collections.Generic;
using System.Text;

namespace Grab
{
    public class Glogger
    {
        public static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}
