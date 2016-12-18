using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Presentation.WinForms.Controls
{
    /// TODO need refactoring
    internal sealed class PanelModelCopyHelper : IPanelModelCopyHelper
    {
        private readonly IPanelModel model;
        private readonly IPanelColumnManager panelColumns;

        private readonly List<int> indexes;
        private PanelItemBase currentItem;
        private IList<PanelColumnHeader> columns;

        public PanelModelCopyHelper(
            IPanelModel model,
            IPanelColumnManager panelColumns)
        {
            if (panelColumns == null) throw new ArgumentNullException(nameof(panelColumns));

            this.model = model;
            this.panelColumns = panelColumns;

            indexes = new List<int>();
        }

        public IPanelModel Model
        {
            get { return model; }
        }

        public IList<int> Indexes
        {
            get { return indexes; }
        }

        public int IndexesCount
        {
            get { return indexes.Count; }
        }

        public IEnumerable<PanelColumnHeader> Columns
        {
            get { return columns; }
        }

        public int ColumnsCount
        {
            get { return columns.Count; }
        }

        public PanelItemBase CurrentItem
        {
            get
            {
                return currentItem;
            }

            set
            {
                currentItem = value;
                if (currentItem is PanelItemDoubleDot)
                    currentItem = currentItem.Parent;
                columns = panelColumns.GetColumns(currentItem.GetType().Name).ToList();
            }
        }

        public void Prepare()
        {
            indexes.Sort();
            if (indexes.Count > 1)
                if (indexes[0] == 0 && model.GetItemAt(0) is PanelItemDoubleDot)
                    indexes.Remove(0);
        }

        public void MoveTo(int index)
        {
            CurrentItem = model.GetItemAt(indexes[index]);
        }

        public string GetColumnValue(int colIndex)
        {
            if (colIndex == -1)
                return currentItem != null ? currentItem.FullName : string.Empty;
            IComparable comparable;
            var column = columns[colIndex];
            if (column.Callback != null)
                column.LazyDict.TryGetValue(currentItem, out comparable);
            else
                comparable = currentItem[colIndex];
            return comparable != null ? comparable.ToString() : string.Empty;
        }

        [Localizable(false)]
        public string GetSelectedText()
        {
            var sb = new StringBuilder();
            for (int index = 0; index < indexes.Count; index++)
            {
                MoveTo(index);
                if (index > 0) sb.AppendLine();
                var first = true;
                foreach (var column in columns)
                    if (column.Visible)
                    {
                        if (!first) sb.Append("\t");
                        sb.Append(GetColumnValue(column.Index));
                        first = false;
                    }
            }

            return sb.ToString();
        }

        public string GetColumnText(int colIndex)
        {
            var sb = new StringBuilder();
            for (int index = 0; index < indexes.Count; index++)
            {
                MoveTo(index);
                if (index > 0) sb.AppendLine();
                sb.Append(GetColumnValue(colIndex));
            }

            return sb.ToString();
        }
    
        public PanelItemBaseHolder GetItems()
        {
            var result = new PanelItemBaseHolder(model.TabName, model.DataType);
            for (int index = 0; index < indexes.Count; index++)
            {
                MoveTo(index);
                result.Add(CurrentItem);
            }

            return result;
        }
    }
}