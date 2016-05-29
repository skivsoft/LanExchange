namespace LanExchange.Presentation.Interfaces
{
    public interface IEditPresenter : IPresenter<IEditView>, IPerformOkCancel
    {
        void SetDataType(string typeName);
    }
}