using LanExchange.Plugin.Shortcut.Properties;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Shortcut
{
    public class ShortcutRoot : PanelItemRootBase
    {
        public override string ImageName
        {
            get { return PanelImageNames.SHORTCUT; }
        }

        public override object Clone()
        {
            return new ShortcutRoot();
        }

        protected override string GetName()
        {
            return Resources.mHelpKeys_Text;
        }
    }
}
