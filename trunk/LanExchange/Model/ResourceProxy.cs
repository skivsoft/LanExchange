using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using LanExchange.Model.Gajatko.IniFiles.Light;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;

namespace LanExchange.Model
{
    public class ResourceProxy : Proxy, IProxy
    {
        public new const string NAME = "ResourceProxy";
        private string m_CurrentLanguage = "";
        private Dictionary<string, Image> m_Images;
        private IniFileLight m_LanguageFile = null;
        //private AppDomain m_PluginsDomain;

        public ResourceProxy()
            : base(NAME)
        {
            m_Images = new Dictionary<string, Image>();
        }

        public override void OnRegister()
        {
            base.OnRegister();
            LoadImages();
        }

        void LoadImages()
        {
            string path = Path.Combine(Path.Combine(AppFacade.ExecutablePath, "resources"), "images");
            string[] files = Directory.GetFiles(path, "*.png", SearchOption.TopDirectoryOnly);
            Image img;
            foreach (string file in files)
            {
                try
                {
                    img = Image.FromFile(file);
                    string name = Path.GetFileName(file);
                    name = name.Substring(0, name.Length - 4);
                    m_Images.Add(name, img);
                }
                catch(Exception)
                {
                }
            }
        }

        void LoadLanguage()
        {
            string FileName = Path.Combine(Path.Combine(Path.Combine(AppFacade.ExecutablePath, "resources"), "langs"), m_CurrentLanguage + ".lng");
            if (File.Exists(FileName))
            {
                m_LanguageFile = null;
                m_LanguageFile = new IniFileLight(FileName);
            }
        }

        public string CurrentLanguage
        {
            get { return m_CurrentLanguage; }
            set
            {
                m_CurrentLanguage = value;
                LoadLanguage();
            }
        }

        public Image GetImage(string name)
        {
            if (m_Images.ContainsKey(name))
                return m_Images[name];
            else
                return null;
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
