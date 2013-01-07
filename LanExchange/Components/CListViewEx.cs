using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace LanExchange
{
    public class CListViewEx : ListView
    {

        public CListViewEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
