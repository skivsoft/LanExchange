using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;
using PureInterfaces;
using ModelNetwork.Model;

namespace ModelNetwork
{
    [Plugin(Description = "Local network computers and shared folders browser", Version = "1.0", Author = "Skiv")]
    public class ModelNetworkPlugin : IPlugin
    {
        public IFacade AppFacade = null;

        public void Initialize(IFacade facade)
        {
            AppFacade = facade;

            if (AppFacade != null)
            {
                // network browser
                AppFacade.RegisterProxy(new DomainProxy());
                AppFacade.RegisterProxy(new ComputerProxy());
                AppFacade.RegisterProxy(new ResourceProxy());
                //Facade.RegisterProxy(new FileProxy());
            }
        }
    }
}
