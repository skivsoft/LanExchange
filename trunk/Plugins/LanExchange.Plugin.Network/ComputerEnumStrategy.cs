using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class ComputerEnumStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            var domain = Subject as DomainPanelItem;
            if (domain == null) return;
            // get server list via OS api
            var list = NetApi32Utils.Instance.NetServerEnum(domain.Name, NetApi32.SV_101_TYPES.SV_TYPE_ALL);
            // convert array to IList<ServerInfo>
            foreach (var item in list)
            {
                var SI = ServerInfo.FromNetApi32(item);
                SI.ResetUtcUpdated();
                Result.Add(new ComputerPanelItem(domain, SI));
            }
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            // computers can be only into domains
            return (subject as DomainPanelItem) != null;
        }
    }
}
