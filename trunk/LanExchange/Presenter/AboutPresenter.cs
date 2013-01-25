using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using LanExchange.Interface;
using LanExchange.Model.Settings;
using LanExchange.Utils;

namespace LanExchange.Presenter
{
    /// <summary>
    /// Presenter for Settings (model) and AboutForm (view).
    /// </summary>
    public sealed class AboutPresenter : IPresenter, IDisposable
    {
        public static bool m_NeedRestart;

        private readonly IAboutView m_View;
        // TODO need re-implement workers, must use BackgroundWorkers singleton
        private readonly BackgroundWorker m_DoCheckVersion;
        private readonly BackgroundWorker m_DoUpdate;

        private string m_UpdateError;
        private string m_FileListContent;


        public AboutPresenter(IAboutView view)
        {
            // initialize background workers
            m_DoCheckVersion = new BackgroundWorker();
            m_DoCheckVersion.DoWork += DoCheckVersion_DoWork;
            m_DoCheckVersion.RunWorkerCompleted += DoCheckVersion_RunWorkerCompleted;
            m_DoUpdate = new BackgroundWorker();
            m_DoUpdate.DoWork += DoUpdate_DoWork;
            m_DoUpdate.RunWorkerCompleted += DoUpdate_RunWorkerCompleted;

            m_View = view;
            m_View.ShowMessage("Проверка обновлений...", Color.Gray);
            if (!m_DoCheckVersion.IsBusy)
                m_DoCheckVersion.RunWorkerAsync();
        }

        public static bool NeedRestart
        {
            get { return m_NeedRestart; }
            set
            {
                if (m_NeedRestart != value)
                {
                    LogUtils.Info("Updater: Restart={0}", value);
                    m_NeedRestart = value;
                }
            }
        }

        public void Dispose()
        {
            m_DoCheckVersion.Dispose();
            m_DoUpdate.Dispose();
        }

        public void LoadFromModel()
        {
            m_View.WebText += Settings.Instance.GetWebSiteUrl();
            m_View.EmailText += Settings.Instance.GetEmailAddress();
        }

        public void StartUpdate()
        {
            m_View.ShowProgressBar();
            if (!m_DoUpdate.IsBusy)
                m_DoUpdate.RunWorkerAsync();
        }

        public static void OpenWebLink()
        {
            Process.Start("http://" + Settings.Instance.GetWebSiteUrl());
        }

        public static void OpenEmailLink()
        {
            Process.Start("mailto:" + Settings.Instance.GetEmailAddress());
        }

        public static string GetFileListURL()
        {
            return Settings.Instance.GetUpdateUrl() + "filelist.php";
        }

