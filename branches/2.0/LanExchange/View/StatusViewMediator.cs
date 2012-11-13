using System;
using System.Collections.Generic;
using System.Text;
using LanExchange;
using LanExchange.Model;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using LanExchange.View.Components;
using GongSolutions.Shell.Interop;

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

            if (AppFacade.Resources != null)
            {
                Status.lCompName.Image = AppFacade.Resources.GetImage(CSIDL.DRIVES.ToString());
                //Status.lUserName.Image = AppFacade.Resources.GetImage("current_user");
            }

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
            list.Add(AppFacade.ITEM_COUNT_CHANGED);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case AppFacade.ITEM_COUNT_CHANGED:
                    int Value = (int)note.Body;
                    Status.lItemsCount.Text = String.Format(AppFacade.T("ItemsShowed"), Value);
                    break;
            }
        }
    }
}
