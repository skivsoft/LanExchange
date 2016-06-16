using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Visitors
{
    internal sealed class MenuBuilderVisitor : IMenuElementVisitor
    {
        private Menu rootMenu;
        private MenuItem submenu;

        public MainMenu BuildMainMenu(IMenuElement menu)
        {
            Contract.Requires<ArgumentNullException>(menu != null);

            rootMenu = new MainMenu();
            submenu = null;
            menu.Accept(this);
            return (MainMenu)rootMenu;
        }

        public ContextMenu BuildContextMenu(IMenuElement menu)
        {
            Contract.Requires<ArgumentNullException>(menu != null);

            rootMenu = new ContextMenu();
            submenu = null;
            menu.Accept(this);
            return (ContextMenu)rootMenu;
        }

        public void VisitMenuGroup(string text)
        {
            submenu = new MenuItem(text);
            rootMenu.MenuItems.Add(submenu);
        }

        private void AddItem(MenuItem menuItem)
        {
            if (submenu == null)
                rootMenu.MenuItems.Add(menuItem);
            else
                submenu.MenuItems.Add(menuItem);
        }

        public void VisitMenuElement(string text, string shortcut)
        {
            var menuItem = new MenuItem(text);
            if (!string.IsNullOrEmpty(shortcut))
            {
                var converter = new KeysConverter();
                menuItem.Shortcut = (Shortcut)converter.ConvertFrom(shortcut);
            }

            AddItem(menuItem);
        }

        public void VisitSeparator()
        {
            AddItem(new MenuItem("-"));
        }
    }
}
