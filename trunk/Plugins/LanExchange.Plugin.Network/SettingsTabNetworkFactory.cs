using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    class SettingsTabNetworkFactory : ISettingsTabViewFactory
    {
        public ISettingsTabView Create()
        {
            return new SettingsTabNetwork();
        }
    }
}
