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
    class TLanEXControl : ILanEXControl, IDisposable
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
    }

    class TLanEXForm : TLanEXControl, ILanEXForm, IServiceProvider
    {

        public TLanEXForm(Control c)
            : base(c)
        {

        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(SkivSoft.LanExchange.SDK.ILanEXForm))
            {
                return this;
            }
            return null;
        }

        protected Form Instance { get { return this.control as Form; } }

        public Rectangle Bounds { get { return this.Instance.Bounds; } set { this.Instance.Bounds = value; } }
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
            return null;
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
