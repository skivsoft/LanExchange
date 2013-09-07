using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using LanExchange.Intf;
using LanExchange.Model.Settings;
using LanExchange.Properties;
using LanExchange.Utils;

namespace LanExchange.Model
{
    // 
    // Модель предоставляет знания: данные и методы работы с этими данными, 
    // реагирует на запросы, изменяя своё состояние. 
    // Не содержит информации, как эти знания можно визуализировать.
    // 

    public class PagesModel : IPagesModel
    {
        private readonly List<IPanelModel> m_List;
        private int m_SelectedIndex;

        public event EventHandler<PanelModelEventArgs> AfterAppendTab;
        public event EventHandler<PanelIndexEventArgs> AfterRemove;
        public event EventHandler<PanelModelEventArgs> AfterRename;
        public event EventHandler<PanelIndexEventArgs> IndexChanged;

        private Tabs m_PagesSettings;

        public PagesModel()
        {
            m_List = new List<IPanelModel>();
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

        public IPanelModel GetItem(int index)
        {
            return m_List[index];
        }

        public int GetItemIndex(IPanelModel item)
        {
            return m_List.IndexOf(item);
        }

        private void DoAfterAppendTab(IPanelModel info)
        {
            if (AfterAppendTab != null)
                AfterAppendTab(this, new PanelModelEventArgs(info));
        }

        private void DoAfterRemove(int index)
        {
            if (AfterRemove != null)
                AfterRemove(this, new PanelIndexEventArgs(index));
        }

        private void DoAfterRename(IPanelModel info)
        {
            if (AfterRename != null)
                AfterRename(this, new PanelModelEventArgs(info));
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
        
        public void AddTab(IPanelModel info)
        {
            // ommit duplicates
            if (!m_List.Contains(info))
            {
                m_List.Add(info);
                if (m_SelectedIndex == -1 && m_List.Count == 1)
                    m_SelectedIndex = 0;
                DoAfterAppendTab(info);
                m_SelectedIndex = m_List.Count - 1;
                DoIndexChanged(m_SelectedIndex);
            }
        }

        public void DelTab(int index)
        {
            if (index >= 0 && index < m_List.Count)
            {
                m_List.RemoveAt(index);
                m_SelectedIndex = m_List.Count - 1;
                DoIndexChanged(m_SelectedIndex);
                DoAfterRemove(index);
            }
        }

        public void RenameTab(int index, string newTabName)
        {
            var info = m_List[index];
            info.TabName = newTabName;
            DoAfterRename(info);
        }

        public string GetTabName(int index)
        {
            return index >= 0 && index <= Count - 1 ? m_List[index].TabName : null;
        }

        public void LoadSettings()
        {
            var fileFName = App.FolderManager.TabsConfigFileName;
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
                catch (Exception ex)
                {
                    Debug.Print(ex.Message);
                }
            }
            if (m_PagesSettings == null)
                throw new ArgumentNullException();
            //if (m_PagesSettings.Items.Length > 0)
            //{
            //    Array.ForEach(m_PagesSettings.Items, page =>
            //    {
            //        var Info = new PanelItemList(page.Name) { Settings = page };
            //        AddTab(Info);
            //    });
            //}
            //else
            {
                App.PanelItemTypes.CreateDefaultRoots();
                foreach (var root in App.PanelItemTypes.DefaultRoots)
                {
                    var info = App.Resolve<IPanelModel>();
                    info.TabName = root.Name;
                    info.SetDefaultRoot(root);
                    //info.FocusedItem = SystemInformation.ComputerName;
                    AddTab(info);
                }
            }
            if (m_PagesSettings.SelectedIndex != -1)
                SelectedIndex = m_PagesSettings.SelectedIndex;
        }

        public void SaveSettings()
        {
            //TODO !!!UNCOMMENT SAVE SETTINGS
            //m_PagesSettings.SelectedIndex = SelectedIndex;
            //var pages = new List<Tab>();
            //for (int i = 0; i < Count; i++)
            //    pages.Add(GetItem(i).Settings);
            //m_PagesSettings.Items = pages.ToArray();
            //var fileFName = GetConfigFileName();
            //try
            //{
            //    Type[] extraTypes = AppPresenter.PanelItemTypes.ToArray();
            //    SerializeUtils.SerializeObjectToXMLFile(fileFName, m_PagesSettings, extraTypes);
            //}
            //catch (Exception e)
            //{
            //    Debug.Fail(e.Message);
            //}
        }

        public bool TabNameExists(string tabName)
        {
            foreach (var itemList in m_List)
                if (string.Compare(itemList.TabName, tabName, StringComparison.CurrentCultureIgnoreCase) == 0)
                    return true;
            return false;
        }

        public string GenerateTabName()
        {
            if (m_List.Count == 0)
                return string.Empty;
            var itemList = m_List[m_SelectedIndex];
            string result;
            var index = 0;
            bool exists;
            do
            {
                if (index == 0)
                    result = string.Format(Resources.PagesModel_CopyOf, itemList.TabName);
                else
                    result = string.Format(Resources.PagesModel_CopyOfMany, index, itemList.TabName);
                exists = TabNameExists(result);
                ++index;
            } while (exists);
            return result;
        }
    }
}
