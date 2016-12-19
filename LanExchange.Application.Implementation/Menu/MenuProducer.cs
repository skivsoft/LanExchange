using LanExchange.Presentation.Interfaces.Menu;

namespace LanExchange.Application.Implementation.Menu
{
    public sealed class MenuProducer : IMenuProducer
    {
        public MenuProducer()
        {
            MainMenu = NullMenu.Instance;
            TrayMenu = NullMenu.Instance;
            ComputerMenu = NullMenu.Instance;
            UserMenu = NullMenu.Instance;
        }

        public IMenuElement MainMenu { get; set; }

        public IMenuElement TrayMenu { get; set; }

        public IMenuElement ComputerMenu { get; set; }

        public IMenuElement UserMenu { get; set; }
    }
}