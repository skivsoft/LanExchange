using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Intf;
using LanExchange.UI;
using LanExchange.Utils;

namespace LanExchange.Misc.Addon
{
    [Localizable(false)]
    [XmlType("Program")]
    public class AddonProgram : AddonObjectId
    {
        private Image m_Image;

        public AddonProgram()
        {
            
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
            var cmdLine = fileName;
            if (!Kernel32.Is64BitOperatingSystem())
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
    }
}
