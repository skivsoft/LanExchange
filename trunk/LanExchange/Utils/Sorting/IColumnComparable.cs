namespace LanExchange.Utils.Sorting
{
    public interface IColumnComparable<T>
    {
        int CompareTo(IColumnComparable<T> other, int column);     
    }
}
