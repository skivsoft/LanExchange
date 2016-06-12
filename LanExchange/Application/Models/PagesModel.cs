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

        public event EventHandler<PanelEventArgs> AppendPanel;
        public event EventHandler<PanelIndexEventArgs> RemovePanel;
        public event EventHandler<PanelIndexEventArgs> SelectedIndexChanged;

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

        public IPanelModel GetAt(int index)
        {
            if (index < 0 || index > panels.Count - 1)
                return null;
            return panels[index];
        }

        private void DoAfterAppendTab(IPanelModel info)
        {
            AppendPanel?.Invoke(this, new PanelEventArgs(info));
        }

        private void DoAfterRemove(int index)
        {
            RemovePanel?.Invoke(this, new PanelIndexEventArgs(index));
        }

        private void DoIndexChanged(int index)
        {
            SelectedIndexChanged?.Invoke(this, new PanelIndexEventArgs(index));
        }

        public bool Append(IPanelModel model)
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

        public void RemoveAt(int index)
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

        public void Assign(PagesDto dto)
        {
            foreach(var item in dto.Items)
            {
                var panel = modelFactory.CreatePanelModel();
                panel.Assign(item);
                Append(panel);
            }
            if (dto.SelectedIndex != -1)
                SelectedIndex = dto.SelectedIndex;

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
                Append(info);
            }
        }
    }
}