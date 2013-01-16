namespace LanExchange.Strategy
{
    public class NetShareEnumStrategy : AbstractSubscriptionStrategy
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
    }
}
