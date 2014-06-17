using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LanExchange.Helpers;
using LanExchange.Interfaces;
using LanExchange.Ioc;
using LanExchange.Plugin.WinForms.Interfaces;
using LanExchange.Plugin.WinForms.Utils;
using LanExchange.Properties;
using LanExchange.SDK;

namespace LanExchange.Plugin.WinForms.Components
{
    public partial class PanelView : UserControl, IPanelView, IListViewItemGetter, ITranslationable
    {
        #region Class declarations and constructor

        private readonly IPanelPresenter m_Presenter;
        private readonly IAddonManager m_AddonManager;
        private readonly IPanelItemFactoryManager m_FactoryManager;

        private PanelModelCopyHelper m_CopyHelper;
        private int m_SortColumn;

        public event EventHandler FocusedItemChanged;

        public PanelView(IPanelPresenter presenter, IAddonManager addonManager, IPanelItemFactoryManager factoryManager)
        {
            InitializeComponent();
            // init presenters
            m_Presenter = presenter;
            m_Presenter.View = this;

            m_AddonManager = addonManager;
            m_FactoryManager = factoryManager;


            // setup items cache
            var cache = new ListViewItemCache(this);
            //LV.CacheVirtualItems += m_Cache.CacheVirtualItems;
            LV.RetrieveVirtualItem += cache.RetrieveVirtualItem;
            // set dropdown direction for sub-menus (actual for dual-monitor system)
            //mComp.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            //mSendToNewTab.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            // focus listview when panel got focus
            GotFocus += (sender, args) => ActiveControl = LV;
            // set filter's presenter
            pFilter.Presenter = App.Resolve<IFilterPresenter>();
            pFilter.Presenter.View = pFilter;
        }
        #endregion

        public void SetVirtualListSize(int count)
        {
            LV.VirtualListSize = count;

        }

        #region IListViewItemGetter interface implementation
        /// <summary>
        /// IListViewItemGetter implementation. 
        /// This method will be called by ListViewItemCache.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [Localizable(false)]
        public ListViewItem GetListViewItemAt(int index)
        {
            if (m_Presenter.Objects == null)
                return null;
            if (index < 0 || index > Math.Min(m_Presenter.Objects.FilterCount, LV.VirtualListSize) - 1)
                return null;
            var panelItem = m_Presenter.Objects.GetItemAt(index);
            if (panelItem == null)
                return null;
            var result = new ListViewItem();
            var sb = new StringBuilder();
            if (!(panelItem is PanelItemDoubleDot))
            {
                var columns = App.PanelColumns.GetColumns(m_Presenter.Objects.DataType);
                for (int i = 0; i < panelItem.CountColumns; i++)
                {
                    IComparable value;
                    var text = string.Empty;
                    if (columns[i].Visible)
                    {
                        if ((i > 0) && (columns[i].Callback != null))
                            value = App.Threads.AsyncGetData(columns[i], panelItem);
                        else
                            value = panelItem[columns[i].Index];

                        text = value != null ? value.ToString() : string.Empty;
                        if (i == 0)
                            result.Text = text;
                        else
                            result.SubItems.Add(text);
                    } else
                        if (columns[i].Callback == null)
                        {
                            value = panelItem[columns[i].Index];
                            text = value != null ? value.ToString() : string.Empty;
                        }
                    if (i > 0 && !string.IsNullOrEmpty(text))
                    {
                        if (sb.Length > 0)
                            sb.AppendLine();
                        sb.Append(string.Format(CultureInfo.CurrentCulture, "{0}: {1}", columns[i].Text, text));
                    }
                }
            }
            result.ImageIndex = App.Images.IndexOf(panelItem.ImageName);
            result.ToolTipText = sb.ToString();
            result.Tag = panelItem;
            return result;
        }
        #endregion

        #region IPanelView interface implementation

        public IFilterView Filter
        {
            get
            {
                return pFilter;
            }
        }

        public IEnumerable<int> SelectedIndexes
        {
            get 
            {
                return LV.SelectedIndices.Cast<int>();
            }
        }

        public void ClearSelected()
        {
            LV.SelectedIndices.Clear();
        }

