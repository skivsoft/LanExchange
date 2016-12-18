using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public sealed class WorkstationInfo
    {
        private readonly int id;
        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string computerName;
        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string lanGroup;
        private readonly int verMajor;
        private readonly int verMinor;

        public WorkstationInfo(int id, string computerName, string lanGroup, int verMajor, int verMinor)
        {
            this.id = id;
            this.computerName = computerName;
            this.lanGroup = lanGroup;
            this.verMajor = verMajor;
            this.verMinor = verMinor;
        }

        private WorkstationInfo()
        {
        }

        public int Id
        {
            get { return id; }
        }

        public string ComputerName
        {
            get { return computerName; }
        }

        public string LanGroup
        {
            get { return lanGroup; }
        }

        public int VerMajor
        {
            get { return verMajor; }
        }

        public int VerMinor
        {
            get { return verMinor; }
        }

        public static WorkstationInfo FromComputer(string computerName)
        {
            IntPtr buffer;
            var retval = SafeNativeMethods.NetWkstaGetInfo(computerName, 100, out buffer);
            if (retval != 0)
                throw new Win32Exception(retval);
            var result = new WorkstationInfo();
            try
            {
                Marshal.PtrToStructure(buffer, result);
            }
            finally
            {
                SafeNativeMethods.NetApiBufferFree(buffer);
            }

            return result;
        }
    }
}