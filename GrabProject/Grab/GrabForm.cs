using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;
using Common;
using System.Net;
using Grab.Taobao;
using Taobao;
using mshtml;
using System.Runtime.InteropServices;

namespace Grab
{

    public partial class GrabForm : Form
    {
        public JSAction globalJS;
        BackgroudThread thread;
        SynchronizationContext syncCxt = null;
        long dTime = 0;
        string strCookie = null;
        public UserOption userOpt = null;
        OptionForm optForm = null;
        public bool isLogin = false;
        public bool isPrepared = false;
        public PrepareThread prepareThread;
        public delegate void ImgUrlGot_delegate(string url);

        long beforeTick = 0;
        long afterTick = 0;

        public void ImgUrlGot(string url)
        {
            if (pictureBox.InvokeRequired)//等待异步 
            {
                Trace.WriteLine("ImgUrlGot: " + url);
                ImgUrlGot_delegate cb = new ImgUrlGot_delegate(ImgUrlGot);
                this.Invoke(cb, new object[1] { url });
            }
            else
            {
                beforeTick = DateTime.Now.Ticks;
                pictureBox.LoadAsync(url);
                pictureBox.Refresh();
                answerText.Focus();
            }
        }

        [ComVisible(true)]
        public class ScriptManager
        {
            // Variable to store the form of type Form1.
            private GrabForm mForm;


            // Constructor.
            public ScriptManager(GrabForm form)
            {
                // Save the form so it can be referenced later.
                mForm = form;
            }

            // This method can also be called from JavaScript.
            public void AnotherMethod(string cmd, string arg)
            {
                if (cmd.Equals("trace"))
                {
                    Trace.WriteLine(DateTime.Now.TimeOfDay.ToString() + ":" + arg);
                }
                else if (cmd.Equals("freshed"))
                {
                    string imgUrl = arg;
                    mForm.globalJS.StopSecKillTimer();
                    //<img class=​"question-img" src=​"http:​/​/​img1.tbcdn.cn/​tfscom/​T1y8gcFR0XXXagOFbX">​
                    Trace.WriteLine("refreshed! " + imgUrl);
                    //                     string answer = null;
                    //                     mForm.pictureBox.LoadAsync();
                    //                     mForm.answerText.Focus();
                }
            }
        }

        // 抢单页面： http://tejia.taobao.com/one.htm?spm=a3109.6190710.0.0.RnSxLv&area=5
        public GrabForm()
        {
            optForm = new OptionForm(this);
            userOpt = UserOption.GetOption();
            if (userOpt == null)
            {
                optForm.ShowDialog();
            }
            userOpt = UserOption.GetOption();

            InitializeComponent();

            webBrowser.ObjectForScripting = new ScriptManager(this);
            globalJS = new JSAction(webBrowser);

            // only new it, but not start.
            prepareThread = new PrepareThread(this, SynchronizationContext.Current);

            //             syncCxt = SynchronizationContext.Current;
            //             thread = new BackgroudThread(this, syncCxt);
            //             thread.Start();
        }

        private void GrabForm_Load(object sender, EventArgs e)
        {
            this.CenterToParent();
            listView.FullRowSelect = true;
            listView.ListViewItemSorter = new ListViewColumnSorter();
            listView.ColumnClick += new ColumnClickEventHandler(ListViewHelper.ListView_ColumnClick);
            taobaoTimeLabel.Text = DateTime.Now.ToShortTimeString() + ":" + DateTime.Now.Second;
        }

