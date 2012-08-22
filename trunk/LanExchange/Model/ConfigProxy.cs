using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;

namespace LanExchange.Model
{
    public class ConfigProxy : Proxy, IProxy
    {
        public new const string NAME = "ConfigProxy";

        public ConfigProxy()
            : base(NAME)
        {
        }
    }
}
