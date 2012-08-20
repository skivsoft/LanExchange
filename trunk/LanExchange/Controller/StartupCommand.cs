using System;
using System.Collections.Generic;
using System.Text;
using PureInterfaces;
using PurePatterns;
using LanExchange.Model;
using LanExchange.SDK;

namespace LanExchange.Controller
{
    public class StartupCommand : SimpleCommand, ICommand
    {

        public override void Execute(INotification notification)
        {
            Facade.RegisterProxy(new CurrentUserProxy());
            // network browser
            Facade.RegisterProxy(new DomainProxy());
            Facade.RegisterProxy(new ComputerProxy());
            //Facade.RegisterProxy(new ResourceProxy());
            //Facade.RegisterProxy(new FileProxy());
            // AD browser
            //Facade.RegisterProxy(new UserProxy());
            // Person browser
            //Facade.RegisterProxy(new PersonProxy());

            IPlugin plugin = new ViewWinForms.ViewWinFormsPlugin();
            plugin.Initialize(ApplicationFacade.Instance);

        }
    }
}
