namespace LanExchange.SDK
{
    public interface ITranslator
    {
        string Translate(string id);
        string TranslatePlural(string id, long number);
    }
}
