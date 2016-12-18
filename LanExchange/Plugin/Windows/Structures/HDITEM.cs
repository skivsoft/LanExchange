using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HDITEM
    {
        public int mask;
        public int cxy;
        public IntPtr pszText;
        public IntPtr hbm;
        public int cchTextMax;
        public int fmt;
        public IntPtr lParam;
        public int iImage;
        public int iOrder;
        public int type;
        public IntPtr pvFilter;
    }
}