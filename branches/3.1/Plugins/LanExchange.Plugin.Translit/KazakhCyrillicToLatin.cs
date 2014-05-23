using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.Translit
{
    internal class KazakhCyrillicToLatin : TranslitStrategyBase
    {
        private static readonly string[] s_ABC = new[]
            {
                "АӘБВГҒДЕЁЖЗИЙКҚЛМНҢОӨПРСТУҮҰФХҺЦЧШЩЪЫІЬЭЮЯ",
                "A|Ӓ|B|V|G|Ğ|D|E|Yo|J|Z|Ï|Y|K|Q|L|M|N|Ñ|O|Ö|P|R|S|T|W|Ü|U|F|X|H|C|Ç|Ş|Ş||I|I||E|Yu|Ya"
            };

        public KazakhCyrillicToLatin() : base(s_ABC, '|', false)
        {
        }
    }
}
