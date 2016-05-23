namespace LanExchange.Plugin.Translit
{
    internal class RussianCyrillicToLatin : TranslitStrategyBase
    {
        private static readonly string[] abc =
        {
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
            "A|B|V|G|D|E|Yo|Zh|Z|I|J|K|L|M|N|O|P|R|S|T|U|F|Kh|Tc|Ch|Sh|Shch|'|Y|'|E|Yu|Ya"
        };

        public RussianCyrillicToLatin() : base(abc, '|', false)
        {
        }
    }
}
