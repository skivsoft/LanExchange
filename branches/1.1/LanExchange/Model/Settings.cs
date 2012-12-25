using System;
using System.IO;
using LanExchange.Utils;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Principal;

namespace LanExchange.Model
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    public class Settings
    {
        private const string UpdateURL_Default = "http://skivsoft.net/lanexchange/update/";
        private const string WebSiteURL_Default = "skivsoft.net/lanexchange/";
        private const string EmailAddress_Default = "skivsoft@gmail.com";

        private static Settings m_Instance;

        public Settings()
        {
            RefreshTimeInSec = 5 * 60;

        }

        public static Settings Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    var temp = new Settings();
                    m_Instance = temp;
                }
                return m_Instance;
            }
        }

        public static void LoadSettings()
        {
            try
            {
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXMLFile(GetConfigFileName(), typeof(Settings));
                m_Instance = null;
                m_Instance = temp;
            }
            catch { }
        }

        public static void SaveSettings()
        {
            SerializeUtils.SerializeTypeToXMLFile(GetConfigFileName(), Instance);
        }

        public static string GetExecutableFileName()
        {
            string[] Params = Environment.GetCommandLineArgs();
            return Params[0];
        }

        public static string GetConfigFileName()
        {
            return Path.ChangeExtension(GetExecutableFileName(), ".cfg");
        }

        public static bool IsAutorun
        {
            get
            {
                return AutorunUtils.Autorun_Exists(GetExecutableFileName());
            }
            set
            {
                string ExeFName = GetExecutableFileName();
                if (value)
                    AutorunUtils.Autorun_Add(ExeFName);
                else
                    AutorunUtils.Autorun_Delete(ExeFName);
            }
        }

        public int SelectedIndex { get; set; }

        public bool RunMinimized { get; set; }

        public bool AdvancedMode { get; set; }

        public decimal RefreshTimeInSec { get; set; }

        public string UpdateURL { get; set; }

        public string WebSiteURL { get; set; }

        public string EmailAddress { get; set; }

        public string GetUpdateURL()
        {
            return String.IsNullOrEmpty(UpdateURL) ? UpdateURL_Default : UpdateURL;
        }

        public string GetWebSiteURL()
        {
            return String.IsNullOrEmpty(WebSiteURL) ? WebSiteURL_Default : WebSiteURL;
        }

        public string GetEmailAddress()
        {
            return String.IsNullOrEmpty(EmailAddress) ? EmailAddress_Default : EmailAddress;
        }

        public static string GetCurrentUserName()
        {
            var user = WindowsIdentity.GetCurrent();
            string[] A = user.Name.Split('\\');
            return A.Length > 1 ? A[1] : A[0];
        }
    }
}
