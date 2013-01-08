using System;

namespace LanExchange.Model
{
    class PuntoSwitcher
    {
        private static string[] ABC = {"АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя", 
                                       "F<DULT~:PBQRKVYJGHCNEA{WXIO}SM\">Zf,dult`;pbqrkvyjghcnea[wxio]sm'.z"};

        public static bool IsValidChar(char Ch)
        {
            return ABC[0].Contains(Ch.ToString()) || ABC[1].Contains(Ch.ToString());
        }

        public static string Change(string str)
        {
            string Result = "";
            int index;
            foreach (char Ch in str)
            {
                index = ABC[1].IndexOf(Ch);
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

        public static bool RussianContains(string Where, string What)
        {
            if (String.IsNullOrEmpty(What))
                return false;
            for (int i = 0; i < Where.Length - What.Length + 1; i++)
            {
                bool IsEqual = true;
                for (int j = 0; j < What.Length; j++)
                {
                    char Ch = Where[i + j];
                    if (What[j] == 'Е')
                    {
                        if (Ch != 'Е' && Ch != 'Ё')
                        {
                            IsEqual = false;
                            break;
                        }
                    }
                    else
                        if (What[j] != Ch)
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
