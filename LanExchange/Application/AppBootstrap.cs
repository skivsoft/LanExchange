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
            this.appView = appView ?? throw new ArgumentNullException(nameof(appView));
            this.addonManager = addonManager ?? throw new ArgumentNullException(nameof(addonManager));
            this.pluginManager = pluginManager ?? throw new ArgumentNullException(nameof(pluginManager));
            this.translationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
            this.windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.logService = logService ?? throw new ArgumentNullException(nameof(logService));

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