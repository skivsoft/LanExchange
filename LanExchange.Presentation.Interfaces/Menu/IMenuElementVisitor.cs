namespace LanExchange.Presentation.Interfaces.Menu
{
    public interface IMenuElementVisitor
    {
        void VisitMenuRoot();
        void VisitMenuGroup(string text);
        void VisitMenuElement(string text);
        void VisitSeparator();
    }
}
