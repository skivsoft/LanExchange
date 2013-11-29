using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using LanExchange.Intf;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.Utils;

namespace LanExchange.Model
{
    [XmlType("LanExchangeTabs")]
    public class PagesModel : IPagesModel
    {
        private const string DEFAULT_PANELITEMTYPE = "DomainPanelItem";
        private readonly List<IPanelModel> m_List;
        private int m_SelectedIndex;

        public event EventHandler<PanelModelEventArgs> AfterAppendTab;
        public event EventHandler<PanelIndexEventArgs> AfterRemove;
        public event EventHandler<PanelModelEventArgs> AfterRename;
        public event EventHandler<PanelIndexEventArgs> IndexChanged;

        public PagesModel()
        {
            m_List = new List<IPanelModel>();
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

        /// <summary>
        /// Gets or sets the items for xml serialization.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public PanelModel[] Items
        {
            get
            {
                var result = new PanelModel[m_List.Count];
                for (int index = 0; index < m_List.Count; index++)
                    result[index] = (PanelModel)m_List[index];
                return result;
            }
            set
            {
                m_List.Clear();
                foreach (var tab in value)
                    AddTab(tab);
            }
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

        public bool AddTab(IPanelModel model)
        {
            // ommit duplicates
            if (m_List.Contains(model))
                return false;
            m_List.Add(model);
            if (m_SelectedIndex == -1 && m_List.Count == 1)
                m_SelectedIndex = 0;
            DoAfterAppendTab(model);
            m_SelectedIndex = m_List.Count - 1;
            DoIndexChanged(m_SelectedIndex);
            return true;
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

        public void LoadSettings(out IPagesModel model)
        {
            var fileFName = App.FolderManager.TabsConfigFileName;
            model = null;
            if (File.Exists(fileFName))
                try
                {
                    model =
                        (PagesModel)
                        SerializeUtils.DeserializeObjectFromXMLFile(fileFName, typeof (PagesModel),
                                                                    App.PanelItemTypes.ToArray());
                }
                catch(Exception ex)
                {
                    Debug.Print(ex.Message);
                }
        }

        public void SetLoadedModel(IPagesModel model)
        {
            if (model != null && model.Count > 0)
            {
                // add loaded tabs if present
                for (int index = 0; index < model.Count; index++)
                    AddTab(model.GetItem(index));
                if (model.SelectedIndex != -1)
                    SelectedIndex = model.SelectedIndex;
            }
            if (Count == 0)
            {
                var root = App.PanelItemTypes.CreateDefaultRoot(DEFAULT_PANELITEMTYPE);
                if (root == null) return;
                // create default tab
                var info = App.Resolve<IPanelModel>();
                info.TabName = root.Name;
                info.SetDefaultRoot(root);
                info.DataType = App.PanelFillers.GetFillType(root).Name;
                AddTab(info);
            }
        }

        public void SaveSettings()
        {
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(App.FolderManager.TabsConfigFileName, this,
                                                        App.PanelItemTypes.ToArray());
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public bool TabNameExists(string tabName)
        {
            foreach (var itemList in m_List)
                if (string.Compare(itemList.TabName, tabName, StringComparison.CurrentCultureIgnoreCase) == 0)
                    return true;
            return false;
        }
    }
}