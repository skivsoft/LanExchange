using LanExchange.SDK.Presentation.Interfaces;

namespace LanExchange.SDK
{
    public interface IFilterPresenter : IPresenter<IFilterView>
    {
        string FilterText { get; set; }
        bool IsFiltered { get; }
        void SetModel(IFilterModel value);
    }
}
