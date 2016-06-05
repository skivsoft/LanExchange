using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.WinForms.Controls;
using LanExchange.Presentation.WinForms.Forms;

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

            // Application
            container.RegisterSingleton<IAppView, AppView>();

            // Controls
            container.RegisterTransient<IInfoView, InfoPanel>();
            container.RegisterTransient<IStatusPanelView, StatusPanel>();
            container.RegisterTransient<IFilterView, FilterView>();
            container.RegisterTransient<IPanelView, PanelView>();
            container.RegisterSingleton<IPagesView, PagesView>();

            // Forms
            container.RegisterTransient<IAboutView, AboutForm>();
            container.RegisterTransient<IEditView, EditForm>();
            container.RegisterTransient<ICheckAvailabilityWindow, CheckAvailabilityForm>();
            container.RegisterTransient<IMainView, MainForm>();

            // TODO remove singleton dependency on PagesPresenter
            container.RegisterSingleton<IClipboardService, ClipboardService>();
            container.RegisterTransient<ISystemInformationService, SystemInformationService>();
            // TODO remove singleton dependency on MainPresenter
            container.RegisterSingleton<IWaitingService, WaitingService>();

            container.RegisterSingleton<IAddonManager, AddonManager>();
            container.RegisterSingleton<IMessageBoxService, MessageBoxService>();


            return container;
        }
    }
}