// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using LanExchange.Interfaces;
using LanExchange.Ioc;
using LanExchange.Misc;
using LanExchange.Plugin;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange
{
    internal static class Program
    {
        public static long LoadPluginsMS;
        private static string s_LibPath;

        [STAThread]
        [Localizable(false)]
        static void Main()
        {
            SubscribeAssemblyResolver();
            SubMain();
        }

        static void SubscribeAssemblyResolver()
        {
            s_LibPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (s_LibPath != null)
                s_LibPath = Path.Combine(s_LibPath, "Lib");
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        [Localizable(false)]
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = args.Name.Substring(0, args.Name.IndexOf(','));
            if (name.EndsWith(".resources")) return null;
            if (name.EndsWith(".XmlSerializers")) return null;
            var filePath = Path.Combine(s_LibPath, name + ".dll");
            return Assembly.LoadFrom(filePath);
        }

        static void SubMain()
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