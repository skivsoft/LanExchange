using System;
using LanExchange.Model;

namespace LanExchange.Strategy.Panel
{
    public abstract class AbstractPanelStrategy : IBackgroundStrategy
    {
        private readonly string m_Subject;

        protected AbstractPanelStrategy(string subject)
        {
            m_Subject = subject;
        }

        public abstract void Algorithm();
        public abstract bool AcceptParent(AbstractPanelItem parent);

        public string Subject
        {
            get { return m_Subject; }
        }

        public override string ToString()
        {
            return String.Format("{0}(\"{1}\")", base.ToString(), m_Subject);
        }
    }
}
