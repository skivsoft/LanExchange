using System;
using System.Collections.Generic;
using System.Text;
using PureInterfaces;
using PurePatterns;
using LanExchange.Model;
using LanExchange.SDK;
using LanExchange.SDK.SDKView;
using LanExchange.SDK.SDKModel;

namespace LanExchange.Controller
{
    public class StartupCommand : SimpleCommand, ICommand
    {

        public override void Execute(INotification notification)
        {
            // set current language
            IResourceProxy resource = (IResourceProxy)Facade.RetrieveProxy("ResourceProxy");
            if (resource != null)
            {
                resource.CurrentLanguage = "russian";
            }
            
            // load and init plugins
            IPluginProxy plugins = (IPluginProxy)Facade.RetrieveProxy("PluginProxy");
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
