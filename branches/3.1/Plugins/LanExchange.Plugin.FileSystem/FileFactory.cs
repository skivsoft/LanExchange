using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new FilePanelItem(parent, Path.Combine(parent.FullName, name));
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public override void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader("Name", 200));
            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader("Size"){TextAlign = HorizontalAlignment.Right});
        }
    }
}
