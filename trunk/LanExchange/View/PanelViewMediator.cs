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

        private PanelItemProxy currentProxy;
        private string currentProxyName = "DomainProxy";

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
            currentProxy = (PanelItemProxy)Facade.RetrieveProxy(currentProxyName);
            if (currentProxy != null)
            {
                currentProxy.Objects.Clear();
                currentProxy.EnumObjects();
                Panel.AddItems(currentProxy.Objects);
            }
        }

        void PV_LevelDown(object sender, EventArgs e)
        {
            currentProxyName = "ComputerProxy";
            UpdateItems();
            //SendNotification(ApplicationFacade.LEVEL_DOWN, this);
        }

        void PV_LevelUp(object sender, EventArgs e)
        {
            currentProxyName = "DomainProxy";
            UpdateItems();
            //SendNotification(ApplicationFacade.LEVEL_UP, this);
        }

        void PV_ItemsCountChanged(object sender, EventArgs e)
        {
            SendNotification(ApplicationFacade.ITEM_COUNT_CHANGED, new IntVO(currentProxy.Objects.Count));
        }
    }
}
