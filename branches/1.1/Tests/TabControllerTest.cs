using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using NUnit.Framework;
using System.Windows.Forms;

namespace Tests
{
    [TestFixture]
    class TabControllerTest
    {
        [Test]
        [Category("Model")]
        public void ModelCreate()
        {
            TabModel Model = new TabModel("TheModel");
            Assert.AreEqual(0, Model.Count);
        }

        [Test]
        [Category("Model")]
        public void ModelNewTab()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Assert.AreEqual("MyTab", Model.GetTabName(0));
            Assert.AreEqual(null, Model.GetTabName(-1));
            Assert.AreEqual(null, Model.GetTabName(1));
        }

        [Test]
        [Category("Model")]
        public void ModelRenameTab()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", Model.GetTabName(0));
        }

        [Test]
        [Category("Model")]
        public void ModelDelTab()
        {
            TabModel Model = new TabModel("TheModel");
            Model.AddTab(new PanelItemList("MyTab"));
            Model.DelTab(0);
            Assert.AreEqual(0, Model.Count, "Count != 0");
        }

        [Test]
        [Category("View")]
        public void ViewSelectedTabText()
        {
            TabControl Pages = new TabControl();
            TabView View = new TabView(Pages);
            Assert.AreEqual("", View.SelectedTabText);
        }


        [Test]
        [Category("Controller")]
        public void CreateFromEmpyTabControl()
        {
            TabControl Pages = new TabControl();
            TabController Controller = new TabController(Pages);
            //TabModel Model = Controller.GetModel();
            //Assert.NotNull(Model);
            //Assert.AreEqual(0, Model.Count, "Empty TabControl");
        }

        [Test]
        [Category("Controller")]
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
