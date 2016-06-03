using System;
using System.Diagnostics.Contracts;
using LanExchange.Application.Interfaces;
using LanExchange.Application.Interfaces.EventArgs;
using LanExchange.Interfaces.Services;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;

namespace LanExchange.Application.Presenters
{
    internal sealed class PagesPresenter : PresenterBase<IPagesView>, IPagesPresenter, IDisposable
    {
        private readonly IPagesModel model;
        private readonly IPagesPersistenceService pagesService;
        private readonly IImageManager imageManager;
        private readonly IPanelColumnManager panelColumns;
        private readonly IClipboardService clipboardService;

        public event EventHandler PanelViewFocusedItemChanged;
        public event EventHandler PanelViewFilterTextChanged;

        public PagesPresenter(
            IPagesModel model, 
            IPagesPersistenceService pagesService,
            IImageManager imageManager,
            IPanelColumnManager panelColumns,
            IDisposableManager disposableManager,
            IClipboardService clipboardService)
        {
            Contract.Requires<ArgumentNullException>(model != null);
            Contract.Requires<ArgumentNullException>(pagesService != null);
            Contract.Requires<ArgumentNullException>(imageManager != null);
            Contract.Requires<ArgumentNullException>(panelColumns != null);
            Contract.Requires<ArgumentNullException>(disposableManager != null);
            Contract.Requires<ArgumentNullException>(clipboardService != null);

            this.model = model;
            this.pagesService = pagesService;
            this.imageManager = imageManager;
            this.panelColumns = panelColumns;
            this.clipboardService = clipboardService;

            disposableManager.RegisterInstance(this);
            this.model.AfterAppendTab += Model_AfterAppendTab;
            this.model.AfterRemove += Model_AfterRemove;
            this.model.IndexChanged += Model_IndexChanged;
        }

        public void Dispose()
        {
            model.Dispose();
        }

        public bool CanSendToNewTab()
        {
            var sourcePanel = View.ActivePanelView;
            if (sourcePanel == null) 
                return false;
            var indexes = sourcePanel.SelectedIndexes.GetEnumerator();
            if (!indexes.MoveNext()) 
                return false;

            return false;
            //TODO hide model
            //return sourcePanel.Presenter.Objects.Count > 1;
        }

