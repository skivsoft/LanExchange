using System;
using System.Diagnostics.Contracts;
using LanExchange.Plugin.Shortcut;
using LanExchange.Presentation.Interfaces;
using LanExchange.SDK;
using LanExchange.SDK.Factories;

namespace LanExchange.Commands
{
    internal sealed class ShortcutKeysCommand : ICommand
    {
        private readonly IMainPresenter mainPresenter;
        private readonly IPagesPresenter pagesPresenter;
        private readonly IModelFactory modelFactory;

        public ShortcutKeysCommand(
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