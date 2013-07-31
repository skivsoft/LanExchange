using LanExchange.SDK;

namespace LanExchange.Plugin.Users
{
    class SettingsTabUsersFactory : ISettingsTabViewFactory
    {
        public ISettingsTabView Create()
        {
            return new SettingsTabUsers();
        }
    }
}