        public void CommandSendToNewTab()
        {
            //if (!CanSendToNewTab()) return;
            //var newTabName = m_Model.GenerateTabName();
            //var sourcePV = View.ActivePanelView;
            //var sourceObjects = sourcePV.Presenter.Objects;
            //var destObjects = App.Resolve<IPanelModel>();
            //destObjects.TabName = newTabName;
            //destObjects.DataType = sourceObjects.DataType;
            ////destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);

            //foreach (int index in sourcePV.SelectedIndexes)
            //{
            //    var panelItem = sourceObjects.GetItemAt(index);
            //    if (panelItem.GetType().Name.Equals(destObjects.DataType))
            //    {
            //        // add item to new panel
            //        var newItem = (PanelItemBase) panelItem.Clone();
            //        newItem.Parent = null;
            //        destObjects.Items.Add(newItem);
            //    }
            //}
            ////destObjects.SyncRetrieveData(true);
            //// add tab
            //m_Model.AddTab(destObjects);
            ////m_View.SelectedIndex = m_Model.Count - 1;
            //View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public bool CanPasteItems()
        {
            if (View.ActivePanelView == null)
                return false;
            var obj = clipboardService.GetDataObject();
            if (obj == null)
                return false;
            if (!obj.GetDataPresent(typeof(PanelItemBaseHolder)))
                return false;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            if (items == null)
                return false;

            return false;
            //TODO hide model
            //return !View.ActivePanelView.Presenter.Objects.TabName.Equals(items.Context);
        }

        public void CommandPasteItems()
        {
            if (!CanPasteItems()) return;
            var obj = clipboardService.GetDataObject();
            if (obj == null) return;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            var destObjects = model.GetItem(model.SelectedIndex);
            destObjects.DataType = items.DataType;
            //destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);
            foreach (var panelItem in items)
                if (panelItem.GetType().Name.Equals(destObjects.DataType))
                {
                    if (destObjects.Contains(panelItem))
                        continue;
                    // add item to new panel
                    var newItem = (PanelItemBase) panelItem.Clone();
                    //newItem.Parent = PanelItemRootBase.ROOT_OF_USERITEMS;
                    destObjects.Items.Add(newItem);
                }
            destObjects.AsyncRetrieveData(true);
            //m_View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public void CommandDeleteItems()
        {
            // TODO hide model
            //var panelView = View.ActivePanelView;
            //if (panelView == null) return;
            //var indexes = panelView.SelectedIndexes.GetEnumerator();
            //if (!indexes.MoveNext()) return;


            //var modified = false;
            //var firstIndex = -1;

            //foreach (int index in panelView.SelectedIndexes)
            //{
            //    var comp = panelView.Presenter.Objects.GetItemAt(index);
            //    if (comp.ImageName.Contains(PanelImageNames.GREEN_POSTFIX) || comp.ImageName.Contains(PanelImageNames.HIDDEN_POSTFIX))
            //    {
            //        if (firstIndex == -1)
            //            firstIndex = index-1;
            //        panelView.Presenter.Objects.Items.Remove(comp);
            //        modified = true;
            //    }
            //}
            //panelView.ClearSelected();
            //if (modified)
            //{
            //    if (firstIndex < 0 || firstIndex > panelView.Presenter.Objects.FilterCount - 1)
            //        firstIndex = panelView.Presenter.Objects.FilterCount - 1;
            //    if (firstIndex >= 0)
            //        panelView.Presenter.Objects.FocusedItem = panelView.Presenter.Objects.GetItemAt(firstIndex);
            //    panelView.Presenter.Objects.AsyncRetrieveData(false);
            //}
        }

        public void CommandCloseTab()
        {
            var index = View.PopupSelectedIndex;
            model.DelTab(index);
        }

        public void CommanCloseOtherTabs()
        {
            var popupIndex = View.PopupSelectedIndex;
            for (int index = model.Count - 1; index >= 0; index--)
                if (index != popupIndex)
                    model.DelTab(index);
            model.SelectedIndex = 0;
        }

        public void CommandReRead()
        {
            var pageModel = GetItem(SelectedIndex);
            // clear refreshable columns
            if (pageModel.DataType != null)
                foreach (var column in panelColumns.GetColumns(pageModel.DataType))
                    if (column.Callback != null && column.Refreshable)
                        column.LazyDict.Clear();
            //var result = pageModel.RetrieveData(RetrieveMode.Sync, false);
            //pageModel.SetFillerResult(result, false);
            pageModel.AsyncRetrieveData(false);
        }

        public int GetPanelIndexByDataType(Type dataType)
        {
            for (int index = 0; index < Count; index++)
                if (GetItem(index).DataType.Equals(dataType.Name))
                    return index;
            return -1;
        }

        public void SetTabImageForModel(IPanelModel theModel, string imageName)
        {
            if (theModel == null) return;
            var index = IndexOf(theModel);
            if (index != -1)
                View.SetTabImage(index, imageManager.IndexOf(imageName));
        }

        public int IndexOf(IPanelModel theModel)
        {
            if (theModel == null)
                return -1;
            for (int index = 0; index < this.model.Count; index++)
                if (model.GetItem(index) == theModel)
                    return index;
            return -1;
        }

        public void Model_AfterAppendTab(object sender, PanelModelEventArgs e)
        {
            // TODO hide model
            //// create panel
            //var panelView = View.CreatePanelView(e.Info);
            //// set update event
            //IPanelPresenter presenter = panelView.Presenter;
            //presenter.Objects = e.Info;

            ////m_View.SelectedIndex = m_View.TabPagesCount - 1;
            //e.Info.Changed += (o, args) => presenter.UpdateItemsAndStatus();
            //e.Info.TabNameUpdated += InfoOnTabNameUpdated;
            //e.Info.OnTabNameUpdated();
            ////e.Info.SubscriptionChanged += Item_SubscriptionChanged;
            //// update items
            ////e.Info.DataChanged(null, ConcreteSubject.s_UserItems);
            //panelView.Presenter.ResetSortOrder();
            //e.Info.AsyncRetrieveData(false);
        }

        private void InfoOnTabNameUpdated(object sender, EventArgs eventArgs)
        {
            var model = sender as IPanelModel;
            if (model == null) return;
            var index = IndexOf(model);
            if (index != -1)
            {
                View.SetTabText(index, model.TabName);
                View.SetTabImage(index, imageManager.IndexOf(model.ImageName));
            }
        }

        public void Model_AfterRemove(object sender, PanelIndexEventArgs e)
        {
            View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelIndexEventArgs e)
        {
            var model = this.model.GetItem(e.Index);
            View.SetTabText(e.Index, model.TabName);
        }

        public void Model_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            View.SelectedIndex = e.Index;
            View.FocusPanelView();
        }

        public void DoPanelViewFocusedItemChanged(object sender, EventArgs e)
        {
            PanelViewFocusedItemChanged?.Invoke(sender, e);
        }

        public void DoPanelViewFilterTextChanged(object sender, EventArgs e)
        {
            PanelViewFilterTextChanged?.Invoke(sender, e);
        }

        public bool SelectTabByName(string tabName)
        {
            for (int index = 0; index < model.Count; index++ )
                if (model.GetTabName(index).Equals(tabName))
                {
                    SelectedIndex = index;
                    return true;
                }
            return false;
        }

        public int Count
        {
            get { return model.Count; }
        }

        public int SelectedIndex
        {
            get { return model.SelectedIndex; }
            set { model.SelectedIndex = value; }
        }

        public IPanelView ActivePanelView
        {
            get { return View.ActivePanelView; }
        }

        public void SaveInstant()
        {
            pagesService.SaveSettings(model);
        }

        public string GetTabName(int index)
        {
            return model.GetTabName(index);
        }

        public void SetupPanelViewEvents(IPanelView panelView)
        {
            panelView.FocusedItemChanged += DoPanelViewFocusedItemChanged;
            panelView.FilterTextChanged += DoPanelViewFilterTextChanged;
        }

        public IPanelModel GetItem(int index)
        {
            return model.GetItem(index);
        }

        public void LoadSettings()
        {
            IPagesModel model;
            pagesService.LoadSettings(out model);
            this.model.SetLoadedModel(model);
        }

        public bool AddTab(IPanelModel info)
        {
            return model.AddTab(info);
        }

        public void UpdateTabName(int index)
        {
            var model = this.model.GetItem(index);
            if (model != null)
                View.SetTabText(index, model.TabName);
        }

        private bool escapeDown;

        public bool PerformEscapeDown()
        {
            var panelView = View.ActivePanelView;
            if (panelView == null) return false;

            if (escapeDown)
            {
                escapeDown = false;
                return false;
            }

            // hide filter panel
            if (panelView.Filter.IsVisible)
                panelView.Filter.SetFilterText(string.Empty);

            escapeDown = true;
            return true;
        }

        public bool PerformEscapeUp()
        {
            if (!escapeDown)
                return false;

            var panelView = View.ActivePanelView;
            panelView.Presenter.CommandLevelUp();

            escapeDown = false;
            return true;
        }

        protected override void InitializePresenter()
        {
        }
    }
}