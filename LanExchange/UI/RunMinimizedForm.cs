using System.Windows.Forms;
using NLog;
using System;

namespace LanExchange.UI
{
    /// <summary>
    /// We must not use ShowInTaskbar=false for MainForm. 
    /// This is need for correctly work system-wide hot-keys in future.
    /// </summary>
    public class RunMinimizedForm : Form
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private FormWindowState m_LastWindowState;
        private bool m_RunMinimized;
        private bool m_AllowVisible;
        private bool m_AllowClose;
        private bool m_ResizeBegan;

        protected RunMinimizedForm()
        {
            Resize += Form_Resize;
            ResizeBegin += (sender, args) => m_ResizeBegan = true;
            ResizeEnd += (sender, args) => m_ResizeBegan = false;
        }

        protected override void SetVisibleCore(bool value)
        {
            if (m_RunMinimized && !m_AllowVisible)
            {
                value = false;
                m_AllowVisible = true;
            }
            base.SetVisibleCore(value);
            if (value && WindowState == FormWindowState.Minimized)
                WindowState = m_LastWindowState;
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

        protected void SetRunMinimized(bool value)
        {
            m_RunMinimized = value;
            logger.Info(value ? "RunMinimized is ON" : "RunMinimized is OFF");
        }

        protected void ApplicationExit()
        {
            m_AllowClose = true;
            Application.Exit();
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            if (m_ResizeBegan) return;

            if (WindowState != FormWindowState.Minimized)
            {
                if (m_LastWindowState != WindowState)
                    logger.Info("WindowState is {0}", WindowState.ToString());
                m_LastWindowState = WindowState;
            }
            else
            {
                logger.Info("WindowState is {0}", WindowState.ToString());
                Visible = false;
            }
        }

        protected void ToggleVisible()
        {
            Visible = !Visible;
            if (Visible) Activate();
        }
    }
}
