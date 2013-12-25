using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public sealed class DomainFiller : PanelFillerBase
    {
        private const string PRIVATE_IP_RANGE1 = "10.0.0.0-10.255.255.255";
        private const string PRIVATE_IP_RANGE2 = "172.16.0.0-172.31.255.255";
        private const string PRIVATE_IP_RANGE3 = "192.168.0.0-192.168.255.255";

        public override bool IsParentAccepted(PanelItemBase parent)
        {
            // domains can be only at root level
            return parent is DomainRoot;
        }

        public override void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            // get domain list via OS api
            foreach (var item in NetApi32Utils.NetServerEnum(null, NativeMethods.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM))
                result.Add(new DomainPanelItem(parent, ServerInfo.FromNetApi32(item)));
        }
    }
}
