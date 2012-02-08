using System;
using System.Collections.Generic;
using System.Text;
using SkivSoft.LanExchange.SDK;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Collections;

namespace LanExchange
{
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
  
    #region TLanEXMenuItem
    public class TLanEXMenuItem : ILanEXMenuItem
    {
        public ToolStripMenuItem Instance = null;

        public TLanEXMenuItem(ToolStripMenuItem item)
        {
            if (item == null)
            {
                this.Instance = new ToolStripMenuItem();
            }
            else
                this.Instance = item;
        }

        public object Tag
        {
            get { return this.Instance.Tag; }
            set { this.Instance.Tag = value; }
        }

        public event EventHandler Click;

        public IList DropDownItems { get { return this.Instance.DropDownItems; } }

        public string Text
        {
            get { return this.Instance.Text; }
            set { this.Instance.Text = value; }
        }

        public bool Checked
        {
            get { return this.Instance.Checked; }
            set { this.Instance.Checked = value; }
        }
    }
    #endregion

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

        public string Name
        {
            get { return this.control.Name; }
            set { this.control.Name = value; }
        }

        public Rectangle Bounds 
        { 
            get { return this.control.Bounds; } 
            set { this.control.Bounds = value; } 
        }
        
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

    #region TLanEXListView
    public class TLanEXListView : TLanEXControl, ILanEXListView
    {
        public ListView Instance = null;
        private ILanEXItemList ItemListInstance = null;

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

        public ILanEXItemList ItemList
        {
            get { return this.ItemListInstance; }
            set { this.ItemListInstance = value; }
        }


        public List<string> GetSelected(bool bAll)
        {
            List<string> Result = new List<string>();
            if (FocusedItem != null)
                Result.Add(FocusedItem.Text);
            else
                Result.Add("");
            if (bAll)
                for (int index = 0; index < ItemsCount; index++)
                    Result.Add(ItemList.Keys[index]);
            else
                foreach (int index in SelectedIndices)
                    Result.Add(ItemList.Keys[index]);
            return Result;
        }

        public void SetSelected(List<string> SaveSelected)
        {
            SelectedIndices.Clear();
            FocusedItem = null;
            if (VirtualListSize > 0)
            {
                for (int i = 0; i < SaveSelected.Count; i++)
                {
                    int index = ItemList.Keys.IndexOf(SaveSelected[i]);
                    if (index == -1) continue;
                    if (i == 0)
                    {
                        FocusedItem = (ILanEXListViewItem)GetItem(index);
                        EnsureVisible(index);
                    }
                    else
                        SelectedIndices.Add(index);
                }
            }
        }

        // <summary>
        // Выбор компьютера по имени в списке.
        // </summary>
        public void SelectComputer(string CompName)
        {
            int index = -1;
            // пробуем найти запомненный элемент
            if (CompName != null)
            {
                index = ItemList.Keys.IndexOf(CompName);
                if (index == -1) index = 0;
            }
            else
                index = 0;
            // установка текущего элемента
            if (VirtualListSize > 0)
            {
                SelectedIndices.Add(index);
                FocusedItem = (ILanEXListViewItem)GetItem(index);
                EnsureVisible(index);
            }
        }

    }
    #endregion

    #region TLanEXTabPage
    public class TLanEXTabPage : TLanEXControl, ILanEXTabPage
    {
        public TabPage Instance = null;

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
    public class TLanEXTabControl : TLanEXControl, ILanEXTabControl
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
            this.Instance.TabPages.Add((Page as TLanEXTabPage).control as TabPage);
        }

