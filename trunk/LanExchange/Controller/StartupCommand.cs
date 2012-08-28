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
            ResourceProxy resource = (ResourceProxy)Facade.RetrieveProxy(ResourceProxy.NAME);
            if (resource != null)
            {
                resource.CurrentLanguage = "russian";
            }

            // network browser
            Facade.RegisterProxy(new LanDomainProxy());
            Facade.RegisterProxy(new LanComputerProxy());
            //Facade.RegisterProxy(new LanShareProxy());
            //facade.RegisterProxy(new FileProxy());

            // personal explorer
            Facade.RegisterProxy(new PersonProxy());

            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy(NavigatorProxy.NAME);
            if (navigator != null)
            {
                navigator.AddTransition(LanDomainProxy.NAME, LanComputerProxy.NAME);
                navigator.AddTransition(LanComputerProxy.NAME, LanShareProxy.NAME);
                navigator.AddTransition(LanShareProxy.NAME, FileProxy.NAME);
                navigator.AddTransition(FileProxy.NAME, ArchiveProxy.NAME);
                
                navigator.AddTransition(PersonProxy.NAME, String.Empty);
            }
            Facade.RegisterMediator(new ApplicationMediator());

            // initial update of panel
            Facade.SendNotification(AppFacade.UPDATE_ITEMS);

            // run app!
            IApplicationMediator App = (IApplicationMediator)Facade.RetrieveMediator(ApplicationMediator.NAME);
            if (App != null)
            {
                App.Run();
            }
        }
    }
}
