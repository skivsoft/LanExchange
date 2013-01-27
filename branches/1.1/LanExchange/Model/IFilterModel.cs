namespace LanExchange.Model
{
    public interface IFilterModel
    {
        string FilterText { get; set; }
        int FilterCount { get; }
        void ApplyFilter();
    }
}
