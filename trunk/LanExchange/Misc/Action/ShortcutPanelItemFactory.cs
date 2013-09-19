using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    public sealed class ShortcutPanelItemFactory : PanelItemFactoryBase
    {
        public override PanelItemBase CreatePanelItem(PanelItemBase parent, string name)
        {
            return new ShortcutPanelItem(parent, name);
        }

        public override PanelItemBase CreateDefaultRoot()
        {
            return null;
        }
    }
}