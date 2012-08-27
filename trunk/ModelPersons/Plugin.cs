using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using PureMVC.PureInterfaces;
using ModelPersons.Model;
using LanExchange.Model;

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

                NavigatorProxy navigator = (NavigatorProxy)facade.RetrieveProxy("NavigatorProxy");
                if (navigator != null)
                {
                    navigator.AddTransition("PersonProxy", String.Empty);
                }
            }
        }
    }
}
