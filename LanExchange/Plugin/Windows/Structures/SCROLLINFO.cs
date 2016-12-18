using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct SCROLLINFO
    {
        public int cbSize;
        public int fMask;
        public int nMin;
        public int nMax;
        public int nPage;
        public int nPos;
        public int nTrackPos;
    }
}