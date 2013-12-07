namespace LanExchange.SDK.UI
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void LoadFromModel();

        void OpenHomeLink();

        void OpenTwitterLink();

        void OpenEmailLink();

        void OpenLocalizationLink();

        void OpenBugTrackerWebLink();
    }
}
