using System;
using System.Collections.Generic;
using System.Text;
using LanExchange.Interface;
using LanExchange.Model;
using LanExchange.Model.Panel;

namespace LanExchange.Presenter
{
    public class TabParamsPresenter : IPresenter, ISubscriber
    {
        private readonly ITabParamsView m_View;
        private readonly IList<string> m_Groups;

        public TabParamsPresenter(ITabParamsView view)
        {
            m_View = view;
            m_Groups = new List<string>();
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

        public void DataChanged(ISubscription sender, ISubject subject)
        {
            //// do not update list after unsubscribe
            //if (subject == ConcreteSubject.NotSubscribed) return;
            //IList<string> Saved;
            //if (lvDomains.Items.Count == 0)
            //    Saved = m_Groups;
            //else
            //    Saved = ListViewUtils.GetCheckedList(lvDomains);
            //string FocusedText = null;
            //if (lvDomains.FocusedItem != null)
            //    FocusedText = lvDomains.FocusedItem.Text;
            //lvDomains.Items.Clear();
            //int index = 0;
            //foreach (var PItem in sender.GetListBySubject(subject))
            //{
            //    var domain = PItem as DomainPanelItem;
            //    if (domain == null) continue;
            //    var LVI = new ListViewItem(domain.Name);
            //    if (Saved.Contains(domain.Name))
            //        LVI.Checked = true;
            //    lvDomains.Items.Add(LVI);
            //    if (FocusedText != null && String.CompareOrdinal(domain.Name, FocusedText) == 0)
            //    {
            //        lvDomains.FocusedItem = lvDomains.Items[index];
            //        lvDomains.FocusedItem.Selected = true;
            //    }
            //    index++;
            //}
        }
    }
}
