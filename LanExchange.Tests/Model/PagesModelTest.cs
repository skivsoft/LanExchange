using LanExchange.Model;
using NUnit.Framework;

namespace LanExchange.Tests
{
    
    
    /// <summary>
    ///This is a test class for TabModelTest and is intended
    ///to contain all TabModelTest Unit Tests
    ///</summary>
    [TestFixture]
    public class PagesModelTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for TabModel Constructor
        ///</summary>
        [Test]
        public void TabModelConstructorTest()
        {
            PagesModel Model = new PagesModel();
            Assert.AreEqual(0, Model.Count);
        }

        /// <summary>
        ///A test for DelTab
        ///</summary>
        [Test]
        public void DelTabTest()
        {
            PagesModel Model = new PagesModel();
            Model.AddTab(new PanelItemList("MyTab"));
            Model.DelTab(0);
            Assert.AreEqual(0, Model.Count, "Count != 0");
        }

        /// <summary>
        ///A test for AddTab
        ///</summary>
        [Test]
        public void AddTabTest()
        {
            PagesModel Model = new PagesModel();
            Model.AddTab(new PanelItemList("MyTab"));
            Assert.AreEqual("MyTab", Model.GetTabName(0));
            Assert.AreEqual(null, Model.GetTabName(-1));
            Assert.AreEqual(null, Model.GetTabName(1));
        }

        /// <summary>
        ///A test for CommandRenameTab
        ///</summary>
        [Test]
        public void RenameTabTest()
        {
            PagesModel Model = new PagesModel();
            Model.AddTab(new PanelItemList("MyTab"));
            Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", Model.GetTabName(0));
        }

        //[TestMethod()]
        //public void CreateFromEmpyTabControl()
        //{
        //    // TODO USE NUnit.Mock HERE
        //    IPagesView view;
        //    PagesPresenter Controller = new PagesPresenter(view);
        //    PagesModel Model = Controller.GetModel();
        //    Assert.IsNotNull(Model);
        //    Assert.AreEqual(0, Model.Count, "Empty TabControl");
        //}

    }
}
