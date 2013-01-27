using System;
using System.Collections.Generic;

namespace LanExchange.Sdk
{
    /// <summary>
    /// Base class for item enumeration strategy displayed in a panel.
    /// </summary>
    public abstract class PanelStrategyBase : IBackgroundStrategy
    {
        private ISubject m_Subject;
        private List<PanelItemBase> m_Result;

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public List<PanelItemBase> Result
        {
            get { return m_Result; }
            set { m_Result = value; }
        }

        /// <summary>
        /// Accepts the subject.
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <param name="accepted">if set to <c>true</c> [accepted].</param>
        public virtual void AcceptSubject(ISubject subject, out bool accepted)
        {
            // accepting nothing by default
            accepted = false;
        }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public ISubject Subject
        {
            get { return m_Subject; }
            set { m_Subject = value; }
        }

        /// <summary>
        /// Run strategy.
        /// </summary>
        public abstract void Algorithm();

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("{0}({1})", base.ToString(), m_Subject);
        }
    }
}
