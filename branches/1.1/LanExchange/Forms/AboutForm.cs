using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
#if NLOG
using NLog;
#endif

namespace LanExchange.Forms
{
    partial class AboutForm : Form
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private Control MsgControl = null;
        private string UpdateError = null;
        private bool bNeedRestart = false;
        private string FileListContent = null;
        
        public AboutForm()
        {
            InitializeComponent();
            this.Text = String.Format("О программе «{0}»", AssemblyProduct);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Версия {0}", AssemblyVersion);
            this.labelWeb.LinkArea = new LinkArea(this.labelWeb.Text.Length, this.labelWeb.LinkArea.Length);
            this.labelWeb.Text += GetWebSiteURL();
            this.labelEmail.LinkArea = new LinkArea(this.labelEmail.Text.Length, this.labelEmail.LinkArea.Length);
            this.labelEmail.Text += GetEmailAddress();
            this.labelCopyright.Text = AssemblyCopyright;
        }

        public static string GetUpdateBaseURL()
        {
            return Settings.GetInstance().GetStrValue("UpdateURL", "http://skivsoft.net/lanexchange/update/");
        }

        public static string GetFileListURL()
        {
            return GetUpdateBaseURL() + "filelist.php";
        }

        public static string GetWebSiteURL()
        {
            return Settings.GetInstance().GetStrValue("Web", "skivsoft.net/lanexchange/");
        }

        public static string GetEmailAddress()
        {
            return Settings.GetInstance().GetStrValue("Email", "skivsoft@gmail.com");
        }

        void HideMessage()
        {
            if (MsgControl != null)
            {
                tableLayoutPanel.Controls.Remove(MsgControl);
                MsgControl.Dispose();
                MsgControl = null;
            }
        }

        void ShowMessage(string text, Color color)
        {
            HideMessage();
            Label label = new Label();
            label.AutoSize = true;
            label.Text = text;
            label.ForeColor = color;
            label.Font = new Font(label.Font, FontStyle.Italic);
            tableLayoutPanel.Controls.Add(label, 1, 2);
            tableLayoutPanel.SetColumnSpan(label, 2);
            MsgControl = label;
        }


        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void AboutForm_Load(object sender, EventArgs e)
        {
            ShowMessage("Проверка обновлений...", Color.Gray);
            if (!DoCheckVersion.IsBusy)
                DoCheckVersion.RunWorkerAsync();
        }

        private void AboutForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                e.Handled = true;
            }
        }

        private void DoCheckVersion_DoWork(object sender, DoWorkEventArgs e)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.Proxy = null;
                    string URL = GetFileListURL();
                    logger.Info("Downloading text from url [{0}]", URL);
                    e.Result = client.DownloadString(URL);
                }
                catch(Exception ex)
                {
                    logger.ErrorException("DoCheckVersion", ex);
                    e.Cancel = true;
                }
            }
        }

        private void DoCheckVersion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ShowMessage("Не удалось подключиться к серверу обновлений.", Color.Red);
                return;
            }
            FileListContent = (string)e.Result;
            StringReader Reader = new StringReader(FileListContent);
            Version siteVersion = new Version(Reader.ReadLine());
            Assembly assembly = Assembly.GetExecutingAssembly();
            if (assembly.GetName().Version.CompareTo(siteVersion) < 0)
            {
                Label L = new Label();
                Button B = new Button();
                B.AutoSize = true;
                B.Text = String.Format("Обновить до версии {0}", siteVersion.ToString());
                B.Click += new System.EventHandler(this.UpdateButton_Click);
                HideMessage();
                tableLayoutPanel.Controls.Add(B, 1, 2);
                MsgControl = B;
            }
            else
                ShowMessage("Установлена последняя версия LanExchange.", Color.Gray);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            // рисуем прогресс обновления
            ProgressBar Progress = new ProgressBar();
            Progress.Width = MsgControl.Width;
            Progress.Style = ProgressBarStyle.Marquee;
            HideMessage();
            tableLayoutPanel.Controls.Add(Progress, 1, 3);
            Progress.Update();
            MsgControl = Progress;
            if (!DoUpdate.IsBusy)
                DoUpdate.RunWorkerAsync();
        }
        
        private bool verifyMd5File(string LocalFName, int RemoteFSize, string RemoteMD5)
        {
            bool Result;
            using (FileStream FS = File.Open(LocalFName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (FS.Length == RemoteFSize)
                {
                    byte[] content = new byte[FS.Length];
                    FS.Read(content, 0, (int)FS.Length);

                    MD5 md5Hasher = MD5.Create();
                    byte[] data = md5Hasher.ComputeHash(content);
                    StringBuilder sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                    string hashOfInput = sBuilder.ToString();

                    StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                    Result = (0 == comparer.Compare(hashOfInput, RemoteMD5));
                }
                else
                    Result = false;
                FS.Close();
            }
            return Result;
        }

        private void DoUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                WebClient client = new WebClient();
                client.Proxy = null;
                StringReader StrReader = new StringReader(FileListContent);
                StrReader.ReadLine();
                string line;
                string LocalFName;
                string LocalDirName;
                string[] Arr;
                string ExeName = Application.ExecutablePath;
                string ExePath = Path.GetDirectoryName(ExeName);

                while (!String.IsNullOrEmpty(line = StrReader.ReadLine()))
                {
                    Arr = line.Split('|');
                    string RemoteMD5 = Arr[0];
                    int RemoteFSize = Int32.Parse(Arr[1]);
                    string RemoteFName = Arr[2];
                    string MustChangeFName = Arr[3];

                    LocalFName = Path.Combine(ExePath, MustChangeFName);
                    LocalDirName = Path.GetDirectoryName(LocalFName);
                    bool bNeedDownload = false;
                    if (File.Exists(LocalFName))
                    {
                        bool verify = verifyMd5File(LocalFName, RemoteFSize, RemoteMD5);
                        if (!verify)
                            bNeedDownload = true;
                    }
                    else
                        bNeedDownload = true;
                    if (bNeedDownload)
                    {
                        if (LocalFName.Equals(ExeName))
                        {
                            string FName = Path.ChangeExtension(LocalFName, ".old.exe");
                            if (File.Exists(FName))
                                File.Delete(FName);
                            File.Move(LocalFName, FName);
                            bNeedRestart = true;
                        }
                        else
                            if (File.Exists(LocalFName))
                                File.Delete(LocalFName);
                            else
                                if (!Directory.Exists(LocalDirName))
                                    Directory.CreateDirectory(LocalDirName);
                        string URL = GetUpdateBaseURL() + RemoteFName;
                        logger.Info("Downloading file from url [{0}] and saving to [{1}]", URL, LocalFName);
                        client.DownloadFile(URL, LocalFName);
                    }
                }
            }
            catch(Exception ex)
            {
                e.Cancel = true;
                UpdateError = ex.Message;
                logger.Info("Error: ", ex.Message);
            }
        }

        private void DoUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                ShowMessage("Обновление не удалось: "+UpdateError, Color.Red);
            }
            else
            {
                ShowMessage("Обвновление успешно завершено.", Color.Gray);
                if (bNeedRestart)
                {
                    // закрываем окно
                    DialogResult = DialogResult.Cancel;
                    // перезапуск приложения
                    Application.Restart();
                }
            }
        }

        private void labelWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://" + GetWebSiteURL());
        }

        private void labelEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:" + GetEmailAddress());
        }
    }
}
