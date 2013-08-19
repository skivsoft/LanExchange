namespace LanExchange.SDK
{
    public interface ITranslator
    {
        string Translate(string id);
        string TranslatePlural(string id, long number);
    }

    public static class TT
    {
        public static ITranslator Translator { get; set; }

        public static string L(string id)
        {
            return Translator.Translate(id);
        }
    }
}
