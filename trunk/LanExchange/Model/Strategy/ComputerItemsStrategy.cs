
using LanExchange.Sdk;

namespace LanExchange.Model.Strategy
{
    internal class ComputerItemsStrategy : PanelStrategyBase
    {
        public override void Algorithm()
        {
            // do nothing here
        }

        public override void AcceptSubject(ISubject subject, out bool accepted)
        {
            // computers can be only into domains
            accepted = subject == ConcreteSubject.UserItems;
        }
    }
}
