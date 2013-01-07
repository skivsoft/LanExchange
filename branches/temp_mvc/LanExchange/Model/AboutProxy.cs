using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.PurePatterns;
using PureMVC.PureInterfaces;

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
