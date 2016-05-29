namespace LanExchange.Presentation.Interfaces
{
    public class MenuElement : IMenuElement
    {
        private readonly string caption;

        public MenuElement(string caption)
        {
            this.caption = caption;
        }

        public void Accept(IMenuElementVisitor visitor)
        {
            visitor.VisitMenuElement(caption);
        }
    }
}