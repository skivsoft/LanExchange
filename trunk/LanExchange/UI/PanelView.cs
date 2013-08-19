using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Presenter;
using LanExchange.Properties;
using LanExchange.SDK;
using LanExchange.Service;
using LanExchange.Utils;
using Settings = LanExchange.Model.Settings.Settings;

namespace LanExchange.UI
{
    public partial class PanelView : UserControl, IPanelView, IListViewItemGetter
    {
        #region Class declarations and constructor
        private readonly static FormPlacement m_WMIPlacement = new FormPlacement();

        private readonly PanelPresenter m_Presenter;
        private readonly ListViewItemCache m_Cache;
        private PanelItemsCopyHelper m_CopyHelper;

        public event EventHandler FocusedItemChanged;

        public PanelView()
        {
            InitializeComponent();
            // init presenters
            m_Presenter = new PanelPresenter(this);
            m_Presenter.CurrentPathChanged += CurrentPath_Changed;
            // setup items cache
            m_Cache = new ListViewItemCache(this);
            //LV.CacheVirtualItems += m_Cache.CacheVirtualItems;
            LV.RetrieveVirtualItem += m_Cache.RetrieveVirtualItem;
            // set mycomputer image
            mComp.Image = AppPresenter.Images.GetSmallImage(PanelImageNames.ComputerNormal);
            mFolder.Image = AppPresenter.Images.GetSmallImage(PanelImageNames.ShareNormal);
            // set dropdown direction for sub-menus (actual for dual-monitor system)
            mComp.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            mFolder.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            mSendToNewTab.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            // focus listview when panel got focus
            GotFocus += (sender, args) => ActiveControl = LV;
        }
        #endregion

        public void SetVirtualListSize(int count)
        {
            LV.VirtualListSize = count;

        }

        private void CurrentPath_Changed(object sender, EventArgs e)
        {
            var path = sender as ObjectPath<PanelItemBase>;
            if (path != null)
                ePath.Text = path.ToString();
        }

        internal void SetupMenu(ContextMenuStrip popTop)
        {
            ToolStripItem[] MyItems = new ToolStripItem[mComp.DropDownItems.Count];
            for (int i = 0; i < MyItems.Length; i++)
            {
                var TI = mComp.DropDownItems[i];
                if (TI is ToolStripSeparator)
                    MyItems[i] = new ToolStripSeparator();
                else
                    if (TI is ToolStripMenuItem)
                        MyItems[i] = MenuUtils.Clone(TI as ToolStripMenuItem);
            }
            popTop.Items.Clear();
            popTop.Items.AddRange(MyItems);
        }

