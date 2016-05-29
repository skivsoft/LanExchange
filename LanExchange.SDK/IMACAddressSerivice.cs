using System;
using System.Net;

namespace LanExchange.SDK
{
    [CLSCompliant(false)]
    public interface IMACAddressSerivice
    {
        string GetMACAddress(IPAddress ipAddress);
    }
}