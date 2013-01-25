using System;
using System.IO;
using LanExchange.Utils;
using System.Security.Principal;
using System.Collections.Generic;

namespace LanExchange.Model.Settings
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    public class Settings
    {
        private const string UpdateUrlDefault = "http://www.skivsoft.net/lanexchange/update/";
        private const string WebSiteUrlDefault = "www.skivsoft.net/lanexchange/";
        private const string EmailAddressDefault = "skivsoft@gmail.com";

        /// <summary>
        /// Default width of MainForm.
        /// </summary>
        public const int MAINFORM_DEFAULTWIDTH = 450;

        private static Settings m_Instance;
        private static bool m_Modified;

        private bool m_RunMinimized;
        private bool m_AdvancedMode;
        private decimal m_RefreshTimeInSec;
        private bool m_ShowHiddenShares;
        private bool m_ShowPrinters;

        private Settings()
        {
            m_RunMinimized = true;
            m_RefreshTimeInSec = 5 * 60;
            WMIClassesInclude = new List<string>();
            WMIClassesInclude.Add("Win32_Desktop");
            WMIClassesInclude.Add("Win32_DesktopMonitor");
            WMIClassesInclude.Add("Win32_DiskDrive");
            WMIClassesInclude.Add("Win32_BIOS");
            WMIClassesInclude.Add("Win32_Processor");
            WMIClassesInclude.Add("Win32_PhysicalMemory");
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

        private static bool Modified
        {
            get { return m_Modified; }
            set
            {
                if (m_Modified != value)
                {
                    LogUtils.Info("Settings.Modified = {0}", value);
                    m_Modified = value;
                }
            }
        }

        /// <summary>
        /// Remove duplicates from list.
        /// </summary>
        /// <param name="list">a list of strings</param>
        public static void List_Distinct(List<string> list)
        {
            list.Sort();
            Int32 index = 0;
            while (index < list.Count - 1)
            {
                if (list[index] == list[index + 1])
                    list.RemoveAt(index);
                else
                    index++;
            } 
        }

        public static void Load()
        {
            var fileName = GetConfigFileName();
            if (!File.Exists(fileName)) return;
            try
            {
                LogUtils.Info("Settings.Load(\"{0}\")", fileName);
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Settings));
                if (temp != null)
                {
                    List_Distinct(temp.WMIClassesInclude);
                    m_Instance = null;
                    m_Instance = temp;
                    Modified = false;
                }
            }
            catch (Exception E)
            {
                LogUtils.Error("Settings: {0}", E.Message);
            }
        }

        public static void SaveIfModified()
        {
            if (!Modified) return;
            var fileName = GetConfigFileName();
            try
            {
                LogUtils.Info("Settings.Save(\"{0}\")", fileName);
                SerializeUtils.SerializeObjectToXMLFile(fileName, Instance);
            }
            catch (Exception E)
            {
                LogUtils.Error("Settings: {0}", E.Message);
            }
            Modified = false;
        }

        public static bool Merge(string newConfigContent)
        {
            bool result = false;
            try
            {
                LogUtils.Info("Settings.Merge()");
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXML(newConfigContent, typeof(Settings));
                if (temp != null)
                {
                    Instance.SetEmailAddress(temp.EmailAddress);
                    Instance.SetWebSiteURL(temp.WebSiteURL);
                    Instance.SetUpdateURL(temp.UpdateURL);
                    result = Modified;
                    SaveIfModified();
                }
            }
            catch (Exception E)
            {
                LogUtils.Error("Settings: {0}", E.Message);
            }
            return result;
        }

        public static string GetExecutableFileName()
        {
            var Params = Environment.GetCommandLineArgs();
            return Params.Length > 0 ? Params[0] : String.Empty;
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
                var exeFName = GetExecutableFileName();
                if (value)
                {
                    LogUtils.Info("Settings.Autorun_Add()");
                    AutorunUtils.Autorun_Add(exeFName);
                }
                else
                {
                    LogUtils.Info("Settings.Autorun_Delete()");
                    AutorunUtils.Autorun_Delete(exeFName);
                }
            }
        }

        public bool RunMinimized
        {
            get { return m_RunMinimized; }
            set
            {
                if (m_RunMinimized != value)
                {
                    m_RunMinimized = value;
                    Modified = true;
                }
            }
        }

        public bool ShowHiddenShares
        {
            get { return m_ShowHiddenShares; }
            set
            {
                if (m_ShowHiddenShares != value)
                {
                    m_ShowHiddenShares = value;
                    Modified = true;
                }
            }
        }

        public bool ShowPrinters
        {
            get { return m_ShowPrinters; }
            set
            {
                if (m_ShowPrinters != value)
                {
                    m_ShowPrinters = value;
                    Modified = true;
                }
            }
        }

        public bool AdvancedMode
        {
            get { return m_AdvancedMode; }
            set
            {
                if (m_AdvancedMode != value)
                {
                    m_AdvancedMode = value;
                    Modified = true;
                }
            }
        }

        public decimal RefreshTimeInSec 
        {
            get { return m_RefreshTimeInSec; }
            set
            {
                if (m_RefreshTimeInSec != value)
                {
                    m_RefreshTimeInSec = value;
                    Modified = true;
                }
            }
        }

        public string GetUpdateUrl()
        {
            return String.IsNullOrEmpty(UpdateURL) ? UpdateUrlDefault : UpdateURL;
        }

        public void SetUpdateURL(string value)
        {
            if (GetUpdateUrl() != value)
            {
                UpdateURL = value;
                Modified = true;
            }
        }

        public string GetWebSiteUrl()
        {
            return String.IsNullOrEmpty(WebSiteURL) ? WebSiteUrlDefault : WebSiteURL;
        }

        public void SetWebSiteURL(string value)
        {
            if (GetWebSiteUrl() != value)
            {
                WebSiteURL = value;
                Modified = true;
            }
        }

        public string GetEmailAddress()
        {
            return String.IsNullOrEmpty(EmailAddress) ? EmailAddressDefault : EmailAddress;
        }

        public void SetEmailAddress(string value)
        {
            if (GetEmailAddress() != value)
            {
                EmailAddress = value;
                Modified = true;
            }
        }

        public static string GetCurrentUserName()
        {
            var user = WindowsIdentity.GetCurrent();
            if (user != null)
            {
                string[] A = user.Name.Split('\\');
                return A.Length > 1 ? A[1] : A[0];
            }
            return String.Empty;
        }

        // properties below must not be modified instantly

        public int MainFormX { get; set; }
        public int MainFormWidth { get; set; }

        public List<string> WMIClassesInclude { get; set; }

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public string UpdateURL { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public string WebSiteURL { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        // ReSharper disable UnusedAutoPropertyAccessor.Global
        public string EmailAddress { get; set; }
        // ReSharper restore UnusedAutoPropertyAccessor.Global

    }
}
