using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model
{
    public interface IPanelColumnHeader
    {
        string Text { get; set; }
        //int Width { get; set; }
        bool Visible { get; set; }
    }
}
