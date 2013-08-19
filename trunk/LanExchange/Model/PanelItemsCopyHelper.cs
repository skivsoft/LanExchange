using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using LanExchange.Presenter;
using LanExchange.SDK;

namespace LanExchange.Model
{
    public class PanelItemsCopyHelper
    {
        private readonly IPanelModel m_Model;
        private readonly List<int> m_Indexes;
        private PanelItemBase m_CurrentItem;
        private IList<PanelColumnHeader> m_Columns;

        public PanelItemsCopyHelper(IPanelModel model)
        {
            m_Model = model;
            m_Indexes = new List<int>();
        }

        public IList<int> Indexes
        {
            get { return m_Indexes; }
        }

        public int Count
        {
            get { return m_Indexes.Count; }
        }

        public IEnumerable<PanelColumnHeader> Columns
        {
            get { return m_Columns; }
        }

        public PanelItemBase CurrentItem
        {
            get { return m_CurrentItem; }
            set
            {
                m_CurrentItem = value;
                if (m_CurrentItem is PanelItemDoubleDot)
                    m_CurrentItem = m_CurrentItem.Parent;
                m_Columns = AppPresenter.PanelColumns.GetColumns(m_CurrentItem.GetType());
            }
        }

        public void Prepare()
        {
            m_Indexes.Sort();
            if (m_Indexes.Count > 1)
            {
                if (m_Indexes[0] == 0 && m_Model.GetItemAt(0) is PanelItemDoubleDot)
                    m_Indexes.Remove(0);
            }
        }

        public void MoveTo(int index)
        {
            CurrentItem = m_Model.GetItemAt(m_Indexes[index]);
        }

        public string GetColumnValue(int colIndex)
        {
            if (colIndex == -1)
                return m_CurrentItem != null ? m_CurrentItem.FullItemName : string.Empty;
            IComparable comparable;
            var column = m_Columns[colIndex];
            if (column.Callback != null)
                column.LazyDict.TryGetValue(m_CurrentItem, out comparable);
            else
                comparable = m_CurrentItem[colIndex];
            return comparable != null ? comparable.ToString() : string.Empty;
        }

        [Localizable(false)]
        public string GetSelectedText()
        {
            var sb = new StringBuilder();
            for (int index = 0; index < Count; index++)
            {
                MoveTo(index);
                if (index > 0) sb.AppendLine();
                var bFirst = true;
                foreach(var column in m_Columns)
                    if (column.Visible)
                    {
                        if (!bFirst) sb.Append("\t");
                        sb.Append(GetColumnValue(column.Index));
                        bFirst = false;
                    }
            }
            return sb.ToString();
        }

        public string GetColumnText(int colIndex)
        {
            var sb = new StringBuilder();
            for (int index = 0; index < Count; index++)
            {
                MoveTo(index);
                if (index > 0) sb.AppendLine();
                sb.Append(GetColumnValue(colIndex));
            }
            return sb.ToString();
        }

    }
}