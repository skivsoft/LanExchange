using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
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
