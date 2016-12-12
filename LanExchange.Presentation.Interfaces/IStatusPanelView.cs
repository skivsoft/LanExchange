namespace LanExchange.Presentation.Interfaces
{
    public interface IStatusPanelView : IView, ISupportImageList, ISupportHandle
    {
        string ComputerName { get; set; }

        int ComputerImageIndex { get; set; }

        string UserName { get; set; }

        int UserImageIndex { get; set; }
    }
}