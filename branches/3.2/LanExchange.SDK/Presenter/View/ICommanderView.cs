namespace LanExchange.SDK.Presenter.View
{
    public interface ICommanderView : IView
    {
        CommanderPanelSide ActivePanelSide { get; set; }
        ICommanderPanelView ActivePanel { get; }
        ICommanderPanelView PassivePanel { get; }
        ICommanderPanelView LeftPanel { get; }
        ICommanderPanelView RightPanel { get; }
    }
}
