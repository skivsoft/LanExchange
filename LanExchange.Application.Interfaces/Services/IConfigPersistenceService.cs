using LanExchange.Presentation.Interfaces.Config;

namespace LanExchange.Application.Interfaces.Services
{
    public interface IConfigPersistenceService
    {
        TConfig Load<TConfig>() where TConfig : ConfigBase, new();
        void Save<TConfig>(TConfig config) where TConfig : ConfigBase;
    }
}
