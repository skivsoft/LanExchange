using LanExchange.Plugin.Shortcut;
using LanExchange.SDK;
using System;
using System.Diagnostics.Contracts;

namespace LanExchange.Actions
{
    internal sealed class ShortcutKeysAction : IAction
    {
        private readonly IMainPresenter mainPresenter;
        private readonly IPagesPresenter pagesPresenter;

        public ShortcutKeysAction(
            IMainPresenter mainPresenter,
            IPagesPresenter pagesPresenter)
        {
            Contract.Requires<ArgumentNullException>(mainPresenter != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);

            this.mainPresenter = mainPresenter;
            this.pagesPresenter = pagesPresenter;
        }

        public void Execute()
        {
            var foundIndex = mainPresenter.FindShortcutKeysPanelIndex();
            if (foundIndex == -1)
            {
                var model = App.Resolve<IPanelModel>();
                var root = new ShortcutRoot();
                model.DataType = typeof (ShortcutPanelItem).Name;
                model.CurrentPath.Push(root);
                pagesPresenter.AddTab(model);
                foundIndex = pagesPresenter.Count - 1;
            }
            pagesPresenter.SelectedIndex = foundIndex;
        }

        public bool Enabled
        {
            get { return true; }
        }
    }
}