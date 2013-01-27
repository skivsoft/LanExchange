using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Utils;
using LanExchange.Model;

namespace LanExchange.Model.Strategy
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
            m_Result.Add(new SharePanelItem(comp, AbstractPanelItem.BACK));
            foreach (var item in list)
            {
                var SI = new ShareInfo(item);
                if (!Settings.Settings.Instance.ShowHiddenShares && SI.IsHidden)
                    continue;
                if (!Settings.Settings.Instance.ShowPrinters && SI.IsPrinter)
                    continue;
                m_Result.Add(new SharePanelItem(comp, SI));
            }
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // parent for share can be only computer
            accepted = (subject as ComputerPanelItem) != null;
        }
    }
}
