using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Grab
{
    public abstract class Step
    {

        public GrabForm MForm { set; get; }
        public string Url { set; get; }
        public bool Done { set; get; }

        //Do pre-work, eg: check if an element is ready.
        public abstract bool PreDo();

        public abstract bool Navigated();

        public abstract bool DocumentCompleted();

        // Do post-work, eg: check if des web is ready. If return true, In the DocumentComplete callback will remove the step.
        public abstract bool PostDo();
    }
}
