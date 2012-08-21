using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using LanExchange.SDK;
using ViewWinForms.View.Components;
using LanExchange.SDK.SDKModel;
using ViewWinForms.Properties;

namespace ViewWinForms.View
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
            ICurrentUserProxy Obj = (ICurrentUserProxy)Facade.RetrieveProxy("CurrentUserProxy");
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
            list.Add(Globals.ITEM_COUNT_CHANGED);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case Globals.ITEM_COUNT_CHANGED:
                    int Value = (int)note.Body;
                    Status.lItemsCount.Text = String.Format(Resources.ItemsShowed, Value);
                    break;
            }
        }
    }
}
