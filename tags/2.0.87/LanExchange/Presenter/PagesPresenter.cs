using System;
using System.ComponentModel;
using System.Windows.Forms;
using LanExchange.Core;
using LanExchange.Intf;
using LanExchange.Model;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public class PagesPresenter : PresenterBase<IPagesView>, IPagesPresenter
    {
        private readonly IPagesModel m_Model;

        public event EventHandler PanelViewFocusedItemChanged;
        public event EventHandler PanelViewFilterTextChanged;

        public PagesPresenter(IPagesModel model)
        {
            m_Model = model;
            m_Model.AfterAppendTab += Model_AfterAppendTab;
            m_Model.AfterRemove += Model_AfterRemove;
            m_Model.AfterRename += Model_AfterRename;
            m_Model.IndexChanged += Model_IndexChanged;
        }

        private void CheckDuplicateOnNew(object sender, CancelEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            var form = control.Parent as InputBoxForm;
            if (form == null) return;
            if (m_Model.TabNameExists(form.Value))
            {
                form.SetError(Resources.InputBoxPresenter_TabNameAlreadyExists);
                e.Cancel = true;
            }
        }

        private void CheckDuplicateOnRename(object sender, CancelEventArgs e)
        {
            var control = sender as Control;
            if (control == null) return;
            var form = control.Parent as InputBoxForm;
            if (form == null) return;
            var index = View.PopupSelectedIndex;
            var itemList = m_Model.GetItem(index);
            if (itemList == null) return;
            if ((string.Compare(form.Value, itemList.TabName, StringComparison.CurrentCultureIgnoreCase) != 0) && 
                m_Model.TabNameExists(form.Value))
            {
                form.SetError(Resources.InputBoxPresenter_TabNameAlreadyExists);
                e.Cancel = true;
            }
        }

        public void CommandNewTab()
        {
            using (
                var form = InputBoxForm.CreateAskForm(Resources.PagesPresenter_NewTab,
                                                      Resources.PagesPresenter_EnterTabName, ""))
            {
                form.InputValidating += InputBoxForm.ValidatingEmpty;
                form.InputValidating += CheckDuplicateOnNew;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var panel = App.Resolve<IPanelModel>();
                    panel.TabName = form.Value;
                    m_Model.AddTab(panel);
                    m_Model.SaveSettings();
                }
            }
        }

        public void CommandProperties()
        {
            var index = View.PopupSelectedIndex;
            var itemList = m_Model.GetItem(index);
            if (itemList == null) return;
            using (var form = InputBoxForm.CreateAskForm(Resources.PagesPresenter_TabProperties,
                                                         Resources.PagesPresenter_TabName, itemList.TabName))
            {
                form.InputValidating += InputBoxForm.ValidatingEmpty;
                form.InputValidating += CheckDuplicateOnRename;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    m_Model.RenameTab(index, form.Value);
                    m_Model.SaveSettings();
                }
            }
        }

        public bool CanSendToNewTab()
        {
            var sourcePV = View.ActivePanelView;
            if (sourcePV == null) 
                return false;
            var indexes = sourcePV.SelectedIndexes.GetEnumerator();
            if (!indexes.MoveNext()) 
                return false;
            return sourcePV.Presenter.Objects.Count > 1;
        }

        public void CommandSendToNewTab()
        {
            if (!CanSendToNewTab()) return;
            var newTabName = m_Model.GenerateTabName();
            var sourcePV = View.ActivePanelView;
            var sourceObjects = sourcePV.Presenter.Objects;
            var destObjects = App.Resolve<IPanelModel>();
            destObjects.TabName = newTabName;
            destObjects.DataType = sourceObjects.DataType;
            destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);

            foreach (int index in sourcePV.SelectedIndexes)
            {
                var panelItem = sourceObjects.GetItemAt(index);
                if (panelItem.GetType() == destObjects.DataType)
                {
                    // add item to new panel
                    var newItem = (PanelItemBase) panelItem.Clone();
                    newItem.Parent = PanelItemRoot.ROOT_OF_USERITEMS;
                    destObjects.Items.Add(newItem);
                }
            }
            //destObjects.SyncRetrieveData(true);
            // add tab
            m_Model.AddTab(destObjects);
            //m_View.SelectedIndex = m_Model.Count - 1;
            m_Model.SaveSettings();
            View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public bool CanPasteItems()
        {
            if (View.ActivePanelView == null)
                return false;
            var obj = Clipboard.GetDataObject();
            if (obj == null)
                return false;
            if (!obj.GetDataPresent(typeof(PanelItemBaseHolder)))
                return false;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            if (items == null)
                return false;
            return !View.ActivePanelView.Presenter.Objects.TabName.Equals(items.Context);
        }

        public void CommandPasteItems()
        {
            if (!CanPasteItems()) return;
            var obj = Clipboard.GetDataObject();
            if (obj == null) return;
            var items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
            var destObjects = m_Model.GetItem(m_Model.SelectedIndex);
            destObjects.DataType = items.DataType;
            //destObjects.CurrentPath.Push(PanelItemRoot.ROOT_OF_USERITEMS);
            foreach (var panelItem in items)
                if (panelItem.GetType() == destObjects.DataType)
                {
                    if (destObjects.Contains(panelItem))
                        continue;
                    // add item to new panel
                    var newItem = (PanelItemBase) panelItem.Clone();
                    newItem.Parent = PanelItemRoot.ROOT_OF_USERITEMS;
                    destObjects.Items.Add(newItem);
                }
            m_Model.SaveSettings();
            destObjects.SyncRetrieveData(true);
            //m_View.ActivePanelView.Presenter.UpdateItemsAndStatus();
        }

        public void CommandDeleteItems()
        {
            var panelView = View.ActivePanelView;
            if (panelView == null) return;
            var indexes = panelView.SelectedIndexes.GetEnumerator();
            if (!indexes.MoveNext()) return;
            var modified = false;
            var firstIndex = -1;
            foreach (int index in panelView.SelectedIndexes)
            {
                var comp = panelView.Presenter.Objects.GetItemAt(index);
                if (comp.Parent == PanelItemRoot.ROOT_OF_USERITEMS)
                {
                    if (firstIndex == -1)
                        firstIndex = index-1;
                    panelView.Presenter.Objects.Items.Remove(comp);
                    modified = true;
                }
            }
            panelView.ClearSelected();
            if (modified)
            {
                if (firstIndex < 0 || firstIndex > panelView.Presenter.Objects.FilterCount - 1)
                    firstIndex = panelView.Presenter.Objects.FilterCount - 1;
                if (firstIndex >= 0)
                    panelView.Presenter.Objects.FocusedItem = panelView.Presenter.Objects.GetItemAt(firstIndex);
                panelView.Presenter.Objects.SyncRetrieveData();
                m_Model.SaveSettings();
            }
        }

        public void CommandCloseTab()
        {
            var index = View.PopupSelectedIndex;
            if (CanCloseTab())
            {
                m_Model.DelTab(index);
                m_Model.SaveSettings();
            }
        }

        public bool CanCloseTab()
        {
            return true;
        }

        public void Model_AfterAppendTab(object sender, PanelModelEventArgs e)
        {
            // create panel
            var panelView = View.CreatePanelView(e.Info);
            // set update event
            IPanelPresenter presenter = panelView.Presenter;
            presenter.Objects = e.Info;
            //m_View.SelectedIndex = m_View.TabPagesCount - 1;
            e.Info.Changed += (o, args) => presenter.UpdateItemsAndStatus();
            //e.Info.SubscriptionChanged += Item_SubscriptionChanged;
            // update items
            //e.Info.DataChanged(null, ConcreteSubject.s_UserItems);
            panelView.Presenter.ResetSortOrder();
            e.Info.SyncRetrieveData();
        }

        public void Model_AfterRemove(object sender, PanelIndexEventArgs e)
        {
            View.RemoveTabAt(e.Index);
        }

        public void Model_AfterRename(object sender, PanelModelEventArgs e)
        {
            View.SelectedTabText = e.Info.TabName;
        }

        public void Model_IndexChanged(object sender, PanelIndexEventArgs e)
        {
            View.SelectedIndex = e.Index;
            View.FocusPanelView();
        }

        public void DoPanelViewFocusedItemChanged(object sender, EventArgs e)
        {
            if (PanelViewFocusedItemChanged != null)
                PanelViewFocusedItemChanged(sender, e);
        }

        public void DoPanelViewFilterTextChanged(object sender, EventArgs e)
        {
            if (PanelViewFilterTextChanged != null)
                PanelViewFilterTextChanged(sender, e);
        }

        public int Count
        {
            get { return m_Model.Count; }
        }

        public int SelectedIndex
        {
            get { return m_Model.SelectedIndex; }
            set { m_Model.SelectedIndex = value; }
        }

        public void SaveSettings()
        {
            m_Model.SaveSettings();
        }


        public string GetTabName(int index)
        {
            return m_Model.GetTabName(index);
        }


        public void SetupPanelViewEvents(IPanelView PV)
        {
            PV.FocusedItemChanged += DoPanelViewFocusedItemChanged;
            PV.FilterTextChanged += DoPanelViewFilterTextChanged;
        }


        public IPanelModel GetItem(int index)
        {
            return m_Model.GetItem(index);
        }

        public void LoadSettings()
        {
            m_Model.LoadSettings();
        }


        public void AddTab(IPanelModel info)
        {
            m_Model.AddTab(info);
        }
    }

}
