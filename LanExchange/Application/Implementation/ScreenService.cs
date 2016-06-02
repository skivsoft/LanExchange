using System.Drawing;
using System.Windows.Forms;
using LanExchange.Application.Interfaces;

namespace LanExchange.Application.Implementation
{
    internal sealed class ScreenService : IScreenService
    {
        public Rectangle PrimaryScreenWorkingArea
        {
            get { return Screen.PrimaryScreen.WorkingArea; }
        }

        public Rectangle GetWorkingArea(Point pt)
        {
            return Screen.GetWorkingArea(pt);
        }

        public Rectangle GetWorkingArea(Rectangle rect)
        {
            return Screen.GetWorkingArea(rect);
        }

        public Point CursorPosition
        {
            get { return Cursor.Position; }
        }
    }
}
