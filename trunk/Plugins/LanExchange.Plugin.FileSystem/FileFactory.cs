using System.IO;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    internal class FileFactory : IPanelItemFactory
    {
        public PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new FilePanelItem(parent, Path.Combine(parent.FullName, name));
        }

        public PanelItemBase CreateDefaultRoot()
        {
            return null;
        }

        public void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader(Resources.Name, 200));
            columnManager.RegisterColumn<FilePanelItem>(new PanelColumnHeader(Resources.Size){TextAlign = HorizontalAlignment.Right});
        }
    }
}
