using System;
using System.Text;
using System.Windows.Forms;
using LanExchange.Interface;
using LanExchange.Model;
using System.Diagnostics;
using LanExchange.Model.Panel;
using LanExchange.Utils;
using LanExchange.UI;

namespace LanExchange.Presenter
{
    public class PanelPresenter : IPresenter
    {
        public const string COMPUTER_MENU = "computer";
        public const string FOLDER_MENU = "folder";
        public const string FILE_MENU = "file";

        private PanelItemList m_Objects;
        private readonly IPanelView m_View;

        public event EventHandler CurrentPathChanged;

        public PanelPresenter(IPanelView view)
        {
            m_View = view;
        }

        public void UpdateItemsAndStatus()
        {
            if (m_Objects == null) return;
            // refresh only for current page
            PagesModel Model = AppPresenter.MainPages.GetModel();
            PanelItemList CurrentItemList = Model.GetItem(Model.SelectedIndex);
            if (CurrentItemList == null) return;
            if (!m_Objects.Equals(CurrentItemList)) return;
            // get number of visible items (filtered) and number of total items
            int ShowCount = m_Objects.FilterCount;
            int TotalCount = m_Objects.Count;
            if (m_Objects.HasBackItem())
            {
                ShowCount--;
                TotalCount--;
            }
            if (ShowCount != TotalCount)
                MainForm.Instance.ShowStatusText("Элементов: {0} из {1}", ShowCount, TotalCount);
            else
                MainForm.Instance.ShowStatusText("Элементов: {0}", ShowCount);
            m_View.SetVirtualListSize(m_Objects.FilterCount);
            if (m_Objects.FilterCount > 0)
            {
                int index = Objects.IndexOf(CurrentItemList.FocusedItemText);
                m_View.FocusedItemIndex = index;
            }
            m_View.Filter.UpdateFromModel(Objects);
        }

        private void CurrentPath_Changed(object sender, EventArgs e)
        {
            if (m_Objects != null && CurrentPathChanged != null)
                CurrentPathChanged(sender, e);
        }

        public void CommandCopyValue(int index)
        {
            if (m_Objects == null) return;
            StringBuilder S = new StringBuilder();
            foreach (int selIndex in m_View.SelectedIndices)
            {
                if (S.Length > 0)
                    S.AppendLine();
                AbstractPanelItem PItem = m_Objects.GetAt(selIndex);
                if (PItem != null)
                    S.Append(@"\\" + PItem[index]);
            }
            if (S.Length > 0)
                m_View.SetClipboardText(S.ToString());
        }

