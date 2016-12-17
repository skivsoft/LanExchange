using System;

namespace LanExchange.Presentation.Interfaces
{
    public interface IAppPresenter : IPresenter<IAppView>
    {
        void TranslateOpenForms();

        void OnNonUIException(Exception exception);

        void OnUIException(Exception exception);

        void OnExit();
    }
}