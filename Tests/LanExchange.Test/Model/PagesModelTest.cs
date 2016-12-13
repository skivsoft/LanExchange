// using System.IO;

// using LanExchange.SDK;

// using NUnit.Framework;


// namespace LanExchange.Model

// {

// [TestFixture]

// public class PagesModelTest

// {

// [SetUp]

// public void SetUp()

// {

// m_Model = new PagesModel();

// m_EventFired = false;

// }


// [TearDown]

// public void TearDown()

// {

// m_Model = null;

// }


// private PagesModel m_Model;

// private bool m_EventFired;


// public void Model_AfterAppend(object sender, PanelModelEventArgs e)

// {

// m_EventFired = true;

// }


// public void Model_AfterRemove(object sender, PanelIndexEventArgs e)

// {

// m_EventFired = true;

// }


// private PanelModel NewPanelModel()

// {

// App.SetContainer(ContainerBuilder.Build());

// var tab = new PanelModel();

// return tab;

// }


// [Test]

// public void TestAddTab()

// {

// m_Model.AddTab(NewPanelModel());

// Assert.IsFalse(m_EventFired);

// Assert.AreEqual(string.Empty, m_Model.GetTabName(0));

// Assert.AreEqual(null, m_Model.GetTabName(-1));

// Assert.AreEqual(null, m_Model.GetTabName(1));

// m_Model.AfterAppendTab += Model_AfterAppend;

// m_Model.AddTab(NewPanelModel());

// Assert.IsTrue(m_EventFired);

// }


// /// <summary>

// ///     Issue 2: Current tab not restored after restart

// /// </summary>

// [Test]

// public void TestAddTab_Issue2()

// {

// App.SetContainer(ContainerBuilder.Build());

// m_Model.AddTab(NewPanelModel());

// Assert.AreEqual(0, m_Model.SelectedIndex);

// m_Model.AddTab(NewPanelModel());

// Assert.AreEqual(0, m_Model.SelectedIndex);

// }


// [Test]

// public void TestDelTab()

// {

// m_Model.AddTab(NewPanelModel());

// m_Model.DelTab(0);

// Assert.AreEqual(0, m_Model.Count);

// Assert.IsFalse(m_EventFired);

// m_Model.AddTab(NewPanelModel());

// m_Model.AfterRemove += Model_AfterRemove;

// m_Model.DelTab(0);

// Assert.IsTrue(m_EventFired);

// }


// [Test]

// public void TestGetItem()

// {

// m_Model.AddTab(NewPanelModel());

// IPanelModel item = m_Model.GetItem(0);

// Assert.NotNull(item);

// Assert.AreEqual(string.Empty, item.TabName);

// }


// [Test]

// public void TestGetItemIndex()

// {

// PanelModel info = NewPanelModel();

// m_Model.AddTab(info);

// Assert.AreEqual(-1, m_Model.GetItemIndex(null));

// Assert.AreEqual(0, m_Model.GetItemIndex(info));

// }


// [Test]

// public void TestGetItemNull()

// {

// IPanelModel item = m_Model.GetItem(0);

// Assert.IsNull(item);

// }


// [Test]

// public void TestIndexChanged()

// {

// m_Model.AddTab(NewPanelModel());

// m_Model.AddTab(NewPanelModel());

// m_Model.SelectedIndex = 1;

// Assert.IsFalse(m_EventFired);

// Assert.AreEqual(1, m_Model.SelectedIndex);

// m_Model.IndexChanged += Model_AfterRemove;

// m_Model.SelectedIndex = 0;

// Assert.IsTrue(m_EventFired);

// }


// [Test]

// public void TestLoadSettings()

// {

// App.SetContainer(ContainerBuilder.Build());

// m_Model.SaveSettings();

// IPagesModel pages;

// m_Model.LoadSettings(out pages);

// }


// [Test]

// public void TestPagesModel()

// {

// Assert.AreEqual(0, m_Model.Count);

// }


// [Test]

// public void TestSaveSettings()

// {

// App.SetContainer(ContainerBuilder.Build());

// string fileFName = App.FolderManager.TabsConfigFileName;

// string fileFNameBak = fileFName + ".bak";

// if (File.Exists(fileFName))

// {

// if (File.Exists(fileFNameBak))

// File.Delete(fileFNameBak);

// File.Move(fileFName, fileFNameBak);

// }

// m_Model.SaveSettings();

// Assert.IsTrue(File.Exists(fileFName));

// if (File.Exists(fileFName))

// File.Delete(fileFName);

// File.Move(fileFNameBak, fileFName);

// }


// [Test]

// public void TestSetLoadedModelCountIs0()

// {

// }

// }

// }
