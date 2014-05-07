using System;
using System.Collections.Generic;
using System.Text;

namespace Grab
{
    public class GrabMessage
    {
        public const int EXIT_APP = 0;
        public int MsgType { get; set; }
        public object[] args;
    }
}
