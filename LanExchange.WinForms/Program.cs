// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

using LanExchange.Extensions;
using System;

namespace LanExchange
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CoreFacade.InitializeDIContainer()
                .Resolve<LanExchangeApp>()
                .Run();
        }
    }
}