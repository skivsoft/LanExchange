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
        private PanelItemProxy currentProxy;
        private string currentProxyName = "DomainProxy";

        public new const string NAME = "PanelViewMediator";

		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
            PV.LevelDown +=new EventHandler(PV_LevelDown);
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

        void PV_LevelDown(object sender, EventArgs e)
        {
            MessageBox.Show("LD");
        }
    }
}
