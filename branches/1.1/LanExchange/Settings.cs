using System;
using System.Collections.Generic;
using Microsoft.Win32;
using OSTools;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Security.AccessControl;
using LanExchange.Forms;
using LanExchange.Gajatko.IniFiles;

namespace LanExchange
{
    public class Settings
    {
        private const string SECTION_GLOBAL = "Global";
        private static Settings m_Instance = null;
        private static IniFile m_INI = null;

        public Settings()
        {
            string FileName = Path.ChangeExtension(GetExecutableFileName(), ".ini");
            IniFileSettings.CaseSensitive = false;
            m_INI = IniFile.FromFile(FileName);
            m_INI.Header = "LanExchange configuration file";
        }

        public static Settings GetInstance()
        {
            if (object.ReferenceEquals(m_Instance, null))
            {
                Settings temp = new Settings();
                m_Instance = temp;
            }
            return m_Instance;
        }

        public void Save()
        {
            string FileName = Path.ChangeExtension(GetExecutableFileName(), ".ini");
            m_INI.Save(FileName);
        }

        public string GetExecutableFileName()
        {
            string[] Params = Environment.GetCommandLineArgs();
            return Params[0];
        }

        public bool IsAutorun
        {
            get
            {
                return Autorun_Exists(GetExecutableFileName());
            }
            set
            {
                string ExeFName = GetExecutableFileName();
                if (value)
                    Autorun_Add(ExeFName);
                else
                    Autorun_Delete(ExeFName);
            }
        }

        public bool IsRunMinimized
        {
            get { return GetBoolValue("RunMinimized", true); }
            set { SetBoolValue("RunMinimized", value); }
        }

        public bool IsAdvancedMode
        {
            get { return GetBoolValue("AdvancedMode", true); }
            set { SetBoolValue("AdvancedMode", value); }
        }

        public int RefreshTimeInSec
        {
            get { return GetIntValue("RefreshTimeInSec", 5 * 60); }
            set { SetIntValue("RefreshTimeInSec", value); }
        }

        #region Сохранение в реестре и чтение из реестра
        private void ParseSection(string name, out string section, out string newname)
        {
            int dot = name.IndexOf('\\');
            if (dot == -1)
            {
                section = SECTION_GLOBAL;
                newname = name;
            }
            else
            {
                section = name.Substring(0, dot);
                newname = name.Remove(0, dot + 1);
            }
        }

        public string GetStrValue(string name, string defaultValue)
        {
            string section;
            string Result;
            ParseSection(name, out section, out name);
            Result = m_INI[section][name];
            return Result == null ? defaultValue : Result;
        }

        public void SetStrValue(string name, string value)
        {
            string section;
            ParseSection(name, out section, out name);
            m_INI[section][name] = value;
        }

        public int GetIntValue(string name, int defaultValue)
        {
            int Result = defaultValue;
            int value;
            if (Int32.TryParse(GetStrValue(name, defaultValue.ToString()), out value))
                Result = value;
            return Result;
        }

        public void SetIntValue(string name, int value)
        {
            SetStrValue(name, value.ToString());
        }

        public bool GetBoolValue(string name, bool defaultValue)
        {
            return GetStrValue(name, defaultValue ? "1" : "0") != "0";
        }

        public void SetBoolValue(string name, bool value)
        {
            SetStrValue(name, value ? "1" : "0");
        }

        public List<string> GetListValue(string name)
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

        public void SetListValue(string name, List<string> value)
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
        private bool Autorun_Exists(string FileName)
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

        private void Autorun_Add(string FileName)
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

        private void Autorun_Delete(string FileName)
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
