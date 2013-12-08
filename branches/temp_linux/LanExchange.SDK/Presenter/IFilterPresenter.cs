using LanExchange.SDK.Model;
using LanExchange.SDK.UI;

namespace LanExchange.SDK.Presenter
{
    public interface IFilterPresenter : IPresenter<IFilterView>
    {
        string FilterText { get; set; }
        bool IsFiltered { get; }
        void SetModel(IFilterModel value);
    }
}
