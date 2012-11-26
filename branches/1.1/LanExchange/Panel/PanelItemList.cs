using System;
using System.Collections.Generic;
using System.Text;
using OSTools;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using LanExchange.Network;
#if DEBUG
using NLog;
#endif

namespace LanExchange
{
    public class PanelItemList : ISubscriber
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private Dictionary<string, PanelItem> m_Data = null;
        private List<string> m_Keys = null;
        private String m_Filter = "";

        private string m_TabName = "";
        private View m_CurrentView = View.Details;
        private Dictionary<string, PanelItem> m_Items = null;
        private bool m_AllGroups = false;
        private List<string> m_Groups = null;

        public event EventHandler Changed;

        public PanelItemList(string name)
        {
            m_Data = new Dictionary<string, PanelItem>();
            m_Items = new Dictionary<string, PanelItem>();
            m_Keys = new List<string>();
            m_TabName = name;
        }

        public string TabName
        {
            get { return m_TabName; }
            set { m_TabName = value; }
        }

        public View CurrentView
        {
            get { return m_CurrentView; }
            set { m_CurrentView = value; }
        }

        public IDictionary<string, PanelItem> Items
        {
            get { return m_Items; }
        }

        public bool AllGroups
        {
            get { return m_AllGroups; }
            set { m_AllGroups = value; }
        }

        public List<string> Groups
        {
            get { return m_Groups; }
            set { m_Groups = value; }
        }

        public IList<string> Keys
        {
            get { return m_Keys; }
        }

        public void UpdateSubsctiption()
        {
            // оформляем подписку на получение списка компов
            if (AllGroups)
                NetworkScanner.GetInstance().SubscribeToAll(this);
            else
            {
                NetworkScanner.GetInstance().UnSubscribe(this);
                foreach (var group in Groups)
                    NetworkScanner.GetInstance().SubscribeToSubject(this, group);
            }
        }


        public void Add(PanelItem Comp)
        {
            if (Comp != null)
                if (!m_Data.ContainsKey(Comp.Name))
                    m_Data.Add(Comp.Name, Comp);
        }

        public void Delete(PanelItem Comp)
        {
            m_Data.Remove(Comp.Name);
        }

        public PanelItem Get(string key)
        {
            PanelItem Result = null;
            if (m_Data.TryGetValue(key, out Result))
            {
                Result.Name = key;
                return Result;
            }
            else
                return null;
        }

        public void Clear()
        {
            m_Data.Clear();
        }

        private bool GoodForFilter(string[] A, string Filter1, string Filter2)
        {
            for (int i = 0; i < A.Length; i++)
            {
                if (i == 0)
                {
                    if (PuntoSwitcher.RussianContains(A[i], Filter1) || (PuntoSwitcher.RussianContains(A[i], Filter2)))
                        return true;
                } else
                if (Filter1 != null && A[i].Contains(Filter1) || Filter2 != null && A[i].Contains(Filter2))
                    return true;
            }
            return false;
        }

        public void ApplyFilter()
        {
            bool bFiltered = IsFiltered;
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

        public bool IsFiltered
        {
            get { return !String.IsNullOrEmpty(m_Filter); }
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

        public String FilterText
        {
            get { return m_Filter; }
            set
            {
                m_Filter = value;
                ApplyFilter();
            }
        }

        public static List<PanelItem> EnumNetShares(string Server)
        {
            List<PanelItem> Result = new List<PanelItem>();
            Result.Add(new SharePanelItem("", "", 0, Server));
            int entriesread = 0;
            int totalentries = 0;
            int resume_handle = 0;
            int nStructSize = Marshal.SizeOf(typeof(NetApi32.SHARE_INFO_1));
            IntPtr bufPtr = IntPtr.Zero;
            StringBuilder server = new StringBuilder(Server);
            logger.Info("WINAPI NetShareEnum");
            int ret = NetApi32.NetShareEnum(server, 1, ref bufPtr, NetApi32.MAX_PREFERRED_LENGTH, ref entriesread, ref totalentries, ref resume_handle);
            if (ret == NetApi32.NERR_Success)
            {
                logger.Info("WINAPI NetServerEnum result: entriesread={0}, totalentries={1}", entriesread, totalentries);
                IntPtr currentPtr = bufPtr;
                for (int i = 0; i < entriesread; i++)
                {
                    NetApi32.SHARE_INFO_1 shi1 = (NetApi32.SHARE_INFO_1)Marshal.PtrToStructure(currentPtr, typeof(NetApi32.SHARE_INFO_1));
                    if ((shi1.shi1_type & (uint)NetApi32.SHARE_TYPE.STYPE_IPC) != (uint)NetApi32.SHARE_TYPE.STYPE_IPC)
                        Result.Add(new SharePanelItem(shi1.shi1_netname, shi1.shi1_remark, shi1.shi1_type, Server));
                    else
                        logger.Info("Skiping IPC$ share");
                    currentPtr = new IntPtr(currentPtr.ToInt32() + nStructSize);
                }
                NetApi32.NetApiBufferFree(bufPtr);
            }
            else
            {
                logger.Info("WINAPI NetServerEnum error: {0}", ret);
            }

            PanelItemComparer comparer = new PanelItemComparer();
            Result.Sort(comparer);
            return Result;
        }


        public List<string> ListView_GetSelected(ListView LV, bool bAll)
        {
            List<string> Result = new List<string>();
            if (LV.FocusedItem != null)
                Result.Add(LV.FocusedItem.Text);
            else
                Result.Add("");
            if (bAll)
                for (int index = 0; index < LV.Items.Count; index++)
                    Result.Add(m_Keys[index]);
            else
                foreach (int index in LV.SelectedIndices)
                    Result.Add(m_Keys[index]);
            return Result;
        }
        
        public void ListView_SetSelected(ListView LV, List<string> SaveSelected)
        {
            LV.SelectedIndices.Clear();
            LV.FocusedItem = null;
            if (LV.VirtualListSize > 0)
            {
                for (int i = 0; i < SaveSelected.Count; i++)
                {
                    int index = m_Keys.IndexOf(SaveSelected[i]);
                    if (index == -1) continue;
                    if (i == 0)
                    {
                        LV.FocusedItem = LV.Items[index];
                        //LV.EnsureVisible(index);
                    }
                    else
                        LV.SelectedIndices.Add(index);
                }
            }
        }

        // <summary>
        // Выбор компьютера по имени в списке.
        // </summary>
        public void ListView_SelectComputer(ListView LV, string CompName)
        {
            int index = -1;
            // пробуем найти запомненный элемент
            if (CompName != null)
            {
                index = this.m_Keys.IndexOf(CompName);
                if (index == -1) index = 0;
            }
            else
                index = 0;
            // установка текущего элемента
            if (LV.VirtualListSize > 0)
            {
                LV.SelectedIndices.Add(index);
                LV.FocusedItem = LV.Items[index];
                LV.EnsureVisible(index);
            }
        }

        public List<string> ToList()
        {
            List<string> Result = new List<string>();
            foreach (var Pair in m_Data)
                Result.Add(Pair.Value.Name);
            return Result;
        }

        public void DataChanged(ISubscriptionProvider sender, DataChangedEventArgs e)
        {
            IList<ServerInfo> List = (IList<ServerInfo>)e.Data;
            lock (m_Data)
                lock(m_Keys)
                {
                    m_Data.Clear();
                    foreach (var SI in List)
                        m_Data.Add(SI.Name, new ComputerPanelItem(SI.Name, SI));
                    ApplyFilter();
                }
            if (Changed != null)
                Changed(this, new EventArgs());
        }
    }
}
