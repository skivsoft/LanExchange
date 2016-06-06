using LanExchange.Application.Interfaces;
using LanExchange.Plugin.Windows;
using LanExchange.Presentation.Interfaces;

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