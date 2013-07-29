using System;
using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Utils;
using System.IO;
using LanExchange.Model.Settings;

namespace LanExchange.Model
{
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class PagesModel
    {
        private const string CONFIG_FNAME = "LanExchange.Tabs.cfg";
        private readonly List<PanelItemList> m_List;
        private int m_SelectedIndex;

        public event EventHandler<PanelItemListEventArgs> AfterAppendTab;
        public event EventHandler<PanelIndexEventArgs> AfterRemove;
        public event EventHandler<PanelItemListEventArgs> AfterRename;
        public event EventHandler<PanelIndexEventArgs> IndexChanged;

        private Tabs m_PagesSettings;

        public PagesModel()
        {
            m_List = new List<PanelItemList>();
            m_PagesSettings = new Tabs();
            m_SelectedIndex = -1;
        }

        private static string GetConfigFileName()
        {
            var path = Path.GetDirectoryName(Settings.Settings.GetExecutableFileName());
            if (path == null)
                throw new ArgumentNullException();
            return Path.Combine(path, CONFIG_FNAME);
        }

        public int Count { get { return m_List.Count; }  }

        private int m_LockCount;

        public int SelectedIndex 
        {
            get
            {
                return m_SelectedIndex;
            }
            set
            {
                m_SelectedIndex = value;
                if (m_LockCount == 0)
                {
                    m_LockCount++;
                    DoIndexChanged(value);
                    m_LockCount--;
                }
            }
        }

        public PanelItemList GetItem(int index)
        {
            return index < 0 || index >= m_List.Count ? null : m_List[index];
        }

        public int GetItemIndex(PanelItemList item)
        {
            return m_List.IndexOf(item);
        }

        private void DoAfterAppendTab(PanelItemList info)
        {
            if (AfterAppendTab != null)
                AfterAppendTab(this, new PanelItemListEventArgs(info));
        }

        private void DoAfterRemove(int index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new PanelIndexEventArgs(index));
        }

        private void DoAfterRename(PanelItemList info)
        {
            if (AfterRename != null)
                AfterRename(this, new PanelItemListEventArgs(info));
        }

        private void DoIndexChanged(int index)
        {
            if (IndexChanged != null)
                IndexChanged(this, new PanelIndexEventArgs(index));
        }

        //public bool Contains(PanelItemList info)
        //{
        //    return m_List.Contains(info);
        //}
        
        public void AddTab(PanelItemList info)
        {
            // ommit duplicates
            if (!m_List.Contains(info))
            {
                m_List.Add(info);
                if (m_SelectedIndex == -1 && m_List.Count == 1)
                    m_SelectedIndex = 0;
                DoAfterAppendTab(info);
                info.UpdateSubscription();
            }
        }

        public void DelTab(int index)
        {
            if (index >= 0 && index < m_List.Count)
            {
                PanelSubscription.Instance.Unsubscribe(m_List[index], true);
                m_List.RemoveAt(index);
                DoAfterRemove(index);
            }
        }

        public void RenameTab(int index, string newTabName)
        {
            var Info = m_List[index];
            Info.TabName = newTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            return i >= 0 && i <= Count - 1 ? m_List[i].TabName : null;
        }

        public void LoadSettings()
        {
            var fileFName = GetConfigFileName();
            if (File.Exists(fileFName))
            {
                try
                {
                    var temp = (Tabs) SerializeUtils.DeserializeObjectFromXMLFile(fileFName, typeof (Tabs));
                    if (temp != null)
                    {
                        m_PagesSettings = null;
                        m_PagesSettings = temp;
                    }
                }
                catch (Exception)
                {
                }
            }
            if (m_PagesSettings == null)
                throw new ArgumentNullException();
            if (m_PagesSettings.Items.Length > 0)
            {
                Array.ForEach(m_PagesSettings.Items, page =>
                {
                    var Info = new PanelItemList(page.Name) { Settings = page };
                    AddTab(Info);
                });
            }
            else
            {
                // TODO UNCOMMENT THIS!
                //var domain = NetApi32Utils.Instance.GetMachineNetBiosDomain(null);
                //var Info = new PanelItemList(domain);
                //Info.Groups.Add(new DomainPanelItem(domain));
                //AddTab(Info);
            }
            if (m_PagesSettings.SelectedIndex != -1)
                SelectedIndex = m_PagesSettings.SelectedIndex;
        }

        public void SaveSettings()
        {
            m_PagesSettings.SelectedIndex = SelectedIndex;
            var pages = new List<Tab>();
            for (int i = 0; i < Count; i++)
                pages.Add(GetItem(i).Settings);
            m_PagesSettings.Items = pages.ToArray();
            var fileFName = GetConfigFileName();
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(fileFName, m_PagesSettings);
            }
            catch (Exception)
            {
            }
        }
    }
}
