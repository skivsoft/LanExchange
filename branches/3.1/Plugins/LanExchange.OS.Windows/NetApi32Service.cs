using LanExchange.SDK;
using System;

namespace LanExchange.OS.Windows
{
    internal class NetApi32Service : INetApi32Service
    {
        public int NetApiBufferFree(IntPtr bufPtr)
        {
            return NetApi32.NativeMethods.NetApiBufferFree(bufPtr);
        }

        public int NetWkstaGetInfo(string server, uint level, out IntPtr info)
        {
            return NetApi32.NativeMethods.NetWkstaGetInfo(server, level, out info);
        }

        public int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen, 
            out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle)
        {
            return NetApi32.NativeMethods.NetServerEnum(server, level, out bufPtr, prefMaxLen, 
                out entriesRead, out totalEntries, serverType, domain, ref resumeHandle);
        }

        public int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle)
        {
            return NetApi32.NativeMethods.NetShareEnum(server, level, out bufPtr, prefMaxLen,
                out entriesRead, out totalEntries, ref resumeHandle);
        }

        public int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle)
        {
            return NetApi32.NativeMethods.NetWkstaUserEnum(server, level, out bufPtr, prefMaxLen,
                out entriesRead, out totalEntries, ref resumeHandle);
        }
    }
}