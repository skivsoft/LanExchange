using System;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class DriveFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new DrivePanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return new FileRoot();
        }

        public override void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<DrivePanelItem>(new PanelColumnHeader("Name", 200));
        }
    }
}
