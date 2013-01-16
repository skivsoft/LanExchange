using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using LanExchange.Utils;

namespace LanExchange.Strategy
{
    public class NetServerEnumStrategy : SubscriptionAbstractStrategy
    {
        private List<ServerInfo> m_Result;

        public NetServerEnumStrategy(string subject) : base(subject)
        {

        }

        public override void Algorithm()
        {
            // get server list via OS api
            NetApi32.SERVER_INFO_101[] list;
            if (String.IsNullOrEmpty(Subject))
                list = NetApi32Utils.GetDomainsArray();
            else
                list = NetApi32Utils.GetComputersOfDomainArray(Subject);
            // convert array to IList<ServerInfo>
            m_Result = new List<ServerInfo>();
            Array.ForEach(list, item => m_Result.Add(new ServerInfo(item)));
        }

        public IList<ServerInfo> Result
        {
            get { return m_Result; }
        }
    }
}