        void ShowStatus()
        {
            string loginStatus = "";
            string prepareStatus = "";
            string usr = "";
            string delta = "";
            string lag = "";
            string secAnswerTime = "";
            string strNextGood = "";

            if (isLogin)
            {
                loginStatus = "已登陆";
            }
            else
            {
                loginStatus = "未登陆";
            }

            if (isPrepared)
            {
                prepareStatus = "准备就绪";
            }
            else
            {
                prepareStatus = "未准备就绪";
            }

            if (userOpt.username != null)
            {
                usr = userOpt.username;
            }
            else
            {
                usr = "未知";
            }

            if (prepareThread.deltaTime != null)
            {
                delta = prepareThread.deltaTime.value / 10000 + "";
                lag = prepareThread.deltaTime.lag / 10000 + "";
            }
            else
            {
                delta = lag = "未知";
            }

            DateTime now = prepareThread.deltaTime.GetCurrentTaobaoTime();
            DateTime nextKillTime = prepareThread.deltaTime.GetNextKillTime(now);
            GoodInfo nextGood = null;
            SecKillThread.GetGoodsToKill().TryGetValue(nextKillTime, out nextGood);
            if (nextGood != null )
            {
                strNextGood = nextGood.GetGoodId();
            } else {
                strNextGood = "无";
            }

            secAnswerTime = (afterTick - beforeTick) / 10000 + "";
            statusLabel.Text = string.Format("登陆状态: {0}   |   准备状态: {1}   |   用户名: {2}   |   淘宝时间差: {3}   |   网络延时: {4}   |   答题时间: {5}   |   下个秒杀: {6}",
                loginStatus, prepareStatus, usr, delta, lag, secAnswerTime, strNextGood);
        }

        private void formResize(object sender, EventArgs e)
        {
            // resize

            Size sz = this.Size;

            // webBrowser
            sz.Height -= 330;
            sz.Width = sz.Width / 3 * 2;
            webBrowser.Size = sz;

            // statusLabel
            statusLabel.Location = new Point(webBrowser.Location.X, webBrowser.Location.Y + webBrowser.Height + 10);
            sz = this.Size;
            sz.Width -= 50;
            sz.Height = statusLabel.Height;
            statusLabel.Size = sz;

            statusLabel.Text = "状态:dddddddddddddddddddasdfasdfasdf";
        }

        public void SetTextSafePost(object obj)
        {
            GrabMessage msg = obj as GrabMessage;
            switch (msg.MsgType)
            {
                case GrabMessage.NAVIGATE:
                    webBrowser.Navigate(msg.args[0] as string);
                    break;

                case GrabMessage.EXIT_APP:
                    thread.Stop();
                    Application.Exit();
                    break;

                case GrabMessage.BEGIN_PREPARE:
                    prepareBtn.Enabled = false;
                    break;

                case GrabMessage.PREPARED_FAILED:
                    MessageBox.Show("秒杀准备失败，请检查用户密码是否正确，以及浏览器中是否正常登陆。");
                    prepareBtn.Enabled = true;
                    break;

                case GrabMessage.PREPARED_OK:
                    showArea(findNextSeckillAreaIndex());
                    prepareBtn.Enabled = true;
                    break;

                case GrabMessage.BEGIN_SECKILL:
                    break;

                case GrabMessage.START_JS_TIMER:
                    globalJS.Inject();
                    globalJS.StartSecKillTimer();
                    break;

                case GrabMessage.END_SECKILL:
                    break;

                default:
                    break;
            }
        }

        private void buttonOption_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(this);
            optionForm.ShowDialog();
        }

