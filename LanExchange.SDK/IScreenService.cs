using System.Drawing;

namespace LanExchange.SDK
{
    public interface IScreenService
    {
        Rectangle PrimaryScreenWorkingArea { get; }

        Rectangle GetWorkingArea(Point pt);

        Rectangle GetWorkingArea(Rectangle rect);

        Point CursorPosition { get; }
    }
}