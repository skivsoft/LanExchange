using System;
using System.Collections.Generic;
using LanExchange.SDK;
using System.IO;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFiller : PanelFillerBase
    {
        private const string SHARE_TYPE_NAME = "SharePanelItem";
        private string m_PathExt;

        public override bool IsParentAccepted(PanelItemBase parent)
        {
            var folder = parent as FilePanelItem;
            return (parent is DrivePanelItem) || 
                   parent.GetType().Name.Equals(SHARE_TYPE_NAME) || 
                   (folder != null && folder.IsDirectory);
        }

        public override void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        public override void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var path = parent.FullName;
            var files = Directory.GetFileSystemEntries(path, "*.*");
            m_PathExt = Environment.GetEnvironmentVariable("PATHEXT") + ";";
            m_PathExt = m_PathExt.ToUpper();
            foreach (var fname in files)
            {
                var file = new FilePanelItem(parent, fname);
                result.Add(file);
                //if (!file.IsDirectory && IsExecutable(fname))
                //    PluginFileSystem.RegisterImageForFileName(fname);
            }
        }

        private bool IsExecutable(string fname)
        {
            var ext = Path.GetExtension(fname);
            if (ext == null) return false;
            return m_PathExt.Contains(ext.ToUpper() + ";");
        }
    }
}
