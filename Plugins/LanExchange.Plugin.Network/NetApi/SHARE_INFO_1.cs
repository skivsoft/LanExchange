using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [CLSCompliant(false)]
    public struct SHARE_INFO_1
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string netname;
        [MarshalAs(UnmanagedType.U4)]
        public uint type;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string remark;
    }
}