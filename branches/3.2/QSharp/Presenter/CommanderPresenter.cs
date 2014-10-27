using System;
using LanExchange.SDK.Presenter;
using LanExchange.SDK.Presenter.Model;
using LanExchange.SDK.Presenter.View;

namespace LanExchange.Presenter
{
    internal class CommanderPresenter : ICommanderPresenter
    {
        private ICommanderPanelModel m_LeftModel;
        private ICommanderPanelModel m_RightModel;

        private static void SetupViewFromModel(ICommanderPanelView targetView, ICommanderPanelModel sourceModel)
        {
            targetView.VirtualListSize = sourceModel.Count;
        }

        public CommanderPresenter(ICommanderView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            View = view;
        }

        public void CycleActiveModel()
        {
            switch (View.ActivePanelSide)
            {
                case CommanderPanelSide.LeftSide:
                    View.ActivePanelSide = CommanderPanelSide.RightSide;
                    break;
                case CommanderPanelSide.RightSide:
                    View.ActivePanelSide = CommanderPanelSide.LeftSide;
                    break;
            }
        }

        public void SetModels(ICommanderPanelModel leftModel, ICommanderPanelModel rightModel)
        {
            m_LeftModel = leftModel;
            m_RightModel = rightModel;
            SetupViewFromModel(View.LeftPanel, m_LeftModel);
            SetupViewFromModel(View.RightPanel, m_RightModel);
        }

        public ICommanderView View { get; set; }

        public ICommanderPanelModel ActiveModel
        {
            get { return m_LeftModel; }
        }

        public ICommanderPanelModel PassiveModel
        {
            get { return m_RightModel; }
        }
    }
}