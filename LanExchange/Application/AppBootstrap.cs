using System;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;
using LanExchange.Properties;

namespace LanExchange.Application
{
    internal sealed class AppBootstrap : IAppBootstrap
    {
        private readonly IAppView appView;
        private readonly IAddonManager addonManager;
        private readonly IPluginManager pluginManager;
        private readonly ITranslationService translationService;
        private readonly IWindowFactory windowFactory;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogService logService;

        public AppBootstrap(
            IAppView appView,
            IAddonManager addonManager,
            IPluginManager pluginManager,
            ITranslationService translationService,
            IWindowFactory windowFactory,
            IServiceProvider serviceProvider,
            ILogService logService)
        {
            if (appView != null) throw new ArgumentNullException(nameof(appView));
            if (addonManager != null) throw new ArgumentNullException(nameof(addonManager));
            if (pluginManager != null) throw new ArgumentNullException(nameof(pluginManager));
            if (translationService != null) throw new ArgumentNullException(nameof(translationService));
            if (windowFactory != null) throw new ArgumentNullException(nameof(windowFactory));
            if (serviceProvider != null) throw new ArgumentNullException(nameof(serviceProvider));
            if (logService != null) throw new ArgumentNullException(nameof(logService));

            this.appView = appView;
            this.addonManager = addonManager;
            this.pluginManager = pluginManager;
            this.translationService = translationService;
            this.windowFactory = windowFactory;
            this.serviceProvider = serviceProvider;
            this.logService = logService;

            Initialize();
        }

        private void Initialize()
        {
            translationService.SetResourceManagerTo<Resources>();
            LoadPlugins();
            addonManager.LoadAddons();
        }

        private void LoadPlugins()
        {
            try
            {
                pluginManager.LoadPlugins();
            }
            catch (Exception exception)
            {
                logService.Log(exception);
            }
            pluginManager.InitializePlugins(serviceProvider);
        }

        public void Run()
        {
            using (var mainView = windowFactory.CreateMainView())
            {
                appView.Run(mainView);
            }
        }
    }
}