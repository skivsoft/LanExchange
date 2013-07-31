using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    class SharePanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new SharePanelItem(parent, name);
        }
    }
}
