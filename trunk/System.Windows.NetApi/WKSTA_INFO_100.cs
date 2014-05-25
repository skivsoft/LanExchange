using System.Runtime.InteropServices;

namespace System.Windows.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct WKSTA_INFO_100
    {
        public uint wki100_platform_id;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wki100_computername;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string wki100_langroup;
        public uint wki100_ver_major;
        public uint wki100_ver_minor;
    }
}