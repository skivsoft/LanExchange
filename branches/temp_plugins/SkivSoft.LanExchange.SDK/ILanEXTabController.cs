using System;
using System.Text;
using System.Collections.Generic;

namespace SkivSoft.LanExchange.SDK
{
    #region TabInfoEventHandler and TabInfoEventArgs
    public class TabInfoEventArgs : EventArgs
    {
        private TabInfo info;

        public TabInfoEventArgs(TabInfo Info)
        {
            this.info = Info;
        }

        public TabInfo Info { get { return this.info; } }
    }

    public delegate void TabInfoEventHandler(object sender, TabInfoEventArgs e);
    #endregion

    #region IndexEventHandler and IndexEventArgs
    public class IndexEventArgs : EventArgs
    {
        private int index;

        public IndexEventArgs(int Index)
        {
            this.index = Index;
        }

        public int Index { get { return this.index; } }
    }

    public delegate void IndexEventHandler(object sender, IndexEventArgs e);

    #endregion

    #region TTabInfo class
    public class TabInfo
    {
        public string TabName = "";
        public string FilterText = "";
        public int CurrentView = 2;
        public List<string> Items = null;

        public TabInfo(string name)
        {
            this.TabName = name;
        }
    }
    #endregion

    #region ILanEXTabModel Interface
    public interface ILanEXTabModel
    {
        event TabInfoEventHandler AfterAppendTab;
        event IndexEventHandler AfterRemove;
        event TabInfoEventHandler AfterRename;

        IList<TabInfo> InfoList { get; }
        void AddTab(TabInfo Info);
        void DelTab(int Index);
        void RenameTab(int Index, string NewTabName);
        string GetTabName(int i);
        void Clear();
    }
    #endregion

    #region ILanEXTabController Interface
    public interface ILanEXTabController
    {
        ILanEXTabModel Model { get; }
        void NewTab();
        void CloseTab();
        void RenameTab();
        void SaveTab();
        void ListTab();
        void AddTabsToMenuItem(ILanEXMenuItem menuitem, EventHandler handler, bool bHideActive);
        void SendPanelItems(ILanEXListView lvSender, ILanEXListView lvReceiver);
        void mSelectTab_Click(object sender, EventArgs e);
        void mSendToNewTab_Click(object sender, EventArgs e);
        void mSendToSelectedTab_Click(object sender, EventArgs e);
        bool CanModifyTab(int Index);
        void StoreSettings();
        void LoadSettings();
    }
    #endregion

}
