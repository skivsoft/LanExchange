using System;
using System.Collections.Generic;
using System.ComponentModel;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class CustomPanelItem : PanelItemBase
    {
        private readonly IDictionary<int, IComparable> m_Data;
        private int m_CountColumns;

        [Localizable(false)]
        public CustomPanelItem(PanelItemBase parent, string name) : base(parent)
        {
            m_Data = new Dictionary<int, IComparable>();
            Name = name;
            SetCountColumns(1);
        }

        public override sealed string Name 
        {
            get { return this[0].ToString(); }
            set { this[0] = value; }
        }

        public override int CountColumns
        {
            get { return m_CountColumns;  }
        }

        public void SetCountColumns(int value)
        {
            m_CountColumns = value;
        }

        public override IComparable GetValue(int index)
        {
            IComparable result;
            if (m_Data.TryGetValue(index, out result))
                return result;
            return null;
        }

        protected override void SetValue(int index, IComparable value)
        {
            m_Data.Remove(index);
            m_Data.Add(index, value);
        }
    }
}
