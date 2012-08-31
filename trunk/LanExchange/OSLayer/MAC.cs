// [url]http://www.codeproject.com/KB/IP/host_info_within_network.aspx[/url]
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Net;

namespace LanExchange.OSLayer
{
    internal class MAC
    {
        internal const string IPHLPAPI = "iphlpapi.dll";

        [DllImport(IPHLPAPI, ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);

        public static string ConvertIpToMAC(IPAddress ip_adr)
        {
            byte[] ab = new byte[6];
            int len = ab.Length;
            int r = SendARP(ip_adr.GetHashCode(), 0, ab, ref len);
            return BitConverter.ToString(ab, 0, 6);
        }
    }
}
