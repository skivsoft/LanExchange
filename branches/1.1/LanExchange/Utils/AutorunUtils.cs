using System;
using System.IO;
using Microsoft.Win32;

namespace LanExchange
{
    public class AutorunUtils
    {
        public static void ExplodeCmd(string CmdLine, out string FName, out string Params)
        {
            FName = "";
            Params = "";
            bool bQuote = false;
            bool bParam = false;
            for (int i = 0; i < CmdLine.Length; i++)
            {
                if (!bParam && CmdLine[i] == '"')
                {
                    bQuote = !bQuote;
                    continue;
                }
                if (!bParam && !bQuote && CmdLine[i] == ' ')
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
            bool Result = false;
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
            Key.Close();
            return Result;
        }

        public static void Autorun_Add(string FileName)
        {
            try
            {
                string ExeName = Path.GetFileName(FileName).ToUpper();
                string FileNameQuoted = String.Format("\"{0}\"", FileName);
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);

                /*
                string user = Environment.UserDomainName + "\\" + Environment.UserName;
                RegistrySecurity mSec = new RegistrySecurity();
                RegistryAccessRule rule = new RegistryAccessRule(user,
                    RegistryRights.ReadKey | RegistryRights.WriteKey | RegistryRights.Delete,
                   InheritanceFlags.ContainerInherit, PropagationFlags.None, AccessControlType.Allow);
                mSec.AddAccessRule(rule);
                Key.SetAccessControl(mSec);
                 */

                bool bFound = false;
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
                            if (!bFound)
                            {
                                bFound = true;
                                Key.SetValue(str, FileNameQuoted);
                            }
                            else
                                Key.DeleteValue(str, false);
                        }
                    }
                }
                if (!bFound)
                    Key.SetValue("LanExchange", FileNameQuoted);
                Key.Close();
            }
            catch { }
        }

        public static void Autorun_Delete(string FileName)
        {
            try
            {
                string ExeName = Path.GetFileName(FileName).ToUpper();
                RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
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
                            Key.DeleteValue(str, false);
                    }
                }
                Key.Close();
            }
            catch { }
        }
    }
}
