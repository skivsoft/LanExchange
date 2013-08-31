using System.Security.Permissions;

namespace LanExchange.SDK
{
    public abstract class PanelItemFactoryBase
    {
        public abstract PanelItemBase CreatePanelItem(PanelItemBase parent, string name);

        [EnvironmentPermission(SecurityAction.LinkDemand, Unrestricted = true)]
        public virtual PanelItemBase CreateDefaultRoot()
        {
            return null;
        }
    }
}