using System;
using LanExchange.Presentation.Interfaces;
using SimpleInjector;
using SimpleInjector.Diagnostics;

namespace LanExchange
{
    internal sealed class ContainerWrapper : IContainerWrapper
    {
        private readonly Container container;

        public ContainerWrapper(Container container)
        {
            if (container != null) throw new ArgumentNullException(nameof(container));

            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)container).GetService(serviceType);
        }

        public IContainerWrapper RegisterTransient<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService
        {
            container.Register<TService, TImplementation>();
            return this;
        }

        public IContainerWrapper RegisterSingleton<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService
        {
            container.RegisterSingleton<TService, TImplementation>();
            return this;
        }

        public IContainerWrapper Verify()
        {
            SuppressDisposableTransientComponentWarning<IMainView>();
            SuppressDisposableTransientComponentWarning<IAboutView>();
            SuppressDisposableTransientComponentWarning<IFilterView>();
            SuppressDisposableTransientComponentWarning<IEditView>();
            SuppressDisposableTransientComponentWarning<IPanelView>();
            SuppressDisposableTransientComponentWarning<IPanelModel>();
            SuppressDisposableTransientComponentWarning<ICheckAvailabilityWindow>();
            SuppressDisposableTransientComponentWarning<IInfoView>();
            SuppressDisposableTransientComponentWarning<IStatusPanelView>();

            container.Verify();
            return this;
        }
        private void SuppressDisposableTransientComponentWarning<T>()
        {
            container.GetRegistration(typeof(T))
                .Registration
                .SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "manual");
        }
    }
}