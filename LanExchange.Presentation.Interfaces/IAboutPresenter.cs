namespace LanExchange.Presentation.Interfaces
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void OpenHomeLink();

        void OpenLocalizationLink();

        void OpenBugTrackerWebLink();

        string GetDetailsRtf();
        void PerformShowDetails();
    }
}
