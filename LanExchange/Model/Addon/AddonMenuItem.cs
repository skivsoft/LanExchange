using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    [XmlType("ToolStripMenuItem")]
    public class AddonMenuItem
    {
        public AddonMenuItem()
        {
            Visible = true;
        }
        
        [XmlAttribute]
        public string Text { get; set; }

        [XmlAttribute]
        public bool Default { get; set; }

        [XmlAttribute]
        public bool Visible { get; set; }

        public string ShortcutKeys  { get; set; }
        
        public AddonObjectId ProgramRef { get; set; }
        
        [XmlIgnore]
        public AddonProgram ProgramValue { get; set; }
        
        public string ProgramArgs { get; set; }

        public bool IsSeparator
        {
            get { return string.IsNullOrEmpty(Text); }
        }
        
        public bool ShortcutPresent
        {
            get { return !string.IsNullOrEmpty(ShortcutKeys); }
        }
    }
}
