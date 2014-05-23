using System.Runtime.InteropServices;

namespace LanExchange.OS.Windows.IPHLPAPI
{
    internal static class NativeMethods
    {
        [DllImport(ExternDll.IPHLPAPI)]
        internal static extern int SendARP(int destIP, int srcIP, [Out] byte[] pMacAddr, ref uint physAddrLen);
    }
}
