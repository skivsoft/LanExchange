using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LanExchange.Network
{
    class Utils
    {
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
    }
}
