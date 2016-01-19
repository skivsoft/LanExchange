using System;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class DriveFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new DrivePanelItem(parent, name);
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return new FileRoot();
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader(Resources.Name, 150));
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader(Resources.Type, 80));
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader(Resources.TotalSize, 100) { TextAlign = HorizontalAlignment.Right });
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader(Resources.FreeSpace, 100) { TextAlign = HorizontalAlignment.Right });
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader(Resources.FileSystem, 80) { Visible = false });
        }
    }
}
