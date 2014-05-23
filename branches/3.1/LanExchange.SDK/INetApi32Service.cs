using System;

namespace LanExchange.SDK
{
    [CLSCompliant(false)]
    public interface INetApi32Service
    {
        int NetApiBufferFree(IntPtr bufPtr);

        int NetWkstaGetInfo(string server, uint level, out IntPtr info);

        int NetServerEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, uint serverType, string domain, ref uint resumeHandle);

        int NetShareEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle);

        int NetWkstaUserEnum(string server, uint level, out IntPtr bufPtr, uint prefMaxLen,
            out uint entriesRead, out uint totalEntries, ref uint resumeHandle);
    }
}