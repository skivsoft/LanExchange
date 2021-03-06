using System;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    [CLSCompliant(false)]
    public sealed class SERVER_INFO_101
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint platform_id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string name;
        [MarshalAs(UnmanagedType.U4)]
        public uint version_major;
        [MarshalAs(UnmanagedType.U4)]
        public uint version_minor;
        [MarshalAs(UnmanagedType.U4)]
        public uint type;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string comment;
    }
}