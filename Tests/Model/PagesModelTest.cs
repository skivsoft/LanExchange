using System;
using System.IO;
using NUnit.Framework;
using LanExchange.Intf;

namespace LanExchange.Model
{
    [TestFixture]
    public class PagesModelTest
    {
        private PagesModel m_Model;
        private bool m_EventFired;

        [SetUp]
        public void SetUp()
        {
            m_Model = new PagesModel();
            m_EventFired = false;
        }

        [TearDown]
        public void TearDown()
        {
            m_Model = null;
        }

        [Test]
        public void TestPagesModel()
        {
            Assert.AreEqual(0, m_Model.Count);
        }

        public void Model_AfterAppend_AfterRename(object sender, PanelModelEventArgs e)
        {
            m_EventFired = true;
        }

        public void Model_AfterRemove_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            m_EventFired = true;
        }

        private PanelModel NewPanelModel(string name)
        {
            var tab = new PanelModel(null);
            tab.TabName = name;
            return tab;
        }

        [Test]
        public void TestDelTab()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            m_Model.DelTab(0);
            Assert.AreEqual(0, m_Model.Count);
            Assert.IsFalse(m_EventFired);
            m_Model.AddTab(NewPanelModel("MyTab"));
            m_Model.AfterRemove += Model_AfterRemove_IndexChanged;
            m_Model.DelTab(0);
            Assert.IsTrue(m_EventFired);
        }

        [Test]
        public void TestAddTab()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            Assert.IsFalse(m_EventFired);
            Assert.AreEqual("MyTab", m_Model.GetTabName(0));
            Assert.AreEqual(null, m_Model.GetTabName(-1));
            Assert.AreEqual(null, m_Model.GetTabName(1));
            m_Model.AfterAppendTab += Model_AfterAppend_AfterRename;
            m_Model.AddTab(NewPanelModel("YourTab"));
            Assert.IsTrue(m_EventFired);
            Assert.IsFalse(m_Model.AddTab(NewPanelModel("MyTab")));
        }

        [Test]
        public void TestRenameTab()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            Assert.IsFalse(m_EventFired);
            m_Model.RenameTab(0, "YourTab");
            Assert.AreEqual("YourTab", m_Model.GetTabName(0));
            m_Model.AfterRename += Model_AfterAppend_AfterRename;
            m_Model.RenameTab(0, "MyTab");
            Assert.IsTrue(m_EventFired);
        }

        [Test]
        public void TestIndexChanged()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            m_Model.AddTab(NewPanelModel("YourTab"));
            m_Model.SelectedIndex = 1;
            Assert.IsFalse(m_EventFired);
            Assert.AreEqual(1, m_Model.SelectedIndex);
            m_Model.IndexChanged += Model_AfterRemove_IndexChanged;
            m_Model.SelectedIndex = 0;
            Assert.IsTrue(m_EventFired);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionGetItem()
        {
            var item = m_Model.GetItem(0);
        }

        [Test]
        public void TestGetItem()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            var item = m_Model.GetItem(0);
            Assert.NotNull(item);
            Assert.AreEqual("MyTab", item.TabName);
        }

        [Test]
        public void TestGetItemIndex()
        {
            var info = NewPanelModel("MyTab");
            m_Model.AddTab(info);
            Assert.AreEqual(-1, m_Model.GetItemIndex(null));
            Assert.AreEqual(0, m_Model.GetItemIndex(info));
        }

        [Test]
        public void TestTabNameExists()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            Assert.IsFalse(m_Model.TabNameExists("MaiTab"));
            Assert.IsTrue(m_Model.TabNameExists("mytAB"));
        }

        [Test]
        public void TestGenerateTabName()
        {
            Assert.IsEmpty(m_Model.GenerateTabName());
            m_Model.AddTab(NewPanelModel("MyTab"));
            Assert.AreEqual("Copy of MyTab", m_Model.GenerateTabName());
            m_Model.AddTab(NewPanelModel("Copy of MyTab"));
            m_Model.SelectedIndex = 0;
            Assert.AreEqual("Copy (1) of MyTab", m_Model.GenerateTabName());
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ExceptionGenerateTabName()
        {
            m_Model.AddTab(NewPanelModel("MyTab"));
            m_Model.SelectedIndex = 100;
            m_Model.GenerateTabName();
        }

        [Test]
        public void TestSaveSettings()
        {
            App.SetContainer(ContainerBuilder.Build());
            var fileFName = App.FolderManager.TabsConfigFileName;
            var fileFNameBak = fileFName + ".bak";
            if (File.Exists(fileFName))
            {
                if (File.Exists(fileFNameBak))
                    File.Delete(fileFNameBak);
                File.Move(fileFName, fileFNameBak);
            }
            m_Model.SaveSettings();
            Assert.IsTrue(File.Exists(fileFName));
            if (File.Exists(fileFName))
                File.Delete(fileFName);
            File.Move(fileFNameBak, fileFName);
        }

        [Test]
        public void TestLoadSettings()
        {
            App.SetContainer(ContainerBuilder.Build());
            m_Model.SaveSettings();
            m_Model.LoadSettings();
        }
    }
}