using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using LanExchange.Model;

namespace LanExchange.UI
{
    public class ColumnHeaderEx : ColumnHeader, IPanelColumnHeader
    {
        public bool Visible { get; set; }
    }
}
