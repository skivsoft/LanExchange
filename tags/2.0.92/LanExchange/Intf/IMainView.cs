namespace LanExchange.Intf
{
    public interface IMainView : IView
    {
        void ApplicationExit();
        void ShowStatusText(string format, params object[] args);
        void SetToolTip(object control, string tipText);
    }
}
