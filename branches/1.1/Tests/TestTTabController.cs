using System;
using System.Collections.Generic;
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
            TabControl Pages = new TabControl();
            TTabView View = new TTabView(Pages);
            Assert.AreEqual("", View.SelectedTabText);
        }


        [Test]
        [Category("Controller")]
        public void CreateFromEmpyTabControl()
        {
            TabControl Pages = new TabControl();
            TTabController Controller = new TTabController(Pages);
            //TTabModel Model = Controller.GetModel();
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
            TTabController Controller = new TTabController(Pages);
            TTabModel Model = Controller.GetModel();
            Assert.AreEqual(0, Model.Count);
        }

        [Test]
        [Category("Controller")]
        public void CreateFromNotEmptyTabControl2()
        {
            TabControl Pages = new TabControl();
            TabPage Tab = new TabPage("MyTab");
            ListView LV = new ListView();
            TPanelItemList ItemList = new TPanelItemList();
            TPanelItemList.ListView_SetObject(LV, ItemList);
            Tab.Controls.Add(LV);
            Pages.TabPages.Add(Tab);
            TTabController Controller = new TTabController(Pages);
            TTabModel Model = Controller.GetModel();
            Assert.AreEqual(1, Model.Count);
        }

    }
}
