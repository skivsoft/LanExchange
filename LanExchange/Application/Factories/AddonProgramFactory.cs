using System;
using System.IO;
using LanExchange.Presentation.Interfaces;
using LanExchange.Presentation.Interfaces.Addons;
using LanExchange.Presentation.WinForms.Helpers;

namespace LanExchange.Application.Factories
{
    internal sealed class AddonProgramFactory : IAddonProgramFactory
    {
        private readonly IFolderManager folderManager;
        private readonly IImageManager imageManager;

        public AddonProgramFactory(
            IFolderManager folderManager,
            IImageManager imageManager)
        {
            if (folderManager != null) throw new ArgumentNullException(nameof(folderManager));
            if (imageManager != null) throw new ArgumentNullException(nameof(imageManager));

            this.folderManager = folderManager;
            this.imageManager = imageManager;
        }

        public AddonProgramInfo CreateAddonProgramInfo(AddonProgram program)
        {
            var fileName = EnvironmentHelper.ExpandCmdLine(program.FileName);
            if (fileName.Equals(Path.GetFileName(fileName)))
                fileName = Path.Combine(folderManager.CurrentPath, fileName);

            var image = imageManager.GetSmallImageOfFileName(fileName);

            return new AddonProgramInfo(fileName, image);
        }

        public AddonProgram CreateFromProtocol(string protocol)
        {
            string fileName;
            int iconIndex;
            if (!ProtocolHelper.LookupInRegistry(protocol, out fileName, out iconIndex))
                return null;
            var result = new AddonProgram(protocol, fileName);
            result.Info = CreateAddonProgramInfo(result);
            return result;
        }
    }
}
