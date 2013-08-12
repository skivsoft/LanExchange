using System;
using System.Net;

namespace LanExchange.Plugin.Network
{
    public class IPAddressArray : IComparable<IPAddressArray>, IComparable
    {
        private readonly IPAddressComparable[] m_List;

        public IPAddressArray(IPAddress[] list)
        {
            m_List = new IPAddressComparable[list.Length];
            for (int i = 0; i < m_List.Length; i++)
                m_List[i] = new IPAddressComparable(list[i]);
            Array.Sort(m_List);
        }

        public IPAddressComparable First
        {
            get
            {
                if (m_List.Length == 0)
                    return null;
                return m_List[0];
            }
        }

        public override string ToString()
        {
            var result = "";
            for (int i = 0; i < m_List.Length; i++)
            {
                if (i > 0) result += ", ";
                result += m_List[i].ToString();
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
