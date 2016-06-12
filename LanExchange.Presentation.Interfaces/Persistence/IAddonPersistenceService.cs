using LanExchange.Presentation.Interfaces.Addons;

namespace LanExchange.Presentation.Interfaces.Persistence
{
    public interface IAddonPersistenceService
    {
        AddOn Load(string fileName);      
    }
}
