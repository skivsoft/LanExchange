// *****************************************************************************
// ROADMAP OF RELEASES
//
// START DATE: Jan 22, 2012
//
// RELEASE 1.0
//   DONE  Basic functional
//
// RELEASE 1.1.0
//   DONE  Autoupdate computer lists
//   DONE  Update counts in status list
//   DONE  Filtering computer items
//   DONE  ContextMenuStrip for top panel
//   DONE  Enum shares after ItemActivate
//
// RELEASE 2.0
//   DONE  Drag&Drop from panel to external app
//   DONE  Sending items to new tab
//   DONE  Classes with IDisposable interface must be a components
//   DONE  MSI installer
//   DONE  Columns sort (name, comment, version)
//   DONE  Help on shortcut keys
//   DONE  Load context menu for panel items from addons 
//   DONE  User list (Users plugin)
// 
// RELEASE 2.11 (Nov, 2013)
//   DONE  Show/Hide panel info option
//   DONE  Show/Hide grid lines option
//   DONE  Open plugin's tab from context menu
//
// RELEASE 2.12 (Dec, 2013)
//   DONE  Changing language without restart program
//   TODO  Network: ping computer before performing action on it
//   DONE  Network: set tab name after changing domain/group
//   DONE  Async enum items 
//   TODO  Cache items
//   TODO  Manual creation of computer items
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//
// RELEASE 2.x
//   TODO  Network: Show several ip addresses if present
//   TODO  Users: Address books as root for users
//   TODO  Users: use fields from ldap in addons like $(AD.employeeID)
//   TODO  Users: set password
//   TODO  Users: Send files to another user
//   TODO  Save/restore sort order for each tab
//   TODO  Sort by ping, ip address, mac address
//   TODO  Ctrl+Left/Ctrl+Right - change form size with phi based step
//   TODO  Changing columns order
//   TODO  Changing font size Ctrl+mouse wheel
//   TODO  Enum files and folders (FS plugin)
//   TODO  Addons editor
//
// RELEASE 2.x
//   TODO  Unit-tests coverage at least 50%
//   TODO  Multi-langual support (Russian, Engligh)
//   TODO  Internal language editor
//   TODO  Move all WMI features to wmi panel plugin
//   TODO  WMI-commands execution with parameters
//   TODO  Platform detection based on Platforms.xml refer
//   TODO  Refactoring for strict Model-View-Presenter pattern
//   TODO  Safe store for passwords
//   TODO  Acceptance level 1: all function Far.Network plugin must be added
//   TODO    Map disk to share
//   TODO    Ask username/password if needed when connect to share
//   TODO  Acceptance level 2: Code Analysis and R# issues must be fixed
//
// *****************************************************************************

using System;
using System.ComponentModel;
using LanExchange.Action;
using LanExchange.Misc;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange
{
    internal static class Program
    {
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
            App.MainView = App.Resolve<IMainView>();
            App.Presenter.View = App.MainView;
            App.Presenter.GlobalTranslateUI();
            App.Presenter.PrepareForm();
            App.MainPages.LoadSettings();
            // run application
            application.Run(App.MainView);
        }

        private static void LoadPlugins()
        {
            var plugins = App.Resolve<IPluginManager>();
            // load os plugins
            plugins.LoadPlugins(PluginType.OS);
            // process cmdline params
            CmdLineProcessor.Processing();
            // load ui plugins
            plugins.LoadPlugins(PluginType.UI);
            var loaded = true;
            try
            {
                App.Images = App.Resolve<IImageManager>();
                App.Addons = App.Resolve<IAddonManager>();
            }
            catch
            {
                loaded = false;
            }
            // exit with exit code 1 if either IImageManager or IAddonManager is not implemented in plugins
            if (!loaded)
                Environment.Exit(1);
            plugins.LoadPlugins(PluginType.Regular);
            // init internal plugin
            (new PluginInternal()).Initialize(App.Resolve<IServiceProvider>());
            // register stage images for icon animation
            AnimationHelper.Register(AnimationHelper.WORKING, Resources.process_working, 16, 16);
        }
    }
}