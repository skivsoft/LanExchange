namespace LanExchange.Presentation.Interfaces
{
    public interface IStatusPanelView : IView, IWithImageList, IWithHandle
    {
        string ComputerName { get; set; }
        int ComputerImageIndex { get; set; }
        string UserName { get; set; }
        int UserImageIndex { get; set; }
    }
}