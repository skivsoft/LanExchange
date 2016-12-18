using LanExchange.Plugin.Network.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    public sealed class DomainRoot : PanelItemRootBase
    {
        public override string ImageName
        {
            get { return PanelImageNames.DOMAIN; }
        }

        public override object Clone()
        {
            return new DomainRoot();
        }

        protected override string GetName()
        {
            return Resources.Network;
        }
    }
}
