using System;
using System.Text;
using System.Runtime.InteropServices;

namespace LanExchange.Utils
{
    public static class NetApi32
    {
        internal const string NETAPI32 = "netapi32.dll";

        #region WinApi: NetServerEnum
        [DllImport(NETAPI32, EntryPoint = "NetServerEnum")]
        [System.Security.SuppressUnmanagedCodeSecurity]
        public static extern NERR NetServerEnum(
             [MarshalAs(UnmanagedType.LPWStr)]string ServerName,
             int Level,
             out IntPtr BufPtr,
             int PrefMaxLen,
             ref int EntriesRead,
             ref int TotalEntries,
             SV_101_TYPES ServerType,
             [MarshalAs(UnmanagedType.LPWStr)] string Domain,
             int ResumeHandle);

        [Serializable]
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

        public enum NERR
        {
            NERR_SUCCESS = 0,
            ERROR_ACCESS_DENIED = 5,
            ERROR_NOT_ENOUGH_MEMORY = 8,
            ERROR_BAD_NETPATH = 53,
            ERROR_NETWORK_BUSY = 54,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_INVALID_LEVEL = 124,
            ERROR_MORE_DATA = 234,
            ERROR_EXTENDED_ERROR = 1208,
            ERROR_NO_NETWORK = 1222,
            ERROR_INVALID_HANDLE_STATE = 1609,
            ERROR_NO_BROWSER_SERVERS_FOUND = 6118,
        }

        [Flags]
        public enum SV_101_TYPES : uint
        {
            SV_TYPE_WORKSTATION = 0x00000001,
            SV_TYPE_SERVER = 0x00000002,
            SV_TYPE_SQLSERVER = 0x00000004,
            SV_TYPE_DOMAIN_CTRL = 0x00000008,
            SV_TYPE_DOMAIN_BAKCTRL = 0x00000010,
            SV_TYPE_TIME_SOURCE = 0x00000020,
            SV_TYPE_AFP = 0x00000040,
            SV_TYPE_NOVELL = 0x00000080,
            SV_TYPE_DOMAIN_MEMBER = 0x00000100,
            SV_TYPE_PRINTQ_SERVER = 0x00000200,
            SV_TYPE_DIALIN_SERVER = 0x00000400,
            SV_TYPE_XENIX_SERVER = 0x00000800,
            SV_TYPE_SERVER_UNIX = SV_TYPE_XENIX_SERVER,
            SV_TYPE_NT = 0x00001000,
            SV_TYPE_WFW = 0x00002000,
            SV_TYPE_SERVER_MFPN = 0x00004000,
            SV_TYPE_SERVER_NT = 0x00008000,
            SV_TYPE_POTENTIAL_BROWSER = 0x00010000,
            SV_TYPE_BACKUP_BROWSER = 0x00020000,
            SV_TYPE_MASTER_BROWSER = 0x00040000,
            SV_TYPE_DOMAIN_MASTER = 0x00080000,
            SV_TYPE_SERVER_OSF = 0x00100000,
            SV_TYPE_SERVER_VMS = 0x00200000,
            SV_TYPE_WINDOWS = 0x00400000,
            SV_TYPE_DFS = 0x00800000,
            SV_TYPE_CLUSTER_NT = 0x01000000,
            SV_TYPE_TERMINALSERVER = 0x02000000,
            SV_TYPE_CLUSTER_VS_NT = 0x04000000,
            SV_TYPE_DCE = 0x10000000,
            SV_TYPE_ALTERNATE_XPORT = 0x20000000,
            SV_TYPE_LOCAL_LIST_ONLY = 0x40000000,
            SV_TYPE_DOMAIN_ENUM = 0x80000000,
            SV_TYPE_ALL = 0xFFFFFFFF,
        }

        public enum SV_101_PLATFORM : uint
        {
            PLATFORM_ID_DOS = 300,
            PLATFORM_ID_OS2 = 400,
            PLATFORM_ID_NT = 500,
            PLATFORM_ID_OSF = 600,
            PLATFORM_ID_VMS = 700,
        }
        #endregion

        #region WinApi: NetApiBufferFree
        [DllImport(NETAPI32, SetLastError = true)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        public static extern int NetApiBufferFree(IntPtr Buffer);
        #endregion

        #region WinApi: NetShareEnum
        [DllImport(NETAPI32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        public static extern NERR NetShareEnum(
             string ServerName,
             int level,
             out IntPtr bufPtr,
             uint prefmaxlen,
             ref int entriesread,
             ref int totalentries,
             int resume_handle
             );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHARE_INFO_1
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi1_netname;
            [MarshalAs(UnmanagedType.U4)]
            public uint shi1_type;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string shi1_remark;
        }

        public const uint MAX_PREFERRED_LENGTH = 0xFFFFFFFF;
        //public const int NERR_Success = 0;
        //private enum NetError : uint
        //{
        //    NERR_Success = 0,
        //    NERR_BASE = 2100,
        //    NERR_UnknownDevDir = (NERR_BASE + 16),
        //    NERR_DuplicateShare = (NERR_BASE + 18),
        //    NERR_BufTooSmall = (NERR_BASE + 23),
        //}
        public enum SHARE_TYPE : uint
        {
            STYPE_DISKTREE = 0,
            STYPE_PRINTQ = 1,
            STYPE_DEVICE = 2,
            STYPE_IPC = 3,
            STYPE_SPECIAL = 0x80000000,
        }
        #endregion

        #region WinApi: NetWkstaGetInfo
        [DllImport(NETAPI32, CharSet = CharSet.Unicode)]
        [System.Security.SuppressUnmanagedCodeSecurity]
        public static extern int NetWkstaGetInfo(string server,
            int level,
            out IntPtr info);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WKSTA_INFO_100
        {
            public int wki100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string wki100_computername;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string wki100_langroup;
            public int wki100_ver_major;
            public int wki100_ver_minor;
        }
        #endregion

    }
}