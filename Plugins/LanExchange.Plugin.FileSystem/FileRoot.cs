using LanExchange.Application.Interfaces;
using LanExchange.Plugin.FileSystem.Properties;

namespace LanExchange.Plugin.FileSystem
{
    public class FileRoot : PanelItemRootBase
    {
        protected override string GetName()
        {
            return Resources.Computer;
        }

        public override string ImageName
        {
            get { return PanelImageNames.COMPUTER; }
        }

        public override object Clone()
        {
            return new FileRoot();
        }
    }
}
