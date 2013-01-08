using System;
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
