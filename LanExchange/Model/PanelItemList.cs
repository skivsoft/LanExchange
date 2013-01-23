using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Model.Panel;
using LanExchange.Utils;
using LanExchange.Model.Settings;

//using NLog;

namespace LanExchange.Model
{
    //public enum PanelItemType
    //{
    //    COMPUTERS = 0,
    //    SHARES = 1,
    //    FILES = 2
    //}

    public class PanelItemList : ISubscriber, IEquatable<PanelItemList>, IFilterModel
    {
        //private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        // items added by user
        private readonly IList<AbstractPanelItem> m_Items;
        // merged all results and user items
        private readonly List<AbstractPanelItem> m_Data;
        // keys for filtering
        private readonly IList<IComparable> m_Keys;
        // current path for item list
        private readonly ObjectPath m_CurrentPath;

        public event EventHandler Changed;
        public event EventHandler SubscriptionChanged;

        //private ListView m_LV = null;
        //private PanelItemType m_CurrentType = PanelItemType.COMPUTERS;
        //private string m_Path = null;

        public PanelItemList(string name)
        {
            m_Items = new List<AbstractPanelItem>();
            m_Data = new List<AbstractPanelItem>();
            m_Keys = new List<IComparable>();
            Groups = new List<ISubject>();
            m_CurrentPath = new ObjectPath();
            TabName = name;
            CurrentView = System.Windows.Forms.View.Details;
        }

        public ObjectPath CurrentPath
        {
            get { return m_CurrentPath; }
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
                Page.SetScanGroups(Groups);
                return Page;
            }
            set
            {
                TabName = value.Name;
                FilterText = value.Filter;
                CurrentView = value.View;
                Groups = value.GetScanGroups();
                FocusedItemText = value.Focused;
            }
            
        }

        public string TabName { get; set; }
        public System.Windows.Forms.View CurrentView { get; set; }
        public IList<ISubject> Groups { get; set; }
        public string FocusedItemText { get; set; }

        public void UpdateSubsctiption()
        {
            if (Groups.Count > 0)
            {
                PanelSubscription.Instance.UnSubscribe(this, false);
                foreach (var group in Groups)
                    PanelSubscription.Instance.SubscribeToSubject(this, group);
            } else
                PanelSubscription.Instance.UnSubscribe(this, true);
            if (SubscriptionChanged != null)
                SubscriptionChanged(this, EventArgs.Empty);
        }


        //TODO: add delete item
        //public void Delete(PanelItem comp)
        //{
        //    m_Data.Remove(comp.Name);
        //}

        public AbstractPanelItem GetAt(int index)
        {
            return Get(m_Keys[index].ToString());
        }

        public AbstractPanelItem Get(string key)
        {
            if (key == null) return null;
            var tempComp = new ComputerPanelItem(null, new ServerInfo { Name = key });
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
            bool bFiltered = !String.IsNullOrEmpty(FilterText);
            if (bFiltered)
            {
                if (m_Data.Count == 0)
                    bFiltered = false;
                else
                if (m_Keys.Count > 0)
                {
                    var PItem = GetAt(0);
                    // apply filter only for computers
                    if (!(PItem is ComputerPanelItem))
                        bFiltered = false;
                }
            }
            else
                FilterText = String.Empty;
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

        public bool HasBackItem()
        {
            if (m_Data.Count > 0)
                if (m_Data[0].Name == AbstractPanelItem.BACK)
                    return true;
            return false;
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
        /// ISubsctiber.DataChanged implementation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="subject"></param>
        public void DataChanged(ISubscription sender, ISubject subject)
        {
            //lock (m_Data)
            {
                m_Data.Clear();
                if (subject != ConcreteSubject.Empty)
                {
                    // for computers only we uses scan and user items
                    if (subject is DomainPanelItem)
                    {
                        foreach (var group in Groups)
                            foreach (AbstractPanelItem comp in sender.GetListBySubject(group))
                                m_Data.Add(comp);
                        foreach (var item in m_Items)
                            m_Data.Add(item);
                    }
                    // for other subjects we will use last element of path
                    else
                    {
                        //ISubject group = (ISubject)m_CurrentPath.Peek();
                        if (subject != null && subject != ConcreteSubject.Empty)
                            foreach (AbstractPanelItem comp in sender.GetListBySubject(subject))
                                m_Data.Add(comp);
                    }
                    m_Data.Sort();
                }
                //lock (m_Keys)
                {
                    // filtering only computer items
                    ApplyFilter();
                }
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
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Groups.Count > 0)
                {
                    foreach (var group in Groups)
                    {
                        if (sb.Length > 0)
                            sb.Append(", ");
                        sb.Append(group.Subject);
                    }
                    sb.Insert(0, "Обзор сети: ");
                    sb.Append(".");
                }
                else
                    sb.Append("Обзор сети отключен.");
                return sb.ToString();
            }
        }
    }
}
