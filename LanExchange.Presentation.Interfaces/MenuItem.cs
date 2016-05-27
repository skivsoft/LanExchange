namespace LanExchange.Presentation.Interfaces
{
    public class MenuItem : IMenuItem
    {
        private readonly string caption;

        public MenuItem(string caption)
        {
            this.caption = caption;
        }

        public void Accept(IMenuItemVisitor visitor)
        {
            visitor.VisitMenuItem(caption);
        }
    }
}