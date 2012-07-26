using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using LanExchange.Model;
using LanExchange.View.Components;
using LanExchange.Model.VO;
using System.Windows.Forms;

namespace LanExchange.View
{
    public class PanelViewMediator : Mediator, IMediator
    {
        public new const string NAME = "PanelViewMediator";

        private PanelItemProxy m_CurrentProxy;
        private string m_CurrentProxyName = "DomainProxy";
        private string m_Path = "";

		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
            PV.LevelDown += new EventHandler(PV_LevelDown);
            PV.LevelUp += new EventHandler(PV_LevelUp);
            PV.ItemsCountChanged +=new EventHandler(PV_ItemsCountChanged);
        }

        private PanelView Panel
        {
            get { return (PanelView)ViewComponent; }
        }

        public override void OnRegister()
        {
            base.OnRegister();
            UpdateItems();
        }

        private void UpdateItems()
        {
            m_CurrentProxy = (PanelItemProxy)Facade.RetrieveProxy(m_CurrentProxyName);
            if (m_CurrentProxy != null)
            {
                m_CurrentProxy.Objects.Clear();
                m_CurrentProxy.EnumObjects(m_Path);
                Panel.AddItems(m_CurrentProxy.Objects);
            }
        }

        void PV_LevelDown(object sender, EventArgs e)
        {
            switch(m_CurrentProxyName)
            {
                case "DomainProxy":
                    if (Panel.SelectedPanelItem != null)
                    {
                        m_CurrentProxyName = "ComputerProxy";
                        m_Path = Panel.SelectedPanelItem.Name;
                        UpdateItems();
                    }
                    break;

                case "ComputerProxy":
                    m_CurrentProxyName = "ResourceProxy";
                    m_Path = "MIKHAILYUK-KA";
                    break;
            }
            //SendNotification(ApplicationFacade.LEVEL_DOWN, this);
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
                case "ResourseProxy":
                    m_CurrentProxyName = "ComputerProxy";
                    m_Path = "FERMAK";
                    UpdateItems();
                    break;
            }
            //SendNotification(ApplicationFacade.LEVEL_UP, this);
        }

        void PV_ItemsCountChanged(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.ITEM_COUNT_CHANGED, new IntVO(m_CurrentProxy.Objects.Count));
        }
    }
}
