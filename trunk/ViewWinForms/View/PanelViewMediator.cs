using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LanExchange;
using LanExchange.Model;
using LanExchange.Model.VO;
using PureMVC.PureInterfaces;
using PureMVC.PurePatterns;
using ViewWinForms.View.Components;

namespace ViewWinForms.View
{
    public class PanelViewMediator : Mediator, IMediator
    {
        public new const string NAME = "PanelViewMediator";

        private PanelItemProxy m_CurrentProxy;
        private string m_CurrentProxyName;
        private string m_Path;

        private const int MAX_TIME_FOR_MULTISORT = 1000;
        private int LastSortTick = 0;
        private PanelItemComparer m_Comparer;


		public PanelViewMediator(PanelView PV)
			: base(NAME, PV)
		{
            PV.LevelDown += new EventHandler(PV_LevelDown);
            PV.LevelUp += new EventHandler(PV_LevelUp);
            PV.ItemsCountChanged += new EventHandler(PV_ItemsCountChanged);
            PV.LV.ColumnClick += new ColumnClickEventHandler(LV_ColumnClick);

            m_Comparer = new PanelItemComparer();
            m_Comparer.SortOrders.Add(new PanelItemSortOrder(0, PanelItemSortDirection.Ascending));
        }

        private PanelView Panel
        {
            get { return (PanelView)ViewComponent; }
        }

        private string GetRootProxyName()
        {
            string Result = String.Empty;
            NavigatorProxy navigator = (NavigatorProxy)Facade.RetrieveProxy("NavigatorProxy");
            if (navigator != null)
            {
                IList<string> list = navigator.GetRoots();
                if (list.Count > 0)
                    Result = list[0];
            }
            return Result;
        }

        public override void OnRegister()
        {
            base.OnRegister();
        }

        public override IList<string> ListNotificationInterests()
        {
            IList<string> list = new List<string>();
            list.Add(Globals.UPDATE_ITEMS);
            return list;
        }

        public override void HandleNotification(INotification note)
        {
            switch (note.Name)
            {
                case Globals.UPDATE_ITEMS:
                    string path = (string)note.Body;
                    UpdateItems(GetRootProxyName(), String.Empty);
                    break;
            }
        }


        private void UpdateItems(string NewProxyName, string NewPath)
        {
            PanelItemProxy Proxy = (PanelItemProxy)Facade.RetrieveProxy(NewProxyName);
            if (Proxy != null)
            {
                // set variables
                m_CurrentProxy = Proxy;
                m_CurrentProxyName = NewProxyName;
                m_Path = NewPath;
                // update items
                Panel.LV.BeginUpdate();
                try
                {
                    m_CurrentProxy.Objects.Clear();
                    m_CurrentProxy.EnumObjects(m_Path);
                    m_CurrentProxy.Sort(m_Comparer);
                    Panel.SetColumns(m_CurrentProxy.GetColumns());
                    Panel.AddItems(m_CurrentProxy.Objects);
                }
                finally
                {
                    Panel.LV.EndUpdate();
                }
            }
        }

        void PV_LevelDown(object sender, EventArgs e)
        {
            PanelItemVO Current = Panel.SelectedPanelItem;
            if (Current == null)
                return;
            if (Current.IsBackButton)
            {
                Panel.SetLevelUp();
                return;
            }
            switch(m_CurrentProxyName)
            {
                case "DomainProxy":
                    UpdateItems("ComputerProxy", Current.Name);
                    break;
                case "ComputerProxy":
                    UpdateItems("ShareProxy", Current.Name);
                    break;
                case "ShareProxy":
                    PanelItemVO First = Panel.FirstPanelItem;
                    if (First != null && First.SubItems.Length > 1)
                    {
                        UpdateItems("FileProxy", Path.Combine(First.SubItems[1], Current.Name));
                    }
                    break;
            }
        }

        void PV_LevelUp(object sender, EventArgs e)
        {
            switch(m_CurrentProxyName)
            {
                case "ComputerProxy":
                    m_Path = "";
                    UpdateItems("DomainProxy", "");
                    break;
                case "ShareProxy":
                    UpdateItems("ComputerProxy", "FERMAK");
                    break;
                case "FileProxy":
                    UpdateItems("ShareProxy", "MIKHAILYUK-KA");
                    break;
            }
            //SendNotification(ApplicationFacade.LEVEL_UP, this);
        }

        void PV_ItemsCountChanged(object sender, EventArgs e)
        {
            SendNotification(Globals.ITEM_COUNT_CHANGED, m_CurrentProxy.NumObjects);
        }


        public void ChangeSortOrder(int ColIndex)
        {
            if (Math.Abs(LastSortTick - System.Environment.TickCount) > MAX_TIME_FOR_MULTISORT)
            {
                if (m_Comparer.SortOrders.Count == 0)
                    m_Comparer.SortOrders.Add(new PanelItemSortOrder(ColIndex, PanelItemSortDirection.Ascending));
                else
                {
                    PanelItemSortOrder order = m_Comparer.SortOrders[0];
                    if (order.Index == ColIndex)
                        order.SwitchDirection();
                    else
                    {
                        order.Index = ColIndex;
                        order.Direction = PanelItemSortDirection.Ascending;
                    }
                    m_Comparer.SortOrders.Clear();
                    m_Comparer.SortOrders.Add(order);
                }
            }
            else
            {
                PanelItemSortOrder order;
                bool bFound = false;
                for (int i = 0; i < m_Comparer.SortOrders.Count; i++)
                    if (m_Comparer.SortOrders[i].Index == ColIndex)
                    {
                        order = m_Comparer.SortOrders[i];
                        order.SwitchDirection();
                        m_Comparer.SortOrders.RemoveAt(i);
                        m_Comparer.SortOrders.Insert(i, order);
                        bFound = true;
                        break;
                    }
                if (!bFound)
                    m_Comparer.SortOrders.Add(new PanelItemSortOrder(ColIndex, PanelItemSortDirection.Ascending));
            }
            LastSortTick = System.Environment.TickCount;
        }


        void LV_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Panel.LV.BeginUpdate();
            Panel.SaveSelected();
            try
            {
                ChangeSortOrder(e.Column);
                m_CurrentProxy.Sort(m_Comparer);
            }
            finally
            {
                Panel.RestoreSelected();
                Panel.LV.EndUpdate();
            }
        }
    }
}
