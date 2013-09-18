using System.Windows.Forms;
using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    internal sealed class UserPanelItemFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new UserPanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            var startPath = LdapUtils.GetUserPath(SystemInformation.UserName);
            startPath = LdapUtils.GetDCNameFromPath(startPath, 2);
            var root = PluginUsers.ROOT_OF_DNS;
            root.Name = LdapUtils.GetLdapValue(startPath);
            return root;
        }
    }
}
