using LanExchange.Properties;
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

        public override void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.mHelpKeys_Text) { Width = 100 });
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Action) { Width = 280 });
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Context) { Width = 80 });
        }
    }
}