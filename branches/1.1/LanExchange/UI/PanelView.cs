using System;
using System.Drawing;
using System.Windows.Forms;
using LanExchange.View;
using LanExchange.Properties;
using LanExchange.Presenter;
using System.Diagnostics;
using NLog;
using LanExchange.Utils;
using System.Reflection;
using LanExchange.Model;
using System.Collections.Generic;

namespace LanExchange.UI
{
    public partial class PanelView : UserControl, IPanelView, IListViewItemGetter
    {
        private readonly static Logger logger = LogManager.GetCurrentClassLogger();

        private readonly PanelPresenter m_Presenter;
        private PanelItemList m_Objects;
        private readonly ListViewItemCache m_Cache;

        public event EventHandler FocusedItemChanged;
        
        public PanelView()
        {
            InitializeComponent();
            // Enable double buffer for ListView
            var mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(LV, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });
            // init presenter
            m_Presenter = new PanelPresenter(this);
            // setup items cache
            m_Cache = new ListViewItemCache(this);
            LV.CacheVirtualItems += m_Cache.CacheVirtualItems;
            LV.RetrieveVirtualItem += m_Cache.RetrieveVirtualItem;
            // set mycomputer image
            mComp.Image = LanExchangeIcons.SmallImageList.Images[LanExchangeIcons.imgCompDefault];
            mFolder.Image = LanExchangeIcons.SmallImageList.Images[LanExchangeIcons.imgFolderNormal];
        }

        /// <summary>
        /// IListViewItemGetter implementation. 
        /// This method will be called by ListViewItemCache.
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public ListViewItem GetListViewItemAt(int Index)
        {
            if (m_Objects == null)
                return null;
            if (Index < 0 || Index > Math.Min(m_Objects.Keys.Count, LV.VirtualListSize) - 1)
                return null;
            ListViewItem Result = new ListViewItem();
            string ItemName = m_Objects.Keys[Index];
            var PItem = m_Objects.Get(ItemName);
            if (PItem != null)
            {
                Result.Text = ItemName;
                string[] A = PItem.GetSubItems();
                Array.ForEach(A, str => Result.SubItems.Add(str));
                Result.ImageIndex = PItem.ImageIndex;
                Result.ToolTipText = PItem.ToolTipText;
            }
            return Result;
        }

