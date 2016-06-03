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

            container.RegisterTransient<IClipboardService, ClipboardService>();
            container.RegisterTransient<ISystemInformationService, SystemInformationService>();
            container.RegisterTransient<IWaitingService, WaitingService>();

            return container;
        }
    }
}