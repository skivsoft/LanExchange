using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.PurePatterns;
using LanExchange.Controller;
using PureMVC.PureInterfaces;
using LanExchange.Model;
using System.IO;
using System.Windows.Forms;
using LanExchange.View;
using System.Reflection;

namespace LanExchange
{
    public class AppFacade : Facade
    {
        public const string CMD_STARTUP = "startup";
        public const string UPDATE_ITEMS = "update_items";
        public const string ITEM_COUNT_CHANGED = "item_count_changed";
        public const string INTERFACE_LANGUAGE_CHANGED = "interface_language_changed";


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
                        if (m_instance == null) m_instance = new AppFacade();
                    }
                }

                return m_instance;
            }
        }

        #endregion

        #region Protected & Internal Methods

        protected AppFacade()
        {
            // Protected constructor.
        }

        /// <summary>
        /// Explicit static constructor to tell C# compiler 
        /// not to mark type as beforefieldinit
        ///</summary>
        static AppFacade()
        {
        }

        /// <summary>
        /// Register Commands with the Controller
        /// </summary>
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(AppFacade.CMD_STARTUP, typeof(StartupCommand));
        }

        protected override void InitializeModel()
        {
            base.InitializeModel();
            RegisterProxy(new ConfigProxy());
            RegisterProxy(new ResourceProxy());
            RegisterProxy(new NavigatorProxy());
            RegisterProxy(new AboutProxy());
            RegisterProxy(new CurrentUserProxy());
            RegisterProxy(new MenuProxy());
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

        public static ResourceProxy Resources
        {
            get
            {
                if (object.ReferenceEquals(m_Resources, null))
                {
                    ResourceProxy temp = (ResourceProxy)Instance.RetrieveProxy("ResourceProxy");
                    m_Resources = temp;
                }
                return m_Resources;
            }
        }
        private static ResourceProxy m_Resources = null;

        /// <summary>
        /// Returns language specific string by unique name from ResourceProxy model.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string T(string name)
        {
            if (Resources != null)
                return Resources.GetText(name);
            else
                return name;
        }

        private static void InternalLocalizeControl(Control control, string path)
        {
            // localize control's string properties
            Type type = control.GetType();
            foreach (PropertyInfo Info in type.GetProperties())
            {
                if (Info.PropertyType.Equals(typeof(String)) && Info.CanWrite)
                {
                    string S = AppFacade.Resources.GetText(String.Format("{0}.{1}", path, Info.Name));
                    if (!String.IsNullOrEmpty(S))
                        type.GetProperty(Info.Name).SetValue(control, S, null);
                    else
                    {
                        S = AppFacade.Resources.GetText(String.Format(".{0}.{1}", control.Name, Info.Name));
                        if (!String.IsNullOrEmpty(S))
                            type.GetProperty(Info.Name).SetValue(control, S, null);
                    }
                }
            }
            // localize child controls
            foreach (Control C in control.Controls)
                InternalLocalizeControl(C, path + "." + C.Name);
        }

        public static void LocalizeControl(Control control)
        {
            if (AppFacade.Resources == null)
                return;
            InternalLocalizeControl(control, control.Name);
        }
    }
}
