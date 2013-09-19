using System;
using System.Collections.Generic;
using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    public sealed class ShortcutFiller : IPanelFiller
    {
        public static PanelItemRoot ROOT_OF_SHORTCUTS = new PanelItemRoot("ShortcutPanelItem");

        public bool IsParentAccepted(PanelItemBase parent)
        {
            return parent == ROOT_OF_SHORTCUTS;
        }

        public void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            result.Add(new ShortcutPanelItem(parent, "F1", "This help on shortcuts."));
            result.Add(new ShortcutPanelItem(parent, "F9", "Show/hide main menu."));
            result.Add(new ShortcutPanelItem(parent, "F10", "Quit from LanExchange program."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+T", "Open new tab."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+P", "Current tab properties."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+F4", "Close current tab."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+R", "Re-read panel."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Win+X", "Show/hide main window."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+A", "Select all items in panel."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+C", "Copy selected item(s) to clipboard."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+V", "Paste items from clipboard."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Ins", "Copy column on which the ordered item(s) to clipboard."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Alt+Ins", "Copy full path of selected item(s) to clipboard."));
            result.Add(new ShortcutPanelItem(parent, "Del", "Delete selected item(s) which was created by user."));
            result.Add(new ShortcutPanelItem(parent, "Esc", "Clear filter OR go level up OR hide main window."));
            result.Add(new ShortcutPanelItem(parent, "Esc (long press)", "Hide main window."));
            result.Add(new ShortcutPanelItem(parent, "Any char", "Filter panel items by all columns."));
            result.Add(new ShortcutPanelItem(parent, "Backspace", "Go level up in panel."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Down", "Increase height of info panel or show it."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Up", "Decrease height of info panel or hide it."));
            result.Add(new ShortcutPanelItem(parent, "Ctrl+Shift+T", "Send selected item(s) to new tab."));
            foreach (var pair in App.Addons.PanelItems)
                foreach (var menuItem in pair.Value.ContextMenuStrip)
                    if (!string.IsNullOrEmpty(menuItem.ShortcutKeys))
                    {
                        var shortcut = new ShortcutPanelItem(parent, menuItem.ShortcutKeys, menuItem.Text);
                        shortcut.Context = SuppressPostfix(pair.Key, "PanelItem");
                        if (menuItem.ProgramValue != null)
                            shortcut.CustomImageName = string.Format(Resources.ProgramImageFormat, menuItem.ProgramValue.Id);
                        result.Add(shortcut);
                    }
        }


        public Type GetFillType()
        {
            return typeof(ShortcutPanelItem);
        }

        private string SuppressPostfix(string value, string postfix)
        {
            if (value.EndsWith(postfix))
                return value.Remove(value.Length - postfix.Length);
            return value;
        }
    }
}