using LanExchange.SDK;

namespace LanExchange.Interfaces.Services
{
    public interface IPagesPersistenceService
    {
        void LoadSettings(out IPagesModel pages);
        void SaveSettings(IPagesModel pages);
    }
}
