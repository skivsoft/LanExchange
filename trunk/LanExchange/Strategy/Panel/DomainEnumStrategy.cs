using System;
using System.Collections.Generic;
using LanExchange.Utils;
using LanExchange.Model.Panel;
using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    public class DomainEnumStrategy : AbstractPanelStrategy
    {
        public override void Algorithm()
        {
            // get server list via OS api
            NetApi32.SERVER_INFO_101[] list = NetApi32Utils.GetDomainsArray();
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            Array.ForEach(list, item => m_Result.Add(new DomainPanelItem(null, new ServerInfo(item))));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // domains can be only at root level
            accepted = subject == ConcreteSubject.Root;
        }
    }
}
