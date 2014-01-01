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

        public override void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                var item = new DrivePanelItem(parent, drive.RootDirectory.Name);
                item.Info = drive;
                result.Add(item);
            }
        }
    }
}
