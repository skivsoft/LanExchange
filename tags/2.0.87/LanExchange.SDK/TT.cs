namespace LanExchange.SDK
{
    public static class TT
    {
        public static ITranslator Translator { get; set; }

        public static string L(string id)
        {
            return Translator.Translate(id);
        }
    }
}