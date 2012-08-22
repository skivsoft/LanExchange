using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;

namespace LanExchange.Model
{
    public class MenuProxy : Proxy, IProxy
    {
        public new const string NAME = "MenuProxy";

        public MenuProxy()
            : base(NAME)
        {

        }
    }
}
