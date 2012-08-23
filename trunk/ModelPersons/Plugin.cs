using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;
using PureInterfaces;
using ModelPersons.Model;
using LanExchange.SDK.SDKModel;

namespace ModelPersons
{
    [Plugin(Description = "Persons explorer via REST service", Version = "1.0", Author = "Skiv")]
    public class ModelPersonsPlugin : IPlugin
    {
        public void Initialize(IFacade facade)
        {
            Globals.Facade = facade;

            if (facade != null)
            {
                facade.RegisterProxy(new PersonProxy());

                INavigatorProxy navigator = (INavigatorProxy)facade.RetrieveProxy("NavigatorProxy");
                if (navigator != null)
                {
                    navigator.AddTransition("PersonProxy", String.Empty);
                }
            }
        }
    }
}
