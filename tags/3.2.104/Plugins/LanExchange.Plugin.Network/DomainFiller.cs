using System.Collections.Generic;
using LanExchange.Plugin.Network.NetApi;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public sealed class DomainFiller : IPanelFiller
    {
        //private const string PRIVATE_IP_RANGE1 = "10.0.0.0-10.255.255.255";
        //private const string PRIVATE_IP_RANGE2 = "172.16.0.0-172.31.255.255";
        //private const string PRIVATE_IP_RANGE3 = "192.168.0.0-192.168.255.255";

        public bool IsParentAccepted(PanelItemBase parent)
        {
            // domains can be only at root level
            return parent is DomainRoot;
        }

        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            // get domain list via OS api
            foreach (var item in NetApiHelper.NetServerEnum(null, SV_101_TYPES.SV_TYPE_DOMAIN_ENUM))
                result.Add(new DomainPanelItem(parent, ServerInfo.FromNetApi32(item)));
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }
    }
}
