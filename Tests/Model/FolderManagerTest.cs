using System.IO;
using LanExchange.Misc;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    class FolderManagerTest
    {
        [Test]
        public void TestTabConfigFileName()
        {
            var fileName = FolderManager.Instance.TabsConfigFileName;
            Assert.AreEqual(FolderManager.TABS_FILE, Path.GetFileName(fileName));
        }
    }
}
