using LanExchange.Core;
using LanExchange.Model;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public interface IFilterPresenter : IPresenter<IFilterView>
    {
        string FilterText { get; set; }
        bool IsFiltered { get; }
        void SetModel(IFilterModel value);
    }
}
