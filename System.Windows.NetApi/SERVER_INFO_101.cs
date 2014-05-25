using System.Runtime.InteropServices;

namespace System.Windows.NetApi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SERVER_INFO_101
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint sv101_platform_id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string sv101_name;
        [MarshalAs(UnmanagedType.U4)]
        public uint sv101_version_major;
        [MarshalAs(UnmanagedType.U4)]
        public uint sv101_version_minor;
        [MarshalAs(UnmanagedType.U4)]
        public uint sv101_type;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string sv101_comment;
    }
}