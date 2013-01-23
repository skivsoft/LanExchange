using System;

namespace LanExchange.Utils
{
    public class ShareInfo : IComparable<ShareInfo>
    {
        private NetApi32.SHARE_INFO_1 m_Info;

        public ShareInfo(NetApi32.SHARE_INFO_1 info)
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
        }

        public bool IsPrinter
        {
            get { return (m_Info.shi1_type == (uint)NetApi32.SHARE_TYPE.STYPE_PRINTQ); }
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
            return String.CompareOrdinal(Name, other.Name);
        }

        #endregion
    }
}
