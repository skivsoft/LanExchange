using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelItemListEventArgsTest
    {
       [Test]
       public void TestInfo()
       {
           var info = new PanelItemList("MyTab");
           var args = new PanelModelEventArgs(info);
           Assert.AreSame(info, args.Info);
       }
    }
}
