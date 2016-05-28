using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IEditPresenter : IPresenter<IEditView>
    {
        void SetDataType(string typeName);
    }
}