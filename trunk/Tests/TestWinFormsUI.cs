using NUnit.Framework;
using SkivSoft.LanExchange;
using LanExchange;

namespace Tests
{
    [TestFixture]
    class TestWinFormsUI
    {
        private TMainAppUI Instance = null;

        [SetUp]
        public void InitMainApp()
        {
            TMainAppUI Instance = new TMainAppUI();
        }

        [TearDown]
        public void DoneMainApp()
        {
            this.Instance = null;
        }

        [Test]
        public void CreateMainApp()
        {
        }


    }

}