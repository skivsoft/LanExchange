using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ComputerFillerStrategy : IPanelFillerStrategy
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            // computers can be only into domains
            return (parent != null) && (parent != Network.ROOT_OF_DOMAINS) && (parent is DomainPanelItem);
        }

        public void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            result.Add(new PanelItemDoubleDot(parent));
            // get server list via OS api
            foreach (var item in NetApi32Utils.Instance.NetServerEnum(parent.Name, NetApi32.SV_101_TYPES.SV_TYPE_ALL))
            {
                var si = ServerInfo.FromNetApi32(item);
                si.ResetUtcUpdated();
                result.Add(new ComputerPanelItem(parent, si));
            }
        }
    }
}
