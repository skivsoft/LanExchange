using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    internal sealed class NullMenu : IMenuElement
    {
        private static IMenuElement instance;

        private NullMenu()
        {
        }

        public static IMenuElement Instance
        {
            get
            {
                if (instance == null)
                    instance = new NullMenu();
                return instance;
            }
        }

        public void Accept(IMenuElementVisitor visitor)
        {
        }
    }
}