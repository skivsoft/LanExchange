using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LanExchange.Application.Interfaces;

namespace LanExchange.Plugin.Translit
{
    internal abstract class TranslitStrategyBase : ITranslitStrategy
    {
        private readonly string abc;
        private readonly string[] result;

        protected TranslitStrategyBase(IList<string> abc, char separator, bool ignoreCase)
        {
            this.abc = abc[0] + abc[0].ToLower();
            if (ignoreCase)
                result = (abc[1] + separator + abc[1]).Split(separator);
            else
                result = (abc[1] + separator + abc[1].ToLower()).Split(separator);
        }

        private string TR(char ch)
        {
            var index = abc.IndexOf(ch);
            return index == -1 || index > result.Length-1 ? ch.ToString(CultureInfo.InvariantCulture) : result[index];
        }

        public string Transliterate(string text)
        {
            return text.Aggregate(string.Empty, (current, t) => current + TR(t));
        }
    }
}