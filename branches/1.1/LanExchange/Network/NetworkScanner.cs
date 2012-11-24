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

        private int m_RefreshInterval = 0;
        private bool m_EnabledAll = true;
        private Timer m_RefreshTimer = null;
        private BackgroundWorkerList m_Workers = null;
        private bool m_InstantUpdate = true;
        private int LockCount = 0;

        public NetworkScanner()
        {
            // lists
            m_AllSubjects = new List<ISubscriber>();
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, IList<ServerInfo>>();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += new EventHandler(RefreshTimer_Tick);
            m_RefreshTimer.Enabled = false;
            // worker for scanning network
            m_Workers = new BackgroundWorkerList();
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

        private BackgroundWorker CreateOneWorker()
        {
            BackgroundWorker Result = new BackgroundWorker();
            Result.WorkerSupportsCancellation = true;
            Result.DoWork += new DoWorkEventHandler(OneWorker_DoWork);
            Result.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OneWorker_RunWorkerCompleted);
            return Result;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            m_Workers.ClearNotBusy();
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
                    foreach (var domain in Utils.GetDomainList())
                        if (!m_Subjects.ContainsKey(domain))
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

        private void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string domain = (string)e.Argument;
            logger.Info("DoWork({0})", domain);
            
            DataChangedEventArgs args = new DataChangedEventArgs();
            args.Subject = domain;
            args.Data = Utils.GetComputerList(domain);
            e.Result = args;
        }

        private void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                if (m_Subjects.ContainsKey(Result.Subject))
                {
                    IList<ISubscriber> List = m_Subjects[Result.Subject];
                    logger.Info("Notify {0} subscriber(s) [one subject]", List.Count);
                    foreach(var Subscriber in List)
                        Subscriber.DataChanged(this, Result);
                }
                if (m_AllSubjects.Count > 0)
                {
                    logger.Info("Notify {0} subscriber(s) [all subjects]", m_AllSubjects.Count);
                    foreach(var Subscriber in m_AllSubjects)
                        Subscriber.DataChanged(this, Result);
                }
            }
        }

        private bool SetResult(string Domain, IList<ServerInfo> List)
        {
            bool bModified = true;
            return bModified;
        }

        private bool HasSubscribers()
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
        #endregion
    }
}
