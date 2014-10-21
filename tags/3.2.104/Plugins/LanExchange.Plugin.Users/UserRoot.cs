using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    public sealed class UserRoot : PanelItemRootBase
    {
        private readonly string m_StartPath;

        public UserRoot()
        {
            m_StartPath = LdapUtils.GetUserPath(PluginUsers.ScreenService.UserName);
            m_StartPath = LdapUtils.GetDCNameFromPath(m_StartPath, 2);
        }

        protected override string GetName()
        {
            return LdapUtils.GetLdapValue(m_StartPath);
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