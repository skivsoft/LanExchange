using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace LanExchange.Plugin.Network
{
    public class NetApi32Utils
    {
        private static NetApi32Utils s_Instance;

        private NetApi32Utils()
        {
        }

        public static NetApi32Utils Instance
        {
            get { return s_Instance ?? (s_Instance = new NetApi32Utils()); }
        }

        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        [EnvironmentPermissionAttribute(SecurityAction.LinkDemand, Unrestricted = true)]
        public string GetMachineNetBiosDomain(string server)
        {
            IntPtr pBuffer;

            var retval = NativeMethods.NetWkstaGetInfo(server, 100, out pBuffer);
            if (retval != 0)
                throw new Win32Exception(retval);
            string domainName;
            try
            {
                var info = (NativeMethods.WKSTA_INFO_100)Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.WKSTA_INFO_100));
                domainName = info.wki100_langroup;
            }
            finally
            {
                NativeMethods.NetApiBufferFree(pBuffer);
            }
            return domainName;
        }

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public IEnumerable<NativeMethods.SERVER_INFO_101> NetServerEnum(string domain, NativeMethods.SV_101_TYPES types)
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

        public IEnumerable<NativeMethods.SHARE_INFO_1> NetShareEnum(string computer)
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
    }
}