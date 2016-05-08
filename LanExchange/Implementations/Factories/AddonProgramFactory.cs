using LanExchange.Base;
using LanExchange.Helpers;
using LanExchange.Interfaces.Factories;
using LanExchange.SDK;
using System;
using System.IO;

namespace LanExchange.Implementations.Factories
{
    internal sealed class AddonProgramFactory : IAddonProgramFactory
    {
        private readonly IFolderManager folderManager;

        public AddonProgramFactory(IFolderManager folderManager)
        {
            if (folderManager == null) throw new ArgumentNullException(nameof(folderManager));

            this.folderManager = folderManager;
        }

        public AddonProgramInfo CreateAddonProgramInfo(AddonProgram program)
        {
            var fileName = EnvironmentHelper.ExpandCmdLine(program.FileName);
            if (fileName.Equals(Path.GetFileName(fileName)))
                fileName = Path.Combine(folderManager.CurrentPath, fileName);

            var image = App.Images.GetSmallImageOfFileName(fileName);

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
