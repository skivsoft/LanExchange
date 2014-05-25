using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.NetApi
{

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
            SV_TYPE_ALL = 0xFFFFFFFF
        }

        public enum SV_101_PLATFORM : uint
        {
            PLATFORM_ID_DOS = 300,
            PLATFORM_ID_OS2 = 400,
            PLATFORM_ID_NT = 500,
            PLATFORM_ID_OSF = 600,
            PLATFORM_ID_VMS = 700
        }

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

    public static class NetApiHelper
    {
        private const uint API_BUFFER_SIZE = 32768;

        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string GetMachineNetBiosDomain(string server)
        {
            IntPtr buffer;
            WKSTA_INFO_100 result;
            var retval = NativeMethods.NetWkstaGetInfo(server, 100, out buffer);
            if (retval != 0)
                throw new Win32Exception(retval);
            try
            {
                result = (WKSTA_INFO_100)Marshal.PtrToStructure(buffer, typeof(WKSTA_INFO_100));
            }
            finally
            {
                NativeMethods.NetApiBufferFree(buffer);
            }
            return result.wki100_langroup;
        }

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<SERVER_INFO_101> NetServerEnum(string domain, SV_101_TYPES types)
        {
            uint resumeHandle = 0;
            int result;
            var itemType = typeof (SERVER_INFO_101);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = NativeMethods.NetServerEnum(null, 101, out bufPtr, NativeMethods.MAX_PREFERRED_LENGTH,
                    out entriesread, out totalentries, (uint) types, domain, ref resumeHandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        yield return (SERVER_INFO_101) Marshal.PtrToStructure(ptr, itemType);
                        ptr = (IntPtr) (ptr.ToInt64() + itemSize);
                    }
                    NativeMethods.NetApiBufferFree(bufPtr);
                }
            } while (result == (int) NativeMethods.NERR.ERROR_MORE_DATA);
        }

        public static IEnumerable<SHARE_INFO_1> NetShareEnum(string computer)
        {
            const uint stypeIPC = (uint)SHARE_TYPE.STYPE_IPC;
            uint resumeHandle = 0;
            int result;
            var itemType = typeof (SHARE_INFO_1);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = NativeMethods.NetShareEnum(computer, 1, out bufPtr, API_BUFFER_SIZE,
                    out entriesread, out totalentries, ref resumeHandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var shi1 = (SHARE_INFO_1)Marshal.PtrToStructure(ptr, itemType);
                        if ((shi1.shi1_type & stypeIPC) != stypeIPC)
                            yield return shi1;
                        ptr = (IntPtr)(ptr.ToInt64() + itemSize);
                    }
                    NativeMethods.NetApiBufferFree(bufPtr);
                }
            } while (result == (int) NativeMethods.NERR.ERROR_MORE_DATA);
        }

        internal static IEnumerable<WKSTA_USER_INFO_1> NetWkstaUserEnum(string computer)
        {
            uint resumehandle = 0;
            int result;
            var itemType = typeof (WKSTA_USER_INFO_1);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = NativeMethods.NetWkstaUserEnum(computer, 1, out bufPtr, API_BUFFER_SIZE, 
                    out entriesread, out totalentries, ref resumehandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var item = (WKSTA_USER_INFO_1)Marshal.PtrToStructure(ptr, itemType);
                        yield return item;
                        ptr = (IntPtr) (ptr.ToInt64() + itemSize);
                    }
                    NativeMethods.NetApiBufferFree(bufPtr);
                }
            } while (result == (int)NativeMethods.NERR.ERROR_MORE_DATA);
        }

        [Localizable(false)]
        public static string[] NetWkstaUserEnumNames(string computer)
        {
            var users = new List<string>();
            var domain = GetMachineNetBiosDomain(null);
            foreach (var item in NetWkstaUserEnum(computer))
            {
                if (item.wkui1_username.EndsWith("$")) continue;
                string name;
                if (domain != item.wkui1_logon_domain)
                    name = string.Format(@"{0}\{1}", item.wkui1_logon_domain.ToUpper(), item.wkui1_username);
                else
                    name = item.wkui1_username;
                if (!users.Contains(name))
                    users.Add(name);
            }
            users.Sort();
            return users.ToArray();
        }
    }
}