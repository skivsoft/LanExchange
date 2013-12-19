namespace LanExchange.SDK
{
    public interface IEditPresenter : IPresenter<IEditView>
    {
        void SetDataType(string typeName);
    }
}