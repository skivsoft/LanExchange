using System;

namespace LanExchange.Sdk
{
    /// <summary>
    /// LanExchange common subjects: Root, NotSubscribed, UserItems.
    /// </summary>
    public class ConcreteSubject : ISubject
    {
        /// <summary>
        /// Root subject need to enumerate very first level.
        /// Ex.: domain list can be returned for Root subject.
        /// </summary>
        public static readonly ConcreteSubject Root = new ConcreteSubject();
        /// <summary>
        /// Subject for unsubscription.
        /// </summary>
        public static readonly ConcreteSubject NotSubscribed = new ConcreteSubject();
        /// <summary>
        /// Subject for showing user added items.
        /// </summary>
        public static readonly ConcreteSubject UserItems = new ConcreteSubject();

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
                if (this == Root) return "<Root>";
                if (this == NotSubscribed) return "<NotSubscribed>";
                if (this == UserItems) return "<UserItems>";
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
