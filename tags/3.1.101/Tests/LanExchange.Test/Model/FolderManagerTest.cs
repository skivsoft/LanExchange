using System.IO;
using LanExchange.Misc.Impl;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    internal class FolderManagerTest
    {
        [Test]
        public void TestTabConfigFileName()
        {
            var manager = new FolderManagerImpl();
            string fileName = manager.TabsConfigFileName;
            Assert.AreEqual(FolderManagerImpl.TABS_FILE, Path.GetFileName(fileName));
        }
    }
}