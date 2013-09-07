using System.Collections.Generic;
using System.Xml.Serialization;
using System.Linq;

namespace LanExchange.Intf.Addon
{
    [XmlType("PanelItemTypeRef")]
    public class AddonItemTypeRef : AddonObjectId
    {
        private readonly List<AddonMenuItem> m_Items;

        public AddonItemTypeRef()
        {
            m_Items = new List<AddonMenuItem>();
        }

        public List<AddonMenuItem> ContextMenuStrip
        {
            get { return m_Items; }
        }

        public int CountVisible
        {
            get { return m_Items.Count(item => item.Visible); }
        }
    }
}
