using LanExchange.Base;
using LanExchange.Helpers;
using LanExchange.Interfaces.Factories;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using LanExchange.Application.Interfaces;

namespace LanExchange.Implementations.Factories
{
    internal sealed class AddonProgramFactory : IAddonProgramFactory
    {
        private readonly IFolderManager folderManager;
        private readonly IImageManager imageManager;

        public AddonProgramFactory(
            IFolderManager folderManager,
            IImageManager imageManager)
        {
            Contract.Requires<ArgumentNullException>(folderManager != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);

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
