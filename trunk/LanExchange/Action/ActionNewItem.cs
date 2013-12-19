using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.SDK;

namespace LanExchange.Action
{
    class ActionNewItem : IAction
    {
        public void Execute()
        {
            var form = App.Resolve<IEditItemView>();
            form.Show();
        }

        public bool Enabled
        {
            get { return true; }
        }
    }
}
