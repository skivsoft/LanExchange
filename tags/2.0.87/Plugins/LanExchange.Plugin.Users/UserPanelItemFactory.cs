using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    class UserPanelItemFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new UserPanelItem(parent, name);
        }
    }
}
