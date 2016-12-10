using System;
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
            this.pagesModel = pagesModel ?? throw new ArgumentNullException(nameof(pagesModel));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
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