using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains the information needed to create a drag image
    [StructLayout(LayoutKind.Sequential)]
    public struct SHDRAGIMAGE
    {
        public SIZE sizeDragImage;
        public POINT ptOffset;
        public IntPtr hbmpDragImage;
        public Color crColorKey;
    }
}