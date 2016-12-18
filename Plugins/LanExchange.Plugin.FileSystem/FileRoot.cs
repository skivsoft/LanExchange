using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.FileSystem
{
    public class FileRoot : PanelItemRootBase
    {
        public override string ImageName
        {
            get { return PanelImageNames.COMPUTER; }
        }

        public override object Clone()
        {
            return new FileRoot();
        }

        protected override string GetName()
        {
            return Resources.Computer;
        }
    }
}
