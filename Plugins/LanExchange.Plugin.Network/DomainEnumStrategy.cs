using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class DomainEnumStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            // get domain list via OS api
            var list = NetApi32Utils.Instance.NetServerEnum(null, NetApi32.SV_101_TYPES.SV_TYPE_DOMAIN_ENUM);
            // convert array to IList<ServerInfo>
            foreach (var item in list)
                Result.Add(new DomainPanelItem(null, ServerInfo.FromNetApi32(item)));
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            // domains can be only at root level
            return subject == ConcreteSubject.s_Root;
        }
    }
}
