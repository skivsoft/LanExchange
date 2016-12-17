using System;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;

namespace LanExchange.Application.Factories
{
    internal sealed class ModelFactory : FactoryBase, IModelFactory
    {
        public ModelFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public IPanelModel CreatePanelModel()
        {
            return ServiceProvider.Resolve<IPanelModel>();
        }
    }
}