using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange.Components
{
    class CHotKey : Control
    {
        public CHotKey()
        {
            SetStyle(ControlStyles.UserPaint, false);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassName = "msctls_hotkey32";

                return cp;
            }
        }
    }
}
