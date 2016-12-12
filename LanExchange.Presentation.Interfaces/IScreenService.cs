using System.Drawing;

namespace LanExchange.Presentation.Interfaces
{
    public interface IScreenService
    {
        Rectangle PrimaryScreenWorkingArea { get; }

        Point CursorPosition { get; }

        Rectangle GetWorkingArea(Point pt);

        Rectangle GetWorkingArea(Rectangle rect);
    }
}