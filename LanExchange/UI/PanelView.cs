using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Presenter;
using NLog;
using LanExchange.Utils;
using System.Reflection;
using LanExchange.Model;
using System.Collections.Generic;

namespace LanExchange.UI
{
    public partial class PanelView : UserControl, IPanelView, IListViewItemGetter
    {
        #region Class declarations and constructor
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly PanelPresenter m_Presenter;
        private readonly ListViewItemCache m_Cache;

        public event EventHandler FocusedItemChanged;
        
        public PanelView()
        {
            InitializeComponent();
            // init presenters
            m_Presenter = new PanelPresenter(this);

            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(LV, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });
            // setup items cache
            m_Cache = new ListViewItemCache(this);
            LV.CacheVirtualItems += m_Cache.CacheVirtualItems;
            LV.RetrieveVirtualItem += m_Cache.RetrieveVirtualItem;
            // set mycomputer image
            mComp.Image = LanExchangeIcons.SmallImageList.Images[LanExchangeIcons.imgCompDefault];
            mFolder.Image = LanExchangeIcons.SmallImageList.Images[LanExchangeIcons.imgFolderNormal];
            // set dropdown direction for sub-menus (actual for dual-monitor system)
            mComp.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            mFolder.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
            mSendToTab.DropDownDirection = ToolStripDropDownDirection.AboveLeft;
        }
        #endregion

        #region IListViewItemGetter interface implementation
        /// <summary>
        /// IListViewItemGetter implementation. 
        /// This method will be called by ListViewItemCache.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public ListViewItem GetListViewItemAt(int Index)
        {
            if (m_Presenter.Objects == null)
                return null;
            if (Index < 0 || Index > Math.Min(m_Presenter.Objects.FilterCount, LV.VirtualListSize) - 1)
                return null;
            ListViewItem Result = new ListViewItem();
            var PItem = m_Presenter.Objects.GetAt(Index);
            if (PItem != null)
            {
                Result.Text = PItem.Name;
                string[] A = PItem.GetSubItems();
                Array.ForEach(A, str => Result.SubItems.Add(str));
                Result.ImageIndex = PItem.ImageIndex;
                Result.ToolTipText = PItem.ToolTipText;
            }
            return Result;
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

        public IEnumerable<int> SelectedIndices
        {
            get
            {
                foreach (int index in LV.SelectedIndices)
                    yield return index;
            }
        }

        public string FocusedItemText
        {
            get
            {
                return LV.FocusedItem == null ? null : LV.FocusedItem.Text;
            }
        }

        public int FocusedItemIndex
        {
            get
            {
                return LV.FocusedItem == null ? -1 : LV.FocusedItem.Index;
            }
        }

        public void SelectItem(int Index)
        {
            LV.SelectedIndices.Add(Index);
        }


        public void SetVirtualListSize(int count)
        {
            LV.VirtualListSize = count;
            if (LV.FocusedItem != null)
                LV.FocusedItem.Selected = true;
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

        public PanelPresenter GetPresenter()
        {
            return m_Presenter;
        }

        public ListView.ListViewItemCollection Items
        {
            get
            {
                return LV.Items;
            }
        }

        public ImageList SmallImageList
        {
            get
            {
                return LV.SmallImageList;
            }
            set
            {
                LV.SmallImageList = value;
            }
        }

        public ImageList LargeImageList
        {
            get
            {
                return LV.LargeImageList;
            }
            set
            {
                LV.LargeImageList = value;
            }
        }



        private void DoFocusedItemChanged()
        {
            if (FocusedItemChanged != null)
                FocusedItemChanged(this, new EventArgs());
        }

        public void lvComps_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
                DoFocusedItemChanged();
        }

        public void lvComps_KeyPress(object sender, KeyPressEventArgs e)
        {
            pFilter.GetPresenter().LinkedControl_KeyPress(sender, e);
        }

        public void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            pFilter.GetPresenter().LinkedControl_KeyDown(sender, e);
            ListView LV = (sender as ListView);
            // Ctrl+A - Select all items
            if (e.Control && e.KeyCode == Keys.A)
            {
                User32Utils.SelectAllItems(LV);
                e.Handled = true;
            }
            // Shift+Enter - Open current item
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = m_Presenter.GetFocusedPanelItem(false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mCompOpen, new EventArgs());
                if (PItem is SharePanelItem)
                    mFolderOpen_Click(mFolderOpen, new EventArgs());
                e.Handled = true;
            }
            // Ctrl+Enter - Run RAdmin for computer and FAR for folder
            if (Settings.Instance.AdvancedMode && e.Control && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = m_Presenter.GetFocusedPanelItem(false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mRadmin1, new EventArgs());
                if (PItem is SharePanelItem)
                    if (!(PItem as SharePanelItem).IsPrinter)
                        mFolderOpen_Click(mFAROpen, new EventArgs());
                e.Handled = true;
            }
            // Backspace - Go level up
            if (e.KeyCode == Keys.Back)
            {
                //CompBrowser.LevelUp();
                e.Handled = true;
            }
            // Ctrl+Ins - Copy to clipboard (similar to Ctrl+C)
            // Ctrl+Alt+C - Copy to clipboard and close window
            if (e.Control && e.KeyCode == Keys.Insert || 
                e.Control && e.Alt && e.KeyCode == Keys.C)
            {
                if (mCopyCompName.Enabled)
                    m_Presenter.CopyCompNameCommand();
                else
                    if (mCopyPath.Enabled)
                        m_Presenter.CopyPathCommand();
                if (e.KeyCode == Keys.C)
                    MainForm.Instance.Hide();
                e.Handled = true;
            }


