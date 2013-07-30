using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ShareFillerStrategy : IPanelFillerStrategy
    {
        public static bool ShowHiddenShares;
        public static bool ShowPrinters;

        public bool IsParentAccepted(PanelItemBase parent)
        {
            // parent for share can be only computer
            return (parent != null) && (parent.GetType() == typeof (ComputerPanelItem));
        }

        public void Algorithm(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            result.Add(new SharePanelItem(parent, PanelItemBase.s_DoubleDot));
            foreach (var item in NetApi32Utils.Instance.NetShareEnum(parent.Name))
            {
                var SI = new ShareInfo(item);
                //if (!Settings.Settings.Instance.ShowHiddenShares && SI.IsHidden)
                //    continue;
                //if (!Settings.Settings.Instance.ShowPrinters && SI.IsPrinter)
                //    continue;
                if (!ShowHiddenShares && SI.IsHidden || !ShowPrinters && SI.IsPrinter)
                    continue;
                result.Add(new SharePanelItem(parent, SI));
            }
        }
    }
}
