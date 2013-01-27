using System.Collections.Generic;
using LanExchange.Sdk;
using LanExchange.Utils;
using LanExchange.Model.Panel;

namespace LanExchange.Model.Strategy
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
                m_Result.Add(new DomainPanelItem(null, ServerInfo.FromNetApi32(item)));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // domains can be only at root level
            accepted = subject == ConcreteSubject.Root;
        }
    }
}
