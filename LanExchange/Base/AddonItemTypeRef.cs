using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LanExchange.Base
{
    [XmlType("ItemTypeRef")]
    public class AddOnItemTypeRef : AddonObjectId
    {
        private readonly List<AddonMenuItem> m_Items;

        public AddOnItemTypeRef()
        {
            m_Items = new List<AddonMenuItem>();
        }

        public List<AddonMenuItem> ContextMenu
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
