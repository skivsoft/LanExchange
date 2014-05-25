using System;
using System.Windows.NetApi;

namespace LanExchange.Plugin.Network
{
    public sealed class ShareInfo : IComparable<ShareInfo>
    {
        private SHARE_INFO_1 m_Info;

        public ShareInfo()
        {
        }

        public ShareInfo(SHARE_INFO_1 info)
        {
            m_Info = info;
        }

        public string Name
        {
            get { return m_Info.shi1_netname; }
            set { m_Info.shi1_netname = value; }
        }

        public string Comment
        {
            get { return m_Info.shi1_remark; }
            set { m_Info.shi1_remark = value; }
        }

        public uint ShareType
        {
            get { return m_Info.shi1_type; }
            set { m_Info.shi1_type = value; }
        }

        public bool IsPrinter
        {
            get { return (m_Info.shi1_type == (uint)SHARE_TYPE.STYPE_PRINTQ); }
        }

        public bool IsHidden
        {
            get
            {
                if (!String.IsNullOrEmpty(m_Info.shi1_netname))
                    return m_Info.shi1_netname[m_Info.shi1_netname.Length - 1] == '$';
                return false;
            }
        }

        #region IComparable<ShareInfo> implementation

        public int CompareTo(ShareInfo other)
        {
            return String.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
