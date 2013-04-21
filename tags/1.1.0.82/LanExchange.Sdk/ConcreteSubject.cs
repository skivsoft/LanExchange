using System;

namespace LanExchange.Sdk
{
    /// <summary>
    /// LanExchange common subjects: s_Root, s_NotSubscribed, s_UserItems.
    /// </summary>
    public class ConcreteSubject : ISubject
    {
        /// <summary>
        /// s_Root subject need to enumerate very first level.
        /// Ex.: domain list can be returned for s_Root subject.
        /// </summary>
        public static readonly ConcreteSubject s_Root = new ConcreteSubject();
        /// <summary>
        /// Subject for unsubscription.
        /// </summary>
        public static readonly ConcreteSubject s_NotSubscribed = new ConcreteSubject();
        /// <summary>
        /// Subject for showing user added items.
        /// </summary>
        public static readonly ConcreteSubject s_UserItems = new ConcreteSubject();

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteSubject"/> class.
        /// </summary>
        protected ConcreteSubject()
        {
        }

        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject
        {
            get
            {
                if (this == s_Root) return "<Root>";
                if (this == s_NotSubscribed) return "<NotSubscribed>";
                if (this == s_UserItems) return "<UserItems>";
                return String.Empty;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is cacheable.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is cacheable; otherwise, <c>false</c>.
        /// </value>
        public bool IsCacheable
        {
            get { return true; }
        }
    }
}
