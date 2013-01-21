using System;
using System.Collections.Generic;
using LanExchange.Utils;
using LanExchange.Model.Panel;

namespace LanExchange.Strategy.Panel
{
    public class ComputerEnumStrategy : AbstractPanelStrategy
    {
        private List<ServerInfo> m_Result;

        public ComputerEnumStrategy(string subject) : base(subject)
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

        public override bool AcceptParent(AbstractPanelItem parent)
        {
            // computers can be only at root level
            return parent == null;
        }
    }
}
