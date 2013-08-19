using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Model.Impl
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
