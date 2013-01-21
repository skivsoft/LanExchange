using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    public class NetShareEnumStrategy : AbstractPanelStrategy
    {
        //private List<ServerInfo> m_Result;

        public NetShareEnumStrategy(string subject)
            : base(subject)
        {

        }

        public override void Algorithm()
        {
            //logger.Info("WinAPI: NetShareEnum for subject {0}", Subject);
        }

        //public IList<ServerInfo> Result
        //{
        //    get { return m_Result; }
        //}

        public override bool AcceptParent(AbstractPanelItem parent)
        {
            // parent for share can be only computer
            return (parent as ComputerPanelItem) != null;
        }
    }
}
