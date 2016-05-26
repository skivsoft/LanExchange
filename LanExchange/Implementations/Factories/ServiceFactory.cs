using LanExchange.SDK.Factories;
using LanExchange.SDK;
using LanExchange.Plugin.Windows;

namespace LanExchange.Implementations.Factories
{
    internal sealed class ServiceFactory : IServiceFactory
    {
        public ISysImageListService CreateSysImageListService()
        {
            return new SysImageListService();
        }
    }
}