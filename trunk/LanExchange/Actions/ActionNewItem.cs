using LanExchange.Ioc;
using LanExchange.SDK;

namespace LanExchange.Actions
{
    class ActionNewItem : IAction
    {
        public void Execute()
        {
            var model = App.MainPages.GetItem(App.MainPages.SelectedIndex);
            if (model == null) return;
            var form = App.Resolve<IEditView>();
            form.Presenter.SetDataType(model.DataType);
            form.ShowModal();
        }

        public bool Enabled
        {
            get
            {
                var model = App.MainPages.GetItem(App.MainPages.SelectedIndex);
                return model != null;
            }
        }
    }
}
