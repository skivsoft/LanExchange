using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LanExchange.Model.VO;

namespace LanExchange.View.Components
{
    public class ListViewEx : ListView
    {
        public ListViewEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
        }
    }
}
