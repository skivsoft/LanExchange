namespace LanExchange.Presentation.Interfaces.Menu
{
    public interface IMenuElement
    {
        void Accept(IMenuElementVisitor visitor);
    }
}