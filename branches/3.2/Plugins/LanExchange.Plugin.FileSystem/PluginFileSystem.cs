using System;
using System.IO;
using LanExchange.Plugin.FileSystem.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.FileSystem
{
    public class PluginFileSystem : IPlugin
    {
        private static IImageManager s_ImageManager;
        public static ColumnSizeFormat SizeFormat = ColumnSizeFormat.Kilobyte;

        public void Initialize(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) return;

            var provider = serviceProvider;

            s_ImageManager = (IImageManager) provider.GetService(typeof (IImageManager));

            // Setup resource manager
            var translationService = (ITranslationService)provider.GetService(typeof(ITranslationService));
            if (translationService != null)
                translationService.SetResourceManagerTo<Resources>();

            // Register new panel item types
            var factoryManager = (IPanelItemFactoryManager)provider.GetService(typeof(IPanelItemFactoryManager));
            if (factoryManager != null)
            {
                factoryManager.RegisterFactory<FileRoot>(new PanelItemRootFactory<FileRoot>());
                factoryManager.RegisterFactory<DrivePanelItem>(new DriveFactory());
                factoryManager.RegisterFactory<FilePanelItem>(new FileFactory());
            }

            // Register new panel fillers
            var fillerManager = (IPanelFillerManager)provider.GetService(typeof(IPanelFillerManager));
            if (fillerManager != null)
            {
                fillerManager.RegisterFiller<DrivePanelItem>(new DriveFiller());
                fillerManager.RegisterFiller<FilePanelItem>(new FileFiller());
            }
        }

        /// <summary>
        /// Register image in ImageManager for specified fname unless if it already registered.
        /// </summary>
        /// <param name="fileName"></param>
        public static void RegisterImageForFileName(string fileName)
        {
            if (s_ImageManager == null || s_ImageManager.IndexOf(fileName) != -1)
                return;

            var image1 = s_ImageManager.GetSmallImageOfFileName(fileName);
            var image2 = s_ImageManager.GetLargeImageOfFileName(fileName);
            s_ImageManager.RegisterImage(fileName, image1, image2);
        }
    }
}
