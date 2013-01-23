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
            var list = NetApi32Utils.NetServerEnum(domain.Name, NetApi32.SV_101_TYPES.SV_TYPE_ALL);
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            foreach (var item in list)
            {
                var SI = new ServerInfo(item);
                SI.SetUtcUpdated();
                m_Result.Add(new ComputerPanelItem(domain, SI));
            }
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // computers can be only into domains
            accepted = (subject as DomainPanelItem) != null;
        }
    }
}
