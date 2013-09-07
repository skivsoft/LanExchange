using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    class UserPanelItemFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new UserPanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            var root = Users.ROOT_OF_DNS;
            root.Name = "QQQ";
            return root;
        }
    }
}
