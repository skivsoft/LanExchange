namespace LanExchange.Interfaces.Services
{
    public interface IConfigPersistenceService
    {
        TConfig Load<TConfig>() where TConfig : class;
        void Save<TConfig>(TConfig config) where TConfig : class;
    }
}
