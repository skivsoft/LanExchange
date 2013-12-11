using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using LanExchange.Presenter.Action;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public sealed class ShortcutFiller : PanelFillerBase
    {
        private const string PANEL_ITEM_SUFFIX = "PanelItem";

        [Localizable(false)]
        public static PanelItemRoot ROOT_OF_SHORTCUTS = new PanelItemRoot("ShortcutPanelItem");

        public override bool IsParentAccepted(PanelItemBase parent)
        {
            return (parent is PanelItemRoot) && (parent.Name.Equals(ROOT_OF_SHORTCUTS.Name));
        }

        public override void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            result.Add(new ShortcutPanelItem(parent, Resources.KeyF1, Resources.KeyF1__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyF10, Resources.KeyF10__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlW, Resources.KeyCtrlW__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlR, Resources.KeyCtrlR__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlWinX, Resources.KeyCtrlWinX__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlA, Resources.KeyCtrlA__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlC, Resources.KeyCtrlC__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlV, Resources.KeyCtrlV__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlIns, Resources.KeyCtrlIns__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlAltIns, Resources.KeyCtrlAltIns__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyDel, Resources.KeyDel__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyEsc, Resources.KeyEsc__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyEscLong, Resources.KeyEscLong__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyAnyChar, Resources.KeyAnyChar__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyBackspace, Resources.KeyBackspace__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlDown, Resources.KeyCtrlDown__));
            result.Add(new ShortcutPanelItem(parent, Resources.KeyCtrlUp, Resources.KeyCtrlUp__));
            foreach (var pair in App.Addons.PanelItems)
                foreach (var menuItem in pair.Value.ContextMenuStrip)
                    if (!string.IsNullOrEmpty(menuItem.ShortcutKeys))
                    {
                        var shortcut = new ShortcutPanelItem(parent, menuItem.ShortcutKeys, App.TR.Translate(menuItem.Text));
                        shortcut.Context = SuppressPostfix(pair.Key, PANEL_ITEM_SUFFIX);
                        if (menuItem.ProgramValue != null)
                            shortcut.CustomImageName = string.Format(CultureInfo.InvariantCulture, PanelImageNames.ADDON_FMT, menuItem.ProgramValue.Id);
                        result.Add(shortcut);
                    }
        }

        private string SuppressPostfix(string value, string postfix)
        {
            if (value.EndsWith(postfix, StringComparison.Ordinal))
                return value.Remove(value.Length - postfix.Length);
            return value;
        }
    }
}