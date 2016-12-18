using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains information about a file object
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHFILEINFO
    {
        public const int MAX_PATH = 260;
        public const int MAX_TYPE = 80;

        public IntPtr hIcon;
        public int iIcon;
        public int dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_TYPE)]
        public string szTypeName;
    }
}