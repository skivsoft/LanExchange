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
    public class PanelItemListEventArgs : EventArgs
    {
        private PanelItemList info;

        public PanelItemListEventArgs(PanelItemList Info)
        {
            this.info = Info;
        }

        public PanelItemList Info { get { return this.info; }}
    }

    public delegate void PanelItemListEventHandler(object sender, PanelItemListEventArgs e);

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

    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class TabModel
    {
        private List<PanelItemList> InfoList = new List<PanelItemList>();
        private string m_Name = null;
        private int m_SelectedIndex = -1;

        public event PanelItemListEventHandler AfterAppendTab;
        public event IndexEventHandler AfterRemove;
        public event PanelItemListEventHandler AfterRename;

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

        public PanelItemList GetItem(int Index)
        {
            return this.InfoList[Index];
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
            InfoList.Add(Info);
            DoAfterAppendTab(Info);
        }

        public void InternalAdd(PanelItemList Info)
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
            PanelItemList Info = InfoList[Index];
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
                PanelItemList Info = GetItem(i);
                string S = String.Format(@"{0}.{1}\", m_Name, i);
                config.SetStrValue(S + "TabName", Info.TabName);
                config.SetStrValue(S + "FilterText", Info.FilterText);
                config.SetIntValue(S + "CurrentView", (int)Info.CurrentView);
                // элементы нулевой закладки не сохраняем, т.к. они формируются после обзора сети
                //config.SetListValue(S + "Items", i > 0 ? Info.Items : null);
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
                    PanelItemList Info = new PanelItemList(tabname);
                    Info.FilterText = config.GetStrValue(S + "FilterText", "");
                    Info.CurrentView = (View)config.GetIntValue(S + "CurrentView", (int)System.Windows.Forms.View.Details);
                    //Info.Items = config.GetListValue(S + "Items");
                    Info.AllGroups = config.GetBoolValue(S + "AllGroups", false);
                    Info.Groups = config.GetListValue(S + "Groups");
                    AddTab(Info);
                }
            }
            else
            {
                string domain = Utils.GetMachineNetBiosDomain(null);
                PanelItemList Info = new PanelItemList(domain);
                Info.FilterText = "";
                Info.CurrentView = (System.Windows.Forms.View.Details);
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
