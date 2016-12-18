namespace LanExchange.Plugin.Translit
{
    internal class RussianCyrillicToGame : TranslitStrategyBase
    {
        private static readonly string[] ABC =
        {
            "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ",
            "a.6.B.r.g.e.e.}|{.3.u.u'.k.J|.m.H.o.n.p.c.T.y.<|>.x.u,.4.LL|.LL|,.'b.b|.b.3.|~O.9|"
        };

        public RussianCyrillicToGame() : base(ABC, '.', true)
        {
        }
    }
}
