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
            // AD browser
            //Facade.RegisterProxy(new UserProxy());
            // Person browser
            //Facade.RegisterProxy(new PersonProxy());

            IPlugin plugin;

            plugin = new ModelNetwork.ModelNetworkPlugin();
            plugin.Initialize(ApplicationFacade.Instance);

            plugin = new ViewWinForms.ViewWinFormsPlugin();
            plugin.Initialize(ApplicationFacade.Instance);

        }
    }
}
