using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using LanExchange.View;
using LanExchange.Demodata;


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

            // register proxies and select strategies (demo or netapi32)
            bool DemoMode = (bool)notification.Body;
            if (DemoMode)
            {
                // demodata
                Facade.RegisterProxy(new LanDomainProxy(new DemoDomainEnumStrategy()));
                Facade.RegisterProxy(new LanComputerProxy(new DemoComputerEnumStrategy()));
                Facade.RegisterProxy(new LanShareProxy(new DemoShareEnumStrategy()));
                Facade.RegisterProxy(new PersonProxy(new DemoPersonEnumStrategy()));
            }
            else
            {
                // network browser
                Facade.RegisterProxy(new LanDomainProxy(new NetApi32DomainEnumStrategy()));
                Facade.RegisterProxy(new LanComputerProxy(new NetApi32ComputerEnumStrategy()));
                Facade.RegisterProxy(new LanShareProxy(new NetApi32ShareEnumStrategy()));
                //Facade.RegisterProxy(new FileProxy(new FileEnumStrategy()));
                //Facade.RegisterProxy(new ArchiveProxy(new ArchiveEnumStrategy()));
                // personal explorer
                Facade.RegisterProxy(new PersonProxy(new PersonEnumStrategy()));
            }

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
