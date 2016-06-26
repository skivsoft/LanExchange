using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    internal sealed class NullMenu : IMenuElement
    {
        private static readonly IMenuElement instance = new NullMenu();

        private NullMenu()
        {
        }

        public static IMenuElement Instance
        {
            get { return instance; }
        }

        public void Accept(IMenuElementVisitor visitor)
        {
        }
    }
}