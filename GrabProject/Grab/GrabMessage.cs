using System;
using System.Collections.Generic;
using System.Text;

namespace Grab
{
    public class GrabMessage
    {
        public const int NAVIGATE = 90;
        public const int EXIT_APP = 100;
        public const int BEGIN_PREPARE = 101;
        public const int PREPARED_OK = 102;
        public const int PREPARED_FAILED = 103;
        public const int BEGIN_SECKILL = 111;
        public const int START_JS_TIMER = 112;
        public const int END_SECKILL = 113;
        public int MsgType { get; set; }
        public object[] args;
    }
}
