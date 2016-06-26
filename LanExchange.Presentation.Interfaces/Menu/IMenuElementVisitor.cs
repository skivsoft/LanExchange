namespace LanExchange.Presentation.Interfaces.Menu
{
    public interface IMenuElementVisitor
    {
        void VisitMenuGroup(string text);
        void VisitMenuElement(string text, string shortcut, ICommand command, bool isDefault);
        void VisitSeparator();
    }
}