        public ListView.ListViewItemCollection Items
        {
            get
            {
                return LV.Items;
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

        public PanelItemList Objects
        {
            get
            {
                return m_Objects;
            }
            set
            {
            	m_Objects = value;
                LV.VirtualListSize = m_Objects.Count;
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

        private void eFilter_TextChanged(object sender, EventArgs e)
        {
            //UpdateFilter(GetActiveListView(), (sender as TextBox).Text, true);
        }

        private void eFilter_KeyDown(object sender, KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
        //    {
        //        ActiveControl = GetActiveListView();
        //        ActiveControl.Focus();
        //        if (e.KeyCode == Keys.Up) SendKeys.Send("{UP}");
        //        if (e.KeyCode == Keys.Down) SendKeys.Send("{DOWN}");
        //        e.Handled = true;
        //    }
        //    if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
        //    {
        //        ActiveControl = GetActiveListView();
        //    }
        //
        }

        private void imgClear_MouseHover(object sender, EventArgs e)
        {
            imgClear.Image = Resources.clear_hover;
        }

        private void imgClear_MouseLeave(object sender, EventArgs e)
        {
            imgClear.Image = Resources.clear_normal;
        }

        private void imgClear_Click(object sender, EventArgs e)
        {
            eFilter.Text = "";
        }

        public static void SendKeysCorrect(string Keys)
        {
            const string Chars = "+^%~{}()[]";
            string NewKeys = "";
            foreach (Char Ch in Keys)
            {
                if (Chars.Contains(Ch.ToString()))
                    NewKeys += "{" + Ch.ToString() + "}";
                else
                    NewKeys = Ch.ToString();
            }
            SendKeys.Send(NewKeys);
        }

        public void lvComps_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) || Char.IsPunctuation(e.KeyChar) || PuntoSwitcher.IsValidChar(e.KeyChar))
            {
                SearchPanelVisible(true);
                ActiveControl = eFilter;
                eFilter.Focus();
                SendKeysCorrect(e.KeyChar.ToString());
                e.Handled = true;
            }
        }

        public void Items_Changed(object sender, EventArgs e)
        {
            if (m_Objects == null)
                return;
            // refresh only for current page
            PanelItemList CurrentItemList = MainPresenter.Instance.Pages.GetModel().GetItem(MainPresenter.Instance.Pages.SelectedIndex);
            if (!m_Objects.Equals(CurrentItemList))
                return;
            // get number of visible items (filtered) and number of total items
            int ShowCount, TotalCount;
            if (m_Objects.IsFiltered)
            {
                ShowCount = m_Objects.FilterCount;
                TotalCount = m_Objects.Count;
            }
            else
            {
                ShowCount = m_Objects.Count;
                TotalCount = m_Objects.Count;
            }
            //if (ShowCount != TotalCount)
            //    StatusText = String.Format("Элементов: {0} из {1}", ShowCount, TotalCount);
            //else
            //    StatusText = String.Format("Элементов: {0}", ShowCount);
            //LV.SelectedIndices.Clear();
            LV.VirtualListSize = ShowCount;
            
            /*
            if (!String.IsNullOrEmpty(ItemList.FocusedItem) && !String.IsNullOrEmpty(ItemList.FocusedItem))
            {
                LV.FocusedItem = LV.Items[ItemList.FocusedItem];
                if (LV.FocusedItem != null)
                    LV.FocusedItem.Selected = true;
            }
            */
            /*
            // update filter panel
            string Text = ItemList.FilterText;
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = Text;
            eFilter.SelectionLength = 0;
            eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            tsBottom.Visible = ItemList.IsFiltered;
            if (!tsBottom.Visible)
                Pages.SelectedTab.Refresh();
             */
        }

        void SearchPanelVisible(bool value)
        {
            //tsBottom.Visible = value;
            //if (!value)
            //    Pages.SelectedTab.Refresh();
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

        public void lvRecent_ItemActivate(object sender, EventArgs e)
        {
            logger.Info("lvRecent_ItemActivate on ", (sender as ListView).FocusedItem.ToString());
            PanelItem PItem = GetFocusedPanelItem(false, true);
            if (PItem != null)
                GotoFavoriteComp(PItem.Name);
        }

        public void lvComps_KeyDown(object sender, KeyEventArgs e)
        {
            ListView LV = (sender as ListView);
            // Ctrl+A выделение всех элементов
            if (e.Control && e.KeyCode == Keys.A)
            {
                User32Utils.SelectAllItems(LV);
                e.Handled = true;
            }
            // Shift+Enter
            if (e.Shift && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mCompOpen, new EventArgs());
                if (PItem is SharePanelItem)
                    mFolderOpen_Click(mFolderOpen, new EventArgs());
                e.Handled = true;
            }
            // Ctrl+Enter в режиме администратора
            if (MainForm.Instance.AdminMode && e.Control && e.KeyCode == Keys.Enter)
            {
                PanelItem PItem = GetFocusedPanelItem(true, false);
                if (PItem is ComputerPanelItem)
                    mCompOpen_Click(mRadmin1, new EventArgs());
                if (PItem is SharePanelItem)
                    if (!(PItem as SharePanelItem).IsPrinter)
                        mFolderOpen_Click(mFAROpen, new EventArgs());
                e.Handled = true;
            }
            // Alt+Enter
            if (MainForm.Instance.AdminMode && e.Alt && e.KeyCode == Keys.Enter)
            {
                mWMI_Click(mWMI, new EventArgs());
                e.Handled = true;
            }
            // клавишы для всех пользовательских вкладок
            if (e.KeyCode == Keys.Back)
            {
                //CompBrowser.LevelUp();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                for (int i = LV.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int Index = LV.SelectedIndices[i];
                    PanelItem Comp = m_Objects.Get(m_Objects.Keys[Index]);
                    if (Comp != null)
                        m_Objects.Delete(Comp);
                }
                LV.SelectedIndices.Clear();
                m_Objects.ApplyFilter();
                LV.VirtualListSize = m_Objects.FilterCount;
            }
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
        public void UpdateFilter(ListView LV, string NewFilter, bool bVisualUpdate)
        {
            if (LV == null)
                return;
            PanelItemList ItemList = null;// PanelItemList.GetObject(LV);
            if (ItemList == null)
                return;
            //List<string> SaveSelected = null;

            // выходим на верхний уровень
            /*
            if (!String.IsNullOrEmpty(NewFilter))
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
             */


            //string SaveCurrent = null;
            if (bVisualUpdate)
            {
                //SaveSelected = ItemList.ListView_GetSelected(LV, false);
                // запоминаем выделенные элементы
                //if (LV.FocusedItem != null)
                //  SaveCurrent = lvComps.FocusedItem.Text;
            }
            // меняем фильтр
            ItemList.FilterText = NewFilter;
            if (bVisualUpdate)
            {
                //TotalItems = CompBrowser.InternalItemList.Count;
                eFilter.BackColor = ItemList.Count > 0 ? Color.White : Color.FromArgb(255, 102, 102); // Firefox Color
                // восстанавливаем выделенные элементы
                //ItemList.ListView_SetSelected(LV, SaveSelected);
                //CompBrowser.SelectComputer(SaveCurrent);
                UpdateFilterPanel();
            }
            else
            {
                //LV.VirtualListSize = ItemList.FilterCount;
            }
        }

        public void UpdateFilterPanel()
        {
            /*
            ListView LV = GetActiveListView();
            PanelItemList ItemList = LV.GetObject();
            string Text = ItemList.FilterText;
            eFilter.TextChanged -= eFilter_TextChanged;
            eFilter.Text = Text;
            eFilter.SelectionLength = 0;
            eFilter.SelectionStart = Text.Length;
            eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            SearchPanelVisible(ItemList.IsFiltered);
            // выводим количество элементов в статус
            Items_Changed(ItemList, new EventArgs());
             */
        }

        /// <summary>
        /// Возвращает имя выбранного компьютера, предварительно проверив пингом включен ли он.
        /// </summary>
        /// <param name="bUpdateRecent">Добавлять ли комп в закладку Активность</param>
        /// <param name="bPingAndAsk">Пинговать ли комп</param>
        /// <returns>Возвращает TComputer</returns>
        public PanelItem GetFocusedPanelItem(bool bUpdateRecent, bool bPingAndAsk)
        {
            //logger.Info("GetFocusedPanelItem. {0}", LV.FocusedItem);
            if (LV.FocusedItem == null)
                return null;
            PanelItem PItem = m_Objects.Get(LV.FocusedItem.Text);
            if (PItem == null)
                return null;
            if (PItem is ComputerPanelItem)
            {
                // пингуем
                if (bPingAndAsk && (PItem is ComputerPanelItem))
                {
                    bool bPingResult = PingThread.FastPing(PItem.Name);
                    if ((PItem as ComputerPanelItem).IsPingable != bPingResult)
                    {
                        (PItem as ComputerPanelItem).IsPingable = bPingResult;
                        int FocusedIndex = LV.FocusedItem.Index;
                        LV.RedrawItems(FocusedIndex, FocusedIndex, false);
                    }
                    if (!bPingResult)
                    {
                        DialogResult Result = MessageBox.Show(
                            String.Format("Компьютер «{0}» не доступен посредством PING.\nПродолжить?", PItem.Name), "Запрос",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (Result != DialogResult.Yes)
                            PItem = null;
                    }
                }
            }
            return PItem;
        }

        private const string COMPUTER_MENU = "computer";
        private const string FOLDER_MENU = "folder";

        /// <summary>
        /// Run parametrized cmdline for focused panel item.
        /// {0} is computer name
        /// {1} is folder name
        /// </summary>
        /// <param name="TagCmd">cmdline from Tag of menu item</param>
        /// <param name="TagParent">Can be "computer" or "folder"</param>
        public void RunCmdOnFocusedItem(string TagCmd, string TagParent)
        {
            // получаем выбранный комп
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null) return;

            string CmdLine = TagCmd;
            string FmtParam = null;

            switch (TagParent)
            {
                case COMPUTER_MENU:
                    if (PItem is ComputerPanelItem)
                        FmtParam = PItem.Name;
                    else
                        if (PItem is SharePanelItem)
                            FmtParam = (PItem as SharePanelItem).ComputerName;
                    break;
                case FOLDER_MENU:
                    if (PItem is ComputerPanelItem)
                        return;
                    if (PItem is SharePanelItem)
                        FmtParam = String.Format(@"\\{0}\{1}", (PItem as SharePanelItem).ComputerName, PItem.Name);
                    break;
            }

            if (!Kernel32.Is64BitOperatingSystem())
                CmdLine = TagCmd.Replace("%ProgramFiles(x86)%", "%ProgramFiles%");
            else
                CmdLine = TagCmd;

            CmdLine = String.Format(Environment.ExpandEnvironmentVariables(CmdLine), FmtParam);
            string FName;
            string Params;
            AutorunUtils.ExplodeCmd(CmdLine, out FName, out Params);
            try
            {
                Process.Start(FName, Params);
            }
            catch
            {
                MessageBox.Show(String.Format("Не удалось выполнить команду:\n{0}", CmdLine), "Ошибка при запуске",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1);
            }
        }

        private ComputerPanelItem GetFocusedComputer()
        {
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null)
                return null;
            ComputerPanelItem Comp = null;
            int CompIndex = -1;
            if (PItem is ComputerPanelItem)
            {
                Comp = PItem as ComputerPanelItem;
                CompIndex = LV.FocusedItem.Index;
            }
            if (PItem is SharePanelItem)
            {
                Comp = new ComputerPanelItem();
                Comp.Name = (PItem as SharePanelItem).ComputerName;
            }
            return Comp;
        }

