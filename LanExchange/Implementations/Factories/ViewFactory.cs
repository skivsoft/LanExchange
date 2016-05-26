using LanExchange.SDK.Factories;
using System;
using LanExchange.SDK;
using LanExchange.SDK.Extensions;

namespace LanExchange.Implementations.Factories
{
    internal sealed class ViewFactory : FactoryBase, IViewFactory
    {
        public ViewFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelView CreatePanelView()
        {
            return serviceProvider.Resolve<IPanelView>();
        }

        public IPagesView GetPagesView()
        {
            return serviceProvider.Resolve<IPagesView>();
        }
    }
}