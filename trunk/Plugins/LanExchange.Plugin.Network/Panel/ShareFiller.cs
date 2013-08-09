using System;
using System.Collections.Generic;
using LanExchange.SDK;

namespace LanExchange.Plugin.Network.Panel
{
    public class ShareFiller : IPanelFiller
    {
        public static bool ShowHiddenShares;
        public static bool ShowPrinters;

        public bool IsParentAccepted(PanelItemBase parent)
        {
            // parent for share can be only computer
            return (parent != null) && (parent != Network.ROOT_OF_DOMAINS) && (parent is ComputerPanelItem);
        }

        public void Fill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");
            //result.Add(new PanelItemDoubleDot(parent));
            foreach (var item in NetApi32Utils.Instance.NetShareEnum(parent.Name))
            {
                var si = new ShareInfo(item);
                //if (!Settings.Settings.Instance.ShowHiddenShares && SI.IsHidden)
                //    continue;
                //if (!Settings.Settings.Instance.ShowPrinters && SI.IsPrinter)
                //    continue;
                if (!ShowHiddenShares && si.IsHidden || !ShowPrinters && si.IsPrinter)
                    continue;
                result.Add(new SharePanelItem(parent, si));
            }
        }


        public Type GetFillType()
        {
            return typeof (SharePanelItem);
        }
    }
}
