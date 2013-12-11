using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace LanExchange.SDK.UI
{
    public interface IScreenService
    {
        Rectangle PrimaryScreenWorkingArea { get; }

        Rectangle GetWorkingArea(Point pt);

        Rectangle GetWorkingArea(Rectangle rect);
    }
}
