namespace LanExchange.Intf
{
    public interface IPuntoSwitcherService
    {
        bool IsValidChar(char ch);
        string Change(string str);
        bool SpecificContains(string s, string what);
    }
}
