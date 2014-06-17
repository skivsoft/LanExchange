using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using LanExchange.Base;
using LanExchange.Helpers;
using LanExchange.Ioc;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Impl
{
    public class AddonCommandStarter
    {
        private readonly PanelItemBase m_PanelItem;
        private readonly AddonMenuItem m_MenuItem;
        private readonly Func<PanelItemBase, bool> m_Checker;

        public static string[] BuildCmdLine(PanelItemBase panelItem, AddonMenuItem menuItem)
        {
            var programFileName = menuItem.ProgramValue.ExpandedFileName;
            var programArgs = AddonProgram.ExpandCmdLine(menuItem.ProgramArgs);
            programArgs = MacroHelper.ExpandPublicProperties(programArgs, panelItem);
            return ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id)
                ? new[] { menuItem.ProgramRef.Id + programArgs }
                : new[] { programFileName, programArgs };
        }

        public AddonCommandStarter(AddonMenuItem menuItem, PanelItemBase panelItem)
        {
            if (menuItem == null)
                throw new ArgumentNullException("menuItem");

            if (panelItem == null)
                throw new ArgumentNullException("panelItem");

            m_PanelItem = panelItem;
            m_MenuItem = menuItem;

            var factoryManager = App.Resolve<IPanelItemFactoryManager>();
            m_Checker = factoryManager.GetAvailabilityChecker(m_PanelItem.GetType());
        }

        [Localizable(false)]
        private void ShowCheckAvailabilityWindow()
        {
            var form = App.Resolve<ICheckAvailabilityWindow>();
            form.Text = string.Format("{0} — {1}", m_PanelItem.Name, m_MenuItem.Text);
            form.CurrentItem = m_PanelItem;
            form.RunText = m_MenuItem.Text;
            if (m_MenuItem.ProgramValue != null)
                form.RunImage = m_MenuItem.ProgramValue.ProgramImage;
            form.CallerControl = this;
            form.RunAction = InternalStart;
            form.AvailabilityChecker = m_Checker;

            form.StartChecking();
            form.WaitAndShow();
        }

        [Localizable(false)]
        private void InternalStart()
        {
            var cmdLine = BuildCmdLine(m_PanelItem, m_MenuItem);
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
                if (!string.IsNullOrEmpty(m_MenuItem.WorkingDirectory))
                {
                    var path = MacroHelper.ExpandPublicProperties(m_MenuItem.WorkingDirectory, m_PanelItem);
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
            if (!m_MenuItem.AllowUnreachable && m_Checker != null)
                ShowCheckAvailabilityWindow();
            else
                InternalStart();
        }
    }
}
