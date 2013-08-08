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
//   DONE  Unit-tests coverage 10%
//   TODO  Sending items to another tab
//   TODO  Manual creation of computer items
//   DONE  Classes with IDisposable interface must be a components
//   TODO  MSI installer
//   TODO  Multi-langual support (Russian, Engligh)
//   TODO  Columns sort (name, comment, version)
//   TODO  Platform detection based on Platforms.xml refer
//
// RELEASE 3.0
//   TODO  WMI-commands execution with parameters
//   TODO  Internal language editor
//   TODO  Recently used items must appears when Tray.onMouseOver event fired
//   TODO  Refactoring for strict Model-View-Presenter pattern
//   TODO  Save/restore sort order for each tab
//   TODO  Map disk to share
//   TODO  Ask username/password if needed when connect to share
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
