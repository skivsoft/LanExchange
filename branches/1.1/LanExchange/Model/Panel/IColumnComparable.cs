namespace LanExchange.Model.Panel
{
    public interface IColumnComparable
    {
        int CompareTo(object other, int column);     
    }
}
