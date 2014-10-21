using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LanExchange.Plugin.Network.NetApi
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public sealed class WorkstationInfo
    {
        private readonly int m_Id;
        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string m_ComputerName;
        [MarshalAs(UnmanagedType.LPWStr)]
        private readonly string m_LanGroup;
        private readonly int m_VerMajor;
        private readonly int m_VerMinor;

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

        private WorkstationInfo()
        {
        }

        public WorkstationInfo(int id, string computerName, string lanGroup, int verMajor, int verMinor)
        {
            m_Id = id;
            m_ComputerName = computerName;
            m_LanGroup = lanGroup;
            m_VerMajor = verMajor;
            m_VerMinor = verMinor;
        }

        public int Id
        {
            get { return m_Id; }
        }

        public string ComputerName
        {
            get { return m_ComputerName; }
        }

        public string LanGroup
        {
            get { return m_LanGroup; }
        }

        public int VerMajor
        {
            get { return m_VerMajor; }
        }

        public int VerMinor
        {
            get { return m_VerMinor; }
        }
    }
}