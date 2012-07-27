using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using LanExchange.Model;
using LanExchange.View.Components;
using LanExchange.Model.VO;
using System.Windows.Forms;
using System.IO;

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
                m_CurrentProxy.EnumObjectsSorted(m_Path);
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
            SendNotification(ApplicationFacade.ITEM_COUNT_CHANGED, new IntVO(m_CurrentProxy.NumObjects));
        }
    }
}
