using System;
using LanExchange.Sdk;

namespace LanExchange.Model
{
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

        protected ConcreteSubject()
        {
        }

        public string Subject
        {
            get { return String.Empty; }
        }

        public bool IsCacheable
        {
            get { return true; }
        }

        public override string ToString()
        {
            if (this == Root) return "<root>";
            if (this == NotSubscribed) return "<empty>";
            return base.ToString();
        }
    }
}
