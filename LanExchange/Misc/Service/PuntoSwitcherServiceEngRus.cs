using System;
using System.ComponentModel;
using System.Globalization;

namespace LanExchange.Misc.Service
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
            foreach (char Ch in str)
            {
                int index = m_ABC[1].IndexOf(Ch);
                if (index != -1)
                    result += m_ABC[0][index];
                else
                {
                    index = m_ABC[0].IndexOf(Ch);
                    if (index != -1)
                        result += m_ABC[1][index];
                    else
                        result += Ch;
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
        public bool RussianContains(string s, string what)
        {
            if (String.IsNullOrEmpty(what))
                return false;
            for (int i = 0; i < s.Length - what.Length + 1; i++)
            {
                bool IsEqual = true;
                for (int j = 0; j < what.Length; j++)
                {
                    char Ch = s[i + j];
                    if (what[j] == 'Е' || what[j] == 'Ё')
                    {
                        if (Ch != 'Е' && Ch != 'Ё')
                        {
                            IsEqual = false;
                            break;
                        }
                    }
                    else
                        if (what[j] != Ch)
                        {
                            IsEqual = false;
                            break;
                        }
                }
                if (IsEqual) return true;
            }
            return false;
        }
    }
}
