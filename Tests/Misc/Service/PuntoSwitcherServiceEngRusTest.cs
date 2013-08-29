using NUnit.Framework;

namespace LanExchange.Misc.Service
{
    [TestFixture]
    class PuntoSwitcherServiceEngRusTest
    {
        [Test]
        public void TestIsValidChar()
        {
            var punto = new PuntoSwitcherServiceEngRus();
            Assert.IsFalse(punto.IsValidChar('@'));
            Assert.IsTrue(punto.IsValidChar('Z'));
        }

        [Test]
        public void TestChange()
        {
            var punto = new PuntoSwitcherServiceEngRus();
            const string RUS = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            const string ENG = "F<DULT~:PBQRKVYJGHCNEA{WXIO}SM\">Zf,dult`;pbqrkvyjghcnea[wxio]sm'.z";
            Assert.AreEqual(RUS, punto.Change(ENG));
            Assert.AreEqual(ENG, punto.Change(RUS));
            Assert.AreEqual("!@#" + RUS, punto.Change("!@#" + ENG));
            Assert.AreEqual("!@#" + ENG, punto.Change("!@#" + RUS));
        }

        [Test]
        public void TestRussianContains()
        {
            var punto = new PuntoSwitcherServiceEngRus();
            Assert.IsFalse(punto.RussianContains("ЕЛКА", null));
            Assert.IsTrue(punto.RussianContains("ЕЛКА", "ЁЛ"));
            Assert.IsTrue(punto.RussianContains("ЁЛКА", "ЕЛ"));
            Assert.IsFalse(punto.RussianContains("ЕЛКА", "ЕЛЬ"));
        }
    }
}
