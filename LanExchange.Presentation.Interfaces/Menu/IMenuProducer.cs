namespace LanExchange.Presentation.Interfaces.Menu
{
    public interface IMenuProducer
    {
        IMenuElement MainMenu { get; set; }
        IMenuElement TrayMenu { get; set; }
        IMenuElement ComputerMenu { get; set; }
        IMenuElement UserMenu { get; set; }
    }
}