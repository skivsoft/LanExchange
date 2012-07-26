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
        private string currentProxyName = "ComputerProxy";

		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
        }

        private PanelView Panel
        {
            get { return (PanelView)ViewComponent; }
        }

        public override void OnRegister()
        {
            base.OnRegister();
            currentProxy = (PanelItemProxy)Facade.RetrieveProxy(currentProxyName);
            if (currentProxy != null)
            {
                currentProxy.EnumObjects();
                Panel.AddItems(currentProxy.Objects);
            }
        }
    }
}
