using System;
using System.Collections.Generic;
using System.Text;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using LanExchange.View.Components;
using LanExchange.Model;
using LanExchange.Model.VO;

namespace LanExchange.View
{
    public class StatusViewMediator : Mediator, IMediator
    {
        public new const string NAME = "StatusViewMediator";

		public StatusViewMediator(StatusView Status)
			: base(NAME, Status)
		{

        }

        public override void OnRegister()
        {
            base.OnRegister();
            CurrentUserProxy Obj = (CurrentUserProxy)Facade.RetrieveProxy(CurrentUserProxy.NAME);
            if (Obj != null)
            {
                Status.lCompName.Text = Obj.ComputerName;
                Status.lUserName.Text = Obj.UserName;
            }
        }

        public StatusView Status
        {
            get { return (StatusView)ViewComponent; }
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(ApplicationFacade.ITEM_COUNT_CHANGED);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case ApplicationFacade.ITEM_COUNT_CHANGED:
                    IntVO V = (IntVO)note.Body;
                    Status.lItemsCount.Text = String.Format("Показано элементов: {0}", V.Value);
                    break;
            }
        }
    }
}
