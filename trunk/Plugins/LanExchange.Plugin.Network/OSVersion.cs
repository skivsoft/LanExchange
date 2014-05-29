using System;
using System.ComponentModel;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class OSVersion : IComparable<OSVersion>, IComparable
    {
        private uint m_PlatformID;
        private uint m_Major;
        private uint m_Minor;
        private uint m_Type;

        public uint Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public uint PlatformID
        {
            get { return m_PlatformID; }
            set { m_PlatformID = value; }
        }

        public uint Major
        {
            get { return m_Major; }
            set { m_Major = value; }
        }

        public uint Minor
        {
            get { return m_Minor; }
            set { m_Minor = value; }
        }

        public bool IsDomainController()
        {
            const uint ctrl =
                (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            return (m_Type & ctrl) != 0;
        }

        public bool IsServer()
        {
            const uint srv = (uint)SV_101_TYPES.SV_TYPE_SERVER;
            const uint ctrl =
                (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            const uint noctrl = (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            return (m_Type & srv) != 0 && (m_Type & (ctrl | noctrl)) != 0;
        }

        public bool IsSQLServer()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_SQLSERVER) != 0;
        }

        public bool IsTimeSource()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_TIME_SOURCE) != 0;
        }

        public bool IsPrintServer()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_PRINTQ_SERVER) != 0;
        }

        public bool IsDialInServer()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_DIALIN_SERVER) != 0;
        }

        public bool IsPotentialBrowser()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER) != 0;
        }

        public bool IsBackupBrowser()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_BACKUP_BROWSER) != 0;
        }

        public bool IsMasterBrowser()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_MASTER_BROWSER) != 0;
        }

        public bool IsDFSRoot()
        {
            return (m_Type & (uint)SV_101_TYPES.SV_TYPE_DFS) != 0;
        }

        public int CompareTo(OSVersion other)
        {
            if (other == null) return 1;
            uint u1 = m_PlatformID;
            uint u2 = other.m_PlatformID;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            bool s1 = IsServer();
            bool c1 = IsDomainController();
            bool s2 = other.IsServer();
            bool c2 = other.IsDomainController();
            if (!s1 && s2) return -1;
            if (s1 && !s2) return 1;
            if (!c1 && c2) return 1;
            if (c1 && !c2) return -1;
            u1 = m_Major;
            u2 = other.m_Major;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            u1 = m_Minor;
            u2 = other.m_Minor;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            return 0;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj as OSVersion);
        }

        /// <summary>
        /// Returns name and version of operation system.
        /// </summary>
        /// <returns></returns>
        [Localizable(false)]
        public override string ToString()
        {
            //return String.Format("{0}.{1}.{2}.{3}", platform_id, ver_major, ver_minor, type);
            bool bServer = IsServer();
            var platform = (SV_101_PLATFORM) m_PlatformID;
            // OS2 same as NT
            if (platform == SV_101_PLATFORM.PLATFORM_ID_OS2)
                platform = SV_101_PLATFORM.PLATFORM_ID_NT;
            switch (platform)
            {
                case SV_101_PLATFORM.PLATFORM_ID_DOS:
                    return String.Format("MS-DOS {0}.{1}", m_Major, m_Minor);
                case SV_101_PLATFORM.PLATFORM_ID_NT:
                    if ((m_Type & (uint)SV_101_TYPES.SV_TYPE_XENIX_SERVER) != 0)
                        return String.Format("Linux Server {0}.{1}", m_Major, m_Minor);
                    switch (m_Major)
                    {
                        case 3:
                            return "Windows NT 3.51";
                        case 4:
                            switch (m_Minor)
                            {
                                case 0:
                                    return "Windows 95";
                                case 10:
                                    return "Windows 98";
                                case 90:
                                    return "Windows ME";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Major, m_Minor);
                            }
                        case 5:
                            switch (m_Minor)
                            {
                                case 0:
                                    return bServer ? "Windows Server 2000" : "Windows 2000";
                                case 1:
                                    return "Windows XP";
                                case 2:
                                    return "Windows Server 2003 R2";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Major, m_Minor);
                            }
                        case 6:
                            switch (m_Minor)
                            {
                                case 0:
                                    return bServer ? "Windows Server 2008" : "Windows Vista";
                                case 1:
                                    return bServer ? "Windows Server 2008 R2" : "Windows 7";
                                case 2:
                                    return bServer ? "Windows Server 2012" : "Windows 8";
                                case 3:
                                    return bServer ? "Windows Server 2012 R2" : "Windows 8.1";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Major, m_Minor);
                            }
                        default:
                            return String.Format("Windows NT {0}.{1}", m_Major, m_Minor);
                    }
                case SV_101_PLATFORM.PLATFORM_ID_OSF:
                    return String.Format("OSF {0}.{1}", m_Major, m_Minor);
                case SV_101_PLATFORM.PLATFORM_ID_VMS:
                    return String.Format("VMS {0}.{1}", m_Major, m_Minor);
                default:
                    return String.Format("{0} {1}.{2}", m_PlatformID, m_Major, m_Minor);
            }
        }

        //public override int GetHashCode()
        //{
        //   return (int)m_PlatformID ^ (int)m_Major ^ (int)m_Minor ^ (int)m_Type;
        //}
    }
}