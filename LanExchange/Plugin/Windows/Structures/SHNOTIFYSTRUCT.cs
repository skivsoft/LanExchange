using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.Structures
{
    // Contains two PIDLs concerning the notify message
    [StructLayout(LayoutKind.Sequential)]
    public struct SHNOTIFYSTRUCT
    {
        public IntPtr dwItem1;
        public IntPtr dwItem2;
    }
}