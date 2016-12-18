using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Defines the x- and y-coordinates of a point
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}