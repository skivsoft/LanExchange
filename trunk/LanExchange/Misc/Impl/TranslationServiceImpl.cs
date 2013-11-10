using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Text;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Misc.Impl
{
    public class TranslationServiceImpl : ITranslationService
    {
        private string[] m_CurrentLanguageLines;
        private string m_CurrentLanguage;

        public TranslationServiceImpl()
        {
            CurrentLanguage = SourceLanguage;
            //CurrentLanguage = "Russian";
        }
        
        [Localizable(false)]
        public string SourceLanguage
        {
            get { return "English"; }
        }

        public string CurrentLanguage
        {
            get { return m_CurrentLanguage; } 
            set
            {
                if (String.Compare(SourceLanguage, value, StringComparison.OrdinalIgnoreCase) == 0)
                    m_CurrentLanguage = value;
                else
                    foreach(var fileName in App.FolderManager.GetLanguagesFiles())
                    {
                        var lang = Path.GetFileNameWithoutExtension(fileName);
                        if (lang != null && String.Compare(lang, value, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            m_CurrentLanguageLines = File.ReadAllLines(fileName);
                            m_CurrentLanguage = value;
                            break;
                        }
                    }
            }
        }

        [Localizable(false)]
        public IDictionary<string, string> GetLanguagesNames()
        {
            const string LANG = "@LANGUAGE_NAME";
            var sorted = new SortedDictionary<string, string>();
            sorted.Add(SourceLanguage, SourceLanguage);
            foreach (var fileName in App.FolderManager.GetLanguagesFiles())
            {
                var lang = Path.GetFileNameWithoutExtension(fileName);
                if (lang == null) continue;
                try
                {
                    sorted.Add(TranslateFromPO(fileName, LANG), lang);
                }
                catch(ArgumentException)
                {
                }
            }
            var result = new Dictionary<string, string>();
            foreach(var pair in sorted)
                result.Add(pair.Value, pair.Key);
            return result;
        }

        [Localizable(false)]
        private string InternalTranslate(IEnumerable<string> lines, string id)
        {
            var marker = string.Format(CultureInfo.InvariantCulture, "msgid \"{0}\"", id);
            var markerFound = false;
            const string MSGSTR_MARKER = "msgstr \"";
            foreach (var line in lines)
            {
                if (line.Equals(marker))
                {
                    markerFound = true;
                    continue;
                }
                if (markerFound && line.StartsWith(MSGSTR_MARKER, StringComparison.Ordinal) && 
                    line.Substring(line.Length - 1, 1).Equals("\""))
                {
                    var result = line.Substring(MSGSTR_MARKER.Length, line.Length - MSGSTR_MARKER.Length - 1);
                    return result;
                }
            }
            return id;
        }

        private string TranslateFromPO(string fileName, string id)
        {
            var lines = File.ReadAllLines(fileName);
            return InternalTranslate(lines, id);
        }

        public string Translate(string id)
        {
            if (SourceLanguage.Equals(CurrentLanguage))
                return id;
            return InternalTranslate(m_CurrentLanguageLines, id);
        }
    }
}
