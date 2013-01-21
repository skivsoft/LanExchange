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
            NetApi32.SHARE_INFO_1[] list = NetApi32Utils.NetShareEnum(comp.Name);
            // convert array to IList<ServerInfo>
            m_Result = new List<AbstractPanelItem>();
            Array.ForEach(list, item => m_Result.Add(new SharePanelItem(comp, new ShareInfo(item))));
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // parent for share can be only computer
            accepted = (subject as ComputerPanelItem) != null;
        }
    }
}
