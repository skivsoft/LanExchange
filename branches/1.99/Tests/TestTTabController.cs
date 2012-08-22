﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkivSoft.LanExchange.Core;
using NUnit.Framework;
using System.Windows.Forms;
using SkivSoft.LanExchange.SDK;

namespace SkivSoft.LanExchange.Tests
{
    [TestFixture]
    class TestTTabController
    {
        [Test]
        [Category("Model")]
        public void ModelCreate()
        {
            TLanEXTabModel Model = new TLanEXTabModel();
            Assert.AreEqual(0, Model.InfoList.Count);
        }

        [Test]
        [Category("Model")]
        public void ModelNewTab()
        {
            TLanEXTabModel Model = new TLanEXTabModel();
            Model.AddTab(new TabInfo("MyTab"));
            Assert.AreEqual("MyTab", Model.GetTabName(0));
            Assert.AreEqual(null, Model.GetTabName(-1));
            Assert.AreEqual(null, Model.GetTabName(1));
        }

        [Test]
        [Category("Model")]
        public void ModelRenameTab()
        {
            TLanEXTabModel Model = new TLanEXTabModel();
            Model.AddTab(new TabInfo("MyTab"));
            Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", Model.GetTabName(0));
        }

        [Test]
        [Category("Model")]
        public void ModelDelTab()
        {
            TLanEXTabModel Model = new TLanEXTabModel();
            Model.AddTab(new TabInfo("MyTab"));
            Model.DelTab(0);
            Assert.AreEqual(0, Model.InfoList.Count, "Count != 0");
        }

        [Test]
        [Category("View")]
        public void ViewSelectedTabText()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TLanEXTabView View = new TLanEXTabView(Pages);
            Assert.AreEqual("", View.SelectedTabText);
        }


        [Test]
        [Category("Controller")]
        public void CreateFromEmpyTabControl()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TLanEXTabController Controller = new TLanEXTabController(Pages);
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
            TLanEXTabController Controller = new TLanEXTabController(Pages);
            ILanEXTabModel Model = Controller.Model;
            Assert.NotNull(Model);
            Assert.AreEqual(0, Model.InfoList.Count);
        }

        [Test]
        [Category("Controller")]
        public void CreateFromNotEmptyTabControl2()
        {
            TLanEXTabControl Pages = new TLanEXTabControl(null);
            TLanEXTabPage Tab = new TLanEXTabPage(null);
            Tab.Text = "MyTab";
            TLanEXListView LV = new TLanEXListView(null);
            LV.ItemList = new TLanEXItemList();
            Tab.ListView = LV;
            Pages.Add(Tab);

            TLanEXTabController Controller = new TLanEXTabController(Pages);
            ILanEXTabModel Model = Controller.Model;
            Assert.NotNull(Model);
            Assert.AreEqual(1, Model.InfoList.Count);
        }

    }
}