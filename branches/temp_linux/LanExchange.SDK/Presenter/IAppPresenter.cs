using LanExchange.SDK.UI;

namespace LanExchange.SDK.Presenter
{
    public interface IAppPresenter
    {
        void Init();
        void Run(IMainView view);
        void TranslateOpenForms();
    }
}