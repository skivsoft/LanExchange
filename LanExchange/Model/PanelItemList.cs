using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using NLog;
using LanExchange.Utils;

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
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        // items added by user
        private readonly SortedDictionary<string, PanelItem> m_Items;
        // merged all results and user items
        private readonly SortedDictionary<string, PanelItem> m_Data;
        // keys for filtering
        private readonly List<string> m_Keys;

        public event EventHandler Changed;
        public event EventHandler SubscriptionChanged;

        //private ListView m_LV = null;
        //private PanelItemType m_CurrentType = PanelItemType.COMPUTERS;
        //private string m_Path = null;

        public PanelItemList(string name)
        {
            m_Items = new SortedDictionary<string, PanelItem>();
            m_Data = new SortedDictionary<string, PanelItem>();
            m_Keys = new List<string>();
            Groups = new List<string>();
            TabName = name;
            CurrentView = System.Windows.Forms.View.Details;
            ScanMode = false;
        }

        public TabSettings Settings
        {
            get
            {
                var Page = new TabSettings { 
                    Name = TabName, 
                    Filter = FilterText, 
                    CurrentView = CurrentView, 
                    ScanMode = ScanMode, 
                    ScanGroups = Groups 
                };
                return Page;
            }
            set
            {
                TabName = value.Name;
                FilterText = value.Filter;
                CurrentView = value.CurrentView;
                ScanMode = value.ScanMode;
                Groups = value.ScanGroups;
            }
            
        }

        public string TabName { get; set; }

        public System.Windows.Forms.View CurrentView { get; set; }

        //public IDictionary<string, PanelItem> Items
        //{
        //    get { return m_Items; }
        //}

        public bool ScanMode { get; set; }

        public List<string> Groups { get; set; }

        //public IList<string> Keys
        //{
        //    get { return m_Keys; }
        //}

        //public string FocusedItem { get; set; }

        public void UpdateSubsctiption()
        {
            switch (ScanMode)
            {
                case true:
                    ServerListSubscription.Instance.UnSubscribe(this);
                    Groups.ForEach(group => ServerListSubscription.Instance.SubscribeToSubject(this, group));
                    break;
                default:
                    ServerListSubscription.Instance.UnSubscribe(this);
                    break;
            }
            if (SubscriptionChanged != null)
                SubscriptionChanged(this, new EventArgs());
        }


        public void Add(PanelItem comp)
        {
            if (comp != null)
                if (!m_Data.ContainsKey(comp.Name))
                    m_Data.Add(comp.Name, comp);
        }

        //TODO: add delete item
        //public void Delete(PanelItem comp)
        //{
        //    m_Data.Remove(comp.Name);
        //}

        public PanelItem GetAt(int index)
        {
            return Get(m_Keys[index]);
        }

        public PanelItem Get(string key)
        {
            if (key == null) return null;
            PanelItem Result;
            if (m_Data.TryGetValue(key, out Result))
            {
                Result.Name = key;
                return Result;
            }
            return null;
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
            if (!bFiltered)
                FilterText = String.Empty;
            m_Keys.Clear();
            string Filter1 = FilterText.ToUpper();
            string Filter2 = PuntoSwitcher.Change(FilterText);
            if (Filter2 != null) Filter2 = Filter2.ToUpper();
            foreach (var Pair in m_Data)
            {
                string[] A = Pair.Value.getStrings();
                if (!bFiltered || String.IsNullOrEmpty(Pair.Value.Name) || GoodForFilter(A, Filter1, Filter2))
                    m_Keys.Add(Pair.Value.Name);
            }
        }

        // Возвращает количество компов в списке
        public int Count
        {
            get { return m_Data.Count; }
        }

        // Возвращает число записей в фильтре
        public int FilterCount
        {
            get { return m_Keys.Count; }
        }

        // TODO: uncomment EnumNetShares
        //public static List<PanelItem> EnumNetShares(string Server)
        //{
        //    List<PanelItem> Result = new List<PanelItem>();
        //    Result.Add(new SharePanelItem("", "", 0, Server));
        //    int entriesread = 0;
        //    int totalentries = 0;
        //    int resume_handle = 0;
        //    int nStructSize = Marshal.SizeOf(typeof(NetApi32.SHARE_INFO_1));
        //    IntPtr bufPtr = IntPtr.Zero;
        //    StringBuilder server = new StringBuilder(Server);
        //    logger.Info("WINAPI NetShareEnum");
        //    int ret = NetApi32.NetShareEnum(server, 1, ref bufPtr, NetApi32.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
        //    if (ret == NetApi32.NERR_Success)
        //    {
        //        logger.Info("WINAPI NetServerEnum result: entriesread={0}, totalentries={1}", entriesread, totalentries);
        //        IntPtr currentPtr = bufPtr;
        //        for (int i = 0; i < entriesread; i++)
        //        {
        //            NetApi32.SHARE_INFO_1 shi1 = (NetApi32.SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(NetApi32.SHARE_INFO_1));
        //            if ((shi1.shi1_type & (uint)NetApi32.SHARE_TYPE.STYPE_IPC) != (uint)NetApi32.SHARE_TYPE.STYPE_IPC)
        //                Result.Add(new SharePanelItem(shi1.shi1_netname, shi1.shi1_remark, shi1.shi1_type, Server));
        //            else
        //                logger.Info("Skiping IPC$ share");
        //            currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
        //        }
        //        NetApi32.NetApiBufferFree(bufPtr);
        //    }
        //    else
        //    {
        //        logger.Info("WINAPI NetServerEnum error: {0}", ret);
        //    }

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


        public List<string> ToList()
        {
            List<string> Result = new List<string>();
            foreach (var Pair in m_Data)
                Result.Add(Pair.Value.Name);
            return Result;
        }

        /// <summary>
        /// ISubsctiber.DataChanged implementation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void DataChanged(ISubscription sender, string subject)
        {
            //lock (m_Data)
            {
                m_Data.Clear();
                if (ScanMode)
                    Groups.ForEach(group =>
                    {
                        foreach (ServerInfo si in sender.GetListBySubject(group))
                            if (!m_Data.ContainsKey(si.Name))
                                m_Data.Add(si.Name, new ComputerPanelItem(si.Name, si));
                    });
                foreach (var Pair in m_Items)
                    m_Data.Add(Pair.Key, Pair.Value);
                //lock (m_Keys)
                {
                    ApplyFilter();
                }
            }
            if (Changed != null)
                Changed(this, new EventArgs());
        }
 
        /// <summary>
        /// Возвращает список элементов с верхнего уровня из стека переходов.
        /// В частности это будет список копьютеров, даже если мы находимся на уровне списка ресуров.
        /// </summary>
        /// <returns></returns>
        public IList<PanelItem> GetTopItemList()
        {
            return null;
            /*
            if (InternalStack.Count == 0)
                return InternalItems;
            else
            {
                IList<PanelItem>[] Arr = InternalStack.ToArray();
                return Arr[0];
            }
             */
        }

        public void LevelDown()
        {
            /*
            if (LV == null || LV.FocusedItem == null)
                return;
            string FocusedText = LV.FocusedItem.Text;
            if (String.IsNullOrEmpty(FocusedText))
            {
                LevelUp();
                return;
            }

            switch (ViewType)
            {
                case LVType.COMPUTERS:
                    if (LV.FocusedItem == null)
                        break;
                    // останавливаем поток пингов
                    MainForm.GetInstance().CancelCompRelatedThreads();
                    // сбрасываем фильтр
                    MainForm.GetInstance().UpdateFilter(MainForm.GetInstance().GetActiveListView(), "", false);
                    // текущий список добавляем в стек
                    //if (InternalItems == null)
                    //    InternalItems = InternalItemList.ToList();
                    InternalStack.Push(InternalItems);
                    // получаем новый список объектов, в данном случае список ресурсов компа
                    InternalItems = PanelItemList.EnumNetShares(FocusedText);
                    // устанавливаем новый список для визуального компонента
                    CurrentDataTable = InternalItems;
                    if (LV.VirtualListSize > 0)
                    {
                        LV.FocusedItem = LV.Items[0];
                        LV.SelectedIndices.Add(0);
                    }
                    // меняем колонки в ListView
                    Path = @"\\" + FocusedText;
                    ViewType = LVType.SHARES;
                    break;
                case LVType.SHARES:
                    MainForm.GetInstance().mFolderOpen_Click(MainForm.GetInstance().mFolderOpen, new EventArgs());
                    break;
                case LVType.FILES:
                    break;
            }
             */
        }

        public void LevelUp()
        {
            /*
            if (InternalStack.Count == 0)
                return;

            //TPanelItem PItem = null;
            string CompName = null;
            if (InternalItemList.Count > 0)
            {
                CompName = Path;
                if (CompName.Length > 2 && CompName[0] == '\\' && CompName[1] == '\\')
                    CompName = CompName.Remove(0, 2);
            }

            InternalItems = InternalStack.Pop();

            
            switch (CurrentType)
            {
                case LVType.COMPUTERS:
                    break;
                case LVType.SHARES:
                    ViewType = LVType.COMPUTERS;
                    break;
                case LVType.FILES:
                    ViewType = LVType.SHARES;
                    break;
            }
            CurrentDataTable = InternalItems;
            InternalItemList.ListView_SelectComputer(MainForm.GetInstance().lvComps, CompName);

            MainForm.GetInstance().UpdateFilter(MainForm.GetInstance().GetActiveListView(), MainForm.GetInstance().eFilter.Text, true);
             */
        }

        public bool Equals(PanelItemList other)
        {
            return String.Compare(TabName, other.TabName, StringComparison.OrdinalIgnoreCase) == 0;
        }

        public string GetTabToolTip()
        {
            StringBuilder sb = new StringBuilder();
            if (ScanMode)
            {
                sb.Append("Обзор сети: ");
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (i > 0)
                        sb.Append(", ");
                    sb.Append(Groups[i]);
                }
                sb.Append(".");
            }
            else
                sb.Append("Обзор сети отключен.");
            return sb.ToString();
        }
    }
}
