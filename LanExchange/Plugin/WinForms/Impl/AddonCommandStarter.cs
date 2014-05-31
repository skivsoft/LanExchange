using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using LanExchange.Base;
using LanExchange.Helpers;
using LanExchange.Plugin.WinForms.Forms;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Impl
{
    public class AddonCommandStarter
    {
        private readonly PanelItemBase m_PanelItem;
        private readonly AddonMenuItem m_MenuItem;

        public static string[] BuildCmdLine(PanelItemBase panelItem, AddonMenuItem menuItem)
        {
            var programFileName = menuItem.ProgramValue.ExpandedFileName;
            var programArgs = AddonProgram.ExpandCmdLine(menuItem.ProgramArgs);
            programArgs = MacroHelper.ExpandPublicProperties(programArgs, panelItem);
            return ProtocolHelper.IsProtocol(menuItem.ProgramRef.Id)
                ? new[] { menuItem.ProgramRef.Id + programArgs }
                : new[] { programFileName, programArgs };
        }

        public AddonCommandStarter(PanelItemBase panelItem, AddonMenuItem menuItem)
        {
            if (panelItem == null)
                throw new ArgumentNullException("panelItem");

            if (menuItem == null)
                throw new ArgumentNullException("menuItem");

            m_PanelItem = panelItem;
            m_MenuItem = menuItem;
        }

        private void CheckAvailability()
        {
            var form = new CheckAvailabilityForm();
            form.CurrentItem = m_PanelItem;
            form.MenuItem = m_MenuItem;
            form.PrepareForm();
            form.Show();
        }

        [Localizable(false)]
        private void InternalStart()
        {
            var cmdLine = BuildCmdLine(m_PanelItem, m_MenuItem);
            try
            {
                switch (cmdLine.Length)
                {
                    case 1:
                        Process.Start(cmdLine[0]);
                        break;
                    case 2:
                        Process.Start(cmdLine[0], cmdLine[1]);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBoxHelper.ShowRunCmdError(string.Format(CultureInfo.InvariantCulture, "{0}\n{1}", string.Join(" ", cmdLine), ex.Message));
            }
        }

        public void Start()
        {
            if (!m_MenuItem.AllowUnreachable)
                CheckAvailability();
            InternalStart();
        }
    }
}
