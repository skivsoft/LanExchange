using System;
using System.Windows.Forms;

namespace LanExchange.Plugin.WinForms.Forms
{
    /// <summary>
    /// We must not use ShowInTaskbar=false for MainForm. 
    /// This is need for correctly work system-wide hot-keys in future.
    /// </summary>
    public class RunMinimizedForm : Form
    {
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
            if (!m_AllowClose)
            {
                Visible = false;
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        public void SetRunMinimized(bool value)
        {
            m_RunMinimized = value;
        }

        public virtual void ApplicationExit()
        {
            m_AllowClose = true;
            Application.Exit();
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            if (m_ResizeBegan) return;

            if (WindowState != FormWindowState.Minimized)
            {
                m_LastWindowState = WindowState;
            }
            else
            {
                Visible = false;
            }
        }

        protected void ToggleVisible()
        {
            Visible = !Visible;
            if (Visible) Activate();
            App.Config.RunMinimized = !Visible;
        }
    }
}
