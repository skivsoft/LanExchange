using System;
using System.Collections.Generic;
using System.Text;
using PurePatterns;
using PureInterfaces;
using System.IO;
using System.Windows.Forms;
using LanExchange.SDK.SDKModel;
using LanExchange.SDK.SDKModel.VO;
using LanExchange.SDK;
using ViewWinForms.View.Components;

namespace ViewWinForms.View
{
    public class PanelViewMediator : Mediator, IMediator
    {
        public new const string NAME = "PanelViewMediator";

        private IPanelItemProxy m_CurrentProxy;
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

        public override void OnRegister()
        {
            base.OnRegister();
            ICurrentUserProxy Obj = (ICurrentUserProxy)Facade.RetrieveProxy("CurrentUserProxy");
            if (Obj != null)
            {
                UpdateItems("DomainProxy", Obj.DomainName);
            }
        }

        private void UpdateItems(string NewProxyName, string NewPath)
        {
            IPanelItemProxy Proxy = (IPanelItemProxy)Facade.RetrieveProxy(NewProxyName);
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
                    UpdateItems("ResourceProxy", Current.Name);
                    break;
                case "ResourceProxy":
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
                case "ResourceProxy":
                    UpdateItems("ComputerProxy", "FERMAK");
                    break;
                case "FileProxy":
                    UpdateItems("ResourceProxy", "MIKHAILYUK-KA");
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
