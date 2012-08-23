using System;
using LanExchange.SDK;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

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
            // Run Application in MVC-style
            ApplicationFacade facade = (ApplicationFacade)ApplicationFacade.Instance;
            facade.SendNotification(Globals.CMD_STARTUP);
        }
    }

}
