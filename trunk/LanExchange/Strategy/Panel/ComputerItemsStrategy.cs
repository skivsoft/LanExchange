using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    internal class ComputerItemsStrategy : AbstractPanelStrategy
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
