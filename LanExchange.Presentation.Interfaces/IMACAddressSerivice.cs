using System;
using System.Net;

namespace LanExchange.Presentation.Interfaces
{
    [CLSCompliant(false)]
    public interface IMACAddressSerivice
    {
        string GetMACAddress(IPAddress ipAddress);
    }
}