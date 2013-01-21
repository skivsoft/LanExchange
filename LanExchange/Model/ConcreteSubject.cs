using System;
using System.Collections.Generic;
using System.Text;

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
        public static readonly ConcreteSubject Empty = new ConcreteSubject();

        protected ConcreteSubject()
        {
        }

        public string Subject
        {
            get { return String.Empty; }
        }

        public override string ToString()
        {
            if (this == Root) return "<root>";
            if (this == Empty) return "<empty>";
            return base.ToString();
        }
    }
}
