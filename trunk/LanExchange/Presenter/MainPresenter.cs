using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Model.Settings;

namespace LanExchange.Presenter
{
    public class MainPresenter : PresenterBase<IMainView>, IMainPresenter
    {
        private int GetDefaultWidth()
        {
            const double phi2 = 0.6180339887498949;
            return (int)(Screen.PrimaryScreen.WorkingArea.Width * phi2 * phi2);
        }

        public Rectangle SettingsGetBounds()
        {
            // correct width and height
            bool boundsIsNotSet = Settings.Instance.MainFormWidth == 0;
            Rectangle workingArea;
            if (boundsIsNotSet)
                workingArea = Screen.PrimaryScreen.WorkingArea;
            else
                workingArea = Screen.GetWorkingArea(new Point(Settings.Instance.MainFormX + Settings.Instance.MainFormWidth / 2, 0));
            var rect = new Rectangle();
            rect.X = Settings.Instance.MainFormX;
            rect.Y = workingArea.Top;
            rect.Width = Math.Min(Math.Max(GetDefaultWidth(), Settings.Instance.MainFormWidth), workingArea.Width);
            rect.Height = workingArea.Height;
            // determination side to snap right or left
            int centerX = (rect.Left + rect.Right) >> 1;
            int workingAreaCenterX = (workingArea.Left + workingArea.Right) >> 1;
            if (boundsIsNotSet || centerX >= workingAreaCenterX)
                // snap to right side
                rect.X = workingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - workingArea.Left;
            return rect;
        }

        public void SettingsSetBounds(Rectangle rect)
        {
            Rectangle workingArea = Screen.GetWorkingArea(rect);
            // shift rect into working area
            if (rect.Left < workingArea.Left) rect.X = workingArea.Left;
            if (rect.Top < workingArea.Top) rect.Y = workingArea.Top;
            if (rect.Right > workingArea.Right) rect.X -= rect.Right - workingArea.Right;
            if (rect.Bottom > workingArea.Bottom) rect.Y -= rect.Bottom - workingArea.Bottom;
            // determination side to snap right or left
            int centerX = (rect.Left + rect.Right) >> 1;
            int workingAreaCenterX = (workingArea.Left + workingArea.Right) >> 1;
            if (centerX >= workingAreaCenterX)
                // snap to right side
                rect.X = workingArea.Right - rect.Width;
            else
                // snap to left side
                rect.X -= rect.Left - workingArea.Left;
            // set properties
            if (rect.Left != Settings.Instance.MainFormX || rect.Width != Settings.Instance.MainFormWidth)
            {
                Settings.Instance.MainFormX = rect.Left;
                Settings.Instance.MainFormWidth = rect.Width;
            }
        }
    }
}