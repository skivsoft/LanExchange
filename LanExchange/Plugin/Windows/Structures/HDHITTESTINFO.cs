using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HDHITTESTINFO
    {
        public int pt_x;
        public int pt_y;
        public int flags;
        public int iItem;
    }
}