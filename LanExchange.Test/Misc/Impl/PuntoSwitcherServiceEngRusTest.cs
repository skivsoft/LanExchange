using LanExchange.Application.Implementation;
using NUnit.Framework;

namespace LanExchange.Misc.Impl
{
    [TestFixture]
    internal class PuntoSwitcherServiceEngRusTest
    {
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
        public void TestIsValidChar()
        {
            var punto = new PuntoSwitcherServiceEngRus();
            Assert.IsFalse(punto.IsValidChar('@'));
            Assert.IsTrue(punto.IsValidChar('Z'));
        }

        [Test]
        public void TestRussianContains()
        {
            var punto = new PuntoSwitcherServiceEngRus();
            Assert.IsFalse(punto.SpecificContains("ЕЛКА", null));
            Assert.IsTrue(punto.SpecificContains("ЕЛКА", "ЁЛ"));
            Assert.IsTrue(punto.SpecificContains("ЁЛКА", "ЕЛ"));
            Assert.IsFalse(punto.SpecificContains("ЕЛКА", "ЕЛЬ"));
        }
    }
}