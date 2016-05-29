namespace LanExchange.Presentation.Interfaces
{
    public interface IStatusPanelPresenter : IPresenter<IStatusPanelView>
    {
        void PerformDoubleClick();
        void PerformComputerRightClick();
        void PerformUserRightClick();
    }
}