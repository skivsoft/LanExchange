using LanExchange.SDK;

namespace LanExchange.Presenter.Action
{
    public sealed class ShortcutFactory : PanelItemFactoryBase
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