using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Model
{
    [XmlType("Tab")]
    public class PanelModel : IPanelModel
    {       
        // items added by user
        private readonly IList<PanelItemBase> m_Items;
        // merged all results and user items
        private readonly List<PanelItemBase> m_Data;
        // keys for filtering
        private readonly IList<PanelItemBase> m_Keys;
        // current path for item list
        private readonly ObjectPath<PanelItemBase> m_CurrentPath;
        // column sorter
        private readonly ColumnComparer m_Comparer;
        // punto switcher service
        private readonly IPuntoSwitcherService m_Punto;
        // panel updater
        private readonly IPanelUpdater m_Updater;

        public event EventHandler Changed;
        public event EventHandler TabNameUpdated;
        
        /// <summary>
        /// Parameterless constructor for xml serialization.
        /// </summary>
        public PanelModel()
        {
            m_Punto = App.Resolve<IPuntoSwitcherService>();
            m_Updater = App.Resolve<IPanelUpdater>();
            m_Items = new List<PanelItemBase>();
            m_Data = new List<PanelItemBase>();
            m_Keys = new List<PanelItemBase>();
            m_CurrentPath = new ObjectPath<PanelItemBase>();
            m_Comparer = new ColumnComparer(0, PanelSortOrder.Ascending);
            CurrentView = PanelViewMode.Details;
        }

        public void Dispose()
        {
            m_Updater.Dispose();
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
                var parent = m_CurrentPath.Peek();
                return parent.Name;
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
                var parent = m_CurrentPath.Peek();
                return parent.ImageName;
            }
        }

        public void AsyncRetrieveData(bool clearFilter)
        {
            m_Updater.Stop();
            m_Updater.Start(this, clearFilter);
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
        public ObjectPath<PanelItemBase> CurrentPath
        {
            get { return m_CurrentPath; }
            set
            {
                // build path from loaded items
                var items = value.Item;
                m_CurrentPath.Clear();
                for (int index = items.Length - 1; index >= 0; index--)
                {
                    var item = items[index];
                    if (index < items.Length - 1)
                        item.Parent = items[index + 1];
                    m_CurrentPath.Push(item);
                }
            }
        }

        [XmlElement("Focused")]
        public PanelItemBase FocusedItem { get; set; }

        public IList<PanelItemBase> Items
        {
            get { return m_Items; }
        }

        public PanelItemBase GetItemAt(int index)
        {
            return m_Keys[index];
        }

        public int IndexOf(PanelItemBase key)
        {
            return m_Keys.IndexOf(key);
        }

        private bool GoodForFilter(string[] strList, string filter1, string filter2)
        {
            for (int i = 0; i < strList.Length; i++)
            {
                if (i == 0)
                {
                    if (m_Punto.SpecificContains(strList[i], filter1) ||
                        m_Punto.SpecificContains(strList[i], filter2))
                        return true;
                } else
                if (filter1 != null && strList[i].Contains(filter1) || filter2 != null && strList[i].Contains(filter2))
                    return true;
            }
            return false;
        }

        public ColumnComparer Comparer
        {
            get { return m_Comparer; }
        }

        public void Sort(IComparer<PanelItemBase> sorter)
        {
            m_Data.Sort(sorter);
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
            m_Keys.Clear();
            var filter1 = FilterText.ToUpper(CultureInfo.CurrentCulture);
            var filter2 = m_Punto.Change(FilterText);
            if (filter2 != null) filter2 = filter2.ToUpper(CultureInfo.CurrentCulture);
            var helper = new PanelModelCopyHelper(this);
            var upperValues = new List<string>();
            foreach (var value in m_Data)
            {
                if (value is PanelItemDoubleDot)
                {
                    m_Keys.Add(value);
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
                    m_Keys.Add(value);
            }
        }

        public int Count
        {
            get { return m_Data.Count; }
        }

        public int FilterCount
        {
            get { return m_Keys.Count; }
        }

        public bool HasBackItem
        {
            get
            {
                if (m_Data.Count > 0)
                    if (m_Data[0] is PanelItemDoubleDot)
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
            var parent = m_CurrentPath.IsEmpty ? null : m_CurrentPath.Peek();
            // retrieve items
            return App.PanelFillers.RetrievePanelItems(parent, mode);
        }

        public void SetFillerResult(PanelFillerResult fillerResult, bool clearFilter)
        {
            lock (m_Data)
            {
                m_Data.Clear();
                // add ".." item
                if (!m_CurrentPath.IsEmptyOrRoot)
                    m_Data.Add(new PanelItemDoubleDot(m_CurrentPath.Peek()));
                // add items from filler
                m_Data.AddRange(fillerResult.Items);
                // set current items DataType and filter
                if (fillerResult.ItemsType != null)
                    DataType = fillerResult.ItemsType.Name;
                // add custom items created by user
                foreach(var panelItem in Items)
                    if (panelItem.GetType().Name == DataType)
                        m_Data.Add(panelItem);
                // sort 
                m_Data.Sort(m_Comparer);
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

        public PanelItemFactoryBase ItemFactory { get; set; }

        public bool Contains(PanelItemBase panelItem)
        {
            if (m_Data.Contains(panelItem))
                return true;
            return m_Items.Contains(panelItem);
        }

        public void SetDefaultRoot(PanelItemBase root)
        {
            if (root.Parent != null)
                SetDefaultRoot(root.Parent);
            CurrentPath.Push(root);
        }
    }
}