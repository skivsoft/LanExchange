using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LanExchange.UI
{
    public class FormPlacement
    {
        private Rectangle m_Bounds;
        //private Rectangle m_MaximizedBox;
        //private Rectangle m_MinimizedBox;
        //private Rectangle m_RestoreBounds;
        private FormWindowState m_State;
        private bool m_Saved;

        public FormPlacement()
        {
        }

        public void AttachToForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException();
            form.Load += Form_Load;
            form.Closed += Form_Closed;
        }

        public void DetachFromForm(Form form)
        {
            if (form == null)
                throw new ArgumentNullException();
            form.Load -= Form_Load;
            form.Closed -= Form_Closed;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form == null || !m_Saved) return;
            form.StartPosition = FormStartPosition.Manual;
            form.Bounds = m_Bounds;
            form.WindowState = m_State;
        }

        private void Form_Closed(object sender, EventArgs e)
        {
            Form form = sender as Form;
            if (form == null) return;
            m_State = form.WindowState;
            if (form.WindowState == FormWindowState.Maximized)
                form.WindowState = FormWindowState.Normal;
            m_Bounds = form.Bounds;
            m_Saved = true;
        }
    }
}
