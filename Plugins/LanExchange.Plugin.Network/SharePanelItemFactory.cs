using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class SharePanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new SharePanelItem(parent, name);
        }
    }
}
