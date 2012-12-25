using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LanExchange.View
{
    public interface IInfoView
    {
        string InfoComp { get; set; }
        string InfoDesc { get; set; }
        string InfoOS { get; set; }
        PictureBox Picture { get; }
    }
}
