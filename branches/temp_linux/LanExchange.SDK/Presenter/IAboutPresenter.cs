using LanExchange.SDK.UI;

namespace LanExchange.SDK.Presenter
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void LoadFromModel();

        void OpenHomeLink();

        void OpenTwitterLink();

        void OpenEmailLink();

        void OpenLocalizationLink();

        void OpenBugTrackerWebLink();

        string GetDetailsRtf();
    }
}
