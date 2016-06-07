namespace LanExchange.Presentation.Interfaces
{
    public interface IStatusPanelPresenter : IPresenter<IStatusPanelView>
    {
        void PerformComputerLeftClick();
        void PerformComputerRightClick(bool control, bool shift);
        void PerformUserRightClick();
    }
}