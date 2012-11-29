#define REMOVE_RANDOM_COMPS

using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.Windows.Forms;


namespace LanExchange.Network
{
    public class NetworkScanner : ISubscriptionProvider
    {
        #region Static fields and methods

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private static NetworkScanner m_Instance = null;

        public static NetworkScanner GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new NetworkScanner();
            }
            return m_Instance;
        }
        #endregion

        private IDictionary<string, IList<ISubscriber>> m_Subjects = null;
        private IList<ISubscriber> m_AllSubjects = null;
        private IDictionary<string, IList<ServerInfo>> m_Results = null;
        private IList<ServerInfo> m_Domains = null;
        private BackgroundWorker m_DomainsWorker = null;

        private int m_RefreshInterval = 0;
        private bool m_EnabledAll = true;
        private Timer m_RefreshTimer = null;
        private BackgroundWorkerList m_Workers = null;
        private bool m_InstantUpdate = true;
        private int LockCount = 0;

        public event EventHandler DomainListChanged;

        protected NetworkScanner()
        {
            // lists
            m_AllSubjects = new List<ISubscriber>();
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, IList<ServerInfo>>();
            m_Domains = new List<ServerInfo>();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            m_RefreshTimer.Enabled = false;
            // worker list for scanning network
            m_Workers = new BackgroundWorkerList();
            // worker for getting domain list
            m_DomainsWorker = CreateDomainsWorker();
            m_DomainsWorker.RunWorkerAsync();
        }

        public bool IsInstantUpdate
        {
            get { return m_InstantUpdate; }
        }

        public bool IsBusy
        {
            get { return m_Workers.IsBusy; }
        }

        public IList<ServerInfo> DomainList
        {
            get { return m_Domains; }
        }

        public int RefreshInterval
        {
            get { return m_RefreshInterval; }
            set
            {
                m_RefreshInterval = value;
                m_RefreshTimer.Interval = value;
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!m_DomainsWorker.IsBusy)
                m_DomainsWorker.RunWorkerAsync();
            if (!m_Workers.IsBusy)
            {
                m_Workers.ClearNotBusy();
                // prepare workers to launch
                foreach (var Pair in m_Subjects)
                {
                    if (!m_Workers.Exists(Pair.Key))
                        m_Workers.Add(Pair.Key, CreateOneWorker());
                }
                if (m_AllSubjects.Count > 0)
                {
                    foreach (var domain in m_Domains)
                        if (!m_Subjects.ContainsKey(domain.Name))
                        {
                            if (!m_Workers.Exists(domain))
                                m_Workers.Add(domain, CreateOneWorker());
                        }
                }
                // launch!
                m_Workers.RunWorkerAsync();
            }
            else
            {
                logger.Info("Tick: {0} of {1} worker(s) busy, no action", m_Workers.BusyCount, m_Workers.Count);
            }
        }

        private BackgroundWorker CreateDomainsWorker()
        {
            BackgroundWorker Result = new BackgroundWorker();
            Result.DoWork += new DoWorkEventHandler(DomainsWorker_DoWork);
            Result.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DomainsWorker_RunWorkerCompleted);
            return Result;
        }

        private void DomainsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            logger.Info("GetDomainList");
            e.Result = Utils.GetDomainList();
        }

        private void DomainsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool bModified;
            lock (m_DomainsWorker)
            {
                bModified = SortedListIsModified(m_Domains, (IList<ServerInfo>)e.Result);
                bModified = true;
                if (bModified)
                    m_Domains = (IList<ServerInfo>)e.Result;
            }
            if (DomainListChanged != null && bModified)
                DomainListChanged(this, new EventArgs());
        }

        private BackgroundWorker CreateOneWorker()
        {
            BackgroundWorker Result = new BackgroundWorker();
            Result.DoWork += new DoWorkEventHandler(OneWorker_DoWork);
            Result.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OneWorker_RunWorkerCompleted);
            return Result;
        }

        protected virtual void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string domain = (string)e.Argument;
            logger.Info("DoWork({0})", domain);

            IList<ServerInfo> List = Utils.GetComputerList(domain);
            logger.Info(String.Format("NetServerEnum: {0}", List.Count));
            #if REMOVE_RANDOM_COMPS
            Random R = new Random();
            int Count = R.Next(List.Count * 2 / 3);
            for (int i = 0; i < Count; i++)
            {
                int Index = R.Next(List.Count);
                List.RemoveAt(Index);
            }
            logger.Info(String.Format("Random comps removed: {0}", List.Count));
            #endif
            DataChangedEventArgs args = new DataChangedEventArgs();
            args.Subject = domain;
            args.Data = List;
            e.Result = args;
        }

        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DataChangedEventArgs Result = (DataChangedEventArgs)e.Result;
            if (e.Cancelled || !m_EnabledAll)
            {
                logger.Info("Cancelled({0})", Result.Subject);
                return;
            }
            logger.Info("Completed({0})", Result.Subject);

            bool bModified = SetResult(Result.Subject, (IList<ServerInfo>)Result.Data);
            if (bModified)
            {
                lock (m_Subjects)
                {
                    if (m_Subjects.ContainsKey(Result.Subject))
                    {
                        IList<ISubscriber> List = m_Subjects[Result.Subject];
                        logger.Info("Notify {0} subscriber(s) [one subject]", List.Count);
                        foreach (var Subscriber in List)
                            Subscriber.DataChanged(this, Result);
                    }
                }
                lock (m_AllSubjects)
                {
                    if (m_AllSubjects.Count > 0)
                    {
                        logger.Info("Notify {0} subscriber(s) [all subjects]", m_AllSubjects.Count);
                        foreach (var Subscriber in m_AllSubjects)
                            Subscriber.DataChanged(this, Result);
                    }
                }
            }
        }

        private bool SortedListIsModified(IList<ServerInfo> ListA, IList<ServerInfo> ListB)
        {
            bool bModified = false;
            if (ListA.Count != ListB.Count)
                bModified = true;
            else
                for (int i = 0; i < ListA.Count - 1; i++)
                    if (ListA[i].CompareTo(ListB[i]) != 0)
                    {
                        bModified = true;
                        break;
                    }
            return bModified;
        }

        private bool SetResult(string Domain, IList<ServerInfo> List)
        {
            bool bModified = false;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(Domain))
                {
                    bModified = true;
                    m_Results.Add(Domain, List);
                }
                if (!bModified)
                {
                    IList<ServerInfo> ResultList = m_Results[Domain];
                    bModified = SortedListIsModified(m_Results[Domain], List);
                    if (bModified)
                        m_Results[Domain] = List;
                }
            }
            return bModified;
        }

        public bool HasSubscribers()
        {
            bool Found = false;
            if (m_AllSubjects.Count > 0)
                Found = true;
            else
                foreach (var Pair in m_Subjects)
                {
                    if (Pair.Value.Count > 0)
                    {
                        Found = true;
                        break;
                    }
                }
            return Found;
        }

        private void SubscribersChanged()
        {
            if (LockCount == 0)
            {
                bool Found = HasSubscribers();
                if (!Found || !m_EnabledAll)
                {
                    m_RefreshTimer.Enabled = false;
                    m_InstantUpdate = true;
                }
                else
                {
                    if (m_InstantUpdate)
                    {
                        m_RefreshTimer.Enabled = false;
                        RefreshTimer_Tick(m_RefreshTimer, new EventArgs());
                        m_RefreshTimer.Enabled = true;
                        m_InstantUpdate = false;
                    }
                }
            }
        }

        #region ISubscriptionProvider interface
        public void SubscribeToSubject(ISubscriber sender, string subject)
        {
            if (m_Subjects.ContainsKey(subject))
            {
                IList<ISubscriber> List = m_Subjects[subject];
                if (!List.Contains(sender))
                {
                    List.Add(sender);
                    SubscribersChanged();
                }
            }
            else
            {
                List<ISubscriber> List = new List<ISubscriber>();
                List.Add(sender);
                m_Subjects.Add(subject, List);
                SubscribersChanged();
            }
        }

        public void SubscribeToAll(ISubscriber sender)
        {
            if (!m_AllSubjects.Contains(sender))
            {
                LockCount++;
                UnSubscribe(sender);
                LockCount--;
                m_AllSubjects.Add(sender);
                SubscribersChanged();
            }
        }

        public void UnSubscribe(ISubscriber sender)
        {
            bool Modified = false;
            if (m_AllSubjects.Contains(sender))
            {
                m_AllSubjects.Remove(sender);
                Modified = true;
            }
            foreach (var Pair in m_Subjects)
            {
                if (Pair.Value.Contains(sender))
                {
                    Pair.Value.Remove(sender);
                    Modified = true;
                }
            }
            if (Modified)
                SubscribersChanged();
        }

        public void EnableSubscriptions()
        {
            m_EnabledAll = true;
            SubscribersChanged();
        }

        public void DisableSubscriptions()
        {
            m_EnabledAll = false;
            SubscribersChanged();
        }

        public IDictionary<string, IList<ISubscriber>> GetSubjects()
        {
            return m_Subjects;
        }

        public IList<ISubscriber> GetAllSubjects()
        {
            return m_AllSubjects;
        }
        #endregion
    }
}
