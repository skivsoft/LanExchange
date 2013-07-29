using System.Collections.Generic;
using LanExchange.Plugin.Network.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network
{
    public class ShareEnumStrategy : PanelStrategyBase
    {
        public static bool ShowHiddenShares;
        public static bool ShowPrinters;

        public override void Algorithm()
        {
            var comp = Subject as ComputerPanelItem;
            if (comp == null) return;
            IEnumerable<NetApi32.SHARE_INFO_1> list = NetApi32Utils.Instance.NetShareEnum(comp.Name);
            // convert array to IList<ServerInfo>
            Result.Add(new SharePanelItem(comp, PanelItemBase.s_DoubleDot));
            foreach (var item in list)
            {
                var SI = new ShareInfo(item);
                //if (!Settings.Settings.Instance.ShowHiddenShares && SI.IsHidden)
                //    continue;
                //if (!Settings.Settings.Instance.ShowPrinters && SI.IsPrinter)
                //    continue;
                if (!ShowHiddenShares && SI.IsHidden || !ShowPrinters && SI.IsPrinter)
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
