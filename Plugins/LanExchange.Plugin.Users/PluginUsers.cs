using System;
using System.ComponentModel.Composition;
using LanExchange.Plugin.Users.Properties;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Extensions;

namespace LanExchange.Plugin.Users
{
    [Export(typeof(IPlugin))]
    public sealed class PluginUsers : IPlugin
    {
        public const string LDAP_PREFIX = "LDAP:// ";


        public static ISystemInformationService sysInfoService;

        public void Initialize(IServiceProvider serviceProvider)
        {
            // Setup resource manager
            var translationService = serviceProvider.Resolve<ITranslationService>();
            translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = serviceProvider.Resolve<IPanelItemFactoryManager>();
            factoryManager.RegisterFactory<UserRoot>(new PanelItemRootFactory<UserRoot>());
            factoryManager.RegisterFactory<UserPanelItem>(new UserFactory());
            factoryManager.RegisterFactory<WorkspaceRoot>(new PanelItemRootFactory<WorkspaceRoot>());
            factoryManager.RegisterFactory<WorkspacePanelItem>(new WorkspaceFactory());

            // Register new panel fillers
            sysInfoService = serviceProvider.Resolve<ISystemInformationService>();
            var fillerManager = serviceProvider.Resolve<IPanelFillerManager>();
            fillerManager.RegisterFiller<UserPanelItem>(new UserFiller());
            fillerManager.RegisterFiller<WorkspacePanelItem>(new WorkspaceFiller());
        }
    }
}