using LanExchange.SDK;
using System;

namespace LanExchange.OS.Linux
{
    internal class NetApi32Service : INetApi32Service
    {
        public int NetApiBufferFree(IntPtr bufPtr)
        {
            return 0;
        }

        public int NetWkstaGetInfo(string server, uint level, out IntPtr info)
        {
            info = IntPtr.Zero;
            return 0;
        }

        public int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen, 
            out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle)
        {
            entriesRead = 0;
            totalEntries = 0;
            bufPtr = IntPtr.Zero;
            return 0;
        }

        public int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle)
        {
            entriesRead = 0;
            totalEntries = 0;
            bufPtr = IntPtr.Zero;
            return 0;
        }

        public int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle)
        {
            entriesRead = 0;
            totalEntries = 0;
            bufPtr = IntPtr.Zero;
            return 0;
        }
    }
}