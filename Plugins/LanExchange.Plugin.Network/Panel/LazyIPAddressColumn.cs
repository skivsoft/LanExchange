using System;
using System.Net;
using System.Threading;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class LazyIPAddressColumn : LazyPanelColumn
    {
        public LazyIPAddressColumn(string text, int width = 0) : base(text, width)
        {
        }

        public override IComparable SyncGetData(PanelItemBase item)
        {
            //#if DEBUG
            //Thread.Sleep(1000);
            //#endif
            return new IPAddressComparable(Dns.GetHostEntry(item.Name).AddressList[0]);
        }
    }
}