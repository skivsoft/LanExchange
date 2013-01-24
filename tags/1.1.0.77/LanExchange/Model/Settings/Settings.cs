using System;
using System.IO;
using LanExchange.Utils;
using System.Security.Principal;
using NLog;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LanExchange.Model.Settings
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    public class Settings
    {
        public readonly static Logger Logger = LogManager.GetCurrentClassLogger();
        private const string UpdateUrlDefault = "http://www.skivsoft.net/lanexchange/update/";
        private const string WebSiteUrlDefault = "www.skivsoft.net/lanexchange/";
        private const string EmailAddressDefault = "skivsoft@gmail.com";

        /// <summary>
        /// Default width of MainForm.
        /// </summary>
        private const int MAINFORM_DEFAULTWIDTH = 450;

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
                    Logger.Info("Settings.Modified = {0}", value);
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
                Logger.Info("Settings.Load(\"{0}\")", fileName);
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
                Logger.Error("Settings: {0}", E.Message);
            }
        }

        public static void SaveIfModified()
        {
            if (!Modified) return;
            var fileName = GetConfigFileName();
            try
            {
                Logger.Info("Settings.Save(\"{0}\")", fileName);
                SerializeUtils.SerializeObjectToXMLFile(fileName, Instance);
            }
            catch (Exception E)
            {
                Logger.Error("Settings: {0}", E.Message);
            }
            Modified = false;
        }

        internal static bool Merge(string newConfigContent)
        {
            bool result = false;
            try
            {
                Logger.Info("Settings.Merge()");
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
                Logger.Error("Settings: {0}", E.Message);
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
                    Logger.Info("Settings.Autorun_Add()");
                    AutorunUtils.Autorun_Add(exeFName);
                }
                else
                {
                    Logger.Info("Settings.Autorun_Delete()");
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

        public Rectangle GetBounds()
        {
            Logger.Info("Settings: MainFormX: {0}, MainFormWidth: {1}", MainFormX, MainFormWidth);
            // correct width and height
            bool BoundsIsNotSet = MainFormWidth == 0;
            Rectangle WorkingArea;
            if (BoundsIsNotSet)
                WorkingArea = Screen.PrimaryScreen.WorkingArea;
            else
                WorkingArea = Screen.GetWorkingArea(new Point(MainFormX + MainFormWidth/2, 0));
            var rect = new Rectangle();
            rect.X = MainFormX;
            rect.Y = WorkingArea.Top;
            rect.Width = Math.Min(Math.Max(MAINFORM_DEFAULTWIDTH, MainFormWidth), WorkingArea.Width);
            rect.Height = WorkingArea.Height;
            // determination side to snap right or left
            int CenterX = (rect.Left + rect.Right) >> 1;
            int WorkingAreaCenterX = (WorkingArea.Left + WorkingArea.Right) >> 1;
            if (BoundsIsNotSet || CenterX >= WorkingAreaCenterX)
                // snap to right side
                rect.X = WorkingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - WorkingArea.Left;
            Logger.Info("Settings.GetBounds(): {0}, {1}, {2}, {3}", rect.Left, rect.Top, rect.Width, rect.Height);
            return rect;
        }

        public void SetBounds(Rectangle rect)
        {
            Rectangle WorkingArea = Screen.GetWorkingArea(rect);
            // shift rect into working area
            if (rect.Left < WorkingArea.Left) rect.X = WorkingArea.Left;
            if (rect.Top < WorkingArea.Top) rect.Y = WorkingArea.Top;
            if (rect.Right > WorkingArea.Right) rect.X -= rect.Right - WorkingArea.Right;
            if (rect.Bottom > WorkingArea.Bottom) rect.Y -= rect.Bottom - WorkingArea.Bottom;
            // determination side to snap right or left
            int CenterX = (rect.Left + rect.Right) >> 1;
            int WorkingAreaCenterX = (WorkingArea.Left + WorkingArea.Right) >> 1;
            if (CenterX >= WorkingAreaCenterX)
                // snap to right side
                rect.X = WorkingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - WorkingArea.Left;
            // set properties
            if (rect.Left != MainFormX || rect.Width != MainFormWidth)
            {
                Logger.Info("Settings.SetBounds(): {0}, {1}, {2}, {3}", rect.Left, rect.Top, rect.Width, rect.Height);
                MainFormX = rect.Left;
                MainFormWidth = rect.Width;
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
