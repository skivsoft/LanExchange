namespace LanExchange.Presentation.Interfaces
{
    public interface IStatusPanelPresenter : IPresenter<IStatusPanelView>
    {
        void PerformDoubleClick();
        void PerformComputerRightClick(bool control, bool shift);
        void PerformUserRightClick();
    }
}