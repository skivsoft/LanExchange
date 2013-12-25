using System;
using System.Runtime.InteropServices;

namespace LanExchange.SDK
{
    [CLSCompliant(false)]
    public interface IIPHLPAPISerivice
    {
        int SendARP(int destIP, int srcIP, [Out] byte[] pMacAddr, ref uint physAddrLen);        
    }
}