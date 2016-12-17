using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Menu;
using System;
using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Visitors
{
    internal sealed class SystemMenuBuilder : IMenuElementVisitor
    {
        private Menu rootMenu;
        private MenuItem submenu;

        public MainMenu BuildMainMenu(IMenuElement menu)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            rootMenu = new MainMenu();
            submenu = null;
            menu.Accept(this);
            return (MainMenu)rootMenu;
        }

        public ContextMenu BuildContextMenu(IMenuElement menu)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

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

        public void VisitMenuElement(string text, string shortcut, ICommand command, bool isDefault)
        {
            var menuItem = new MenuItem(text);
            if (!string.IsNullOrEmpty(shortcut))
            {
                var converter = new KeysConverter();
                try
                {
                    menuItem.Shortcut = (Shortcut)converter.ConvertFrom(shortcut);
                }
                catch (ArgumentException)
                {
                }
            }
            menuItem.Enabled = command.Enabled;
            menuItem.Click += (sender, e) => command.Execute();
            menuItem.DefaultItem = isDefault;
            AddItem(menuItem);
        }

        public void VisitSeparator()
        {
            AddItem(new MenuItem("-"));
        }
    }
}
