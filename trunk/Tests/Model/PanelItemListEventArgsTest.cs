using LanExchange.SDK;
using NUnit.Framework;

namespace LanExchange.Model
{
    [TestFixture]
    class PanelItemListEventArgsTest
    {
       [Test]
       public void TestInfo()
       {
           var info = new PanelModel();
           info.TabName = "MyTab";
           var args = new PanelModelEventArgs(info);
           Assert.AreSame(info, args.Info);
       }
    }
}
