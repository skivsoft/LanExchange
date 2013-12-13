using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Misc.Impl
{
    [Localizable(false)]
    public class TranslationServiceImpl : ITranslationService
    {
        private const string LANGUAGE_NAME = "@LANGUAGE_NAME";
        private const string AUTHOR = "@AUTHOR";

        private readonly IList<string> m_CurrentLanguageLines;
        private string m_CurrentLanguage;

        public TranslationServiceImpl()
        {
            m_CurrentLanguageLines = new List<string>();
            CurrentLanguage = SourceLanguage;
            //CurrentLanguage = "Russian";
        }
        
        public string SourceLanguage
        {
            get { return "English"; }
        }

        private IEnumerable<string> ReadAllLines(string fileName)
        {
            string line;
            using (var sr = new StreamReader(fileName, Encoding.UTF8))
                while ((line = sr.ReadLine()) != null)
                    yield return line;
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
                            m_CurrentLanguageLines.Clear();
                            foreach(var line in ReadAllLines(fileName))
                                m_CurrentLanguageLines.Add(line);
                            m_CurrentLanguage = value;
                            break;
                        }
                    }
            }
        }

        public IDictionary<string, string> GetLanguagesNames()
        {
            var sorted = new SortedDictionary<string, string>();
            sorted.Add(SourceLanguage, SourceLanguage);
            foreach (var fileName in App.FolderManager.GetLanguagesFiles())
            {
                var lang = Path.GetFileNameWithoutExtension(fileName);
                if (lang == null) continue;
                try
                {
                    sorted.Add(TranslateFromPO(fileName, LANGUAGE_NAME), lang);
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

        public IDictionary<string, string> GetTranslations()
        {
            var result = new SortedDictionary<string, string>();
            foreach (var fileName in App.FolderManager.GetLanguagesFiles())
            {
                var lang = Path.GetFileNameWithoutExtension(fileName);
                if (lang == null) continue;
                try
                {
                    result.Add(lang, TranslateFromPO(fileName, AUTHOR));
                }
                catch (ArgumentException)
                {
                }
            }
            return result;
        }

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
            return InternalTranslate(ReadAllLines(fileName), id);
        }

        public string Translate(string id)
        {
            if (SourceLanguage.Equals(CurrentLanguage))
                return id;
            return InternalTranslate(m_CurrentLanguageLines, id);
        }

        public string PluralForm(string forms, int num)
        {
            switch (CurrentLanguage)
            {
                case "Kazakh":
                    var arr = forms.Split('|');
                    var index = PluralFormKazakh(num);
                    return index <= arr.Length - 1 ? arr[index] : arr[0];
                default:
                    return forms;
            }
        }

        /// <summary>
        /// Translation of plural form in Kazakh language.
        /// </summary>
        /// <param name="num"></param>
        /// <returns>0: "-дан", 1: "-ден", 2: "-нан", 3: "-нен", 4: "-тан", 5: "-тен".</returns>
        private static int PluralFormKazakh(int num)
        {
            if (num%10 == 6 || num%10 == 9 || num%100 == 20 || num%100 == 30)
                return 0;
            if (num%10 == 1 || num%10 == 2 || num%10 == 7 || num%10 == 8 || num%100 == 50 || num%1000 == 100)
                return 1;
            if (num%100 == 10 || num%100 == 90)
                return 2;
            if (num%100 == 80)
                return 3;
            if (num%100 == 40 || num%100 == 60)
                return 4;
            if (num%10 == 3 || num%10 == 4 || num%10 == 5 || num%100 == 70)
                return 5;
            return 0;
        }

        [Localizable(false)]
        public void SetResourceManagerTo<TClass>() where TClass : class
        {
            var resourceMan = new TranslationResourceManager(typeof (TClass).FullName, typeof (TClass).Assembly);
            ReflectionUtils.SetClassPrivateField<TClass, ResourceManager>("resourceMan", resourceMan);
        }
    }
}