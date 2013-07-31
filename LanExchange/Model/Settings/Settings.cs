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
        private const string WebSiteUrlDefault = "code.google.com/p/lanexchange/";
        private const string TwitterDefault = "@LanExchangeHere";
        private const string EmailAddressDefault = "skivsoft@gmail.com";

        /// <summary>
        /// Default width of MainForm.
        /// </summary>
        public const int MAINFORM_DEFAULTWIDTH = 450;

        private static Settings m_Instance;
        private static bool m_Modified;

        private bool m_RunMinimized;
        private bool m_AdvancedMode;

        private Settings()
        {
            m_RunMinimized = true;
            WMIClassesInclude = new List<string>();
            WMIClassesInclude.Add("Win32_Desktop");
            WMIClassesInclude.Add("Win32_DesktopMonitor");
            WMIClassesInclude.Add("Win32_DiskDrive");
            WMIClassesInclude.Add("Win32_BIOS");
            WMIClassesInclude.Add("Win32_Processor");
            WMIClassesInclude.Add("Win32_PhysicalMemory");
            Language = "en-US";
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
                m_Modified = value;
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
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Settings));
                if (temp != null)
                {
                    List_Distinct(temp.WMIClassesInclude);
                    m_Instance = null;
                    m_Instance = temp;
                    Modified = false;
                }
            }
            catch (Exception)
            {
            }
        }

        public static void SaveIfModified()
        {
            if (!Modified) return;
            var fileName = GetConfigFileName();
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(fileName, Instance);
            }
            catch (Exception)
            {
            }
            Modified = false;
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
                    AutorunUtils.Autorun_Add(exeFName);
                }
                else
                {
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

        public string GetWebSiteUrl()
        {
            return WebSiteUrlDefault;
        }

        public string GetTwitter()
        {
            return TwitterDefault;
        }

        public string GetEmailAddress()
        {
            return EmailAddressDefault;
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

        public string Language { get; set; }
    }
}