        public void CommandCopySelected()
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
                    S.Append(@"\\" + PItem[0]);
                    S.Append("\t");
                    S.Append(PItem[1]);
                }
            }
            if (S.Length > 0)
                m_View.SetClipboardText(S.ToString());
        }

        public void CommandCopyPath()
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
                m_View.SetClipboardText(S.ToString());
        }

        internal void Items_Changed(object sender, EventArgs e)
        {
            UpdateItemsAndStatus();
        }

        public PanelItemList Objects
        {
            get { return m_Objects; }
            set
            {
                if (m_Objects != null)
                    m_Objects.CurrentPath.Changed -= CurrentPath_Changed;
                m_Objects = value;
                if (m_Objects != null)
                    m_Objects.CurrentPath.Changed += CurrentPath_Changed;
                (m_View.Filter.GetPresenter() as FilterPresenter).SetModel(value);
                //m_View.SetVirtualListSize(m_Objects.Count);
            }
        }

        public AbstractPanelItem GetFocusedPanelItem(bool bPingAndAsk, bool bCanReturnParent)
        {
            var item = m_Objects.Get(m_View.FocusedItemText);
            var comp =  item as ComputerPanelItem;
            if (comp != null && bPingAndAsk)
            {
                bool bPingResult = PingThread.FastPing(comp.Name);
                if (comp.IsPingable != bPingResult)
                {
                    comp.IsPingable = bPingResult;
                    m_View.RedrawFocusedItem();
                }
                if (!bPingResult)
                {
                    DialogResult Result = MessageBox.Show(
                        String.Format("Компьютер «{0}» не доступен посредством PING.\nПродолжить?", comp.Name), "Запрос",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (Result != DialogResult.Yes)
                        item = null;
                }
            }
            if (bCanReturnParent && item != null && item.Name == AbstractPanelItem.BACK)
                item = item.Parent;
            return item;
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
            AbstractPanelItem PItem = GetFocusedPanelItem(true, false);
            if (PItem == null) return;

            string CmdLine;
            string FmtParam = null;

            switch (tagParent)
            {
                case COMPUTER_MENU:
                    if (PItem is ComputerPanelItem)
                        FmtParam = (PItem as ComputerPanelItem).Name;
                    else
                        if (PItem is SharePanelItem)
                            FmtParam = (PItem as SharePanelItem).ComputerName;
                    break;
                case FOLDER_MENU:
                    if (PItem is ComputerPanelItem)
                        return;
                    if (PItem is SharePanelItem)
                        FmtParam = String.Format(@"\\{0}\{1}", (PItem as SharePanelItem).ComputerName, (PItem as SharePanelItem).Name);
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
                m_View.ShowRunCmdError(CmdLine);
            }
        }

        /// <summary>
        /// Returns computer either focused item is computer or focused item is subitem of computer.
        /// </summary>
        /// <returns>a ComputerPanelItem or null</returns>
        public ComputerPanelItem GetFocusedComputer(bool bPingAndAsk)
        {
            var PItem = GetFocusedPanelItem(bPingAndAsk, false);
            if (PItem == null)
                return null;
            while (!(PItem is ComputerPanelItem) && (PItem.Parent != null))
                PItem = PItem.Parent;
            return PItem as ComputerPanelItem;
        }

        public bool CommandLevelDown()
        {
            var PItem = GetFocusedPanelItem(false, false);
            if (PItem == null || Objects == null) return false;
            if (PItem.Name == AbstractPanelItem.BACK)
                return CommandLevelUp();
            var result = PanelSubscription.Instance.HasStrategyForSubject(PItem);
            if (result)
            {
                LogUtils.Info("LevelDown()");
                PanelSubscription.Instance.UnSubscribe(Objects, false);
                PanelSubscription.Instance.SubscribeToSubject(Objects, PItem);
                Objects.CurrentPath.Push(PItem);
            }
            return result;
        }

        public bool CommandLevelUp()
        {
            if (Objects == null || Objects.CurrentPath.IsEmpty) return false;
            var PItem = Objects.CurrentPath.Peek() as AbstractPanelItem;
            if (PItem == null) return false;
            ISubject subject = PItem.Parent ?? PItem.ParentSubject;
            if (subject == null) return false;
            var result = PanelSubscription.Instance.HasStrategyForSubject(subject);
            if (result)
            {
                Objects.FocusedItemText = PItem.Name;
                LogUtils.Info("LevelUp()");
                Objects.CurrentPath.Pop();
                PanelSubscription.Instance.UnSubscribe(Objects, false);
                PanelSubscription.Instance.SubscribeToSubject(Objects, subject);
            }
            return result;
        }

        public static string DetectMENU(AbstractPanelItem PItem)
        {
            while (PItem != null)
            {
                if (PItem.Name == AbstractPanelItem.BACK)
                {
                    PItem = PItem.Parent;
                    continue;
                }
                if (PItem is SharePanelItem)
                    return FOLDER_MENU;
                if (PItem is ComputerPanelItem)
                    return COMPUTER_MENU;
                if (PItem is FilePanelItem)
                    if ((PItem as FilePanelItem).IsDirectory)
                        return FOLDER_MENU;
                    else
                        return FILE_MENU;
            }
            return String.Empty;
        }

        public void CommandDeleteItems()
        {
            bool Modified = false;
            foreach (int index in m_View.SelectedIndices)
            {
                var comp = m_Objects.GetAt(index);
                if (comp != null && comp.ParentSubject == ConcreteSubject.UserItems)
                {
                    m_Objects.Items.Remove(comp);
                    Modified = true;
                }
            }
            m_View.ClearSelected();
            if (Modified)
            {
                m_Objects.DataChanged(null, ConcreteSubject.UserItems);
            }
        }
    }
}