        public void RemoveAt(int Index)
        {
            this.Instance.TabPages.RemoveAt(Index);
        }
    }
    #endregion

    #region TLanEXStatusStrip
    public class TLanEXStatusStrip : TLanEXControl, ILanEXStatusStrip
    {
        private StatusStrip Instance = null;

        public TLanEXStatusStrip(Control c)
            : base(c)
        {
            if (c == null)
            {
                this.Instance = new StatusStrip();
                this.control = this.Instance;
            }
            else
                this.Instance = c as StatusStrip;
        }

        public void SetText(int Index, string Text)
        {
            int NewIndex = Index * 2;
            this.Instance.Items[NewIndex].Text = Text;
        }
    }
    #endregion

    #region TMainAppUI
    public class TMainAppUI : TMainApp
    {


        public TMainAppUI()
        {
        }

        public override void Init()
        {
            base.Init();
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

        public override ILanEXControl CreateMainForm()
        {
            return new TLanEXControl(LanExchange.MainForm.Instance);
        }

        public override ILanEXTabControl CreatePages()
        {
            return new TLanEXTabControl(LanExchange.MainForm.Instance.Pages);
        }
        
        public override ILanEXStatusStrip CreateStatusStrip()
        {
            return new TLanEXStatusStrip(LanExchange.MainForm.Instance.statusStrip1);
        }

        public override ILanEXComponent CreateComponent(Type type)
        {
            ILanEXComponent Result = null;
            if (type == typeof(ILanEXItemList))
                Result = new TLanEXItemList();
            else
            if (type == typeof(ILanEXListViewItem))
                Result = new TLanEXListViewItem(null);
            else
            if (type == typeof(ILanEXMenuItem))
                Result = new TLanEXMenuItem(null);
            LogPrint("Create component of type [{0}]: {1}", type.Name, Result == null ? "null" : Result.ToString());
            return Result;
        }

        public override ILanEXControl CreateControl(Type type)
        {
            ILanEXControl Result = null;
            if (type == typeof(ILanEXControl))
            {
                Result = new TLanEXControl(null);
            } else
            if (type == typeof(ILanEXTabControl))
            {
                Result = new TLanEXTabControl(null);
            } else
            if (type == typeof(ILanEXTabPage))
            {
                Result = new TLanEXTabPage(null);
            } else
            if (type == typeof(ILanEXListView))
            {
                Result = new TLanEXListView(null);
            }
            LogPrint("Create control of type [{0}]: {1}", type.Name, Result == null ? "null" : Result.ToString());
            return Result;
        }

        public override void DoLoaded()
        {
            // set up main form location
            Rectangle Rect = new Rectangle();
            Rect.Size = new Size(450, Screen.PrimaryScreen.WorkingArea.Height);
            Rect.Location = new Point(Screen.PrimaryScreen.WorkingArea.Left + (Screen.PrimaryScreen.WorkingArea.Width - Rect.Width),
                                      Screen.PrimaryScreen.WorkingArea.Top + (Screen.PrimaryScreen.WorkingArea.Height - Rect.Height));
            //this.SetBounds(Rect.X, Rect.Y, Rect.Width, Rect.Height);
            MainForm.Bounds = Rect;
            // call parent class Loaded() 
            base.DoLoaded();
        }

        public override void ListView_SetupTip(ILanEXListView LV)
        {
            LanExchange.MainForm.Instance.tipComps.SetToolTip((LV as TLanEXListView).Instance, "!");
            LanExchange.MainForm.Instance.tipComps.Active = true;
        }

        public override void ListView_Setup(ILanEXListView LV)
        {
            ListView LVInstance = (LV as TLanEXListView).Instance;
            TMainApp.App.LogPrint("Setup control {0}", LVInstance.ToString());
            LVInstance.Columns.Clear();
            LVInstance.Columns.Add("Q1", 130);
            LVInstance.Columns.Add("Q2", 250);
            LVInstance.ContextMenuStrip = LanExchange.MainForm.Instance.popComps;
            LVInstance.Location = new Point(3, 3);
            LVInstance.Dock = System.Windows.Forms.DockStyle.Fill;
            LVInstance.FullRowSelect = true;
            LVInstance.GridLines = true;
            LVInstance.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            LVInstance.HideSelection = false;
            LVInstance.LargeImageList = LanExchange.MainForm.Instance.ilLarge;
            LVInstance.ShowGroups = false;
            LVInstance.ShowItemToolTips = true;
            LVInstance.SmallImageList = LanExchange.MainForm.Instance.ilSmall;
            LVInstance.View = System.Windows.Forms.View.Details;
            LVInstance.VirtualMode = true;
            //LVInstance.ItemActivate += new System.EventHandler(lvRecent_ItemActivate);
            //LVInstance.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(lvComps_RetrieveVirtualItem);
            //LVInstance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(lvComps_KeyPress);
            //LVInstance.KeyDown += new System.Windows.Forms.KeyEventHandler(lvComps_KeyDown);
            ListView_SetupTip(LV);
            ListView_Update(LV);
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

        public override int RegisterImageIndex(Bitmap pic16x16, Bitmap pic32x32)
        {
            LanExchange.MainForm.Instance.ilSmall.Images.Add(pic16x16);
            LanExchange.MainForm.Instance.ilLarge.Images.Add(pic32x32);
            return LanExchange.MainForm.Instance.ilLarge.Images.Count-1;
        }

    }
    #endregion

}
