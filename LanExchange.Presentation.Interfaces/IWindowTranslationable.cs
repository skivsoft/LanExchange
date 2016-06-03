namespace LanExchange.Presentation.Interfaces
{
    public interface IWindowTranslationable : IWindow, ITranslationable
    {
        bool RightToLeftValue { get; set; }
    }
}
