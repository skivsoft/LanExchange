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
            bool isQuote = false;
            bool isParam = false;
            for (int i = 0; i < cmdLine.Length; i++)
            {
                if (!isParam && cmdLine[i].Equals('"'))
                {
                    isQuote = !isQuote;
                    continue;
                }
                if (!isParam && !isQuote && cmdLine[i].Equals(' '))
                    isParam = true;
                else
                    if (isParam)
                        cmdParams += cmdLine[i].ToString(CultureInfo.InvariantCulture);
                    else
                        fileName += cmdLine[i].ToString(CultureInfo.InvariantCulture);
            }
        }

        public static bool Autorun_Exists(string fileName)
        {
            string exeName = Path.GetFileName(fileName);
            if (exeName == null) return false;
            exeName = exeName.ToUpper(CultureInfo.InvariantCulture);
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", false);
            if (regKey == null)
                return false;
            bool result = false;
            try
            {
                foreach (string str in regKey.GetValueNames())
                {
                    var kind = regKey.GetValueKind(str);
                    if (kind == RegistryValueKind.String || kind == RegistryValueKind.ExpandString)
                    {
                        string fName;
                        string Params;
                        ExplodeCmd(regKey.GetValue(str).ToString(), out fName, out Params);
                        var name = Path.GetFileName(fName);
                        if (name != null)
                        {
                            string value = name.ToUpper(CultureInfo.InvariantCulture);
                            if (exeName.Equals(value))
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                regKey.Close();
            }
            return result;
        }

        [Localizable(false)]
        public static void Autorun_Add(string fileName)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (regKey == null)
                return;
            try
            {
                var name = Path.GetFileName(fileName);
                if (name != null)
                {
                    string exeName = name.ToUpper(CultureInfo.InvariantCulture);
                    string fileNameQuoted = String.Format(CultureInfo.InvariantCulture, "\"{0}\"", fileName);

                    bool found = false;
                    Array.ForEach(regKey.GetValueNames(), str =>
                        {
                            var kind = regKey.GetValueKind(str);
                            if (kind == RegistryValueKind.String || kind == RegistryValueKind.ExpandString)
                            {
                                string fName;
                                string Params;
                                ExplodeCmd(regKey.GetValue(str).ToString(), out fName, out Params);
                                var s = Path.GetFileName(fName);
                                if (s != null)
                                {
                                    string value = s.ToUpper(CultureInfo.InvariantCulture);
                                    if (exeName.Equals(value))
                                        if (!found)
                                        {
                                            found = true;
                                            regKey.SetValue(str, fileNameQuoted);
                                        }
                                        else
                                            regKey.DeleteValue(str, false);
                                }
                            }
                        });
                    if (!found)
                        regKey.SetValue("LanExchange", fileNameQuoted);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
            finally
            {
                regKey.Close();
            }
        }

        public static void Autorun_Delete(string fileName)
        {
            var regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (regKey == null)
                return;
            try
            {
                var name = Path.GetFileName(fileName);
                if (name != null)
                {
                    string exeName = name.ToUpper(CultureInfo.InvariantCulture);
                    Array.ForEach(regKey.GetValueNames(), str =>
                        {
                            var kind = regKey.GetValueKind(str);
                            if (kind == RegistryValueKind.String || kind == RegistryValueKind.ExpandString)
                            {
                                string fName;
                                string Params;
                                ExplodeCmd(regKey.GetValue(str).ToString(), out fName, out Params);
                                var s = Path.GetFileName(fName);
                                if (s != null)
                                {
                                    string value = s.ToUpper(CultureInfo.InvariantCulture);
                                    if (exeName.Equals(value))
                                        regKey.DeleteValue(str, false);
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
                regKey.Close();
            }
        }
    }
}