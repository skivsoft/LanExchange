using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Factories
{
    internal sealed class ViewFactory : FactoryBase, IViewFactory
    {
        /// <exception cref="ArgumentNullException"></exception>
        public ViewFactory(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public IPanelView CreatePanelView()
        {
            return Resolve<IPanelView>();
        }

        public IPagesView GetPagesView()
        {
            return Resolve<IPagesView>();
        }

        public IStatusPanelView CreateStatusPanelView()
        {
            return Resolve<IStatusPanelView>();
        }
    }
}