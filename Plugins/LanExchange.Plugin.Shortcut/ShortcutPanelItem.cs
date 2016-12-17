using System;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Shortcut
{
    public sealed class ShortcutPanelItem : PanelItemBase
    {
        private readonly string action;
        private readonly string context;
        private readonly string customImageName;

        public ShortcutPanelItem(PanelItemBase parent, string name, string action, string context, string customImageName) : base(parent)
        {
            Name = name;
            this.action = action;
            this.context = context;
            this.customImageName = customImageName;
        }

        public ShortcutPanelItem(PanelItemBase parent, string name, string action) : 
            this(parent, name, action, string.Empty, string.Empty)
        {
        }

        public override string Name { get; set; }

        public override string ImageName
        {
            get { return string.IsNullOrEmpty(customImageName) ? PanelImageNames.SHORTCUT : customImageName; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public override object Clone()
        {
            var result = new ShortcutPanelItem(Parent, Name, action, context, customImageName);
            return result;
        }

        public override int CountColumns
        {
            get { return base.CountColumns + 2; }
        }

        public override IComparable GetValue(int index)
        {
            switch (index)
            {
                case 1:
                    return action;
                case 2:
                    return context;
                default:
                    return base.GetValue(index);
            }
        }
    }
}