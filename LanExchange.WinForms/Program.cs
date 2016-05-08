// *****************************************************************************
// START DATE: Jan 22, 2012
// *****************************************************************************

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
            var container = CoreFacade.InitializeIoCContainer();
            using (var application = (LanExchangeApp)container.GetService(typeof(LanExchangeApp)))
            {
                application.Run();
            }
        }
    }
}