using System;
using System.Net;

namespace LanExchange.Plugin.Network
{
    public class IPAddressComparable : IPAddress, IComparable<IPAddressComparable>, IComparable
    {
        public IPAddressComparable(IPAddress ip)
            : base(ip.GetAddressBytes())
        {
        }

        public int CompareTo(object other)
        {
            return CompareTo(other as IPAddressComparable);
        }

        public int CompareTo(IPAddressComparable other)
        {
            var a1 = GetAddressBytes();
            var a2 = other.GetAddressBytes();
            if (a1.Length < a2.Length) return -1;
            if (a1.Length > a2.Length) return 1;
            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] < a2[i]) return -1;
                if (a1[i] > a2[i]) return 1;
            }
            return 0;
        }
    }
}
