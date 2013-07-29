using LanExchange.SDK;

namespace LanExchange.Model
{
    public class ComputerItemsStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            // do nothing here
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            // computers can be only into domains
            return subject == ConcreteSubject.s_UserItems;
        }
    }
}
