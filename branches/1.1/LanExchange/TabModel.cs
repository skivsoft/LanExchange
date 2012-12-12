using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LanExchange.Network;

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

        public TabModel(string name)
        {
            m_List = new List<PanelItemList>();
            m_Name = name;
            SelectedIndex = -1;
        }

        public int Count { get { return this.m_List.Count; }  }

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
            PanelItemList Info = m_List[Index];
            Info.TabName = NewTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            if (i < 0 || i > Count - 1)
                return null;
            else
                return m_List[i].TabName;
        }

        internal void Clear()
        {
            m_List.Clear();
        }


        public void StoreSettings()
        {
            /*
            //UpdateModelFromView();
            Settings config = Settings.GetInstance();
            config.SetIntValue(String.Format(@"{0}\SelectedIndex", m_Name), SelectedIndex);
            config.SetIntValue(String.Format(@"{0}\Count", m_Name), Count);
            for (int i = 0; i < Count; i++)
            {
                PanelItemList Info = GetItem(i);
                string S = String.Format(@"{0}.{1}\", m_Name, i);
                config.SetStrValue(S + "TabName", Info.TabName);
                config.SetStrValue(S + "FilterText", Info.FilterText);
                config.SetIntValue(S + "CurrentView", (int)Info.CurrentView);
                // элементы нулевой закладки не сохраняем, т.к. они формируются после обзора сети
                //config.SetListValue(S + "Items", i > 0 ? Info.Items : null);
                config.SetIntValue(S + "Scope", (int)(Info.Scope));
                config.SetListValue(S + "Groups", Info.Groups);
            }
             */
        }

        public void LoadSettings()
        {
            /*
            Clear();
            Settings config = Settings.GetInstance();
            int CNT = config.GetIntValue(String.Format(@"{0}\Count", m_Name), 0);
            if (CNT > 0)
            {
                for (int i = 0; i < CNT; i++)
                {
                    string S = String.Format(@"{0}.{1}\", m_Name, i);
                    string tabname = config.GetStrValue(S + "TabName", "");
                    PanelItemList Info = new PanelItemList(tabname) { 
                        FilterText = config.GetStrValue(S + "FilterText", ""), 
                        CurrentView = (View)config.GetIntValue(S + "CurrentView", (int)View.Details), 
                        Scope = (PanelItemList.PanelItemListScope)config.GetIntValue(S + "Scope", 0), 
                        Groups = config.GetListValue(S + "Groups") 
                    };
                    AddTab(Info);
                }
            }
            else
            {
                string domain = NetApi32Utils.GetMachineNetBiosDomain(null);
                PanelItemList Info = new PanelItemList(domain) { 
                    FilterText = "", 
                    CurrentView = (View.Details), 
                    Groups = new List<string>() };
                Info.Groups.Add(domain);
                AddTab(Info);
            }
            int Index = config.GetIntValue(String.Format(@"{0}\SelectedIndex", m_Name), 0);
            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            SelectedIndex = -1;
            SelectedIndex = Index;
             */
            string domain = NetApi32Utils.GetMachineNetBiosDomain(null);
            PanelItemList Info = new PanelItemList(domain)
            {
                FilterText = "",
                CurrentView = (View.Details),
                Groups = new List<string>()
            };
            Info.Groups.Add(domain);
            AddTab(Info);
        }

    }
}
