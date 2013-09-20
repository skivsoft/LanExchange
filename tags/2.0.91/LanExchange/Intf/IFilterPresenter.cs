using LanExchange.UI;

namespace LanExchange.Intf
{
    public interface IFilterPresenter : IPresenter<IFilterView>
    {
        string FilterText { get; set; }
        bool IsFiltered { get; }
        void SetModel(IFilterModel value);
    }
}
