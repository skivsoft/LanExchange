namespace LanExchange.Misc.Service
{
    public interface IPuntoSwitcherService
    {
        bool IsValidChar(char ch);
        string Change(string str);
        bool RussianContains(string s, string what);
    }
}
