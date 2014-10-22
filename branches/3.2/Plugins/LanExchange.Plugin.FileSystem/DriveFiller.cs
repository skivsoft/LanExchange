using System.Collections.Generic;
using System.IO;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class DriveFiller : IPanelFiller
    {
        public bool IsParentAccepted(PanelItemBase parent)
        {
            return parent is FileRoot;
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            foreach (var driveName in Directory.GetLogicalDrives())
                result.Add(new DrivePanelItem(parent, driveName));
        }
    }
}