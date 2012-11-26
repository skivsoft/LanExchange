using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using LanExchange.Forms;
using LanExchange.Network;

namespace LanExchange
{
    public class TabInfoEventArgs : EventArgs
    {
        private TabInfo info;

        public TabInfoEventArgs(TabInfo Info)
        {
            this.info = Info;
        }

        public TabInfo Info { get { return this.info; }}
    }

    public delegate void TabInfoEventHandler(object sender, TabInfoEventArgs e);

    public class IndexEventArgs : EventArgs
    {
        private int index;

        public IndexEventArgs(int Index)
        {
            this.index = Index;
        }

        public int Index { get { return this.index; } }
    }

    public delegate void IndexEventHandler(object sender,  IndexEventArgs e);

    public class TabInfo
    {
        public string TabName = "";
        public string FilterText = "";
        public View CurrentView = View.Details;
        public List<string> Items = null;
        public bool AllGroups = false;
        public List<string> Groups = null;

        public TabInfo(string name)
        {
            this.TabName = name;
        }
    }
    
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class TabModel
    {
        private List<TabInfo> InfoList = new List<TabInfo>();
        private string m_Name = null;
        private int m_SelectedIndex = -1;

        public event TabInfoEventHandler AfterAppendTab;
        public event IndexEventHandler AfterRemove;
        public event TabInfoEventHandler AfterRename;

        public TabModel(string name)
        {
            m_Name = name;
        }

        public int Count { get { return this.InfoList.Count; }  }

        public int SelectedIndex
        {
            get { return this.m_SelectedIndex; }
            set
            {
                m_SelectedIndex = value;
            }
        }

        public TabInfo GetItem(int Index)
        {
            return this.InfoList[Index];
        }

        public void DoAfterAppendTab(TabInfo Info)
        {
            if (AfterAppendTab != null)
               AfterAppendTab(this, new TabInfoEventArgs(Info));
        }

        public void DoAfterRemove(int Index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new IndexEventArgs(Index));
        }

        public void DoAfterRename(TabInfo Info)
        {
            if (AfterRename != null)
                AfterRename(this, new TabInfoEventArgs(Info));
        }

        public void AddTab(TabInfo Info)
        {
            InfoList.Add(Info);
            DoAfterAppendTab(Info);
        }

        public void InternalAdd(TabInfo Info)
        {
            InfoList.Add(Info);
        }

        public void DelTab(int Index)
        {
            InfoList.RemoveAt(Index);
            DoAfterRemove(Index);
        }

        public void RenameTab(int Index, string NewTabName)
        {
            TabInfo Info = InfoList[Index];
            Info.TabName = NewTabName;
            DoAfterRename(Info);
        }

        public string GetTabName(int i)
        {
            if (i < 0 || i > Count - 1)
                return null;
            else
                return InfoList[i].TabName;
        }

        internal void Clear()
        {
            InfoList.Clear();
        }


        public void StoreSettings()
        {
            //UpdateModelFromView();
            Settings config = Settings.GetInstance();
            config.SetIntValue(String.Format(@"{0}\SelectedIndex", m_Name), SelectedIndex);
            config.SetIntValue(String.Format(@"{0}\Count", m_Name), Count);
            for (int i = 0; i < Count; i++)
            {
                TabInfo Info = GetItem(i);
                string S = String.Format(@"{0}.{1}\", m_Name, i);
                config.SetStrValue(S + "TabName", Info.TabName);
                config.SetStrValue(S + "FilterText", Info.FilterText);
                config.SetIntValue(S + "CurrentView", (int)Info.CurrentView);
                // элементы нулевой закладки не сохраняем, т.к. они формируются после обзора сети
                config.SetListValue(S + "Items", i > 0 ? Info.Items : null);
                config.SetBoolValue(S + "AllGroups", Info.AllGroups);
                config.SetListValue(S + "Groups", Info.Groups);
            }
        }

        public void LoadSettings()
        {
            Clear();
            Settings config = Settings.GetInstance();
            int CNT = config.GetIntValue(String.Format(@"{0}\Count", m_Name), 0);
            if (CNT > 0)
            {
                for (int i = 0; i < CNT; i++)
                {
                    string S = String.Format(@"{0}.{1}\", m_Name, i);
                    string tabname = config.GetStrValue(S + "TabName", "");
                    TabInfo Info = new TabInfo(tabname);
                    Info.FilterText = config.GetStrValue(S + "FilterText", "");
                    Info.CurrentView = (View)config.GetIntValue(S + "CurrentView", (int)System.Windows.Forms.View.Details);
                    Info.Items = config.GetListValue(S + "Items");
                    Info.AllGroups = config.GetBoolValue(S + "AllGroups", false);
                    Info.Groups = config.GetListValue(S + "Groups");
                    AddTab(Info);
                }
            }
            else
            {
                string domain = Utils.GetMachineNetBiosDomain(null);
                TabInfo Info = new TabInfo(domain);
                Info.FilterText = "";
                Info.CurrentView = (System.Windows.Forms.View.Details);
                Info.Items = new List<string>();
                Info.Groups = new List<string>();
                Info.Groups.Add(domain);
                AddTab(Info);
            }
            int Index = config.GetIntValue(String.Format(@"{0}\SelectedIndex", m_Name), 0);
            // присваиваем сначала -1, чтобы всегда срабатывал евент PageSelected при установке нужной странице
            SelectedIndex = -1;
            SelectedIndex = Index;
        }

    }
}
