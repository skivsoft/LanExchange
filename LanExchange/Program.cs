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
// RELEASE 2.0 (Iteration 3)
//   TODO  Unit-tests coverage at least 30%
//   DONE  Drag&Drop from panel to external app
//   DONE  Sending items to new tab
//   TODO    Drag&Drop from external app to panel
//   TODO    Manual creation of computer items
//   DONE  Classes with IDisposable interface must be a components
//   DONE  MSI installer
//   DONE  Columns sort (name, comment, version)
//   TODO  Ctrl+Left/Ctrl+Right - change form size with phi based step
//   TODO  Help on shortcut keys
//   TODO  Save/restore sort order for each tab
//   DONE  Load context menu for panel items from addons 
//   TODO  Sort by ping, ip address, mac address
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//   TODO  Async enum items and cache items
//   TODO  Changing columns order
//   TODO  Changing font size Ctrl+mouse wheel
//   TODO  User list (Users plugin)
//   TODO  Enum files and folders (FS plugin)
//   TODO  Addons editor
// 
// RELEASE 2.1 (Iteration 4)
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
using System.Windows.Forms;
using LanExchange.Intf;
using LanExchange.Misc;
using LanExchange.Misc.Addon;
using LanExchange.Properties;
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

                App.SetContainer(ContainerBuilder.Build());
                App.Plugins.LoadPlugins();
#if DEBUG
                //AddonGen.Generate();
#endif
                AddonManager.Instance.LoadAddons();
                AppView.ApplicationRun();
            }
            catch(Exception e)
            {
                MessageBox.Show(null, string.Format("{0}\n{1}", e.Message, e.StackTrace), Resources.Program_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
