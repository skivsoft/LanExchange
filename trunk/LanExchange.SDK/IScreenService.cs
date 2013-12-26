using System.Drawing;

namespace LanExchange.SDK
{
    public interface IScreenService
    {
        Rectangle PrimaryScreenWorkingArea { get; }

        Rectangle GetWorkingArea(Point pt);

        Rectangle GetWorkingArea(Rectangle rect);

        int MenuHeight { get; }

        string UserName { get; }

        string ComputerName { get; }
    }
}