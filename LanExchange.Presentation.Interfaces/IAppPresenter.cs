namespace LanExchange.Presentation.Interfaces
{
    public interface IAppPresenter : IPresenter<IAppView>
    {
        void TranslateOpenForms();
    }
}