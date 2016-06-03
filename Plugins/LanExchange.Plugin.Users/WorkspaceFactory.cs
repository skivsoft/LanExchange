using System;
using LanExchange.Application.Interfaces;
using LanExchange.Plugin.Users.Properties;

namespace LanExchange.Plugin.Users
{
    public class WorkspaceFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new WorkspacePanelItem(parent, name);
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return new WorkspaceRoot();
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<WorkspacePanelItem>(new PanelColumnHeader(Resources.Name, 300));
        }
    }
}
