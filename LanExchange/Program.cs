// *****************************************************************************
// ROADMAP OF RELEASES
//
// RELEASE 1.1.0
//   DONE  Autoupdate computer lists
//   DONE  Update counts in status list
//   DONE  Filtering computer items
//   DONE  ContextMenuStrip for top panel
//   DONE  Enum shares after ItemActivate
//
// RELEASE 2.0
//   TODO  Unit-tests coverage 30%
//   TODO  Sending items to another tab
//   TODO  Manual creation of computer items
//   DONE  Classes with IDisposable interface must be a components
//   TODO  MSI installer
//   TODO  Multi-langual support (Russian, Engligh)
//   DONE  Columns sort (name, comment, version)
//   TODO  Platform detection based on Platforms.xml refer
//   TODO  Ctrl+Left/Ctrl+Right - change form size with phi based step
//   TODO  Help on shortcut keys
//   TODO  Internal language editor
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//   TODO  Move all WMI features to wmi panel plugin
//   TODO  WMI-commands execution with parameters
//   TODO  Refactoring for strict Model-View-Presenter pattern
//   TODO  Save/restore sort order for each tab
//   TODO  Acceptance level 1: all function Far.Network plugin must be added
//   TODO    Map disk to share
//   TODO    Ask username/password if needed when connect to share
//   TODO  Acceptance level 2: Code Analisys and R# issues must be fixed
// 
// *****************************************************************************

using System;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            SingleInstanceCheck.Check();
            try
            {
                CmdLineProcessor.Processing();
                AppPresenter.Setup();
                AppPresenter.Plugins.LoadPlugins();
                AppView.ApplicationRun();
            }
            catch(Exception e)
            {
                MessageBox.Show(null, e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