            // TODO need delete only for user items
            //if (e.KeyCode == Keys.Delete)
            //{
            //    for (int i = LV.SelectedIndices.Count - 1; i >= 0; i--)
            //    {
            //        int Index = LV.SelectedIndices[i];
            //        PanelItem Comp = m_Objects.Get(m_Objects.Keys[Index]);
            //        if (Comp != null)
            //            m_Objects.Delete(Comp);
            //    }
            //    LV.SelectedIndices.Clear();
            //    m_Objects.ApplyFilter();
            //    LV.VirtualListSize = m_Objects.FilterCount;
            //}
        }

        private void lvComps_ItemActivate(object sender, EventArgs e)
        {
            ListView LV = (sender as ListView);
            if (LV.FocusedItem == null)
            {
                if (LV.VirtualListSize > 0)
                    LV.FocusedItem = LV.Items[0];
                else
                    return;
            }
            logger.Info("LV_ItemActivate on {0}", LV.FocusedItem.ToString());
            /*
            if (CompBrowser.InternalStack.Count == 0)
            {
                PanelItem Comp = GetFocusedPanelItem(true, true);
                if (Comp == null)
                    return;
            }
            CompBrowser.LevelDown();
             */
        }

        private void mWMI_Click(object sender, EventArgs e)
        {
            ComputerPanelItem comp = m_Presenter.GetFocusedComputer();
            if (comp == null) return;
            WMIForm form = new WMIForm(comp);
            using (Bitmap bitmap = new Bitmap(LanExchangeIcons.SmallImageList.Images[LanExchangeIcons.imgCompDefault]))
            {
                form.Icon = Icon.FromHandle(bitmap.GetHicon());
            }
            form.Show();
        }

        public void mCompOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.COMPUTER_MENU);
        }

        public void mFolderOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            m_Presenter.RunCmdOnFocusedItem(MenuItem.Tag.ToString(), PanelPresenter.FOLDER_MENU);
        }

        private void UpdateViewTypeMenu()
        {
            mCompLargeIcons.Checked = false;
            mCompSmallIcons.Checked = false;
            mCompList.Checked = false;
            mCompDetails.Checked = false;
            switch (LV.View)
            {
                case System.Windows.Forms.View.LargeIcon:
                    mCompLargeIcons.Checked = true;
                    break;
                case System.Windows.Forms.View.SmallIcon:
                    mCompSmallIcons.Checked = true;
                    break;
                case System.Windows.Forms.View.List:
                    mCompList.Checked = true;
                    break;
                case System.Windows.Forms.View.Details:
                    mCompDetails.Checked = true;
                    break;
                case System.Windows.Forms.View.Tile:
                default:
                    break;
            }
        }

        public void popComps_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LV.FocusedItem != null)
                if (LV.FocusedItem.Selected)
                    DoFocusedItemChanged();
            UpdateViewTypeMenu();

            PanelItem PItem = m_Presenter.GetFocusedPanelItem(false);
            bool bCompVisible = false;
            bool bFolderVisible = false;
            if (PItem != null)
            {
                if (PItem is ComputerPanelItem)
                {
                    mComp.Text = @"\\" + PItem.Name;
                    bCompVisible = Settings.Instance.AdvancedMode;
                }
                if (PItem is SharePanelItem)
                {
                    mComp.Text = @"\\" + (PItem as SharePanelItem).ComputerName;
                    bCompVisible = Settings.Instance.AdvancedMode;
                    if (!String.IsNullOrEmpty(PItem.Name))
                    {
                        mFolder.Text = String.Format(@"\\{0}\{1}", (PItem as SharePanelItem).ComputerName, PItem.Name);
                        if (SmallImageList != null)
                            mFolder.Image = SmallImageList.Images[PItem.ImageIndex];
                        bFolderVisible = true;
                        mFAROpen.Enabled = !(PItem as SharePanelItem).IsPrinter;
                    }
                }
            }
            SetEnabledAndVisible(mComp, bCompVisible);
            SetEnabledAndVisible(mFolder, bFolderVisible);

            bool bSenderIsComputer = true; // (Pages.SelectedIndex > 0) /*|| (CompBrowser.InternalStack.Count == 0)*/;
            SetEnabledAndVisible(new ToolStripItem[] { 
                mCopyCompName, mCopyComment, mCopySelected, 
                mSendSeparator, mSendToTab }, bSenderIsComputer);
            SetEnabledAndVisible(mCopyPath, !bSenderIsComputer);

            mSeparatorAdmin.Visible = bCompVisible || bFolderVisible;

            // resolve computer related and folder related shortcut conflict
            mCompOpen.ShowShortcutKeys = bCompVisible && !bFolderVisible;
            mRadmin1.ShowShortcutKeys = bCompVisible && !bFolderVisible;
        }

        private static void SetEnabledAndVisible(ToolStripItem Item, bool Value)
        {
            Item.Enabled = Value;
            Item.Visible = Value;
        }

        private static void SetEnabledAndVisible(ToolStripItem[] Items, bool Value)
        {
            Array.ForEach(Items, Item => SetEnabledAndVisible(Item, Value));
        }

        private void mLargeIcons_Click(object sender, EventArgs e)
        {
            int Tag;
            if (!int.TryParse((sender as ToolStripMenuItem).Tag.ToString(), out Tag))
                Tag = 0;
            switch (Tag)
            {
                case 1:
                    LV.View = System.Windows.Forms.View.LargeIcon;
                    break;
                case 2:
                    LV.View = System.Windows.Forms.View.SmallIcon;
                    break;
                case 3:
                    LV.View = System.Windows.Forms.View.List;
                    break;
                case 4:
                    LV.View = System.Windows.Forms.View.Details;
                    break;
                default:
                    break;
            }
        }

        private void mCopyCompName_Click(object sender, EventArgs e)
        {
            m_Presenter.CopyCompNameCommand();
        }

        private void mCopyComment_Click(object sender, EventArgs e)
        {
            m_Presenter.CopyCommentCommand();
        }

        private void mCopySelected_Click(object sender, EventArgs e)
        {
            m_Presenter.CopySelectedCommand();
        }

        private void mCopyPath_Click(object sender, EventArgs e)
        {
            m_Presenter.CopyPathCommand();
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
                //LV.FocusedItem = GetListViewItemAt(0);
                //LV.FocusedItem.Selected = true;
            }
            DoFocusedItemChanged();
        }
        #endregion

        private void pFilter_FilterCountChanged(object sender, EventArgs e)
        {
            m_Presenter.UpdateItemsAndStatus();
        }
    }
}
