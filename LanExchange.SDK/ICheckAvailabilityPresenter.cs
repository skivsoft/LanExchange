using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface ICheckAvailabilityPresenter : IPresenter<ICheckAvailabilityWindow>, IPerformOkCancel
    {
        void OnCurrentItemChanged();
        void StartChecking();
        void WaitAndShow();
    }
}