        public string FocusedItemText
        {
            get { return LV.FocusedItem == null ? null : LV.FocusedItem.Text; }
        }

        public PanelItemBase FocusedItem
        {
            get { return LV.FocusedItem == null ? null : LV.FocusedItem.Tag as PanelItemBase;  }
        }

        public void RedrawItem(int index)
        {
            LV.RedrawItems(index, index, true);
        }

        public int FocusedItemIndex
        {
            get
            {
                return LV.FocusedItem == null ? -1 : LV.FocusedItem.Index;
            }
            set
            {
                int index = -1;
                if (value != -1 && value < LV.VirtualListSize)
                    index = value;
                if (index == -1)
                {
                    if (LV.FocusedItem == null)
                    {
                        focusedLockCount++;
                        LV.FocusedItem = LV.Items[0];
                        focusedLockCount--;
                    }

                } else
                {
                    if (LV.FocusedItem != null)
                    {
                        LV.FocusedItem.Selected = false;
                    }
                    focusedLockCount++;
                    LV.FocusedItem = LV.Items[index];
                    focusedLockCount--;
                    LV.EnsureVisible(index);
                }
                if (LV.FocusedItem != null)
                    LV.FocusedItem.Selected = true;
            }
        }

        public void SelectItem(int index)
        {
            LV.SelectedIndices.Add(index);
        }

        public void RedrawFocusedItem()
        {
            if (LV.FocusedItem != null)
            {
                int focusedIndex = LV.FocusedItem.Index;
                LV.RedrawItems(focusedIndex, focusedIndex, false);
            }
        }

        #endregion

        #region PanelView class implementation

        public IPanelPresenter Presenter
        {
            get { return m_Presenter; }
        }

        private int focusedLockCount;


        public event EventHandler FilterTextChanged
        {
            add { pFilter.eFilter.TextChanged += value; }
            remove { pFilter.eFilter.TextChanged -= value; }
        }

        private void DoFocusedItemChanged()
        {
            //logger.Info("FocusedItemChanged: {0}", FocusedItem);
            if (focusedLockCount == 0 && LV.FocusedItem != null)
                m_Presenter.Objects.FocusedItem = LV.FocusedItem.Tag as PanelItemBase;
            if (FocusedItemChanged != null)
                FocusedItemChanged(this, EventArgs.Empty);
        }

        private void lvComps_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                DoFocusedItemChanged();
        }

