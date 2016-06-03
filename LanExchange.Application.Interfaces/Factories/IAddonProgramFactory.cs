using LanExchange.Presentation.Interfaces.Addons;

namespace LanExchange.Application.Interfaces.Factories
{
    public interface IAddonProgramFactory
    {
        AddonProgramInfo CreateAddonProgramInfo(AddonProgram program);
        AddonProgram CreateFromProtocol(string protocol);
   }
}
