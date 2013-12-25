using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace LanExchange.Plugin.Network
{
    public static class NetApi32Utils
    {
        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public static string NetWkstaGetInfo(string server)
        {
            if (PluginNetwork.NETAPI32 == null)
                return string.Empty;
            IntPtr buffer;
            NativeMethods.WKSTA_INFO_100 result;
            var retval = PluginNetwork.NETAPI32.NetWkstaGetInfo(server, 102, out buffer);
            if (retval != 0)
                throw new Win32Exception(retval);
            try
            {
                result = (NativeMethods.WKSTA_INFO_100)Marshal.PtrToStructure(buffer, typeof(NativeMethods.WKSTA_INFO_100));
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
        public static IEnumerable<NativeMethods.SERVER_INFO_101> NetServerEnum(string domain, NativeMethods.SV_101_TYPES types)
        {
            IntPtr pInfo;
            uint entriesread = 0;
            uint totalentries = 0;
            NativeMethods.NERR err;
            unchecked
            {
                err = NativeMethods.NetServerEnum(null, 101, out pInfo, (uint)-1, ref entriesread, ref totalentries, types, domain, 0);
            }
            if ((err == NativeMethods.NERR.NERR_SUCCESS || err == NativeMethods.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                try
                {
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        yield return (NativeMethods.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NativeMethods.SERVER_INFO_101));
                        ptr += Marshal.SizeOf(typeof (NativeMethods.SERVER_INFO_101));
                    }
                }
                finally
                {
                    if (pInfo != IntPtr.Zero)
                        NativeMethods.NetApiBufferFree(pInfo);
                }
        }

        public static IEnumerable<NativeMethods.SHARE_INFO_1> NetShareEnum(string computer)
        {
            IntPtr pInfo;
            int entriesread = 0;
            int totalentries = 0;
            NativeMethods.NERR err = NativeMethods.NetShareEnum(computer, 1, out pInfo, NativeMethods.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, 0);
            if ((err == NativeMethods.NERR.NERR_SUCCESS || err == NativeMethods.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                try
                {
                    const uint stypeIPC = (uint)NativeMethods.SHARE_TYPE.STYPE_IPC;
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        var shi1 = (NativeMethods.SHARE_INFO_1)Marshal.PtrToStructure(new IntPtr(ptr), typeof (NativeMethods.SHARE_INFO_1));
                        if ((shi1.shi1_type & stypeIPC) != stypeIPC)
                            yield return shi1;
                        ptr += Marshal.SizeOf(typeof (NativeMethods.SHARE_INFO_1));
                    }
                }
                finally
                {
                    if (pInfo != IntPtr.Zero)
                        NativeMethods.NetApiBufferFree(pInfo);
                }
        }

        public static IEnumerable<NativeMethods.WKSTA_USER_INFO_1> NetWkstaUserEnum(string computer)
        {
            IntPtr pInfo;
            uint entriesread = 0;
            uint totalentries = 0;
            uint resumehandle = 0;

            var err = NativeMethods.NetWkstaUserEnum(computer, 1, out pInfo, NativeMethods.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resumehandle);
            if ((err == NativeMethods.NERR.NERR_SUCCESS || err == NativeMethods.NERR.ERROR_MORE_DATA) && (pInfo != IntPtr.Zero))
            try
            {
                int ptr = pInfo.ToInt32();
                for (int i = 0; i < entriesread; i++)
                {
                    var item =
                        (NativeMethods.WKSTA_USER_INFO_1)
                        Marshal.PtrToStructure(new IntPtr(ptr), typeof (NativeMethods.WKSTA_USER_INFO_1));
                    yield return item;
                    ptr += Marshal.SizeOf(typeof (NativeMethods.WKSTA_USER_INFO_1));
                }
            }
            finally
            {
                NativeMethods.NetApiBufferFree(pInfo);
            }
        }

        [Localizable(false)]
        public static string[] NetWkstaUserEnumNames(string computer)
        {
            var users = new List<string>();
            var domain = NetWkstaGetInfo(null);
            foreach (var item in NetWkstaUserEnum(computer))
            {
                if (item.wkui1_username.EndsWith("$")) continue;
                string name;
                if (domain != item.wkui1_logon_domain)
                    name = string.Format(@"{0}\{1}", item.wkui1_logon_domain, item.wkui1_username);
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