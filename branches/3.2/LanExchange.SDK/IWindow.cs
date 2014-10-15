namespace LanExchange.SDK
{
    public interface IWindow : IView
    {
        string Text { get; set; }
        void SetBounds(int left, int top, int width, int height);
        void Show();
        void Activate();
    }
}