using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Controls;

namespace LanExchange.Presentation.WinForms
{
    /// <summary>
    /// LanExchange core facade class.
    /// </summary>
    public static class PresentationFacade
    {
        public static IContainerWrapper RegisterPresentationLayer(this IContainerWrapper container)
        {
            Contract.Requires<ArgumentNullException>(container != null);

            container.RegisterTransient<IInfoView, InfoPanel>();
            container.RegisterTransient<IStatusPanelView, StatusPanel>();
            container.RegisterTransient<IFilterView, FilterView>();
            container.RegisterTransient<IPanelView, PanelView>();
            container.RegisterSingleton<IPagesView, PagesView>();
            container.RegisterSingleton<IAppView, AppView>();

            // TODO remove singleton dependency on PagesPresenter
            container.RegisterSingleton<IClipboardService, ClipboardService>();
            container.RegisterTransient<ISystemInformationService, SystemInformationService>();
            // TODO remove singleton dependency on MainPresenter
            container.RegisterSingleton<IWaitingService, WaitingService>();


            return container;
        }
    }
}