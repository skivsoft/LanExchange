using LanExchange.SDK.Factories;
using System;
using LanExchange.SDK;
using LanExchange.SDK.Extensions;

namespace LanExchange.Implementations.Factories
{
    internal sealed class ModelFactory : FactoryBase, IModelFactory
    {
        public ModelFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelModel CreatePanelModel()
        {
            return serviceProvider.Resolve<IPanelModel>();
        }
    }
}