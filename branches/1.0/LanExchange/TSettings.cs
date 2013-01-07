using System;
using System.Collections.Generic;
using Microsoft.Win32;
using OSTools;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Security.AccessControl;

namespace LanExchange
{
    public class TSettings
    {

        public static bool IsAutorun
        {
            get
            {
                string[] Params = Environment.GetCommandLineArgs();
                return Autorun_Exists(Params[0]);
            }
            set
            {
                string[] Params = Environment.GetCommandLineArgs();
                if (value)
                    Autorun_Add(Params[0]);
                else
                    Autorun_Delete(Params[0]);
            }
        }

        public static bool IsRunMinimized
        {
            get { return GetBoolValue("RunMinimized", true); }
            set { SetBoolValue("RunMinimized", value); }
        }

        public static bool IsAdvancedMode
        {
            get { return GetBoolValue("AdvancedMode", true); }
            set { SetBoolValue("AdvancedMode", value); }
        }

        public static int RefreshTimeInSec
        {
            get { return GetIntValue("RefreshTimeInSec", 5 * 60); }
            set { SetIntValue("RefreshTimeInSec", value); }
        }

        public static Rectangle MainFormRect
        {
            get
            {
                Rectangle Rect = new Rectangle();
                Rect.X = GetIntValue("MainForm.Location.X", 0);
                Rect.Y = GetIntValue("MainForm.Location.Y", 0);
                Rect.Width = GetIntValue("MainForm.Size.Width", 0);
                Rect.Height = GetIntValue("MainForm.Size.Height", 0);
                return Rect;
            }
            set
            {
                Rectangle Rect = MainForm.MainFormInstance.Bounds;
                SetIntValue("MainForm.Location.X", Rect.X);
                SetIntValue("MainForm.Location.Y", Rect.Y);
                SetIntValue("MainForm.Size.Width", Rect.Width);
                SetIntValue("MainForm.Size.Height", Rect.Height);

            }
        }

        #region Сохранение в реестре и чтение из реестра
        public static string GetStrValue(string name, string defaultValue)
        {
            string RegPath = @"SOFTWARE\LanExchange\" + Path.GetDirectoryName(name);
            name = Path.GetFileName(name);
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, false);
            string Result = defaultValue;
            if (Key != null)
            {
                Result = Key.GetValue(name, defaultValue).ToString();
                Key.Close();
            }
            return Result;
        }

        public static void SetStrValue(string name, string value)
        {
            string RegPath = @"SOFTWARE\LanExchange\" + Path.GetDirectoryName(name);
            name = Path.GetFileName(name);
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, true);
            if (Key == null)
                Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegPath);
            if (Key != null)
            {
                Key.SetValue(name, value);
                Key.Close();
            }
        }

        public static int GetIntValue(string name, int defaultValue)
        {
            string RegPath = @"SOFTWARE\LanExchange\" + Path.GetDirectoryName(name);
            name = Path.GetFileName(name);
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, false);
            int Result = defaultValue;
            if (Key != null)
            {
                int value;
                if (Int32.TryParse(Key.GetValue(name, defaultValue).ToString(), out value))
                    Result = value;
                Key.Close();
            }
            return Result;
        }

        public static void SetIntValue(string name, int value)
        {
            string RegPath = @"SOFTWARE\LanExchange\" + Path.GetDirectoryName(name);
            name = Path.GetFileName(name);
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegPath, true);
            if (Key == null)
                Key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegPath);
            if (Key != null)
            {
                Key.SetValue(name, value);
                Key.Close();
            }
        }

        private static bool GetBoolValue(string name, bool defaultValue)
        {
            return GetIntValue(name, defaultValue ? 1 : 0) != 0;
        }

        private static void SetBoolValue(string name, bool value)
        {
            SetIntValue(name, value ? 1 : 0);
        }

        public static List<string> GetListValue(string name)
        {
            List<string> Result = new List<string>();
            string S = GetStrValue(name, "");
            if (!String.IsNullOrEmpty(S))
            {
                string[] A = S.Split(',');
                foreach (string str in A)
                    Result.Add(str);
            }
            return Result;
        }

        public static void SetListValue(string name, List<string> value)
        {
            string S = "";
            if (value != null)
                foreach (string str in value)
                {
                    if (S.Length != 0) S += ",";
                    S += str;
                }
            SetStrValue(name, S);
        }

        #endregion

        #region Чтение и запись для ветки автозапуска
        private static bool Autorun_Exists(string FileName)
        {
            string ExeName = System.IO.Path.GetFileName(FileName).ToUpper();
            Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", false);
            bool Result = false;
            foreach (string str in Key.GetValueNames())
            {
                RegistryValueKind Kind = Key.GetValueKind(str);
                if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                {
                    string FName;
                    string Params;
                    PathUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                    string value = System.IO.Path.GetFileName(FName).ToUpper();
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

        private static void Autorun_Add(string FileName)
        {
            try
            {
                string ExeName = System.IO.Path.GetFileName(FileName).ToUpper();
                string FileNameQuoted = String.Format("\"{0}\"", FileName);
                Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\", true);

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
                        PathUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        string value = System.IO.Path.GetFileName(FName).ToUpper();
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

        private static void Autorun_Delete(string FileName)
        {
            try
            {
                string ExeName = System.IO.Path.GetFileName(FileName).ToUpper();
                Microsoft.Win32.RegistryKey Key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                foreach (string str in Key.GetValueNames())
                {
                    RegistryValueKind Kind = Key.GetValueKind(str);
                    if (Kind == RegistryValueKind.String || Kind == RegistryValueKind.ExpandString)
                    {
                        string FName;
                        string Params;
                        PathUtils.ExplodeCmd(Key.GetValue(str).ToString(), out FName, out Params);
                        string value = System.IO.Path.GetFileName(FName).ToUpper();
                        if (ExeName.Equals(value))
                            Key.DeleteValue(str, false);
                    }
                }
                Key.Close();
            }
            catch { }
        }
        #endregion
    }
}
