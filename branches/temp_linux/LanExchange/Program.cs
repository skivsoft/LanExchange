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
//   TODO  Network: set tab name after changing domain/group
//   TODO  Async enum items and cache items
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
using LanExchange.Core;
using LanExchange.Intf;
using LanExchange.Misc;
using LanExchange.Presenter.Action;
using LanExchange.SDK;
using LanExchange.SDK.UI;

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
            App.TR.SetResourceManagerTo<Properties.Resources>();
            // process cmdline params
            CmdLineProcessor.Processing();
            // load settings from cfg-file (must be loaded before plugins)
            App.Config.Changed += App.Presenter.ConfigOnChanged;
            App.Config.Load();
            // load plugins
            var plugins = App.Resolve<IPluginManager>();
            plugins.LoadPlugins(PluginType.OS);
            plugins.LoadPlugins(PluginType.UI);
            plugins.LoadPlugins(PluginType.Regular);
            // load addons
            App.Addons.LoadAddons();
            // register ShortcutPanelItem
            App.PanelItemTypes.RegisterFactory<ShortcutPanelItem>(new ShortcutFactory());
            App.PanelFillers.RegisterFiller<ShortcutPanelItem>(new ShortcutFiller());
            // create main form
            App.MainView = App.Resolve<IMainView>();
            App.Presenter.PrepareForm();
            // run application
            App.Resolve<IAppPresenter>().ApplicationRun(App.MainView);
        }
    }
}