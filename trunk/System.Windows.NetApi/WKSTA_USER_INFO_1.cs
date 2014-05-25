using System.Runtime.InteropServices;

namespace System.Windows.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct WKSTA_USER_INFO_1
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wkui1_username;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wkui1_logon_domain;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wkui1_oth_domains;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wkui1_logon_server;
    }
}