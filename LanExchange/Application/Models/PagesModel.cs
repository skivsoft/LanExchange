using System;
using System.Collections.Generic;
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

        public event EventHandler<PanelEventArgs> PanelAdded;
        public event EventHandler<PanelIndexEventArgs> PanelRemoved;
        public event EventHandler<PanelIndexEventArgs> SelectedIndexChanged;
        public event EventHandler Cleared;

        public PagesModel(
            IPanelItemFactoryManager factoryManager,
            IPanelFillerManager fillerManager,
            IModelFactory modelFactory)
        {
            if (factoryManager != null) throw new ArgumentNullException(nameof(factoryManager));
            if (fillerManager != null) throw new ArgumentNullException(nameof(fillerManager));
            if (modelFactory != null) throw new ArgumentNullException(nameof(modelFactory));

            this.factoryManager = factoryManager;
            this.panelFillers = fillerManager;
            this.modelFactory = modelFactory;

            panels = new List<IPanelModel>();
            selectedIndex = -1;
        }

        public int Count
        {
            get { return panels.Count; }
        }

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
                    NotifySelectedIndexChanged(value);
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

        private void NotifyPanelAdded(IPanelModel panel)
        {
            PanelAdded?.Invoke(this, new PanelEventArgs(panel));
        }

        private void NotifyPanelRemoved(int index)
        {
            PanelRemoved?.Invoke(this, new PanelIndexEventArgs(index));
        }

        private void NotifySelectedIndexChanged(int index)
        {
            SelectedIndexChanged?.Invoke(this, new PanelIndexEventArgs(index));
        }

        private void NotifyPanelCleared()
        {
            Cleared?.Invoke(this, EventArgs.Empty);
        }

        public bool Add(IPanelModel panel)
        {
            // ommit duplicates
            if (panels.Contains(panel))
                return false;
            panels.Add(panel);
            NotifyPanelAdded(panel);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < panels.Count)
            {
                panels.RemoveAt(index);
                NotifyPanelRemoved(index);
            }
        }

        public void Clear()
        {
            if (panels.Count > 0)
            {
                panels.Clear();
                NotifyPanelCleared();
            }
        }

        public void Assign(PagesDto dto)
        {
            foreach(var item in dto.Items)
            {
                var panel = modelFactory.CreatePanelModel();
                panel.Assign(item);
                Add(panel);
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
                Add(info);
            }
        }
    }
}