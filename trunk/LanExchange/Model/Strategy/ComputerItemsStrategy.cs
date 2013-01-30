using LanExchange.Sdk;

namespace LanExchange.Model.Strategy
{
    internal class ComputerItemsStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            // do nothing here
        }

        public override bool IsSubjectAccepted(ISubject subject)
        {
            // computers can be only into domains
            return subject == ConcreteSubject.UserItems;
        }
    }
}
