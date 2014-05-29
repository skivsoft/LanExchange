using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    public static class NetApiHelper
    {
        private const uint API_BUFFER_SIZE = 32768;

        /// <summary>
        /// Get list of serves of specified domain and specified types.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        [CLSCompliant(false)]
        public static IEnumerable<SERVER_INFO_101> NetServerEnum(string domain, SV_101_TYPES types)
        {
            uint resumeHandle = 0;
            int retval;
            var itemSize = Marshal.SizeOf(typeof (SERVER_INFO_101));
            var result = new List<SERVER_INFO_101>();

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                retval = SafeNativeMethods.NetServerEnum(null, 101, out bufPtr, SafeNativeMethods.MAX_PREFERRED_LENGTH,
                    out entriesread, out totalentries, (uint) types, domain, ref resumeHandle);
                if (retval == (int) NERR.NERR_SUCCESS || retval == (int) NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var item = new SERVER_INFO_101();
                        Marshal.PtrToStructure(ptr, item);
                        result.Add(item);
                        ptr = (IntPtr) (ptr.ToInt64() + itemSize);
                    }
                    SafeNativeMethods.NetApiBufferFree(bufPtr);
                }
            } while (retval == (int) NERR.ERROR_MORE_DATA);
            return result;
        }

        public static IEnumerable<SHARE_INFO_1> NetShareEnum(string computer)
        {
            const uint stypeIpc = (uint)SHARE_TYPE.STYPE_IPC;
            uint resumeHandle = 0;
            int retval;
            var itemSize = Marshal.SizeOf(typeof (SHARE_INFO_1));
            var result = new List<SHARE_INFO_1>();

            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                retval = SafeNativeMethods.NetShareEnum(computer, 1, out bufPtr, API_BUFFER_SIZE,
                    out entriesread, out totalentries, ref resumeHandle);
                if (retval == (int) NERR.NERR_SUCCESS || retval == (int) NERR.ERROR_MORE_DATA)
                {
                    var ptr = bufPtr;
                    for (int i = 0; i < entriesread; i++)
                    {
                        var shi1 = new SHARE_INFO_1();
                        Marshal.PtrToStructure(ptr, shi1);
                        if ((shi1.type & stypeIpc) != stypeIpc)
                            result.Add(shi1);
                        ptr = (IntPtr)(ptr.ToInt64() + itemSize);
                    }
                    SafeNativeMethods.NetApiBufferFree(bufPtr);
                }
            } while (retval == (int) NERR.ERROR_MORE_DATA);
            return result;
        }

        public static IEnumerable<WKSTA_USER_INFO_1> NetWkstaUserEnum(string computer)
        {
            uint resumehandle = 0;
            int retval;
            var itemSize = Marshal.SizeOf(typeof (WKSTA_USER_INFO_1));
            var result = new List<WKSTA_USER_INFO_1>();
            do
            {
                IntPtr bufPtr;
                uint entriesread;
                uint totalentries;
                retval = SafeNativeMethods.NetWkstaUserEnum(computer, 1, out bufPtr, API_BUFFER_SIZE, 
                    out entriesread, out totalentries, ref resumehandle);
                switch (retval)
                {
                    case (int) NERR.ERROR_MORE_DATA:
                    case (int) NERR.NERR_SUCCESS:
                        var ptr = bufPtr;
                        for (int i = 0; i < entriesread; i++)
                        {
                            var item = new WKSTA_USER_INFO_1();
                            Marshal.PtrToStructure(ptr, item);
                            result.Add(item);
                            ptr = (IntPtr) (ptr.ToInt64() + itemSize);
                        }
                        SafeNativeMethods.NetApiBufferFree(bufPtr);
                        break;
                }
            } while (retval == (int)NERR.ERROR_MORE_DATA);
            return result;
        }
    }
}