using System.Runtime.InteropServices;

namespace System.Windows.NetApi
{
    internal static class NativeMethods
    {
        public const uint MAX_PREFERRED_LENGTH = 0xFFFFFFFF;

        public enum NERR
        {
            NERR_SUCCESS = 0,
            ERROR_ACCESS_DENIED = 5,
            ERROR_NOT_ENOUGH_MEMORY = 8,
            ERROR_BAD_NETPATH = 53,
            ERROR_NETWORK_BUSY = 54,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_INVALID_LEVEL = 124,
            ERROR_MORE_DATA = 234,
            ERROR_EXTENDED_ERROR = 1208,
            ERROR_NO_NETWORK = 1222,
            ERROR_INVALID_HANDLE_STATE = 1609,
            ERROR_NO_BROWSER_SERVERS_FOUND = 6118
        }


        [DllImport(ExternDll.NetApi32)]
        [Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetApiBufferFree(IntPtr bufPtr);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetWkstaGetInfo(string server, uint level, out IntPtr info);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
             out uint entriesRead, out uint totalEntries, ref uint resumeHandle);

        [DllImport(ExternDll.NetApi32, CharSet = CharSet.Unicode)]
        [Security.SuppressUnmanagedCodeSecurity]
        internal static extern int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle);
    }
}