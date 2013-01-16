
using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using NLog;
using LanExchange.Utils;
using System.IO;
using System.Collections;
using LanExchange.Strategy;

namespace LanExchange.Model
{
    public class ServerListSubscription : ISubscription
    {
        #region Static fields and methods

        private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private static ServerListSubscription m_Instance;

        public static ISubscription Instance
        {
            get
            {
                if (m_Instance == null) 
                    m_Instance = new ServerListSubscription();
                return m_Instance;
            }
        }
        #endregion

        private readonly IDictionary<string, IList<ISubscriber>> m_Subjects;
        private readonly IDictionary<string, IList<ServerInfo>> m_Results;

        private int m_RefreshInterval;
        private readonly Timer m_RefreshTimer;
        private bool m_InstantUpdate = true;

        protected ServerListSubscription()
        {
            // lists
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, IList<ServerInfo>>();
            // load cached results
            LoadResultsFromCache();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += RefreshTimer_Tick;
            m_RefreshTimer.Enabled = false;
        }

        //public bool IsInstantUpdate
        //{
        //    get { return m_InstantUpdate; }
        //}

        //public bool IsBusy
        //{
        //    get { return BackgroundWorkers.Instance.IsBusy; }
        //}

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
            logger.Info("RefreshTimer.Tick() executed. Next tick in {0} sec.", m_RefreshInterval/1000);
            // prepare workers to launch
            foreach (var Pair in m_Subjects)
            {
                bool subjectFound = false;
                foreach(BackgroundContext ctx in BackgroundWorkers.Instance.EnumContexts())
                    if (ctx.Strategy is SubscriptionAbstractStrategy)
                    {
                        var sub = ctx.Strategy as SubscriptionAbstractStrategy;
                        if (sub.Subject.Equals(Pair.Key))
                        {
                            subjectFound = true;
                            break;
                        }
                    }
                if (!subjectFound)
                {
                    var worker = CreateOneWorker();
                    var context = new BackgroundContext(new NetServerEnumStrategy(Pair.Key));
                    BackgroundWorkers.Instance.Add(context, worker);
                    worker.RunWorkerAsync(context);
                }
            }
        }

        private BackgroundWorkerEx CreateOneWorker()
        {
            var Result = new BackgroundWorkerEx();
            Result.DoWork += OneWorker_DoWork;
            Result.RunWorkerCompleted += OneWorker_RunWorkerCompleted;
            return Result;
        }

