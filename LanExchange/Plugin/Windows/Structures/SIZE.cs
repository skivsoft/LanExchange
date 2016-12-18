using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // The SIZE structure specifies the width and height of a rectangle
    [StructLayout(LayoutKind.Sequential)]
    public struct SIZE
    {
        public int cx;
        public int cy;
    }
}