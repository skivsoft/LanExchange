namespace LanExchange.Presentation.Interfaces
{
    public interface IMenuItem
    {
        void Accept(IMenuItemVisitor visitor);
    }
}