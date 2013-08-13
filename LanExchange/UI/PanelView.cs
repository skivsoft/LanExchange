using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.Model.Settings;
using LanExchange.Presenter;
using LanExchange.SDK;
using LanExchange.Service;
using LanExchange.Utils;

namespace LanExchange.UI
{
    public partial class PanelView : UserControl, IPanelView, IListViewItemGetter
    {
        #region Class declarations and constructor
        private readonly static FormPlacement m_WMIPlacement = new FormPlacement();

        private readonly PanelPresenter m_Presenter;
        private readonly ListViewItemCache m_Cache;

        public event EventHandler FocusedItemChanged;

        public PanelView()
        {
            InitializeComponent();
            // init presenters
            m_Presenter = new PanelPresenter(this);
            m_Presenter.CurrentPathChanged += CurrentPath_Changed;

            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(LV, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });
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
            var result = new ListViewItem();
            var panelItem = m_Presenter.Objects.GetItemAt(index);
            if (panelItem != null)
            {
                var columns = AppPresenter.PanelColumns.GetColumns(m_Presenter.Objects.DataType);
                for (int i = 0; i < panelItem.CountColumns; i++)
                    if (columns[i].Visible)
                    {
                        IComparable value;
                        if ((i > 0) && (columns[i].Callback != null))
                            value = AppPresenter.LazyThreadPool.AsyncGetData(columns[i], panelItem);
                        else
                            value = panelItem[i];

                        var text = value != null ? value.ToString() : string.Empty;
                        if (i == 0)
                            result.Text = text;
                        else
                            result.SubItems.Add(text);
                    }
                result.ImageIndex = AppPresenter.Images.IndexOf(panelItem.ImageName);
                result.ToolTipText = panelItem.ToolTipText;
                result.Tag = panelItem;
            }
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

        public void SetColumnMarker(int columnIndex, SortOrder sortOrder)
        {
            NativeMethods.SetColumnImage(LV, columnIndex, sortOrder, -1);
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
            if (m_Presenter.Objects.CurrentPath.IsEmpty)
            {
                var punto = PuntoSwitcherServiceFactory.GetPuntoSwitcherService();
                if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || punto.IsValidChar(e.KeyChar))
                {
                    pFilter.FocusAndKeyPress(e);
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Shift+Enter - Open current item.
        /// </summary>
        private void OpenCurrentItem()
        {
            var panelItem = m_Presenter.GetFocusedPanelItem(false, true);
            if (panelItem != null)
            {
                if (panelItem.GetType().Name.Equals("ComputerPanelItem"))
                    mCompOpen_Click(mCompOpen, EventArgs.Empty);
                if (panelItem.GetType().Name.Equals("SharePanelItem"))
                    mFolderOpen_Click(mFolderOpen, EventArgs.Empty);
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
                if (panelItem.GetType().Name.Equals("ComputerPanelItem"))
                    mCompOpen_Click(mRadmin1, EventArgs.Empty);
                if (panelItem.GetType().Name.Equals("SharePanelItem"))
                    //if (!(panelItem as SharePanelItem).SHI.IsPrinter)
                        mFolderOpen_Click(mFAROpen, EventArgs.Empty);
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
                OpenCurrentItem();
                e.Handled = true;
            }
            // Ctrl+Enter - Run RAdmin for computer and FAR for folder
            if (Settings.Instance.General.AdvancedMode && e.Control && e.KeyCode == Keys.Enter)
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
            // Ctrl+Ins - Copy to clipboard (similar to Ctrl+C)
            // Ctrl+Alt+C - Copy to clipboard and close window
            if (e.Control && e.KeyCode == Keys.Insert || e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                if (mCopyCompName.Enabled)
                    m_Presenter.CommandCopyValue(0);
                else
                    if (mCopyPath.Enabled)
                        m_Presenter.CommandCopyPath();
                if (e.KeyCode == Keys.C)
                    MainForm.Instance.Hide();
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
            if (!m_Presenter.CommandLevelDown())
                OpenCurrentItem();
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
            var MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
                m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.COMPUTER_MENU);
        }

        private void mFolderOpen_Click(object sender, EventArgs e)
        {
            var MenuItem = sender as ToolStripMenuItem;
            if (MenuItem != null)
                m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.FOLDER_MENU);
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
            mComp.Visible = Settings.Instance.General.AdvancedMode;
            if (Settings.Instance.General.AdvancedMode && !bCompVisible)
            {
                mComp.Text = @"\\<ИмяКомпьютера>";
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

        private void popComps_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PrepareContextMenu();
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

        private void mCopyCompName_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandCopyValue(0);
        }

        private void mCopyComment_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandCopyValue(1);
        }

        private void mCopySelected_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandCopySelected();
        }

        private void mCopyPath_Click(object sender, EventArgs e)
        {
            m_Presenter.CommandCopyPath();
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

        private void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            m_Presenter.ColumnClick(e.Column);
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

        public void SetClipboardText(string value)
        {
          Clipboard.SetText(value);
        }

        public void ShowRunCmdError(string CmdLine)
        {
                MessageBox.Show(String.Format("Не удалось выполнить команду:\n{0}", CmdLine), "Ошибка при запуске",
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
    }
}
