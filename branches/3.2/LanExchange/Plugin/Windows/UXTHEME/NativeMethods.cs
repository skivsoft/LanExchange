using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Windows.UXTHEME
{
    internal static class NativeMethods
    {
        [DllImport(ExternDll.UXTHEME, ExactSpelling = true, CharSet = CharSet.Auto)]
        internal static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);
    }
}