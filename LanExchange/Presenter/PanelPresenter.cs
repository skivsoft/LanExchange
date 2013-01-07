using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.View;
using System.Windows.Forms;
using LanExchange.Model;
using LanExchange.UI;
using LanExchange.Utils;
using System.Diagnostics;

namespace LanExchange.Presenter
{
    public class PanelPresenter
    {
        public const string COMPUTER_MENU = "computer";
        public const string FOLDER_MENU = "folder";

        private PanelItemList m_Objects;
        private readonly IPanelView m_View;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;

        }
        public void CopyCompNameCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = GetItem(index);
                if (PItem != null)
                    S.Append(@"\\" + PItem.Name);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopyCommentCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = GetItem(index);
                if (PItem != null)
                    S.Append(PItem.Comment);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopySelectedCommand()
        {
            StringBuilder S = new StringBuilder();
            PanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = GetItem(index);
                if (PItem != null)
                {
                    S.Append(@"\\" + PItem.Name);
                    S.Append("\t");
                    S.Append(PItem.Comment);
                }
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopyPathCommand()
        {
            StringBuilder S = new StringBuilder();
            SharePanelItem PItem;
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                PItem = GetItem(index) as SharePanelItem;
                if (PItem != null)
                    S.Append(String.Format(@"\\{0}\{1}", PItem.ComputerName, PItem.Name));
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void Items_Changed(object sender, EventArgs e)
        {
            if (m_Objects == null)
                return;
            // refresh only for current page
            PagesPresenter Pages = MainPresenter.Instance.Pages;
            PanelItemList CurrentItemList = Pages.GetModel().GetItem(Pages.SelectedIndex);
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
            if (ShowCount != TotalCount)
                MainForm.Instance.ShowStatusText("Элементов: {0} из {1}", ShowCount, TotalCount);
            else
                MainForm.Instance.ShowStatusText("Элементов: {0}", ShowCount);
            m_View.SetVirtualListSize(ShowCount);

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

        public void UpdateFilter(string NewFilter, bool bVisualUpdate)
        {
            if (m_Objects == null) return;
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
            m_Objects.FilterText = NewFilter;
            if (bVisualUpdate)
            {
                //TotalItems = CompBrowser.InternalItemList.Count;
                m_View.SetIsFound(m_Objects.Count > 0);
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
            //string Text = m_Objects.FilterText;
            //eFilter.TextChanged -= eFilter_TextChanged;
            //eFilter.Text = Text;
            //eFilter.SelectionLength = 0;
            //eFilter.SelectionStart = Text.Length;
            //eFilter.TextChanged += eFilter_TextChanged;
            // показываем или скрываем панель фильтра
            m_View.FilterVisible = m_Objects.IsFiltered;
            // show count items in the current panel
            Items_Changed(m_Objects, new EventArgs());
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

        public PanelItemList Objects
        {
            get { return m_Objects; }
            set
            {
                m_Objects = value;
                m_View.SetVirtualListSize(m_Objects.Count);
            }
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
            PanelItem PItem = m_Objects.Get(m_View.FocusedItemText);
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
                        m_View.RedrawFocusedItem();
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

        public ComputerPanelItem GetFocusedComputer()
        {
            PanelItem PItem = GetFocusedPanelItem(true, true);
            if (PItem == null)
                return null;
            ComputerPanelItem Comp = null;
            int CompIndex = -1;
            if (PItem is ComputerPanelItem)
            {
                Comp = PItem as ComputerPanelItem;
                CompIndex = m_View.FocusedItemIndex;
            }
            if (PItem is SharePanelItem)
            {
                Comp = new ComputerPanelItem();
                Comp.Name = (PItem as SharePanelItem).ComputerName;
            }
            return Comp;
        }

        public void CancelCurrentFilter()
        {
            if (m_View.FilterVisible)
                m_View.FilterText = "";
            else
                MainForm.Instance.IsFormVisible = false;
        }
    }
}
