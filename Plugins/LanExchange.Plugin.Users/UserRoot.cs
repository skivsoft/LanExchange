using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class UserRoot : PanelItemRootBase
    {
        private readonly string startPath;

        public UserRoot()
        {
            startPath = LdapUtils.GetUserPath(PluginUsers.sysInfoService.UserName);
            startPath = LdapUtils.GetDCNameFromPath(startPath, 2);
        }

        protected override string GetName()
        {
            return LdapUtils.GetLdapValue(startPath);
        }

        public override string ImageName
        {
            get { return PanelImageNames.USER; }
        }

        public override object Clone()
        {
            return new UserRoot();
        }
    }
}