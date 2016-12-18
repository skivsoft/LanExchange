using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WKSTA_USER_INFO_1
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string username;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string logon_domain;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string other_domains;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string logon_server;
    }
}