        private void webUrl_Click(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                goBtn_Click(null, null);
            }
        }


        private void docCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().Contains("tejia.taobao.com"))
            {
                HtmlElement usr = webBrowser.Document.GetElementById("TPL_username_1");
                HtmlElement pwd = webBrowser.Document.GetElementById("J_PwdV");
                HtmlElement btn = webBrowser.Document.GetElementById("J_VerifySubmit");

                // login
                if (usr != null && pwd != null && btn != null)
                {
                    usr.InnerText = userOpt.username;
                    pwd.InnerText = userOpt.passwd;
                    btn.InvokeMember("click");
                }
            }
            else if (e.Url.ToString().Contains("item.taobao.com"))
            {
                webUrl.Text = e.Url.ToString();
            }
            else
            {
                if (webBrowser.Document.Title.Contains("1元特价 - 淘宝天天特价") && isLogin == false)
                {
                    isLogin = true;
                    prepareThread.Stop();
                    prepareThread.Start();
                }
            }

        }

        public string GetWebHtml()
        {
            StreamReader reader = new StreamReader(webBrowser.DocumentStream, Encoding.GetEncoding("GBK"));
            string str = reader.ReadToEnd();
            reader.Close();
            return str;
        }

        public string GetGlobleCookie()
        {
            if (strCookie == null || strCookie.Length == 0)
            {
                strCookie = FullWebBrowserCookie.GetCookieInternal(webBrowser.Url, true);
                Trace.WriteLine("Cookie: " + strCookie);
            }

            return strCookie;
        }

        int CallBack_Fun(object obj, HttpWebResponse resp)
        {
            AsyncHttpWebRequest asyncReq = obj as AsyncHttpWebRequest;
            string responseFromServer = "";
            Stream dataStream = resp.GetResponseStream();
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
                } while (count > 0);

                responseFromServer = System.Text.Encoding.GetEncoding("GBK").GetString(memory.ToArray());
                responseFromServer = Utils.GBKToUtf8(responseFromServer);
            }

            dataStream.Close();
            Trace.WriteLine(responseFromServer);

            return 0;
        }

        // important
        //         private void timer1_Tick_GetImg(object sender, EventArgs e)
        //         {
        //             for (int i = 1; i <= 24; i++)
        //             {
        //                 long now = (DateTime.UtcNow.Ticks - Utils.UTCZero.Ticks) / 10000;
        //                 long verifiedNow = now - dTime;
        //                 long startTime = (areaArray[i].startTime.Ticks - Utils.UTCZero.Ticks) / 10000;
        // 
        //                 if ((startTime > verifiedNow && startTime - verifiedNow < 1 * 1000) ||
        //                     (verifiedNow > startTime && verifiedNow - startTime < 20 * 1000))
        //                 {
        //                     if (areaTriggerFlag[i] == false)
        //                     {
        //                         areaTriggerFlag[i] = true;
        //                         foreach (GoodInfo good in areaArray[i].goodList)
        //                         {
        //                             Trace.WriteLine("trigger " + i + ": " + DateTime.Now.ToString());
        //                             good.QueryReq(GetGlobleCookie(), 25 * 1000, this);
        //                         }
        //                     }
        //                 }
        //             }
        //         }

        //执行脚本
        private void executeBtn_Click(object sender, EventArgs e)
        {
            HtmlElement head = webBrowser.Document.GetElementsByTagName("head")[0];
            HtmlElement script = webBrowser.Document.CreateElement("script");
            IHTMLScriptElement domElement = (IHTMLScriptElement)script.DomElement;
            domElement.text = "";
            head.AppendChild(script);
        }

        private void webBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            WebBrowser webBrowser_temp = (WebBrowser)sender;
            string newUrl = webBrowser_temp.Document.ActiveElement.GetAttribute("href");
            webBrowser.Url = new Uri(newUrl);
            e.Cancel = true;
        }

        private void alertBtn_Click(object sender, EventArgs e)
        {
            globalJS.Alert("1234444");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            globalJS.Refresh();
        }




        private void goBtn_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate(webUrl.Text);
        }

        private void seckillBtn_Click(object sender, EventArgs e)
        {
            string id = GetCurrentItemIdFromCurrentWeb();
            GoodInfo good = prepareThread.GetGoodById(id);
            if (good != null)
            {
                GoodInfo goodInMap = null;
                SecKillThread.GetGoodsToKill().TryGetValue(good.startTime, out goodInMap);
                if (goodInMap != null)
                {
                    MessageBox.Show("该时段已经存在秒杀物品");
                }
                else
                {
                    SecKillThread.GetGoodsToKill().Add(good.startTime, good);
                }
            }
        }


        public string GetCurrentItemIdFromCurrentWeb()
        {
            //priceCutUrl:"http://detailskip.taobao.com/json/pricecutStatic.htm?id=38862449645&rootCatId=50008163"

            if (webBrowser.DocumentText.Contains("http://detailskip.taobao.com/json/pricecutStatic.htm?id="))
            {
                return Utils.GetMiddleString(webBrowser.DocumentText,
                    "http://detailskip.taobao.com/json/pricecutStatic.htm?id=",
                    "&");
            }
            return null;
        }

        private int findNextSeckillAreaIndex()
        {
            DateTime taobaoNow = prepareThread.deltaTime.GetCurrentTaobaoTime();
            DateTime nextKillTime = prepareThread.deltaTime.GetNextKillTime(taobaoNow);

            for (int i = 0; i < SecKillThread.KillTimes.Length; i++ )
            {
                if (SecKillThread.KillTimes[i].Equals(nextKillTime))
                {
                    return i + 1;
                }
            }

            return 1;
        }

        private void showArea(int index)
        {
            if (prepareThread.areaArray[index] == null)
            {
                return;
            }
            webBrowser.Navigate(prepareThread.areaArray[index].uri);
            listView.Items.Clear();
            pageLabel.Text = "第" + index + "区";
            foreach (GoodInfo good in prepareThread.areaArray[index].goodList)
            {
                ListViewItem li = new ListViewItem(good.name); ;
                li.SubItems.Add(good.orgPrice);
                li.SubItems.Add(good.seckillPrice);

                li.SubItems.Add(prepareThread.areaArray[index].startTime.ToShortTimeString());
                listView.Items.Add(li);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            showArea(findNextSeckillAreaIndex());
        }

        private void prepageBtn_Click(object sender, EventArgs e)
        {
            int index = int.Parse(pageLabel.Text.Replace("第", "").Replace("区", ""));
            if (index == 1)
            {
                index = 1;
            }
            else
            {
                --index;
            }
            showArea(index);
        }

        private void nextpageBtn_Click(object sender, EventArgs e)
        {
            int index = int.Parse(pageLabel.Text.Replace("第", "").Replace("区", ""));
            if (index == 24)
            {
                index = 24;
            }
            else
            {
                ++index;
            }
            showArea(index);
        }

        public WebBrowser GetWebBrowser()
        {
            return webBrowser;
        }

        public string GetDefaultUrl()
        {
            return AreaInfo.prefix + 0;
        }


        private void perpareBtn_Click(object sender, EventArgs e)
        {
            prepareThread.Stop();
            prepareThread.Start();
        }

        private void answerText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                globalJS.StopSecKillTimer();
                globalJS.SecKill(answerText.Text);
                afterTick = DateTime.Now.Ticks;
                ShowStatus();
            }
        }




        /// <summary>
        /// countTimer: start in readyTimer; never stop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void countTimer_Tick(object sender, EventArgs e)
        {
            DateTime dtTaobao = prepareThread.deltaTime.GetCurrentTaobaoTime();
            DateTime dtNextKill = prepareThread.deltaTime.GetNextKillTime(dtTaobao);
            TimeSpan delta = dtNextKill.Subtract(dtTaobao);

            // set counter
            taobaoTimeLabel.Text = dtTaobao.ToShortTimeString() + ":" + dtTaobao.Second;
            countLabel.Text = delta.Minutes + ":" + delta.Seconds;

            // find next kill thread.
            SecKillThread nextKillThread = null;
            SecKillThread.GetKillThreads().TryGetValue(dtNextKill, out nextKillThread);
            if (null == nextKillThread)
            {
                return;
            }

            // if thread has started, return directly.
            if (nextKillThread.Status != SecKillThread.SECTHREAD_IDLE)
            {
                return;
            }

            // check goods
            GoodInfo good = null;
            SecKillThread.GetGoodsToKill().TryGetValue(dtNextKill, out good);
            if (good == null)
            {
                return;
            }

            TimeSpan d2 = good.startTime.Subtract(dtTaobao);
            if (d2.TotalMilliseconds > 0 && d2.TotalSeconds < 4.5 * 60)
            {
                webBrowser.Navigate(good.uri);
                nextKillThread.SetForm(this, SynchronizationContext.Current);
                nextKillThread.SetGood(good);
                nextKillThread.Start();
            }

        }

        private void testBtn_Click(object sender, EventArgs e)
        {
            ImgUrlGot("./curImg.jpg");
        }

        private void listview_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (listView.SelectedItems != null && listView.SelectedItems.Count > 0)
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    int index = findNextSeckillAreaIndex();
                    foreach (GoodInfo good in prepareThread.areaArray[index].goodList)
                    {
                        if (good.name.Contains(item.Text))
                        {
                            webBrowser.Navigate(good.uri);
                        }
                    }
                }
        }

        private void navigateFreshTimer_Tick(object sender, EventArgs e)
        {
            DateTime now = prepareThread.deltaTime.GetCurrentTaobaoTime();
            if ((now.Minute > 5 && now.Minute < 25) ||
                (now.Minute > 35 && now.Minute < 55))
            {
                Trace.WriteLine("refreshing webbrowser.");
                webBrowser.Refresh();
            }
        }

        private void pictureBox_LoadCOmpleted(object sender, AsyncCompletedEventArgs e)
        {
            answerText.Focus();
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            ShowStatus();
        }

    }
}

