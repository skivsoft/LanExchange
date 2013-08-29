using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    class TranslatorImpl : ITranslator
    {
        public string Translate(string id)
        {
            return id;
        }

        public string TranslatePlural(string id, long number)
        {
            return id;
        }
    }
}
