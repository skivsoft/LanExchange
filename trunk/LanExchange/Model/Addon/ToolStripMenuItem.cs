using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    public class ToolStripMenuItem
    {
        public string Text { get; set; }

        public string ShortcutKeys  { get; set; }
        
        public ObjectId ProgramRef { get; set; }
        
        [XmlIgnore]
        public AddonProgram ProgramValue { get; set; }
        
        public string ProgramArgs { get; set; }

        public bool IsSeparator
        {
            get { return string.IsNullOrEmpty(Text); }
        }
    }
}
