using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Plugin.Translit
{
    internal class KazakhCyrillicToArabic : ITranslitStrategy
    {
        private string TR(char ch, char upper)
        {
            switch (upper)
            {
                case 'А': return "ا";
                case 'Ә': return "ا";
                case 'Б': return "ب";
                case 'В': return "أ";
                case 'Г': return "گ";
                case 'Ғ': return "ع";
                case 'Д': return "د";
                case 'Е': return "ة";
                case 'Ё': return "ة";
                case 'Ж': return "ج";
                case 'З': return "ز";
                case 'И': return "ي";
                case 'Й': return "ي";
                case 'К': return "ك";
                case 'Қ': return "ق";
                case 'Л': return "ل";
                case 'М': return "م";
                case 'Н': return "ن";
                case 'Ң': return "ث";
                case 'О': return "و";
                case 'Ө': return "و";
                case 'П': return "پ";
                case 'Р': return "ر";
                case 'С': return "س";
                case 'Т': return "ت";
                case 'У': return "ؤ";
                case 'Ү': return "ذ";
                case 'Ұ': return "ذ";
                case 'Ф': return "ف";
                case 'Х': return "ح";
                case 'Һ': return "ھ";
                case 'Ц': return "چ";
                case 'Ч': return "ش";
                case 'Ш': return "ش";
                case 'Щ': return "ﯼ";
                case 'Ъ': return null;
                case 'Ы': return "ئ";
                case 'І': return "ئ";
                case 'Ь': return null;
                case 'Э': return "ة";
                case 'Ю': return "يؤ";
                case 'Я': return "يا";
                case ',': return "،";
                default: return ch.ToString();
            }
        }

        public string Transliterate(string text)
        {
            var result = string.Empty;
            var upper = text.ToUpper();
            for (int i = 0; i < upper.Length; i++)
            {
                var arabic = TR(text[i], upper[i]);
                if (arabic != null)
                result += arabic;
            }
            return result;
        }
    }
}
