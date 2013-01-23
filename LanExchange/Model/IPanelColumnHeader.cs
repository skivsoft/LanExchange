namespace LanExchange.Model
{
    public interface IPanelColumnHeader
    {
        string Text { get; set; }
        void SetVisible(bool value);
    }
}
