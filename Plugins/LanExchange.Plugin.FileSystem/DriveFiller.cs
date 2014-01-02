using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class DriveFiller : PanelFillerBase
    {
        public override bool IsParentAccepted(PanelItemBase parent)
        {
            return parent is FileRoot;
        }

        public override void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        public override void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
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
