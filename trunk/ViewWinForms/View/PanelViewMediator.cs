using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.IO;
using System.Windows.Forms;
using LanExchange.SDK.SDKModel;
using LanExchange.SDK.SDKModel.VO;
using LanExchange.SDK;
using ViewWinForms.View.Components;

namespace ViewWinForms.View
{
    public class PanelViewMediator : Mediator, IMediator
    {
        public new const string NAME = "PanelViewMediator";

        private IPanelItemProxy m_CurrentProxy;
        private string m_CurrentProxyName = "DomainProxy";
        private string m_Path;

		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
            PV.LevelDown += new EventHandler(PV_LevelDown);
            PV.LevelUp += new EventHandler(PV_LevelUp);
            PV.ItemsCountChanged += new EventHandler(PV_ItemsCountChanged);
            PV.LV.ColumnClick += new ColumnClickEventHandler(LV_ColumnClick);
        }

        private PanelView Panel
        {
            get { return (PanelView)ViewComponent; }
        }

        public override void OnRegister()
        {
            base.OnRegister();
            ICurrentUserProxy Obj = (ICurrentUserProxy)Facade.RetrieveProxy("CurrentUserProxy");
            if (Obj != null)
            {
                m_Path = Obj.DomainName;
                UpdateItems();
            }
        }

        private void UpdateItems()
        {
            m_CurrentProxy = (IPanelItemProxy)Facade.RetrieveProxy(m_CurrentProxyName);
            if (m_CurrentProxy != null)
            {
                m_CurrentProxy.Objects.Clear();
                m_CurrentProxy.EnumObjects(m_Path);
                m_CurrentProxy.Sort();
                Panel.SetColumns(m_CurrentProxy.GetColumns());
                Panel.AddItems(m_CurrentProxy.Objects);
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
            switch(m_CurrentProxyName)
            {
                case "DomainProxy":
                    m_CurrentProxyName = "ComputerProxy";
                    m_Path = Current.Name;
                    UpdateItems();
                    break;
                case "ComputerProxy":
                    m_CurrentProxyName = "ResourceProxy";
                    m_Path = Current.Name;
                    UpdateItems();
                    break;
                case "ResourceProxy":
                    PanelItemVO First = Panel.FirstPanelItem;
                    if (First != null)
                    {
                        m_CurrentProxyName = "FileProxy";
                        m_Path = Path.Combine(First.SubItems[1], Current.Name);
                        UpdateItems();
                    }
                    break;
            }
        }

        void PV_LevelUp(object sender, EventArgs e)
        {
            switch(m_CurrentProxyName)
            {
                case "ComputerProxy":
                    m_CurrentProxyName = "DomainProxy";
                    m_Path = "";
                    UpdateItems();
                    break;
                case "ResourceProxy":
                    m_CurrentProxyName = "ComputerProxy";
                    m_Path = "FERMAK";
                    UpdateItems();
                    break;
                case "FileProxy":
                    m_CurrentProxyName = "ResourceProxy";
                    m_Path = "MIKHAILYUK-KA";
                    UpdateItems();
                    break;
            }
            //SendNotification(ApplicationFacade.LEVEL_UP, this);
        }

        void PV_ItemsCountChanged(object sender, EventArgs e)
        {
            SendNotification(Globals.ITEM_COUNT_CHANGED, m_CurrentProxy.NumObjects);
        }

        void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Panel.SaveSelected();
            m_CurrentProxy.ChangeSort(e.Column);
            m_CurrentProxy.Sort();
            Panel.RestoreSelected();
            Panel.Refresh();
        }
    }
}
