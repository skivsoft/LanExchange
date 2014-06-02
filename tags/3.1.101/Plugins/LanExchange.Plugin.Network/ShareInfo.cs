using System;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    public sealed class ShareInfo : IComparable<ShareInfo>
    {
        private readonly SHARE_INFO_1 m_Info;

        public ShareInfo()
        {
            m_Info = new SHARE_INFO_1();
        }

        public ShareInfo(SHARE_INFO_1 info)
        {
            m_Info = info;
        }

        public string Name
        {
            get { return m_Info.netname; }
            set { m_Info.netname = value; }
        }

        public string Comment
        {
            get { return m_Info.remark; }
            set { m_Info.remark = value; }
        }

        public uint ShareType
        {
            get { return m_Info.type; }
            set { m_Info.type = value; }
        }

        public bool IsPrinter
        {
            get { return (m_Info.type == (uint)SHARE_TYPE.STYPE_PRINTQ); }
        }

        public bool IsHidden
        {
            get
            {
                if (!String.IsNullOrEmpty(m_Info.netname))
                    return m_Info.netname[m_Info.netname.Length - 1] == '$';
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
