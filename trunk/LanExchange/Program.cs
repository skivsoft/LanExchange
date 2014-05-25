// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using LanExchange.Action;
using LanExchange.Misc;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange
{
    internal static class Program
    {
        public static long LoadPluginsMS;

        [STAThread]
        [Localizable(false)]
        static void Main()
        {
            // global map interfaces to classes
            App.SetContainer(ContainerBuilder.Build());
            App.TR.SetResourceManagerTo<Resources>();
            // load plugins
            LoadPlugins();
            // load settings from cfg-file (must be loaded before plugins)
            App.Config.Load();
            App.Config.Changed += App.Presenter.ConfigOnChanged;
            // load addons
            App.Addons.LoadAddons();
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

        private static void LoadPlugins()
        {
            var sw = new Stopwatch();
            sw.Start();
            var plugins = App.Resolve<IPluginManager>();
            // process cmdline params
            CmdLineProcessor.Processing();
            plugins.LoadPlugins();
            // init internal plugin
            (new PluginInternal()).Initialize(App.Resolve<IServiceProvider>());
            // register stage images for icon animation
            AnimationHelper.Register(AnimationHelper.WORKING, Resources.process_working, 16, 16);
            LoadPluginsMS = sw.ElapsedMilliseconds;
        }
    }
}