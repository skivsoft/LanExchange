using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using LanExchange.Controller;
using PureInterfaces;
using LanExchange.SDK;

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

        #endregion

    }
}
