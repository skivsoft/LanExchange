using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK.SDKModel;

namespace LanExchange.Model
{
    public class PluginProxy : Proxy, IPluginProxy, IProxy
    {
        public new const string NAME = "PluginProxy";

        public PluginProxy()
            : base(NAME)
        {

        }

        public void InitializePlugins()
        {

        }
    }
}
