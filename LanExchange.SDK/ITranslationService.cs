using System.Collections.Generic;

namespace LanExchange.SDK
{
    public interface ITranslationService
    {
        string CurrentLanguage { get; set; }
        bool RightToLeft { get; }
        IDictionary<string, string> GetLanguagesNames();
        IDictionary<string, string> GetTranslations();
        string Translate(string id);
        string PluralForm(string forms, int num);
        void SetResourceManagerTo<TClass>() where TClass : class;
        void RegisterTranslit<TTranslit>() where TTranslit : ITranslitStrategy;
    }
}
