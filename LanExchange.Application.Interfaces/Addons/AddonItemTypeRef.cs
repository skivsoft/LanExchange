using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace LanExchange.Application.Interfaces.Addons
{
    [XmlType("ItemTypeRef")]
    public class AddOnItemTypeRef : AddonObjectId
    {
        private readonly List<AddonMenuItem> items;

        public AddOnItemTypeRef()
        {
            items = new List<AddonMenuItem>();
        }

        public List<AddonMenuItem> ContextMenu
        {
            get { return items; }
        }

        public int CountVisible
        {
            get
            {
                return items.Count(item => item.Visible);
            }
        }
    }
}
