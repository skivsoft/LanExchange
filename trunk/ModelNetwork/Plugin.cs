using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;
using PureInterfaces;
using ModelNetwork.Model;
using LanExchange.SDK.SDKModel;

namespace ModelNetwork
{
    [Plugin(Description = "Local network computers and shared folders browser", Version = "1.0", Author = "Skiv")]
    public class ModelNetworkPlugin : IPlugin
    {

        public void Initialize(IFacade facade)
        {
            Globals.Facade = facade;

            if (facade != null)
            {
                // network browser
                facade.RegisterProxy(new DomainProxy());
                facade.RegisterProxy(new ComputerProxy());
                facade.RegisterProxy(new ShareProxy());
                facade.RegisterProxy(new FileProxy());

                INavigatorProxy navigator = (INavigatorProxy)facade.RetrieveProxy("NavigatorProxy");
                if (navigator != null)
                {
                    navigator.AddTransition("DomainProxy", "ComputerProxy");
                    navigator.AddTransition("ComputerProxy", "ShareProxy");
                }
            }
        }
    }
}
