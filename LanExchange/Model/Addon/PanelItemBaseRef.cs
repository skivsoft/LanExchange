using System.Collections.Generic;
using System.Xml.Serialization;

namespace LanExchange.Model.Addon
{
    public class PanelItemBaseRef : ObjectId
    {
        private readonly List<ToolStripMenuItem> m_Items;

        public PanelItemBaseRef()
        {
            m_Items = new List<ToolStripMenuItem>();
        }

        public List<ToolStripMenuItem> ContextMenuStrip
        {
            get { return m_Items; }
        }
    }
}