        private void mWMI_Click(object sender, EventArgs e)
        {
            ComputerPanelItem comp = GetFocusedComputer();
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
            RunCmdOnFocusedItem(MenuItem.Tag.ToString(), COMPUTER_MENU);
        }

        public void mFolderOpen_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem MenuItem = sender as ToolStripMenuItem;
            RunCmdOnFocusedItem(MenuItem.Tag.ToString(), FOLDER_MENU);
        }


        public void GotoFavoriteComp(string ComputerName)
        {
            LV.BeginUpdate();
            try
            {
                LV.SelectedIndices.Clear();
                UpdateFilter(LV, "", false);
                // выходим на верхний уровень
                /*
                while (CompBrowser.InternalStack.Count > 0)
                    CompBrowser.LevelUp();
                 */
                //!!!
                //CompBrowser.InternalItemList.ListView_SelectComputer(LV, ComputerName);
                PanelItem PItem = GetFocusedPanelItem(false, false);
                if (PItem != null && PItem.Name == ComputerName)
                    lvComps_ItemActivate(LV, new EventArgs());
            }
            finally
            {
                LV.EndUpdate();
            }
            UpdateFilterPanel();
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
            }
        }

        public void popComps_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (LV.FocusedItem != null)
                if (LV.FocusedItem.Selected)
                    DoFocusedItemChanged();
            UpdateViewTypeMenu();

            PanelItem PItem = GetFocusedPanelItem(false, false);
            bool bCompVisible = false;
            bool bFolderVisible = false;
            if (PItem != null)
            {
                if (PItem is ComputerPanelItem)
                {
                    mComp.Text = @"\\" + PItem.Name;
                    bCompVisible = MainForm.Instance.AdminMode;
                }
                if (PItem is SharePanelItem)
                {
                    mComp.Text = @"\\" + (PItem as SharePanelItem).ComputerName;
                    bCompVisible = MainForm.Instance.AdminMode;
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

        private void SetEnabledAndVisible(ToolStripItem Item, bool Value)
        {
            Item.Enabled = Value;
            Item.Visible = Value;
        }

        private void SetEnabledAndVisible(ToolStripItem[] Items, bool Value)
        {
            foreach (ToolStripItem Item in Items)
                SetEnabledAndVisible(Item, Value);
        }

        private void mLargeIcons_Click(object sender, EventArgs e)
        {
            //LV.BeginUpdate();
            try
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
            finally
            {
                //LV.EndUpdate();
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

        /// <summary>
        /// IPanelView.GetItem implementation
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public PanelItem GetItem(int Index)
        {
            return m_Objects.Get(m_Objects.Keys[Index]);
        }


        public void SelectItem(int Index)
        {
            LV.SelectedIndices.Add(Index);
        }

        private void mContextClose_Click(object sender, EventArgs e)
        {
            MainForm.Instance.IsFormVisible = false;
        }

    }
}
