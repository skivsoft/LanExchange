using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    public sealed class NullMenu : IMenuElement
    {
        private static readonly IMenuElement NullInstance = new NullMenu();

        private NullMenu()
        {
        }

        public static IMenuElement Instance
        {
            get { return NullInstance; }
        }

        public void Accept(IMenuElementVisitor visitor)
        {
        }
    }
}