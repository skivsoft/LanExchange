using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanExchange.UI;
using LanExchange.Model;

namespace Tests
{
    /// <summary>
    /// Summary description for PagesViewTest
    /// </summary>
    [TestClass]
    public class PagesViewTest
    {
        public PagesViewTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void View_SelectedIndex()
        {
            using (var View = new PagesView())
            {
                var Presenter = View.GetPresenter();
                var Model = Presenter.GetModel();
                var Items1 = new PanelItemList("test1");
                var Items2 = new PanelItemList("test2");
                View.NewTabFromItemList(Items1);
                View.NewTabFromItemList(Items2);
                View.SetSelectedIndex(1);
                Assert.AreEqual(1, View.GetSelectedIndex());
                Assert.AreEqual(1, Model.SelectedIndex);
            }
        }

        [TestMethod]
        public void Model_SelectedIndex()
        {
            using (var View = new PagesView())
            {
                var Presenter = View.GetPresenter();
                var Model = Presenter.GetModel();
                var Items1 = new PanelItemList("test1");
                var Items2 = new PanelItemList("test2");
                View.NewTabFromItemList(Items1);
                View.NewTabFromItemList(Items2);
                Model.SelectedIndex = 1;
                Assert.AreEqual(1, View.GetSelectedIndex());
                Assert.AreEqual(1, Model.SelectedIndex);
            }
        }
    }
}
