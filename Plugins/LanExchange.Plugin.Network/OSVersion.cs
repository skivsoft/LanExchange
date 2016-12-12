using System;
using System.ComponentModel;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    [Serializable]
    public sealed class OSVersion : IComparable<OSVersion>, IComparable
    {
        private uint platformId;
        private uint major;
        private uint minor;
        private uint type;

        public uint Type
        {
            get { return type; }
            set { type = value; }
        }

        public uint PlatformId
        {
            get { return platformId; }
            set { platformId = value; }
        }

        public uint Major
        {
            get { return major; }
            set { major = value; }
        }

        public uint Minor
        {
            get { return minor; }
            set { minor = value; }
        }

        public bool IsDomainController()
        {
            const uint ctrl =
                (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            return (type & ctrl) != 0;
        }

        public bool IsServer()
        {
            const uint srv = (uint)SV_101_TYPES.SV_TYPE_SERVER;
            const uint ctrl =
                (uint)SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            const uint noctrl = (uint)SV_101_TYPES.SV_TYPE_SERVER_NT;
            return (type & srv) != 0 && (type & (ctrl | noctrl)) != 0;
        }

        public bool IsSqlServer()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_SQLSERVER) != 0;
        }

        public bool IsTimeSource()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_TIME_SOURCE) != 0;
        }

        public bool IsPrintServer()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_PRINTQ_SERVER) != 0;
        }

        public bool IsDialInServer()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_DIALIN_SERVER) != 0;
        }

        public bool IsPotentialBrowser()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER) != 0;
        }

        public bool IsBackupBrowser()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_BACKUP_BROWSER) != 0;
        }

        public bool IsMasterBrowser()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_MASTER_BROWSER) != 0;
        }

        public bool IsDfsRoot()
        {
            return (type & (uint)SV_101_TYPES.SV_TYPE_DFS) != 0;
        }

        public int CompareTo(OSVersion other)
        {
            if (other == null) return 1;
            uint u1 = platformId;
            uint u2 = other.platformId;
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
            u1 = major;
            u2 = other.major;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            u1 = minor;
            u2 = other.minor;
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
            //return string.Format("{0}.{1}.{2}.{3}", platform_id, ver_major, ver_minor, type);
            bool bServer = IsServer();
            var platform = (SV_101_PLATFORM) platformId;
            // OS2 same as NT
            if (platform == SV_101_PLATFORM.PLATFORM_ID_OS2)
                platform = SV_101_PLATFORM.PLATFORM_ID_NT;
            switch (platform)
            {
                case SV_101_PLATFORM.PLATFORM_ID_DOS:
                    return string.Format("MS-DOS {0}.{1}", major, minor);
                case SV_101_PLATFORM.PLATFORM_ID_NT:
                    if ((type & (uint)SV_101_TYPES.SV_TYPE_XENIX_SERVER) != 0)
                        return string.Format("Linux Server {0}.{1}", major, minor);
                    switch (major)
                    {
                        case 3:
                            return "Windows NT 3.51";
                        case 4:
                            switch (minor)
                            {
                                case 0:
                                    return "Windows 95";
                                case 10:
                                    return "Windows 98";
                                case 90:
                                    return "Windows ME";
                                default:
                                    return string.Format("Windows NT {0}.{1}", major, minor);
                            }
                        case 5:
                            switch (minor)
                            {
                                case 0:
                                    return bServer ? "Windows Server 2000" : "Windows 2000";
                                case 1:
                                    return "Windows XP";
                                case 2:
                                    return "Windows Server 2003 R2";
                                default:
                                    return string.Format("Windows NT {0}.{1}", major, minor);
                            }
                        case 6:
                            switch (minor)
                            {
                                case 0:
                                    return bServer ? "Windows Server 2008" : "Windows Vista";
                                case 1:
                                    return bServer ? "Windows Server 2008 R2" : "Windows 7";
                                case 2:
                                    return bServer ? "Windows Server 2012" : "Windows 8";
                                case 3:
                                    return bServer ? "Windows Server 2012 R2" : "Windows 8.1";
                                case 4:
                                    return bServer ? "Windows 10 Server" : "Windows 10";
                                default:
                                    return string.Format("Windows NT {0}.{1}", major, minor);
                            }
                        default:
                            return string.Format("Windows NT {0}.{1}", major, minor);
                    }
                case SV_101_PLATFORM.PLATFORM_ID_OSF:
                    return string.Format("OSF {0}.{1}", major, minor);
                case SV_101_PLATFORM.PLATFORM_ID_VMS:
                    return string.Format("VMS {0}.{1}", major, minor);
                default:
                    return string.Format("{0} {1}.{2}", platformId, major, minor);
            }
        }

        //public override int GetHashCode()
        //{
        //   return (int)m_PlatformID ^ (int)m_Major ^ (int)m_Minor ^ (int)m_Type;
        //}
    }
}