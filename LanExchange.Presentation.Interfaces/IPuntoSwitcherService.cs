namespace LanExchange.Presentation.Interfaces
{
    public interface IPuntoSwitcherService
    {
        bool IsValidChar(char ch);

        string Change(string str);

        bool SpecificContains(string s, string what);
    }
}
