using System;
using System.ComponentModel;
using System.Globalization;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Implementation
{
    internal sealed class PuntoSwitcherServiceEngRus : IPuntoSwitcherService
    {
        [Localizable(false)] 
        private readonly string[] abc = 
        {"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", 
         "F<DULT~:PBQRKVYJGHCNEA{WXIO}SM\">Zf,dult`;pbqrkvyjghcnea[wxio]sm'.z"};

        public bool IsValidChar(char ch)
        {
            return abc[0].Contains(ch.ToString(CultureInfo.InvariantCulture)) || abc[1].Contains(ch.ToString(CultureInfo.InvariantCulture));
        }

        public string Change(string str)
        {
            var result = string.Empty;
            foreach (char ch in str)
            {
                int index = abc[1].IndexOf(ch);
                if (index != -1)
                    result += abc[0][index];
                else
                {
                    index = abc[0].IndexOf(ch);
                    if (index != -1)
                        result += abc[1][index];
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
