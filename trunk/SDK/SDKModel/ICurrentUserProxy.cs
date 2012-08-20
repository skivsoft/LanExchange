using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange.SDK.SDKModel
{
    /// <summary>
    /// ICurrentUserProxy interface
    /// </summary>
    public interface ICurrentUserProxy
    {
        /// <summary>
        /// Domain name.
        /// </summary>
        string DomainName { get; }

        /// <summary>
        /// Computer name.
        /// </summary>
        string ComputerName { get; }

        /// <summary>
        /// Current user name.
        /// </summary>
        string UserName { get; }
    }
}
