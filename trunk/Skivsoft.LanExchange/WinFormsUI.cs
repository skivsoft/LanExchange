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

        public void BringToFront()
        {
            this.control.BringToFront();
        }

        public void SendToBack()
        {
            this.control.SendToBack();
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

        public override ILanEXForm CreateMainForm()
        {
            return new TLanEXForm(LanExchange.MainForm.Instance);
        }

        public override ILanEXTabControl CreatePages()
        {
            return new TLanEXTabControl(LanExchange.MainForm.Instance.Pages);
        }
        
        public override ILanEXStatusStrip CreateStatusStrip()
        {
            return new TLanEXStatusStrip(LanExchange.MainForm.Instance.statusStrip1);
        }

        public override ILanEXControl CreateControl(Type type)
        {
            ILanEXControl Result = null;
            if (type == typeof(ILanEXControl))
            {
                Result = new TLanEXControl(null);
            } else
            if (type == typeof(ILanEXForm))
            {
                Result = new TLanEXForm(null);
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
            } else
            if (type == typeof(ILanEXStatusStrip))
            {
                Result = new TLanEXStatusStrip(LanExchange.MainForm.Instance.statusStrip1);
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
