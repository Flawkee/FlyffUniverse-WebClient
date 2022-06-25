using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Text.RegularExpressions;
using System.Threading;

namespace FlyffUniverse_WebClient
{
    public partial class FlyffWCForm : Form
    {
        private static FlyffWCForm _instance;
        public ChromiumWebBrowser chromeBrowser;
        private System.Windows.Forms.Timer waitForGameExitTimer;
        public FlyffWCForm()
        {
            if (_instance == null) { _instance = this; }
            InitializeComponent();
            SetArguments();
        }
        public static FlyffWCForm Instance { get { return _instance; } }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Shown += new EventHandler(Form1_Shown);
        }
        private void SetArguments()
        {
            ArgumentManager.Instance.InitializeArguments();
            InitializeChromium();
        }

        public void InitializeChromium()
        {
            CefSettings settings = new CefSettings();
            Cef.EnableHighDPISupport();
            settings.CachePath = ArgumentManager.profilePath;
            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 /CefSharp Browser" + Cef.CefSharpVersion;
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser("https://universe.flyff.com/play");
            this.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;

        }
        private void Form1_Shown(Object sender, EventArgs e)
        {
            InitWaitForGameExitTimer();
        }
        public void WaitForGameExit()
        {
            string pat = "Restart the Game";
            Regex r = new Regex(pat, RegexOptions.IgnoreCase);
                if (chromeBrowser.IsBrowserInitialized)
                {
                    var task = chromeBrowser.GetTextAsync();
                task.ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        var response = t.Result;
                        Match m = r.Match(response);
                        if (m.Success) { Application.Exit(); }
                    }
                });
                }
        }
        public void InitWaitForGameExitTimer()
        {
            waitForGameExitTimer = new System.Windows.Forms.Timer();
            waitForGameExitTimer.Tick += new EventHandler(waitForGameExitTimer_Tick);
            waitForGameExitTimer.Interval = 1000; // in miliseconds
            waitForGameExitTimer.Start();
        }
        private void waitForGameExitTimer_Tick(object sender, EventArgs e)
        {
            Thread t = new Thread(WaitForGameExit);
            t.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }


    }

}
