using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LanExchange.Utils
{
    public static class NetApi32Utils
    {
        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string GetMachineNetBiosDomain(string server)
        {
            IntPtr pBuffer;

            var retval = NetApi32.NetWkstaGetInfo(server, 100, out pBuffer);
            if (retval != 0)
                throw new Win32Exception(retval);
            string domainName = null;
            try
            {
                var info = (NetApi32.WKSTA_INFO_100)Marshal.PtrToStructure(pBuffer, typeof(NetApi32.WKSTA_INFO_100));
                domainName = info.wki100_langroup;
            }
            finally
            {
                NetApi32.NetApiBufferFree(pBuffer);
            }
            return domainName;
        }

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static IEnumerable<NetApi32.SERVER_INFO_101> NetServerEnum(string domain, NetApi32.SV_101_TYPES types)
        {
            IntPtr pInfo;
            uint entriesread = 0;
            uint totalentries = 0;
            NetApi32.NERR err;
            unchecked
            {
                err = NetApi32.NetServerEnum(null, 101, out pInfo, (uint)-1, ref entriesread, ref totalentries, types, domain, 0);
            }
            if ((err == NetApi32.NERR.NERR_SUCCESS || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                try
                {
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        yield return (NetApi32.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NetApi32.SERVER_INFO_101));
                        ptr += Marshal.SizeOf(typeof (NetApi32.SERVER_INFO_101));
                    }
                }
                finally
                {
                    if (pInfo != IntPtr.Zero)
                        NetApi32.NetApiBufferFree(pInfo);
                }
        }

        public static IEnumerable<NetApi32.SHARE_INFO_1> NetShareEnum(string computer)
        {
            IntPtr pInfo;
            int entriesread = 0;
            int totalentries = 0;
            NetApi32.NERR err = NetApi32.NetShareEnum(computer, 1, out pInfo, NetApi32.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, 0);
            if ((err == NetApi32.NERR.NERR_SUCCESS || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                try
                {
                    const uint stypeIPC = (uint)NetApi32.SHARE_TYPE.STYPE_IPC;
                    int ptr = pInfo.ToInt32();
                    for (int i = 0; i < entriesread; i++)
                    {
                        var shi1 = (NetApi32.SHARE_INFO_1)Marshal.PtrToStructure(new IntPtr(ptr), typeof (NetApi32.SHARE_INFO_1));
                        if ((shi1.shi1_type & stypeIPC) != stypeIPC)
                            yield return shi1;
                        ptr += Marshal.SizeOf(typeof (NetApi32.SHARE_INFO_1));
                    }
                }
                finally
                {
                    if (pInfo != IntPtr.Zero)
                        NetApi32.NetApiBufferFree(pInfo);
                }
        }
    }
}
