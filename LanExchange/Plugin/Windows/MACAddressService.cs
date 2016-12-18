using System;
using System.Net;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Windows
{
    internal class MACAddressService : IMACAddressSerivice
    {
        public string GetMACAddress(IPAddress ipAddress)
        {
            var mac = new byte[6];
            var macLength = (uint)mac.Length;
            var result = IPHLPAPI.NativeMethods.SendARP(ipAddress.GetHashCode(), 0, mac, ref macLength);
            return result == 0 ? BitConverter.ToString(mac, 0, 6) : string.Empty;
        }
    }
}