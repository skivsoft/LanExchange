using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.EventArgs;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Application.Models
{
    internal sealed class PagesModel : IPagesModel
    {
        private const string DEFAULT1_PANELITEMTYPE = "DomainPanelItem";
        private const string DEFAULT2_PANELITEMTYPE = "DrivePanelItem";

        private readonly IPanelItemFactoryManager factoryManager;
        private readonly IPanelFillerManager panelFillers;
        private readonly IModelFactory modelFactory;
        private readonly List<IPanelModel> panels;
        private int selectedIndex;

        public event EventHandler<PanelModelEventArgs> AfterAppendTab;
        public event EventHandler<PanelIndexEventArgs> AfterRemove;
        public event EventHandler<PanelIndexEventArgs> IndexChanged;

        public PagesModel(
            IPanelItemFactoryManager factoryManager,
            IPanelFillerManager fillerManager,
            IModelFactory modelFactory)
        {
            Contract.Requires<ArgumentNullException>(factoryManager != null);
            Contract.Requires<ArgumentNullException>(fillerManager != null);
            Contract.Requires<ArgumentNullException>(modelFactory != null);

            this.factoryManager = factoryManager;
            this.panelFillers = fillerManager;
            this.modelFactory = modelFactory;

            panels = new List<IPanelModel>();
            selectedIndex = -1;
        }

        public int Count { get { return panels.Count; }  }

        private int lockCount;

        public int SelectedIndex 
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                if (lockCount == 0)
                {
                    lockCount++;
                    DoIndexChanged(value);
                    lockCount--;
                }
            }
        }

        public IPanelModel GetItem(int index)
        {
            if (index < 0 || index > panels.Count - 1)
                return null;
            return panels[index];
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
                var result = new PanelModel[panels.Count];
                for (int index = 0; index < panels.Count; index++)
                    result[index] = (PanelModel)panels[index];
                return result;
            }
            set
            {
                panels.Clear();
                foreach (var tab in value)
                    AddTab(tab);
            }
        }

        public int GetItemIndex(IPanelModel item)
        {
            return panels.IndexOf(item);
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
            panels.Add(model);
            if (selectedIndex == -1 && panels.Count == 1)
                selectedIndex = 0;
            DoAfterAppendTab(model);
            return true;
        }

        public void DelTab(int index)
        {
            if (index >= 0 && index < panels.Count)
            {
                var model = panels[index];
                panels.RemoveAt(index);
                selectedIndex = panels.Count - 1;
                DoIndexChanged(selectedIndex);
                DoAfterRemove(index);
            }
        }

        public string GetTabName(int index)
        {
            return index >= 0 && index <= Count - 1 ? panels[index].TabName : null;
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
                var root = factoryManager.CreateDefaultRoot(DEFAULT1_PANELITEMTYPE);
                if (root == null)
                    root = factoryManager.CreateDefaultRoot(DEFAULT2_PANELITEMTYPE);
                if (root == null) return;
                // create default tab
                var info = modelFactory.CreatePanelModel();
                info.SetDefaultRoot(root);
                info.DataType = panelFillers.GetFillType(root).Name;
                AddTab(info);
            }
        }

        public void Dispose()
        {
            for (int i = panels.Count - 1; i >= 0; i--)
            {
                var model = panels[i];
                panels.RemoveAt(i);
            }
        }
    }
}