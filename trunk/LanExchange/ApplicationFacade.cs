using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.PurePatterns;
using LanExchange.Controller;
using PureMVC.PureInterfaces;
using LanExchange.Model;
using System.IO;

namespace LanExchange
{
    public class ApplicationFacade : Facade
    {
        #region Accessors

        /// <summary>
        /// Facade Singleton Factory method.  This method is thread safe.
        /// </summary>
        public new static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null) m_instance = new ApplicationFacade();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Protected & Internal Methods

        protected ApplicationFacade()
        {
            // Protected constructor.
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        ///</summary>
        static ApplicationFacade()
        {
        }

        /// <summary>
        /// Register Commands with the Controller
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(Globals.CMD_STARTUP, typeof(StartupCommand));
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();
            RegisterProxy(new ConfigProxy());
            RegisterProxy(new ResourceProxy());
            RegisterProxy(new NavigatorProxy());
            RegisterProxy(new PluginProxy());
            RegisterProxy(new AboutProxy());
            RegisterProxy(new CurrentUserProxy());
            RegisterProxy(new MenuProxy());

            RegisterProxy(new DomainProxy());
        }
        #endregion

        public static string ExecutablePath
        {
            get
            {
                if (m_ExecutablePath == null)
                {
                    string[] args = Environment.GetCommandLineArgs();
                    m_ExecutablePath = Path.GetDirectoryName(args[0]);
                }
                return m_ExecutablePath;
            }
        }

        private static string m_ExecutablePath = null;

        //public static AppDomain PluginDomain = null;
    }
}
