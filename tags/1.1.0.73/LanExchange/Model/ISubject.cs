using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.Model
{
    /// <summary>
    /// ISubject interface declaration in Subscription-Subscriber-Subject model.
    /// </summary>
    public interface ISubject
    {
        string Subject { get; }
        bool IsCacheable { get; }
    }
}
