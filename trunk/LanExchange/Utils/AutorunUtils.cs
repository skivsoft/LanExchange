using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace LanExchange.Utils
{
    public static class AutorunUtils
    {
        public static void ExplodeCmd(string cmdLine, out string fileName, out string cmdParams)
        {
            fileName = "";
            cmdParams = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < cmdLine.Length; i++)
            {
                if (!bParam && cmdLine[i].Equals('"'))
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && cmdLine[i].Equals(' '))
                    bParam = true;
                else
                    if (bParam)
                        cmdParams += cmdLine[i].ToString(CultureInfo.InvariantCulture);
                    else
                        fileName += cmdLine[i].ToString(CultureInfo.InvariantCulture);
            }
        }

        public static bool Autorun_Exists(string fileName)
        {
            string ExeName = Path.GetFileName(fileName);
            if (ExeName == null) return false;
            ExeName = ExeName.ToUpper();
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", false);
            if (Key == null)
                return false;
            bool Result = false;
            try
            {
                foreach (string str in Key.GetValueNames())
                {
                    RegistryValueKind Kind = Key.GetValueKind(str);
                    if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                    {
                        string FName;
                        string Params;
                        ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        var name = Path.GetFileName(FName);
                        if (name != null)
                        {
                            string value = name.ToUpper();
                            if (ExeName.Equals(value))
                            {
                                Result = true;
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                Key.Close();
            }
            return Result;
        }

        [Localizable(false)]
        public static void Autorun_Add(string fileName)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (Key == null)
                return;
            try
            {
                var name = Path.GetFileName(fileName);
                if (name != null)
                {
                    string ExeName = name.ToUpper();
                    string FileNameQuoted = String.Format("\"{0}\"", fileName);

                    bool bFound = false;
                    Array.ForEach(Key.GetValueNames(), str =>
                        {
                            RegistryValueKind Kind = Key.GetValueKind(str);
                            if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                            {
                                string FName;
                                string Params;
                                ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                                var s = Path.GetFileName(FName);
                                if (s != null)
                                {
                                    string value = s.ToUpper();
                                    if (ExeName.Equals(value))
                                        if (!bFound)
                                        {
                                            bFound = true;
                                            Key.SetValue(str, FileNameQuoted);
                                        }
                                        else
                                            Key.DeleteValue(str, false);
                                }
                            }
                        });
                    if (!bFound)
                        Key.SetValue("LanExchange", FileNameQuoted);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                Key.Close();
            }
        }

        public static void Autorun_Delete(string fileName)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (Key == null)
                return;
            try
            {
                var name = Path.GetFileName(fileName);
                if (name != null)
                {
                    string ExeName = name.ToUpper();
                    Array.ForEach(Key.GetValueNames(), str =>
                        {
                            RegistryValueKind Kind = Key.GetValueKind(str);
                            if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                            {
                                string FName;
                                string Params;
                                ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                                var s = Path.GetFileName(FName);
                                if (s != null)
                                {
                                    string value = s.ToUpper();
                                    if (ExeName.Equals(value))
                                        Key.DeleteValue(str, false);
                                }
                            }
                        });
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                Key.Close();
            }
        }
    }
}
