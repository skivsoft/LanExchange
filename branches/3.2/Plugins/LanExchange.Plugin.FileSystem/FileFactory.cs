using System;
using System.IO;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            return new FilePanelItem(parent, Path.Combine(parent.FullName, name));
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public Func<PanelItemBase, bool> GetAvailabilityChecker()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            if (columnManager == null)
                throw new ArgumentNullException("columnManager");

            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader(Resources.Name, 200));
            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader(Resources.Size){TextAlign = HorizontalAlignment.Right});
        }
    }
}
