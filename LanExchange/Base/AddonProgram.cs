using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Helpers;

namespace LanExchange.Base
{
    [Localizable(false)]
    [XmlType("Program")]
    public class AddonProgram : AddonObjectId, IDisposable
    {
        private Image m_Image;

        public AddonProgram()
        {
        }

        public void Dispose()
        {
            if (m_Image != null)
                m_Image.Dispose();
        }

        public AddonProgram(string id, string fileName)
        {
            Id = id;
            FileName = fileName;
        }

        [XmlAttribute]
        public string FileName { get; set; }

        public bool Exists
        {
            get { return File.Exists(ExpandedFileName); } 
        }

        public Image ProgramImage
        {
            get { return m_Image; }
        }

        [XmlIgnore]
        public string ExpandedFileName { get; set; }

        public static string ExpandCmdLine(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;

            var cmdLine = fileName;
            if (!EnvironmentHelper.Is64BitOperatingSystem())
                cmdLine = cmdLine.Replace("%ProgramFiles(x86)%", "%ProgramFiles%");
            return Environment.ExpandEnvironmentVariables(cmdLine);
        }

        public void PrepareFileNameAndIcon()
        {
            var fileName = ExpandCmdLine(FileName);
            if (fileName.Equals(Path.GetFileName(fileName)))
                fileName = Path.Combine(App.FolderManager.CurrentPath, fileName);
            ExpandedFileName = fileName;
            m_Image = App.Images.GetSmallImageOfFileName(fileName);
        }

        public static AddonProgram CreateFromProtocol(string protocol)
        {
            string fileName;
            int iconIndex;
            if (!ProtocolHelper.LookupInRegistry(protocol, out fileName, out iconIndex))
                return null;
            var result = new AddonProgram(protocol, fileName);
            result.PrepareFileNameAndIcon();
            return result;
        }
    }
}