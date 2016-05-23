using System;
using System.ComponentModel;
using System.Net;

namespace LanExchange.Plugin.Network
{
    public sealed class IPAddressArray : IComparable<IPAddressArray>, IComparable
    {
        private readonly IPAddressComparable[] addresses;

        public IPAddressArray(IPAddress[] list)
        {
            addresses = new IPAddressComparable[list.Length];
            for (int i = 0; i < addresses.Length; i++)
                addresses[i] = new IPAddressComparable(list[i]);
            Array.Sort(addresses);
        }

        public IPAddressComparable First
        {
            get
            {
                if (addresses.Length == 0)
                    return null;
                return addresses[0];
            }
        }

        [Localizable(false)]
        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < addresses.Length; i++)
            {
                if (i > 0) result += ", ";
                result += addresses[i].ToString();
            }
            return result;
        }

        public int CompareTo(object other)
        {
            return CompareTo(other as IPAddressArray);
        }

        public int CompareTo(IPAddressArray other)
        {
            return First.CompareTo(other.First);
        }
    }
}
