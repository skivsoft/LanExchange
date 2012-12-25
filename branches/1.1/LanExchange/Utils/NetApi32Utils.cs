using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LanExchange.Utils
{
    class NetApi32Utils
    {
        /// <summary>
        /// Get domain name for specified machine.
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public static string GetMachineNetBiosDomain(string server)
        {
            IntPtr pBuffer = IntPtr.Zero;

            NetApi32.WKSTA_INFO_100 info;
            int retval = NetApi32.NetWkstaGetInfo(server, 100, out pBuffer);
            if (retval != 0)
                throw new Win32Exception(retval);

            info = (NetApi32.WKSTA_INFO_100)Marshal.PtrToStructure(pBuffer, typeof(NetApi32.WKSTA_INFO_100));
            string domainName = info.wki100_langroup;
            NetApi32.NetApiBufferFree(pBuffer);
            return domainName;
        }

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public static NetApi32.SERVER_INFO_101[] GetServerList(string domain, NetApi32.SV_101_TYPES types)
        {
            NetApi32.SERVER_INFO_101[] Result = new NetApi32.SERVER_INFO_101[0];
            var pInfo = IntPtr.Zero;
            int entriesread = 0;
            int totalentries = 0;
            NetApi32.NERR err = NetApi32.NetServerEnum(null, 101, out pInfo, -1, ref entriesread, ref totalentries, types, domain, 0);
            try
            {
                if ((err == NetApi32.NERR.NERR_Success || err == NetApi32.NERR.ERROR_MORE_DATA) && pInfo != IntPtr.Zero)
                {
                    int ptr = pInfo.ToInt32();
                    Result = new NetApi32.SERVER_INFO_101[entriesread];
                    for (int i = 0; i < entriesread; i++)
                    {
                        Result[i] = (NetApi32.SERVER_INFO_101)Marshal.PtrToStructure(new IntPtr(ptr), typeof(NetApi32.SERVER_INFO_101));
                        ptr += Marshal.SizeOf(typeof(NetApi32.SERVER_INFO_101));
                    }
                }
            }
            finally
            {
                if (pInfo != IntPtr.Zero)
                    NetApi32.NetApiBufferFree(pInfo);
            }
            return Result;
        }

        /// <summary>
        /// Get domain list.
        /// </summary>
        /// <returns></returns>
        public static NetApi32.SERVER_INFO_101[] GetDomainList()
        {
            return GetServerList(null, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
        }

        /// <summary>
        /// Get computer list of specified domain.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public static NetApi32.SERVER_INFO_101[] GetComputerList(string domain)
        {
            return GetServerList(domain, NetApi32.SV_101_TYPES.SV_TYPE_ALL);
        }
    }
}
