using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ComputerPanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ComputerPanelItem(parent, name);
        }
    }
}
