using LanExchange.Helpers;
using LanExchange.Model.Comparers;
using LanExchange.SDK;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.Domain;
using LanExchange.Infrastructure;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Model
{
    [XmlType("Tab")]
    public class PanelModel : IPanelModel
    {
        private readonly IPanelFillerManager panelFillers;
        private readonly IPanelColumnManager panelColumns;

        // items added by user
        private readonly IList<PanelItemBase> items;
        // merged all results and user items
        private readonly List<PanelItemBase> data;
        // keys for filtering
        private readonly IList<PanelItemBase> keys;
        // current path for item list
        private readonly IObjectPath<PanelItemBase> currentPath;
        // column sorter
        private readonly IColumnComparer comparer;
        // punto switcher service
        private readonly IPuntoSwitcherService puntoService;
        // panel updater
        private readonly IPanelUpdater panelUpdater;

        public event EventHandler Changed;
        public event EventHandler TabNameUpdated;
        
        /// <summary>
        /// Parameterless constructor for xml serialization.
        /// </summary>
        public PanelModel(
            IPanelFillerManager panelFillers,
            IPanelColumnManager panelColumns,
            IPuntoSwitcherService puntoService,
            IPanelUpdater panelUpdater)
        {
            Contract.Requires<ArgumentNullException>(panelFillers != null);
            Contract.Requires<ArgumentNullException>(panelColumns != null);
            Contract.Requires<ArgumentNullException>(puntoService != null);
            Contract.Requires<ArgumentNullException>(panelUpdater != null);

            this.panelFillers = panelFillers;
            this.panelColumns = panelColumns;
            this.puntoService = puntoService;
            this.panelUpdater = panelUpdater;

            items = new List<PanelItemBase>();
            data = new List<PanelItemBase>();
            keys = new List<PanelItemBase>();
            currentPath = new ObjectPath<PanelItemBase>();
            comparer = new ColumnComparer(0, PanelSortOrder.Ascending);
            CurrentView = PanelViewMode.Details;
        }

        public void Dispose()
        {
            panelUpdater.Dispose();
        }

        /// <summary>
        /// Gets or sets the name of the tab.
        /// </summary>
        /// <value>
        /// The name of the tab.
        /// </value>
        [Localizable(false)]
        //[XmlAttribute("Name")]
        [XmlIgnore]
        public string TabName
        {
            get
            {
                var parent = currentPath.Peek();
                return parent.Single().Name;
            }
        }

        public void OnTabNameUpdated()
        {
            if (TabNameUpdated != null)
                TabNameUpdated(this, EventArgs.Empty);
        }

        public string ImageName
        {
            get 
            { 
                var parent = currentPath.Peek();
                return parent.Single().ImageName;
            }
        }

        public void AsyncRetrieveData(bool clearFilter)
        {
            panelUpdater.Stop();
            panelUpdater.Start(this, clearFilter);
        }

        [XmlAttribute]
        public string DataType { get; set; }

        [Localizable(false)]
        [XmlAttribute("View")]
        public PanelViewMode CurrentView { get; set; }

        [Localizable(false)]
        [XmlAttribute("Filter")]
        public string FilterText { get; set; }

        [XmlElement("Path")]
        public IObjectPath<PanelItemBase> CurrentPath
        {
            get { return currentPath; }
            set
            {
                currentPath.Clear();
                foreach (var item in value)
                    currentPath.Push(item);
            }
        }

        [XmlElement("Focused")]
        public PanelItemBase FocusedItem { get; set; }

        public IList<PanelItemBase> Items
        {
            get { return items; }
        }

        public PanelItemBase GetItemAt(int index)
        {
            return keys[index];
        }

        public int IndexOf(PanelItemBase key)
        {
            return keys.IndexOf(key);
        }

        private bool GoodForFilter(string[] strList, string filter1, string filter2)
        {
            for (int i = 0; i < strList.Length; i++)
            {
                if (i == 0)
                {
                    if (puntoService.SpecificContains(strList[i], filter1) ||
                        puntoService.SpecificContains(strList[i], filter2))
                        return true;
                } else
                if (filter1 != null && strList[i].Contains(filter1) || filter2 != null && strList[i].Contains(filter2))
                    return true;
            }
            return false;
        }

        public IColumnComparer Comparer
        {
            get { return comparer; }
        }

        public void Sort(IComparer<PanelItemBase> sorter)
        {
            data.Sort(sorter);
            ApplyFilter();
            OnChanged();
        }

        
        /// <summary>
        /// IFilterModel.AppliFilter()
        /// </summary>
        public void ApplyFilter()
        {
            if (FilterText == null) 
                FilterText = string.Empty;
            var filtered = FilterText != string.Empty;
            keys.Clear();
            var filter1 = FilterText.ToUpper(CultureInfo.CurrentCulture);
            var filter2 = puntoService.Change(FilterText);
            if (filter2 != null) filter2 = filter2.ToUpper(CultureInfo.CurrentCulture);
            var helper = new PanelModelCopyHelper(this, panelColumns);
            var upperValues = new List<string>();
            foreach (var value in data)
            {
                if (value is PanelItemDoubleDot)
                {
                    keys.Add(value);
                    continue;
                }
                helper.CurrentItem = value;
                upperValues.Clear();
                if (helper.Columns != null)
                    for (int i = 0; i < helper.ColumnsCount; i++)
                    {
                        var column = helper.GetColumnValue(i);
                        if (!string.IsNullOrEmpty(column))
                            upperValues.Add(column.ToUpper(CultureInfo.CurrentCulture));
                    }
                if (!filtered || GoodForFilter(upperValues.ToArray(), filter1, filter2))
                    keys.Add(value);
            }
        }

        public int Count
        {
            get { return data.Count; }
        }

        public int FilterCount
        {
            get { return keys.Count; }
        }

        public bool HasBackItem
        {
            get
            {
                if (data.Count > 0)
                    if (data[0] is PanelItemDoubleDot)
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Sync retrieving panel items using appropriate filler strategy.
        /// </summary>
        public PanelFillerResult RetrieveData(RetrieveMode mode, bool clearFilter)
        {
            // get parent
            var parent = currentPath.Peek();
            // retrieve items
            return panelFillers.RetrievePanelItems(parent.SingleOrDefault(), mode);
        }

        public void SetFillerResult(PanelFillerResult fillerResult, bool clearFilter)
        {
            lock (data)
            {
                data.Clear();
                // add ".." item
                if (currentPath.Any())
                    data.Add(new PanelItemDoubleDot(currentPath.Peek().Single()));
                // add items from filler
                data.AddRange(fillerResult.Items);
                // set current items DataType and filter
                if (fillerResult.ItemsType != null)
                    DataType = fillerResult.ItemsType.Name;
                // add custom items created by user
                foreach(var panelItem in Items)
                    if (panelItem.GetType().Name == DataType)
                        data.Add(panelItem);
                // sort 
                data.Sort(comparer);
                if (clearFilter)
                    FilterText = string.Empty;
                ApplyFilter();
            }
            OnChanged();
        }

        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public bool Equals(IPanelModel other)
        {
            return String.Compare(TabName, other.TabName, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public string ToolTipText
        {
            get { return string.Empty; }
        }

        public bool IsCacheable
        {
            get { return false; }
        }

        public bool Contains(PanelItemBase panelItem)
        {
            if (data.Contains(panelItem))
                return true;
            return items.Contains(panelItem);
        }

        public void SetDefaultRoot(PanelItemBase root)
        {
            if (root.Parent != null)
                SetDefaultRoot(root.Parent);
            CurrentPath.Push(root);
        }
    }
}