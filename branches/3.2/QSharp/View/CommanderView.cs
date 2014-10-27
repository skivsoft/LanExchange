using System;
using LanExchange.SDK.Presenter.View;

namespace LanExchange.View
{
    internal class CommanderView : ICommanderView
    {
        private readonly ICommanderPanelView m_LeftPanel;
        private readonly ICommanderPanelView m_RightPanel;

        public CommanderView(ICommanderPanelView leftPanel, ICommanderPanelView rightPanel)
        {
            m_LeftPanel = leftPanel;
            m_RightPanel = rightPanel;
        }

        public CommanderPanelSide ActivePanelSide { get; set; }

        public ICommanderPanelView ActivePanel
        {
            get { return ActivePanelSide == CommanderPanelSide.LeftSide ? LeftPanel : RightPanel; }
        }

        public ICommanderPanelView PassivePanel
        {
            get { return ActivePanelSide == CommanderPanelSide.LeftSide ? RightPanel : LeftPanel; }
        }

        public ICommanderPanelView LeftPanel 
        {
            get { return m_LeftPanel; }
        }

        public ICommanderPanelView RightPanel
        {
            get { return m_RightPanel; }
        }

        public IntPtr Handle
        {
            get { return IntPtr.Zero; }
        }
    }
}