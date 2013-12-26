using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace LanExchange.Plugin.Network
{
    internal static class NetApi32Utils
    {
        private const uint API_BUFFER_SIZE = 32768; // 128 

        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        internal static string NetWkstaGetInfo(string server)
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
                PluginNetwork.NETAPI32.NetApiBufferFree(buffer);
            }
            return result.wki100_langroup;
        }

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        internal static IEnumerable<NativeMethods.SERVER_INFO_101> NetServerEnum(string domain, NativeMethods.SV_101_TYPES types)
        {
            if (PluginNetwork.NETAPI32 == null)
                yield break;

            uint resumeHandle = 0;
            int result;
            var itemType = typeof (NativeMethods.SERVER_INFO_101);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = PluginNetwork.NETAPI32.NetServerEnum(null, 101, out bufPtr, API_BUFFER_SIZE,
                    out entriesread, out totalentries, (uint) types, domain, ref resumeHandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        yield return (NativeMethods.SERVER_INFO_101) Marshal.PtrToStructure(ptr, itemType);
                        ptr = (IntPtr) (ptr.ToInt32() + itemSize);
                    }
                    PluginNetwork.NETAPI32.NetApiBufferFree(bufPtr);
                }
            } while (result == (int) NativeMethods.NERR.ERROR_MORE_DATA);
        }

        internal static IEnumerable<NativeMethods.SHARE_INFO_1> NetShareEnum(string computer)
        {
            if (PluginNetwork.NETAPI32 == null)
                yield break;

            const uint stypeIPC = (uint)NativeMethods.SHARE_TYPE.STYPE_IPC;
            uint resumeHandle = 0;
            int result;
            var itemType = typeof (NativeMethods.SHARE_INFO_1);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = PluginNetwork.NETAPI32.NetShareEnum(computer, 1, out bufPtr, API_BUFFER_SIZE,
                    out entriesread, out totalentries, ref resumeHandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var shi1 = (NativeMethods.SHARE_INFO_1)Marshal.PtrToStructure(ptr, itemType);
                        if ((shi1.shi1_type & stypeIPC) != stypeIPC)
                            yield return shi1;
                        ptr = (IntPtr)(ptr.ToInt32() + itemSize);
                    }
                    PluginNetwork.NETAPI32.NetApiBufferFree(bufPtr);
                }
            } while (result == (int) NativeMethods.NERR.ERROR_MORE_DATA);
        }

        internal static IEnumerable<NativeMethods.WKSTA_USER_INFO_1> NetWkstaUserEnum(string computer)
        {
            if (PluginNetwork.NETAPI32 == null)
                yield break;

            uint resumehandle = 0;
            int result;
            var itemType = typeof (NativeMethods.WKSTA_USER_INFO_1);
            var itemSize = Marshal.SizeOf(itemType);

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                result = PluginNetwork.NETAPI32.NetWkstaUserEnum(computer, 1, out bufPtr, API_BUFFER_SIZE, 
                    out entriesread, out totalentries, ref resumehandle);
                if (result == (int) NativeMethods.NERR.NERR_SUCCESS || result == (int) NativeMethods.NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var item = (NativeMethods.WKSTA_USER_INFO_1)Marshal.PtrToStructure(ptr, itemType);
                        yield return item;
                        ptr = (IntPtr) (ptr.ToInt32() + itemSize);
                    }
                    PluginNetwork.NETAPI32.NetApiBufferFree(bufPtr);
                }
            } while (result == (int)NativeMethods.NERR.ERROR_MORE_DATA);
        }

        [Localizable(false)]
        internal static string[] NetWkstaUserEnumNames(string computer)
        {
            var users = new List<string>();
            var domain = NetWkstaGetInfo(null);
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