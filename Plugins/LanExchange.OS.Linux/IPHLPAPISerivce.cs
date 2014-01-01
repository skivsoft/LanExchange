using System.Runtime.InteropServices;
using LanExchange.SDK;

namespace LanExchange.OS.Linux
{
    internal class IPHLPAPISerivce : IIPHLPAPISerivice
    {
        public int SendARP(int destIP, int srcIP, [Out] byte[] pMacAddr, ref uint physAddrLen)
        {
            return 0;
        }
    }
}