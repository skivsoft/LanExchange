using System;
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
            if (m_Objects == null) return;
            StringBuilder S = new StringBuilder();
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                AbstractPanelItem PItem = m_Objects.GetAt(index);
                if (PItem != null)
                    S.Append(@"\\" + PItem.Name);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopyCommentCommand()
        {
            if (m_Objects == null) return;
            StringBuilder S = new StringBuilder();
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                AbstractPanelItem PItem = m_Objects.GetAt(index);
                if (PItem != null)
                    S.Append(PItem.Comment);
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void CopySelectedCommand()
        {
            if (m_Objects == null) return;
            StringBuilder S = new StringBuilder();
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                AbstractPanelItem PItem = m_Objects.GetAt(index);
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
            if (m_Objects == null) return;
            StringBuilder S = new StringBuilder();
            foreach (int index in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                SharePanelItem PItem = m_Objects.GetAt(index) as SharePanelItem;
                if (PItem != null)
                    S.Append(String.Format(@"\\{0}\{1}", PItem.ComputerName, PItem.Name));
            }
            if (S.Length > 0)
                Clipboard.SetText(S.ToString());
        }

        public void Items_Changed(object sender, EventArgs e)
        {
            UpdateItemsAndStatus();
        }

        public void UpdateItemsAndStatus()
        {
            if (m_Objects == null) return;
            // refresh only for current page
            PagesModel Model = MainForm.Instance.MainPages.GetModel();
            PanelItemList CurrentItemList = Model.GetItem(Model.SelectedIndex);
            if (CurrentItemList == null) return;
            if (!m_Objects.Equals(CurrentItemList)) return;
            // get number of visible items (filtered) and number of total items
            int ShowCount = m_Objects.FilterCount;
            int TotalCount = m_Objects.Count;
            if (ShowCount != TotalCount)
                MainForm.Instance.ShowStatusText("Элементов: {0} из {1}", ShowCount, TotalCount);
            else
                MainForm.Instance.ShowStatusText("Элементов: {0}", ShowCount);
            m_View.SetVirtualListSize(ShowCount);
        }

        public PanelItemList Objects
        {
            get { return m_Objects; }
            set
            {
                m_Objects = value;
                m_View.Filter.GetPresenter().SetModel(value);
                //m_View.SetVirtualListSize(m_Objects.Count);
            }
        }

        /// <summary>
        /// Возвращает имя выбранного компьютера, предварительно проверив пингом включен ли он.
        /// </summary>
        /// <param name="bPingAndAsk">Пинговать ли комп</param>
        /// <returns>Возвращает TComputer</returns>
        public AbstractPanelItem GetFocusedPanelItem(bool bPingAndAsk)
        {
            //logger.Info("GetFocusedPanelItem. {0}", LV.FocusedItem);
            AbstractPanelItem PItem = m_Objects.Get(m_View.FocusedItemText);
            if (PItem == null)
                return null;
            if (PItem is ComputerPanelItem)
            {
                // пингуем
                if (bPingAndAsk)
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
        /// <param name="tagCmd">cmdline from Tag of menu item</param>
        /// <param name="tagParent">Can be "computer" or "folder"</param>
        public void RunCmdOnFocusedItem(string tagCmd, string tagParent)
        {
            // получаем выбранный комп
            AbstractPanelItem PItem = GetFocusedPanelItem(true);
            if (PItem == null) return;

            string CmdLine;
            string FmtParam = null;

            switch (tagParent)
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
                CmdLine = tagCmd.Replace("%ProgramFiles(x86)%", "%ProgramFiles%");
            else
                CmdLine = tagCmd;

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
            AbstractPanelItem PItem = GetFocusedPanelItem(true);
            if (PItem == null)
                return null;
            ComputerPanelItem Comp = null;
            if (PItem is ComputerPanelItem)
            {
                Comp = PItem as ComputerPanelItem;
            }
            if (PItem is SharePanelItem)
            {
                Comp = (PItem as SharePanelItem).Computer;
            }
            return Comp;
        }

        // TODO uncomment SelectComputer
        //public void ListView_SelectComputer(ListView lv, string compName)
        //{
        //    int index = -1;
        //    if (compName != null)
        //    {

        //        index = m_Keys.IndexOf(compName);
        //        if (index == -1) index = 0;
        //    }
        //    else
        //        index = 0;
        //    if (lv.VirtualListSize > 0)
        //    {
        //        lv.SelectedIndices.Add(index);
        //        lv.FocusedItem = lv.Items[index];
        //        lv.EnsureVisible(index);
        //    }
        //}
    }
}
