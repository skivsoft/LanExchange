using LanExchange.SDK;
using System;

namespace LanExchange.OS.Windows
{
    internal class NetApi32Service : INetApi32Service
    {
        public int NetWkstaGetInfo(string server, int level, out IntPtr info)
        {
            return NetApi32.NativeMethods.NetWkstaGetInfo(server, level, out info);
        }
    }
}