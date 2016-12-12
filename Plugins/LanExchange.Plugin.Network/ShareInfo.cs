using System;
using LanExchange.Plugin.Network.NetApi;

namespace LanExchange.Plugin.Network
{
    public sealed class ShareInfo : IComparable<ShareInfo>
    {
        private readonly SHARE_INFO_1 shareInfo;

        public ShareInfo()
        {
            shareInfo = new SHARE_INFO_1();
        }

        public ShareInfo(SHARE_INFO_1 info)
        {
            shareInfo = info;
        }

        public string Name
        {
            get { return shareInfo.netname; }
            set { shareInfo.netname = value; }
        }

        public string Comment
        {
            get { return shareInfo.remark; }
            set { shareInfo.remark = value; }
        }

        public uint ShareType
        {
            get { return shareInfo.type; }
            set { shareInfo.type = value; }
        }

        public bool IsPrinter
        {
            get { return shareInfo.type == (uint)SHARE_TYPE.STYPE_PRINTQ; }
        }

        public bool IsHidden
        {
            get
            {
                if (!string.IsNullOrEmpty(shareInfo.netname))
                    return shareInfo.netname[shareInfo.netname.Length - 1] == '$';
                return false;
            }
        }

        #region IComparable<ShareInfo> implementation

        public int CompareTo(ShareInfo other)
        {
            return string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        #endregion
    }
}
