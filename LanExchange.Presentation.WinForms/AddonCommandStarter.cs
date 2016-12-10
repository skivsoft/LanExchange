using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Addons;
using LanExchange.Presentation.WinForms.Helpers;

namespace LanExchange.Presentation.WinForms
{
    public class AddonCommandStarter
    {
        private readonly IWindowFactory windowFactory;

        private readonly PanelItemBase panelItem;
        private readonly AddonMenuItem menuItem;
        private readonly Func<PanelItemBase, bool> checker;

        public static string[] BuildCmdLine(PanelItemBase panelItem, AddonMenuItem menuItem)
        {
            var programFileName = menuItem.ProgramValue.ExpandedFileName;
            var programArgs = EnvironmentHelper.ExpandCmdLine(menuItem.ProgramArgs);
            programArgs = MacroHelper.ExpandPublicProperties(programArgs, panelItem);
            return ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id)
                ? new[] { menuItem.ProgramRef.Id + programArgs }
                : new[] { programFileName, programArgs };
        }

        public AddonCommandStarter(
            IPanelItemFactoryManager factoryManager,
            IWindowFactory windowFactory,
            AddonMenuItem menuItem, 
            PanelItemBase panelItem)
        {
            if (factoryManager == null) throw new ArgumentNullException(nameof(factoryManager));
            this.windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));
            this.panelItem = panelItem ?? throw new ArgumentNullException(nameof(panelItem));
            this.menuItem = menuItem ?? throw new ArgumentNullException(nameof(menuItem));

            checker = factoryManager.GetAvailabilityChecker(this.panelItem.GetType());
        }

        [Localizable(false)]
        private void ShowCheckAvailabilityWindow()
        {
            // TODO hide model
            //var form = windowFactory.CreateCheckAvailabilityWindow();
            //form.Text = string.Format("{0} — {1}", panelItem.Name, menuItem.Text);
            //form.CurrentItem = panelItem;
            //form.RunText = menuItem.Text;
            //if (menuItem.ProgramValue != null)
            //    form.RunImage = menuItem.ProgramValue.ProgramImage;
            //form.CallerControl = this;
            //form.RunAction = InternalStart;
            //form.AvailabilityChecker = checker;

            //form.StartChecking();
            //form.WaitAndShow();
        }

        [Localizable(false)]
        private void InternalStart()
        {
            var cmdLine = BuildCmdLine(panelItem, menuItem);
            try
            {
                ProcessStartInfo startInfo;
                switch (cmdLine.Length)
                {
                    case 1:
                        startInfo = new ProcessStartInfo(cmdLine[0]);
                        break;
                    case 2:
                        startInfo = new ProcessStartInfo(cmdLine[0], cmdLine[1]);
                        break;
                    default:
                        return;
                }
                if (!string.IsNullOrEmpty(menuItem.WorkingDirectory))
                {
                    var path = MacroHelper.ExpandPublicProperties(menuItem.WorkingDirectory, panelItem);
                    // get parent directory if file was selected
                    if (File.Exists(path))
                        path = Path.GetDirectoryName(path);
                    if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                        startInfo.WorkingDirectory = path;
                }
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowRunCmdError(string.Format(CultureInfo.InvariantCulture, "{0}\n{1}", string.Join(" ", cmdLine), ex.Message));
            }
        }

        public void Start()
        {
            if (!menuItem.AllowUnreachable && checker != null)
                ShowCheckAvailabilityWindow();
            else
                InternalStart();
        }
    }
}
