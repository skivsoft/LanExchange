using LanExchange.Plugin.Shortcut;
using LanExchange.SDK;
using LanExchange.SDK.Factories;
using System;
using System.Diagnostics.Contracts;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Actions
{
    internal sealed class ShortcutKeysAction : IAction
    {
        private readonly IMainPresenter mainPresenter;
        private readonly IPagesPresenter pagesPresenter;
        private readonly IModelFactory modelFactory;

        public ShortcutKeysAction(
            IMainPresenter mainPresenter,
            IPagesPresenter pagesPresenter,
            IModelFactory modelFactory)
        {
            Contract.Requires<ArgumentNullException>(mainPresenter != null);
            Contract.Requires<ArgumentNullException>(pagesPresenter != null);
            Contract.Requires<ArgumentNullException>(modelFactory != null);

            this.mainPresenter = mainPresenter;
            this.pagesPresenter = pagesPresenter;
            this.modelFactory = modelFactory;
        }

        public void Execute()
        {
            var foundIndex = mainPresenter.FindShortcutKeysPanelIndex();
            if (foundIndex == -1)
            {
                var model = modelFactory.CreatePanelModel();
                var root = new ShortcutRoot();
                model.DataType = typeof (ShortcutPanelItem).Name;
                model.CurrentPath.Push(root);
                //TODO: hide model
                //pagesPresenter.AddTab(model);
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