using System;

namespace LanExchange.Intf
{
    public interface IPagesModel : IModel
    {
        event EventHandler<PanelModelEventArgs> AfterAppendTab;
        event EventHandler<PanelIndexEventArgs> AfterRemove;
        event EventHandler<PanelModelEventArgs> AfterRename;
        event EventHandler<PanelIndexEventArgs> IndexChanged;

        void AddTab(IPanelModel panelItemList);

        bool TabNameExists(string tabName);

        void DelTab(int index);

        string GenerateTabName();

        int Count { get; }
        int SelectedIndex { get; set; }
        IPanelModel GetItem(int index);
        void RenameTab(int index, string newTabName);
        string GetTabName(int index);

        void SaveSettings();
        void LoadSettings();
    }

    public class PanelIndexEventArgs : EventArgs
    {
        private readonly int m_Index;

        public PanelIndexEventArgs(int index)
        {
            m_Index = index;
        }

        public int Index
        {
            get { return m_Index; }
        }
    }

    public class PanelModelEventArgs : EventArgs
    {
        private readonly IPanelModel m_Info;

        public PanelModelEventArgs(IPanelModel info)
        {
            m_Info = info;
        }

        public IPanelModel Info
        {
            get { return m_Info; }
        }
    }
}
