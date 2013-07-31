using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.Panel;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.Utils;
using LanExchange.Model.Settings;

namespace LanExchange.Model
{
    public class PanelItemList : IEquatable<PanelItemList>, IPanelModel
    {       
        //private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        // items added by user
        private readonly IList<PanelItemBase> m_Items;
        // merged all results and user items
        private readonly List<PanelItemBase> m_Data;
        // keys for filtering
        private readonly IList<IComparable> m_Keys;
        // current path for item list
        private readonly ObjectPath m_CurrentPath;

        public event EventHandler Changed;
        
        //private ListView m_LV = null;
        //private PanelItemType m_CurrentType = PanelItemType.COMPUTERS;
        //private string m_Path = null;

        public PanelItemList(string name)
        {
            m_Items = new List<PanelItemBase>();
            m_Data = new List<PanelItemBase>();
            m_Keys = new List<IComparable>();
            m_CurrentPath = new ObjectPath();
            TabName = name;
            CurrentView = PanelViewMode.Details;
        }

        public ObjectPath CurrentPath
        {
            get { return m_CurrentPath; }
        }

        public IList<PanelItemBase> Items
        {
            get { return m_Items; }
        }

        public Tab Settings
        {
            get
            {
                var Page = new Tab { 
                    Name = TabName, 
                    Filter = FilterText, 
                    View = CurrentView,
                    Focused = FocusedItemText
                };
                // TODO UNCOMMENT THIS!
                //var TempList = new List<ServerInfo>();
                //foreach (ComputerPanelItem PItem in m_Items)
                //    TempList.Add(PItem.SI);
                //Page.Items = TempList.ToArray();
                return Page;
            }
            set
            {
                TabName = value.Name;
                FilterText = value.Filter;
                CurrentView = value.View;
                FocusedItemText = value.Focused;
                Items.Clear();
                // TODO UNCOMMENT THIS!
                //foreach (var si in value.Items)
                //{
                //    var comp = new ComputerPanelItem(null, si);
                //    comp.ParentSubject = ConcreteSubject.s_UserItems;
                //    Items.Add(comp);
                //}
            }
            
        }

        public string TabName { get; set; }
        public PanelViewMode CurrentView { get; set; }

        public string FocusedItemText { get; set; }

        //TODO: add delete item
        //public void Delete(PanelItem comp)
        //{
        //    m_Data.Remove(comp.Name);
        //}

        public PanelItemBase GetItemAt(int index)
        {
            var item = m_Keys[index];
            return item != null ? GetItem(item.ToString()) : null;
        }

        public PanelItemBase GetItem(string key)
        {
            // TODO UNCOMMENT THIS!
            if (key == null) return null;
            var tempComp = new PanelItemCustom(null, key);
            int index = m_Data.BinarySearch(tempComp);
            if (index >= 0)
                return m_Data[index];
            return null;
        }

        public int IndexOf(string key)
        {
            return m_Keys.IndexOf(key);
        }

        //public void Clear()
        //{
        //    m_Data.Clear();
        //}

        private static bool GoodForFilter(string[] strList, string filter1, string filter2)
        {
            for (int i = 0; i < strList.Length; i++)
            {
                if (i == 0)
                {
                    if (PuntoSwitcher.RussianContains(strList[i], filter1) || (PuntoSwitcher.RussianContains(strList[i], filter2)))
                        return true;
                } else
                if (filter1 != null && strList[i].Contains(filter1) || filter2 != null && strList[i].Contains(filter2))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// IFilterModel.FilterText
        /// </summary>
        public String FilterText { get; set; }

        /// <summary>
        /// IFilterModel.AppliFilter()
        /// </summary>
        public void ApplyFilter()
        {
            if (FilterText == null) 
                FilterText = String.Empty;
            bool bFiltered = FilterText != String.Empty;
            if (bFiltered && !CurrentPath.IsEmpty)
            {
                bFiltered = false;
            }
            m_Keys.Clear();
            string Filter1 = FilterText.ToUpper();
            string Filter2 = PuntoSwitcher.Change(FilterText);
            if (Filter2 != null) Filter2 = Filter2.ToUpper();
            foreach (var value in m_Data)
            {
                string[] A = value.GetStringsUpper();
                if (!bFiltered || String.IsNullOrEmpty(value[0].ToString()) || GoodForFilter(A, Filter1, Filter2))
                    m_Keys.Add(value[0]);
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
                    if (m_Data[0].Name == PanelItemBase.s_DoubleDot)
                        return true;
                return false;
            }
        }

        //    PanelItemComparer comparer = new PanelItemComparer();
        //    Result.Sort(comparer);
        //    return Result;
        //}

        // TODO uncomment ListView_GetSelected
        //public List<string> ListView_GetSelected(ListView LV, bool bAll)
        //{
        //    List<string> Result = new List<string>();
        //    if (LV.FocusedItem != null)
        //        Result.Add(LV.FocusedItem.Text);
        //    else
        //        Result.Add("");
        //    if (bAll)
        //        for (int index = 0; index < LV.Items.Count; index++)
        //            Result.Add(m_Keys[index]);
        //    else
        //        foreach (int index in LV.SelectedIndices)
        //            Result.Add(m_Keys[index]);
        //    return Result;
        //}
        
        // TODO uncomment ListView_SetSelected
        //public void ListView_SetSelected(ListView LV, List<string> SaveSelected)
        //{
        //    LV.SelectedIndices.Clear();
        //    LV.FocusedItem = null;
        //    if (LV.VirtualListSize > 0)
        //    {
        //        for (int i = 0; i < SaveSelected.Count; i++)
        //        {
        //            int index = m_Keys.IndexOf(SaveSelected[i]);
        //            if (index == -1) continue;
        //            if (i == 0)
        //            {
        //                LV.FocusedItem = LV.Items[index];
        //                //LV.EnsureVisible(index);
        //            }
        //            else
        //                LV.SelectedIndices.Add(index);
        //        }
        //    }
        //}


        //public void Add(AbstractPanelItem comp)
        //{
        //    if (comp == null)
        //        throw new ArgumentNullException("comp");
        //    if (!m_Data.Contains(comp))
        //        m_Data.Add(comp);
        //}

        //public List<string> ToList()
        //{
        //    List<string> Result = new List<string>();
        //    foreach (var Pair in m_Data)
        //        Result.Add(Pair.Value.Name);
        //    return Result;
        //}

        /// <summary>
        /// Sync retrieving panel items using appropriate filler strategy.
        /// </summary>
        public void SyncRetrieveData()
        {
            // update data
            var items = AppPresenter.PanelFillers.RetrievePanelItems(m_CurrentPath.Peek() as PanelItemBase);
            lock (m_Data)
            {
                m_Data.Clear();
                m_Data.AddRange(items);
                m_Data.Sort();
                ApplyFilter();
            }
            OnChanged();
        }

 
        private void OnChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        public bool Equals(PanelItemList other)
        {
            return String.Compare(TabName, other.TabName, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public string ToolTipText
        {
            get { return String.Empty; }
        }

        public string Subject
        {
            get { return String.Format("Items_{0}", TabName); }
        }

        public bool IsCacheable
        {
            get { return false; }
        }

        public PanelItemBaseFactory ItemFactory { get; set; }
    }
}
