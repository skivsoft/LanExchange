namespace LanExchange.Presentation.Interfaces
{
    public interface ICheckAvailabilityPresenter : IPresenter<ICheckAvailabilityWindow>, IPerformOkCancel
    {
        void OnCurrentItemChanged();

        void StartChecking();

        void WaitAndShow();
    }
}