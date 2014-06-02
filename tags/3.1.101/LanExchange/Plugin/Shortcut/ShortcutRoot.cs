using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Shortcut
{
    public class ShortcutRoot : PanelItemRootBase
    {
        protected override string GetName()
        {
            return Resources.mHelpKeys_Text;
        }

        public override string ImageName
        {
            get { return PanelImageNames.SHORTCUT; }
        }

        public override object Clone()
        {
            return new ShortcutRoot();
        }
    }
}
