using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LanExchange.SDK;

namespace LanExchange.Plugin.Translit
{
    internal abstract class TranslitStrategyBase : ITranslitStrategy
    {
        private readonly string m_ABC;
        private readonly string[] m_Result;

        protected TranslitStrategyBase(IList<string> abc, char separator, bool ignoreCase)
        {
            m_ABC = abc[0] + abc[0].ToLower();
            if (ignoreCase)
                m_Result = (abc[1] + separator + abc[1]).Split(separator);
            else
                m_Result = (abc[1] + separator + abc[1].ToLower()).Split(separator);
        }

        private string TR(char ch)
        {
            var index = m_ABC.IndexOf(ch);
            return index == -1 || index > m_Result.Length-1 ? ch.ToString(CultureInfo.InvariantCulture) : m_Result[index];
        }

        public string Transliterate(string text)
        {
            return text.Aggregate(string.Empty, (current, t) => current + TR(t));
        }
    }
}