        private static void DoCheckVersion_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var client = new WebClient())
            {
                try
                {
                    client.Proxy = null;
                    string URL = GetFileListURL();
                    LogUtils.Info("Updater: downloading text from url [{0}]", URL);
                    e.Result = client.DownloadString(URL);
                }
                catch (Exception ex)
                {
                    LogUtils.Error("DoCheckVersion_DoWork() {0}", ex.Message);
                    e.Cancel = true;
                }
            }
        }

        private void DoCheckVersion_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                m_View.ShowMessage("Не удалось подключиться к серверу обновлений.", Color.Red);
                return;
            }
            m_FileListContent = (string)e.Result;
            using (var Reader = new StringReader(m_FileListContent))
            {
                var line = Reader.ReadLine();
                if (line != null)
                {
                    var siteVersion = new Version(line);
                    var assembly = Assembly.GetEntryAssembly();
                    if (assembly.GetName().Version.CompareTo(siteVersion) < 0)
                        m_View.ShowUpdateButton(siteVersion);
                    else
                        m_View.ShowMessage("Установлена последняя версия LanExchange.", Color.Gray);
                }
            }
        }

        private void DoUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Cancel = true;
            string ExeName = Settings.GetExecutableFileName();
            string ExePath = Path.GetDirectoryName(ExeName);
            if (ExePath == null) return;
            string newSettingsContent = String.Empty;
            string localConfigFName = Settings.GetConfigFileName();
            try
            {
                using (var client = new WebClient { Proxy = null })
                {
                    using (var StrReader = new StringReader(m_FileListContent))
                    {
                        StrReader.ReadLine();
                        string line;
                        while (!String.IsNullOrEmpty(line = StrReader.ReadLine()))
                        {
                            string[] Arr = line.Split('|');
                            string remoteMd5 = Arr[0];
                            int RemoteFSize = Int32.Parse(Arr[1]);
                            string RemoteFName = Arr[2];
                            string MustChangeFName = Arr[3];
                            string LocalFName = Path.Combine(ExePath, MustChangeFName);
                            string LocalDirName = Path.GetDirectoryName(LocalFName);
                            if (LocalDirName == null) continue;
                            bool bNeedDownload = false;
                            bool IsMainConfig = String.Compare(LocalFName, localConfigFName, StringComparison.OrdinalIgnoreCase) == 0;
                            if (File.Exists(LocalFName))
                            {
                                bool verify = verifyMd5File(LocalFName, RemoteFSize, remoteMd5);
                                if (!verify)
                                {
                                    LogUtils.Info("Updater: {0}", LocalFName);
                                    bNeedDownload = true;
                                }
                            }
                            else
                                bNeedDownload = true;
                            if (bNeedDownload)
                            {
                                if (File.Exists(LocalFName))
                                {
                                    bool bNeedRename = false;
                                    try
                                    {
                                        // delete local file unless it config-file
                                        if (!IsMainConfig)
                                            File.Delete(LocalFName);
                                    }
                                    catch (UnauthorizedAccessException)
                                    {
                                        LogUtils.Error("Updater: unable to delete file {0}", LocalFName);
                                        bNeedRename = true;
                                    }
                                    if (bNeedRename)
                                    {
                                        string FName = Path.ChangeExtension(LocalFName, Path.GetExtension(LocalFName) + ".tmp");
                                        LogUtils.Info("Updater: renaming file to {0}", FName);
                                        if (File.Exists(FName))
                                            File.Delete(FName);
                                        File.Move(LocalFName, FName);
                                        NeedRestart = true;
                                    }
                                }
                                else if (!Directory.Exists(LocalDirName))
                                    Directory.CreateDirectory(LocalDirName);
                                string URL = Settings.Instance.GetUpdateUrl() + RemoteFName;
                                if (IsMainConfig)
                                {
                                    LogUtils.Info("Updater: downloading new settings from [{0}]", URL);
                                    newSettingsContent = client.DownloadString(URL);
                                }
                                else
                                {
                                    LogUtils.Info("Updater: downloading from [{0}] to [{1}]", URL, LocalFName);
                                    client.DownloadFile(URL, LocalFName);
                                }
                            }
                        }
                    }
                }
                if (!String.IsNullOrEmpty(newSettingsContent))
                {
                    if (Settings.Merge(newSettingsContent))
                        NeedRestart = true;
                }
                e.Cancel = false;
            }
            catch (Exception ex)
            {
                m_UpdateError = ex.Message;
                LogUtils.Info("Updater: ", ex.Message);
            }
        }

        private void DoUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                m_View.ShowMessage("Обновление не удалось: " + m_UpdateError, Color.Red);
            }
            else
            {
                m_View.ShowMessage("Обвновление успешно завершено.", Color.Gray);
                if (NeedRestart)
                {
                    m_View.CancelView();
                    Application.Exit();
                }
            }
        }

        private static bool verifyMd5File(string localFName, int remoteFSize, string remoteMd5)
        {
            bool Result;
            using (var FS = File.Open(localFName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (FS.Length == remoteFSize)
                {
                    byte[] content = new byte[FS.Length];
                    FS.Read(content, 0, (int)FS.Length);

                    var md5Hasher = MD5.Create();
                    byte[] data = md5Hasher.ComputeHash(content);
                    var sBuilder = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                        sBuilder.Append(data[i].ToString("x2"));
                    string hashOfInput = sBuilder.ToString();

                    var comparer = StringComparer.OrdinalIgnoreCase;
                    Result = (0 == comparer.Compare(hashOfInput, remoteMd5));
                }
                else
                    Result = false;
            }
            return Result;
        }
    }
}
