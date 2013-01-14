using System;
using System.IO;
using LanExchange.Utils;
using System.Security.Principal;
using NLog;
using System.Drawing;
using System.Windows.Forms;

namespace LanExchange.Model
{
    /// <summary>
    /// Program settings. Implemented as Singleton.
    /// </summary>
    public class Settings
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private const string UpdateUrlDefault = "http://www.skivsoft.net/lanexchange/update/";
        private const string WebSiteUrlDefault = "www.skivsoft.net/lanexchange/";
        private const string EmailAddressDefault = "skivsoft@gmail.com";

        /// <summary>
        /// Default width of MainForm.
        /// </summary>
        private const int MAINFORM_DEFAULTWIDTH = 450;

        private static Settings m_Instance;
        private bool m_Modified;
        private bool m_RunMinimized;
        private bool m_AdvancedMode;
        private decimal m_RefreshTimeInSec;

        public Settings()
        {
            RunMinimized = true;
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
            var fileName = GetConfigFileName();
            if (!File.Exists(fileName)) return;
            try
            {
                logger.Info("LoadSettings(\"{0}\")", fileName);
                var temp = (Settings)SerializeUtils.DeserializeObjectFromXMLFile(fileName, typeof(Settings));
                if (temp != null)
                {
                    m_Instance = null;
                    m_Instance = temp;
                }
            }
            catch (Exception E)
            {
                logger.Error("LoadSettings: {0}", E.Message);
            }
        }

        public static void SaveSettingsIfModified()
        {
            if (!Instance.m_Modified) return;
            var fileName = GetConfigFileName();
            try
            {
                logger.Info("SaveSettings(\"{0}\")", fileName);
                SerializeUtils.SerializeObjectToXMLFile(fileName, Instance);
            }
            catch (Exception E)
            {
                logger.Error("SaveSettings: {0}", E.Message);
            }
            Instance.m_Modified = false;
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
                    AutorunUtils.Autorun_Add(exeFName);
                else
                    AutorunUtils.Autorun_Delete(exeFName);
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
                    m_Modified = true;
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
                    m_Modified = true;
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
                    m_Modified = true;
                }
            }
        }

        public Rectangle GetBounds()
        {
            logger.Info("MainFormX: {0}, MainFormWidth: {1}", MainFormX, MainFormWidth);
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
            logger.Info("Settings.GetBounds(): {0}, {1}, {2}, {3}", rect.Left, rect.Top, rect.Width, rect.Height);
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
                logger.Info("Settings.SetBounds(): {0}, {1}, {2}, {3}", rect.Left, rect.Top, rect.Width, rect.Height);
                MainFormX = rect.Left;
                MainFormWidth = rect.Width;
                m_Modified = true;
            }
        }

        public string GetUpdateUrl()
        {
            return String.IsNullOrEmpty(UpdateUrl) ? UpdateUrlDefault : UpdateUrl;
        }

        public string GetWebSiteUrl()
        {
            return String.IsNullOrEmpty(WebSiteUrl) ? WebSiteUrlDefault : WebSiteUrl;
        }

        public string GetEmailAddress()
        {
            return String.IsNullOrEmpty(EmailAddress) ? EmailAddressDefault : EmailAddress;
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
        public string UpdateUrl { get; set; }
        public string WebSiteUrl { get; set; }
        public string EmailAddress { get; set; }
    }
}
