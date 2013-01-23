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
            // get domain list via OS api
            var list = NetApi32Utils.NetServerEnum(null, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            foreach(var item in list)
                m_Result.Add(new DomainPanelItem(null, new ServerInfo(item)));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // domains can be only at root level
            accepted = subject == ConcreteSubject.Root;
        }
    }
}
