// *****************************************************************************
// RELEASE 1.1 MAJOR TODO LIST
//   DONE * Autoupdate computer lists
//   TODO * Sending items to another tab
//   DONE * Update counts in status list
//   DONE * Filtering computer items
//   DONE * ContextMenuStrip for top panel
//   TODO * Manual creation of computer items
//   TODO * WMI-commands execution with parameters
//   TODO * Enum shares after ItemActivate
//   TODO * Classes with IDisposable interface must be a components
//   TODO * MSI installer
//   TODO * Publishing and autoupdate from v1.0
//
// RELEASE 1.1 MINOR TODO LIST
//   TODO  Multi-langual support (Russian, Engligh)
//   TODO  Internal language editor
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//   TODO  Refactoring for strict Model-View-Presenter pattern
//   TODO  Columns sort
//   TODO  Save/restore sort order for each tab
// 
// RELEASE 1.2 TODO LIST
//   TODO  Map disk to share
//   TODO  Ask username/password if needed when connect to share
// 
// *****************************************************************************

using System;
using System.Reflection;
using System.Windows.Forms;
using LanExchange.Presenter;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange
{
    internal static class Program
    {
        static void LogHeader()
        {
            LogUtils.Info("----------------------------------------");
            LogUtils.Info("Version: [{0}], Executable: [{1}]", Assembly.GetExecutingAssembly().GetName().Version.ToString(), Application.ExecutablePath);
            LogUtils.Info(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            LogUtils.Info("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
        }

        [STAThread]
        static void Main()
        {
            SingleInstanceCheck.Check();
            LogHeader();
            AppPresenter.Plugins = new PluginLoader();
            AppPresenter.Plugins.LoadPlugins();
            AppView.ApplicationRun();
            LogUtils.Stop();
        }
    }
}
