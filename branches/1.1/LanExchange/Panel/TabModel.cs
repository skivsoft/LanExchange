using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Network;
using LanExchange.Utils;
using System.IO;
using LanExchange.Model;

namespace LanExchange
{
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class TabModel
    {
        #region Subclasses and delegates
        public class PanelItemListEventArgs : EventArgs
        {
            private readonly PanelItemList m_Info;

            public PanelItemListEventArgs(PanelItemList Info)
            {
                m_Info = Info;
            }

            public PanelItemList Info { get { return m_Info; } }
        }

        public class IndexEventArgs : EventArgs
        {
            private readonly int m_Index;

            public IndexEventArgs(int Index)
            {
                m_Index = Index;
            }

            public int Index { get { return m_Index; } }
        }

        public delegate void PanelItemListEventHandler(object sender, PanelItemListEventArgs e);
        public delegate void IndexEventHandler(object sender, IndexEventArgs e);
        #endregion


        private readonly List<PanelItemList> m_List;
        private readonly string m_Name;

        public event PanelItemListEventHandler AfterAppendTab;
        public event IndexEventHandler AfterRemove;
        public event PanelItemListEventHandler AfterRename;

        private LanExchangeTabs m_Pages;

        public TabModel(string name)
        {
            m_List = new List<PanelItemList>();
            m_Pages = new LanExchangeTabs();
            m_Name = name;
            SelectedIndex = -1;
        }

        public int Count { get { return m_List.Count; }  }

        public int SelectedIndex { get; set; }

        public PanelItemList GetItem(int Index)
        {
            return m_List[Index];
        }

        public void DoAfterAppendTab(PanelItemList Info)
        {
            if (AfterAppendTab != null)
               AfterAppendTab(this, new PanelItemListEventArgs(Info));
        }

        public void DoAfterRemove(int Index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new IndexEventArgs(Index));
        }

        public void DoAfterRename(PanelItemList Info)
        {
            if (AfterRename != null)
                AfterRename(this, new PanelItemListEventArgs(Info));
        }

        public void AddTab(PanelItemList Info)
        {
            m_List.Add(Info);
            DoAfterAppendTab(Info);
        }

        public void InternalAdd(PanelItemList Info)
        {
            m_List.Add(Info);
        }

        public void DelTab(int Index)
        {
            if (Index >= 0 && Index < m_List.Count)
            {
                NetworkScanner.GetInstance().UnSubscribe(m_List[Index]);
                m_List.RemoveAt(Index);
                DoAfterRemove(Index);
            }
        }

        public void RenameTab(int Index, string NewTabName)
        {
            var Info = m_List[Index];
            Info.TabName = NewTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            return i >= 0 && i <= Count - 1 ? m_List[i].TabName : null;
        }

        internal void Clear()
        {
            m_List.Clear();
        }

        public static string GetConfigFileName()
        {
            return Path.Combine(Path.GetDirectoryName(Settings.GetExecutableFileName()), "Pages.cfg");
        }

        public void StoreSettings()
        {
            m_Pages.SelectedIndex = SelectedIndex;
            TabSettings[] pages = new TabSettings[Count];
            for (int i = 0; i < Count; i++)
                pages[i] = GetItem(i).Settings;
            m_Pages.Items = pages;
            SerializeUtils.SerializeTypeToXMLFile(GetConfigFileName(), m_Pages);
        }

        public void LoadSettings()
        {
            Clear();
            try
            {
                m_Pages = (LanExchangeTabs)SerializeUtils.DeserializeObjectFromXMLFile(GetConfigFileName(), typeof(LanExchangeTabs));
            }
            catch { }
            if (m_Pages.Items.Length > 0)
                Array.ForEach(m_Pages.Items, Page =>
                {
                    var Info = new PanelItemList(Page.Name);
                    Info.Settings = Page;
                    AddTab(Info);
                });
            else
            {
                string domain = NetApi32Utils.GetMachineNetBiosDomain(null);
                var Info = new PanelItemList(domain)
                {
                    CurrentView = System.Windows.Forms.View.Details,
                    ScanMode = PanelItemList.PanelScanMode.Selected
                };
                Info.Groups.Add(domain);
                AddTab(Info);
            }

            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            SelectedIndex = -1;
            SelectedIndex = Settings.Instance.SelectedIndex;
        }

    }
}
