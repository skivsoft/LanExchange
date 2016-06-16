using System;
using System.Diagnostics.Contracts;
using LanExchange.Application.Interfaces;
using LanExchange.Presentation.Interfaces;

namespace LanExchange.Plugin.Shortcut
{
    internal sealed class ShortcutKeysCommand : ICommand
    {
        private readonly IPagesModel pagesModel;
        private readonly IModelFactory modelFactory;

        public ShortcutKeysCommand(
            IPagesModel pagesModel,
            IModelFactory modelFactory)
        {
            Contract.Requires<ArgumentNullException>(pagesModel != null);
            Contract.Requires<ArgumentNullException>(modelFactory != null);

            this.pagesModel = pagesModel;
            this.modelFactory = modelFactory;
        }

        public void Execute()
        {
            var foundIndex = GetPanelIndexByDataType(typeof(ShortcutPanelItem));
            if (foundIndex == -1)
            {
                var model = modelFactory.CreatePanelModel();
                var root = new ShortcutRoot();
                model.DataType = typeof (ShortcutPanelItem).Name;
                model.CurrentPath.Push(root);
                pagesModel.Add(model);
                foundIndex = pagesModel.Count - 1;
            }
            pagesModel.SelectedIndex = foundIndex;
        }

        public bool Enabled
        {
            get { return true; }
        }

        private int GetPanelIndexByDataType(Type dataType)
        {
            for (int index = 0; index < pagesModel.Count; index++)
                if (pagesModel.GetAt(index).DataType.Equals(dataType.Name))
                    return index;
            return -1;
        }
    }
}