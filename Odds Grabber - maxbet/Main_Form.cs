using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odds_Grabber___maxbet
{
    public partial class Main_Form : Form
    {
        private ChromiumWebBrowser chromeBrowser;
        private string __app = "Odds Grabber";
        private string __app_type = "{edit this}";
        private string __brand_code = "{edit this}";
        private string __brand_color = "#0E3560";
        private string __url = "www.maxbet.com";
        private string __website_name = "maxbet";
        private string __app__website_name = "";
        private string __api_key = "youdieidie";
        private string __running_01 = "wft";
        private string __running_02 = "tbs";
        private string __running_11 = "WFT";
        private string __running_22 = "TBS";
        private int __send = 0;
        private int __r = 211;
        private int __g = 78;
        private int __b = 0;
        private bool __is_close;
        private bool __is_login = false;
        private bool __is_send = false;
        private bool __m_aeroEnabled;
        Form __mainFormHandler;

        // Drag Header to Move
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        // ----- Drag Header to Move

        // Form Shadow
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;
        private const int WM_NCHITTEST = 0x84;
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;
        private const int WS_MINIMIZEBOX = 0x20000;
        private const int CS_DBLCLKS = 0x8;
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }
        protected override CreateParams CreateParams
        {
            get
            {
                __m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;
                if (!__m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }
        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCPAINT:
                    if (__m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 0,
                            rightWidth = 0,
                            topHeight = 0
                        };
                        DwmExtendFrameIntoClientArea(Handle, ref margins);

                    }
                    break;
                default:
                    break;
            }
            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        // ----- Form Shadow

        public Main_Form()
        {
            InitializeComponent();

            timer_landing.Start();
        }

        // Drag to Move
        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_title_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_loader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void label_brand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_landing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void pictureBox_header_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----- Drag to Move

        // Click Close
        private void pictureBox_close_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                __is_close = true;
                Environment.Exit(0);
            }
        }

        // Click Minimize
        private void pictureBox_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        const UInt32 WM_CLOSE = 0x0010;

        void ___CloseMessageBox()
        {
            IntPtr windowPtr = FindWindowByCaption(IntPtr.Zero, "JavaScript Alert - https://www.maxbet.coms");

            if (windowPtr == IntPtr.Zero)
            {
                return;
            }

            SendMessage(windowPtr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        private void timer_close_message_box_Tick(object sender, EventArgs e)
        {
            if (__is_login)
            {
                ___CloseMessageBox();
            }
        }

        private void timer_size_Tick(object sender, EventArgs e)
        {
            __mainFormHandler = Application.OpenForms[0];
            __mainFormHandler.Size = new Size(466, 168);
        }

        // Form Closing
        private void Main_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!__is_close)
            {
                DialogResult dr = MessageBox.Show("Exit the program?", __app__website_name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            else
            {
                Environment.Exit(0);
            }
        }

        // Form Load
        private void Main_Form_Load(object sender, EventArgs e)
        {
            __app__website_name = __app + " - " + __website_name;
            panel1.BackColor = Color.FromArgb(__r, __g, __b);
            panel2.BackColor = Color.FromArgb(__r, __g, __b);
            label_brand.BackColor = Color.FromArgb(__r, __g, __b);
            Text = __app__website_name;
            label_title.Text = __app__website_name;

            InitializeChromium();
        }

        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (__is_send)
            {
                __is_send = false;
                MessageBox.Show("Telegram Notification is Disabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                __is_send = true;
                MessageBox.Show("Telegram Notification is Enabled.", __brand_code + " " + __app, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void timer_landing_Tick(object sender, EventArgs e)
        {
            panel_landing.Visible = false;
            //panel_cefsharp.Visible = false;
            //pictureBox_loader.Visible = true;
            //panel3.Visible = true;
            //panel4.Visible = true;
            //timer_size.Start();
            timer_landing.Stop();
        }

        public static void ___FlushMemory()
        {
            Process prs = Process.GetCurrentProcess();
            try
            {
                prs.MinWorkingSet = (IntPtr)(300000);
            }
            catch (Exception err)
            {
                // leave blank
            }
        }

        private void timer_flush_memory_Tick(object sender, EventArgs e)
        {
            ___FlushMemory();
        }

        private void SendMyBot(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "772918363:AAHn2ufmP3ocLEilQ1V-IHcqYMcSuFJHx5g";
                string chatId = "@allandrake";
                string text = "-----" + __app__website_name + "-----%0A%0AIP:%20ABC PC%0ALocation:%20Pacific%20Star%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20" + message;
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendMyBot(message);
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void SendABCTeam(string message)
        {
            try
            {
                string datetime = DateTime.Now.ToString("dd MMM HH:mm:ss");
                string urlString = "https://api.telegram.org/bot{0}/sendMessage?chat_id={1}&text={2}";
                string apiToken = "651945130:AAGMFj-C4wX0yElG2dBU1SRbfrNZi75jPHg";
                string chatId = "@odds_bot_abc_team";
                string text = "Bot:%20-----" + __website_name.ToUpper() + "-----%0ADate%20and%20Time:%20[" + datetime + "]%0AMessage:%20<b>" + message + "</>&parse_mode=html";
                urlString = String.Format(urlString, apiToken, chatId, text);
                WebRequest request = WebRequest.Create(urlString);
                Stream rs = request.GetResponse().GetResponseStream();
                StreamReader reader = new StreamReader(rs);
                string line = "";
                StringBuilder sb = new StringBuilder();
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                        sb.Append(line);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        __Flag();
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        SendABCTeam(message);
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private void timer_detect_running_Tick(object sender, EventArgs e)
        {
            //___DetectRunningAsync();
        }

        private async void ___DetectRunningAsync()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://192.168.10.252:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunning2Async();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___DetectRunning2Async()
        {
            try
            {
                string datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string password = __brand_code + datetime + "youdieidie";
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash)
                   .Replace("-", string.Empty)
                   .ToLower();

                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection
                    {
                        ["brand_code"] = __brand_code,
                        ["app_type"] = __app_type,
                        ["last_update"] = datetime,
                        ["token"] = token
                    };

                    var response = wb.UploadValues("http://zeus.ssitex.com:8080/API/updateAppStatus", "POST", data);
                    string responseInString = Encoding.UTF8.GetString(response);
                }
                __send = 0;
            }
            catch (Exception err)
            {
                __send++;

                if (___CheckForInternetConnection())
                {
                    if (__send == 5)
                    {
                        SendMyBot(err.ToString());
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___DetectRunningAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        // CefSharp Initialize
        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings();

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            Cef.Initialize(settings);
            chromeBrowser = new ChromiumWebBrowser(__url);
            panel_cefsharp.Controls.Add(chromeBrowser);
            chromeBrowser.AddressChanged += ChromiumBrowserAddressChanged;
        }

        private bool __detect = false;

        // CefSharp Address Changed
        private void ChromiumBrowserAddressChanged(object sender, AddressChangedEventArgs e)
        {
            __url = e.Address.ToString();
            Invoke(new Action(() =>
            {
                panel3.Visible = true;
                panel4.Visible = true;
            }));


            if (e.Address.ToString().Equals("https://www.maxbet.com/Default.aspx?IsSSL=1"))
            {
                var html = String.Empty;
                chromeBrowser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    html = taskHtml.Result.ToString().ToLower();

                    if (html.Contains("location forbidden"))
                    {
                        SendABCTeam("Please setup first VPN to the installed PC.");
                        __is_close = false;
                        Environment.Exit(0);
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            chromeBrowser.FrameLoadEnd += (sender_, args) =>
                            {
                                if (args.Frame.IsMain)
                                {
                                    Invoke(new Action(async () =>
                                    {
                                        if (!__is_login)
                                        {
                                            chromeBrowser.Focus();
                                            args.Frame.ExecuteJavaScriptAsync("document.getElementsByClassName('txtID')[0].focus();");
                                            await ___TaskWait_Handler(2);
                                            SendKeys.Send("s5mmmmm100");
                                            SendKeys.Send("\t");
                                            SendKeys.Send("Aa123456");
                                            SendKeys.SendWait("{ENTER}");

                                            chromeBrowser.SetZoomLevel(-5);
                                        }
                                        else
                                        {
                                            __detect = true;
                                        }
                                    }));
                                }
                            };
                        }));
                    }
                });
            }

            if (__detect)
            {
                if (e.Address.ToString().Contains("maxbet.com/sports"))
                {
                    Invoke(new Action(() =>
                    {
                        chromeBrowser.FrameLoadEnd += (sender_, args) =>
                        {
                            if (args.Frame.IsMain)
                            {
                                Invoke(new Action(() =>
                                {
                                    __is_login = true;
                                    SendABCTeam("Firing up!");

                                    Task task_01 = new Task(delegate { ___FIRST_RUNNINGAsync(); });
                                    task_01.Start();
                                }));
                            }
                        };
                    }));
                }
                else
                {
                    MessageBox.Show(e.Address.ToString());
                }
            }
        }

        // ----- Functions
        // WFT -----
        private async void ___FIRST_RUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel3.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                string start_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                string end_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

                start_time = start_time.Replace("-", "%2F");
                start_time = start_time.Replace(" ", "+");
                start_time = start_time.Replace(":", "%3A");

                end_time = end_time.Replace("-", "%2F");
                end_time = end_time.Replace(" ", "+");
                end_time = end_time.Replace(":", "%3A");

                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies("http://sports.wclub888.com", true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] result = await wc.DownloadDataTaskAsync("http://sports.wclub888.com/_View/RMOdds2GenRun.aspx?ot=t&update=false&sa=false&tv=0&tf=-1&TFStatus=0&mt=0&r=1038308059&t=" + _epoch + "&RId=0&_=" + _epoch);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserializeObject.ToString());
                JToken _count = _jo.SelectToken("$.JSOdds");

                string password = __website_name + __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_count.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    for (int i = 0; i < _count.Count(); i++)
                    {
                        JToken LeagueName = _jo.SelectToken("$.JSOdds[" + i + "][7]").ToString();
                        JToken HomeScore__AwayScore = _jo.SelectToken("$.JSOdds[" + i + "][16]").ToString();
                        string[] HomeScore__AwayScore_Replace = HomeScore__AwayScore.ToString().Split('-');
                        string HomeScore = HomeScore__AwayScore_Replace[0].Trim();
                        string AwayScore = HomeScore__AwayScore_Replace[1].Trim();
                        string[] AwayScore__MatchTimeHalf = null;
                        string MatchTimeHalf = "";
                        string MatchStatus = "";
                        JToken MatchTimeMinute = "0";
                        if (HomeScore.Contains("<br>") || AwayScore.Contains("<br>"))
                        {
                            AwayScore = HomeScore__AwayScore_Replace[1].Trim().Replace("<br>", "|");
                            AwayScore__MatchTimeHalf = AwayScore.Split('|');
                            AwayScore = AwayScore__MatchTimeHalf[0];
                            MatchTimeHalf = AwayScore__MatchTimeHalf[1];
                        }
                        if (MatchTimeHalf.ToLower().Contains("live"))
                        {
                            MatchTimeHalf = "LIVE";
                        }
                        else if (MatchTimeHalf.ToLower().Contains("ht"))
                        {
                            MatchTimeHalf = "HT";
                        }
                        else
                        {
                            MatchTimeHalf = _jo.SelectToken("$.JSOdds[" + i + "][18]").ToString() + "H";
                            MatchTimeMinute = _jo.SelectToken("$.JSOdds[" + i + "][19]").ToString().ToUpper();
                        }
                        if (__is_numeric(MatchTimeMinute.ToString()))
                        {
                            if (MatchTimeHalf.ToString() == "2H" && Convert.ToInt32(MatchTimeMinute.ToString()) > 30)
                            {
                                MatchTimeHalf = "FT";
                                MatchStatus = "C";
                            }
                            else
                            {
                                MatchStatus = "R";
                            }
                        }
                        else
                        {
                            MatchStatus = "R";
                        }
                        JToken HomeTeamName = _jo.SelectToken("$.JSOdds[" + i + "][26]").ToString();
                        JToken AwayTeamName = _jo.SelectToken("$.JSOdds[" + i + "][28]").ToString();
                        string StatementDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                        JToken KickOffDateTime = _jo.SelectToken("$.JSOdds[" + i + "][17]").ToString();
                        DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "dd/MM HH:mm", CultureInfo.InvariantCulture);
                        KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken FTHDP = _jo.SelectToken("$.JSOdds[" + i + "][24]").ToString();
                        JToken FTH = _jo.SelectToken("$.JSOdds[" + i + "][36]").ToString().Trim().Replace(".", "");
                        string FTH_Replace = FTH.ToString().Replace("-", "");
                        if (FTH_Replace.Length == 1 && FTH_Replace != "0")
                        {
                            FTH = FTH + "0";
                        }
                        FTH = Convert.ToDecimal(FTH) / 100;
                        JToken FTA = _jo.SelectToken("$.JSOdds[" + i + "][37]").ToString().Trim().Replace(".", "");
                        string FTA_Replace = FTA.ToString().Replace("-", "");
                        if (FTA_Replace.Length == 1 && FTA_Replace != "0")
                        {
                            FTA = FTA + "0";
                        }
                        FTA = Convert.ToDecimal(FTA) / 100;
                        string BetIDFTOU = "";
                        JToken FTOU = _jo.SelectToken("$.JSOdds[" + i + "][42]").ToString();
                        JToken FTO = _jo.SelectToken("$.JSOdds[" + i + "][46]").ToString().Trim().Replace(".", "");
                        string FTO_Replace = FTO.ToString().Replace("-", "");
                        if (FTO_Replace.Length == 1 && FTO_Replace != "0")
                        {
                            FTO = FTO + "0";
                        }
                        JToken FTU = _jo.SelectToken("$.JSOdds[" + i + "][47]").ToString().Trim().Replace(".", "");
                        string FTU_Replace = FTU.ToString().Replace("-", "");
                        if (FTU_Replace.Length == 1 && FTU_Replace != "0")
                        {
                            FTU = FTU + "0";
                        }
                        FTU = Convert.ToDecimal(FTU) / 100;
                        JToken FT1 = _jo.SelectToken("$.JSOdds[" + i + "][51]").ToString();
                        JToken FT2 = _jo.SelectToken("$.JSOdds[" + i + "][52]").ToString();
                        JToken FTX = _jo.SelectToken("$.JSOdds[" + i + "][53]").ToString();
                        string BetIDFTOE = "";
                        string FTOdd = "";
                        string FTEven = "";
                        string BetIDFT1X2 = "";
                        string SpecialGame = "";
                        JToken FHHDP = _jo.SelectToken("$.JSOdds[" + i + "][56]").ToString();
                        JToken FHH = _jo.SelectToken("$.JSOdds[" + i + "][60]").ToString().Trim().Replace(".", "");
                        string FHH_Replace = FHH.ToString().Replace("-", "");
                        if (FHH_Replace.Length == 1 && FHH_Replace != "0")
                        {
                            FHH = FHH + "0";
                        }
                        FHH = Convert.ToDecimal(FHH) / 100;
                        JToken FHA = _jo.SelectToken("$.JSOdds[" + i + "][61]").ToString().Trim().Replace(".", "");
                        string FHA_Replace = FHA.ToString().Replace("-", "");
                        if (FHA_Replace.Length == 1 && FHA_Replace != "0")
                        {
                            FHA = FHA + "0";
                        }
                        FHA = Convert.ToDecimal(FHA) / 100;
                        JToken FHOU = _jo.SelectToken("$.JSOdds[" + i + "][64]").ToString();
                        JToken FHO = _jo.SelectToken("$.JSOdds[" + i + "][67]").ToString().Trim().Replace(".", "");
                        string FHO_Replace = FHO.ToString().Replace("-", "");
                        if (FHO_Replace.Length == 1 && FHO_Replace != "0")
                        {
                            FHO = FHO + "0";
                        }
                        FHO = Convert.ToDecimal(FHO) / 100;
                        JToken FHU = _jo.SelectToken("$.JSOdds[" + i + "][67]").ToString().Trim().Replace(".", "");
                        string FHU_Replace = FHU.ToString().Replace("-", "");
                        if (FHU_Replace.Length == 1 && FHU_Replace != "0")
                        {
                            FHU = FHU + "0";
                        }
                        FHU = Convert.ToDecimal(FHU) / 100;
                        JToken FH1 = _jo.SelectToken("$.JSOdds[" + i + "][72]").ToString();
                        JToken FH2 = _jo.SelectToken("$.JSOdds[" + i + "][73]").ToString();
                        JToken FHX = _jo.SelectToken("$.JSOdds[" + i + "][74]").ToString();

                        string ref_id_password = DateTime.Now.ToString("yyyy-MM-dd") + "3" + "Soccer" + LeagueName + HomeTeamName + AwayTeamName;
                        byte[] ref_id_encodedpassword = new UTF8Encoding().GetBytes(ref_id_password.Trim());
                        byte[] ref_hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(ref_id_encodedpassword);
                        string ref_token = BitConverter.ToString(ref_hash).Replace("-", string.Empty).ToLower().Substring(0, 8);
                        ref_match_id = ref_token;

                        if (ref_match_id == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = ref_match_id;

                        if (HomeTeamName.ToString().ToLower().Contains("vs") || AwayTeamName.ToString().ToLower().Contains("vs"))
                        {
                            string[] replace = HomeTeamName.ToString().Split(new string[] { "vs" }, StringSplitOptions.None);
                            HomeTeamName = replace[0].Trim();
                            AwayTeamName = replace[1].Trim();
                        }
                        else if (HomeTeamName.ToString().ToLower().Contains("+") || AwayTeamName.ToString().ToLower().Contains("+"))
                        {
                            string[] replace = HomeTeamName.ToString().Split('+');
                            HomeTeamName = replace[0].Trim();
                            AwayTeamName = replace[1].Trim();
                        }

                        var reqparm_ = new NameValueCollection
                        {
                            {"source_id", "3"},
                            {"sport_name", ""},
                            {"league_name", LeagueName.ToString().Trim()},
                            {"home_team", HomeTeamName.ToString().Trim()},
                            {"away_team", AwayTeamName.ToString().Trim()},
                            {"home_team_score", HomeScore.ToString()},
                            {"away_team_score", AwayScore.ToString()},
                            {"ref_match_id", ref_match_id},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdp", (FTHDP.ToString() != "") ? FTHDP.ToString() : "0"},
                            {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                            {"betidftou", (BetIDFTOU.ToString() != "") ? BetIDFTOU.ToString() : "0"},
                            {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                            {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                            {"betidftoe", (BetIDFTOE.ToString() != "") ? BetIDFTOE.ToString() : "0"},
                            {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                            {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                            {"betidft1x2", (BetIDFT1X2.ToString() != "") ? BetIDFT1X2.ToString() : "0"},
                            {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                            {"specialgame", (SpecialGame.ToString() != "") ? SpecialGame.ToString() : "0"},
                            {"fhhdp", (FHHDP.ToString() != "") ? FHHDP.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                            {"fhodd", "0"},
                            {"fheven", "0"},
                            {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", MatchTimeHalf.ToString()},
                            {"match_status", MatchStatus},
                            {"match_minute", MatchTimeMinute.ToString()},
                            {"api_status", "R"},
                            {"token_api", token},
                        };

                        try
                        {
                            WebClient wc_ = new WebClient();
                            byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                            string responsebody_ = Encoding.UTF8.GetString(result_);
                            __send = 0;
                        }
                        catch (Exception err)
                        {
                            __send++;

                            if (___CheckForInternetConnection())
                            {
                                if (__send == 5)
                                {
                                    SendMyBot(err.ToString());
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    WebClient wc_ = new WebClient();
                                    byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody_ = Encoding.UTF8.GetString(result_);
                                }
                            }
                            else
                            {
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                __send = 0;
                ___FIRST_NOTRUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_NOTRUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_RUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        private async void ___FIRST_NOTRUNNINGAsync()
        {
            Invoke(new Action(() =>
            {
                panel3.BackColor = Color.FromArgb(0, 255, 0);
            }));

            try
            {
                string start_time = DateTime.Now.AddDays(-2).ToString("yyyy-MM-dd 00:00:00");
                string end_time = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");

                start_time = start_time.Replace("-", "%2F");
                start_time = start_time.Replace(" ", "+");
                start_time = start_time.Replace(":", "%3A");

                end_time = end_time.Replace("-", "%2F");
                end_time = end_time.Replace(" ", "+");
                end_time = end_time.Replace(":", "%3A");

                var cookieManager = Cef.GetGlobalCookieManager();
                var visitor = new CookieCollector();
                cookieManager.VisitUrlCookies("http://sports.wclub888.com", true, visitor);
                var cookies = await visitor.Task;
                var cookie = CookieCollector.GetCookieHeader(cookies);
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie", cookie);
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                int _epoch = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                byte[] result = await wc.DownloadDataTaskAsync("http://sports.wclub888.com/_View/RMOdds2Gen.aspx?ot=t&update=false&sa=false&tv=0&tf=-1&TFStatus=0&mt=0&r=342146139&t=" + _epoch + "&RId=0&_=" + _epoch);
                string responsebody = Encoding.UTF8.GetString(result);
                var deserializeObject = JsonConvert.DeserializeObject(responsebody);
                JObject _jo = JObject.Parse(deserializeObject.ToString());
                JToken _count = _jo.SelectToken("$.JSOdds");

                string password = __website_name + __running_01 + __api_key;
                byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                string token = BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
                string ref_match_id = "";

                if (_count.Count() > 0)
                {
                    string _last_ref_id = "";
                    int _row_no = 1;
                    for (int i = 0; i < _count.Count(); i++)
                    {
                        JToken LeagueName = _jo.SelectToken("$.JSOdds[" + i + "][7]").ToString();
                        string HomeScore = "0";
                        string AwayScore = "0";
                        string MatchTimeHalf = "";
                        string MatchStatus = "";
                        JToken MatchTimeMinute = "Upcoming";
                        JToken HomeTeamName = _jo.SelectToken("$.JSOdds[" + i + "][25]").ToString();
                        JToken AwayTeamName = _jo.SelectToken("$.JSOdds[" + i + "][27]").ToString();
                        string StatementDate = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                        JToken KickOffDateTime = _jo.SelectToken("$.JSOdds[" + i + "][16]").ToString();
                        DateTime KickOffDateTime_Replace = DateTime.ParseExact(KickOffDateTime.ToString(), "dd/MM HH:mm", CultureInfo.InvariantCulture);
                        KickOffDateTime = KickOffDateTime_Replace.ToString("yyyy-MM-dd HH:mm:ss");
                        JToken FTHDP = _jo.SelectToken("$.JSOdds[" + i + "][23]").ToString();
                        JToken FTH = _jo.SelectToken("$.JSOdds[" + i + "][35]").ToString().Trim().Replace(".", "");
                        string FTH_Replace = FTH.ToString().Replace("-", "");
                        if (FTH_Replace.Length == 1 && FTH_Replace != "0")
                        {
                            FTH = FTH + "0";
                        }
                        FTH = Convert.ToDecimal(FTH) / 100;
                        JToken FTA = _jo.SelectToken("$.JSOdds[" + i + "][36]").ToString().Trim().Replace(".", "");
                        string FTA_Replace = FTA.ToString().Replace("-", "");
                        if (FTA_Replace.Length == 1 && FTA_Replace != "0")
                        {
                            FTA = FTA + "0";
                        }
                        FTA = Convert.ToDecimal(FTA) / 100;
                        string BetIDFTOU = "";
                        JToken FTOU = _jo.SelectToken("$.JSOdds[" + i + "][41]").ToString();
                        JToken FTO = _jo.SelectToken("$.JSOdds[" + i + "][44]").ToString().Trim().Replace(".", "");
                        string FTO_Replace = FTO.ToString().Replace("-", "");
                        if (FTO_Replace.Length == 1 && FTO_Replace != "0")
                        {
                            FTO = FTO + "0";
                        }
                        FTO = Convert.ToDecimal(FTO) / 100;
                        JToken FTU = _jo.SelectToken("$.JSOdds[" + i + "][45]").ToString().Trim().Replace(".", "");
                        string FTU_Replace = FTU.ToString().Replace("-", "");
                        if (FTU_Replace.Length == 1 && FTU_Replace != "0")
                        {
                            FTU = FTU + "0";
                        }
                        FTU = Convert.ToDecimal(FTU) / 100;
                        JToken FT1 = _jo.SelectToken("$.JSOdds[" + i + "][49]").ToString();
                        JToken FT2 = _jo.SelectToken("$.JSOdds[" + i + "][50]").ToString();
                        JToken FTX = _jo.SelectToken("$.JSOdds[" + i + "][51]").ToString();
                        JToken FTOdd = _jo.SelectToken("$.JSOdds[" + i + "][54]").ToString().Trim().Replace(".", "");
                        string FTOdd_Replace = FTOdd.ToString().Replace("-", "");
                        if (FTOdd_Replace.Length == 1 && FTOdd_Replace != "0")
                        {
                            FTOdd = FTOdd + "0";
                        }
                        FTOdd = Convert.ToDecimal(FTOdd) / 100;
                        JToken FTEven = _jo.SelectToken("$.JSOdds[" + i + "][55]").ToString().Trim().Replace(".", "");
                        string FTEven_Replace = FTEven.ToString().Replace("-", "");
                        if (FTEven_Replace.Length == 1 && FTEven_Replace != "0")
                        {
                            FTEven = FTEven + "0";
                        }
                        FTEven = Convert.ToDecimal(FTEven) / 100;
                        string BetIDFTOE = "";
                        string BetIDFT1X2 = "";
                        string SpecialGame = "";
                        JToken FHHDP = _jo.SelectToken("$.JSOdds[" + i + "][59]").ToString();
                        JToken FHH = _jo.SelectToken("$.JSOdds[" + i + "][63]").ToString().Trim().Replace(".", "");
                        string FHH_Replace = FHH.ToString().Replace("-", "");
                        if (FHH_Replace.Length == 1 && FHH_Replace != "0")
                        {
                            FHH = FHH + "0";
                        }
                        FHH = Convert.ToDecimal(FHH) / 100;
                        JToken FHA = _jo.SelectToken("$.JSOdds[" + i + "][64]").ToString().Trim().Replace(".", "");
                        string FHA_Replace = FHA.ToString().Replace("-", "");
                        if (FHA_Replace.Length == 1 && FHA_Replace != "0")
                        {
                            FHA = FHA + "0";
                        }
                        FHA = Convert.ToDecimal(FHA) / 100;
                        JToken FHOU = _jo.SelectToken("$.JSOdds[" + i + "][67]").ToString();
                        JToken FHO = _jo.SelectToken("$.JSOdds[" + i + "][70]").ToString().Trim().Replace(".", "");
                        string FHO_Replace = FHO.ToString().Replace("-", "");
                        if (FHO_Replace.Length == 1 && FHO_Replace != "0")
                        {
                            FHO = FHO + "0";
                        }
                        FHO = Convert.ToDecimal(FHO) / 100;
                        JToken FHU = _jo.SelectToken("$.JSOdds[" + i + "][71]").ToString().Trim().Replace(".", "");
                        string FHU_Replace = FHU.ToString().Replace("-", "");
                        if (FHU_Replace.Length == 1 && FHU_Replace != "0")
                        {
                            FHU = FHU + "0";
                        }
                        FHU = Convert.ToDecimal(FHU) / 100;
                        JToken FH1 = _jo.SelectToken("$.JSOdds[" + i + "][75]").ToString();
                        JToken FH2 = _jo.SelectToken("$.JSOdds[" + i + "][76]").ToString();
                        JToken FHX = _jo.SelectToken("$.JSOdds[" + i + "][77]").ToString();

                        string ref_id_password = DateTime.Now.ToString("yyyy-MM-dd") + "3" + "Soccer" + LeagueName + HomeTeamName + AwayTeamName;
                        byte[] ref_id_encodedpassword = new UTF8Encoding().GetBytes(ref_id_password.Trim());
                        byte[] ref_hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(ref_id_encodedpassword);
                        string ref_token = BitConverter.ToString(ref_hash).Replace("-", string.Empty).ToLower().Substring(0, 8);
                        ref_match_id = ref_token;

                        if (ref_match_id == _last_ref_id)
                        {
                            _row_no++;
                        }
                        else
                        {
                            _row_no = 1;
                        }

                        _last_ref_id = ref_match_id;

                        if (HomeTeamName.ToString().ToLower().Contains("vs") || AwayTeamName.ToString().ToLower().Contains("vs"))
                        {
                            string[] replace = HomeTeamName.ToString().Split(new string[] { "Vs" }, StringSplitOptions.None);
                            HomeTeamName = replace[0].Trim();
                            AwayTeamName = replace[1].Trim();
                        }
                        else if (HomeTeamName.ToString().ToLower().Contains("+") || AwayTeamName.ToString().ToLower().Contains("+"))
                        {
                            string[] replace = HomeTeamName.ToString().Split('+');
                            HomeTeamName = replace[0].Trim();
                            AwayTeamName = replace[1].Trim();
                        }

                        var reqparm_ = new NameValueCollection
                        {
                            {"source_id", "3"},
                            {"sport_name", ""},
                            {"league_name", LeagueName.ToString().Trim()},
                            {"home_team", HomeTeamName.ToString().Trim()},
                            {"away_team", AwayTeamName.ToString().Trim()},
                            {"home_team_score", HomeScore.ToString()},
                            {"away_team_score", AwayScore.ToString()},
                            {"ref_match_id", ref_match_id},
                            {"odds_row_no", _row_no.ToString()},
                            {"fthdp", (FTHDP.ToString() != "") ? FTHDP.ToString() : "0"},
                            {"fth", (FTH.ToString() != "") ? FTH.ToString() : "0"},
                            {"fta", (FTA.ToString() != "") ? FTA.ToString() : "0"},
                            {"betidftou", (BetIDFTOU.ToString() != "") ? BetIDFTOU.ToString() : "0"},
                            {"ftou", (FTOU.ToString() != "") ? FTOU.ToString() : "0"},
                            {"fto", (FTO.ToString() != "") ? FTO.ToString() : "0"},
                            {"ftu", (FTU.ToString() != "") ? FTU.ToString() : "0"},
                            {"betidftoe", (BetIDFTOE.ToString() != "") ? BetIDFTOE.ToString() : "0"},
                            {"ftodd", (FTOdd.ToString() != "") ? FTOdd.ToString() : "0"},
                            {"fteven", (FTEven.ToString() != "") ? FTEven.ToString() : "0"},
                            {"betidft1x2", (BetIDFT1X2.ToString() != "") ? BetIDFT1X2.ToString() : "0"},
                            {"ft1", (FT1.ToString() != "") ? FT1.ToString() : "0"},
                            {"ftx", (FTX.ToString() != "") ? FTX.ToString() : "0"},
                            {"ft2", (FT2.ToString() != "") ? FT2.ToString() : "0"},
                            {"specialgame", (SpecialGame.ToString() != "") ? SpecialGame.ToString() : "0"},
                            {"fhhdp", (FHHDP.ToString() != "") ? FHHDP.ToString() : "0"},
                            {"fhh", (FHH.ToString() != "") ? FHH.ToString() : "0"},
                            {"fha", (FHA.ToString() != "") ? FHA.ToString() : "0"},
                            {"fhou", (FHOU.ToString() != "") ? FHOU.ToString() : "0"},
                            {"fho", (FHO.ToString() != "") ? FHO.ToString() : "0"},
                            {"fhu", (FHU.ToString() != "") ? FHU.ToString() : "0"},
                            {"fhodd", "0"},
                            {"fheven", "0"},
                            {"fh1", (FH1.ToString() != "") ? FH1.ToString() : "0"},
                            {"fhx", (FHX.ToString() != "") ? FHX.ToString() : "0"},
                            {"fh2", (FH2.ToString() != "") ? FH2.ToString() : "0"},
                            {"statement_date", StatementDate.ToString()},
                            {"kickoff_date", KickOffDateTime.ToString()},
                            {"match_time", "Upcoming"},
                            {"match_status", "N"},
                            {"match_minute", "0"},
                            {"api_status", "R"},
                            {"token_api", token},
                        };

                        try
                        {
                            WebClient wc_ = new WebClient();
                            byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                            string responsebody_ = Encoding.UTF8.GetString(result_);
                            __send = 0;
                        }
                        catch (Exception err)
                        {
                            __send++;

                            if (___CheckForInternetConnection())
                            {
                                if (__send == 5)
                                {
                                    SendMyBot(err.ToString());
                                    __is_close = false;
                                    Environment.Exit(0);
                                }
                                else
                                {
                                    await ___TaskWait_Handler(10);
                                    WebClient wc_ = new WebClient();
                                    byte[] result_ = wc_.UploadValues("http://oddsgrabber.ssitex.com/API/sendOdds", "POST", reqparm_);
                                    string responsebody_ = Encoding.UTF8.GetString(result_);
                                }
                            }
                            else
                            {
                                __is_close = false;
                                Environment.Exit(0);
                            }
                        }
                    }
                }

                if (!Properties.Settings.Default.______odds_iswaiting_01 && Properties.Settings.Default.______odds_issend_01)
                {
                    Properties.Settings.Default.______odds_issend_01 = false;
                    Properties.Settings.Default.Save();

                    SendABCTeam(__running_11 + " Back to Normal.");
                }

                Properties.Settings.Default.______odds_iswaiting_01 = false;
                Properties.Settings.Default.Save();

                Invoke(new Action(() =>
                {
                    panel3.BackColor = Color.FromArgb(16, 90, 101);
                }));

                __send = 0;
                await ___TaskWait();
                ___FIRST_RUNNINGAsync();
            }
            catch (Exception err)
            {
                if (___CheckForInternetConnection())
                {
                    __send++;
                    if (__send == 5)
                    {
                        Properties.Settings.Default.______odds_iswaiting_01 = true;
                        Properties.Settings.Default.Save();

                        if (!Properties.Settings.Default.______odds_issend_01)
                        {
                            Properties.Settings.Default.______odds_issend_01 = true;
                            Properties.Settings.Default.Save();
                            SendABCTeam(__running_11 + " Under Maintenance.");
                        }

                        ___FIRST_RUNNINGAsync();
                        SendMyBot(err.ToString());
                    }
                    else
                    {
                        await ___TaskWait_Handler(10);
                        ___FIRST_NOTRUNNINGAsync();
                    }
                }
                else
                {
                    __is_close = false;
                    Environment.Exit(0);
                }
            }
        }

        public static bool ___CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task ___TaskWait()
        {
            Random _random = new Random();
            int _random_number = _random.Next(10, 16);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        private async Task ___TaskWait_Handler(int sec)
        {
            sec++;
            Random _random = new Random();
            int _random_number = _random.Next(sec, sec);
            string _randowm_number_replace = _random_number.ToString() + "000";
            await Task.Delay(Convert.ToInt32(_randowm_number_replace));
        }

        public bool __is_numeric(string value)
        {
            return value.All(char.IsNumber);
        }

        private void __Flag()
        {
            string _flag = Path.Combine(Path.GetTempPath(), __app + " - " + __website_name + ".txt");
            using (StreamWriter sw = new StreamWriter(_flag, true))
            {
                sw.WriteLine("<<>>" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<<>>");
            }
        }
    }
}