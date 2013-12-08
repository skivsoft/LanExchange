using System;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Misc
{
    public sealed class ShortcutPanelItem : PanelItemBase
    {
        public ShortcutPanelItem()
        {
        }

        public ShortcutPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            Name = name;
        }

        public ShortcutPanelItem(PanelItemBase parent, string name, string action) : base(parent)
        {
            Name = name;
            Action = action;
        }

        [XmlAttribute]
        public override string Name { get; set; }

        [XmlIgnore]
        public string Action { get; set; }

        [XmlIgnore]
        public string Context { get; set; }

        [XmlIgnore]
        public string CustomImageName { get; set; }

        public override string ImageName
        {
            get { return string.IsNullOrEmpty(CustomImageName) ? PanelImageNames.ShortcutNormal : CustomImageName; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override object Clone()
        {
            var result = new ShortcutPanelItem(Parent, Name, Action);
            return result;
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 2; }
        }

        public override IComparable GetValue(int index)
        {
            switch(index)
            {
                case 1:
                    return Action;
                case 2:
                    return Context;
                default:
                    return base.GetValue(index);
            }
        }
    }
}