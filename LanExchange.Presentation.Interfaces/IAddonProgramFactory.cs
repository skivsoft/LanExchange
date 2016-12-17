using LanExchange.Presentation.Interfaces.Addons;

namespace LanExchange.Presentation.Interfaces
{
    public interface IAddonProgramFactory
    {
        AddonProgramInfo CreateAddonProgramInfo(AddonProgram program);

        AddonProgram CreateFromProtocol(string protocol);
   }
}
