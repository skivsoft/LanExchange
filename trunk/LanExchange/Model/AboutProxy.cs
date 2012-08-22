using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;

namespace LanExchange.Model
{
    public class AboutProxy : Proxy, IProxy
    {
        public new const string NAME = "AboutProxy";

        public AboutProxy()
            : base(NAME)
        {

        }
    }
}
