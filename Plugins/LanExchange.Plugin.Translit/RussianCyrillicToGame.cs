using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.Translit
{
    internal class RussianCyrillicToGame : TranslitStrategyBase
    {
        private static readonly string[] s_ABC = new []
            {
                "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
                "a.6.B.r.g.e.e.}|{.3.u.u'.k.J|.m.H.o.n.p.c.T.y.<|>.x.u,.4.LL|.LL|,.'b.b|.b.3.|~O.9|"
            };

        public RussianCyrillicToGame() : base(s_ABC, '.', true)
        {
        }
    }
}
