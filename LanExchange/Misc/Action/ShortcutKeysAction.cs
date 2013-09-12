using System.ComponentModel;
using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    class ShortcutKeysAction : IAction
    {
        public void Execute()
        {
            var presenter = App.MainPages;
            var info = App.Resolve<IPanelModel>();
            info.TabName = Resources.ShortcutKeys;
            SetupPanelModel(info);
            if (presenter.AddTab(info))
                App.MainPages.View.ActivePanelView.Presenter.UpdateItemsAndStatus();
            // !!! cycle is bad!
            for (int index = 0; index < presenter.Count; index++)
                if (presenter.GetItem(index).Equals(info))
                {
                    presenter.SelectedIndex = index;
                }
            //presenter.SelectedIndex = presenter.Count - 1;
        }

        [Localizable(false)]
        private void SetupPanelModel(IPanelModel info)
        {
            info.ImageName = PanelImageNames.ShortcutNormal;
            info.DataType = typeof (ShortcutPanelItem);
            info.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);
            var parent = PanelItemRoot.ROOT_OF_USERITEMS;
            info.Items.Add(new ShortcutPanelItem(parent, "F1", "This help on shortcuts."));
            info.Items.Add(new ShortcutPanelItem(parent, "F9", "Show/hide main menu."));
            info.Items.Add(new ShortcutPanelItem(parent, "F10", "Quit from LanExchange program."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+T", "Open new tab."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+P", "Current tab properties."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+F4", "Close current tab."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+R", "Re-read panel."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Win+X", "Show/hide main window."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+A", "Select all items in panel."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+C", "Copy selected item(s) to clipboard."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+V", "Paste items from clipboard."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Ins", "Copy first column of selected item(s) to clipboard."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Alt+Ins", "Copy full path of selected item(s) to clipboard."));
            info.Items.Add(new ShortcutPanelItem(parent, "Del", "Delete selected item(s) which was created by user."));
            info.Items.Add(new ShortcutPanelItem(parent, "Esc", "Hide main window."));
            info.Items.Add(new ShortcutPanelItem(parent, "Esc", "Go level up in panel."));
            info.Items.Add(new ShortcutPanelItem(parent, "Esc", "Clear filter if present."));
            info.Items.Add(new ShortcutPanelItem(parent, "Esc (long press)", "Hide main window."));
            info.Items.Add(new ShortcutPanelItem(parent, "Any char", "Filter panel items by all columns."));
            info.Items.Add(new ShortcutPanelItem(parent, "Backspace", "Go level up in panel."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Down", "Increase height of info panel or show it."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Up", "Decrease height of info panel or hide it."));
            info.Items.Add(new ShortcutPanelItem(parent, "Ctrl+Shift+T", "Send selected item(s) to new tab."));
            foreach (var pair in App.Addons.PanelItems)
                foreach(var menuItem in pair.Value.ContextMenuStrip)
                    if (!string.IsNullOrEmpty(menuItem.ShortcutKeys))
                    {
                        var shortcut = new ShortcutPanelItem(parent, menuItem.ShortcutKeys, menuItem.Text);
                        shortcut.Context = SuppressPostfix(pair.Key, "PanelItem");
                        if (menuItem.ProgramValue != null)
                            shortcut.CustomImageName = string.Format(Resources.ProgramImageFormat, menuItem.ProgramValue.Id);
                        info.Items.Add(shortcut);
                    }
        }

        private string SuppressPostfix(string value, string postfix)
        {
            if (value.EndsWith(postfix))
                return value.Remove(value.Length - postfix.Length);
            return value;
        }
    }
}