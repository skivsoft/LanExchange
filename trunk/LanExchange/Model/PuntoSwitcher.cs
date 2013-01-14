using System;
using System.Globalization;

namespace LanExchange.Model
{
    static class PuntoSwitcher
    {
        private static readonly string[] ABC = 
        {"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", 
         "F<DULT~:PBQRKVYJGHCNEA{WXIO}SM\">Zf,dult`;pbqrkvyjghcnea[wxio]sm'.z"};

        public static bool IsValidChar(char ch)
        {
            return ABC[0].Contains(ch.ToString(CultureInfo.InvariantCulture)) || ABC[1].Contains(ch.ToString(CultureInfo.InvariantCulture));
        }

        public static string Change(string str)
        {
            string Result = "";
            foreach (char Ch in str)
            {
                int index = ABC[1].IndexOf(Ch);
                if (index != -1)
                    Result += ABC[0][index];
                else
                {
                    index = ABC[0].IndexOf(Ch);
                    if (index != -1)
                        Result += ABC[1][index];
                    else
                        Result += Ch;
                }
            }
            return Result;
        }

        public static bool RussianContains(string s, string what)
        {
            if (String.IsNullOrEmpty(what))
                return false;
            for (int i = 0; i < s.Length - what.Length + 1; i++)
            {
                bool IsEqual = true;
                for (int j = 0; j < what.Length; j++)
                {
                    char Ch = s[i + j];
                    if (what[j] == 'Е')
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
