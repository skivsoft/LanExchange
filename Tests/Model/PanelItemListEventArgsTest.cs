using LanExchange.Model;
using NUnit.Framework;

namespace LanExchange.Tests.Model
{
    [TestFixture]
    class PanelItemListEventArgsTest
    {
       [Test]
       public void TestInfo()
       {
           var info = new PanelItemList("MyTab");
           var args = new PanelItemListEventArgs(info);
           Assert.AreSame(info, args.Info);
       }
    }
}
