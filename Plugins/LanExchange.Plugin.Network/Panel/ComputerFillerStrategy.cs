using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ComputerFillerStrategy : IPanelFillerStrategy
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            // computers can be only into domains
            return (parent != null) && (parent.GetType() == typeof (DomainPanelItem));
        }

        public void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            // get server list via OS api
            var list = NetApi32Utils.Instance.NetServerEnum(parent.Name, NetApi32.SV_101_TYPES.SV_TYPE_ALL);
            // convert array to IList<ServerInfo>
            foreach (var item in list)
            {
                var SI = ServerInfo.FromNetApi32(item);
                SI.ResetUtcUpdated();
                result.Add(new ComputerPanelItem(parent, SI));
            }
        }
    }
}
