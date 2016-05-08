using LanExchange.Base;

namespace LanExchange.Interfaces.Factories
{
    public interface IAddonProgramFactory
    {
        AddonProgramInfo CreateAddonProgramInfo(AddonProgram program);
        AddonProgram CreateFromProtocol(string protocol);
   }
}
