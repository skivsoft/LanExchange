#define __REMOVE_RANDOM_COMPS

using System;
using System.Collections.Generic;

using System.ComponentModel;
using System.Windows.Forms;
using NLog;
using LanExchange.Utils;
using System.IO;
using System.Collections;

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
                {
                    m_Instance = new ServerListSubscription();
                }
                return m_Instance;
            }
        }
        #endregion

        private readonly IDictionary<string, IList<ISubscriber>> m_Subjects;
        private readonly IDictionary<string, IList<ServerInfo>> m_Results;

        private int m_RefreshInterval;
        private readonly Timer m_RefreshTimer;
        private readonly BackgroundWorkerList m_Workers;
        private bool m_InstantUpdate = true;

        protected ServerListSubscription()
        {
            // lists
            m_Subjects = new Dictionary<string, IList<ISubscriber>>();
            m_Results = new Dictionary<string, IList<ServerInfo>>();
            // load cached results
            LoadResultFromCache();
            // timer
            m_RefreshTimer = new Timer();
            m_RefreshTimer.Tick += RefreshTimer_Tick;
            m_RefreshTimer.Enabled = false;
            // worker list for scanning network
            m_Workers = new BackgroundWorkerList();
        }

        public bool IsInstantUpdate
        {
            get { return m_InstantUpdate; }
        }

        public bool IsBusy
        {
            get { return m_Workers.IsBusy; }
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
            logger.Info("RefreshTimer.Tick() executed. Next tick in {0} sec.", (int)(m_RefreshInterval/1000));
            if (!m_Workers.IsBusy)
            {
                // prepare workers to launch
                foreach (var Pair in m_Subjects)
                {
                    if (!m_Workers.Exists(Pair.Key))
                        m_Workers.Add(Pair.Key, CreateOneWorker());
                }
                // launch!
                m_Workers.RunWorkerAsync();
            }
            else
                logger.Info("Tick: {0} of {1} worker(s) busy, no action", m_Workers.BusyCount, m_Workers.Count);
        }

        private BackgroundWorker CreateOneWorker()
        {
            var Result = new BackgroundWorker();
            Result.DoWork += OneWorker_DoWork;
            Result.RunWorkerCompleted += OneWorker_RunWorkerCompleted;
            return Result;
        }

        /// <summary>
        /// This method must return server list instantly. Use cache is preferred.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        private IList<ServerInfo> GetResultNow(string domain, bool cachePref, out bool Modified)
        {
            NetApi32.SERVER_INFO_101[] List;
            IList<ServerInfo> Result;
            if (cachePref && m_Results.ContainsKey(domain))
            {
                logger.Info("GetFromCache(domain:{0})", domain);
                Result = m_Results[domain];
                Modified = true;
            }
            else
            {
                // get server list via OS api
                if (String.IsNullOrEmpty(domain))
                    List = NetApi32Utils.GetDomainList();
                else
                    List = NetApi32Utils.GetComputerList(domain);
                // convert array to IList<ServerInfo>
                Result = new List<ServerInfo>();
                Array.ForEach(List, item =>
                {
                    Result.Add(new ServerInfo(item));
                });
                Modified = SetResult(domain, Result);
            }
            return Result;
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
                    var ResultList = m_Results[Domain];
                    bModified = SortedListIsModified(m_Results[Domain], List);
                    if (bModified)
                        m_Results[Domain] = List;
                }
            }
            if (bModified)
            {
                SaveResultToCache();
            }
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
            string domain = (string)e.Argument;
            bool Modified;
            var List = GetResultNow(domain, false, out Modified);
            #if REMOVE_RANDOM_COMPS
            var R = new Random();
            int Count = R.Next(List.Count * 2 / 3);
            for (int i = 0; i < Count; i++)
            {
                int Index = R.Next(List.Count);
                List.RemoveAt(Index);
            }
            logger.Info(String.Format("Random comps removed: {0}", List.Count));
            #endif
            if (Modified)
                e.Result = domain;
            else
                e.Result = null;
        }

        /// <summary>
        /// Browsing network complete. If serveinfo list is not modified e.Cancel set to False.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OneWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var Subject = (string)e.Result;
            if (e.Cancelled || Subject == null)
                return;
            logger.Info("Completed(domain:{0})", Subject);
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

        private static bool SortedListIsModified(IList<ServerInfo> ListA, IList<ServerInfo> ListB)
        {
            if (ListA == null || ListB == null)
                return false;
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

        private static string GetCacheFileName()
        {
            return Path.ChangeExtension(Settings.GetExecutableFileName(), ".cache");
        }


        private void SaveResultToCache()
        {
            logger.Info("SaveResultToCache()");
            var Temp = new Dictionary<string, NetApi32.SERVER_INFO_101[]>();
            foreach (var Pair in m_Results)
            {
                NetApi32.SERVER_INFO_101[] TempList = new NetApi32.SERVER_INFO_101[Pair.Value.Count];
                for (int i = 0; i < Pair.Value.Count; i++)
                    TempList[i] = Pair.Value[i].GetInfo();
                Temp.Add(Pair.Key, TempList);
            }
            try
            {
                SerializeUtils.SerializeObjectToBinaryFile(GetCacheFileName(), Temp);
            }
            catch { }
        }

        private void LoadResultFromCache()
        {
            logger.Info("LoadResultFromCache()");
            try
            {
                var Temp = (Dictionary<string, NetApi32.SERVER_INFO_101[]>)SerializeUtils.DeserializeObjectFromBinaryFile(GetCacheFileName());
                lock (m_Results)
                {
                    m_Results.Clear();
                    foreach (var Pair in Temp)
                    {
                        var TempList = new List<ServerInfo>();
                        Array.ForEach(Pair.Value, Info => TempList.Add(new ServerInfo(Info)));
                        m_Results.Add(Pair.Key, TempList);
                        TempList.Sort();
                    }
                }
            }
            catch { }
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
                    logger.Info("RefreshTimer started. Next tick in {0} sec.", (int)(m_RefreshInterval/1000));
                }
            }
        }

        #region ISubscription interface
        public void SubscribeToSubject(ISubscriber sender, string subject)
        {
            if (sender == null)
                return;
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
                Modified = true;
            }
            if (Modified)
            {
                bool ResultsModified;
                var Result = GetResultNow(subject, true, out ResultsModified);
                sender.DataChanged(this, subject);
                SubscribersChanged();
            }
        }

        public void UnSubscribe(ISubscriber sender)
        {
            if (sender == null)
                return;
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
        public IDictionary<string, IList<ISubscriber>> GetSubjects()
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
                foreach (var SI in m_Results[subject])
                    yield return SI;
            }
        }
    }
}
