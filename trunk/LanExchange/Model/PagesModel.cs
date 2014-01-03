using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using LanExchange.SDK;

namespace LanExchange.Model
{
    [XmlType("LanExchangeTabs")]
    public class PagesModel : IPagesModel
    {
        private const string DEFAULT1_PANELITEMTYPE = "DomainPanelItem";
        private const string DEFAULT2_PANELITEMTYPE = "DrivePanelItem";
        private readonly List<IPanelModel> m_List;
        private int m_SelectedIndex;

        public event EventHandler<PanelModelEventArgs> AfterAppendTab;
        public event EventHandler<PanelIndexEventArgs> AfterRemove;
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
            if (index < 0 || index > m_List.Count - 1)
                return null;
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

        private void DoIndexChanged(int index)
        {
            if (IndexChanged != null)
                IndexChanged(this, new PanelIndexEventArgs(index));
        }

        public bool AddTab(IPanelModel model)
        {
            // ommit duplicates
            //if (m_List.Contains(model))
            //    return false;
            m_List.Add(model);
            if (m_SelectedIndex == -1 && m_List.Count == 1)
                m_SelectedIndex = 0;
            DoAfterAppendTab(model);
            return true;
        }

        public void DelTab(int index)
        {
            if (index >= 0 && index < m_List.Count)
            {
                var model = m_List[index];
                m_List.RemoveAt(index);
                model.Dispose();
                m_SelectedIndex = m_List.Count - 1;
                DoIndexChanged(m_SelectedIndex);
                DoAfterRemove(index);
            }
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
                {
                    var panelModel = model.GetItem(index);
                    AddTab(panelModel);
                }
                if (model.SelectedIndex != -1)
                    SelectedIndex = model.SelectedIndex;
            }
            if (Count == 0)
            {
                var root = App.PanelItemTypes.CreateDefaultRoot(DEFAULT1_PANELITEMTYPE);
                if (root == null)
                    root = App.PanelItemTypes.CreateDefaultRoot(DEFAULT2_PANELITEMTYPE);
                if (root == null) return;
                // create default tab
                var info = App.Resolve<IPanelModel>();
                info.SetDefaultRoot(root);
                info.DataType = App.PanelFillers.GetFillType(root).Name;
                AddTab(info);
            }
        }

        public void SaveSettings()
        {
            try
            {
                SerializeUtils.SerializeObjectToXMLFile(App.FolderManager.TabsConfigFileName, this, App.PanelItemTypes.ToArray());
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }

        public void Dispose()
        {
            for (int i = m_List.Count - 1; i >= 0; i--)
            {
                var model = m_List[i];
                m_List.RemoveAt(i);
                model.Dispose();
            }
        }
    }
}