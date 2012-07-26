using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using LanExchange.Controller;
using PureMVC.Interfaces;

namespace LanExchange
{
    public class ApplicationFacade : Facade
    {
        #region Notification name constants
        
        public const string STARTUP    = "startup";
        //public const string LEVEL_DOWN = "levelDown";
        //public const string LEVEL_UP   = "levelUp";
        public const string ITEM_COUNT_CHANGED = "itemCountChanged";

        #endregion

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

        #region Public methods

        /// <summary>
        /// Start the application
        /// </summary>
        /// <param name="app"></param>
        public void Startup(object app)
        {
            SendNotification(STARTUP, app);
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
            RegisterCommand(STARTUP, typeof(StartupCommand));
        }

        #endregion

    }
}
