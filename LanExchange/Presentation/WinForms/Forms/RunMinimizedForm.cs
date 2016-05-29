using System;
using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Forms
{
    /// <summary>
    /// We must not use ShowInTaskbar=false for MainForm. 
    /// This is need for correctly work system-wide hot-keys in future.
    /// </summary>
    public class RunMinimizedForm : Form
    {
        private FormWindowState lastWindowState;
        private bool runMinimized;
        private bool allowVisible;
        private bool allowClose;
        private bool resizeBegan;

        protected RunMinimizedForm()
        {
            Resize += Form_Resize;
            ResizeBegin += (sender, args) => resizeBegan = true;
            ResizeEnd += (sender, args) => resizeBegan = false;
        }

        protected override void SetVisibleCore(bool value)
        {
            if (runMinimized && !allowVisible)
            {
                value = false;
                allowVisible = true;
            }
            base.SetVisibleCore(value);
            if (value && WindowState == FormWindowState.Minimized)
                WindowState = lastWindowState;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowClose)
            {
                Visible = false;
                e.Cancel = true;
            }
            base.OnFormClosing(e);
        }

        public void SetRunMinimized(bool value)
        {
            runMinimized = value;
        }

        public virtual void ApplicationExit()
        {
            allowClose = true;
            Application.Exit();
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            if (resizeBegan) return;

            if (WindowState != FormWindowState.Minimized)
            {
                lastWindowState = WindowState;
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
