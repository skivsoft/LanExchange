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
            
            // load and init plugins
            PluginProxy plugins = (PluginProxy)Facade.RetrieveProxy("PluginProxy");
            if (plugins != null)
            {
                plugins.InitializePlugins();

                /*
                IPlugin PLG;

                PLG = new ModelNetwork.ModelNetworkPlugin();
                PLG.Initialize(ApplicationFacade.Instance);
                PLG = new ViewWinForms.ViewWinFormsPlugin();
                PLG.Initialize(ApplicationFacade.Instance);
                */

                //PLG = new ModelPersons.ModelPersonsPlugin();
                //PLG.Initialize(ApplicationFacade.Instance);
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
