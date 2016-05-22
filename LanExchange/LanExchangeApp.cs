using LanExchange.Properties;
using LanExchange.Interfaces;
using LanExchange.SDK;
using System;

using LanExchange.Misc;
using LanExchange.Interfaces.Services;
using LanExchange.Model;
using System.Diagnostics.Contracts;

namespace LanExchange
{
    public class LanExchangeApp
    {
        private readonly IImageManager imageManager;
        private readonly IAppPresenter application;

        public LanExchangeApp(
            IConfigPersistenceService configService,
            IImageManager imageManager)
        {
            Contract.Requires<ArgumentNullException>(configService != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);

            this.imageManager = imageManager;

            // global map interfaces to classes
            App.TR.SetResourceManagerTo<Resources>();
            // load plugins
            LoadPlugins();

            // load settings from cfg-file (must be loaded before plugins)
            App.Config = configService.Load<ConfigModel>();
            App.Config.PropertyChanged += App.Presenter.ConfigOnChanged;
            // load addons
            App.Resolve<IAddonManager>().LoadAddons();
            // init application
            application = App.Resolve<IAppPresenter>();
            application.Init();
        }

        public void Run()
        {
            // create main form
            //App.Presenter.ConfigOnChanged(App.Config, new ConfigChangedArgs(ConfigNames.Language));
            App.MainView = App.Resolve<IMainView>();
            App.Presenter.View = App.MainView;
            App.Presenter.PrepareForm();
            App.MainPages.LoadSettings();
            // run application
            application.Run(App.MainView);
        }

        private void LoadPlugins()
        {
            var plugins = App.Resolve<IPluginManager>();
            plugins.LoadPlugins();
            // register stage images for icon animation
            AnimationHelper.Register(imageManager, AnimationHelper.WORKING, Resources.process_working, 16, 16);
        }

    }
}