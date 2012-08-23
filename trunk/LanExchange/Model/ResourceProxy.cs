using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.Drawing;
using LanExchange.SDK.SDKModel;
using System.IO;

using Gajatko.IniFiles.Light;

namespace LanExchange.Model
{
    public class ResourceProxy : Proxy, IResourceProxy, IProxy
    {
        public new const string NAME = "ResourceProxy";
        private string m_CurrentLanguage = "";
        private IniFileLight m_LanguageFile;
        //private AppDomain m_PluginsDomain;

        public ResourceProxy()
            : base(NAME)
        {

        }

        public Image GetImage(string name)
        {
            Image Result = null;
            string FileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources"), "images"), name + ".png");
            try
            {
                if (File.Exists(FileName))
                    Result = Image.FromFile(FileName);
            }
            catch (Exception)
            {
            }
            return Result;
        }

        public string CurrentLanguage
        {
            get { return m_CurrentLanguage; }
            set { 
                m_CurrentLanguage = value;
                LoadLanguage();
            }
        }

        void LoadLanguage()
        {
            string FileName = Path.Combine(Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "resources"), "langs"), m_CurrentLanguage + ".lng");
            m_LanguageFile = null;
            m_LanguageFile = new IniFileLight(FileName);
        }

        public string GetText(string name)
        {
            string Result = String.Empty;
            if (m_LanguageFile.Sections.ContainsKey("MSG"))
                if (m_LanguageFile.Sections["MSG"].ContainsKey(name))
                    Result = m_LanguageFile.Sections["MSG"][name];
            return Result;
        }
    }
}
