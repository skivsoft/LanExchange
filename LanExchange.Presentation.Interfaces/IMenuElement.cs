namespace LanExchange.Presentation.Interfaces
{
    public interface IMenuElement
    {
        void Accept(IMenuElementVisitor visitor);
    }
}