using LanExchange.Presentation.Interfaces.Menu;
using System.Windows.Forms;

namespace LanExchange.Presentation.WinForms.Visitors
{
    internal class MenuBuilderVisitor : IMenuElementVisitor
    {
        private MainMenu mainMenu;
        private MenuItem group;

        public void VisitMenuRoot()
        {
            mainMenu = new MainMenu();
        }

        public void VisitMenuGroup(string text)
        {
            group = new MenuItem(text);
            mainMenu.MenuItems.Add(group);
        }

        public void VisitMenuElement(string text)
        {
            group.MenuItems.Add(new MenuItem(text));
        }

        public void VisitSeparator()
        {
            group.MenuItems.Add(new MenuItem("-"));
        }

        internal MainMenu Build()
        {
            return mainMenu;
        }
    }
}
