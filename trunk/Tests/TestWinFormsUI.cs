using NUnit.Framework;
using SkivSoft.LanExchange;
using SkivSoft.LanExchange.Core;
using SkivSoft.LanExchange.SDK;

namespace SkivSoft.LanExchange.Tests
{
    [TestFixture]
    class TestWinFormsUI
    {
        private TMainAppUI Instance = null;
        private TLanEXControl OBJ = null;
        private ILanEXControl INT = null;

        [Test]
        public void TLanEXItemList()
        {
            Instance = new TMainAppUI();
            TLanEXItemList OBJ = new TLanEXItemList();
            ILanEXComponent INT = Instance.CreateComponent(typeof(ILanEXItemList));
            Assert.NotNull(OBJ);
            Assert.IsInstanceOf(typeof(TLanEXItemList), INT);
            Assert.NotNull(INT);
        }

        [Test]
        public void TLanEXListViewItem()
        {
            Instance = new TMainAppUI();
            TLanEXListViewItem OBJ = new TLanEXListViewItem(null);
            ILanEXComponent INT = Instance.CreateComponent(typeof(ILanEXListViewItem));
            Assert.NotNull(OBJ.Instance);
            Assert.IsInstanceOf(typeof(TLanEXListViewItem), INT);
            Assert.NotNull((INT as TLanEXListViewItem).Instance);
        }

        [Test]
        public void TLanEXMenuItem()
        {
            Instance = new TMainAppUI();
            TLanEXMenuItem OBJ = new TLanEXMenuItem(null);
            ILanEXComponent INT = Instance.CreateComponent(typeof(ILanEXMenuItem));
            Assert.NotNull(OBJ.Instance);
            Assert.IsInstanceOf(typeof(TLanEXMenuItem), INT);
            Assert.NotNull((INT as TLanEXMenuItem).Instance);
        }

        [Test]
        public void TLanEXControl()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXControl(null);
            INT = Instance.CreateControl(typeof(ILanEXControl));
            Assert.IsNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXControl), INT);
            Assert.IsNull((INT as TLanEXControl).control);
        }

        [Test]
        public void TLanEXListView()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXListView(null);
            INT = Instance.CreateControl(typeof(ILanEXListView));
            Assert.NotNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXListView), INT);
            Assert.NotNull((INT as TLanEXListView).control);
        }

        [Test]
        public void TLanEXTabPage()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXTabPage(null);
            INT = Instance.CreateControl(typeof(ILanEXTabPage));
            Assert.NotNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXTabPage), INT);
            Assert.NotNull((INT as TLanEXTabPage).control);
        }

        [Test]
        public void TLanEXTabControl()
        {
            Instance = new TMainAppUI();
            OBJ = new TLanEXTabControl(null);
            INT = Instance.CreateControl(typeof(ILanEXTabControl));
            Assert.NotNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXTabControl), INT);
            Assert.NotNull((INT as TLanEXTabControl).control);
        }

        [Test]
        public void TLanEXStatusStrip()
        {
            Instance = new TMainAppUI();
            TMainApp.App = Instance;
            new MainForm();
            OBJ = new TLanEXStatusStrip(null);
            INT = Instance.CreateStatusStrip();
            Assert.NotNull(OBJ.control);
            Assert.IsInstanceOf(typeof(TLanEXStatusStrip), INT);
            Assert.NotNull((INT as TLanEXStatusStrip).control);
        }
    }

}