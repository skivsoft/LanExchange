namespace LanExchange.Presentation.Interfaces
{
    public interface IAboutPresenter : IPresenter<IAboutView>
    {
        void OpenHomeLink();
        string GetDetailsRtf();
        void PerformShowDetails();
    }
}
