using LanExchange.Presenter.Action;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Misc
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
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.mHelpKeys_Text, 100));
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Action, 280));
            columnManager.RegisterColumn<ShortcutPanelItem>(new PanelColumnHeader(Resources.Context, 80));
        }
    }
}