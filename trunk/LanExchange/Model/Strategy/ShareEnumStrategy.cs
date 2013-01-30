using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Sdk;
using LanExchange.Utils;

namespace LanExchange.Model.Strategy
{
    public class ShareEnumStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            var comp = Subject as ComputerPanelItem;
            if (comp == null) return;
            IEnumerable<NetApi32.SHARE_INFO_1> list = NetApi32Utils.NetShareEnum(comp.Name);
            // convert array to IList<ServerInfo>
            Result.Add(new SharePanelItem(comp, PanelItemBase.s_DoubleDot));
            foreach (var item in list)
            {
                var SI = new ShareInfo(item);
                if (!Settings.Settings.Instance.ShowHiddenShares && SI.IsHidden)
                    continue;
                if (!Settings.Settings.Instance.ShowPrinters && SI.IsPrinter)
                    continue;
                Result.Add(new SharePanelItem(comp, SI));
            }
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            // parent for share can be only computer
            return (subject as ComputerPanelItem) != null;
        }
    }
}
