using System;
using System.Runtime.InteropServices;

namespace LanExchange.Windows
{
    internal static class NativeMethods
    {
        public static HandleRef NullHandleRef = new HandleRef(null, IntPtr.Zero);

        public const int
            WM_QUERYENDSESSION = 0x0011,
            WM_ENDSESSION = 0x0016,
            SW_HIDE = 0,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_MAX = 10,
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_DRAWFRAME = 0x0020,
            SWP_NOOWNERZORDER = 0x0200;

        public const uint
            LP_ENDSESSION_CLOSEAPP = 0x00000001,
            LP_ENDSESSION_CRITICAL = 0x40000000,
            LP_ENDSESSION_LOGOFF   = 0x80000000;


    }
}
