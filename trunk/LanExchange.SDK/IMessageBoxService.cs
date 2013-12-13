namespace LanExchange.SDK
{
    public interface IMessageBoxService
    {
        int AskQuestionFmt(string title, string format, params object[] args);
        bool IsYes(int result);
        bool IsNo(int result);
        bool IsCancel(int result);
    }
}
