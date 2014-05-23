using System;
using System.Runtime.InteropServices;

namespace LanExchange.OS.Windows.NetApi32
{
    internal static class NativeMethods
    {
        [DllImport(ExternDll.NetApi32)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetApiBufferFree(IntPtr bufPtr);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetWkstaGetInfo(string server, uint level, out IntPtr info);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, ref uint resumeHandle);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle);
    }
}