using System;

namespace LanExchange.Utils
{
    [Serializable]
    public class ServerInfo : IComparable<ServerInfo>
    {
        private readonly NetApi32.SERVER_INFO_101 m_Info;

        public ServerInfo(NetApi32.SERVER_INFO_101 info)
        {
            m_Info = info;
        }

        public ServerInfo(string name, string comment)
        {
            m_Info = new NetApi32.SERVER_INFO_101();
            m_Info.sv101_name = name;
            m_Info.sv101_comment = comment;
        }

        public ServerInfo(string name)
        {
            m_Info = new NetApi32.SERVER_INFO_101();
            m_Info.sv101_name = name;
        }

        public string Name
        {
            get { return m_Info.sv101_name; }
        }

        public string Comment
        {
            get { return m_Info.sv101_comment; }
        }

        /// <summary>
        /// Returns name and version of operation system.
        /// </summary>
        /// <returns></returns>
        public string Version()
        {
            //return String.Format("{0}.{1}.{2}.{3}", platform_id, ver_major, ver_minor, type);
            bool bServer = IsServer();
            switch ((NetApi32.SV_101_PLATFORM)m_Info.sv101_platform_id)
            {
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_DOS:
                    return String.Format("MS-DOS {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_OS2:
                    switch (m_Info.sv101_version_major)
                    {
                        case 3: return "Windows NT 3.51";
                        case 4:
                            switch (m_Info.sv101_version_minor)
                            {
                                case 0: return "Windows 95";
                                case 10: return "Windows 98";
                                case 90: return "Windows ME";
                                default:
                                    return "Windows 9x";
                            }
                        default:
                            return String.Format("Windows NT {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                    }
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_NT:
                    if ((m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_XENIX_SERVER) != 0)
                        return String.Format("Linux Server {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                    switch (m_Info.sv101_version_major)
                    {
                        case 3: return "Windows NT 3.51";
                        case 4:
                            switch (m_Info.sv101_version_minor)
                            {
                                case 0: return "Windows 95";
                                case 10: return "Windows 98";
                                case 90: return "Windows ME";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                            }
                        case 5:
                            switch (m_Info.sv101_version_minor)
                            {
                                case 0: return bServer ? "Windows Server 2000" : "Windows 2000";
                                case 1: return "Windows XP";
                                case 2: return "Windows Server 2003 R2";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                            }
                        case 6:
                            switch (m_Info.sv101_version_minor)
                            {
                                case 0: return bServer ? "Windows Server 2008" : "Windows Vista";
                                case 1: return bServer ? "Windows Server 2008 R2" : "Windows 7";
                                case 2: return bServer ? "Windows 8 Server" : "Windows 8";
                                default:
                                    return String.Format("Windows NT {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                            }
                        default:
                            return String.Format("Windows NT {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                    }
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_OSF:
                    return String.Format("OSF {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                case NetApi32.SV_101_PLATFORM.PLATFORM_ID_VMS:
                    return String.Format("VMS {0}.{1}", m_Info.sv101_version_major, m_Info.sv101_version_minor);
                default:
                    return String.Format("{0} {1}.{2}", m_Info.sv101_platform_id, m_Info.sv101_version_major, m_Info.sv101_version_minor);
            }
        }

        public bool IsDomainController()
        {
            const uint ctrl = (uint)NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            return (m_Info.sv101_type & ctrl) != 0;
        }

        public bool IsServer()
        {
            const uint srv = (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER;
            const uint ctrl = (uint)NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_CTRL | (uint)NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_BAKCTRL;
            const uint noctrl = (uint)NetApi32.SV_101_TYPES.SV_TYPE_SERVER_NT;
            return (m_Info.sv101_type & srv) != 0 && (m_Info.sv101_type & (ctrl | noctrl)) != 0;
        }

        public bool IsSQLServer()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_SQLSERVER) != 0;
        }

        public bool IsTimeSource()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_TIME_SOURCE) != 0;
        }

        public bool IsPrintServer()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_PRINTQ_SERVER) != 0;
        }

        public bool IsDialInServer()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_DIALIN_SERVER) != 0;
        }

        public bool IsPotentialBrowser()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_POTENTIAL_BROWSER) != 0;
        }

        public bool IsBackupBrowser()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_BACKUP_BROWSER) != 0;
        }

        public bool IsMasterBrowser()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_MASTER_BROWSER) != 0;
        }

        public bool IsDFSRoot()
        {
            return (m_Info.sv101_type & (uint)NetApi32.SV_101_TYPES.SV_TYPE_DFS) != 0;
        }

        public int CompareVersionTo(object obj)
        {
            var comp = obj as ServerInfo;
            if (comp == null) return 1;
            uint u1 = m_Info.sv101_platform_id;
            uint u2 = comp.m_Info.sv101_platform_id;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            bool s1 = IsServer();
            bool c1 = IsDomainController();
            bool s2 = comp.IsServer();
            bool c2 = comp.IsDomainController();
            if (!s1 && s2) return -1;
            if (s1 && !s2) return 1;
            if (!c1 && c2) return 1;
            if (c1 && !c2) return -1;
            u1 = m_Info.sv101_version_major;
            u2 = comp.m_Info.sv101_version_major;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            u1 = m_Info.sv101_version_minor;
            u2 = comp.m_Info.sv101_version_minor;
            if (u1 < u2) return -1;
            if (u1 > u2) return 1;
            return 0;
        }

        public NetApi32.SERVER_INFO_101 GetInfo()
        {
            return m_Info;
        }

        #region IComparable Members

        public int CompareTo(ServerInfo other)
        {
            return String.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        #endregion
    }
}
