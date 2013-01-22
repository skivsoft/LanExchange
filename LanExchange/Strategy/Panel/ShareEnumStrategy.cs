using System;
using System.Collections.Generic;
using LanExchange.Model;
using LanExchange.Model.Panel;
using LanExchange.Utils;

namespace LanExchange.Strategy.Panel
{
    public class ShareEnumStrategy : AbstractPanelStrategy
    {
        public override void Algorithm()
        {
            var comp = Subject as ComputerPanelItem;
            if (comp == null) return;
            IEnumerable<NetApi32.SHARE_INFO_1> list = NetApi32Utils.NetShareEnum(comp.Name);
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            m_Result.Add(ComputerPanelItem.GoBack);
            foreach(var item in list)
                m_Result.Add(new SharePanelItem(comp, new ShareInfo(item)));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // parent for share can be only computer
            accepted = (subject as ComputerPanelItem) != null;
        }
    }
}
