using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IEditPresenter : IPresenter<IEditView>, IPerformOkCancel
    {
        void SetDataType(string typeName);
    }
}