using System;
using System.Collections.Generic;
using System.Text;
using PureInterfaces;

namespace LanExchange.SDK
{
    /// <summary>
    /// LanExchange plugin inteface
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Initialization of plugin.
        /// </summary>
        /// <param name="facade">ApplicationFacade object of main program</param>
        void Initialize(IFacade facade);
    }
}
