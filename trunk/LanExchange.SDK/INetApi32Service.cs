using System;

namespace LanExchange.SDK
{
    public interface INetApi32Service
    {
        int NetWkstaGetInfo(string server, int level, out IntPtr info);
    }
}