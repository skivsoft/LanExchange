using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;

namespace LanExchange.Presentation.Interfaces.Addons
{
    [Localizable(false)]
    [XmlType("Program")]
    public class AddonProgram : AddonObjectId
    {
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

        [XmlIgnore]
        public AddonProgramInfo Info { get; set; }

        [XmlIgnore]
        public string ExpandedFileName
        {
            get { return Info.ExpandedFileName; }
        }

        [XmlIgnore]
        public Image ProgramImage
        {
            get { return Info.ProgramImage; }
        }
    }
}