        private void lvComps_KeyPress(object sender, KeyPressEventArgs e)
        {
            var punto = App.Resolve<IPuntoSwitcherService>();
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || punto.IsValidChar(e.KeyChar))
            {
                pFilter.FocusAndKeyPress(e);
                e.Handled = true;
            }
        }

        private void OpenCurrentItem()
        {
            if (m_Presenter.CommandLevelDown())
                return;
            m_AddonManager.RunDefaultCmdLine();
        }

        private void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+Down - Set focus to filter panel
            if (e.Control && e.KeyCode == Keys.Down)
            {
                if (pFilter.IsVisible)
                    pFilter.FocusMe();
                e.Handled = true;
            }
            // Ctrl+A - Select all items
            if (e.Control && e.KeyCode == Keys.A)
            {
                var listView = sender as ListView;
                if (listView != null)
                    App.Resolve<IUser32Service>().SelectAllItems(listView.Handle);
                e.Handled = true;
            }
            // Backspace - Go level up
            if (e.KeyCode == Keys.Back)
            {

                var parent = m_Presenter.Objects.CurrentPath.IsEmpty ? null : m_Presenter.Objects.CurrentPath.Peek();
                if (parent != null && !m_FactoryManager.DefaultRoots.Contains(parent))
                {
                    m_Presenter.CommandLevelUp();
                    e.Handled = true;
                }
            }
            // Ctrl+C - copy selected items
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedToClipboard(true);
                e.Handled = true;
            }
            // Ctrl+Ins - copy item name
            // Ctrl+Alt+Ins - copy full path to item name
            if (e.Control && e.KeyCode == Keys.Insert)
            {
                SetupCopyHelper();
                CopyColumnToClipboard(e.Alt ? -1 : GetCtrlInsColumnIndex());
                e.Handled = true;
            }
            // Del - Delete selected items
            if (e.KeyCode == Keys.Delete)
            {
                App.MainPages.CommandDeleteItems();
                e.Handled = true;
            }
            // process KeyDown on addons if KeyDown event not handled yet
            if (!e.Handled)
                m_AddonManager.ProcessKeyDown(e);
        }

        [Localizable(false)]
        private int GetCtrlInsColumnIndex()
        {
            foreach(var item in CreateCopyMenuItems(m_CopyHelper))
                if (item is ToolStripMenuItem)
                {
                    var menuItem = item as ToolStripMenuItem;
                    if (!string.IsNullOrEmpty(menuItem.ShortcutKeyDisplayString))
                        if (menuItem.ShortcutKeyDisplayString.Equals("Ctrl+Ins"))
                            return (int) item.Tag;
                }
            return 0;
        }

        private void lvComps_ItemActivate(object sender, EventArgs e)
        {
            m_CanDrag = false;
            OpenCurrentItem();
        }

        private void LV_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    OpenCurrentItem();
                    break;
            }
        }

        private bool m_CanDrag;

        private void LV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                var hitInfo = LV.HitTest(e.Location);
                if (hitInfo.Item != null && hitInfo.Item.Selected)
                {
                    SetupCopyHelper();
                    if (m_CopyHelper.Indexes.Count > 0)
                    {
                        m_CanDrag = true;
                    }
                }
            }
        }

        private void LV_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_CanDrag)
            {
                m_CanDrag = false;
                var obj = new DataObject();
                obj.SetText(m_CopyHelper.GetSelectedText(), TextDataFormat.UnicodeText);
                if (App.MainPages.CanSendToNewTab())
                    obj.SetData(m_CopyHelper.GetType(), m_CopyHelper);
                LV.DoDragDrop(obj, DragDropEffects.Copy);
            }
        }


        public void FocusListView()
        {
            ActiveControl = LV;
            if (LV.FocusedItem != null)
            {
                if (!LV.FocusedItem.Selected)
                    LV.FocusedItem.Selected = true;
            }
            else
            if (LV.VirtualListSize > 0)
            {
                // TODO need select item with index 0
                LV.Items[0].Selected = true;
                LV.Select();
            }
            DoFocusedItemChanged();
        }
        #endregion

        private void pFilter_FilterCountChanged(object sender, EventArgs e)
        {
            m_Presenter.UpdateItemsAndStatus();
        }

        public void SetColumnMarker(int columnIndex, PanelSortOrder sortOrder)
        {
			var service = App.Resolve<IUser32Service>();
			service.SetColumnImage(LV.Handle, columnIndex, (int)sortOrder, -1);
            //NativeMethods.SetColumnImage(LV, columnIndex, (SortOrder)sortOrder, -1);
            m_SortColumn = columnIndex;
        }

        private void ShowHideColumn_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
                m_Presenter.ShowHideColumnClick((int)menuItem.Tag);
        }

        public void ShowHeaderMenu(IList<PanelColumnHeader> columns)
        {
            var strip = new ContextMenuStrip();
            strip.RightToLeft = RightToLeft;
            for (int i = 0; i < columns.Count; i++)
            {
                var menuItem = new ToolStripMenuItem(columns[i].Text);
                menuItem.Checked = columns[i].Visible;
                menuItem.Enabled = i > 0;
                menuItem.Click += ShowHideColumn_Click;
                menuItem.Tag = i;
                strip.Items.Add(menuItem);
            }
            strip.Show(Cursor.Position);
        }

        private void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var header = LV.Columns[e.Column].Tag as PanelColumnHeader;
            if (header != null)
                m_Presenter.ColumnClick(header.Index);
        }

        public void ColumnsClear()
        {
            LV.Columns.Clear();
        }

        public void AddColumn(PanelColumnHeader header)
        {
            var column = LV.Columns.Add(header.Text, header.Width);
            column.TextAlign = (System.Windows.Forms.HorizontalAlignment)header.TextAlign;
            column.Tag = header;
        }


        public PanelViewMode ViewMode
        {
            get { return (PanelViewMode) LV.View; }
            set
            {
                LV.View = (View) value;
                LV.ToolTipActive = LV.View != View.Details;
                m_Presenter.Objects.CurrentView = value;
            }
        }

        private void LV_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            e.Cancel = !m_Presenter.ReorderColumns(e.OldDisplayIndex, e.NewDisplayIndex);
        }


        private void LV_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            m_Presenter.ColumnWidthChanged(e.ColumnIndex, LV.Columns[e.ColumnIndex].Width);
        }

        private void LV_ColumnRightClick(object sender, ColumnClickEventArgs e)
        {
            m_Presenter.ColumnRightClick(e.Column);
        }

        private void CopySelectedToClipboard(bool needSetup)
        {
            if (needSetup)
                SetupCopyHelper();
            var selectedText = m_CopyHelper.GetSelectedText();
            // put object clipboard for this app and for paste into another tab
            var obj = new DataObject();
            var items = m_CopyHelper.GetItems();
            obj.SetData(typeof(PanelItemBaseHolder), items);
            obj.SetText(selectedText, TextDataFormat.UnicodeText);
            Clipboard.SetDataObject(obj);
            // check correctness of item in clipboard
            obj = (DataObject)Clipboard.GetDataObject();
            if (obj != null)
            {
                items = (PanelItemBaseHolder)obj.GetData(typeof(PanelItemBaseHolder));
                if (items == null)
                    // put text to clipboard for external apps
                    Clipboard.SetText(selectedText, TextDataFormat.UnicodeText);
            }
        }

        private void CopyColumnToClipboard(int colIndex)
        {
            Clipboard.SetText(m_CopyHelper.GetColumnText(colIndex), TextDataFormat.UnicodeText);
        }

        private void CopySelectedOnClick(object sender, EventArgs e)
        {
            CopySelectedToClipboard(false);
        }

        private void CopyColumnOnClick(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
                CopyColumnToClipboard((int) menuItem.Tag);
        }

        /// <summary>
        /// Creates menu items depends on visible columns.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<ToolStripItem> CreateCopyMenuItems(PanelModelCopyHelper helper)
        {
            if (helper.IndexesCount == 1)
                helper.MoveTo(0);
            var result = new List<ToolStripItem>();
            var columns = App.PanelColumns.GetColumns(helper.IndexesCount == 1 ? helper.CurrentItem.GetType().Name : m_Presenter.Objects.DataType);
            var ctrlInsColumn = 0;
            foreach (var column in columns)
                if (column.Visible)
                {
                    if (column.Index == 0)
                    {
                        var valuePath = helper.GetColumnValue(-1);
                        if (helper.IndexesCount > 1 || !string.IsNullOrEmpty(valuePath))
                        {
                            var menuPath = new ToolStripMenuItem();
                            if (helper.IndexesCount == 1)
                                menuPath.Text = string.Format(CultureInfo.CurrentCulture, Resources.PanelView_CopyColumn, valuePath);
                            else
                                menuPath.Text = string.Format(CultureInfo.CurrentCulture, Resources.PanelView_CopyPathTo, column.Text);
                            menuPath.ShortcutKeyDisplayString = Resources.KeyCtrlAltIns;
                            menuPath.Tag = -1;
                            menuPath.Click += CopyColumnOnClick;
                            result.Add(menuPath);
                        }
                        result.Add(new ToolStripSeparator());
                    }
                    string value;
                    if (helper.IndexesCount == 1)
                        value = helper.GetColumnValue(column.Index);
                    else
                        value = column.Text;
                    if (!string.IsNullOrEmpty(value))
                    {
                        var menuItem = new ToolStripMenuItem(string.Format(CultureInfo.CurrentCulture, Resources.PanelView_CopyColumn, value));
                        if (column.Index == m_SortColumn)
                            ctrlInsColumn = m_SortColumn;
                        menuItem.Tag = column.Index;
                        menuItem.Click += CopyColumnOnClick;
                        result.Add(menuItem);
                    }
                }
            foreach(var menuItem in result)
                if ((menuItem is ToolStripMenuItem) && (int)menuItem.Tag == ctrlInsColumn)
                {
                    (menuItem as ToolStripMenuItem).ShortcutKeyDisplayString = Resources.KeyCtrlIns;
                    break;
                }
            return result;
        }

        private void SetupCopyHelper()
        {
            m_CopyHelper = new PanelModelCopyHelper(m_Presenter.Objects);
            foreach (int index in LV.SelectedIndices)
                m_CopyHelper.Indexes.Add(index);
            m_CopyHelper.Prepare();
        }

        internal bool PrepareContextMenu()
        {
            if (LV.FocusedItem != null)
                if (LV.FocusedItem.Selected)
                    DoFocusedItemChanged();

            var panelItem = m_Presenter.GetFocusedPanelItem(true);
            var menuVisible = false;
            if (panelItem != null)
            {
                mComp.Image = App.Images.GetSmallImage(panelItem.ImageName);
                mComp.Text = panelItem.Name;
                var typeId = panelItem.GetType().Name;
                menuVisible = m_AddonManager.BuildMenuForPanelItemType(mComp, typeId);
                if (!menuVisible)
                {
                    mComp.DropDownItems.Clear();
                    mComp.Tag = null;
                } else
                    m_AddonManager.SetupMenuForPanelItem(mComp, panelItem);
            }
            mAfterComp.Visible = panelItem != null;
            mComp.Visible = panelItem != null;
            return menuVisible;
        }

        private void popComps_Opening(object sender, CancelEventArgs e)
        {
            mComp.Enabled = PrepareContextMenu();
            SetupCopyHelper();
            mCopyMenu.Enabled = m_CopyHelper.IndexesCount > 0;
            //mSendToNewTab.Enabled = App.MainPages.CanSendToNewTab();
            mPaste.Enabled = App.MainPages.CanPasteItems();
            mDelete.Enabled = false;
            // lookup at least 1 item for delete
            for (int index = 0; index < m_CopyHelper.IndexesCount; index++)
            {
                m_CopyHelper.MoveTo(index);
                if (Presenter.Objects.Items.Contains(m_CopyHelper.CurrentItem))
                {
                    mDelete.Enabled = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Add copy-related menu items in "Copy" submenu.
        /// </summary>
        private void PrepareCopyMenu()
        {
            const int INSERT_INDEX = 1;
            // remove old items
            for (int index = mCopyMenu.DropDownItems.Count - 1; index >= INSERT_INDEX; index-- )
                {
                    var menuItem = mCopyMenu.DropDownItems[index];
                    mCopyMenu.DropDownItems.RemoveAt(index);
                    menuItem.Dispose();
                }
            // choose single or plural form for text
            if (m_CopyHelper.IndexesCount == 1)
                mCopySelected.Text = Resources.PanelView_CopySelected;
            else
                mCopySelected.Text = string.Format(CultureInfo.CurrentCulture, Resources.PanelView_CopySelectedPlural, m_CopyHelper.IndexesCount);
            // add new items
            foreach (var item in CreateCopyMenuItems(m_CopyHelper))
                mCopyMenu.DropDownItems.Add(item);
        }

        private void mCopyMenu_DropDownOpening(object sender, EventArgs e)
        {
            PrepareCopyMenu();
        }

        private void mPaste_Click(object sender, EventArgs e)
        {
            App.MainPages.CommandPasteItems();
        }

        private void mDelete_Click(object sender, EventArgs e)
        {
            App.MainPages.CommandDeleteItems();
        }



        public bool GridLines
        {
            get { return LV.GridLines; }
            set { LV.GridLines = value; }
        }

        public void TranslateUI()
        {
            TranslationUtils.TranslateComponents(Resources.ResourceManager, this, components);
            TranslationUtils.TranslateControls(Controls);
            mComp.Tag = null;
            //PrepareContextMenu();
            var panelView = App.MainPages.View.ActivePanelView;
            if (panelView == this)
                m_Presenter.UpdateItemsAndStatus();
        }

        [Localizable(false)]
        private void mNewItem_Click(object sender, EventArgs e)
        {
            App.Presenter.ExecuteAction("ActionNewItem");
        }

        private void PanelView_RightToLeftChanged(object sender, EventArgs e)
        {
            LV.RightToLeftLayout = RightToLeft == RightToLeft.Yes;
        }
    }
}
