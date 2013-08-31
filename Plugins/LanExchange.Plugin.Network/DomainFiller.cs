using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class DomainFiller : IPanelFiller
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            // domains can be only at root level
            return parent == Network.ROOT_OF_DOMAINS;
        }

        public void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            // get domain list via OS api
            foreach (var item in NetApi32Utils.NetServerEnum(null, NativeMethods.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM))
                result.Add(new DomainPanelItem(parent, ServerInfo.FromNetApi32(item)));
        }


        public System.Type GetFillType()
        {
            return typeof (DomainPanelItem);
        }
    }
}
