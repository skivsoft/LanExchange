using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanExchange.UI;

namespace Tests
{
    /// <summary>
    /// Summary description for FilterViewTest
    /// </summary>
    [TestClass]
    public class FilterViewTest
    {
        private TestContext m_TestContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return m_TestContextInstance;
            }
            set
            {
                m_TestContextInstance = value;
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
        public void View_FilterText()
        {
            var View = new FilterView();
            var Presenter = View.GetPresenter();
            Assert.AreEqual("", View.FilterText);
            Assert.AreEqual(false, View.Visible);
            View.FilterText = "test";
            Assert.AreEqual(true, View.Visible);
            Assert.AreEqual("test", View.FilterText);
            var Model = new FilterModelMock();
            Model.FilterText = "hello";
            Presenter.SetModel(Model);
            Assert.AreEqual("hello", View.FilterText);
        }
    }
}
