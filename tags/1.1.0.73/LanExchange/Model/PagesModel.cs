﻿using System;
using System.Collections.Generic;
using LanExchange.Model.Panel;
using LanExchange.Utils;
using System.IO;
using NLog;
using LanExchange.Model.Settings;

namespace LanExchange.Model
{
    public class PanelItemListEventArgs : EventArgs
    {
        private readonly PanelItemList m_Info;

        public PanelItemListEventArgs(PanelItemList info)
        {
            m_Info = info;
        }

        public PanelItemList Info { get { return m_Info; } }
    }

    public class IndexEventArgs : EventArgs
    {
        private readonly int m_Index;

        public IndexEventArgs(int index)
        {
            m_Index = index;
        }

        public int Index { get { return m_Index; } }
    }

    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class PagesModel
    {
        private const string CONFIG_FNAME = "LanExchange.Tabs.cfg";
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly List<PanelItemList> m_List;
        private int m_SelectedIndex;

        public event EventHandler<PanelItemListEventArgs> AfterAppendTab;
        public event EventHandler<IndexEventArgs> AfterRemove;
        public event EventHandler<PanelItemListEventArgs> AfterRename;
        public event EventHandler<IndexEventArgs> IndexChanged;

        private Tabs m_PagesSettings;

        public PagesModel()
        {
            m_List = new List<PanelItemList>();
            m_PagesSettings = new Tabs();
            m_SelectedIndex = -1;
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

        internal int GetItemIndex(PanelItemList item)
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
                AfterRemove(this, new IndexEventArgs(index));
        }

        private void DoAfterRename(PanelItemList info)
        {
            if (AfterRename != null)
                AfterRename(this, new PanelItemListEventArgs(info));
        }

        private void DoIndexChanged(int index)
        {
            if (IndexChanged != null)
                IndexChanged(this, new IndexEventArgs(index));
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
                info.UpdateSubsctiption();
            }
        }

        public void DelTab(int index)
        {
            if (index >= 0 && index < m_List.Count)
            {
                PanelSubscription.Instance.UnSubscribe(m_List[index], true);
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

        private static string GetConfigFileName()
        {
            var path = Path.GetDirectoryName(Settings.Settings.GetExecutableFileName());
            if (path == null)
                throw new ArgumentNullException();
            return Path.Combine(path, CONFIG_FNAME);
        }

        public void LoadSettings()
        {
            try
            {
                var fileFName = GetConfigFileName();
                logger.Info("PagesModel.LoadSettings(\"{0}\")", fileFName);
                var temp = (Tabs)SerializeUtils.DeserializeObjectFromXMLFile(fileFName, typeof (Tabs));
                if (temp != null)
                {
                    m_PagesSettings = null;
                    m_PagesSettings = temp;
                }
            }
            catch(Exception E)
            {
                logger.Error("PagesModel.LoadSettings: {0}", E.Message);
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
                var domain = NetApi32Utils.GetMachineNetBiosDomain(null);
                var Info = new PanelItemList(domain);
                Info.Groups.Add(new DomainPanelItem(domain));
                AddTab(Info);
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
                logger.Info("PagesModel.SaveSettings(\"{0}\")", fileFName);
                SerializeUtils.SerializeObjectToXMLFile(fileFName, m_PagesSettings);
            }
            catch (Exception E)
            {
                logger.Error("PagesModel.SaveSettings: {0}", E.Message);
            }
        }
    }
}