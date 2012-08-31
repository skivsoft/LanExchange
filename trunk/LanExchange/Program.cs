/*
 * LanExchange - Computer and shared folders browser for local networks.
 *   
 * 
 * Change log
 * 
 * 2012-08-30 Skiv  - Strategy Pattern implmented for enum objects in proxies
 * 2012-08-29 Skiv  - Async run EnumObjects via BackgroundWorked
 * 
 * 
 * 
 */

using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace LanExchange
{
    static class Program
    {

        static bool CheckParam(string param)
        {
            bool Result = false;
            string[] args = Environment.GetCommandLineArgs();
            foreach (string str in args)
            {
                if (String.Compare(str, param, true) == 0)
                {
                    Result = true;
                    break;
                }
            }
            return Result;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Run Application in MVC-style
            AppFacade facade = (AppFacade)AppFacade.Instance;
            facade.SendNotification(AppFacade.CMD_STARTUP, CheckParam("/DEMO"));
        }
    }

}
