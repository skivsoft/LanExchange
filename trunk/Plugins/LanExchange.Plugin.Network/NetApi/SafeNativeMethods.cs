using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    internal static class SafeNativeMethods
    {
        internal const string NetApi32 = "netapi32.dll";

        public const uint MAX_PREFERRED_LENGTH = 0xFFFFFFFF;

        [DllImport(NetApi32, CharSet = CharSet.Unicode)]
        internal static extern int NetWkstaGetInfo(string servername, int level, out IntPtr bufptr);

        [DllImport(NetApi32, SetLastError = true)]
        internal static extern int NetApiBufferFree(IntPtr bufptr);

        [DllImport(NetApi32, CharSet = CharSet.Unicode)]
        internal static extern int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle);

        [DllImport(NetApi32, CharSet = CharSet.Unicode)]
        internal static extern int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, ref uint resumeHandle);

        [DllImport(NetApi32, CharSet = CharSet.Unicode)]
        internal static extern int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle);
    }
}