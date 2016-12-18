using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Users
{
    public sealed class UserRoot : PanelItemRootBase
    {
        private readonly string startPath;

        public UserRoot()
        {
            startPath = LdapUtils.GetUserPath(PluginUsers.SysInfoService.UserName);
            startPath = LdapUtils.GetDCNameFromPath(startPath, 2);
        }

        public override string ImageName
        {
            get { return PanelImageNames.USER; }
        }

        public override object Clone()
        {
            return new UserRoot();
        }

        protected override string GetName()
        {
            return LdapUtils.GetLdapValue(startPath);
        }
    }
}