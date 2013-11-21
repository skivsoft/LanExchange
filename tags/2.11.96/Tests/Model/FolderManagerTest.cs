using System.IO;
using LanExchange.Misc;
using NUnit.Framework;
using LanExchange.Misc.Impl;

namespace LanExchange.Model
{
    [TestFixture]
    class FolderManagerTest
    {
        [Test]
        public void TestTabConfigFileName()
        {
            var manager = new FolderManagerImpl();
            var fileName = manager.TabsConfigFileName;
            Assert.AreEqual(FolderManagerImpl.TABS_FILE, Path.GetFileName(fileName));
        }
    }
}
