using System;
using System.Runtime.InteropServices;

namespace LanExchange.OS.Windows.NetApi32
{
    public static class NativeMethods
    {
        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetWkstaGetInfo(string server, int level, out IntPtr info);
    }
}
