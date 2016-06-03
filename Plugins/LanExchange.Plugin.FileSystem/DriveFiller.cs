using System.Collections.Generic;
using System.IO;
using LanExchange.Presentation.Interfaces;

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
            foreach (var drive in DriveInfo.GetDrives())
            {
                var fname = drive.RootDirectory.Name;
                PluginFileSystem.RegisterImageForFileName(fname);
                var item = new DrivePanelItem(parent, fname);
                item.Info = drive;
                result.Add(item);
            }
        }
    }
}
