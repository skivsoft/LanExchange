using System;
using System.Security;
using System.Runtime.InteropServices;

namespace LanExchange.OS.Windows.Utils
{
    [SuppressUnmanagedCodeSecurity]
    internal static class SafeNativeMethods
    {
        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern bool IsWindowVisible(HandleRef hWnd);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumThreadWindowsCallback callback, IntPtr extraData);
        internal delegate bool EnumThreadWindowsCallback(IntPtr hWnd, IntPtr lParam); 

        [DllImport(ExternDll.User32, ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(HandleRef hWnd, out int lpdwProcessId);

        [DllImport(ExternDll.User32, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(HandleRef hWnd);

    }
}
