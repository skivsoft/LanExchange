using LanExchange.SDK;

namespace LanExchange.Plugin.Users.Panel
{
    class UserPanelItemFactory : PanelItemBaseFactory
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new UserPanelItem(parent, name);
        }
    }
}
