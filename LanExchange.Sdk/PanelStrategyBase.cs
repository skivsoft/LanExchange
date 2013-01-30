using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Collections.Generic;

namespace LanExchange.Sdk
{
    /// <summary>
    /// Base class for item enumeration strategy displayed in a panel.
    /// </summary>
    public abstract class PanelStrategyBase : IBackgroundStrategy
    {
        private ISubject m_Subject;
        private readonly Collection<PanelItemBase> m_Result;

        /// <summary>
        /// Initializes a new instance of the <see cref="PanelStrategyBase"/> class.
        /// </summary>
        protected PanelStrategyBase()
        {
            m_Result = new Collection<PanelItemBase>();
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public Collection<PanelItemBase> Result
        {
            get { return m_Result; }
        }

        /// <summary>
        /// Determines whether [is subject accepted] [the specified subject].
        /// </summary>
        /// <param name="subject">The subject.</param>
        /// <returns>
        ///   <c>true</c> if [is subject accepted] [the specified subject]; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsSubjectAccepted(ISubject subject)
        {
            // accepting nothing by default
            return false;
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
            return String.Format(CultureInfo.InvariantCulture, "{0}({1})", base.ToString(), m_Subject);
        }
    }
}