        #region IListViewItemGetter interface implementation
        /// <summary>
        /// IListViewItemGetter implementation. 
        /// This method will be called by ListViewItemCache.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
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
            if (!(panelItem is PanelItemDoubleDot))
            {
                var columns = AppPresenter.PanelColumns.GetColumns(m_Presenter.Objects.DataType);
                for (int i = 0; i < panelItem.CountColumns; i++)
                    if (columns[i].Visible)
                    {
                        IComparable value;
                        if ((i > 0) && (columns[i].Callback != null))
                            value = AppPresenter.LazyThreadPool.AsyncGetData(columns[i], panelItem);
                        else
                            value = panelItem[columns[i].Index];

                        var text = value != null ? value.ToString() : string.Empty;
                        if (i == 0)
                            result.Text = text;
                        else
                            result.SubItems.Add(text);
                    }
            }
            result.ImageIndex = AppPresenter.Images.IndexOf(panelItem.ImageName);
            result.ToolTipText = panelItem.ToolTipText;
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
                foreach (int index in LV.SelectedIndices)
                    yield return index;
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
                int FocusedIndex = LV.FocusedItem.Index;
                LV.RedrawItems(FocusedIndex, FocusedIndex, false);
            }
        }

        #endregion

        #region PanelView class implementation

        public IPanelPresenter Presenter
        {
            get { return m_Presenter; }
        }

        //public ImageList SmallImageList
        //{
        //    get
        //    {
        //        return LV.SmallImageList;
        //    }
        //}

        //public ImageList LargeImageList
        //{
        //    get
        //    {
        //        return LV.LargeImageList;
        //    }
        //    set
        //    {
        //        LV.LargeImageList = value;
        //    }
        //}

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
            var punto = PuntoSwitcherServiceFactory.GetPuntoSwitcherService();
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || punto.IsValidChar(e.KeyChar))
            {
                pFilter.FocusAndKeyPress(e);
                e.Handled = true;
            }
        }

        private void OpenCurrentItem(bool canLevelDown)
        {
            if (canLevelDown)
                if (m_Presenter.CommandLevelDown())
                    return;
            var panelItem = m_Presenter.GetFocusedPanelItem(false, true);
            if (panelItem != null)
            {
                //if (panelItem.GetType().Name.Equals("ComputerPanelItem"))
                //    mCompOpen_Click(mCompOpen, EventArgs.Empty);
                //if (panelItem.GetType().Name.Equals("SharePanelItem"))
                //    mFolderOpen_Click(mFolderOpen, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Ctrl+Enter - Run RAdmin for computer and FAR for folder.
        /// </summary>
        private void RunCurrentItem()
        {
            var panelItem = m_Presenter.GetFocusedPanelItem(false, true);
            if (panelItem != null)
            {
                //if (panelItem.GetType().Name.Equals("ComputerPanelItem"))
                //    mCompOpen_Click(mRadmin1, EventArgs.Empty);
                //if (panelItem.GetType().Name.Equals("SharePanelItem"))
                //    //if (!(panelItem as SharePanelItem).SHI.IsPrinter)
                //        mFolderOpen_Click(mFAROpen, EventArgs.Empty);
            }
        }

        private void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            ListView lv = (sender as ListView);
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
                User32Utils.SelectAllItems(lv);
                e.Handled = true;
            }
            // Shift+Enter - Open current item
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                OpenCurrentItem(false);
                e.Handled = true;
            }
            // Ctrl+Enter - Run RAdmin for computer and FAR for folder
            if (Settings.Instance.AdvancedMode && e.Control && e.KeyCode == Keys.Enter)
            {
                RunCurrentItem();
                e.Handled = true;
            }
            // Backspace - Go level up
            if (e.KeyCode == Keys.Back)
            {
                m_Presenter.CommandLevelUp();
                e.Handled = true;
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
                CopyColumnToClipboard(true, e.Alt ? -1 : 0);
                e.Handled = true;
            }
            // Del - Delete selected items
            if (e.KeyCode == Keys.Delete)
            {
                m_Presenter.CommandDeleteItems();
                e.Handled = true;
            }
        }

        private void lvComps_ItemActivate(object sender, EventArgs e)
        {
            bCanDrag = false;
            OpenCurrentItem(true);
        }

        private void LV_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    OpenCurrentItem(true);
                    break;
            }
        }

        private bool bCanDrag = false;

        private void LV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                var hitInfo = LV.HitTest(e.Location);
                if (hitInfo.Item != null && hitInfo.Item.Selected)
                {
                    SetupCopyHelper();
                    if (m_CopyHelper.Count > 0)
                    {
                        bCanDrag = true;
                    }
                }
            }
        }

        private void LV_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && bCanDrag)
            {
                bCanDrag = false;
                var obj = new DataObject(DataFormats.Text, m_CopyHelper.GetSelectedText());
                LV.DoDragDrop(obj, DragDropEffects.Copy);
            }
        }

        private void mWMI_Click(object sender, EventArgs e)
        {
            // TODO UNCOMMENT THIS!
            //// check advanced mode
            //if (!Settings.Instance.AdvancedMode) return;
            //// get focused computer
            //ComputerPanelItem comp = m_Presenter.GetFocusedComputer(true) as ComputerPanelItem;
            //if (comp == null) return;
            //// create wmi form
            //WMIForm form = new WMIForm(comp);
            //// try connect to computer via wmi
            //if (!form.GetPresenter().ConnectToComputer())
            //{
            //    form.Dispose();
            //    return;
            //}
            //// asynchronous load avaible wmi classes list, if needed
            //if (!WMIClassList.Instance.Loaded)
            //{
            //    WMIClassList.Instance.IncludeClasses.Clear();
            //    foreach (string str in Settings.Instance.WMIClassesInclude)
            //        WMIClassList.Instance.IncludeClasses.Add(str);
            //    BackgroundWorkers.Instance.Add(new BackgroundContext(new WMIClassesInitStrategy()));
            //}
            //// set MyComputer icon to form
            //form.Icon = LanExchangeIcons.Instance.GetSmallIcon(PanelImageNames.ComputerNormal);
            //// display wmi form
            //m_WMIPlacement.AttachToForm(form);
            //try
            //{
            //    form.ShowDialog();
            //}
            //finally
            //{
            //    m_WMIPlacement.DetachFromForm(form);
            //    form.Dispose();
            //}
        }

        private void mCompOpen_Click(object sender, EventArgs e)
        {
            //var MenuItem = sender as ToolStripMenuItem;
            //if (MenuItem != null)
            //    m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.COMPUTER_MENU);
        }

        private void mFolderOpen_Click(object sender, EventArgs e)
        {
            //var MenuItem = sender as ToolStripMenuItem;
            //if (MenuItem != null)
            //    m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.FOLDER_MENU);
        }

        internal bool PrepareContextMenu()
        {
            if (LV.FocusedItem != null)
                if (LV.FocusedItem.Selected)
                    DoFocusedItemChanged();
            //UpdateViewTypeMenu();

            PanelItemBase PItem = m_Presenter.GetFocusedPanelItem(false, false);
            bool bCompVisible = false;
            bool bFolderVisible = false;
            if (PItem != null)
            {
                // TODO UNCOMMENT THIS!
                //if (PItem is ComputerPanelItem)
                //{
                //    var comp = PItem as ComputerPanelItem;
                //    mComp.Text = @"\\" + comp.Name;
                //    bCompVisible = Settings.Instance.AdvancedMode;
                //}
                //if (PItem is SharePanelItem)
                //{
                //    var share = PItem as SharePanelItem;
                //    mComp.Text = @"\\" + share.ComputerName;
                //    bCompVisible = Settings.Instance.AdvancedMode;
                //    if (share.Name != PanelItemBase.s_DoubleDot)
                //    {
                //        mFolder.Text = String.Format(@"\\{0}\{1}", share.ComputerName, share.Name);
                //        mFolder.Image = LanExchangeIcons.Instance.GetSmallImage(share.ImageName);
                //        bFolderVisible = true;
                //        mFAROpen.Enabled = !share.SHI.IsPrinter;
                //    }
                //}
            }
            mComp.Enabled = bCompVisible;
            mComp.Visible = Settings.Instance.AdvancedMode;
            if (Settings.Instance.AdvancedMode && !bCompVisible)
            {
                mComp.Text = Resources.PanelView_ComputerName;
            }
            //SetEnabledAndVisible(mFolder, bFolderVisible);

            //var menu = PanelPresenter.DetectMENU(PItem);
            //SetEnabledAndVisible(new ToolStripItem[] { mCopyCompName, mCopyComment, mCopySelected }, menu == PanelPresenter.COMPUTER_MENU);
            //bool bSend = false;
            //if (menu == PanelPresenter.COMPUTER_MENU)
            //    bSend = (PItem != null) && (PItem.Name != PanelItemBase.s_DoubleDot);
            //SetEnabledAndVisible(new ToolStripItem[] { mSendSeparator, mSendToNewTab }, bSend);

            //SetEnabledAndVisible(mCopyPath, menu == PanelPresenter.FOLDER_MENU);
            //mCopySeparator.Visible = menu != string.Empty;

            //mSeparatorAdmin.Visible = bCompVisible || bFolderVisible || Settings.Instance.AdvancedMode;

            // resolve computer related and folder related shortcut conflict
            mCompOpen.ShowShortcutKeys = bCompVisible && !bFolderVisible;
            mRadmin1.ShowShortcutKeys = bCompVisible && !bFolderVisible;

            return mComp.Enabled;
        }

        private static void SetEnabledAndVisible(ToolStripItem item, bool value)
        {
            item.Enabled = value;
            item.Visible = value;
            if (item is ToolStripMenuItem)
                foreach(var MI in (item as ToolStripMenuItem).DropDownItems)
                    if (MI is ToolStripMenuItem)
                        SetEnabledAndVisible((MI as ToolStripMenuItem), value);
        }

        private static void SetEnabledAndVisible(IEnumerable<ToolStripItem> items, bool value)
        {
            foreach(var item in items)
                SetEnabledAndVisible(item, value);
        }

        private void mContextClose_Click(object sender, EventArgs e)
        {
            MainForm.Instance.Hide();
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
            //TODO !!!NEED UPDATE ITEMS
            //m_Presenter.UpdateItemsAndStatus();
        }

        public void SetColumnMarker(int columnIndex, SortOrder sortOrder)
        {
            NativeMethods.SetColumnImage(LV, columnIndex, sortOrder, -1);
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

        private void ePath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C || e.Control && e.KeyCode == Keys.Insert)
            {
                if (ePath.SelectionLength == 0)
                {
                    Clipboard.SetText(ePath.Text);
                } else
                    Clipboard.SetText(ePath.SelectedText);
                e.Handled = true;
            }
        }

        private void ePath_DoubleClick(object sender, EventArgs e)
        {
            // TODO: need change path on double click
            //var P = ePath.PointToClient(MousePosition);
            //int index = ePath.GetCharIndexFromPosition(P);
            //MessageBox.Show(index.ToString());
        }

        private void mSendToNewTab_Click(object sender, EventArgs e)
        {
            AppPresenter.MainPages.CommandSendToNewTab();
        }

        public void ShowRunCmdError(string CmdLine)
        {
                MessageBox.Show(String.Format(Resources.PanelView_RunCmdErrorMsg, CmdLine), Resources.PanelView_RunCmdErrorCaption,
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
        }

        public void ColumnsClear()
        {
            LV.Columns.Clear();
        }

        public void AddColumn(PanelColumnHeader header)
        {
            var column = LV.Columns.Add(header.Text, header.Width);
            column.Tag = header;
        }


        public PanelViewMode ViewMode
        {
            get { return (PanelViewMode) LV.View; }
            set
            {
                LV.View = (View) value;
                m_Presenter.Objects.CurrentView = value;
                AppPresenter.MainPages.GetModel().SaveSettings();
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
            Clipboard.SetText(m_CopyHelper.GetSelectedText(), TextDataFormat.UnicodeText);
        }

        private void CopyColumnToClipboard(bool needSetup, int colIndex)
        {
            if (needSetup)
                SetupCopyHelper();
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
                CopyColumnToClipboard(false, (int) menuItem.Tag);
        }

        /// <summary>
        /// Creates menu items depends on visible columns.
        /// </summary>
        /// <returns></returns>
        [Localizable(false)]
        private IEnumerable<ToolStripItem> CreateCopyMenuItems(PanelItemsCopyHelper helper)
        {
            if (helper.Count == 1)
                helper.MoveTo(0);
            var columns = AppPresenter.PanelColumns.GetColumns(helper.Count == 1 ? helper.CurrentItem.GetType() : m_Presenter.Objects.DataType);
            foreach (var column in columns)
                if (column.Visible)
                {
                    if (column.Index == 0)
                    {
                        var valuePath = helper.GetColumnValue(-1);
                        if (helper.Count > 1 || !string.IsNullOrEmpty(valuePath))
                        {
                            var menuPath = new ToolStripMenuItem();
                            if (helper.Count == 1)
                                menuPath.Text = string.Format(Resources.PanelView_CopyColumn, valuePath);
                            else
                                menuPath.Text = string.Format(Resources.PanelView_CopyPathTo, column.Text);
                            menuPath.ShortcutKeyDisplayString = "Ctrl+Alt+Ins";
                            menuPath.Tag = -1;
                            menuPath.Click += CopyColumnOnClick;
                            yield return menuPath;
                        }
                        yield return new ToolStripSeparator();
                    }
                    string value;
                    if (helper.Count == 1)
                        value = helper.GetColumnValue(column.Index);
                    else
                        value = column.Text;
                    if (!string.IsNullOrEmpty(value))
                    {
                        var menuItem = new ToolStripMenuItem(string.Format(Resources.PanelView_CopyColumn, value));
                        if (column.Index == 0)
                            menuItem.ShortcutKeyDisplayString = "Ctrl+Ins";
                        menuItem.Tag = column.Index;
                        menuItem.Click += CopyColumnOnClick;
                        yield return menuItem;
                    }
                }
        }

        private void SetupCopyHelper()
        {
            m_CopyHelper = new PanelItemsCopyHelper(m_Presenter.Objects);
            foreach (int index in LV.SelectedIndices)
                m_CopyHelper.Indexes.Add(index);
            m_CopyHelper.Prepare();
        }

        private void popComps_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //PrepareContextMenu();
            // if no selected items just exit
            SetupCopyHelper();
            mCopyMenu.Enabled = m_CopyHelper.Count > 0;
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
            if (m_CopyHelper.Count == 1)
                mCopySelected.Text = Resources.PanelView_CopySelected;
            else
                mCopySelected.Text = string.Format(Resources.PanelView_CopySelectedPlural, m_CopyHelper.Count);
            // add new items
            foreach (var item in CreateCopyMenuItems(m_CopyHelper))
                mCopyMenu.DropDownItems.Add(item);
        }

        private void mCopyMenu_DropDownOpening(object sender, EventArgs e)
        {
            PrepareCopyMenu();
        }


    }
}
