using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using LanExchange.View;


namespace LanExchange.Controller
{
    public class StartupCommand : SimpleCommand, ICommand
    {

        public override void Execute(INotification notification)
        {
            // set current language
            ResourceProxy resource = (ResourceProxy)Facade.RetrieveProxy("ResourceProxy");
            if (resource != null)
            {
                resource.CurrentLanguage = "russian";
            }

            // network browser
            Facade.RegisterProxy(new DomainProxy());
            Facade.RegisterProxy(new DomainProxy());
            Facade.RegisterProxy(new ComputerProxy());
            Facade.RegisterProxy(new ShareProxy());
            //facade.RegisterProxy(new FileProxy());

            // personal explorer
            Facade.RegisterProxy(new PersonProxy());

            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy("NavigatorProxy");
            if (navigator != null)
            {
                navigator.AddTransition("DomainProxy", "ComputerProxy");
                navigator.AddTransition("ComputerProxy", "ShareProxy");

                navigator.AddTransition("PersonProxy", String.Empty);
            }


            Facade.SendNotification(Globals.UPDATE_ITEMS);

            // run app!
            IApplicationMediator App = (IApplicationMediator)Facade.RetrieveMediator("ApplicationMediator");
            if (App != null)
            {
                App.Run();
            }
        }
    }
}
