using System.Runtime.InteropServices;
using LanExchange.SDK;

namespace LanExchange.OS.Windows
{
    internal class IPHLPAPISerivce : IIPHLPAPISerivice
    {
        public int SendARP(int destIP, int srcIP, [Out] byte[] pMacAddr, ref uint physAddrLen)
        {
            return IPHLPAPI.NativeMethods.SendARP(destIP, srcIP, pMacAddr, ref physAddrLen);
        }
    }
}