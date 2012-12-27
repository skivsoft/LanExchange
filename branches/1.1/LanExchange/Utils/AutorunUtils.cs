using System;
using System.IO;
using Microsoft.Win32;
using NLog;

namespace LanExchange.Utils
{
    public class AutorunUtils
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        public static void ExplodeCmd(string CmdLine, out string FName, out string Params)
        {
            FName = "";
            Params = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < CmdLine.Length; i++)
            {
                if (!bParam && CmdLine[i].Equals('"'))
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && CmdLine[i].Equals(' '))
                    bParam = true;
                else
                    if (bParam)
                        Params += CmdLine[i].ToString();
                    else
                        FName += CmdLine[i].ToString();
            }
        }

        public static bool Autorun_Exists(string FileName)
        {
            string ExeName = Path.GetFileName(FileName).ToUpper();
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
                        AutorunUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        string value = Path.GetFileName(FName).ToUpper();
                        if (ExeName.Equals(value))
                        {
                            Result = true;
                            break;
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

        public static void Autorun_Add(string FileName)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);
            if (Key == null)
                return;
            try
            {
                string ExeName = Path.GetFileName(FileName).ToUpper();
                string FileNameQuoted = String.Format("\"{0}\"", FileName);

                bool bFound = false;
                Array.ForEach(Key.GetValueNames(), str =>
                {
                    RegistryValueKind Kind = Key.GetValueKind(str);
                    if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                    {
                        string FName;
                        string Params;
                        AutorunUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        string value = Path.GetFileName(FName).ToUpper();
                        if (ExeName.Equals(value))
                            if (!bFound)
                            {
                                bFound = true;
                                Key.SetValue(str, FileNameQuoted);
                            }
                            else
                                Key.DeleteValue(str, false);
                    }
                });
                if (!bFound)
                    Key.SetValue("LanExchange", FileNameQuoted);
            }
            catch (Exception e)
            {
                logger.Error("AutoRun_Add() {0}", e.Message);
            }
            finally
            {
                Key.Close();
            }
        }

        public static void Autorun_Delete(string FileName)
        {
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (Key == null)
                return;
            try
            {
                string ExeName = Path.GetFileName(FileName).ToUpper();
                Array.ForEach(Key.GetValueNames(), str =>
                {
                    RegistryValueKind Kind = Key.GetValueKind(str);
                    if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                    {
                        string FName;
                        string Params;
                        AutorunUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        string value = Path.GetFileName(FName).ToUpper();
                        if (ExeName.Equals(value))
                            Key.DeleteValue(str, false);
                    }
                });
            }
            catch (Exception e)
            {
                logger.Error("Autorun_Delete() {0}", e.Message);
            }
            finally
            {
                Key.Close();
            }
        }
    }
}
