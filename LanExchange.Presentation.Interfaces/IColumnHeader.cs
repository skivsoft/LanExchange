namespace LanExchange.Presentation.Interfaces
{
    public interface IColumnHeader
    {
        string Text { get; }

        int Width { get; }

        HorizontalAlignment TextAlign { get; }
    }
}