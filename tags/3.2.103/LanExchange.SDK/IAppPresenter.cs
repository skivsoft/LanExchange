namespace LanExchange.SDK
{
    public interface IAppPresenter
    {
        void Init();
        void Run(IMainView view);
        void TranslateOpenForms();
    }
}