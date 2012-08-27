using System;
using System.Collections.Generic;
using System.Text;

namespace LanExchange
{
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
