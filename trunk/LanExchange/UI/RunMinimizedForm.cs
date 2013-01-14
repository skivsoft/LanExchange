using System.Windows.Forms;
using NLog;

namespace LanExchange.UI
{
    public partial class RunMinimizedForm : Form
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private bool m_RunMinimized;
        /// <summary>
        /// Last window state for correct show/hide MainForm in IsFormVisible.
        /// </summary>
        //private FormWindowState LastWindowState;

        private bool m_AllowVisible;     // ContextMenu's Show command used
        private bool m_AllowClose;       // ContextMenu's Exit command used
        //private bool m_LoadFired;        // Form was shown once

        public RunMinimizedForm()
        {

        }

        protected override void SetVisibleCore(bool value)
        {
            logger.Info("SetVisibleCore({0})", value);
            if (m_RunMinimized && !m_AllowVisible)
                value = false;
            base.SetVisibleCore(value);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            logger.Info("OnFormClosing({0})", e.CloseReason);
            if (!m_AllowClose)
            {
                Visible = false;
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        protected bool RunMinimized
        {
            get { return m_RunMinimized; }
            set
            {
                m_RunMinimized = value;
                if (value)
                    logger.Info("RunMinimized is ON");
                else
                    logger.Info("RunMinimized is OFF");
            }
        }

        /// <summary>
        /// We must not use ShowInTaskbar=false for MainForm. 
        /// This is need for correctly work system-wide hot-keys in future.
        /// </summary>
        public bool IsFormVisible
        {
            get
            {
                return Visible;
                //return WindowState != FormWindowState.Minimized && Visible; 
            }
            set
            {
                m_AllowVisible = true;
                //m_LoadFired = true;
                Visible = value;
                if (value)
                    Activate();
                /*
                bool bMinimized = WindowState == FormWindowState.Minimized;
                Visible = value;
                if (bMinimized)
                {
                    //TODO uncomment or del
                    //ShowInTaskbar = true;
                    WindowState = LastWindowState;
                }
                if (Visible)
                {
                    Activate();
                    Pages.FocusPanelView();
                }
                 */
            }
        }

        public void ApplicationExit()
        {
            m_AllowClose = true;
            Application.Exit();
        }

    }
}
