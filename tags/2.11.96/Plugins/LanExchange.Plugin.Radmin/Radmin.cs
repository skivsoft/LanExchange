using System;
using System.Collections.Generic;
using System.IO;
using LanExchange.SDK;
using System.Drawing;

namespace LanExchange.Plugin.Radmin
{
    internal sealed class Radmin : IPlugin
    {
        private const string ImagesFolder = "images";
        private const string ImagesMask = "*.png";
        private const string ImageEnd16 = "_16.png";
        private const string ImageEnd32 = "_32.png";

        private IServiceProvider m_Provider;
        private IImageManager m_ImageManager;

        private IDictionary<string, int> m_Names;
        private IDictionary<string, string> m_Names16;
        private IDictionary<string, string> m_Names32;

        public void Initialize(IServiceProvider serviceProvider)
        {
            m_Provider = serviceProvider;
            if (m_Provider == null) return;

            m_ImageManager = m_Provider.GetService(typeof (IImageManager)) as IImageManager;
            if (m_ImageManager == null) return;

            m_Names = new Dictionary<string, int>();
            m_Names16 = new Dictionary<string, string>();
            m_Names32 = new Dictionary<string, string>();

            ScanFolderForImages();
            RegisterFoundImages();
        }

        private void ScanFolderForImages()
        {
            //var args = Environment.GetCommandLineArgs();
            //var folder = Path.GetDirectoryName(args[0]);
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            folder = Path.Combine(folder, ImagesFolder);
            var files = Directory.GetFiles(folder, ImagesMask, SearchOption.TopDirectoryOnly);
            foreach (var fname in files)
            {
                if (fname.EndsWith(ImageEnd16, StringComparison.Ordinal))
                {
                    var s = Path.GetFileName(fname.Substring(0, fname.Length - ImageEnd16.Length));
                    if (!String.IsNullOrEmpty(s))
                    {
                        int index;
                        if (!m_Names.TryGetValue(s, out index))
                            m_Names.Add(s, 0);
                        m_Names16.Add(s, fname);
                    }
                    continue;
                }
                if (fname.EndsWith(ImageEnd32, StringComparison.Ordinal))
                {
                    var s = Path.GetFileName(fname.Substring(0, fname.Length - ImageEnd32.Length));
                    if (!String.IsNullOrEmpty(s))
                    {
                        int index;
                        if (!m_Names.TryGetValue(s, out index))
                            m_Names.Add(s, 0);
                        m_Names32.Add(s, fname);
                    }
                }
            }
        }

        private void RegisterFoundImages()
        {
            foreach (var pair in m_Names)
            {
                Image image1 = null;
                Image image2 = null;
                string fname;
                if (m_Names16.TryGetValue(pair.Key, out fname))
                    image1 = Image.FromFile(fname);
                if (m_Names32.TryGetValue(pair.Key, out fname))
                    image2 = Image.FromFile(fname);
                m_ImageManager.RegisterImage(pair.Key, image1, image2);
            }
        }
    }
}
