namespace LanExchange.Utils.Sorting
{
    public interface IColumnComparable
    {
        int CompareTo(object other, int column);     
    }
}
