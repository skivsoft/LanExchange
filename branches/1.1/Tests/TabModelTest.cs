using LanExchange;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for TabModelTest and is intended
    ///to contain all TabModelTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TabModelTest
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
        [TestMethod()]
        public void TabModelConstructorTest()
        {
            TabModel Model = new TabModel("TheModel");
            Assert.AreEqual(0, Model.Count);
        }

        /// <summary>
        ///A test for DelTab
        ///</summary>
        [TestMethod()]
        public void DelTabTest()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Model.DelTab(0);
            Assert.AreEqual(0, Model.Count, "Count != 0");
        }

        /// <summary>
        ///A test for AddTab
        ///</summary>
        [TestMethod()]
        public void AddTabTest()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Assert.AreEqual("MyTab", Model.GetTabName(0));
            Assert.AreEqual(null, Model.GetTabName(-1));
            Assert.AreEqual(null, Model.GetTabName(1));
        }

        /// <summary>
        ///A test for RenameTab
        ///</summary>
        [TestMethod()]
        public void RenameTabTest()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", Model.GetTabName(0));
        }

        [TestMethod()]
        public void ViewSelectedTabText()
        {
            TabControl Pages = new TabControl();
            TabView View = new TabView(Pages);
            Assert.AreEqual("", View.SelectedTabText);
        }


        [TestMethod()]
        public void CreateFromEmpyTabControl()
        {
            TabControl Pages = new TabControl();
            TabController Controller = new TabController(Pages);
            //TabModel Model = Controller.GetModel();
            //Assert.NotNull(Model);
            //Assert.AreEqual(0, Model.Count, "Empty TabControl");
        }

        [TestMethod()]
        public void CreateFromNotEmptyTabControl1()
        {
            TabControl Pages = new TabControl();
            TabPage Tab = new TabPage("MyTab");
            Pages.TabPages.Add(Tab);
            TabController Controller = new TabController(Pages);
            TabModel Model = Controller.GetModel();
            Assert.AreEqual(0, Model.Count);
        }

    }
}
