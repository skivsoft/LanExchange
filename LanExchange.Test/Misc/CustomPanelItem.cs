using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Misc
{
    public class CustomPanelItem : PanelItemBase
    {
        private readonly IDictionary<int, IComparable> data;
        private int countColumns;

        [Localizable(false)]
        public CustomPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            data = new Dictionary<int, IComparable>();
            Name = name;
            SetCountColumns(1);
        }

        public sealed override string Name 
        {
            get { return this[0].ToString(); }
            set { this[0] = value; }
        }

        public override int CountColumns
        {
            get { return countColumns;  }
        }

        public override string ImageName
        {
            get { return string.Empty; }
        }

        public override string ImageLegendText
        {
            get { return string.Empty; }
        }

        public void SetCountColumns(int value)
        {
            countColumns = value;
        }

        public override IComparable GetValue(int index)
        {
            IComparable result;
            if (data.TryGetValue(index, out result))
                return result;
            return null;
        }

        public override object Clone()
        {
            var result = new CustomPanelItem(Parent, Name);
            result.SetCountColumns(CountColumns);
            for (int index = 0; index < CountColumns; index++)
                result.SetValue(index, GetValue(index));
            return result;
        }

        protected override void SetValue(int index, IComparable value)
        {
            data.Remove(index);
            data.Add(index, value);
        }
    }
}
