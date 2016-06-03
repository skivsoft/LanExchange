using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.Factories;
using LanExchange.Plugin.Windows;

namespace LanExchange.Application.Factories
{
    internal sealed class ServiceFactory : IServiceFactory
    {
        public ISysImageListService CreateSysImageListService()
        {
            return new SysImageListService();
        }
    }
}