using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface ITranslationService
    {
        string SourceLanguage { get; }
        string CurrentLanguage { get; set; }
        IDictionary<string, string> GetLanguagesNames();
        IDictionary<string, string> GetTranslations();
        string Translate(string id);
        string PluralForm(string forms, int num);
    }
}
