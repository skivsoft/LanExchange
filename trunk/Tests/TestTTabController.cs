using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanExchange;
using NUnit.Framework;
using System.Windows.Forms;

namespace Tests
{
    [TestFixture]
    class TestTTabController
    {
        [Test]
        [Category("Model")]
        public void ModelCreate()
        {
            TTabModel Model = new TTabModel();
            Assert.AreEqual(0, Model.Count);
        }

        [Test]
        [Category("Model")]
        public void ModelNewTab()
        {
            TTabModel Model = new TTabModel();
            Model.AddTab(new TTabInfo("MyTab"));
            Assert.AreEqual("MyTab", Model.GetTabName(0));
            Assert.AreEqual(null, Model.GetTabName(-1));
            Assert.AreEqual(null, Model.GetTabName(1));
        }

        [Test]
        [Category("Model")]
        public void ModelRenameTab()
        {
            TTabModel Model = new TTabModel();
            Model.AddTab(new TTabInfo("MyTab"));
            Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", Model.GetTabName(0));
        }

        [Test]
        [Category("Model")]
        public void ModelDelTab()
        {
            TTabModel Model = new TTabModel();
            Model.AddTab(new TTabInfo("MyTab"));
            Model.DelTab(0);
            Assert.AreEqual(0, Model.Count, "Count != 0");
        }

        [Test]
        [Category("View")]
        public void ViewSelectedTabText()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TTabView View = new TTabView(Pages);
            Assert.AreEqual("", View.SelectedTabText);
        }


        [Test]
        [Category("Controller")]
        public void CreateFromEmpyTabControl()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TTabController Controller = new TTabController(Pages);
            //TTabModel Model = Controller.GetModel();
            //Assert.NotNull(Model);
            //Assert.AreEqual(0, Model.Count, "Empty TabControl");
        }

        [Test]
        [Category("Controller")]
        public void CreateFromNotEmptyTabControl1()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TLanEXTabPage Tab = new TLanEXTabPage(null);
            Tab.Text = "MyTab";
            Pages.Add(Tab);
            TTabController Controller = new TTabController(Pages);
            TTabModel Model = Controller.GetModel();
            Assert.AreEqual(0, Model.Count);
        }

        [Test]
        [Category("Controller")]
        public void CreateFromNotEmptyTabControl2()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TLanEXTabPage Tab = new TLanEXTabPage(null);
            Tab.Text = "MyTab";
            TLanEXListView LV = new TLanEXListView(null);
            LV.ItemList = new TPanelItemList();
            Tab.ListView = LV;
            Pages.Add(Tab);

            TTabController Controller = new TTabController(Pages);
            TTabModel Model = Controller.GetModel();
            Assert.AreEqual(1, Model.Count);
        }

    }
}
