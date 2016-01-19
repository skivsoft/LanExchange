// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using LanExchange.Properties;
using LanExchange.Interfaces;
using LanExchange.SDK;
using System;
using System.Windows.Forms;

namespace LanExchange
{
    public class LanExchangeApp : IDisposable
    {
        public LanExchangeApp()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void Run()
        {
            // global map interfaces to classes
            App.SetContainer(ContainerBuilder.Build());
            App.TR.SetResourceManagerTo<Resources>();
            // load plugins
            // load settings from cfg-file (must be loaded before plugins)
            App.Config.Load();
            App.Config.Changed += App.Presenter.ConfigOnChanged;
            // load addons
            App.Resolve<IAddonManager>().LoadAddons();
            // init application
            var application = App.Resolve<IAppPresenter>();
            application.Init();
           
            // create main form
            App.Presenter.ConfigOnChanged(App.Config, new ConfigChangedArgs(ConfigNames.Language));
            App.MainView = App.Resolve<IMainView>();
            App.Presenter.View = App.MainView;
            App.Presenter.PrepareForm();
            App.MainPages.LoadSettings();
            // run application
            application.Run(App.MainView);
        }

        public void Dispose()
        {
        }
    }
}
