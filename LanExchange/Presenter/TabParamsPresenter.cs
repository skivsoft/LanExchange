using System;
using System.Collections.Generic;
using LanExchange.Interface;
using LanExchange.Model;
using LanExchange.Model.Panel;

namespace LanExchange.Presenter
{
    public class TabParamsPresenter : ITabParamsPresenter
    {
        private ITabParamsView m_View;
        private readonly IList<string> m_Groups;
        private PanelItemList m_Info;

        public TabParamsPresenter()
        {
            m_Groups = new List<string>();
        }

        public void SetView(ITabParamsView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            m_View = view;
            m_View.OKClicked += View_OKClicked;
        }

        public void SetInfo(PanelItemList info)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            if (m_View == null)
                throw new NullReferenceException("View is null");
            m_Info = info;
            m_View.Text = String.Format(m_View.Text, m_Info.TabName);
            Groups = m_Info.Groups;
            // subscribe Form to ROOT subject (must return domain list)
            PanelSubscription.Instance.SubscribeToSubject(this, ConcreteSubject.Root);
            // unsubscribe Form from any subjects when Closed event will be fired
            m_View.Closed += (sender, args) => PanelSubscription.Instance.UnSubscribe(this, false);
        }

        public IList<ISubject> Groups
        {
            get
            {
                var result = new List<ISubject>();
                if (m_View.SelectedChecked)
                    foreach (string domain in m_View.Groups)
                        result.Add(new DomainPanelItem(domain));
                return result;
            }
            set
            {
                m_Groups.Clear();
                if (value != null)
                    foreach (var item in value)
                        m_Groups.Add(item.Subject);
                if (m_Groups.Count > 0)
                    m_View.SelectedChecked = true;
                else
                    m_View.DontScanChecked = true;
            }
        }

        private void View_OKClicked(object sender, EventArgs e)
        {
            m_Info.Groups = Groups;
            m_Info.UpdateSubsctiption();
        }

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            if (m_View == null)
                throw new Exception("View is null");
            // do not update list after unsubscribe
            if (subject == ConcreteSubject.NotSubscribed) return;
            IList<string> Saved;
            if (m_View.DomainsCount == 0)
                Saved = m_Groups;
            else
                Saved = m_View.GetCheckedList();
            string FocusedText = m_View.DomainsFocusedText;
            m_View.DomainsClear();
            foreach (var PItem in sender.GetListBySubject(subject))
            {
                var domain = PItem as DomainPanelItem;
                if (domain == null) continue;
                m_View.DomainsAdd(domain.Name, Saved.Contains(domain.Name));
            }
            m_View.DomainsFocusedText = FocusedText;
        }
    }
}
