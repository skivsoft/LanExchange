using System;
using System.Collections.Generic;
using System.Text;
using PureInterfaces;

namespace LanExchange.SDK
{
    public class Globals
    {
        public const string CMD_LOAD_PLUGINS = "load_plugins";
        public const string CMD_STARTUP = "startup";
        public const string ITEM_COUNT_CHANGED = "itemCountChanged";
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        private string version = "";
        private string description = "";
        private string author = "";

        public string Version
        {
            get { return version; }
            set { version = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Author
        {
            get { return author; }
            set { author = value; }
        }

    }


}
