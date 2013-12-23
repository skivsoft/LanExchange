using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK
{
    public interface ITranslitStrategy
    {
        string Transliterate(string text);
    }
}
