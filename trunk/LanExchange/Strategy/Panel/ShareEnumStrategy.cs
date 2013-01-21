using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Model;
using LanExchange.Utils;

namespace LanExchange.Strategy.Panel
{
    public class ShareEnumStrategy : AbstractPanelStrategy
    {
        //private List<ServerInfo> m_Result;

        public override void Algorithm()
        {
            var comp = Subject as ComputerPanelItem;
            if (comp == null) return;
            //logger.Info("WinAPI: NetShareEnum for subject {0}", Subject);
            m_Result = new List<AbstractPanelItem>();
            NetApi32.SHARE_INFO_1 info = new NetApi32.SHARE_INFO_1();
            info.shi1_netname = "QQQ";
            var Info = new ShareInfo(info);
            m_Result.Add(new SharePanelItem(comp, Info));
        }

        //public IList<ServerInfo> Result
        //{
        //    get { return m_Result; }
        //}

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // parent for share can be only computer
            accepted = (subject as ComputerPanelItem) != null;
        }
    }
}
