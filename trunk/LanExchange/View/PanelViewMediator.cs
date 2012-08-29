using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LanExchange;
using LanExchange.Model;
using LanExchange.Model.VO;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using LanExchange.View.Components;
using BrightIdeasSoftware;

namespace LanExchange.View
{
    public class Computer
    {
        private string m_Name = String.Empty;
        private string m_Comment = String.Empty;
        private string m_Version = String.Empty;

        public Computer(string name)
        {
            m_Name = name;
        }

        public Computer(string name, string comment, string version)
        {
            m_Name = name;
            m_Comment = comment;
            m_Version = version;
        }

        public Computer(Computer other)
        {
            m_Name = other.m_Name;
            m_Comment = other.m_Comment;
            m_Version = other.m_Version;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public string Comment
        {
            get { return m_Comment; }
            set { m_Comment = value; }
        }

        public string Version
        {
            get { return m_Version; }
            set { m_Version = value; }
        }
    }


    public class PanelViewMediator : Mediator, IMediator
    {
        public new const string NAME = "PanelViewMediator";

        private PanelItemProxy m_CurrentProxy;
        private string m_CurrentProxyName;
        private string m_Path;

        private const int MAX_TIME_FOR_MULTISORT = 1000;
        private int LastSortTick = 0;


		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
            PV.LevelDown += new EventHandler(PV_LevelDown);
            PV.LevelUp += new EventHandler(PV_LevelUp);
            PV.LV.ItemsChanged += new EventHandler<BrightIdeasSoftware.ItemsChangedEventArgs>(PV_ItemsChanged);
            //PV.LV.ColumnClick += new ColumnClickEventHandler(LV_ColumnClick);
        }

        private PanelView Panel
        {
            get { return (PanelView)ViewComponent; }
        }

        private string GetRootProxyName()
        {
            string Result = String.Empty;
            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy(NavigatorProxy.NAME);
            if (navigator != null)
            {
                IList<string> list = navigator.GetRoots();
                if (list.Count > 0)
                    Result = list[0];
            }
            return Result;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(AppFacade.UPDATE_ITEMS);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case AppFacade.UPDATE_ITEMS:
                    string path = (string)note.Body;
                    UpdateItems(GetRootProxyName(), String.Empty);
                    break;
            }
        }


        private void UpdateItems(string NewProxyName, string NewPath)
        {
            PanelItemProxy Proxy = (PanelItemProxy)Facade.RetrieveProxy(NewProxyName);
            if (Proxy != null)
            {
                // set variables
                m_CurrentProxy = Proxy;
                m_CurrentProxyName = NewProxyName;
                m_Path = NewPath;
                // update items
                m_CurrentProxy.EnumObjects(m_Path);
                Panel.SetColumns(m_CurrentProxy.GetColumns());
                Panel.LV.BeginUpdate();
                try
                {
                    Panel.LV.SetObjects(m_CurrentProxy.Objects);
                    Panel.LV.Sort(Panel.LV.GetColumn(0));
                }
                finally
                {
                    Panel.LV.EndUpdate();
                }
            }
        }

        void PV_LevelDown(object sender, EventArgs e)
        {
            PanelItemVO Current = Panel.SelectedPanelItem;
            if (Current == null)
                return;
            if (Current.IsBackButton)
            {
                Panel.SetLevelUp();
                return;
            }
            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy(NavigatorProxy.NAME);
            if (navigator != null)
            {
                String S = navigator.GetChildLevel(m_CurrentProxyName);
                UpdateItems(S, Current.Name);
            }
        }

        void PV_LevelUp(object sender, EventArgs e)
        {
            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy(NavigatorProxy.NAME);
            if (navigator != null)
            {
                String S = navigator.GetParentLevel(m_CurrentProxyName);
                UpdateItems(S, String.Empty);
            }
            //SendNotification(ApplicationFacade.LEVEL_UP, this);
        }

        void PV_ItemsChanged(object sender, BrightIdeasSoftware.ItemsChangedEventArgs e)
        {
            SendNotification(AppFacade.ITEM_COUNT_CHANGED, e.NewObjectCount);
        }
    }
}
