using System;
using System.IO;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class PluginFileSystem : IPlugin
    {
        private IServiceProvider m_Provider;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;

            // Setup resource manager
            var translationService = (ITranslationService)m_Provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)m_Provider.GetService(typeof(IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<FileRoot>(new PanelItemRootFactory<FileRoot>());
                factoryManager.RegisterFactory<DrivePanelItem>(new DriveFactory());
                factoryManager.RegisterFactory<FilePanelItem>(new FileFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)m_Provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<DrivePanelItem>(new DriveFiller());
                fillerManager.RegisterFiller<FilePanelItem>(new FileFiller());
            }

            // Register images for disk drives
            foreach (var drive in DriveInfo.GetDrives())
                RegisterImageForFileName(drive.RootDirectory.Name);
        }

        public static void RegisterImageForFileName(string fname)
        {
            if (App.Images != null && App.Images.IndexOf(fname) == -1)
            {
                var image1 = App.Images.GetSmallImageOfFileName(fname);
                var image2 = App.Images.GetLargeImageOfFileName(fname);
                App.Images.RegisterImage(fname, image1, image2);
            }
        }
    }
}
