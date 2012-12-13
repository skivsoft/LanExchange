using System;
using System.Windows.Forms;
using System.Drawing;
using LanExchange.Forms;
using System.Reflection;

namespace LanExchange
{
    // Представление отвечает за отображение информации (визуализация). 
    // Часто в качестве представления выступает
    public class TabView
    {
        private readonly static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly TabControl m_Pages;

        public TabView(TabControl Pages)
        {
            m_Pages = Pages;
        }

        public string Name { get { return m_Pages.Name; }}

        public int SelectedIndex 
        { 
            get { return m_Pages.SelectedIndex; }
            set { m_Pages.SelectedIndex = value; }
        }

        public TabControl.TabPageCollection TabPages { get { return m_Pages.TabPages; } }

        public string SelectedTabText 
        { 
            get 
            {
                TabPage Tab = m_Pages.SelectedTab;
                return Tab != null ? m_Pages.SelectedTab.Text : ""; 
            }
        }

        public void AfterAppendTab(object sender, LanExchange.TabModel.PanelItemListEventArgs e)
        {
            logger.Info("AfterAppendTab()");
            TabModel Model = (TabModel)sender;
            // init controls
            TabPage NewTab = new TabPage();
            ListView LV = new ListView();
            // suspend layouts
            //Instance.SuspendLayout();
            //m_Pages.SuspendLayout();
            //NewTab.SuspendLayout();
            //
            // m_Pages
            //
            m_Pages.Controls.Add(NewTab);
            //
            // LV
            //
            logger.Info("Setup control {0}", LV.ToString());
            MethodInfo mi = typeof(Control).GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(LV, new object[] { ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true });
            LV.Dock = DockStyle.Fill;
            LV.Location = new Point(3, 3);
            LV.Size = new Size(NewTab.Size.Width - 6, NewTab.Size.Height - 6);
            LV.View = e.Info.CurrentView;
            LV.Columns.Add("Сетевое имя", 130);
            LV.Columns.Add("Описание", 250);
            LV.ContextMenuStrip = MainForm.Instance.popComps;
            LV.FullRowSelect = true;
            LV.GridLines = true;
            LV.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            LV.HideSelection = false;
            LV.LargeImageList = MainForm.Instance.ilLarge;
            LV.ShowGroups = false;
            LV.ShowItemToolTips = true;
            LV.SmallImageList = MainForm.Instance.ilSmall;
            LV.VirtualMode = true;
            // events
            LV.ItemActivate += MainForm.Instance.lvRecent_ItemActivate;
            LV.RetrieveVirtualItem += MainForm.Instance.lvComps_RetrieveVirtualItem;
            LV.KeyPress += MainForm.Instance.lvComps_KeyPress;
            LV.KeyDown += MainForm.Instance.lvComps_KeyDown;
            LV.ItemSelectionChanged += MainForm.Instance.lvComps_ItemSelectionChanged;
            LV.SelectedIndexChanged += MainForm.Instance.lvComps_SelectedIndexChanged;
            //LV.CacheVirtualItems += MainForm.Instance.lvComps_CacheVirtualItems;
            LV.RetrieveVirtualItem += MainForm.Instance.lvComps_RetrieveVirtualItem;
            e.Info.AttachObjectTo(LV);
            e.Info.Changed += MainForm.Instance.Items_Changed;
            e.Info.UpdateSubsctiption();
            //
            // NewTab
            //
            NewTab.Controls.Add(LV);
            NewTab.Text = e.Info.TabName;
            NewTab.UseVisualStyleBackColor = true;
            // resume layouts
            MainForm.Instance.tipComps.SetToolTip(LV, " ");
            //NewTab.ResumeLayout(false);
            //m_Pages.ResumeLayout(false);
            //Instance.ResumeLayout(false);
            //Instance.PerformLayout();
            // update filter
            //MainForm.GetInstance().UpdateFilter(LV, e.Info.FilterText, false);
            Model.SelectedIndex = Model.Count - 1;
        }

        public void AfterRemove(object sender, LanExchange.TabModel.IndexEventArgs e)
        {
            m_Pages.TabPages.RemoveAt(e.Index);
        }

        public void AfterRename(object sender, LanExchange.TabModel.PanelItemListEventArgs e)
        {
            m_Pages.SelectedTab.Text = e.Info.TabName;
        }

        public static string InputBoxAsk(string caption, string prompt, string defText)
        {
            return MainForm.Instance.inputBox.Ask(caption, prompt, defText, false);
        }

        internal ListView GetActiveListView()
        {
            return (ListView)m_Pages.SelectedTab.Controls[0];
        }

        internal static SaveFileDialog GetSaveFileDialog()
        {
            return MainForm.Instance.dlgSave;
        }
    }
}
