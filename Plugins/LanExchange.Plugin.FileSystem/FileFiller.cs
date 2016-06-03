using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using LanExchange.Application.Interfaces;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFiller : IPanelFiller
    {
        private const string SHARE_TYPE_NAME = "SharePanelItem";
        private string pathExt;

        public bool IsParentAccepted(PanelItemBase parent)
        {
            var folder = parent as FilePanelItem;
            return (parent is DrivePanelItem) || 
                   parent.GetType().Name.Equals(SHARE_TYPE_NAME) || 
                   (folder != null && folder.IsDirectory);
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        [Localizable(false)]
        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            var path = parent.FullName;
            var files = Directory.GetFileSystemEntries(path, "*.*");
            pathExt = Environment.GetEnvironmentVariable("PATHEXT") + ";";
            pathExt = pathExt.ToUpper();
            foreach (var fname in files)
            {
                var file = new FilePanelItem(parent, fname);
                result.Add(file);
                //if (!file.IsDirectory && IsExecutable(fname))
                //    PluginFileSystem.RegisterImageForFileName(fname);
            }
        }

        //private bool IsExecutable(string fname)
        //{
        //    var ext = Path.GetExtension(fname);
        //    if (ext == null) return false;
        //    return m_PathExt.Contains(ext.ToUpper() + ";");
        //}
    }
}
