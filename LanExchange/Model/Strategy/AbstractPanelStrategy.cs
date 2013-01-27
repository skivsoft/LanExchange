using System;
using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Sdk;

namespace LanExchange.Model.Strategy
{
    public abstract class AbstractPanelStrategy : IBackgroundStrategy
    {
        private ISubject m_Subject;
        protected List<AbstractPanelItem> m_Result;

        public virtual void AcceptSubject(ISubject subject, out bool accepted)
        {
            // accepting nothing by default
            accepted = false;
        }

        public ISubject Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        public IList<AbstractPanelItem> Result
        {
            get { return m_Result; }
        }

        public abstract void Algorithm();

        public override string ToString()
        {
            return String.Format("{0}({1})", base.ToString(), m_Subject);
        }
    }
}
