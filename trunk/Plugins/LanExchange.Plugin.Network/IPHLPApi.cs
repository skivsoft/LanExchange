using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network
{
    public static class IPHLPApi
    {
        internal const string IPHLPAPI = "iphlpapi.dll";

        [DllImport(IPHLPAPI, ExactSpelling = true)]
        public static extern int SendARP(int DestIP, int SrcIP, [Out] byte[] pMacAddr, ref int PhyAddrLen);
    }
}
