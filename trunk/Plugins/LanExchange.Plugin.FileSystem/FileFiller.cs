using System;
using System.Collections.Generic;
using LanExchange.SDK;
using System.IO;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFiller : PanelFillerBase
    {
        private const string SHARE_TYPE_NAME = "SharePanelItem";

        public override bool IsParentAccepted(PanelItemBase parent)
        {
            var folder = parent as FilePanelItem;
            return parent.GetType().Name.Equals(SHARE_TYPE_NAME) || (folder != null && folder.IsDirectory);
        }

        public override void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var path = parent.FullName;
            var files = Directory.GetFileSystemEntries(path, "*.*");
            foreach (var fname in files)
                result.Add(new FilePanelItem(parent, fname));
        }
    }
}
