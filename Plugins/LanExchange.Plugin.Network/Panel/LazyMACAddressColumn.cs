using System;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class LazyMACAddressColumn : LazyPanelColumn
    {
        internal const string IPHLPAPI = "iphlpapi.dll";

        /// http://www.codeproject.com/KB/IP/host_info_within_network.aspx
        [DllImport(IPHLPAPI, ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);


        public LazyMACAddressColumn(string text, int width = 0) : base(text, width)
        {
        }

        public override IComparable SyncGetData(PanelItemBase item)
        {
            //#if DEBUG
            //Thread.Sleep(1000);
            //#endif
            var ip_addr = Dns.GetHostEntry(item.Name).AddressList[0];
            var ab = new byte[6];
            int len = ab.Length;
            int r = SendARP(ip_addr.GetHashCode(), 0, ab, ref len);
            return BitConverter.ToString(ab, 0, 6);
        }
    }
}
