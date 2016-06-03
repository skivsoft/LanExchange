using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace LanExchange.Application.Interfaces.Addons
{
    [XmlType("LanExchangeAddon")]
    public class AddOn
    {
        private readonly Collection<AddonProgram> programs;
        private readonly Collection<AddOnItemTypeRef> itemTypes;

        public AddOn()
        {
            itemTypes = new Collection<AddOnItemTypeRef>();
            programs = new Collection<AddonProgram>();
        }

        public Collection<AddonProgram> Programs
        {
            get { return programs; }
        }

        public Collection<AddOnItemTypeRef> ItemTypes
        {
            get { return itemTypes; }
        }
    }
}
