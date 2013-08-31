using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class ComputerPanelItemFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ComputerPanelItem(parent, name);
        }
    }
}
