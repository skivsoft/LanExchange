using NUnit.Framework;
using SkivSoft.LanExchange;
using LanExchange;
using SkivSoft.LanExchange.SDK;

namespace Tests
{
    [TestFixture]
    class TestWinFormsUI
    {
        private TMainAppUI Instance = null;
        private TLanEXControl OBJ = null;
        private ILanEXControl INT = null;

        [Test]
        public void CreateTLanEXControl()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXControl(null);
            INT = Instance.CreateControl(typeof(ILanEXControl));
            Assert.IsNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXControl), INT);
            Assert.IsNull((INT as TLanEXControl).control);
        }

        [Test]
        public void CreateTLanEXForm()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXForm(null);
            INT = Instance.CreateControl(typeof(ILanEXForm));
            Assert.NotNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXForm), INT);
            Assert.NotNull((INT as TLanEXForm).control);
        }
    }

}