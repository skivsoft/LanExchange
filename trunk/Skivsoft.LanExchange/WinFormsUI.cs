using System;
using System.Collections.Generic;
using System.Text;
using SkivSoft.LanExchange.SDK;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace LanExchange
{

    #region TLanEXControl
    public class TLanEXControl : ILanEXControl, IDisposable
    {
        public Control control = null;

        public TLanEXControl(Control c)
        {
            this.control = c;
        }

        public void Dispose()
        {
            if (this.control != null)
            {
                this.control.Dispose();
            }
        }

        public string Name { get { return this.control.Name; } set { this.control.Name = value; } }
        public object Tag { get { return this.control.Tag; } set { this.control.Tag = value; } }
        public event EventHandler Click { add { this.control.Click += value; } remove { this.control.Click -= value; } }
        
        public virtual void Add(ILanEXControl childControl)
        {
            this.control.Controls.Add((childControl as TLanEXControl).control);
        }

        public void Focus()
        {
            this.control.Focus();
        }
    }
    #endregion

    #region TLanEXForm
    public class TLanEXForm : TLanEXControl, ILanEXForm
    {

        public TLanEXForm(Control c)
            : base(c)
        {

        }

        protected Form Instance { get { return this.control as Form; } }

        public Rectangle Bounds { get { return this.Instance.Bounds; } set { this.Instance.Bounds = value; } }
    }
    #endregion

    #region TLanEXListViewItem
    public class TLanEXListViewItem : ILanEXListViewItem
    {
        public ListViewItem Instance = null;

        public TLanEXListViewItem(ListViewItem item)
        {
            if (item == null)
            {
                this.Instance = new ListViewItem();
            }
            else
                this.Instance = item;
        }

        public string Text
        {
            get { return this.Instance.Text; }
            set { this.Instance.Text = value; }
        }
    }
    #endregion

    #region TLanEXListView
    public class TLanEXListView : TLanEXControl, ILanEXListView
    {
        private ListView Instance = null;

        public TLanEXListView(Control c)
            : base(c)
        {
            if (c == null)
            {
                this.Instance = new ListView();
                this.control = this.Instance;
            }
            else
                this.Instance = c as ListView;
        }
        
        public int View
        {
            get
            {
                return (int)this.Instance.View;
            }
            set
            {
                this.Instance.View = (View)value;
            }
        }

        public int VirtualListSize
        {
            get { return this.Instance.VirtualListSize; }
            set { this.Instance.VirtualListSize = value; }
        }

        public IList<int> SelectedIndices
        {
            get { return this.Instance.SelectedIndices as IList<int>; }
        }

        public void EnsureVisible(int Index)
        {
            this.Instance.EnsureVisible(Index);
        }

        public ILanEXListViewItem FocusedItem
        {
            get 
            {
                if (this.Instance.FocusedItem == null)
                    return null;
                else
                    return new TLanEXListViewItem(this.Instance.FocusedItem); 
            }
            set
            {
                this.Instance.FocusedItem = (value as TLanEXListViewItem).Instance;
            }
        }

        public int ItemsCount { get { return this.Instance.Items.Count; } }

        public ILanEXListViewItem GetItem(int Index)
        {
            return new TLanEXListViewItem(this.Instance.Items[Index]);
        }

    }
    #endregion

    #region TLanEXTabPage
    class TLanEXTabPage : TLanEXControl, ILanEXTabPage
    {
        private TabPage Instance = null;

        public TLanEXTabPage(Control c)
            : base(c)
        {
            if (c == null)
            {
                this.Instance = new TabPage();
                this.control = this.Instance;
            }
            else
                this.Instance = c as TabPage;
        }

        public string Text 
        { 
            get { return this.Instance.Text; }
            set { this.Instance.Text = value; }
        }

        public bool IsListViewPresent
        {
            get { return this.Instance.Controls.Count > 0; }
        }

        public ILanEXListView ListView
        {
            get
            {
                if (IsListViewPresent)
                    return new TLanEXListView(this.Instance.Controls[0]);
                else
                    return null;
            }
            set
            {
                if (!IsListViewPresent)
                    this.Add(value);
            }
        }
    }
    #endregion

    #region TLanEXTabControl
    class TLanEXTabControl : TLanEXControl, ILanEXTabControl
    {
        private TabControl Instance = null;

        public TLanEXTabControl(Control c)
            : base(c)
        {
            if (c == null)
            {
                this.Instance = new TabControl();
                this.Instance.Dock = DockStyle.Fill;
                this.control = this.Instance;
            } else
                this.Instance = c as TabControl;
        }

        public int TabCount { get { return this.Instance.TabCount; } }
        public ILanEXTabPage SelectedTab { get { return new TLanEXTabPage(this.Instance.SelectedTab); }}
        
        public int SelectedIndex 
        { 
            get { return this.Instance.SelectedIndex; }
            set { this.Instance.SelectedIndex = value; }
        }

        public ILanEXTabPage GetPage(int Index)
        {
            return new TLanEXTabPage(this.Instance.TabPages[Index]);
        }

        public override void Add(ILanEXControl childControl)
        {
            ILanEXTabPage Page = childControl as ILanEXTabPage;
            this.Instance.TabPages.Add(Page.Text);
        }

        public void RemoveAt(int Index)
        {
            this.Instance.TabPages.RemoveAt(Index);
        }
    }
    #endregion

    #region TMainAppUI
    public class TMainAppUI : TMainApp
    {


        public TMainAppUI()
        {
            // load plugins
            LoadPlugins();
            // print version info to log
            LogPrint("OSVersion: [{0}], Processors count: {1}", Environment.OSVersion, Environment.ProcessorCount);
            LogPrint(@"MachineName: {0}, UserName: {1}\{2}, Interactive: {3}", Environment.MachineName, Environment.UserDomainName, Environment.UserName, Environment.UserInteractive);
            LogPrint("Executable: [{0}], Version: {1}", Application.ExecutablePath, Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        public override object GetService(Type serviceType)
        {
            if (serviceType == typeof(SkivSoft.LanExchange.SDK.ILanEXMainApp))
            {
                return this;
            }
            return null;
        }

        public override ILanEXControl CreateControl(Type type)
        {
            ILanEXControl Result = null;
            if (type == typeof(ILanEXForm))
            {
                Result = new TLanEXForm(null);
            } else
            if (type == typeof(ILanEXTabControl))
            {
                Result = new TLanEXTabControl(null);
            }
            return Result;
        }


        public override ILanEXForm CreateMainForm()
        {
            ILanEXForm Result = new TLanEXForm(LanExchange.MainForm.Instance);
            return Result;
        }

        public override void ListView_SetupTip(ILanEXListView LV)
        {
            //MainForm.Instance.tipComps.SetToolTip(LV, "!");
            //MainForm.Instance.tipComps.Active = true;
        }

        public override void ListView_Setup(ILanEXListView LV)
        {
            /*
            TMainApp.App.LogPrint("Setup control {0}", LV.ToString());
            LV.Columns.Clear();
            LV.Columns.Add("Сетевое имя", 130);
            LV.Columns.Add("Описание", 250);
            LV.ContextMenuStrip = MainForm.MainFormInstance.popComps;
            LV.Location = new Point(3, 3);
            LV.Dock = System.Windows.Forms.DockStyle.Fill;
            LV.FullRowSelect = true;
            LV.GridLines = true;
            LV.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            LV.HideSelection = false;
            LV.LargeImageList = MainForm.MainFormInstance.ilLarge;
            LV.ShowGroups = false;
            LV.ShowItemToolTips = true;
            LV.SmallImageList = MainForm.MainFormInstance.ilSmall;
            LV.View = System.Windows.Forms.View.Details;
            LV.VirtualMode = true;
            LV.ItemActivate += new System.EventHandler(MainForm.MainFormInstance.lvRecent_ItemActivate);
            LV.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(MainForm.MainFormInstance.lvComps_RetrieveVirtualItem);
            LV.KeyPress += new System.Windows.Forms.KeyPressEventHandler(MainForm.MainFormInstance.lvComps_KeyPress);
            LV.KeyDown += new System.Windows.Forms.KeyEventHandler(MainForm.MainFormInstance.lvComps_KeyDown);
            ListView_SetupTip(LV);
            ListView_Update(LV);
             */
        }

        public override void ListView_Update(ILanEXListView LV)
        {
            /*
            TPanelItemList ItemList = TPanelItemList.ListView_GetObject(LV);
            if (ItemList != null)
            {
                LV.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(MainForm.MainFormInstance.lvComps_RetrieveVirtualItem);
                LV.VirtualMode = true;
                LV.VirtualListSize = ItemList.FilterCount;
            }
             */
        }

        public override string InputBoxAsk(string caption, string prompt, string defText)
        {
            //return MainForm.Instance.inputBox.Ask(caption, prompt, defText, false);
            return "";
        }
    }
    #endregion

}
