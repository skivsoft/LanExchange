using System;
using System.Collections.Generic;
using LanExchange.Model;
using LanExchange.Model.Panel;
using LanExchange.Sdk;

namespace LanExchange.Presenter
{
    public class TabSettingsPresenter : ITabSettingsPresenter
    {
        private ITabSettingView m_View;
        private IPanelModel m_Info;
        private readonly IList<string> m_Groups;

        public TabSettingsPresenter()
        {
            m_Groups = new List<string>();
        }

        public void SetView(ITabSettingView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            m_View = view;
            m_View.OkClicked += View_OKClicked;
        }

        public void SetInfo(IPanelModel info)
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
            m_View.Closed += (sender, args) => PanelSubscription.Instance.Unsubscribe(this, false);
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
                    m_View.DoNotScanChecked = true;
            }
        }

        private void View_OKClicked(object sender, EventArgs e)
        {
            m_Info.Groups.Clear();
            foreach(var item in Groups)
                m_Info.Groups.Add(item);
            m_Info.UpdateSubscription();
        }

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            if (m_View == null)
                throw new Exception("View is null");
            // do not update list after unsubscribe
            if (Equals(subject, ConcreteSubject.NotSubscribed)) return;
            IList<string> Saved;
            if (m_View.DomainsCount == 0)
                Saved = m_Groups;
            else
                Saved = m_View.CheckedList;
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
