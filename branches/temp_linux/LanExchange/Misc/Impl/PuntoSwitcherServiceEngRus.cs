using System;
using System.ComponentModel;
using System.Globalization;
using LanExchange.Intf;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public class PuntoSwitcherServiceEngRus : IPuntoSwitcherService
    {
        [Localizable(false)] 
        private readonly string[] m_ABC = 
        {"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", 
         "F<DULT~:PBQRKVYJGHCNEA{WXIO}SM\">Zf,dult`;pbqrkvyjghcnea[wxio]sm'.z"};

        public bool IsValidChar(char ch)
        {
            return m_ABC[0].Contains(ch.ToString(CultureInfo.InvariantCulture)) || m_ABC[1].Contains(ch.ToString(CultureInfo.InvariantCulture));
        }

        public string Change(string str)
        {
            var result = string.Empty;
            foreach (char ch in str)
            {
                int index = m_ABC[1].IndexOf(ch);
                if (index != -1)
                    result += m_ABC[0][index];
                else
                {
                    index = m_ABC[0].IndexOf(ch);
                    if (index != -1)
                        result += m_ABC[1][index];
                    else
                        result += ch;
                }
            }
            return result;
        }

        /// <summary>
        /// Regular function Contains but with special letter 'Ё' processing.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="what"></param>
        /// <returns></returns>
        public bool SpecificContains(string s, string what)
        {
            if (String.IsNullOrEmpty(what))
                return false;
            for (int i = 0; i < s.Length - what.Length + 1; i++)
            {
                bool isEqual = true;
                for (int j = 0; j < what.Length; j++)
                {
                    char ch = s[i + j];
                    if (what[j] == 'Е' || what[j] == 'Ё')
                    {
                        if (ch != 'Е' && ch != 'Ё')
                        {
                            isEqual = false;
                            break;
                        }
                    }
                    else
                        if (what[j] != ch)
                        {
                            isEqual = false;
                            break;
                        }
                }
                if (isEqual) return true;
            }
            return false;
        }
    }
}
