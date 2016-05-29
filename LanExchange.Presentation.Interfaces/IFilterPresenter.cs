namespace LanExchange.Presentation.Interfaces
{
    public interface IFilterPresenter : IPresenter<IFilterView>
    {
        string FilterText { get; set; }
        bool IsFiltered { get; }
        //TODO: subscribe on model changed event
        //void SetModel(IFilterModel value);
    }
}
