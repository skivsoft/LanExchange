namespace LanExchange.Application.Interfaces
{
    public interface IPagesPersistenceService
    {
        void LoadSettings(out IPagesModel pages);
        void SaveSettings(IPagesModel pages);
    }
}
