using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface ITranslationService
    {
        string SourceLanguage { get; }
        string CurrentLanguage { get; set; }
        IDictionary<string, string> GetLanguagesNames();
        string Translate(string id);
    }
}
