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
            NetApi32.SERVER_INFO_101[] List;
            if (String.IsNullOrEmpty(Subject))
                List = NetApi32Utils.GetDomainList();
            else
                List = NetApi32Utils.GetComputerList(Subject);
            // convert array to IList<ServerInfo>
            m_Result = new List<ServerInfo>();
            Array.ForEach(List, item => m_Result.Add(new ServerInfo(item)));
        }

        public IList<ServerInfo> Result
        {
            get { return m_Result; }
        }
    }
}
