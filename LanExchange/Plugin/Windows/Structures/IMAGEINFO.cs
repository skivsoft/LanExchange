using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct IMAGEINFO
    {
        private readonly IntPtr hbmImage;
        private readonly IntPtr hbmMask;
        private readonly int Unused1;
        private readonly int Unused2;
        private readonly RECT rcImage;
    }
}