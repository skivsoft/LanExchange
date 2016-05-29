namespace LanExchange.Presentation.Interfaces
{
    public interface IAppPresenter
    {
        void Init();
        void Run(IMainView view);
        void TranslateOpenForms();
    }
}