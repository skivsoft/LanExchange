using System;
using System.Collections.Generic;
using System.Text;
using PureInterfaces;
using LanExchange.SDK.SDKModel;
using PurePatterns;

namespace LanExchange.SDK
{
    public class Globals
    {
        #region LanExchange SDK notification constants

        public const string CMD_STARTUP = "startup";
        public const string UPDATE_ITEMS = "update_items";
        public const string ITEM_COUNT_CHANGED = "item_count_changed";

        #endregion

        public static IFacade Facade = null;

        public static IResourceProxy Resources
        {
            get
            {
                if (object.ReferenceEquals(m_Resources, null))
                {
                    IResourceProxy temp = (IResourceProxy)Facade.RetrieveProxy("ResourceProxy");
                    m_Resources = temp;
                }
                return m_Resources;
            }
        }
        private static IResourceProxy m_Resources = null;

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
                return "";
        }
    }
}
