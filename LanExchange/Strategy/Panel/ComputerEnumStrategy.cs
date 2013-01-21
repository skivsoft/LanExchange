using System;
using System.Collections.Generic;
using LanExchange.Utils;
using LanExchange.Model.Panel;
using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    internal class ComputerEnumStrategy : AbstractPanelStrategy
    {
        public override void Algorithm()
        {
            var domain = Subject as DomainPanelItem;
            if (domain == null) return;
            // get server list via OS api
            NetApi32.SERVER_INFO_101[] list = NetApi32Utils.GetComputersOfDomainArray(domain.Name);
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            Array.ForEach(list, item => m_Result.Add(new ComputerPanelItem(domain, new ServerInfo(item))));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // computers can be only into domains
            accepted = (subject as DomainPanelItem) != null;
        }
    }
}
