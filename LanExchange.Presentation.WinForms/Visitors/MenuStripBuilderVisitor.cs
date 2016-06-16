using LanExchange.Presentation.Interfaces.Menu;
using System.Windows.Forms;
using System;

namespace LanExchange.Presentation.WinForms.Visitors
{
    internal sealed class MenuStripBuilderVisitor : IMenuElementVisitor
    {
        private ToolStrip menuStrip;
        private ToolStripMenuItem submenu;

        public MenuStrip BuildMainMenu(IMenuElement menu)
        {
            menuStrip = new MenuStrip();
            submenu = null;
            menu.Accept(this);
            return (MenuStrip)menuStrip;
        }

        public ContextMenuStrip BuildContextMenu(IMenuElement menu)
        {
            menuStrip = new ContextMenuStrip();
            menu.Accept(this);
            return (ContextMenuStrip)menuStrip;
        }

        private void AddItem(ToolStripItem menuItem)
        {
            if (submenu == null)
                menuStrip.Items.Add(menuItem);
            else
                submenu.DropDownItems.Add(menuItem);
        }

        public void VisitMenuElement(string text, string shortcut)
        {
            var menuItem = new ToolStripMenuItem(text);
            if (!string.IsNullOrEmpty(shortcut))
            {
                var converter = new KeysConverter();
                menuItem.ShortcutKeys = (Keys)converter.ConvertFrom(shortcut);
            }
            AddItem(menuItem);
        }

        public void VisitMenuGroup(string text)
        {
            submenu = new ToolStripMenuItem(text);
            menuStrip.Items.Add(submenu);
        }

        public void VisitSeparator()
        {
            AddItem(new ToolStripSeparator());
        }
    }
}
