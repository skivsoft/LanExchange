using System;
using LanExchange.SDK;

namespace LanExchange.Misc.Action
{
    public sealed class ShortcutPanelItem : PanelItemBase
    {
        public static void RegisterColumns(IPanelColumnManager columnManager)
        {
            columnManager.RegisterColumn(typeof(ShortcutPanelItem), new PanelColumnHeader("Shortcut keys"));
            columnManager.RegisterColumn(typeof(ShortcutPanelItem), new PanelColumnHeader("Action") { Width = 300});
            columnManager.RegisterColumn(typeof(ShortcutPanelItem), new PanelColumnHeader("Context"));
        }

        public ShortcutPanelItem(PanelItemBase parent, string name, string action)
        {
            Name = name;
            Action = action;
        }

        public override string Name { get; set; }

        public string Action { get; set; }

        public string Context { get; set; }

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