        private bool SetResult(string domain, IList<ServerInfo> list)
        {
            bool bModified = false;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(domain))
                {
                    bModified = true;
                    m_Results.Add(domain, list);
                }
                if (!bModified)
                {
                    bModified = SortedListIsModified(m_Results[domain], list);
                    if (bModified)
                        m_Results[domain] = list;
                }
            }
            if (bModified)
                SaveResultsToCache();
            return bModified;
        }

        /// <summary>
        /// Browsing network process.
        /// This method virtual for unit-tests.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var context = e.Argument as BackgroundContext;
            if (context == null)
            {
                e.Cancel = true;
                return;
            }
            context.ExecuteOperation();
            e.Result = null;
            if (context.Strategy is NetServerEnumStrategy)
            {
                var strategy = (context.Strategy as NetServerEnumStrategy);
                if (SetResult(strategy.Subject, strategy.Result))
                    e.Result = strategy.Subject;
                else
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Browsing network complete. If serveinfo list is not modified e.Cancel set to False.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) return;
            var Subject = (string)e.Result;
            // notify all subscribers for current subject
            lock (m_Subjects)
            {
                if (m_Subjects.ContainsKey(Subject))
                {
                    var List = m_Subjects[Subject];
                    if (List.Count > 0)
                    {
                        logger.Info("Notify {0} subscriber(s) with subject \"{1}\"", List.Count, Subject);
                        foreach (var Subscriber in List)
                            Subscriber.DataChanged(this, Subject);
                    }
                }
            }
        }

        private static bool SortedListIsModified(IList<ServerInfo> listA, IList<ServerInfo> listB)
        {
            if (listA == null || listB == null)
                return false;
            bool bModified = false;
            if (listA.Count != listB.Count)
                bModified = true;
            else
                for (int i = 0; i < listA.Count - 1; i++)
                    if (listA[i].CompareTo(listB[i]) != 0)
                    {
                        bModified = true;
                        break;
                    }
            return bModified;
        }

        private static string GetCacheFileName()
        {
            return Path.ChangeExtension(Settings.GetExecutableFileName(), ".cache");
        }


        private void SaveResultsToCache()
        {
            var fileName = GetCacheFileName();
            logger.Info("SaveResultsToCache(\"{0}\")", fileName);
            lock (m_Results)
            {
                var Temp = new Dictionary<string, NetApi32.SERVER_INFO_101[]>();
                foreach (var Pair in m_Results)
                {
                    var TempList = new NetApi32.SERVER_INFO_101[Pair.Value.Count];
                    for (int i = 0; i < Pair.Value.Count; i++)
                        TempList[i] = Pair.Value[i].GetInfo();
                    Temp.Add(Pair.Key, TempList);
                }
                try
                {
                    SerializeUtils.SerializeObjectToBinaryFile(fileName, Temp);
                }
                catch (Exception E)
                {
                    logger.Error("SaveResultsToCache: {0}", E.Message);
                }
            }
        }

        private void LoadResultsFromCache()
        {
            var fileName = GetCacheFileName();
            if (!File.Exists(fileName)) return;
            logger.Info("LoadResultsFromCache(\"{0}\")", fileName);
            lock (m_Results)
            {
                Dictionary<string, NetApi32.SERVER_INFO_101[]> Temp = null;
                try
                {
                    Temp = (Dictionary<string, NetApi32.SERVER_INFO_101[]>)SerializeUtils.DeserializeObjectFromBinaryFile(fileName);
                }
                catch (Exception E)
                {
                    logger.Error("LoadResultsFromCache: {0}", E.Message);
                }
                if (Temp != null)
                {
                    m_Results.Clear();
                    foreach (var Pair in Temp)
                    {
                        var TempList = new List<ServerInfo>();
                        Array.ForEach(Pair.Value, info => TempList.Add(new ServerInfo(info)));
                        m_Results.Add(Pair.Key, TempList);
                        TempList.Sort();
                    }
                }
            }
        }

        public bool HasSubscribers()
        {
            foreach (var Pair in m_Subjects)
                if (Pair.Value.Count > 0)
                    return true;
            return false;
        }

        private void SubscribersChanged()
        {
            bool Found = HasSubscribers();
            if (!Found)
            {
                logger.Info("RefreshTimer stopped.");
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
                    logger.Info("RefreshTimer started. Next tick in {0} sec.", m_RefreshInterval/1000);
                }
            }
        }

        #region ISubscription interface
        public void SubscribeToSubject(ISubscriber sender, string subject)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");
            if (subject == null)
                throw new ArgumentNullException("subject");
            bool Modified = false;
            if (m_Subjects.ContainsKey(subject))
            {
                var List = m_Subjects[subject];
                if (!List.Contains(sender))
                {
                    List.Add(sender);
                    Modified = true;
                }
            }
            else
            {
                var List = new List<ISubscriber>();
                List.Add(sender);
                m_Subjects.Add(subject, List);
                m_InstantUpdate = true;
                Modified = true;
            }
            if (Modified)
            {
                if (m_Results.ContainsKey(subject))
                {
                    logger.Info("Subject \"{0}\" already exists in cache.", subject);
                    sender.DataChanged(this, subject);
                }
                SubscribersChanged();
            }
        }

        public void UnSubscribe(ISubscriber sender)
        {
            if (sender == null)
                throw new ArgumentNullException("sender");
            bool Modified = false;
            foreach (var Pair in m_Subjects)
            {
                if (Pair.Value.Contains(sender))
                {
                    Pair.Value.Remove(sender);
                    Modified = true;
                }
            }
            if (Modified)
            {
                sender.DataChanged(this, null);
                SubscribersChanged();
            }
        }
        #endregion
        public IEnumerable<KeyValuePair<string, IList<ISubscriber>>> GetSubjects()
        {
            return m_Subjects;
        }


        public IEnumerable GetListBySubject(string subject)
        {
            if (subject == null)
                yield break;
            lock (m_Results)
            {
                if (!m_Results.ContainsKey(subject))
                    yield break;
                foreach (var si in m_Results[subject])
                    yield return si;
            }
        }
    }
}
