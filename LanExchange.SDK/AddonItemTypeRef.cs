using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LanExchange.SDK
{
    [XmlType("PanelItemTypeRef")]
    public class AddOnItemTypeRef : AddonObjectId
    {
        private readonly List<AddonMenuItem> m_Items;

        public AddOnItemTypeRef()
        {
            m_Items = new List<AddonMenuItem>();
        }

        public List<AddonMenuItem> ContextMenuStrip
        {
            get { return m_Items; }
        }

        public int CountVisible
        {
            get
            {
                return m_Items.Count(item => item.Visible);
            }
        }
    }
}
