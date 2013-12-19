using System.Drawing;
using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.UI.WinForms
{
    internal class ScreenImpl : IScreenService
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

        public int MenuHeight
        {
            get { return SystemInformation.MenuHeight; }
        }

        public string UserName
        {
            get { return SystemInformation.UserName; }
        }
    }
}
