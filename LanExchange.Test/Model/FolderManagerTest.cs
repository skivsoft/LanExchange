using System.IO;
using LanExchange.Application.Implementation;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    internal class FolderManagerTest
    {
        [Test]
        public void TestTabConfigFileName()
        {
            var manager = new FolderManager();
            string fileName = manager.TabsConfigFileName;
            Assert.AreEqual(FolderManager.TABS_FILE, Path.GetFileName(fileName));
        }
    }
}