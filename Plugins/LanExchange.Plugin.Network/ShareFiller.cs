using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanExchange.Plugin.Network.NetApi;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Network
{
    public sealed class ShareFiller : IPanelFiller
    {
        public static bool ShowHiddenShares = true;
        public static bool ShowPrinters = true;

        public bool IsParentAccepted(PanelItemBase parent)
        {
            // parent for share can be only computer
            return parent is ComputerPanelItem;
        }

        public void SyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
        }

        [Localizable(false)]
        public void AsyncFill(PanelItemBase parent, ICollection<PanelItemBase> result)
        {
            if (parent == null) throw new ArgumentNullException(nameof(parent));

            //result.Add(new PanelItemDoubleDot(parent));
            foreach (var item in NetApiHelper.NetShareEnum(parent.Name))
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
            // enum logged users
            foreach(var item in NetworkHelper.NetWorkstationUserEnumNames(parent.Name))
            {
                var si = new ShareInfo();
                si.Name = item;
                si.ShareType = 100;
                result.Add(new SharePanelItem(parent, si));
            }
        }
    }
}