using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;

namespace Taobao
{
    public class JSAction
    {
        WebBrowser browser;
        public const string jsScript =
@"
if (!document.getElementsByClassName) {
	document.getElementsByClassName = function(className, element) {
		var children = (element || document).getElementsByTagName('*');
		var elements = new Array();
		for ( var i = 0; i < children.length; i++) {
			var child = children[i];
			var classNames = child.className.split(' ');
			for ( var j = 0; j < classNames.length; j++) {
				if (classNames[j] == className) {
					elements.push(child);
					break;
				}
			}
		}
		return elements;
	};
};

function grabRefresh() {
	var e1 = document.getElementById('J_SecKill');
	var btn = document.getElementsByClassName('sk-button', e1);
	btn[0].click();
};

function grabSecKill(answer) {
	var e1 = document.getElementById('J_SecKill');
	document.getElementsByClassName('answer-input', e1)[0].value = answer;
	document.getElementsByClassName('J_Submit', e1)[0].click();
};

function getQuestionUrl() {
	var e1 = document.getElementById('J_SecKill');
	var e2 = document.getElementsByClassName('question-img', e1);
	return e2[0].src;
};

// kill Timer
function killTimer() {
	try {
		var e1 = document.getElementById('J_SecKill');
		var e2 = document.getElementsByClassName('answer-input', e1);
		if (e2 && e2.length > 0) {
			return;
		} else {
			grabRefresh();
		}
	} catch (err) {

	}
	
	if (bKillTimerStart) {
        window.external.AnotherMethod('trace', 'in-timer');
		setTimeout('killTimer()', 50);
	}
};

bKillTimerStart = false;
function startTimer() {
	bKillTimerStart = true;
	setTimeout('killTimer()', 50);
};

function stopTimer() {
	bKillTimerStart = false;
};

function freshedTest() {
	window.external.AnotherMethod('freshed', 'http://img1.tbcdn.cn/tfscom/T1Amv2FH4cXXagOFbX');
};

";
        public delegate void JSCallBack();
        JSCallBack jsCB;
        bool started = false;

        public JSAction(WebBrowser browser)
        {
            this.browser = browser;
        }

        public void Inject()
        {
            HtmlElement head = browser.Document.GetElementsByTagName("head")[0];
            HtmlElement script = browser.Document.CreateElement("script");
            IHTMLScriptElement domElement = (IHTMLScriptElement)script.DomElement;
            domElement.text = jsScript;
            head.AppendChild(script);
        }

        public void Refresh()
        {
            browser.Document.InvokeScript("grabRefresh");
        }

        public void SecKill(string answer)
        {
            browser.Document.InvokeScript("grabSecKill", new object[] {answer});
        }

        public void Alert(string text)
        {
            browser.Document.InvokeScript("alert", new object[] { text });
        }

        public bool IsScekillStarted()
        {
            return started;
        }

        public void StartSecKillTimer()
        {
            started = true;
            browser.Document.InvokeScript("startTimer");
        }

        public void StopSecKillTimer()
        {
            browser.Document.InvokeScript("stopTimer");
            started = false;
        }


        // TEST
        public void FreshedTest()
        {
            browser.Document.InvokeScript("freshedTest");
        }
    }
}
